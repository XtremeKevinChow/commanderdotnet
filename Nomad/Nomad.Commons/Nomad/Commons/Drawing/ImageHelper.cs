﻿namespace Nomad.Commons.Drawing
{
    using Microsoft;
    using Microsoft.Win32;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;

    public static class ImageHelper
    {
        public static readonly Size DefaultLargeIconSize = new Size(0x20, 0x20);
        public static readonly Size DefaultSmallIconSize = new Size(0x10, 0x10);

        public static Bitmap CreateBlendImage(Image source, System.Drawing.Color blendColor, float blendLevel)
        {
            Bitmap image = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                DrawBlendImage(graphics, source, blendColor, blendLevel, 0, 0);
            }
            return image;
        }

        public static void DrawBlendImage(Graphics canvas, Image source, System.Drawing.Color blendColor, float blendLevel, int x, int y)
        {
            Rectangle destRect = new Rectangle(x, y, source.Width, source.Height);
            ColorMatrix newColorMatrix = new ColorMatrix {
                Matrix00 = 0f,
                Matrix11 = 0f,
                Matrix22 = 0f,
                Matrix40 = ((float) blendColor.R) / 255f,
                Matrix41 = ((float) blendColor.G) / 255f,
                Matrix42 = ((float) blendColor.B) / 255f
            };
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            ColorMatrix matrix2 = new ColorMatrix {
                Matrix33 = blendLevel
            };
            ImageAttributes attributes2 = new ImageAttributes();
            attributes2.SetColorMatrix(matrix2, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            canvas.DrawImage(source, destRect, 0, 0, source.Width, source.Height, GraphicsUnit.Pixel, imageAttr);
            canvas.DrawImage(source, destRect, 0, 0, source.Width, source.Height, GraphicsUnit.Pixel, attributes2);
        }

        public static Bitmap FromHbitmapWithAlpha(IntPtr hbm)
        {
            if (hbm == IntPtr.Zero)
            {
                throw new ArgumentException();
            }
            Microsoft.Win32.BITMAP lpObject = new Microsoft.Win32.BITMAP();
            if (Windows.GetObjectBitmap(hbm, Marshal.SizeOf(lpObject), ref lpObject) != 0)
            {
                Bitmap bitmap2;
                int bmWidth = lpObject.bmWidth;
                int bmHeight = lpObject.bmHeight;
                if ((lpObject.bmBits != IntPtr.Zero) && (lpObject.bmBitsPixel == 0x20))
                {
                    bitmap2 = new Bitmap(bmWidth, bmHeight, PixelFormat.Format32bppArgb);
                    BitmapData bitmapdata = bitmap2.LockBits(new Rectangle(0, 0, bitmap2.Width, bitmap2.Height), ImageLockMode.WriteOnly, bitmap2.PixelFormat);
                    Debug.Assert(lpObject.bmWidthBytes == bitmapdata.Stride);
                    byte[] destination = new byte[bitmapdata.Stride];
                    IntPtr bmBits = lpObject.bmBits;
                    IntPtr ptr2 = bitmapdata.Scan0;
                    for (int i = 0; i < bmHeight; i++)
                    {
                        Marshal.Copy(bmBits, destination, 0, destination.Length);
                        Marshal.Copy(destination, 0, ptr2, destination.Length);
                        bmBits = bmBits.Offset(destination.Length);
                        ptr2 = ptr2.Offset(destination.Length);
                    }
                    bitmap2.UnlockBits(bitmapdata);
                    return bitmap2;
                }
                if (OS.IsWin2k)
                {
                    bitmap2 = new Bitmap(bmWidth, bmHeight, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics = Graphics.FromImage(bitmap2))
                    {
                        IntPtr hdc = graphics.GetHdc();
                        IntPtr ptr4 = Windows.CreateCompatibleDC(hdc);
                        IntPtr hgdiobj = Windows.SelectObject(ptr4, hbm);
                        BLENDFUNCTION blendFunction = new BLENDFUNCTION(0, 0, 0xff, 1);
                        Windows.AlphaBlend(hdc, 0, 0, bmWidth, bmHeight, ptr4, 0, 0, bmWidth, bmHeight, blendFunction);
                        Windows.SelectObject(ptr4, hgdiobj);
                        Windows.DeleteDC(ptr4);
                        graphics.ReleaseHdc(hdc);
                    }
                    return bitmap2;
                }
            }
            return Image.FromHbitmap(hbm);
        }

        private static double GetColorDelta(System.Drawing.Color x, System.Drawing.Color y)
        {
            int num = (x.R + y.R) / 2;
            int num2 = x.R - y.R;
            int num3 = x.G - y.G;
            int num4 = x.B - y.B;
            return Math.Sqrt((double) ((((((0x200 + num) * num2) * num2) >> 8) + ((4 * num3) * num3)) + ((((0x2ff - num) * num4) * num4) >> 8)));
        }

        public static Size GetThumbnailSize(Size currentSize, Size maxThumbnailSize)
        {
            int width = maxThumbnailSize.Width;
            int height = (width * currentSize.Height) / currentSize.Width;
            if (height > maxThumbnailSize.Height)
            {
                height = maxThumbnailSize.Height;
                width = (height * currentSize.Width) / currentSize.Height;
            }
            return new Size(width, height);
        }

        public static Bitmap IconToBitmap(IntPtr hIcon)
        {
            if (hIcon == IntPtr.Zero)
            {
                return null;
            }
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                using (Icon icon = Icon.FromHandle(hIcon))
                {
                    return IconToBitmap(icon, icon.Size);
                }
            }
            return Bitmap.FromHicon(hIcon);
        }

        public static Bitmap IconToBitmap(Icon icon, Size size)
        {
            Bitmap image = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.Clear(System.Drawing.Color.Transparent);
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.DrawIcon(icon, new Rectangle(0, 0, size.Width, size.Height));
            }
            return image;
        }

        public static bool IsCloseColors(System.Drawing.Color x, System.Drawing.Color y)
        {
            double colorDelta = GetColorDelta(x, y);
            return ((colorDelta < 100.0) || ((colorDelta < 210.0) && (Math.Abs((float) (x.GetBrightness() - y.GetBrightness())) < 0.1)));
        }

        public static System.Drawing.Color MergeColors(System.Drawing.Color x, System.Drawing.Color y)
        {
            if (y.IsEmpty || (y.A == 0))
            {
                return x;
            }
            if (y.A == 0xff)
            {
                return y;
            }
            float num = ((float) y.A) / 255f;
            return System.Drawing.Color.FromArgb((int) (((1f - num) * x.R) + (y.R * num)), (int) (((1f - num) * x.G) + (y.G * num)), (int) (((1f - num) * x.B) + (y.B * num)));
        }

        public static Image MergeImages(params Image[] images)
        {
            if (images == null)
            {
                return null;
            }
            if (images.Length == 1)
            {
                return images[0];
            }
            Bitmap bitmap = new Bitmap(images[0].Size.Width, images[0].Size.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                foreach (Image image in images)
                {
                    graphics.DrawImage(image, 0, 0);
                }
            }
            return bitmap;
        }
    }
}


﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Shell
{
    public abstract class ShellItem
    {
        protected IShellItem item;
        protected IntPtr pidl;
        private string name;
        private string path;
        private int? imageIndex;


        protected ShellItem(IShellItem item, IntPtr pidl)
        {
            this.item = item;
            this.pidl = pidl;
        }


        public virtual string Name { get { return name ?? (name = GetName()); } }

        public virtual string Path { get { return path ?? (path = GetPath()); } }

        //public int ImageIndex { get { return (imageIndex ?? (imageIndex = GetImageIndex())).Value; } }

        public abstract bool IsFolder { get; }


        private string GetName()
        {
            string result;
            this.item.GetDisplayName(SIGDN.NormalDisplay, out result);
            return result;
        }

        private string GetPath()
        {            
            if (item != null)
            {
                try
                {
                    string result;
                    this.item.GetDisplayName(SIGDN.FileSysPath, out result);
                    return result;
                }
                catch (Exception)
                { 
                }
            }

            return string.Empty;
        }

        private IShellItem GetParent()
        {
            IShellItem result;
            this.item.GetParent(out result);
            return result;
        }

        private IShellFolder ToIShellFolder()
        {
            IShellFolder result;
            item.BindToHandler(IntPtr.Zero, ShellGuids.ShellFolderObject, ShellGuids.IShellFolder, out result);

            return result;
        }

        private SFGAO GetAttributes()
        {
            SFGAO result;
            this.item.GetAttributes(SFGAO.FileSystem | SFGAO.Folder, out result);
            return result;
        }

        public int GetImageIndex()
        {
            ShFileInfo info = new ShFileInfo();
            Shell32.SHGetFileInfo(pidl, 0, ref info, Marshal.SizeOf(info), SHGFI.Pidl | SHGFI.SysIconIndex | SHGFI.OverlayIndex | SHGFI.LargeIcon | SHGFI.AddOverlays | SHGFI.LinkOverlay);
            Shell32.DestroyIcon(info.IconHandle);

            return info.IconIndex;
        }
    }
}

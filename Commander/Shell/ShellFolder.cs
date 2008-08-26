﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ShellDll
{
    public static class ShellFolder
    {
        public static IntPtr GetPathPIDL(string path)
        {
            string parentDirectory = Path.GetDirectoryName(path);
            if (parentDirectory == null)
            {
                parentDirectory = SpecialFolderPath.MyComputer;
            }
            string fileName = Path.GetFileName(path);
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = path;
            }

            IShellFolder parentFolder = ShellFolder.GetShellFolder(parentDirectory);
            if (parentFolder != null)
            {
                uint pchEaten = 0;
                ShellAPI.SFGAO pdwAttributes = 0;
                IntPtr pidl = IntPtr.Zero;
                int result = parentFolder.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, fileName, ref pchEaten, out pidl, ref pdwAttributes);
                if (result == ShellAPI.S_OK)
                {
                    return pidl;
                }
            }

            return IntPtr.Zero;
        }

        public static IntPtr GetShellFolderIntPtr(string path)
        {
            IShellFolder desktopShellFolder = GetDesktopFolder();
            if (null == desktopShellFolder)
            {
                return IntPtr.Zero;
            }

            // Get the PIDL for the folder file is in
            IntPtr pidl = IntPtr.Zero;
            uint pchEaten = 0;
            ShellAPI.SFGAO pdwAttributes = 0;
            int result = desktopShellFolder.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, path, ref pchEaten, out pidl, ref pdwAttributes);
            if (ShellAPI.S_OK != result)
            {
                return IntPtr.Zero;
            }

            // Get the IShellFolder for folder
            IntPtr shellFolder = IntPtr.Zero;
            result = desktopShellFolder.BindToObject(pidl, IntPtr.Zero, ref ShellAPI.IID_IShellFolder, out shellFolder);
            // Free the PIDL first
            Marshal.FreeCoTaskMem(pidl);
            if (ShellAPI.S_OK != result)
            {
                return IntPtr.Zero;
            }

            return shellFolder;
        }

        public static IShellFolder GetShellFolder(string path)
        {
            IntPtr shellFolder = GetShellFolderIntPtr(path);

            return (IShellFolder)Marshal.GetTypedObjectForIUnknown(shellFolder, typeof(IShellFolder));
        }

        public static IShellFolder GetDesktopFolder()
        {
            IntPtr desktopFolder = IntPtr.Zero;
            int result = ShellAPI.SHGetDesktopFolder(out desktopFolder);
            IShellFolder shellFolder = (IShellFolder)Marshal.GetTypedObjectForIUnknown(desktopFolder, typeof(IShellFolder));

            return shellFolder;
        }
    }
}

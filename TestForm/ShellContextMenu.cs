﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Shell;

namespace TestForm
{
    public class ShellContextMenu : NativeWindow
    {
        private IContextMenu iContextMenu;
        private IContextMenu2 iContextMenu2, newContextMenu2;
        private IContextMenu3 iContextMenu3, newContextMenu3;
        private IntPtr newSubmenuPtr;


        public ShellContextMenu()
        {
            this.CreateHandle(new CreateParams());
        }


        public void Show(Point location, params string[] pathList)
        {
            IntPtr[] pidls = ShellHelper.GetPIDLs(pathList);
            IShellFolder parentShellFolder;
            string parentDirectory = null;
            if (pathList[0].StartsWith("::{"))
            {
                parentShellFolder = ShellHelper.GetDesktopFolder();
            }
            else
            {
                parentDirectory = ShellHelper.GetParentDirectoryPath(pathList[0]);
                parentShellFolder = ShellHelper.GetShellFolder(parentDirectory);
            }

            IntPtr contextMenu = IntPtr.Zero;
            IntPtr iContextMenuPtr = IntPtr.Zero;
            IntPtr iContextMenuPtr2 = IntPtr.Zero;
            IntPtr iContextMenuPtr3 = IntPtr.Zero;

            // Show / Invoke
            try
            {
                if (ContextMenuHelper.GetIContextMenu(parentShellFolder, pidls, out iContextMenuPtr, out iContextMenu))
                {
                    contextMenu = User32.CreatePopupMenu();
                    iContextMenu.QueryContextMenu(contextMenu, 0, ShellApi.CmdFirst, ShellApi.CmdLast, CMF.Explore | CMF.CanRename | ((Control.ModifierKeys & Keys.Shift) != 0 ? CMF.ExtendedVerbs : 0));

                    Marshal.QueryInterface(iContextMenuPtr, ref ShellGuids.IContextMenu2, out iContextMenuPtr2);
                    Marshal.QueryInterface(iContextMenuPtr, ref ShellGuids.IContextMenu3, out iContextMenuPtr3);

                    try
                    {
                        iContextMenu2 = (IContextMenu2) Marshal.GetTypedObjectForIUnknown(iContextMenuPtr2, typeof (IContextMenu2));

                        iContextMenu3 = (IContextMenu3) Marshal.GetTypedObjectForIUnknown(iContextMenuPtr3, typeof (IContextMenu3));
                    }
                    catch (Exception)
                    {
                    }

                    uint selected = User32.TrackPopupMenuEx(contextMenu, TPM.ReturnCmd, location.X, location.Y, this.Handle, IntPtr.Zero);


                    if (selected >= ShellApi.CmdFirst)
                    {
                        string command = ContextMenuHelper.GetCommandString(iContextMenu, selected - ShellApi.CmdFirst, true);

                        if (command == "Explore")
                        {
                            /*if (!br.FolderView.SelectedNode.IsExpanded)
                                br.FolderView.SelectedNode.Expand();

                            br.FolderView.SelectedNode = br.FolderView.SelectedNode.Nodes[hitTest.Item.Text];*/
                        }
                        else if (command == "rename")
                        {
                            /*hitTest.Item.BeginEdit();*/
                        }
                        else
                        {
                            ContextMenuHelper.InvokeCommand(iContextMenu, selected - ShellApi.CmdFirst, parentDirectory, location);
                        }
                    }
                }
            }
            catch (Exception e)
            {
#if DEBUG
                MessageBox.Show("", e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
            finally
            {
                if (iContextMenu != null)
                {
                    Marshal.ReleaseComObject(iContextMenu);
                    iContextMenu = null;
                }

                if (iContextMenu2 != null)
                {
                    Marshal.ReleaseComObject(iContextMenu2);
                    iContextMenu2 = null;
                }

                if (iContextMenu3 != null)
                {
                    Marshal.ReleaseComObject(iContextMenu3);
                    iContextMenu3 = null;
                }

                if (contextMenu != null)
                {
                    User32.DestroyMenu(contextMenu);
                }

                if (iContextMenuPtr != IntPtr.Zero)
                {
                    Marshal.Release(iContextMenuPtr);
                }

                if (iContextMenuPtr2 != IntPtr.Zero)
                {
                    Marshal.Release(iContextMenuPtr2);
                }

                if (iContextMenuPtr3 != IntPtr.Zero)
                {
                    Marshal.Release(iContextMenuPtr3);
                }
            }
        }

        public void CreateNewFolder(DirectoryInfo directory)
        {
            Command("NewFolder", directory);
        }

        public void CopyCommand(params FileSystemInfo[] items)
        {
            Command("copy", items);
        }

        public void CopyCommand(params string[] items)
        {
            Command("copy", items);
        }

        public void PasteCommand(DirectoryInfo directory)
        {
            Command("paste", directory);
        }

        public void CutCommand(params FileSystemInfo[] items)
        {
            Command("cut", items);
        }

        public void CutCommand(params string[] items)
        {
            Command("cut", items);
        }

        public void DeleteCommand(params FileSystemInfo[] items)
        {
            Command("delete", items);
        }

        public void DeleteCommand(params string[] items)
        {
            Command("delete", items);
        }

        public void Command(string command, params string[] items)
        {
            IntPtr[] pidls = ShellHelper.GetPIDLs(items);
            if (pidls.Length > 0)
            {
                string parentDirectory = ShellHelper.GetParentDirectoryPath(items[0]);
                IShellFolder parentShellFolder = ShellHelper.GetShellFolder(parentDirectory);
                ContextMenuHelper.InvokeCommand(parentShellFolder, parentDirectory, pidls, command, new Point(0, 0));
            }
        }

        public void Command(string command, params FileSystemInfo[] items)
        {
            IntPtr[] pidls = ShellHelper.GetPIDLs(items);
            if (pidls.Length > 0)
            {
                string parentDirectory = ShellHelper.GetParentDirectoryPath(items[0]);
                IShellFolder parentShellFolder = ShellHelper.GetShellFolder(parentDirectory);
                ContextMenuHelper.InvokeCommand(parentShellFolder, parentDirectory, pidls, command, new Point(0, 0));
            }
        }

        public void DefaultCommand(FileInfo file)
        {
            DefaultCommand(file.FullName, file.DirectoryName);
        }

        public void DefaultCommand(string path)
        {
            string parentDirectory = Path.GetDirectoryName(path);
            DefaultCommand(path, parentDirectory);
        }


        /// <summary>
        /// This method receives WindowMessages. It will make the "Open With" and "Send To" work 
        /// by calling HandleMenuMsg and HandleMenuMsg2. It will also call the OnContextMenuMouseHover 
        /// method of Browser when hovering over a ContextMenu item.
        /// </summary>
        /// <param name="m">the Message of the Browser's WndProc</param>
        /// <returns>true if the message has been handled, false otherwise</returns>
        protected override void WndProc(ref Message m)
        {
            if (iContextMenu2 != null && (m.Msg == (int) WM.InitMenuPopup || m.Msg == (int) WM.MeasureItem || m.Msg == (int) WM.DrawItem))
            {
                if (iContextMenu2.HandleMenuMsg((uint) m.Msg, m.WParam, m.LParam) == 0)
                {
                    return;
                }
            }

            if (newContextMenu2 != null && ((m.Msg == (int) WM.InitMenuPopup && m.WParam == newSubmenuPtr) || m.Msg == (int) WM.MeasureItem || m.Msg == (int) WM.DrawItem))
            {
                if (newContextMenu2.HandleMenuMsg((uint) m.Msg, m.WParam, m.LParam) == 0)
                {
                    return;
                }
            }

            if (iContextMenu3 != null && m.Msg == (int) WM.MenuChar)
            {
                if (iContextMenu3.HandleMenuMsg2((uint) m.Msg, m.WParam, m.LParam, IntPtr.Zero) == 0)
                {
                    return;
                }
            }

            if (newContextMenu3 != null && m.Msg == (int) WM.MenuChar)
            {
                if (newContextMenu3.HandleMenuMsg2((uint) m.Msg, m.WParam, m.LParam, IntPtr.Zero) == 0)
                {
                    return;
                }
            }

            base.WndProc(ref m);
        }


        private void DefaultCommand(string path, string parentDirectory)
        {
            IntPtr[] pidls = ShellHelper.GetPIDLs(path);

            IntPtr icontextMenuPtr = IntPtr.Zero;
            ContextMenu contextMenu = new ContextMenu();
            IShellFolder parentShellFolder = ShellHelper.GetShellFolder(parentDirectory);

            try
            {
                if (ContextMenuHelper.GetIContextMenu(parentShellFolder, pidls, out icontextMenuPtr, out iContextMenu))
                {
                    iContextMenu.QueryContextMenu(contextMenu.Handle, 0, ShellApi.CmdFirst, ShellApi.CmdLast, CMF.DefaultOnly);

                    int defaultCommand = User32.GetMenuDefaultItem(contextMenu.Handle, false, 0);
                    if (defaultCommand >= ShellApi.CmdFirst)
                    {
                        ContextMenuHelper.InvokeCommand(iContextMenu, (uint) defaultCommand - ShellApi.CmdFirst, parentDirectory, Control.MousePosition);
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (iContextMenu != null)
                {
                    Marshal.ReleaseComObject(iContextMenu);
                    iContextMenu = null;
                }

                //if (contextMenu.Handle != null)
                //    Marshal.FreeCoTaskMem(contextMenu.Handle);

                Marshal.Release(icontextMenuPtr);
            }
        }
    }
}
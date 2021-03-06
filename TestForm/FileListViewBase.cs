﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Shell;

namespace TestForm
{
    public partial class FileListViewBase : ListView
    {
        private FileSystemNode selectedNode;
        private Dictionary<int, ListViewItem> items = new Dictionary<int, ListViewItem>();
        private ShellContextMenu contextMenu = new ShellContextMenu();


        public FileListViewBase()
        {
            InitializeComponent();

            this.RetrieveVirtualItem += FileListView_RetrieveVirtualItem;
        }



        public FileSystemNode SelectedNode
        {
            get { return selectedNode; }
            set
            {
                if (selectedNode != value)
                {
                    try
                    {
                        if (value.ChildNodes != null && value.ChildNodes.Length > 0)
                        {
                            selectedNode = value;
                            LoadNode(value);
                            OnNodeSelected(value);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        public NodeSelectedEventHandler NodeSelected;


        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Assign the image lists to the ListView
            ShellImageList.Set32SmallImageList(this);
            ShellImageList.SetLargeImageList(this);
        }

        protected override void OnItemActivate(EventArgs e)
        {
            var item = items[this.SelectedIndices[0]];

            if (item != null)
            {
                FileSystemNode node = (FileSystemNode)item.Tag;

                if (node.AllowOpen)
                {
                    this.SelectedNode = node;
                }
                else if (node is IExecutive)
                {
                    ((IExecutive)node).Activate(contextMenu);
                }

            }

            base.OnItemActivate(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.SelectedIndices.Count > 0)
                {
                    Point location = this.PointToScreen(e.Location);

                    List<string> list = new List<string>(this.SelectedIndices.Count);
                    foreach (int index in this.SelectedIndices)
                    {
                        var item = items[this.SelectedIndices[0]];

                        if (item != null)
                        {
                            FileSystemNode node = (FileSystemNode)item.Tag;

                            if (!node.IsVirtual)
                            {
                                list.Add(node.Path);
                            }
                        }
                    }

                    if (list.Count > 0)
                    {
                        contextMenu.Show(location, list.ToArray());
                    }
                }
            }

            base.OnMouseUp(e);
        }

        protected virtual void OnNodeSelected(FileSystemNode node)
        {
            if (NodeSelected != null)
            {
                NodeSelected(this, node);
            }
        }


        private void SetVScroll(int value)
        {
            User32.SendMessage(this.Handle, WM.VScroll, value, IntPtr.Zero);
        }

        private void SetHScroll(int value)
        {
            User32.SendMessage(this.Handle, WM.HScroll, value, IntPtr.Zero);
        }


        private void LoadNode(FileSystemNode node)
        {
            if (node.ChildNodes != null && node.ChildNodes.Length > 0)
            {
                this.BeginUpdate();
                largeImageList.Images.Clear();
                items.Clear();

                int count = node.ChildNodes.Length;
                try
                {
                    this.VirtualListSize = count;
                }
                catch (NullReferenceException)
                {
                }
                this.VirtualMode = node.ChildNodes.Length > 0;

                this.SelectedIndices.Clear();
                this.SelectedIndices.Add(0);
                this.EndUpdate();

                SetVScroll((int)ScrollBarMessage.Top);
                SetHScroll((int)ScrollBarMessage.Left);
            }
        }

        private ListViewItem GetItem(int index)
        {
            if (this.items.ContainsKey(index))
            {
                return this.items[index];
            }
            else
            {
                ListViewItem item = new ListViewItem(selectedNode.ChildNodes[index].Name, selectedNode.ChildNodes[index].GetImageIndex());
                item.Tag = selectedNode.ChildNodes[index];
                for (int i = 1; i < this.Columns.Count; i++)
                {
                    item.SubItems.Add("");
                }

                this.items.Add(index, item);
                return item;
            }
        }

        private void FileListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = GetItem(e.ItemIndex);
        }
    }

    public delegate string GetColumntNodeContentEventHandler(ColumnHeader column, FileSystemNode node);

    public delegate void NodeSelectedEventHandler(object sender, FileSystemNode node);
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ShellDll;

namespace Commander
{
    public partial class CommanderForm : Form
    {
        private Dictionary<DriveType, int> imageIndexes = new Dictionary<DriveType, int>();

        public CommanderForm()
        {
            InitializeComponent();

            leftDrivesToolBar.Tag = leftFileView;
            rightDriveToolBar.Tag = rightFileView;


            imageIndexes.Add(DriveType.Fixed, 1);
            imageIndexes.Add(DriveType.CDRom, 2);
            imageIndexes.Add(DriveType.Removable, 3);
            imageIndexes.Add(DriveType.Network, 4);

            Load();

            toolStripButton2_Click(null, null);

            drivesToolBar_ButtonClick(leftDrivesToolBar, new ToolBarButtonClickEventArgs(leftDrivesToolBar.Buttons[0]));
            drivesToolBar_ButtonClick(rightDriveToolBar, new ToolBarButtonClickEventArgs(rightDriveToolBar.Buttons[1]));

#if DEBUG
            //TestForm testForem = new TestForm();
            //testForem.Show();
#endif
        }

        private void Load()
        {
            LoadDiskDrives(leftDrivesToolBar);
            LoadDiskDrives(rightDriveToolBar);
        }

        private ToolBarButton CreateDiskDriveButton(DriveInfo drive)
        {
            ToolBarButton button = new ToolBarButton();
            button.Name = string.Format("{0}DriveButton", drive.Name.ToLower());
            button.Text = drive.Name.Remove(drive.Name.Length - 2, 2).ToLower();
            button.Tag = drive;
            button.ImageIndex = imageIndexes[drive.DriveType];            


            return button;
        }
        
        private void LoadDiskDrives(ToolBar toolBar)
        {
            toolBar.Buttons.Clear();

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                ToolBarButton button = CreateDiskDriveButton(d);
                toolBar.Buttons.Add(button);
            }
        }

        private void drivesToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            ToolBar toolBar = (ToolBar)sender;
            SetPushedDriveButton(toolBar, e.Button);

            FileView fileView = (FileView)toolBar.Tag;
            DriveInfo drive = (DriveInfo)e.Button.Tag;
            fileView.LoadDirectory(drive.RootDirectory);
        }

        private void SetPushedDriveButton(ToolBar toolBar, ToolBarButton button)
        {
            int index = toolBar.Buttons.IndexOf(button);

            for (int i = 0; i < leftDrivesToolBar.Buttons.Count; i++)
            {
                toolBar.Buttons[i].Pushed = (i == index);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //leftListView.View = View.Details;
            //rightListView.View = View.Details;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            /*leftListView.View = View.Tile;
            leftListView.TileSize = new Size(230, 32);
            rightListView.View = View.Tile;
            rightListView.TileSize = new Size(230, 32);
            
            leftListView.View = View.Details;
            rightListView.View = View.Details;

            leftListView.View = View.Tile;
            leftListView.TileSize = new Size(230, 32);
            rightListView.View = View.Tile;
            rightListView.TileSize = new Size(230, 32);*/
        }

        private void leftDrivesToolBar_MouseUp(object sender, MouseEventArgs e)
        {
            Control control = leftDrivesToolBar.GetChildAtPoint(e.Location);
        }

    }
}

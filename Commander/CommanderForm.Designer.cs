﻿namespace Commander
{
    partial class CommanderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommanderForm));
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.topPanel = new System.Windows.Forms.Panel();
            this.topSplitContainer = new System.Windows.Forms.SplitContainer();
            this.leftDrivesToolBar = new System.Windows.Forms.ToolBar();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.rightDriveToolBar = new System.Windows.Forms.ToolBar();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.leftListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.pathImageList = new System.Windows.Forms.ImageList(this.components);
            this.largePathImageList = new System.Windows.Forms.ImageList(this.components);
            this.testLabel = new System.Windows.Forms.Label();
            this.toolStrip.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.topSplitContainer.Panel1.SuspendLayout();
            this.topSplitContainer.Panel2.SuspendLayout();
            this.topSplitContainer.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3});
            this.menuItem1.Text = "File";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "Item";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "Exit";
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(580, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton";
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.testLabel);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 312);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(580, 38);
            this.bottomPanel.TabIndex = 1;
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.topSplitContainer);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 25);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(580, 33);
            this.topPanel.TabIndex = 2;
            // 
            // topSplitContainer
            // 
            this.topSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topSplitContainer.IsSplitterFixed = true;
            this.topSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.topSplitContainer.Name = "topSplitContainer";
            // 
            // topSplitContainer.Panel1
            // 
            this.topSplitContainer.Panel1.Controls.Add(this.leftDrivesToolBar);
            // 
            // topSplitContainer.Panel2
            // 
            this.topSplitContainer.Panel2.Controls.Add(this.rightDriveToolBar);
            this.topSplitContainer.Size = new System.Drawing.Size(580, 33);
            this.topSplitContainer.SplitterDistance = 274;
            this.topSplitContainer.TabIndex = 0;
            // 
            // leftDrivesToolBar
            // 
            this.leftDrivesToolBar.ButtonSize = new System.Drawing.Size(36, 21);
            this.leftDrivesToolBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftDrivesToolBar.DropDownArrows = true;
            this.leftDrivesToolBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.leftDrivesToolBar.ImageList = this.imageList;
            this.leftDrivesToolBar.Location = new System.Drawing.Point(0, 0);
            this.leftDrivesToolBar.Name = "leftDrivesToolBar";
            this.leftDrivesToolBar.ShowToolTips = true;
            this.leftDrivesToolBar.Size = new System.Drawing.Size(274, 27);
            this.leftDrivesToolBar.TabIndex = 0;
            this.leftDrivesToolBar.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
            this.leftDrivesToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.drivesToolBar_ButtonClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "flopydrive.png");
            this.imageList.Images.SetKeyName(1, "Drive.png");
            this.imageList.Images.SetKeyName(2, "cddrive.png");
            this.imageList.Images.SetKeyName(3, "net.png");
            // 
            // rightDriveToolBar
            // 
            this.rightDriveToolBar.ButtonSize = new System.Drawing.Size(36, 21);
            this.rightDriveToolBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightDriveToolBar.DropDownArrows = true;
            this.rightDriveToolBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rightDriveToolBar.ImageList = this.imageList;
            this.rightDriveToolBar.Location = new System.Drawing.Point(0, 0);
            this.rightDriveToolBar.Name = "rightDriveToolBar";
            this.rightDriveToolBar.ShowToolTips = true;
            this.rightDriveToolBar.Size = new System.Drawing.Size(302, 27);
            this.rightDriveToolBar.TabIndex = 1;
            this.rightDriveToolBar.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
            this.rightDriveToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.drivesToolBar_ButtonClick);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 58);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.leftListView);
            this.splitContainer.Size = new System.Drawing.Size(580, 254);
            this.splitContainer.SplitterDistance = 274;
            this.splitContainer.TabIndex = 3;
            // 
            // leftListView
            // 
            this.leftListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.leftListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftListView.LargeImageList = this.largePathImageList;
            this.leftListView.Location = new System.Drawing.Point(0, 0);
            this.leftListView.Name = "leftListView";
            this.leftListView.Size = new System.Drawing.Size(274, 254);
            this.leftListView.SmallImageList = this.pathImageList;
            this.leftListView.TabIndex = 0;
            this.leftListView.UseCompatibleStateImageBehavior = false;
            this.leftListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 98;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 98;
            // 
            // pathImageList
            // 
            this.pathImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.pathImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.pathImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // largePathImageList
            // 
            this.largePathImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.largePathImageList.ImageSize = new System.Drawing.Size(32, 32);
            this.largePathImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // testLabel
            // 
            this.testLabel.AutoSize = true;
            this.testLabel.Location = new System.Drawing.Point(4, 13);
            this.testLabel.Name = "testLabel";
            this.testLabel.Size = new System.Drawing.Size(50, 13);
            this.testLabel.TabIndex = 0;
            this.testLabel.Text = "testLabel";
            // 
            // CommanderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 350);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.toolStrip);
            this.Menu = this.mainMenu;
            this.Name = "CommanderForm";
            this.Text = "Form";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.topPanel.ResumeLayout(false);
            this.topSplitContainer.Panel1.ResumeLayout(false);
            this.topSplitContainer.Panel1.PerformLayout();
            this.topSplitContainer.Panel2.ResumeLayout(false);
            this.topSplitContainer.Panel2.PerformLayout();
            this.topSplitContainer.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.SplitContainer topSplitContainer;
        private System.Windows.Forms.ToolBar leftDrivesToolBar;
        private System.Windows.Forms.ToolBar rightDriveToolBar;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ListView leftListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList pathImageList;
        private System.Windows.Forms.ImageList largePathImageList;
        private System.Windows.Forms.Label testLabel;


    }
}


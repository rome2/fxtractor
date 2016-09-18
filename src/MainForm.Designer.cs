#region License
///////////////////////////////////////////////////////////////////////////////
//                                                                           //
// File:           MainForm.Designer.cs                                      //
//                                                                           //
// Functionality:  Form designer managed part of the main window.            //
//                                                                           //
// Author:         Rolf Meyerhoff (rollo@indygo.de)                          //
//                                                                           //
// Copyright:      © 2009 by Rolf Meyerhoff. All rights reserved.            //
//                                                                           //
///////////////////////////////////////////////////////////////////////////////
//                                                                           //
// License:                                                                  //
//                                                                           //
//   Redistribution  and  use in source  and  binary forms,  with or without //
//   modification, are permitted  provided that the following conditions are //
//   met:                                                                    //
//                                                                           //
//   1. Redistributions  of  source  code  must  retain the  above copyright //
//      notice, this list of conditions and the following disclaimer.        //
//                                                                           //
//   2. Redistributions  in binary  form must reproduce  the above copyright //
//      notice, this list of conditions  and the following disclaimer in the //
//      documentation and/or other materials provided with the distribution. //
//                                                                           //
//   3. Neither the  name of  the author  nor the names of  his contributors //
//      may  be  used  to  endorse or  promote  products  derived  from this //
//      software without specific prior written permission.                  //
//                                                                           //
///////////////////////////////////////////////////////////////////////////////
//                                                                           //
// Disclaimer:                                                               //
//                                                                           //
//   This software  is provided  by the  copyright holders  and contributors //
//   "as is"  and any  express or  implied  warranties,  including,  but not //
//   limited to, the implied warranties of merchantability and fitness for a //
//   particular  purpose  are disclaimed.  In no event  shall the  copyright //
//   owner or contributors be liable for any direct,  indirect,  incidental, //
//   special,   exemplary,  or  consequential  damages (including,  but  not //
//   limited to,  procurement of substitute goods or services;  loss of use, //
//   data,  or profits;  or business interruption) however caused and on any //
//   theory  of liability,  whether in contract,  strict liability,  or tort //
//   (including  negligence or otherwise)  arising in any way out of the use //
//   of this software, even if advised of the possibility of such damage.    //
//                                                                           //
///////////////////////////////////////////////////////////////////////////////
#endregion License

namespace FXTractor
{
  #region class MainForm
  /// <summary>
  /// Main window of the application.
  /// </summary>
  partial class MainForm
  {
    #region Public Destruction

    #region void Dispose(bool disposing)
    /// <summary>
    /// Clear resources.
    /// </summary>
    /// <param name="disposing">
    /// True, managed ressources has to be delete, false otherwise.
    /// </param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }
    #endregion void Dispose(bool disposing)

    #endregion Public Destruction

    #region Windows form designer generated code

    /// <summary>
    /// Required methode for design mode support.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.checkOverwrite = new System.Windows.Forms.CheckBox();
      this.checkXML = new System.Windows.Forms.CheckBox();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.labelDrop = new System.Windows.Forms.Label();
      this.checkDry = new System.Windows.Forms.CheckBox();
      this.menuStrip = new System.Windows.Forms.MenuStrip();
      this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
      this.menuItemConvertFiles = new System.Windows.Forms.ToolStripMenuItem();
      this.menuItemConvertDirectory = new System.Windows.Forms.ToolStripMenuItem();
      this.menuItemSeperator1 = new System.Windows.Forms.ToolStripSeparator();
      this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
      this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
      this.btnConvertFiles = new System.Windows.Forms.Button();
      this.btnConvertDirectory = new System.Windows.Forms.Button();
      this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.menuStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // checkOverwrite
      // 
      this.checkOverwrite.AllowDrop = true;
      this.checkOverwrite.AutoSize = true;
      this.checkOverwrite.Checked = true;
      this.checkOverwrite.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkOverwrite.Location = new System.Drawing.Point(12, 120);
      this.checkOverwrite.Name = "checkOverwrite";
      this.checkOverwrite.Size = new System.Drawing.Size(130, 17);
      this.checkOverwrite.TabIndex = 0;
      this.checkOverwrite.Text = "Overwrite existing files";
      this.checkOverwrite.UseVisualStyleBackColor = true;
      this.checkOverwrite.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
      this.checkOverwrite.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
      // 
      // checkXML
      // 
      this.checkXML.AllowDrop = true;
      this.checkXML.AutoSize = true;
      this.checkXML.Location = new System.Drawing.Point(12, 143);
      this.checkXML.Name = "checkXML";
      this.checkXML.Size = new System.Drawing.Size(108, 17);
      this.checkXML.TabIndex = 1;
      this.checkXML.Text = "Extract XML data";
      this.checkXML.UseVisualStyleBackColor = true;
      this.checkXML.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
      this.checkXML.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
      // 
      // labelDrop
      // 
      this.labelDrop.AllowDrop = true;
      this.labelDrop.AutoSize = true;
      this.labelDrop.Location = new System.Drawing.Point(63, 60);
      this.labelDrop.Name = "labelDrop";
      this.labelDrop.Size = new System.Drawing.Size(118, 26);
      this.labelDrop.TabIndex = 3;
      this.labelDrop.Text = "Drop vstpreset files and\r\nfolders here";
      this.labelDrop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.labelDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
      this.labelDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
      // 
      // checkDry
      // 
      this.checkDry.AllowDrop = true;
      this.checkDry.AutoSize = true;
      this.checkDry.Location = new System.Drawing.Point(12, 166);
      this.checkDry.Name = "checkDry";
      this.checkDry.Size = new System.Drawing.Size(160, 17);
      this.checkDry.TabIndex = 4;
      this.checkDry.Text = "Dry run (don\'t write anything)";
      this.checkDry.UseVisualStyleBackColor = true;
      this.checkDry.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
      this.checkDry.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
      // 
      // menuStrip
      // 
      this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemHelp});
      this.menuStrip.Location = new System.Drawing.Point(0, 0);
      this.menuStrip.Name = "menuStrip";
      this.menuStrip.Size = new System.Drawing.Size(245, 24);
      this.menuStrip.TabIndex = 5;
      this.menuStrip.Text = "menuStrip";
      // 
      // menuItemFile
      // 
      this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemConvertFiles,
            this.menuItemConvertDirectory,
            this.menuItemSeperator1,
            this.menuItemExit});
      this.menuItemFile.Name = "menuItemFile";
      this.menuItemFile.Size = new System.Drawing.Size(36, 20);
      this.menuItemFile.Text = "&File";
      // 
      // menuItemConvertFiles
      // 
      this.menuItemConvertFiles.Image = ((System.Drawing.Image)(resources.GetObject("menuItemConvertFiles.Image")));
      this.menuItemConvertFiles.Name = "menuItemConvertFiles";
      this.menuItemConvertFiles.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
      this.menuItemConvertFiles.Size = new System.Drawing.Size(216, 22);
      this.menuItemConvertFiles.Text = "&Convert File(s)...";
      this.menuItemConvertFiles.Click += new System.EventHandler(this.menuItemConvertFiles_Click);
      // 
      // menuItemConvertDirectory
      // 
      this.menuItemConvertDirectory.Image = ((System.Drawing.Image)(resources.GetObject("menuItemConvertDirectory.Image")));
      this.menuItemConvertDirectory.Name = "menuItemConvertDirectory";
      this.menuItemConvertDirectory.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
      this.menuItemConvertDirectory.Size = new System.Drawing.Size(216, 22);
      this.menuItemConvertDirectory.Text = "Convert &Directory";
      this.menuItemConvertDirectory.Click += new System.EventHandler(this.menuItemConvertDirectory_Click);
      // 
      // menuItemSeperator1
      // 
      this.menuItemSeperator1.Name = "menuItemSeperator1";
      this.menuItemSeperator1.Size = new System.Drawing.Size(213, 6);
      // 
      // menuItemExit
      // 
      this.menuItemExit.Image = ((System.Drawing.Image)(resources.GetObject("menuItemExit.Image")));
      this.menuItemExit.Name = "menuItemExit";
      this.menuItemExit.Size = new System.Drawing.Size(216, 22);
      this.menuItemExit.Text = "E&xit";
      this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
      // 
      // menuItemHelp
      // 
      this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAbout});
      this.menuItemHelp.Name = "menuItemHelp";
      this.menuItemHelp.Size = new System.Drawing.Size(43, 20);
      this.menuItemHelp.Text = "&Help";
      // 
      // menuItemAbout
      // 
      this.menuItemAbout.Image = ((System.Drawing.Image)(resources.GetObject("menuItemAbout.Image")));
      this.menuItemAbout.Name = "menuItemAbout";
      this.menuItemAbout.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
      this.menuItemAbout.Size = new System.Drawing.Size(166, 22);
      this.menuItemAbout.Text = "&About...";
      this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
      // 
      // btnConvertFiles
      // 
      this.btnConvertFiles.Location = new System.Drawing.Point(12, 198);
      this.btnConvertFiles.Name = "btnConvertFiles";
      this.btnConvertFiles.Size = new System.Drawing.Size(105, 23);
      this.btnConvertFiles.TabIndex = 6;
      this.btnConvertFiles.Text = "Convert File(s)";
      this.btnConvertFiles.UseVisualStyleBackColor = true;
      this.btnConvertFiles.Click += new System.EventHandler(this.menuItemConvertFiles_Click);
      // 
      // btnConvertDirectory
      // 
      this.btnConvertDirectory.Location = new System.Drawing.Point(123, 198);
      this.btnConvertDirectory.Name = "btnConvertDirectory";
      this.btnConvertDirectory.Size = new System.Drawing.Size(105, 23);
      this.btnConvertDirectory.TabIndex = 7;
      this.btnConvertDirectory.Text = "Convert Directory";
      this.btnConvertDirectory.UseVisualStyleBackColor = true;
      this.btnConvertDirectory.Click += new System.EventHandler(this.menuItemConvertDirectory_Click);
      // 
      // openFileDialog
      // 
      this.openFileDialog.DefaultExt = "vstpreset";
      this.openFileDialog.Filter = "Vst3 Presets (*.vstpreset)|*.vstpreset";
      this.openFileDialog.Multiselect = true;
      this.openFileDialog.Title = "Select presets to convert";
      // 
      // MainForm
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(245, 236);
      this.Controls.Add(this.btnConvertDirectory);
      this.Controls.Add(this.btnConvertFiles);
      this.Controls.Add(this.checkDry);
      this.Controls.Add(this.labelDrop);
      this.Controls.Add(this.checkXML);
      this.Controls.Add(this.checkOverwrite);
      this.Controls.Add(this.menuStrip);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip;
      this.MaximizeBox = false;
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "FXTractor";
      this.TopMost = true;
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
      this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
      this.menuStrip.ResumeLayout(false);
      this.menuStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.CheckBox checkOverwrite;
    private System.Windows.Forms.CheckBox checkXML;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.Label labelDrop;
    private System.Windows.Forms.CheckBox checkDry;

    #endregion Windows form designer generated code
    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStripMenuItem menuItemFile;
    private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
    private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
    private System.Windows.Forms.ToolStripMenuItem menuItemConvertFiles;
    private System.Windows.Forms.ToolStripMenuItem menuItemConvertDirectory;
    private System.Windows.Forms.ToolStripSeparator menuItemSeperator1;
    private System.Windows.Forms.ToolStripMenuItem menuItemExit;
    private System.Windows.Forms.Button btnConvertFiles;
    private System.Windows.Forms.Button btnConvertDirectory;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
  }
  #endregion class MainForm
}

///////////////////////////////// End of File /////////////////////////////////

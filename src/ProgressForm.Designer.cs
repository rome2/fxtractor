#region License
///////////////////////////////////////////////////////////////////////////////
//                                                                           //
// File:           ProgressForm.Designer.cs                                  //
//                                                                           //
// Functionality:  Progress (worker) window of the application.              //
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
  #region class ProgressForm
  /// <summary>
  /// Progress (working) window of the application.
  /// </summary>
  partial class ProgressForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressForm));
      this.buttonOK = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.buttonClipboard = new System.Windows.Forms.Button();
      this.listViewFiles = new System.Windows.Forms.ListView();
      this.columnHeaderStatus = new System.Windows.Forms.ColumnHeader();
      this.columnHeaderFile = new System.Windows.Forms.ColumnHeader();
      this.columnHeaderMessage = new System.Windows.Forms.ColumnHeader();
      this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
      this.SuspendLayout();
      // 
      // buttonOK
      // 
      this.buttonOK.Enabled = false;
      this.buttonOK.Location = new System.Drawing.Point(444, 364);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(75, 23);
      this.buttonOK.TabIndex = 3;
      this.buttonOK.Text = "&OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      // 
      // buttonCancel
      // 
      this.buttonCancel.Location = new System.Drawing.Point(363, 364);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(75, 23);
      this.buttonCancel.TabIndex = 2;
      this.buttonCancel.Text = "&Stop";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // buttonClipboard
      // 
      this.buttonClipboard.Enabled = false;
      this.buttonClipboard.Location = new System.Drawing.Point(12, 364);
      this.buttonClipboard.Name = "buttonClipboard";
      this.buttonClipboard.Size = new System.Drawing.Size(130, 23);
      this.buttonClipboard.TabIndex = 1;
      this.buttonClipboard.Text = "&Copy to clipboard";
      this.buttonClipboard.UseVisualStyleBackColor = true;
      this.buttonClipboard.Click += new System.EventHandler(this.buttonClipboard_Click);
      // 
      // listViewFiles
      // 
      this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderStatus,
            this.columnHeaderFile,
            this.columnHeaderMessage});
      this.listViewFiles.Location = new System.Drawing.Point(12, 12);
      this.listViewFiles.Name = "listViewFiles";
      this.listViewFiles.Size = new System.Drawing.Size(507, 346);
      this.listViewFiles.SmallImageList = this.imageListSmall;
      this.listViewFiles.TabIndex = 0;
      this.listViewFiles.UseCompatibleStateImageBehavior = false;
      this.listViewFiles.View = System.Windows.Forms.View.Details;
      // 
      // columnHeaderStatus
      // 
      this.columnHeaderStatus.Text = "";
      this.columnHeaderStatus.Width = 24;
      // 
      // columnHeaderFile
      // 
      this.columnHeaderFile.Text = "File";
      this.columnHeaderFile.Width = 320;
      // 
      // columnHeaderMessage
      // 
      this.columnHeaderMessage.Text = "Error Message";
      this.columnHeaderMessage.Width = 150;
      // 
      // imageListSmall
      // 
      this.imageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmall.ImageStream")));
      this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
      this.imageListSmall.Images.SetKeyName(0, "ok.png");
      this.imageListSmall.Images.SetKeyName(1, "error.png");
      // 
      // ProgressForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(531, 395);
      this.Controls.Add(this.listViewFiles);
      this.Controls.Add(this.buttonClipboard);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = "ProgressForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Progress";
      this.Shown += new System.EventHandler(this.ProgressForm_Shown);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressForm_FormClosing);
      this.ResumeLayout(false);

    }

    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Button buttonClipboard;
    private System.Windows.Forms.ListView listViewFiles;
    private System.Windows.Forms.ColumnHeader columnHeaderStatus;
    private System.Windows.Forms.ColumnHeader columnHeaderFile;
    private System.Windows.Forms.ColumnHeader columnHeaderMessage;
    private System.Windows.Forms.ImageList imageListSmall;

    #endregion Windows form designer generated code
  }
  #endregion class ProgressForm
}

///////////////////////////////// End of File /////////////////////////////////

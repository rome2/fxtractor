#region License
///////////////////////////////////////////////////////////////////////////////
//                                                                           //
// File:           MainForm.cs                                               //
//                                                                           //
// Functionality:  Main window of the application.                           //
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

using System;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FXTractor
{
  #region class MainForm
  /// <summary>
  /// Main window of the application.
  /// </summary>
  public partial class MainForm : Form
  {
    #region Private Member

    /// <summary>
    /// The names of the files to convert.
    /// </summary>
    private StringCollection processFiles;

    /// <summary>
    /// The current working thread.
    /// </summary>
    private Thread processThread;

    #endregion Private Member

    #region Public Construction

    #region MainForm()
    /// <summary>
    /// Standard constructor of this class.
    /// </summary>
    public MainForm()
    {
      // Init controls:
      InitializeComponent();
    }
    #endregion MainForm()

    #endregion Public Construction

    #region Internal helper functions

    #region void ProcessFunc()
    /// <summary>
    /// Private helper function to ensure correct multi threading drag and drop.
    /// </summary>
    private void ProcessFunc()
    {
      // Save window z state as the dialog will take over:
      bool topMost = TopMost;
      this.TopMost = false;

      // Init and show the worker dialog:
      ProgressForm dlg      = new ProgressForm();
      dlg.ProcessFiles      = processFiles;
      dlg.OverWriteExisting = checkOverwrite.Checked;
      dlg.ExtractXml        = checkXML.Checked;
      dlg.DryRun            = checkDry.Checked;
      dlg.ShowDialog(this);
      
      // Restore z state:
      TopMost = topMost;

      // Clear thread state:
      processThread = null;
      processFiles  = null;
    }
    #endregion void ProcessFunc()

    #endregion Internal helper functions

    #region Event Handler

    #region void MainForm_DragEnter(object, DragEventArgs)
    /// <summary>
    /// Event handler for the DragEnter event.
    /// </summary>
    /// <param name="sender">
    /// The source control, emitting the event.
    /// </param>
    /// <param name="e">
    /// Event arguments describing the circumstances of the event.
    /// </param>
    private void MainForm_DragEnter(object sender, DragEventArgs e)
    {
      // Already running?
      if ((processThread != null) && processThread.IsAlive)
        return;

      // If the data is a file display the copy cursor:
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
        e.Effect = DragDropEffects.Copy;
      else
        e.Effect = DragDropEffects.None;
    }
    #endregion void MainForm_DragEnter(object, DragEventArgs)

    #region void MainForm_DragDrop(object, DragEventArgs)
    /// <summary>
    /// Event handler for the DragDrop event.
    /// </summary>
    /// <param name="sender">
    /// The source control, emitting the event.
    /// </param>
    /// <param name="e">
    /// Event arguments describing the circumstances of the event.
    /// </param>
    private void MainForm_DragDrop(object sender, DragEventArgs e)
    {
      // Already running?
      if ((processThread != null) && processThread.IsAlive)
        return;

      // Handle FileDrop data:
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        // Assign the file names to a string array, in  case the user has selected multiple files:
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        Array.Sort(files);
        try
        {
          // Loop through files and compose a list with all presets to process:
          processFiles = new StringCollection();
          for (int i = 0; i < files.Length; i++)
          {
            try
            {
              // Is a directory?
              if (Directory.Exists(files[i]))
              {
                // Search for files:
                string[] newFiles = Directory.GetFiles(files[i], "*.vstpreset", SearchOption.AllDirectories);
                if (newFiles != null)
                  processFiles.AddRange(newFiles);
              }

              // Is a *.preset file?
              else if (string.Compare(Path.GetExtension(files[i]), ".vstpreset", true) == 0)
              {
                // Add it:
                processFiles.Add(files[i]);
              }
            }
            catch
            {
            }
          }

          // Found anything?
          if (processFiles.Count == 0)
            return;

          // The sending application (drop source) is still waiting for us to
          // finish the drag and drop operation. We thus create a working thread
          // and delegate the hard work to this thread:
          processThread = new Thread((ThreadStart)delegate() { Invoke((MethodInvoker)delegate() { ProcessFunc(); }); });
          processThread.Start();
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message);
          return;
        }
      }
    }
    #endregion void MainForm_DragDrop(object, DragEventArgs)

    #region void MainForm_FormClosing(object, FormClosingEventArgs)
    /// <summary>
    /// Event handler for the FormClosing event.
    /// </summary>
    /// <param name="sender">
    /// The source control, emitting the event.
    /// </param>
    /// <param name="e">
    /// Event arguments describing the circumstances of the event.
    /// </param>
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      // Still running?
      if ((processThread != null) && processThread.IsAlive)
      {
        // Stop closing:
        e.Cancel = true;
        return;
      }

      // Closing is fine:
      e.Cancel = false;
    }
    #endregion void MainForm_FormClosing(object, FormClosingEventArgs)

    #region void menuItemConvertFiles_Click(object, EventArgs)
    /// <summary>
    /// Event handler for the convert file(s) menu or button event.
    /// </summary>
    /// <param name="sender">
    /// The source control, emitting the event.
    /// </param>
    /// <param name="e">
    /// Event arguments describing the circumstances of the event.
    /// </param>
    private void menuItemConvertFiles_Click(object sender, EventArgs e)
    {
      // Already running?
      if ((processThread != null) && processThread.IsAlive)
        return;

      try
      {
        // Get file(s):
        if (openFileDialog.ShowDialog() != DialogResult.OK)
          return;

        // Anything to do?
        if (openFileDialog.FileNames != null)
        {
          // Add files to the file list:
          processFiles = new StringCollection();
          processFiles.AddRange(openFileDialog.FileNames);

          // Create a working thread and delegate the hard work to this thread:
          processThread = new Thread((ThreadStart)delegate() { Invoke((MethodInvoker)delegate() { ProcessFunc(); }); });
          processThread.Start();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        return;
      }
    }
    #endregion void menuItemConvertFiles_Click(object, EventArgs)

    #region void menuItemConvertDirectory_Click(object, EventArgs)
    /// <summary>
    /// Event handler for the convert directory menu or button event.
    /// </summary>
    /// <param name="sender">
    /// The source control, emitting the event.
    /// </param>
    /// <param name="e">
    /// Event arguments describing the circumstances of the event.
    /// </param>
    private void menuItemConvertDirectory_Click(object sender, EventArgs e)
    {
      // Already running?
      if ((processThread != null) && processThread.IsAlive)
        return;

      try
      {
        // Get directory:
        if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
          return;

        // Is a directory?
        if (Directory.Exists(folderBrowserDialog.SelectedPath))
        {
          // Search for files:
          string[] newFiles = Directory.GetFiles(folderBrowserDialog.SelectedPath, "*.vstpreset", SearchOption.AllDirectories);
          if (newFiles != null)
          {
            // Add files to the file list:
            processFiles = new StringCollection();
            processFiles.AddRange(newFiles);

            // Create a working thread and delegate the hard work to this thread:
            processThread = new Thread((ThreadStart)delegate() { Invoke((MethodInvoker)delegate() { ProcessFunc(); }); });
            processThread.Start();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        return;
      }
    }
    #endregion void menuItemConvertDirectory_Click(object, EventArgs)

    #region void menuItemExit_Click(object, EventArgs)
    /// <summary>
    /// Event handler for the exit menu event.
    /// </summary>
    /// <param name="sender">
    /// The source control, emitting the event.
    /// </param>
    /// <param name="e">
    /// Event arguments describing the circumstances of the event.
    /// </param>
    private void menuItemExit_Click(object sender, EventArgs e)
    {
      // Still running?
      if ((processThread != null) && processThread.IsAlive)
        return;

      // Close the application:
      this.Close();
    }
    #endregion void menuItemExit_Click(object, EventArgs)

    #region void menuItemAbout_Click(object, EventArgs)
    /// <summary>
    /// Event handler for the about menu entry event.
    /// </summary>
    /// <param name="sender">
    /// The source control, emitting the event.
    /// </param>
    /// <param name="e">
    /// Event arguments describing the circumstances of the event.
    /// </param>
    private void menuItemAbout_Click(object sender, EventArgs e)
    {
      // Currently running?
      if ((processThread != null) && processThread.IsAlive)
        return;

      // Show about informations:
      MessageBox.Show("FXTractor v0.03\nCopyright 2009 Rolf Meyerhoff\nhttp://www.indygo.de", "About FXTractor", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    #endregion void menuItemAbout_Click(object, EventArgs)

    #endregion Event Handler
  }
  #endregion class MainForm
}

///////////////////////////////// End of File /////////////////////////////////

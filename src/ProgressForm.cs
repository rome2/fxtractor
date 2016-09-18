#region License
///////////////////////////////////////////////////////////////////////////////
//                                                                           //
// File:           ProgressForm.cs                                           //
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

using System;
using System.Collections.Specialized;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FXTractor
{
  #region class ProgressForm
  /// <summary>
  /// Progress (working) window of the application.
  /// </summary>
  public partial class ProgressForm : Form
  {
    #region Private Member

    /// <summary>
    /// The files to process.
    /// </summary>
    private volatile StringCollection processFiles;

    /// <summary>
    /// Result as string:
    /// </summary>
    private volatile StringCollection reportFile;

    /// <summary>
    /// Overwrite existing files?
    /// </summary>
    private volatile bool overWriteExisting;

    /// <summary>
    /// Extract XML data too?
    /// </summary>
    private volatile bool extractXml;

    /// <summary>
    /// Test run only?
    /// </summary>
    private volatile bool dryRun;

    /// <summary>
    /// Marker to stop the background working thread.
    /// </summary>
    private volatile bool threadStopped;

    /// <summary>
    /// The background working thread.
    /// </summary>
    private Thread workerThread;

    #endregion Private Member

    #region Public Properties

    #region StringCollection ProcessFiles
    /// <summary>
    /// The files to process.
    /// </summary>
    public StringCollection ProcessFiles
    {
      get
      {
        return processFiles;
      }
      set
      {
        processFiles = value;
      }
    }
    #endregion StringCollection ProcessFiles

    #region bool OverWriteExisting
    /// <summary>
    /// Overwrite existing files?
    /// </summary>
    public bool OverWriteExisting
    {
      get
      {
        return overWriteExisting;
      }
      set
      {
        overWriteExisting = value;
      }
    }
    #endregion bool OverWriteExisting

    #region bool ExtractXml
    /// <summary>
    /// Extract XML data too?
    /// </summary>
    public bool ExtractXml
    {
      get
      {
        return extractXml;
      }
      set
      {
        extractXml = value;
      }
    }
    #endregion bool ExtractXml

    #region bool DryRun
    /// <summary>
    /// Test run only?
    /// </summary>
    public bool DryRun
    {
      get
      {
        return dryRun;
      }
      set
      {
        dryRun = value;
      }
    }
    #endregion bool DryRun

    #endregion Public Properties

    #region Public Construction

    #region ProgressForm()
    /// <summary>
    /// Standard constructor of this class.
    /// </summary>
    public ProgressForm()
    {
      // Init controls:
      InitializeComponent();
    }
    #endregion ProgressForm()

    #endregion Public Construction

    #region Internal helper functions

    #region void ConvertFileFunc()
    /// <summary>
    /// The actual conversion function. Converts the files one by one and updates
    /// the list box as needed.
    /// </summary>
    private void ConvertFileFunc()
    {
      // Create report:
      reportFile = new StringCollection();

      // Process all files:
      for (int i = 0; i < processFiles.Count && !threadStopped; i++)
      {
        // Conversion state:
        bool success        = true;
        string errorMessage = string.Empty;

        try
        {
          // Convert the file:
          FxbpTools.ConvertVst3PresetToFxx(processFiles[i], overWriteExisting, extractXml, dryRun);
        }
        catch (Exception e)
        {
          // Failure, store reasons:
          errorMessage = e.Message;
          success      = false;
        }

        // Update the list box:
        Invoke((MethodInvoker)delegate()
        {
          string[] texts = new string[3];
          texts[0] = string.Empty;
          texts[1] = processFiles[i];
          texts[2] = errorMessage;
          ListViewItem item = new ListViewItem(texts, success ? 0 : 1);
          item.StateImageIndex = success ? 0 : 1;
          listViewFiles.Items.Add(item);
          item.EnsureVisible();
        });

        // Update report file:
        reportFile.Add((success ? "OK   " : "FAIL ") + processFiles[i] + (success ? string.Empty : " Error: " + errorMessage));
      }

      // Done with the files, update form:
      Invoke((MethodInvoker)delegate()
      {
        // Update button states:
        this.buttonCancel.Enabled    = false;
        this.buttonClipboard.Enabled = true;
        this.buttonOK.Enabled        = true;
      });

      // Stop thread:
      threadStopped = true;
      workerThread  = null;
    }
    #endregion void ConvertFileFunc()

    #endregion Internal helper functions

    #region Event Handler

    #region void ProgressForm_Shown(object, EventArgs)
    /// <summary>
    /// Event handler for the Shown event.
    /// </summary>
    /// <param name="sender">
    /// The source control, emitting the event.
    /// </param>
    /// <param name="e">
    /// Event arguments describing the circumstances of the event.
    /// </param>
    private void ProgressForm_Shown(object sender, EventArgs e)
    {
      // Create working thread:
      threadStopped = false;
      workerThread = new Thread(new ThreadStart(ConvertFileFunc));
      workerThread.Start();
    }
    #endregion void ProgressForm_Shown(object, EventArgs)

    #region void ProgressForm_FormClosing(object, FormClosingEventArgs)
    /// <summary>
    /// Event handler for the FormClosing event.
    /// </summary>
    /// <param name="sender">
    /// The source control, emitting the event.
    /// </param>
    /// <param name="e">
    /// Event arguments describing the circumstances of the event.
    /// </param>
    private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      // Only allow closing of this window when the worker thread is finished:
      if ((workerThread != null) && (workerThread.IsAlive))
        e.Cancel = true;
    }
    #endregion void ProgressForm_FormClosing(object, FormClosingEventArgs)

    #region void buttonOK_Click(object, EventArgs)
    /// <summary>
    /// Event handler for the OK-button click event.
    /// </summary>
    /// <param name="sender">
    /// The source control, emitting the event.
    /// </param>
    /// <param name="e">
    /// Event arguments describing the circumstances of the event.
    /// </param>
    private void buttonOK_Click(object sender, EventArgs e)
    {
      // Close this window:
      this.Close();
    }
    #endregion void buttonOK_Click(object, EventArgs)

    #region void buttonCancel_Click(object, EventArgs)
    /// <summary>
    /// Event handler for the Cancel-button click event.
    /// </summary>
    /// <param name="sender">
    /// The source control, emitting the event.
    /// </param>
    /// <param name="e">
    /// Event arguments describing the circumstances of the event.
    /// </param>
    private void buttonCancel_Click(object sender, EventArgs e)
    {
      // Stop the worker thread:
      threadStopped = true;
    }
    #endregion void buttonCancel_Click(object, EventArgs)

    #region void buttonClipboard_Click(object, EventArgs)
    /// <summary>
    /// Event handler for the Copy-button click event.
    /// </summary>
    /// <param name="sender">
    /// The source control, emitting the event.
    /// </param>
    /// <param name="e">
    /// Event arguments describing the circumstances of the event.
    /// </param>
    private void buttonClipboard_Click(object sender, EventArgs e)
    {
      // Copy report to clipboard:
      if (reportFile != null && reportFile.Count > 0)
      {
        // Compose a string out of the report:
        StringBuilder sb = new StringBuilder();
        foreach (string s in reportFile)
          sb.AppendLine(s);

        // Copy to clipboard:
        Clipboard.SetText(sb.ToString());
      }
    }
    #endregion void buttonClipboard_Click(object, EventArgs)

    #endregion Event Handler
  }
  #endregion class ProgressForm
}

///////////////////////////////// End of File /////////////////////////////////

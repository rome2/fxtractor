#region License
///////////////////////////////////////////////////////////////////////////////
//                                                                           //
// File:           FxbpTools.cs                                              //
//                                                                           //
// Functionality:  Fxb/fxp related tools.                                    //
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
using System.IO;
using System.Text;

namespace FXTractor
{
  #region class FxbpTools
  /// <summary>
  /// This class holds all tools for Fxp, Fxb and Vstpreset files.
  /// </summary>
  internal sealed class FxbpTools
  {
    #region Private Construction

    #region FxbpTools()
    /// <summary>
    /// Private constructor to prevent instantiation of this singleton class.
    /// </summary>
    private FxbpTools()
    {
      // Always empty.
    }
    #endregion FxbpTools()

    #endregion Private Construction

    #region Internal Helper Functions

    #region string ReadFourCC(BinaryReader)
    /// <summary>
    /// Convenience function to read a Fourcc chunk id as a string.
    /// </summary>
    /// <param name="br">
    /// The source binary reader.
    /// </param>
    /// <returns>
    /// The resulting fourcc code as string.
    /// </returns>
    private static string ReadFourCC(BinaryReader br)
    {
      char[] c = new char[4];
      c[0] = (char)br.ReadByte();
      c[1] = (char)br.ReadByte();
      c[2] = (char)br.ReadByte();
      c[3] = (char)br.ReadByte();
      return new string(c);
    }
    #endregion string ReadFourCC(BinaryReader)

    #region UInt32 ReadUInt32(BinaryReader, bool)
    /// <summary>
    /// Endian independend read of an unsigned 32 bit integer from a stream.
    /// </summary>
    /// <param name="br">
    /// The source binary reader.
    /// </param>
    /// <param name="invert">
    /// Vstpreset files use both little endian and big endian numbers in the same
    /// file. :-( This parameter selects which one is needed depending on the file
    /// position.
    /// </param>
    /// <returns>
    /// The resulting integer.
    /// </returns>
    private static UInt32 ReadUInt32(BinaryReader br, bool invert)
    {
      if (invert)
        return (UInt32)(br.ReadByte() | (br.ReadByte() << 8) | (br.ReadByte() << 16) | (br.ReadByte() << 24));
      else
        return (UInt32)((br.ReadByte() << 24) | (br.ReadByte() << 16) | (br.ReadByte() << 8) | br.ReadByte());
    }
    #endregion UInt32 ReadUInt32(BinaryReader, bool)

    #endregion Internal Helper Functions

    #region Public Methods

    #region void ConvertVst3PresetToFxx(string, bool, bool)
    /// <summary>
    /// Convert a *.vstpreset file to a *.fxb or *.fxp file.
    /// </summary>
    /// <param name="fileName">
    /// The source file name.
    /// </param>
    /// <param name="overWriteExisting">
    /// Overwrite existing files.
    /// </param>
    /// <param name="extractXml">
    /// Extract the media bay tag data too?
    /// </param>
    /// <param name="dryRun">
    /// Omitt writing of files?
    /// </param>
    public static void ConvertVst3PresetToFxx(string fileName, bool overWriteExisting, bool extractXml, bool dryRun)
    {
      // Check file for existance:
      if (!File.Exists(fileName))
        throw new Exception("File Not Found: " + fileName);

      // Read the file:
      using (Stream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
      {
        try
        {
          // Create a binary reader:
          BinaryReader br = new BinaryReader(file, Encoding.ASCII);

          // Get file size:
          UInt32 fileSize = (UInt32)file.Length;
          if (fileSize < 64)
            throw new Exception("Invalid file size: " + fileSize.ToString());

          // Read file header:
          string chunkID = ReadFourCC(br);
          if (chunkID != "VST3")
            throw new Exception("Invalid file type: " + chunkID);

          // Read version:
          UInt32 fileVersion = ReadUInt32(br, true);

          // Read VST3 ID:
          string vst3ID = new string(br.ReadChars(32));

          // Read filesize - (minus sizeof(this header)):
          UInt32 fileSize2 = ReadUInt32(br, true);

          // Read unknown value:
          UInt32 unknown1 = ReadUInt32(br, true);

          // Read data chunk ID:
          chunkID = ReadFourCC(br);

          // Single preset?
          bool singlePreset = false;
          if (chunkID == "LPXF")
          {
            // Check file size:
            if (fileSize != (fileSize2 + (file.Position - 4)))
              throw new Exception("Invalid file size: " + fileSize);

            // This is most likely a single preset:
            singlePreset = true;
          }
          else if (chunkID == "VstW")
          {
            // Read unknown value (most likely VstW chunk size):
            UInt32 unknown2 = ReadUInt32(br, true);

            // Read unknown value (most likely VstW chunk version):
            UInt32 unknown3 = ReadUInt32(br, true);

            // Read unknown value (no clue):
            UInt32 unknown4 = ReadUInt32(br, true);

            // Check file size (The other check is needed because Cubase tends to forget the items of this header:
            if ((fileSize != (fileSize2 + file.Position + 4)) && (fileSize != (fileSize2 + file.Position - 16)))
              throw new Exception("Invalid file size: " + fileSize);

            // This is most likely a preset bank:
            singlePreset = false;
          }

          // Unknown file:
          else
            throw new Exception("This file does not contain any FXB or FXP data (1)");

          // OK, getting here we should have access to a fxp/fxb chunk:
          long chunkStart = file.Position;
          chunkID = ReadFourCC(br);
          if (chunkID != "CcnK")
            throw new Exception("This file does not contain any FXB or FXP data (2)");

          // OK, seems to be a valid fxb or fxp chunk. Get chunk size:
          UInt32 chunkSize = ReadUInt32(br, false) + 8;
          if ((file.Position + chunkSize) >= fileSize)
            throw new Exception("Invalid chunk size: " + chunkSize);

          // Read magic value:
          chunkID = ReadFourCC(br);

          // Is a single preset?
          if (chunkID == "FxCk" || chunkID == "FPCh")
          {
            // Check consistency with the header:
            if (singlePreset == false)
              throw new Exception("Header indicates a bank file but data seems to be a preset file (" + chunkID + ").");
          }

          // Is a bank?
          else if (chunkID == "FxBk" || chunkID == "FBCh")
          {
            // Check consistency with the header:
            if (singlePreset == true)
              throw new Exception("Header indicates a preset file but data seems to be a bank file (" + chunkID + ").");
          }

          // And now for something completely different:
          else
            throw new Exception("This file does not contain any FXB or FXP data (3)");

          // OK, we've found something usefull, create new file name:
          string newFileName = Path.ChangeExtension(fileName, singlePreset ? ".fxp" : ".fxb");

          // Read the source data:
          file.Position = chunkStart;
          byte[] fileData = br.ReadBytes((int)chunkSize);

          // Write to new file:
          if (!dryRun && (!File.Exists(newFileName) || overWriteExisting))
          {
            Stream destFile = null;
            try
            {
              // Create target stream:
              destFile = new FileStream(newFileName, FileMode.Create, FileAccess.Write);

              // Create a binary writer:
              BinaryWriter wr = new BinaryWriter(destFile, Encoding.ASCII);

              // Write contents:
              wr.Write(fileData);
            }
            finally
            {
              // Cleanup:
              if (destFile != null)
                destFile.Close();
            }
          }

          // This part is really quick and dirty. Needs a major rewrite if someone actually uses
          // this feature for something usefull.
          if (extractXml)
          {
            // Read xml indicator:
            UInt32 xmlid = ReadUInt32(br, true);
            if (xmlid != 0x3CBFBBEF)
              throw new Exception("Extraction of XML data failed.");

            // We need a specialized reader because the xml data is stored as utf8:
            file.Position -= 1;
            BinaryReader ubr = new BinaryReader(file, Encoding.UTF8);
            StringBuilder sb = new StringBuilder();
            while (true)
            {
              char c = ubr.ReadChar();
              if (c == 0 || (file.Position == file.Length))
                break;
              sb.Append(c);
            }
            string xmlData = sb.ToString();
            xmlData = xmlData.Substring(0, xmlData.LastIndexOf('>') + 1);

            // OK, we've found something usefull, create new file name:
            newFileName = Path.ChangeExtension(fileName, ".xml");

            // Write to new file:
            if (!dryRun && (!File.Exists(newFileName) || overWriteExisting))
            {
              Stream destFile = null;
              try
              {
                // Create target stream:
                destFile = new FileStream(newFileName, FileMode.Create, FileAccess.Write);

                // Create a binary writer:
                BinaryWriter wr = new BinaryWriter(destFile, Encoding.UTF8);

                // Write contents:
                wr.Write(xmlData.ToCharArray());
              }
              finally
              {
                // Cleanup:
                if (destFile != null)
                  destFile.Close();
              }
            }
          }
        }
        finally
        {
          // Cleanup:
          if (file != null)
            file.Close();
        }
      }
    }
    #endregion void ConvertVst3PresetToFxx(string, bool, bool)

    #endregion Public Methods
  }
  #endregion class FxbpTools
}

///////////////////////////////// End of File /////////////////////////////////

﻿/*
==========================================================================
This file is part of Tools of Doom, a library providing a collection of
classes to load/edit/save Doom maps and wad archives, created by @akaAgar
(https://github.com/akaAgar/tools-of-doom).

Tools of Doom is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Tools of Doom is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Tools of Doom. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using System.IO;
using System.Text.RegularExpressions;
using ToolsOfDoom.Wad;

namespace ToolsOfDoom
{
    public sealed class WadPacker
    {
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">Array of command-line parameters</param>
        private static void Main(string[] args)
        {
            if (args.Length == 0)
                return;

            using (WadFile wad = new WadFile())
            {
                foreach (string file in args)
                {
                    if (!File.Exists(file)) continue;

                    string lumpName = Regex.Replace(
                        Path.GetFileNameWithoutExtension(file).ToUpperInvariant(),
                        @"[^\u0000-\u007F]+", string.Empty); // Remove non-ASCII characters

                    wad.AddLump(lumpName, File.ReadAllBytes(file));
                }

                wad.SaveToFile("PackedFiles.wad");
            }
        }
    }
}

//  wherugo - WherUGo for Magellan eXplorist x10
//  Copyright (C) 2011 Peter Siegmund <developer@mars3142.org>
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Windows.Forms;
using org.mars3142.wherugo.Cartridges;

namespace org.mars3142.wherugo.decoder.Windows
{
   public partial class Main : Form
   {
      File _gwc = new File();

      public Main()
      {
         InitializeComponent();
      }

      ~Main()
      {
         _gwc = null;
      }

      private void pbOpen_Click(object sender, EventArgs e)
      {
         if (fdGWC.ShowDialog() == DialogResult.OK)
            _gwc.Read(fdGWC.FileName);
      }
   }
}
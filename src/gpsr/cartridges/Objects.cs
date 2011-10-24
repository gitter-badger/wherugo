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
using System.IO;
using org.mars3142.wherugo.Shared;

namespace org.mars3142.wherugo.Cartridges
{
   public class Objects
   {
      #region Members
      private readonly short _objectId;
      public short ObjectId
      {
         get { return _objectId; }
      }

      private readonly long _address;
      public long Address
      {
         get { return _address; }
      }

      private string _data;
      public string Data
      {
         get { return _data; }
      }

      private long _objectType;
      public long ObjectType
      {
         get { return _objectType; }
      }
      public string ObjectTypeString
      {
         get
         {
            string type = String.Format("unknown ({0})", _objectType);
            
            switch (_objectType)
            {
               case 0:
                  type = "Lua bytecode";
                  break;
               case 1:
                  type = "BMP";
                  break;
               case 2:
                  type = "PNG";
                  break;
               case 3:
                  type = "JPG";
                  break;
               case 4:
                  type = "GIF";
                  break;
               case 17:
                  type = "WAV";
                  break;
               case 19:
                  type = "FDL";
                  break;
            }
            return type;
         }
      }
      #endregion

      #region Ctr
      public Objects(short objectId, long address)
      {
         _objectId = objectId;
         _address = address;
      }
      #endregion

      public bool LoadObject(BinaryReader binaryReader)
      {
         byte value;
         bool retValue = false;

         binaryReader.BaseStream.Seek(_address, SeekOrigin.Begin);
         try
         {
            long length;
            if (_objectId == 0)
            {
               length = SeekFile.GetLong(binaryReader);
               for (int i = 0; i < length; i++)
               {
                  _data += binaryReader.ReadByte();
               }
            }
            else
            {
               byte validObj = SeekFile.GetByte(binaryReader);
               if (validObj != 0)
               {
                  _objectType = SeekFile.GetLong(binaryReader);
                  length = SeekFile.GetLong(binaryReader);
                  for (int i = 1; i < length; i++)
                  {
                     _data += binaryReader.ReadByte();
                  }
               }
            }
            retValue = true;
         }
         catch (Exception ex)
         {
            throw new WherugoException("Cartridges.Objects.LoadObject()", ex);
         }

         return retValue;
      }
   }
}
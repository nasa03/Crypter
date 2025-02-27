﻿/*
 * Copyright (C) 2023 Crypter File Transfer
 * 
 * This file is part of the Crypter file transfer project.
 * 
 * Crypter is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * The Crypter source code is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 * 
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * You can be released from the requirements of the aforementioned license
 * by purchasing a commercial license. Buying such a license is mandatory
 * as soon as you develop commercial activities involving the Crypter source
 * code without disclosing the source code of your own applications.
 * 
 * Contact the current copyright holder to discuss commercial license options.
 */

using System;
using System.Text;
using System.Text.Json;

namespace Crypter.ClientServices.Interfaces.Models
{
   public class RecoveryKey
   {
      public byte[] MasterKey { get; set; }
      public byte[] Proof { get; set; }

      public RecoveryKey(byte[] masterKey, byte[] proof)
      {
         MasterKey = masterKey;
         Proof = proof;
      }

      public string ToBase64String()
      {
         string json = JsonSerializer.Serialize(this);
         byte[] bytes = Encoding.UTF8.GetBytes(json);
         return Convert.ToBase64String(bytes);
      }

      public static RecoveryKey FromBase64String(string encodedRecoveryKey)
      {
         byte[] bytes = Convert.FromBase64String(encodedRecoveryKey);
         string json = Encoding.UTF8.GetString(bytes);
         return JsonSerializer.Deserialize<RecoveryKey>(json);
      }
   }
}

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

using Crypter.ClientServices.Interfaces;
using Crypter.ClientServices.Transfer.Handlers;
using Crypter.ClientServices.Transfer.Models;
using Crypter.Common.Enums;
using Crypter.Crypto.Common;
using System.IO;

namespace Crypter.ClientServices.Transfer
{
   public class TransferHandlerFactory
   {
      private readonly ICrypterApiService _crypterApiService;
      private readonly ICryptoProvider _cryptoProvider;
      private readonly IUserSessionService _userSessionService;
      private readonly TransferSettings _transferSettings;

      public TransferHandlerFactory(ICrypterApiService crypterApiService, ICryptoProvider cryptoProvider,
         IUserSessionService userSessionService, TransferSettings transferSettings)
      {
         _crypterApiService = crypterApiService;
         _cryptoProvider = cryptoProvider;
         _userSessionService = userSessionService;
         _transferSettings = transferSettings;
      }

      public UploadFileHandler CreateUploadFileHandler(Stream fileStream, string fileName, long fileSize, string fileContentType, int expirationHours)
      {
         var handler = new UploadFileHandler(_crypterApiService, _cryptoProvider, _transferSettings);
         handler.SetTransferInfo(fileStream, fileName, fileSize, fileContentType, expirationHours);
         return handler;
      }

      public UploadMessageHandler CreateUploadMessageHandler(string messageSubject, string messageBody, int expirationHours)
      {
         var handler = new UploadMessageHandler(_crypterApiService, _cryptoProvider, _transferSettings);
         handler.SetTransferInfo(messageSubject, messageBody, expirationHours);
         return handler;
      }

      public DownloadFileHandler CreateDownloadFileHandler(string hashId, TransferUserType userType)
      {
         var handler = new DownloadFileHandler(_crypterApiService, _cryptoProvider, _userSessionService, _transferSettings);
         handler.SetTransferInfo(hashId, userType);
         return handler;
      }

      public DownloadMessageHandler CreateDownloadMessageHandler(string hashId, TransferUserType userType)
      {
         var handler = new DownloadMessageHandler(_crypterApiService, _cryptoProvider, _userSessionService, _transferSettings);
         handler.SetTransferInfo(hashId, userType);
         return handler;
      }
   }
}

using RS.FileTransfer.Common.Models;
using RS.FileTransfer.Service.Queues;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RS.FileTransfer.Service.Controllers
{
    [Authorize]
    public class FileTransferAPIController : BaseAPIController
    {
        [HttpPost]
        [Route("api/file/offer")]
        public Guid OfferFile([FromBody]FileOfferModel model)
        {
            if ((model.OfferedToUserName != UserName) && (!Program.ConnectionsManager.ConnectionExists(UserName, model.OfferedToUserName)))
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "You are not authorised to send files to the specified user" });

            if ((model.NumberOfFileParts == 0) || (model.NumberOfFileParts > 5000))
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Number of file parts is invalid." });

            var details = new FileDetails()
            {
                Date = DateTime.Now,
                Description = model.Description,
                FileName = model.FileName,
                Size = model.Size,
                Hash = model.Hash,
                NumberOfFileParts = model.NumberOfFileParts,
                SourceDeviceId = DeviceId,
                SourceUserName = UserName,
                DestinationDeviceId = null,
                DestinationUserName = model.OfferedToUserName
            };

            QueueInstances.FileTransfersQueue.Add(details);
#if DEBUG
            Console.WriteLine(string.Format("OfferFile : Created FileTransfer offer. File Name '{0}, from user '{1}' to '{2}'", model.FileName, UserName, model.OfferedToUserName));
#endif
            UserCommandsLock.Wake(UserName);
            return details.Id;
        }

        /*
        [HttpPost]
        [Route("api/file/request")]
        public Guid RequestFile([FromBody]FileRequestModel model)
        {
            if (!Program.ConnectionsManager.ConnectionExists(UserName, model.RequestedFromUserName))
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "You are not authorised to request files from to the specified user" });

            var details = new FileDetails()
            {
                Date = DateTime.Now,
                Description = null,
                FileName = model.FileName,
                SourceDeviceId = null,
                SourceUserName = model.RequestedFromUserName,
                DestinationDeviceId = DeviceId,
                DestinationUserName = UserName
            };

            QueueInstances.FileTransfersQueue.Add(details);
#if DEBUG
            Console.WriteLine(string.Format("RequestFile : Created FileTransfer request. File Name '{0}, from user '{1}' to '{2}'", model.FileName, model.RequestedFromUserName, UserName));
#endif
            UserCommandsLock.Wake(model.RequestedFromUserName);
            return details.Id;
        }
         * */

        [HttpGet]
        [Route("api/file/uploadcommand/{fileId:Guid}")]
        public FileUploadCommand GetFileUploadCommand(Guid fileId)
        {
            var item = QueueInstances.FileTransfersQueue.Get(fileId);
            if (item == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Invalid file Id." });

            if (item.SourceUserName.ToLower() != UserName.ToLower())
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "You are not authorised to access this file information." });

            return new FileUploadCommand()
            {
                FileId = item.Id,
                DestinationUserName = item.DestinationUserName,
                FileName = item.FileName,
                Size = item.Size,
                Hash = item.Hash,
                NumberParts = item.NumberOfFileParts,
                Description = item.Description
            };
        }

        [HttpGet]
        [Route("api/file/downloadcommand/{fileId:Guid}")]
        public FileDownloadCommand GetFileDownloadCommand(Guid fileId)
        {
            var item = QueueInstances.FileTransfersQueue.Get(fileId);
            if (item == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Invalid file Id." });

            if (item.DestinationUserName.ToLower() != UserName.ToLower())
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "You are not authorised to access this file information." });

            if (!item.UploadCompletedDate.HasValue)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "File is not ready." });

            return new FileDownloadCommand()
                {
                    FileId = item.Id,
                    SourceDeviceId = item.SourceDeviceId,
                    SourceUserName = item.SourceUserName,
                    FileName = item.FileName,
                    Description = item.Description,
                    UploadedDate = item.UploadCompletedDate.Value,
                    Size = item.Size,
                    Hash = item.Hash,
                    NumberParts = item.FileParts.Count
                };
        }

        [HttpGet]
        [Route("api/file/download/{fileId:Guid}/{filePartNumber:int}")]
        public HttpResponseMessage DownloadFile(Guid fileId, int filePartNumber)
        {
            var fileDetails = QueueInstances.FileTransfersQueue.Get(fileId);
            if (fileDetails == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Invalid file Id." });

            if (fileDetails.DestinationUserName.ToLower() != UserName.ToLower())
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not authorised to download the specified file");

            var filePart = fileDetails.FileParts.FirstOrDefault(x => x.PartNumber == filePartNumber);
            if (filePart == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Invalid part number." });

            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StreamContent(new FileStream(filePart.FilePath, FileMode.Open));
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileDetails.FileName
            };
#if DEBUG
            Console.WriteLine(string.Format("DownloadFile : File '{0}' Part : {1} downloaded by User '{2}'", fileDetails.FileName, filePartNumber, UserName));
#endif
            return response;
        }

        [HttpPost]
        [Route("api/file/upload/{fileId:Guid}/{filePartNumber:int}")]
        public async Task<HttpResponseMessage> UploadFile(Guid fileId, int filePartNumber)
        {
            if (filePartNumber > 5000)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Invalid part number." });

            var fileDetails = QueueInstances.FileTransfersQueue.Get(fileId);
            if (fileDetails == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Invalid file Id." });

            if (fileDetails.SourceUserName.ToLower() != UserName.ToLower())
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not authorised to download the specified file");

            if (fileDetails.NumberOfFileParts < filePartNumber)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Part number too large." });

            var filePart = fileDetails.FileParts.FirstOrDefault(x => x.PartNumber == filePartNumber);
            if (filePart != null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "File Part already uploaded." });

            // Check if the request contains multipart/form-data. 
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            if (!File.Exists("c:\\uploadedFiles"))
                Directory.CreateDirectory("c:\\uploadedFiles");

            var provider = new MultipartFormDataStreamProvider("c:\\uploadedFiles");

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                if (provider.FileData.Count != 1)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Invalid file data." });

                var fileData = provider.FileData[0];

                if (fileDetails.FileParts.Count == 0)
                    fileDetails.UploadStartDate = DateTime.Now;

                fileDetails.FileParts.Add(new FilePart()
                {
                    FilePath = fileData.LocalFileName,
                    PartNumber = filePartNumber
                });
#if DEBUG
                Console.WriteLine(string.Format("UploadFile : File '{0}', part : {1} uploaded by User '{1}'", fileDetails.FileName, filePartNumber, UserName));
#endif

                if (fileDetails.FileParts.Count >= fileDetails.NumberOfFileParts)
                {
                    fileDetails.UploadCompletedDate = DateTime.Now;
                    UserCommandsLock.Wake(fileDetails.DestinationUserName);
                }

                return new HttpResponseMessage() { StatusCode  = HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                if (ex is HttpResponseException)
                    throw;
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}

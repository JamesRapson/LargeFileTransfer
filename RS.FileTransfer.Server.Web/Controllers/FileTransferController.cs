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
    public class FileTransferController : ApiController
    {
        [HttpPost]
        [Route("api/filetransfer/offer")]
        public Guid FileOffer([FromBody]FileOfferModel model)
        {
            var details = new FileTransferDetails()
            {
                Date = DateTime.Now,
                Description = model.Description,
                FileName = model.FileName,
                Size = model.Size,
                SourceDeviceId = model.OfferedFromDeviceId,
                SourceUserName = model.OfferedByUserName,
                DestinationDeviceId = null,
                DestinationUserName = model.OfferedToUserName
            };

            QueueInstances.FileTransfersQueue.Add(details);
            return details.Id;
        }

        [HttpPost]
        [Route("api/filetransfer/request")]
        public Guid FileOffer([FromBody]FileRequestModel model)
        {
            var details = new FileTransferDetails()
            {
                Date = DateTime.Now,
                Description = null,
                FileName = model.FileName,
                Size = 0,
                SourceDeviceId = null,
                SourceUserName = model.RequestedFromUserName,
                DestinationDeviceId = model.RequestedByDeviceId,
                DestinationUserName = model.RequestedByUserName
            };

            QueueInstances.FileTransfersQueue.Add(details);
            return details.Id;
        }

        [HttpPost]
        [Route("api/filetransfers")]
        public IEnumerable<FileTransferDetailsModel> GetFileTransfersDetails([FromBody]Guid[] ids)
        {
            var list = new List<FileTransferDetailsModel>();
            foreach (var item in QueueInstances.FileTransfersQueue.Get(ids))
            {
                list.Add(new FileTransferDetailsModel()
                {
                    Id = item.Id,
                    SourceDeviceId = item.SourceDeviceId,
                    SourceUserName = item.SourceUserName,
                    DestinationDeviceId = item.DestinationDeviceId,
                    DestinationUserName = item.DestinationUserName,
                    Date = item.Date,
                    FileName = item.FileName,
                    Description = item.Description,
                    Size = item.Size
                });
            }
            return list;
        }

        [HttpPost]
        [Route("api/availablefiles")]
        public IEnumerable<AvailableFileDetailsModel> GetUploadedFiles([FromBody]Guid[] ids)
        {
            var list = new List<AvailableFileDetailsModel>();
            foreach (var item in QueueInstances.AvailableFilesQueue.Get(ids))
            {
                list.Add(new AvailableFileDetailsModel()
                {
                    Id = item.Id,
                    UploadedFromDeviceId = item.UploadedFromDeviceId,
                    UploadedByUserName = item.UploadedByUserName,
                    FileName = item.FileName,
                    UploadedDate = item.Date,
                    Size = item.Size,
                    Hash = item.Hash
                });
            }
            return list;
        }

        [HttpGet]
        [Route("api/file/download/{fileId:Guid}")]
        public HttpResponseMessage DownloadFile(Guid fileId)
        {
            FileDetails fileDetails = QueueInstances.AvailableFilesQueue.Get(fileId);
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StreamContent(new FileStream(fileDetails.FilePath, FileMode.Open));
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileDetails.FileName
            };
            return response;
        }


        [HttpPost]
        [Route("api/file/upload")]
        public async Task<HttpResponseMessage> UploadFile()
        {
            // Check if the request contains multipart/form-data. 
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data and return an async task. 
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    Guid id = new Guid(file.Headers.ContentDisposition.Name);
                    var transfer = QueueInstances.FileTransfersQueue.Get(id);
                    if (transfer == null)
                        throw new Exception("Unknown File Id");

                    QueueInstances.AvailableFilesQueue.Delete(id);
                    FileInfo info = new FileInfo(file.LocalFileName);
                    QueueInstances.AvailableFilesQueue.Add(
                        new FileDetails()
                        {
                            FileName = file.Headers.ContentDisposition.FileName,
                            FilePath = file.LocalFileName,
                            Date = DateTime.Now,
                            Size = info.Length,
                            DestinationUserName = transfer.DestinationUserName,
                            UploadedByUserName = "TODO"
                        });
                }
                return new HttpResponseMessage() { StatusCode  = HttpStatusCode.OK };
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}

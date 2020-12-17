using Newtonsoft.Json;
using RS.FileTransfer.Common.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


namespace RS.FileTransfer.Client
{
    public class WebAPIException : Exception
    {
        string _message;
        string _errorDesc;
        HttpStatusCode _errorCode;

        public WebAPIException(string message, string errorDesc, HttpStatusCode errorCode)
        {
            _message = message;
            _errorDesc = errorDesc;
            _errorCode = errorCode;
        }

        public override string ToString()
        {
            return string.Format("{0}. Error - {1} (Code : {2})", _message, _errorDesc, _errorCode);
        }

        public override string Message
        {
            get { return ToString(); }
        }
    }

    public class ServerCommunications
    {
        ConfigurationDetails _configuration = null;
        Dictionary<string, string> _tokenDictionary = null;
        string _accessToken = null;

        public ServerCommunications(ConfigurationDetails configuration)
        {
            _configuration = configuration;
        }

        public bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(_accessToken);
        }

        public void Logout()
        {
            _tokenDictionary = null;
            _accessToken = null;
        }

        public async Task LogIn(string userName, string password)
        {
            _tokenDictionary = null;
            _accessToken = null;

            try
            {
                var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ), 
                    new KeyValuePair<string, string>( "username", userName ), 
                    new KeyValuePair<string, string> ( "password", password ),
                    new KeyValuePair<string, string> ( "deviceid", _configuration.DeviceId.ToString() )
                };
                var content = new FormUrlEncodedContent(pairs);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var url = string.Format("{0}/Token", _configuration.ServerBaseUrl);
                    var response = await client.PostAsync(url, content);

                    var responseContent = await response.Content.ReadAsStringAsync();
                    _tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred performing login. " + ex.Message);
            }

            if (_tokenDictionary.ContainsKey("error"))
                throw new Exception(string.Format("Login failure. {0}", _tokenDictionary["error_description"]));

            _accessToken = _tokenDictionary["access_token"];
        }

        public async Task<CommandModel> WaitCommand(CancellationTokenSource cts)
        {
            try
            {
                if (string.IsNullOrEmpty(_accessToken))
                    throw new Exception("Not logged in");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(0, 0, 6000);

                    var url = string.Format("{0}/api/command", _configuration.ServerBaseUrl);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    var response = await client.GetAsync(url, cts.Token);
                    if (!response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.RequestTimeout)
                            return null;
                        throw new WebAPIException("An error occurred getting command", response.ReasonPhrase, response.StatusCode);
                    }
                    return await response.Content.ReadAsAsync<CommandModel>();
                }
            }
            catch (Exception ex)
            {
                if (ex is WebAPIException)
                    throw;
                throw new Exception("An error occurred getting command. " + ex.Message);
            }
        }

        public async Task<List<ReceivedMessageModel>> WaitMessages(CancellationTokenSource cts)
        {
            try
            {
                if (string.IsNullOrEmpty(_accessToken))
                    throw new Exception("Not logged in");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(0, 0, 6000);

                    var url = string.Format("{0}/api/message/wait", _configuration.ServerBaseUrl);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    var response = await client.GetAsync(url, cts.Token);
                    if (!response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.RequestTimeout)
                            return null;
                        throw new WebAPIException("An error occurred getting command", response.ReasonPhrase, response.StatusCode);
                    }
                    return await response.Content.ReadAsAsync<List<ReceivedMessageModel>>();
                }
            }
            catch (Exception ex)
            {
                if (ex is WebAPIException)
                    throw;
                throw new Exception("An error occurred waiting for messages. " + ex.Message);
            }
        }

        public async Task SendMessage(SendMessageModel message)
        {
            try
            {
                if (string.IsNullOrEmpty(_accessToken))
                    throw new Exception("Not logged in");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var url = string.Format("{0}/api/message/send", _configuration.ServerBaseUrl);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    var response = await client.PostAsJsonAsync<SendMessageModel>(url, message);
                    if (!response.IsSuccessStatusCode)
                        throw new WebAPIException("An error occurred sending message", response.ReasonPhrase, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                if (ex is WebAPIException)
                    throw;
                throw new Exception("An error occurred sending message. " + ex.Message);
            }
        }

        public async Task<List<ReceivedMessageModel>> GetMessage(IEnumerable<Guid> itemIds)
        {
            try
            {
                if (string.IsNullOrEmpty(_accessToken))
                    throw new Exception("Not logged in");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var url = string.Format("{0}/api/message/get", _configuration.ServerBaseUrl);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    var response = await client.PostAsJsonAsync<Guid[]>(url, itemIds.ToArray());
                    if (!response.IsSuccessStatusCode)
                        throw new WebAPIException("An error occurred gettting message", response.ReasonPhrase, response.StatusCode);

                    var items = await response.Content.ReadAsAsync<ReceivedMessageModel[]>();
                    return items.ToList();
                }
            }
            catch (Exception ex)
            {
                if (ex is WebAPIException)
                    throw;
                throw new Exception("An error occurred gettting message. " + ex.Message);
            }
        }

        public async Task<Guid> SendFileTransferOffer(FileOfferModel fileOffer)
        {
            try
            {
                if (string.IsNullOrEmpty(_accessToken))
                    throw new Exception("Not logged in");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var url = string.Format("{0}/api/file/offer", _configuration.ServerBaseUrl);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    var response = await client.PostAsJsonAsync<FileOfferModel>(url, fileOffer);
                    if (!response.IsSuccessStatusCode)
                        throw new WebAPIException("An error occurred sending file information", response.ReasonPhrase, response.StatusCode);

                    return await response.Content.ReadAsAsync<Guid>();
                }
            }
            catch (Exception ex)
            {
                if (ex is WebAPIException)
                    throw;
                throw new Exception("An error occurred sending file information. " + ex.Message);
            }
        }

        public async Task<FileDownloadCommand> GetFileDownloadCommand(Guid itemId)
        {
            try
            {
                if (string.IsNullOrEmpty(_accessToken))
                    throw new Exception("Not logged in");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var url = string.Format("{0}/api/file/downloadcommand/{1}", _configuration.ServerBaseUrl, itemId);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                        throw new WebAPIException("An error occurred getting file download command", response.ReasonPhrase, response.StatusCode);

                    var item = await response.Content.ReadAsAsync<FileDownloadCommand>();
                    return item;
                }
            }
            catch (Exception ex)
            {
                if (ex is WebAPIException)
                    throw;
                throw new Exception("An error occurred getting file download command. " + ex.Message);
            }
        }

        public async Task<FileUploadCommand> GetFileUploadCommand(Guid itemId)
        {
            try
            {
                if (string.IsNullOrEmpty(_accessToken))
                    throw new Exception("Not logged in");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var url = string.Format("{0}/api/file/uploadcommand/{1}", _configuration.ServerBaseUrl, itemId);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                        throw new WebAPIException("An error occurred getting file upload command", response.ReasonPhrase, response.StatusCode);

                    var item = await response.Content.ReadAsAsync<FileUploadCommand>();
                    return item;
                }
            }
            catch (Exception ex)
            {
                if (ex is WebAPIException)
                    throw;
                throw new Exception("An error occurred getting file upload command. " + ex.Message);
            }
        }

        public async Task UploadFilePart(FileUploadCommand uploadCommand, byte[] data, int filePartIndex)
        {
            try
            {
                if (string.IsNullOrEmpty(_accessToken))
                    throw new Exception("Not logged in");

                var fileName = Path.GetFileName(uploadCommand.FileName);
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
                    {
                        content.Add(new StreamContent(new MemoryStream(data)), uploadCommand.FileId.ToString(), fileName);
                        var url = string.Format("{0}/api/file/upload/{1}/{2}", _configuration.ServerBaseUrl, uploadCommand.FileId, filePartIndex);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                        var response = await client.PostAsync(url, content);
                        if (!response.IsSuccessStatusCode)
                            throw new WebAPIException("An error occurred uploading file", response.ReasonPhrase, response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is WebAPIException)
                    throw;
                throw new Exception("An error occurred uploading file. " + ex.Message);
            }
        }

        public async Task DownloadFilePart(FileDownloadCommand downloadCommand, string filePartPath, int filePartIndex)
        {
            try
            {
                if (string.IsNullOrEmpty(_accessToken))
                    throw new Exception("Not logged in");

                using (var client = new HttpClient())
                {
                    var url = string.Format("{0}/api/file/download/{1}/{2}", _configuration.ServerBaseUrl, downloadCommand.FileId, filePartIndex);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                        throw new WebAPIException("An error occurred downloading file", response.ReasonPhrase, response.StatusCode);

                    using (Stream contentStream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var stream = new FileStream(filePartPath, FileMode.Create, FileAccess.Write, FileShare.None, 10000, true))
                        {
                            contentStream.CopyTo(stream);
                            stream.Flush();
                            stream.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is WebAPIException)
                    throw;
                throw new Exception("An error occurred downloading file. " + ex.Message);
            }
        }

        public async Task<List<ConnectionRequestModel>> GetConnectionRequest(IEnumerable<Guid> itemIds)
        {
            try
            {
                if (string.IsNullOrEmpty(_accessToken))
                    throw new Exception("Not logged in");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var url = string.Format("{0}/api/connection/request/get", _configuration.ServerBaseUrl);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    var response = await client.PostAsJsonAsync<Guid[]>(url, itemIds.ToArray());
                    if (!response.IsSuccessStatusCode)
                        throw new WebAPIException("An error occurred gettting request", response.ReasonPhrase, response.StatusCode);

                    var items = await response.Content.ReadAsAsync<ConnectionRequestModel[]>();
                    return items.ToList();
                }
            }
            catch (Exception ex)
            {
                if (ex is WebAPIException)
                    throw;
                throw new Exception("An error occurred gettting connection requests. " + ex.Message);
            }
        }

        public async Task<List<UserConnectionModel>> GetConnections()
        {
            try
            {
                if (string.IsNullOrEmpty(_accessToken))
                    throw new Exception("Not logged in");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var url = string.Format("{0}/api/connection/all", _configuration.ServerBaseUrl);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                        throw new WebAPIException("An error occurred gettting connections list", response.ReasonPhrase, response.StatusCode);

                    var items = await response.Content.ReadAsAsync<List<UserConnectionModel>>();
                    return items.ToList();
                }
            }
            catch (Exception ex)
            {
                if (ex is WebAPIException)
                    throw;
                throw new Exception("An error occurred gettting connection requests. " + ex.Message);
            }
        }

        public async Task SendConnectionRequest(CreateConnectionRequestModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(_accessToken))
                    throw new Exception("Not logged in");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var url = string.Format("{0}/api/connection/request", _configuration.ServerBaseUrl);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    var response = await client.PostAsJsonAsync<CreateConnectionRequestModel>(url, model);
                    if (!response.IsSuccessStatusCode)
                        throw new WebAPIException("An error occurred requesting new contact", response.ReasonPhrase, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                if (ex is WebAPIException)
                    throw;
                throw new Exception("An error occurred requesting new contact. " + ex.Message);
            }
        }

        public async Task SetContactRequestStatus(ConnectionRequestUpdateStatusModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(_accessToken))
                    throw new Exception("Not logged in");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var url = string.Format("{0}/api/connection/request/setstatus", _configuration.ServerBaseUrl);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    var response = await client.PostAsJsonAsync<ConnectionRequestUpdateStatusModel>(url, model);
                    if (!response.IsSuccessStatusCode)
                        throw new WebAPIException("An error occurred approving contact request", response.ReasonPhrase, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                if (ex is WebAPIException)
                    throw;
                throw new Exception("An error occurred approving contact request. " + ex.Message);
            }
        }       
    }
}

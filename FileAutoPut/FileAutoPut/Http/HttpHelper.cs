using FileAutoPut.Common;
using FileAutoPut.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 FileAutoPut.Http
 * 接口名称 HttpHelper
 * 开发人员：-nhy
 * 创建时间：2022/4/13 9:26:23
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace FileAutoPut.Http
{
    public class HttpHelper
    {
        private HttpClient httpClient;
        public HttpHelper()
        {
            httpClient=new HttpClient();
        }
        private string Boundary = "EAD567A8E8524B2FAC2E0628ABB6DF6E";
        private string ContentType = "Content-Type";
        private string Form_data = $"multipart/form-data;";


        /// <summary>
        /// 批量提交文件
        /// </summary>
        /// <param name="files"></param>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<HttpMessageModel<string>> UpLoadFile(FileEntity [] files,string url,ApiEntity api,string token,string projectName,string moduleName)
        {
            Console.WriteLine("UploadFile");
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }
            if (files==null)
            {
                throw new ArgumentNullException(nameof(files));
            }
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethodExtension.GetHttpMethod(api.HttpMethod), $"{url}{api.Path}");
            httpRequest.Headers.Add("Authorization",$"Bearer {token}");
            var requestContent = new MultipartFormDataContent(Boundary);
            httpRequest.Content = requestContent;
            requestContent.Headers.Remove(ContentType);
            requestContent.Headers.TryAddWithoutValidation(ContentType, $"{Form_data} boundary={Boundary}");
            foreach (var file in files)
            {
             requestContent.Add(new ByteArrayContent(await File.ReadAllBytesAsync(file.FilePath)), "files", file.FileName);
            }    
            requestContent.Add(new StringContent(projectName), "projectName");
            requestContent.Add(new StringContent(moduleName), "moduleName");
            HttpResponseMessage result = await httpClient.SendAsync(httpRequest);
            HttpMessageModel<string> data = null;
            if (result.StatusCode== System.Net.HttpStatusCode.OK)
            {
                Stream stream = await result.Content.ReadAsStreamAsync();
                StreamReader sr = new StreamReader(stream);
                string msg = sr.ReadToEnd();
                data = JsonConvert.DeserializeObject<HttpMessageModel<string>>(msg);
            }
            return data;
          
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<HttpMessageModel<string>> Login(string account,string password, string url,ApiEntity api)
        {
            Console.WriteLine("login");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethodExtension.GetHttpMethod(api.HttpMethod), $"{url}{api.Path}");
            var requestContent = new MultipartFormDataContent(Boundary);
            requestContent.Headers.Remove(ContentType);
            requestContent.Headers.Add(ContentType,$"{Form_data} boundary={Boundary}");
            httpRequest.Content = requestContent;

            requestContent.Add(new StringContent(account), "Account");
            requestContent.Add(new StringContent(password), "UserPassword");
            HttpResponseMessage result = await httpClient.SendAsync(httpRequest);
            HttpMessageModel<string> data=null;
            if (result.StatusCode==System.Net.HttpStatusCode.OK)
            {
                Stream stream = await result.Content.ReadAsStreamAsync();
                StreamReader sr = new StreamReader(stream);
                string msg = sr.ReadToEnd();
                data = JsonConvert.DeserializeObject<HttpMessageModel<string>>(msg);
            }
            return data;
        }
    }
}

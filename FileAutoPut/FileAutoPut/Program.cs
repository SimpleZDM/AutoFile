using FileAutoPut.Entity;
using FileAutoPut.FileHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

namespace FileAutoPut
{
    internal class Program
    {

        #region
        // 关闭64位（文件系统）的操作转向
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);
        // 开启64位（文件系统）的操作转向
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        private bool disabled = false;
        private IntPtr oldWOW64State = new IntPtr();

        /// <summary>
        /// 禁止64位系统转向（true关闭转向，false开启）
        /// </summary>
        public bool DisableWow64
        {
            get
            {
                return disabled;
            }
            set
            {
                if (disabled == value)
                {
                    return;
                }

                disabled = value;


                if (disabled)
                {
                    Wow64RevertWow64FsRedirection(oldWOW64State);
                }
                else
                {
                    Wow64RevertWow64FsRedirection(oldWOW64State);
                }

            }
        }
        #endregion
        static void Main(string[] args)
        {
            Management management = new Management();
            Console.WriteLine("请按任意键结束!");
            Console.ReadLine();
            //List<string> list = new List<string>();
            //list.Add("aa");
            //list.Add("bb");
            //list.Add("cc");
            //string[] array = list.ToArray();
            //List<string> list1 = array.ToList();
            //Console.WriteLine(list1.Remove(list[0]));
            //Console.WriteLine(ReferenceEquals(array, list.ToArray()));
            //DateTime data = DateTime.Now;
            //Console.WriteLine($"{data}  {data.AddMilliseconds(1000)}");
            //ProjectEntity[] projects = JsonConvert.DeserializeObject<ProjectEntity[]>(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Config", "AppConfig.json")));
            //FileWatcherAutoPut fileWatcherAutoPut = new FileWatcherAutoPut("account","password","url","path","proName",null);
            //1.sqlite db 数据 主要记录
            //FileSystemWatcher watcher = new FileSystemWatcher();
            //watcher.Path = "F:\\MyProject\\C#\\WebApp\\Platform\\MDT.VirtualSoftPlatform\\MDT.VirtualSoftPlatform.ServiceInstance\\bin\\Release\\net5.0";
            //watcher.NotifyFilter = NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.FileName;
            //watcher.Changed += new FileSystemEventHandler(OnProcess);
            //watcher.Created += new FileSystemEventHandler(OnProcess);
            //watcher.Deleted += new FileSystemEventHandler(OnProcess);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);
            //watcher.Error += new ErrorEventHandler(OnError);
            //watcher.Filter = "*.dll|*.exe";
            //watcher.IncludeSubdirectories = true;
            //watcher.EnableRaisingEvents = true;
            //设置是否监控目录下的所有子目录
            //HttpClient http = new HttpClient();
            //HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "http://172.16.1.17:5002/api/user/login");
            //const string Boundary = "EAD567A8E8524B2FAC2E0628ABB6DF6E";
            //var requestContent = new MultipartFormDataContent(Boundary);

            //httpRequest.Content = requestContent;

            //requestContent.Headers.Remove("Content-Type");
            //requestContent.Headers.TryAddWithoutValidation("Content-Type", $"multipart/form-data; boundary={Boundary}");
            //var fileContent = File.ReadAllBytesAsync(@"C:\Users\11920\Desktop\WebIDT.xlsx").Result;
            //var fileContent1 = File.ReadAllBytesAsync(@"C:\Users\11920\Desktop\iptable.sql").Result;
            //var byteArrayContent = new ByteArrayContent(fileContent);

            //var byteArrayContent1 = new ByteArrayContent(fileContent1);


            // byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
            //requestContent.Add(new StringContent("123456"), "Account");
            //requestContent.Add(new StringContent("123456"), "UserPassword");
            //HttpResponseMessage result = http.SendAsync(httpRequest).Result;
            //Stream stream = result.Content.ReadAsStream();
            //StreamReader sr = new StreamReader(stream);
            //string msg = sr.ReadToEnd();

        }
       
    }
}

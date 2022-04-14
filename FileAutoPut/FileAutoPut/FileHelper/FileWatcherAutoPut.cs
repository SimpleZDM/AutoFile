using FileAutoPut.Entity;
using FileAutoPut.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 FileAutoPut.FileHelper
 * 接口名称 FileWatcherAutoPut
 * 开发人员：-nhy
 * 创建时间：2022/4/12 17:06:49
 * 描述说明：监控文件并且自动提交
 * 更改历史：
 * 
 * *******************************************************/
namespace FileAutoPut.FileHelper
{
    public class FileWatcherAutoPut
    {
        #region
        private string _account;
        private string _password;

        private List<ApiEntity> _apis;

        /// <summary>
        /// 服务器ip端口
        /// </summary>
        private string _url;
        /// <summary>
        /// 远程接口地址
        /// </summary>

        private string _projectName;
        private ModuleEntity _module;

        private List<FileEntity> files;

        private static readonly ReaderWriterLockSlim Lock = new ReaderWriterLockSlim();

        private FileSystemWatcher watcher;

        private HttpHelper httpHelper;
        private DateTime expressDate;
        private string token;
        #endregion


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <param name="porjectName"></param>
        /// <param name="module"></param>
        public FileWatcherAutoPut(string account, string password,string url,List<ApiEntity> apis,
            string porjectName, ModuleEntity module)
        {
            _account = account;
            _password = password;
            _url = url;
            _apis = apis;
            _projectName = porjectName;
            _module = module;
            files = new List<FileEntity>();
            watcher = new FileSystemWatcher();
            httpHelper = new HttpHelper();
            SetFileWatcher();
            Work();
            expressDate = DateTime.Now;
        }
        public void SetFileWatcher()
        {
            try
            {
                Console.WriteLine("Start SetFileWatcher");
                if (watcher == null)
                    watcher = new FileSystemWatcher();

                watcher.Path =_module.WatchPath;
                watcher.NotifyFilter = NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.FileName;
                //watcher.Changed += new FileSystemEventHandler(OnProcess);
                watcher.Created += new FileSystemEventHandler(OnProcess);
                //watcher.Deleted += new FileSystemEventHandler(OnProcess);
                //watcher.Renamed += new RenamedEventHandler(OnRenamed);
                watcher.Error += new ErrorEventHandler(OnError);
                //watcher.Filter = "*.dll|*.exe"; 
                watcher.IncludeSubdirectories = _module.IncludeSubdirectories;
                watcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                watcher.Dispose();
                watcher = null;
                if (string.IsNullOrEmpty(_module.WatchPath)|| Directory.Exists(_module.WatchPath)==false)
                {
                    Console.WriteLine("监控的文件名称为空，或者路径不存在!");
                    return;
                }
                 
                SetFileWatcher();
                Console.WriteLine($"SetFileWatcher======{ex.Message}!");
            }
            finally
            {
               
            }

        }

        private void OnProcess(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                OnCreated(source, e);

            }
            else if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                OnChanged(source, e);

            }
            else if (e.ChangeType == WatcherChangeTypes.Deleted)
            {
                OnDeleted(source, e);

            }
            else
            {
                OnOther(source, e);
            }
        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            AddFile(e.Name, e.FullPath);
            Console.WriteLine($"新建文件名称:{e.Name}");

        }

        private  void OnChanged(object source, FileSystemEventArgs e)
        {
            //Console.WriteLine($"改变:{e.FullPath},文件名称:{e.Name}");
        }

        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            //Console.WriteLine($"删除:{e.FullPath},文件名称:{e.Name}");
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {

            //Console.WriteLine($"重命名{e.FullPath},文件名称:{e.Name}");
        }
        private void OnOther(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("其他");
        }
        /// <summary>
        /// 遇到错误一般情况是将监控的目录也删除了
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnError(object source, ErrorEventArgs e)
        {
            Exception exception = e.GetException();
            watcher.Dispose();
            watcher = null;
            SetFileWatcher();
            Console.WriteLine($"错误" + e.GetException().Message);

        }

        private void AddFile(string fileName, string filePath)
        {
            try
            {
                if (files.Where(f => f.FileName.Equals(fileName) && f.FilePath.Equals(filePath)).Any() == false)
                { 
                    Lock.EnterWriteLock();
                    files.Add(new FileEntity(filePath, fileName));
                    Lock.ExitWriteLock();
                    expressDate=DateTime.Now.AddMilliseconds(1500);
                }
            }
            catch(Exception ex)
            {
                Lock.ExitWriteLock();
                Console.WriteLine($"addfile====={ex.Message}!");
            }
            finally
            {
               
            }
        }
        private void Work()
        {
            Task.Run(async () =>
            {
                
                while (true)
                {
                    try
                    {
                        Console.WriteLine(expressDate.ToString());
                        if (files != null && files.Count > 0 && expressDate<=DateTime.Now)
                        {
                            
                            if (string.IsNullOrEmpty(token))
                            {
                                var data = await httpHelper.Login(_account, _password,_url,_apis[0]);
                                if (data!=null&&data.code == 200)
                                {
                                    token = data.data;
                                }
                                else
                                {
                                    Console.WriteLine($"{data.msg}_获取token失败请检查账号密码!");
                                }
                            }
                            var arrfile = files.ToArray();
                            var msg = await httpHelper.UpLoadFile(arrfile,_url,_apis[1], token,_projectName,_module.Name);
                            Console.WriteLine("提交完成");
                            if (msg!=null && msg.code == 200)
                            {
                                Console.WriteLine("清理已提交垃圾"); 
                                Lock.EnterWriteLock();
                                foreach (var item in arrfile.ToList())
                                {
                                    files.Remove(item);
                                }
                                Lock.ExitWriteLock();
                                Console.WriteLine("本次提交已完成清理");
                            }
                            else
                            {
                                if (msg==null ||msg.msg.Contains("你的账号已在其它地方登录,如有需要请及时修改密码"))
                                {
                                    token=string.Empty;
                                }
                                Console.WriteLine($"{msg.msg},文件提交失败!请检查接口地址,或者检查token是否过期!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Lock.ExitWriteLock();
                        Console.WriteLine($"work======={ex.Message}");
                    }
                    finally
                    {
                       await Task.Delay(1000 * 2); 
                    }
                    #region 测试代码
                    //try
                    //{
                    //    StringBuilder sb = new StringBuilder();
                    //    sb.Append("print---start\n");
                    //    foreach (var item in files)
                    //    {
                    //        sb.Append($"文件名称:{item.FileName};\n");
                    //    }
                    //    sb.Append("print---end\n");
                    //    Console.WriteLine(sb.ToString());
                    //    Console.WriteLine("start clear files");
                    //    Lock.EnterWriteLock();
                    //    files.Clear();
                    //    Lock.ExitWriteLock();
                    //    Console.WriteLine("end clear files");
                    //}
                    //catch (Exception ex)
                    //{

                    //}
                    //finally
                    //{

                    //} await Task.Delay(1000 * 5);
                    #endregion
                }
            });
            Console.WriteLine("e-work");
        }
    }
}

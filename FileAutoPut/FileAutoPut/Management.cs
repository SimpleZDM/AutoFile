using FileAutoPut.Entity;
using FileAutoPut.FileHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 FileAutoPut
 * 接口名称 Management
 * 开发人员：-nhy
 * 创建时间：2022/4/12 17:01:30
 * 描述说明：自动检测目录文件变化然后自动提交
 * 更改历史：
 * 
 * *******************************************************/
namespace FileAutoPut
{
    public class Management
    {
        /// <summary>
        /// 项目
        /// </summary>
        public List<ProjectEntity> projects { get; set; }
        public List<FileWatcherAutoPut> fileWatcherAutoPuts { get; set; }
        public Management()
        {
            string confPath = Path.Combine(AppContext.BaseDirectory, "Config", "AppConfig.json");

            if (File.Exists(confPath) == false)
            {
                throw new FileNotFoundException("配置文件不存在");
            }
            projects = JsonConvert.DeserializeObject<List<ProjectEntity>>(File.ReadAllText(confPath));
            fileWatcherAutoPuts = new List<FileWatcherAutoPut>();
            foreach (var project in projects)
            {
                Console.WriteLine($"项目名称:{project.ProjectName}");
                if (project.Apis != null && project.Apis.Count>= 2)
                {
                    foreach (var module in project.Modules)
                    {
                        Console.WriteLine($"模块名称:{module.Name}");
                        Console.WriteLine($"已经开始监控目录生成的文件:{module.WatchPath}");
                        fileWatcherAutoPuts.Add(new FileWatcherAutoPut(project.Account, project.Password, project.URLPath,project.Apis,project.ProjectName, module));
                    }
                }
                else
                {
                    Console.WriteLine("请配置需要提交的远程接口!");
                }

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 FileAutoPut.Entity
 * 接口名称 ModuleEntity
 * 开发人员：-nhy
 * 创建时间：2022/4/12 16:51:51
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace FileAutoPut.Entity
{
    public class ModuleEntity
    {
        /// <summary>
        /// 自定义模块存放位置
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否自动提交
        /// </summary>
        public bool IsAutoPut { get; set; }
        /// <summary>
        /// 监控的目录
        /// </summary>
        public string WatchPath { get; set; }

        public bool IncludeSubdirectories { get; set; }
    }
}

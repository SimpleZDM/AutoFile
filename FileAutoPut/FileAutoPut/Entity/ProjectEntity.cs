using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 FileAutoPut.Entity
 * 接口名称 ProjectEntity
 * 开发人员：-nhy
 * 创建时间：2022/4/12 16:44:33
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace FileAutoPut.Entity
{
    public class ProjectEntity:BaseEntity<int>
    {
        public ProjectEntity()
        {

        }
        public ProjectEntity(string name,string url,string putpath,string account, string password)
        {
            ProjectName = name;
            URLPath = url;
            Account= account;
            Password = password;
        }
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目地址
        /// </summary>
        public string URLPath { get; set; }
        public List<ModuleEntity> Modules { get; set; }

        public List<ApiEntity> Apis { get; set; }

        /// <summary>
        /// 访问网站的等账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 访问网站的密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 登录成功之后token存放的位置
        /// </summary>
        public string AccessToken { get; set; }


    }
}

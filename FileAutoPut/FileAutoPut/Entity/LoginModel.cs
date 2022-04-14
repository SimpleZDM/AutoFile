using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 FileAutoPut.Entity
 * 接口名称 LoginModel
 * 开发人员：-nhy
 * 创建时间：2022/4/13 13:18:56
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace FileAutoPut.Entity
{
    public class LoginModel
    {
         static LoginModel()
        {

        }
        public string Account { get; set; }

        public string Password { get; set; }
    }
}

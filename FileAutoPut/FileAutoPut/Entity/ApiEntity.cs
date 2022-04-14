using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 FileAutoPut.Entity
 * 接口名称 ApiEntity
 * 开发人员：-nhy
 * 创建时间：2022/4/14 10:39:51
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace FileAutoPut.Entity
{
    public class ApiEntity
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int HttpMethod { get; set; }
    }
}

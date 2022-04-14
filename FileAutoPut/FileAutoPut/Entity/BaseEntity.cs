using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 FileAutoPut.Entity
 * 接口名称 BaseEntity
 * 开发人员：-nhy
 * 创建时间：2022/4/12 16:40:43
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace FileAutoPut.Entity
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}

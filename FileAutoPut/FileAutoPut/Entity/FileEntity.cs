using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 FileAutoPut.Entity
 * 接口名称 FileEntity
 * 开发人员：-nhy
 * 创建时间：2022/4/13 10:26:20
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace FileAutoPut.Entity
{
    public class FileEntity
    {
        public FileEntity()
        {

        }
        public FileEntity(string filePath, string fileName)
        {
            FilePath = filePath;
            FileName = fileName;
        }


        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}

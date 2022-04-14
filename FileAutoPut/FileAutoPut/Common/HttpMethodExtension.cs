using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 FileAutoPut
 * 接口名称 Menu
 * 开发人员：-nhy
 * 创建时间：2022/4/14 10:36:39
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace FileAutoPut.Common
{
    public class HttpMethodExtension
    {
        public enum HttpMethodType
        {
            GET=1,
            POST=2,
            PUT=3,
            DELETE=4,
        }

        public static HttpMethod GetHttpMethod(int type)
        {

            switch (type)
            {
                case 1:
                    return HttpMethod.Get;
                case 2:
                    return HttpMethod.Post;
                case 3:
                    return HttpMethod.Put;
                case 4:
                    return HttpMethod.Delete;
                default:
                    return HttpMethod.Get;
            }
        }

        public static HttpMethod GetHttpMethod(HttpMethodType type)
        {
            switch (type)
            {
                case HttpMethodType.GET:
                    return HttpMethod.Get;
                case HttpMethodType.POST:
                    return HttpMethod.Post;
                case HttpMethodType.PUT:
                   return HttpMethod.Put;
                case HttpMethodType.DELETE:
                    return HttpMethod.Delete;
                default:
                    return HttpMethod.Get;
            }
        }
    }
}

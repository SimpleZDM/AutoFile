using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 FileAutoPut.Entity
 * 接口名称 HttpMessageModel
 * 开发人员：-nhy
 * 创建时间：2022/4/13 13:25:15
 * 描述说明：返回消息的结构
 * 更改历史：
 * 
 * *******************************************************/
namespace FileAutoPut.Entity
{
    public class HttpMessageModel<T>
    {
        //     "data": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjM3IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiMCIsIlJvbGVOYW1lIjoiQWRtaW4iLCJDb21wYW55SUQiOiIxMyIsIlBsYWNlVHlwZUlEIjoiNDQiLCJQbGFjZVR5cGVOYW1lIjoi5biCIiwiUm9sZUlEIjoiMSIsIkN1cnJlbnRBcmVhSUQiOiIwIiwiZXhwIjoxNjQ5ODgxMjk3LCJpc3MiOiLpooHlj5HogIUiLCJhdWQiOiLmjqXmlLbogIUifQ.2k0wfSSgD6EqE5HqxLBEzw0i4mFy2BeDNcDQVcr2xgs",
        //"code": 200,
        //"status": true,
        //"msg": "登录成功!"

        public string msg { get; set; }
        public bool status { get; set; }
        public int code { get; set; }

        public T data { get; set; }
    }
}

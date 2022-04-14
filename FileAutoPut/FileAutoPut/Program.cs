using FileAutoPut.Entity;
using FileAutoPut.FileHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

namespace FileAutoPut
{
    internal class Program
    {
        #region
        // 关闭64位（文件系统）的操作转向
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);
        // 开启64位（文件系统）的操作转向
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        private bool disabled = false;
        private IntPtr oldWOW64State = new IntPtr();

        /// <summary>
        /// 禁止64位系统转向（true关闭转向，false开启）
        /// </summary>
        public bool DisableWow64
        {
            get
            {
                return disabled;
            }
            set
            {
                if (disabled == value)
                {
                    return;
                }

                disabled = value;


                if (disabled)
                {
                    Wow64RevertWow64FsRedirection(oldWOW64State);
                }
                else
                {
                    Wow64RevertWow64FsRedirection(oldWOW64State);
                }

            }
        }
        #endregion
        static void Main(string[] args)
        {
            Management management = new Management();
            Console.WriteLine("请按任意键结束!");
            Console.ReadLine();
        }
       
    }
}

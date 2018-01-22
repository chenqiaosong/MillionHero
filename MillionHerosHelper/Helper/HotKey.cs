using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MillionHerosHelper
{
    static class HotKey
    {
        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="hWnd">句柄</param>
        /// <param name="id">热键ID</param>
        /// <param name="fsModifiers">特殊键</param>
        /// <param name="vk">键代码</param>
        /// <returns>非0为成功</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,
            int id,
            KeyModifiers fsModifiers,
            Keys vk
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,
            int id
            );


        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }
    }

}

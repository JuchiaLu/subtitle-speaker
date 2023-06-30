using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SubtitleSpeaker
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //获取所有和当前进程同名的进程
            Process current = Process.GetCurrentProcess();
            Process[] processArr = Process.GetProcessesByName(current.ProcessName);

            foreach (Process process in processArr)
            {
                if (process.Id != current.Id && process.MainModule.FileName == current.MainModule.FileName)
                {
                    UnhideProcess(process);

                    //ShowWindowAsync(process.MainWindowHandle, 1); //这对隐藏的窗口无效
                    SetForegroundWindow(process.MainWindowHandle);

                    return;
               }
            }

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        private static void UnhideProcess(Process process)
        {
            IntPtr handle = IntPtr.Zero;
            int prcsId = 0;
            do
            {
                handle = FindWindowEx(IntPtr.Zero, handle, null, "SubtitleSpeaker");

                GetWindowThreadProcessId(handle, out prcsId);

                if (process.Id == prcsId)
                {
                    ShowWindowAsync(handle, 1);
                    return;
                }
            } while (handle != IntPtr.Zero);
        }

        [DllImport("User32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string strClassName, string strWindowName);

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int ProcessId);

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

    }
}

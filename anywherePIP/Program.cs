using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anywherePIP
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            Application.EnableVisualStyles();
            new Form1();
            // Window.WindowService.GetWindows().ForEach(x => x.ReleaseForground());
            Application.Run();
            Application.ApplicationExit += new EventHandler((object sender, EventArgs e) =>
            {
                Window.WindowService.GetWindows().ForEach(x => x.ReleaseForground());
            });
        }
    }

}

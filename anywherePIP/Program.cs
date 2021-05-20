﻿using System;
using System.Windows.Forms;

namespace anywherePIP
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            
            new MainForm();
            Application.Run();
            Application.ApplicationExit += new EventHandler((object sender, EventArgs e) =>
           {
               Window.WindowService.GetWindows().ForEach(x => x.ReleaseForground());
           });
        }
    }
}

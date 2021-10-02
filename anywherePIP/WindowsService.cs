using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace anywherePIP
{
    class WindowsService
    {
        public const string User32DLL = "user32.dll";
        public const string StartupRegistryKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        public const string StartupRegistryValue = "anywherePIP";
        public static string ExePath => Application.ExecutablePath;
    }
}

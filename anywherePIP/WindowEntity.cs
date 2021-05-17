using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anywherePIP
{
    namespace Window
    {
        public class WindowEntity
        {
            private IntPtr hwnd;
            private int styleRaw;
            private int exStyleRaw;
            private string title;

            private List<WindowStyles> styles;
            private List<WindowStylesEx> exStyles;

            public string Title => title;

            public WindowEntity(IntPtr hwnd, int style, int exStyle, string title)
            {
                this.hwnd = hwnd;
                this.styleRaw = style;
                this.exStyleRaw = exStyle;
                this.title = title;

                this.styles = BitService.GetFlags<WindowStyles>(styleRaw);
                this.exStyles = BitService.GetFlags<WindowStylesEx>(exStyleRaw);
            }

            public void FixForground()
            {
                Window.WindowService.SetWindowPos(hwnd, Window.WindowService.HWND_TOPMOST, 0, 0, 0, 0, SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE);
            }

            public void ReleaseForground()
            {
                Window.WindowService.SetWindowPos(hwnd, Window.WindowService.HWND_NOTOPMOST, 0, 0, 0, 0, SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE);
            }

            public bool IsInTaskBar()
            {
                if ((styleRaw & (int)WindowStyles.WS_VISIBLE) <= 0)
                {
                    return false;
                }

                if ((exStyleRaw & (int)WindowStylesEx.WS_EX_TOOLWINDOW) != 0)
                {
                    return false;
                }

                if ((exStyleRaw & (int)WindowStylesEx.WS_EX_NOREDIRECTIONBITMAP) != 0)
                {
                    return false;
                }

                return true;
            }

            public bool IsTopMost()
            {
                return exStyles.Any(x => x == WindowStylesEx.WS_EX_TOPMOST);
            }

            public void DisplayToConsole()
            {
                Console.WriteLine($"window title: {title}");
                Console.WriteLine("Display Style");
                foreach (var style in styles)
                {
                    Console.WriteLine(style.ToString());
                }
                Console.WriteLine("Display StyleEx");
                foreach (var value in exStyles)
                {
                    Console.WriteLine(value.ToString());
                }
                Console.WriteLine("");
            }
        }

    }
}

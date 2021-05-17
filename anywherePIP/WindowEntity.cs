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

            public WindowEntity(IntPtr hwnd, int style, int exStyle, string title)
            {
                this.hwnd = hwnd;
                this.styleRaw = style;
                this.exStyleRaw = exStyle;
                this.title = title;

                this.styles = BitService.GetFlags<WindowStyles>(styleRaw);
                this.exStyles = BitService.GetFlags<WindowStylesEx>(exStyleRaw);
            }

            public bool IsInTaskBar()
            {
            
                if(styles.All(x => ((int)x & (int)WindowStyles.WS_VISIBLE) <= 0))
                {
                    return false;
                }

                if (exStyles.Any(x => ((int)x & (int)WindowStylesEx.WS_EX_TOOLWINDOW) != 0))
                {
                    return false;
                }

                if (exStyles.Any(x => ((int)x & (int)WindowStylesEx.WS_EX_NOREDIRECTIONBITMAP) != 0))
                {
                    return false;
                }

                return true;
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

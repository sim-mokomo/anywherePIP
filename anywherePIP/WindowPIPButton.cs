using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace anywherePIP
{
    class WindowPIPButton
    {
        private Button formButton;
        public Button FormButton => formButton;

        public WindowPIPButton(string title,int width, EventHandler onClick)
        {
            formButton = new Button();
            formButton.Text = title;
            formButton.Width = width;
            formButton.Click += onClick;
        }

        public void IsTopMost(bool isTopMost)
        {
            formButton.BackColor = isTopMost ? Color.Red : Color.White;
        }

        public void SetWidth(int width)
        {
            formButton.Width = width;
        }
    }
}

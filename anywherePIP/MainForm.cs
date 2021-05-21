using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace anywherePIP
{
    public partial class MainForm : Form
    {
        private List<WindowPIPButton> windowPIPButtons = new List<WindowPIPButton>();
        private List<Window.WindowEntity> windowEntities = new List<Window.WindowEntity>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!this.Visible)
            {
                this.ShowDialog();
            }

            Activate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshView();
            flowLayoutPanel1.Resize += FlowLayoutPanel1_Resize;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var senderButton = sender as Button;
            int buttonIndex = windowPIPButtons.FindIndex(x => x.FormButton == senderButton);
            if (buttonIndex < 0)
            {
                return;
            }
            var entity = windowEntities[buttonIndex];
            if (entity.IsTopMost())
            {
                entity.ReleaseForground();
            }
            else
            {
                entity.FixForground();
            }

            RefreshView();
        }

        private void FlowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            windowPIPButtons.ForEach(x => x.SetWidth(ClientRectangle.Width));
        }

        public void RefreshView()
        {
            flowLayoutPanel1.Controls.Clear();
            windowPIPButtons.Clear();
            windowEntities.Clear();

            windowEntities = Window.WindowService
                .GetWindows()
                .Where(x => x.IsInTaskBar())
                .ToList();
            foreach (var window in windowEntities.Select((x, index) => new { item = x, i = index }))
            {
                var button = new WindowPIPButton(window.item.Title, ClientRectangle.Width, Button_Click);
                button.IsTopMost(window.item.IsTopMost());
                windowPIPButtons.Add(button);
                flowLayoutPanel1.Controls.Add(button.FormButton);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach(var entity in windowEntities)
            {
                entity.ReleaseForground();
            }
            RefreshView();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void Exit()
        {
            foreach(var entity in windowEntities)
            {
                entity.ReleaseForground();
            }
            Application.Exit();
        }
    }
}

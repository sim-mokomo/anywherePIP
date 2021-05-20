using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace anywherePIP
{
    public partial class Form1 : Form
    {
        private List<Button> buttons = new List<Button>();
        private List<Window.WindowEntity> windowEntities = new List<Window.WindowEntity>();
        public Form1()
        {
            InitializeComponent();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                this.Visible = false;
            }
            else
            {
                this.ShowDialog();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshView();
            flowLayoutPanel1.Resize += FlowLayoutPanel1_Resize;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button senderButton = sender as Button;
            int buttonIndex = buttons.FindIndex(x => x == senderButton);
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
            foreach (var button in buttons)
            {
                button.Width = ClientRectangle.Width;
            }
        }

        public void RefreshView()
        {
            flowLayoutPanel1.Controls.Clear();
            buttons.Clear();
            windowEntities.Clear();

            windowEntities = Window.WindowService
                .GetWindows()
                .Where(x => x.IsInTaskBar())
                .ToList();
            foreach (var window in windowEntities.Select((x, index) => new { item = x, i = index }))
            {
                Button button = new Button();
                button.Tag = window.i;
                button.Text = window.item.Title;
                button.Width = ClientRectangle.Width;
                button.Click += Button_Click;
                button.BackColor = window.item.IsTopMost() ? Color.Red : Color.White;
                buttons.Add(button);
                flowLayoutPanel1.Controls.Add(button);
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
            Application.Exit();
        }
    }
}

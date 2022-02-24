using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeAreDevs_API;

namespace Synister_Xploit
{
    public partial class Synister : Form
    {
        ExploitAPI api = new ExploitAPI();
        private Point lastPoint;

        public Synister()
        {
            InitializeComponent();
        }

        private void Execute_Click(object sender, EventArgs e)
        {
            if (!api.isAPIAttached())
            {
                MessageBox.Show("Exploit is not Injected");
            }
            else
            {
                string ScriptText = ScriptBox.Text;
                api.SendLuaScript(ScriptText);
            }
        }

        private void Inject_Click(object sender, EventArgs e)
        {
            if (!api.isAPIAttached())
            {
                api.LaunchExploit();
            }
            else
            {
                MessageBox.Show("Exploit is already Injected");
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            ScriptBox.Clear();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Synister_Load(object sender, EventArgs e)
        {

        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(ScriptBox.Text);
                }
            }
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openFileDialog1.Title = "Open";
                ScriptBox.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
    }
}

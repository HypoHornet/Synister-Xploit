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
using KrnlAPI;

namespace Synister_Xploit
{
    public partial class Synister : Form
    {
        KrnlApi krnlApi = new KrnlApi();
        private Point lastPoint;

        public Synister()
        {
            InitializeComponent();
        }

        private void Execute_Click(object sender, EventArgs e)
        {
            if (!krnlApi.IsInjected())
            {
                MessageBox.Show("Exploit is not Injected");
            }
            else
            {
                string ScriptText = ScriptBox.Text;
                krnlApi.Execute(ScriptBox.Text);
            }
        }

        private void Inject_Click(object sender, EventArgs e)
        {
            if (!krnlApi.IsInjected())
            {
                krnlApi.Inject();
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
            krnlApi.Initialize();
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

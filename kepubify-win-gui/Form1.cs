using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace kepubify_win_gui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string filelocation;
        string savefolder;
        private void button1_Click(object sender, EventArgs e)
        {
            filelocation = textBox2.Text;
            button1.Enabled = false;
            runexec(filelocation);
        }
        private void runexec(string args)
        {
            string filename;
            if (Environment.Is64BitOperatingSystem == true)
            {
                filename = "kepubify-windows-64bit.exe";
            }
            else
            {
                filename = "kepubify-windows-32bit.exe";
            }
            if (checkBox1.Checked == true)
            {
                args = "-o \"" + savefolder + "\" " + "\"" + args + "\"";
            }
            else
            {
                args = "\"" + args + "\"";
            }
            if (checkBox2.Checked == true)
            {
                args = "-u " + args;
            }
            if (checkBox3.Checked == true)
            {
                args = "-i " + args;
            }
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = filename;
            startInfo.Arguments = args;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;


            Process processTemp = new Process();
            processTemp.StartInfo = startInfo;
            processTemp.EnableRaisingEvents = true;
            processTemp.Start();
            string output;
            while((output = processTemp.StandardOutput.ReadLine()) != null)
            {
                textBox1.AppendText(output + Environment.NewLine);
            }
            string err = processTemp.StandardError.ReadToEnd();
            if (err != "")
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            processTemp.WaitForExit();
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
                filelocation = textBox2.Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox3.Text = folderBrowserDialog1.SelectedPath;
                savefolder = textBox3.Text;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox3.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
                button3.Enabled = false;
            }
        }
    }
}

using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
namespace Cleanup_PC
{
    ///<summary>
    ///-----------------------------------------------
    ///                github --> TNAlotaibi
    ///-----------------------------------------------
    ///This tool for cleaning useless files,
    ///and uses a large amount of memory is deleted,
    ///and also helps to speed up the PC,
    /// Note --> Do not clean the PC 100%
    /// </summary>
    public partial class Form1 : Form
    {
        bool boolean = false;
        int X = 0;
        int Y = 0;
        Random random = new Random();
        int cleaned_int = 0;
        float bytes = 0.0f;
        string[] paths =
        { 
           /* path 1 ---> */ string.Format(@"C:\Users\{0}\AppData\Roaming\Microsoft\Windows\Recent", Environment.UserName),
           /* path 2 ---> */  @"C:\Windows\Temp"
        };
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            new Thread(method).Start();
            button1.Text = "Started ..";

        }
        void method()
        {
            foreach (string path in paths)
            {
                calcSize(path);
                folders_cleaner(path);
                files_cleaner(path);
            }
            string gg = converttomb(bytes).ToString();
            label2.Text = converttomb(bytes).ToString().Split('.')[0] + " MB";
            label1.Text = cleaned_int.ToString() + " Files";
            MessageBox.Show("----------------------Info------------------------" + Environment.NewLine+ Environment.NewLine + "Finished task .. " +cleaned_int.ToString()+" Files has been Deleted! " + Environment.NewLine + "Deleted useless files .. Size " + gg.Split('.')[0] + " MB" + Environment.NewLine  +Environment.NewLine + "--------------------------------------------------", "CleanUp My PC" ,MessageBoxButtons.OK , MessageBoxIcon.Information);
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        void calcSize(string folder_name)
        {
            foreach (string name in Directory.GetFiles(folder_name))
            {
                try
                {
                    FileInfo info = new FileInfo(name);
                    bytes += info.Length;
                }
                catch (Exception)
                {

                }
            }
        }
        void files_cleaner(string path)
        {
                foreach (string name in Directory.GetFiles(path))
                {
                    try
                    {

                    FileInfo info = new FileInfo(name);
                    bytes += info.Length;
                    File.Delete(name);
                    richTextBox1.AppendText("Deleted --> " + name.Split('\\').Last() + Environment.NewLine);
                    cleaned_int++;
                    label2.Text = converttomb(bytes).ToString().Split('.')[0] + " MB";
                    label1.Text = cleaned_int.ToString() + " Files";
                }
                catch (Exception)
                    {
                    }
                Thread.Sleep(random.Next(10,200));

            }
        }
        void folders_cleaner(string path)
        {
            foreach (string name in Directory.GetDirectories(path))
                {
                try
                    {
      
                    calcSize(name);
                    Directory.Delete(name);
                    richTextBox1.AppendText("Deleted --> " + name.Split('\\').Last() + Environment.NewLine);
                    cleaned_int++;
                    label2.Text = converttomb(bytes).ToString().Split('.')[0] + " MB";
                    label1.Text = cleaned_int.ToString() + " Files";
                }
                catch (Exception)
                    {

                    }
                Thread.Sleep(random.Next(50, 350));

            }
        }
        double converttomb(double megabytes) 
        {
            return megabytes / (1024.0*1024.0);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("chrome", "https://github.com/TNAlotaibi");
        }

   
        private void label4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void lblName_MouseDown(object sender, MouseEventArgs e)
        {
            boolean = true;
            X = e.X;
            Y = e.Y;
        }

        private void lblName_MouseMove(object sender, MouseEventArgs e)
        {
            if (boolean)
                SetDesktopLocation(MousePosition.X - X, MousePosition.Y - Y);
        }

        private void lblName_MouseUp(object sender, MouseEventArgs e)
        {
            boolean = false;
        }
    }
}

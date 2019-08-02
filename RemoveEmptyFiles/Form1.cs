using RemoveEmptyFiles.BL;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoveEmptyFiles
{
    public partial class Form1 : Form
    {
        FileInfo[] fAr;
        RemoveEmptyFilesClass removeEmptyFilesClass;
        public Form1()
        {
            InitializeComponent();
            removeEmptyFilesClass = new RemoveEmptyFilesClass();
            this.MaximizeBox = false;
            this.Text = "Remove empty files";
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            button1.Text = "Choose directory";
            button1.Font = textBox1.Font = button2.Font = new Font("Arial", 15);
            button2.Text = "Remove";
            button2.Enabled = false;

            textBox1.ReadOnly = true;
            textBox1.Multiline = true;
            textBox1.ScrollBars = ScrollBars.Both;

            button1.Click += SelectDirectoryClick;
            button2.Click += RemoveFilesClick;


        }
        private async void RemoveFilesClick(object sender, EventArgs e)
        {
            button2.Enabled = false;
            textBox1.Text = "Please wait...";
            int count = await removeEmptyFilesClass?.RemoveFilesAsync(fAr);
            textBox1.Text = count.ToString() + " files deleted";
        }

        private async void SelectDirectoryClick(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    textBox1.Text = fbd.SelectedPath.ToString();
                    await Task.Run(() =>
                    {
                        DirectoryInfo di = new DirectoryInfo(fbd.SelectedPath);
                        fAr = null;
                        fAr = di.GetFiles()
                                           .Where(x => x.Length == 0).ToArray();
                    });
                }
                else
                {
                    textBox1.Text = "Error" + fbd.SelectedPath;
                }
            }
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(await removeEmptyFilesClass?.AppendFindFilesAsync(fAr));
            textBox1.SelectionStart = 0;
            textBox1.ScrollToCaret();
            if (fAr.Length > 0)
            {
                button2.Enabled = true;
            }
        }
    }
}

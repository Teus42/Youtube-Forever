using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubeForever
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            string folderName = "ffmpeg";

            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

        }

        private void Splash_Load(object sender, EventArgs e)
        {
            this.Width = (int)(this.BackgroundImage.Width   / 1.45f);
            this.Height = (int)(this.BackgroundImage.Height / 1.45f);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            this.Close();
        }
    }
}

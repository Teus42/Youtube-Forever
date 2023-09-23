using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using static System.Windows.Forms.LinkLabel;

namespace YoutubeForever
{
    public partial class Form1 : Form
    {
        string link = "";
        string format = "";
        string linkYT = "";
        Image img_thumb;
        bool GetInfoComplete = false;
        IDataObject iData;
        Baixar vd;
        DownloadFFMPEG d_ffmpeg;
        private static NotifyIcon notifyIcon;

        public Form1()
        {
            InitializeComponent();
            //Inicializando classe Baixar
            vd = new Baixar();

            d_ffmpeg = new DownloadFFMPEG();

            string[] tortuga =
            {
                    "Ajude as Tartaruginhas" ,
                    "Tu manda muito bem esguicho" ,
                    "Continue a nadar" ,
                    "Não utilize canudos de plástico" ,
                    "Todas as espécies de tartarugas marinhas que ocorrem no Brasil continuam ameaçadas de extinção"
            };

            DateTime dt = DateTime.Today;

            if (dt.Day == 16 && dt.Month == 6)
            {
                lbl_tortuga.Text = "Dia Mundial da Tartaruga Marinha";
            }
            else
            {
                Random randow = new();
                int rnd = randow.Next(0, tortuga.Length);
                lbl_tortuga.Text = tortuga[rnd];
            }
        }     
        private async void button1_Click(object sender, EventArgs e)
        {
            txt_log.Text = "";
            vd.video_status = "";

            //Iniciando checagem do FFMPEG e download se necessario            
            if (!File.Exists(Path.Combine("ffmpeg", "ffmpeg.exe")))
            {
                txt_log.Text = "Baixando FFMPEG, essa operação só será realizada uma vez\r\n";
                await d_ffmpeg.CheckFFMPEG();
            }
                   

            if (textBox1.Text == "")
            {
                MessageBox.Show("Link n�o pode ser vazio", "Aten��o");
                return;
            }
            else{
               
                link = textBox1.Text;
            }

            /*
             * O nome do format não é o formato em si, apenas a nomeclatura que usei para distinguir.
             * Todos os vídeos saem com o formato .mp4
            */
            if(rdb_mp4.Checked){
                format = ".mp4"; //720p ou menor
            }
            
            if (rdb_mp4Best.Checked)
            {
                format = ".best"; //Melhor qualidade possivel do v�deo
            }

            if (rdb_mp3.Checked)
            {
                format = ".mp3"; //Apenas o MP3
            }

            if (rdb_1080.Checked)
            {
                format = ".fullhd"; //1080p, 1080p60, 1080p HRD
            }

            if (rdb_2k.Checked)
            {
                format = ".2k"; //1440p, 1440p60, 1440p HRD
            }

            if (rdb_4k.Checked)
            {
                format = ".4k"; //2160p, 2160p60, 2160p HRD
            }
            
            
            txt_log.Text += "Processo de download iniciado, por favor aguarde...\r\n";

            //Chamando o download 
            await vd.VideoDown(link,format,vd.get_video_name);

            txt_log.Text += vd.video_status; //Retorna a variavel da calsse Baixar com o status do download                       
        }    
        private async void tmr_ctrlc_Tick(object sender, EventArgs e)
        {
            //Pegando dados do Ctrl + C
            iData = Clipboard.GetDataObject();          
            linkYT = (String)iData.GetData(DataFormats.Text);

            if (linkYT != null)
            {
                //Verificando se os dados contem o link do YouTube
                if (linkYT.Contains("https://www.youtube.com/") || linkYT.Contains("https://youtu.be"))
                {
                    textBox1.Text = linkYT;
                    
                    if (!GetInfoComplete)
                    {
                        await vd.GetInfoVideo(linkYT);
                        txt_log.Text = vd.video_info;
                        GetThumbnail(linkYT);

                        Activate();
                        GetInfoComplete = true;
                        //Ap�s puxar os dados o GetInfoComplete informa se deve parar de puxar,
                        //isso evite um looping do timer puxando sem parar os dados.
                    }

                }
            }            
        }

        //Limpando as coisas
        private void btn_Clean_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            txt_log.Text = "";            
            Clipboard.SetDataObject("");
            pcb_thumb.Image = null;
        }

        //Caso o link mude limpo o log
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            txt_log.Text = "";
            GetInfoComplete = false;
        }

        //Tartaruginhas 
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://www.tamar.org.br/";
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        private void GetThumbnail(string linkYT) 
        {
            string thumb_id = "";

            if (linkYT.Contains("https://www.youtube.com/"))
            {
                thumb_id = linkYT.Split("=")[1];

            }
            else if (linkYT.Contains("https://youtu.be"))
            {

                thumb_id = linkYT.Split("e/")[1];
            }
            else 
            {
                return;
            }
            
            using (WebClient client = new WebClient())
            {
                byte[] imageData;

                try
                {
                    imageData = client.DownloadData($"https://img.youtube.com/vi/{thumb_id}/maxresdefault.jpg");
                }
                catch 
                {
                    try
                    {
                        imageData = client.DownloadData($"https://img.youtube.com/vi/{thumb_id}/hqdefault.jpg");
                    }
                    catch
                    {
                        try
                        {
                            imageData = client.DownloadData($"https://img.youtube.com/vi/{thumb_id}/mqdefault.jpg");
                        }
                        catch 
                        {
                            try
                            {
                                imageData = client.DownloadData($"https://img.youtube.com/vi/{thumb_id}/sddefault.jpg");
                            }
                            catch 
                            {
                                return;
                            }  
                        }
                    }
                }
               
                img_thumb = Image.FromStream(new MemoryStream(imageData));
                
                pcb_thumb.Image = img_thumb;
            }
        }

        private void SaveImg()
        {
            string pastaDownloads = KnownFolders.GetPath(KnownFolder.Downloads);
            string video_name = vd.get_video_name;
            if (video_name != null){

                video_name = Regex.Replace(video_name, "[^\\w\\s]", "");
                string downloadThumb = Path.Combine(pastaDownloads, video_name + ".jpeg");
                img_thumb.Save(downloadThumb, ImageFormat.Jpeg);
                txt_log.Text += "Thumbnail baixada em "+downloadThumb+"\r\n";
            }
                        
        }

        private void pcb_thumb_Click(object sender, EventArgs e)
        {
            SaveImg();            
        }
    }
}
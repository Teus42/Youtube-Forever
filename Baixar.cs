using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YoutubeForever
{
    internal class Baixar
    {
        public string? get_video_name {get; set;}
        public string? video_info {get;set;}
        public string? video_status {get;set;}

        public async Task GetInfoVideo(string link)
        {
            var youtube = new YoutubeClient();

            string videoUrl = link;

            //Coletando os dados do vídeo
            var video = await youtube.Videos.GetAsync(videoUrl);
            get_video_name = video.Title;
            video_info = "-------------------------- VIDEO INFO --------------------------\r\n";
            video_info += "Name: " + video.Title + "\r\n";
            video_info += "Channel: " + video.Author.ChannelTitle+"\r\n";
            video_info += "Duration: " + video.Duration.ToString() + "\r\n";
            video_info += "Upload Date: " + video.UploadDate.ToString() + "\r\n";
            //video_info += "Description: " + video.Description + "\r\n";

            
        }

        //Gerando id único para tratar internamente o vídeo e suportar multiplos downloads 
        //Assim com a task de download é assincrona ela vai fazer tudo com base no id recebido   
        public static string GenID(string keyword)
        {
            //Pegando o timestamp atual
            long timestamp = DateTimeOffset.Now.Ticks;

            //Pegando um número aleatório 
            Random random = new();              
            string numberRandom = random.Next(100000, 999999).ToString();

            //Combinando tudo, titulo do vídeo + timestamp + número aleatório 
            string combinedString = keyword + timestamp.ToString() + numberRandom;
            
            //Pegando os bytes da string e utilizando os mesmos para criar um SHA256
            byte[] bytes = Encoding.UTF8.GetBytes(combinedString);
            byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(bytes);
            
            //Convertendo hash do SHA256 em string, removendo os traços e pegando apenas os primeiros digitos
            string id = BitConverter.ToString(hashBytes).Replace("-", string.Empty).Substring(0, 8);

            //Retornando id
            return id;
        }
        public async Task VideoDown(string link, string format,string video_name)
        {   
            //Iniciando o YoutubeClient do YoutubeExplode
            var youtube = new YoutubeClient();

            //Armazenando link na variavel interna
            string videoUrl = link;

            //Coletando os dados do vídeo
            var video = await youtube.Videos.GetAsync(videoUrl);           

            //Coletando o Stream Manifest do vídeo
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
            
            //Remove qualquer caractere especial que não deixe salvar o arquivo
            video_name = Regex.Replace(video_name, "[^\\w\\s]", "");

            //Pegando pasta padrão de downloads
            string pastaDownloads = KnownFolders.GetPath(KnownFolder.Downloads);

            //Solicitando id unico
            string id = GenID(video_name);

            //Modos acima do 720p baixam separadamente audio e video
            //desta forma é necessario especificar que o video e som
            //são do mesmo id para mais para frente concatenar
            string _video = "video-" + id;
            string _som = "som-" + id;
            
            string vidPath = Path.Combine(pastaDownloads, _video + ".tmp");
            string audioPath = Path.Combine(pastaDownloads, _som + ".tmp");

            IStreamInfo audioStreamInfo;
            IVideoStreamInfo videoStreamInfo;

            if (format == ".mp3")
            {
                //MP3

                string videoPath = Path.Combine(pastaDownloads, video_name + ".mp3");

                //Pegando apenas a faixa de áudio 
                audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

                if (audioStreamInfo != null)
                {                    
                    //Passando a Stream Info para pegar o áudio e inserir na videoPath
                    await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, videoPath);
                    video_status += "Download Completo: " + videoPath + "\r\n";
                }
                else
                {                    
                    MessageBox.Show("Nenhuma qualidade de áudio encontrada.");
                }
                return;
            }
            else if (format == ".mp4")
            {
                //HD or minimal resolution (480p, 360p, ...)
                string videoPath = Path.Combine(pastaDownloads, video_name + ".mp4");

                //Coletando vídeo e áudio juntos (limitado a até 720p)
                videoStreamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
                
                if (videoStreamInfo != null)
                {                    
                    await youtube.Videos.Streams.DownloadAsync(videoStreamInfo, videoPath);
                    video_status += "Download Completo: " + videoPath + "\r\n";
                }
                else
                {
                    MessageBox.Show("Nenhuma qualidade de vídeo encontrada.");
                }

                return;
                
            }
            else if(format == ".fullhd")
            {
                //Full HD

                //Verificação do tipo de vídeo, priorizando o HDR
                try
                {
                    try
                    {
                        //Pegando apenas o vídeo sem áudio
                        videoStreamInfo = streamManifest.GetVideoOnlyStreams().Where(s => s.Container == Container.Mp4).First(s => s.VideoQuality.Label == "1080p HDR");
                    }
                    catch
                    {
                        try
                        {
                            videoStreamInfo = streamManifest.GetVideoOnlyStreams().Where(s => s.Container == Container.Mp4).First(s => s.VideoQuality.Label.Contains("1080p"));

                        }
                        catch
                        {
                            videoStreamInfo = streamManifest.GetVideoOnlyStreams().Where(s => s.Container == Container.WebM).First(s => s.VideoQuality.Label.Contains("1080p"));

                        }
                    }

                    //Pegando apenas o áudio
                    audioStreamInfo = streamManifest.GetAudioStreams().Where(s => s.Container == Container.Mp4).GetWithHighestBitrate();

                    //Baixando ambos na pasta download, o vídeo como video.mp4 e o áudio como som.mp3
                    await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, audioPath);
                    await youtube.Videos.Streams.DownloadAsync(videoStreamInfo, vidPath);

                    //Após os IF do format continua para concatenar ambos
                }
                catch
                {
                    MessageBox.Show("Qualidade inexistente neste vídeo", "ERROR");
                    return;
                }

            }
            else if (format == ".2k")
            {
                //2K                
                try
                {
                    try
                    {
                        videoStreamInfo = streamManifest.GetVideoOnlyStreams().Where(s => s.Container == Container.Mp4).First(s => s.VideoQuality.Label == "1440p HDR");
                    }
                    catch{
                        try
                        {
                            videoStreamInfo = streamManifest.GetVideoOnlyStreams().Where(s => s.Container == Container.Mp4).First(s => s.VideoQuality.Label.Contains("1440p"));

                        }
                        catch
                        {
                            videoStreamInfo = streamManifest.GetVideoOnlyStreams().Where(s => s.Container == Container.WebM).First(s => s.VideoQuality.Label.Contains("1440p"));

                        }
                    }

                    audioStreamInfo = streamManifest.GetAudioStreams().Where(s => s.Container == Container.Mp4).GetWithHighestBitrate();

                    await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, audioPath);
                    await youtube.Videos.Streams.DownloadAsync(videoStreamInfo, vidPath);
                }
                catch
                {
                    MessageBox.Show("Qualidade inexistente neste vídeo", "ERROR");
                    return;
                }

            }
            else if (format == ".4k")
            {
                //4K
                try
                {
                    try
                    {
                        videoStreamInfo = streamManifest.GetVideoOnlyStreams().Where(s => s.Container == Container.Mp4).First(s => s.VideoQuality.Label == "2160p HDR");
                    }
                    catch
                    {
                        try
                        {
                            videoStreamInfo = streamManifest.GetVideoOnlyStreams().Where(s => s.Container == Container.Mp4).First(s => s.VideoQuality.Label.Contains("2160p"));

                        }
                        catch
                        {
                            videoStreamInfo = streamManifest.GetVideoOnlyStreams().Where(s => s.Container == Container.WebM).First(s => s.VideoQuality.Label.Contains("2160p"));

                        }
                    }
                    
                    audioStreamInfo = streamManifest.GetAudioStreams().Where(s => s.Container == Container.Mp4).GetWithHighestBitrate();

                    await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, audioPath);
                    await youtube.Videos.Streams.DownloadAsync(videoStreamInfo, vidPath);
                }
                catch{
                    MessageBox.Show("Qualidade inexistente neste vídeo", "ERROR");
                    return;
                }
            }
            else if (format == ".best")
            {
                //Best Quality
                
                try
                {
                    //Buscando a melhor qualidade do vídeo, seja ela qual for
                    videoStreamInfo = streamManifest.GetVideoOnlyStreams().Where(s => s.Container == Container.Mp4).GetWithHighestVideoQuality();
                    audioStreamInfo = streamManifest.GetAudioStreams().Where(s => s.Container == Container.Mp4).GetWithHighestBitrate();

                    await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, audioPath);
                    await youtube.Videos.Streams.DownloadAsync(videoStreamInfo, vidPath);
                }
                catch
                {
                    MessageBox.Show("Qualidade inexistente neste vídeo", "ERROR");
                    return;
                }
            }

            //Informando o diretorio do app para poder acionar o ffmpeg
            string diretorioBase = AppDomain.CurrentDomain.BaseDirectory;
            string caminhoFFmpeg = Path.Combine(diretorioBase, "ffmpeg", "ffmpeg.exe");

            string _final = "final-"+id;
            string final = Path.Combine(pastaDownloads, _final+".mp4");
            string rename = Path.Combine(pastaDownloads, video_name + ".mp4");           

            //Argumento do ffmpeg para concatenar vídeo com áudio
            string args = " -i "+ _video + ".tmp -i "+ _som +".tmp -map 0:v -map 1:a -c:v copy -c:a copy "+ _final +".mp4 -y";

            //Iniciando processo de concatenação
            try
            {
                ProcessStartInfo startInfo = new()
                {
                    CreateNoWindow = true,
                    FileName = caminhoFFmpeg,
                    WorkingDirectory = pastaDownloads,
                    Arguments = args
                };
                Process exeProcess = Process.Start(startInfo);
                exeProcess.WaitForExit();
            }
            catch{
                MessageBox.Show("Não foi possivel realizar a concatenação", "ERROR");
            }
            //Finalizando concatenação           
            
            //Após a concatenação é gerado um vídeo chamado final.mp4,
            //após isso realizo a exclusão do video.mp4 e som.mp3
            
            
            // Excluindo o arquivo de vídeo 
            if (File.Exists(vidPath))
            {
                File.Delete(vidPath);
            }

            // Excluindo o arquivo de áudio
            if (File.Exists(audioPath))
            {
                File.Delete(audioPath);
            }

            //Renomeia o final.mp4 para o nome do vídeo
            //Caso já exista o mesmo arquivo com o nome do vídeo
            //ele será apagado

            // Excluindo o arquivo caso já exista
            
            if (File.Exists(rename))
            {
                File.Delete(rename);
            }

            //Renomeando 
            if (File.Exists(final))
            {
                File.Move(final, rename);
            }

            //Finalizado processo
            video_status += "Download Completo: " + rename + "\r\n";
            
        }
    }
    
    //Código para pegar as pastas, valeu Stack Overflow 
    public enum KnownFolder
    {
        Contacts,
        Downloads,
        Favorites,
        Links,
        SavedGames,
        SavedSearches
    }

    public static class KnownFolders
    {
        private static readonly Dictionary<KnownFolder, Guid> _guids = new()
        {
            [KnownFolder.Contacts] = new("56784854-C6CB-462B-8169-88E350ACB882"),
            [KnownFolder.Downloads] = new("374DE290-123F-4565-9164-39C4925E467B"),
            [KnownFolder.Favorites] = new("1777F761-68AD-4D8A-87BD-30B759FA33DD"),
            [KnownFolder.Links] = new("BFB9D5E0-C6A9-404C-B2B2-AE6DB6AF4968"),
            [KnownFolder.SavedGames] = new("4C5C32FF-BB9D-43B0-B5B4-2D72E54EAAA4"),
            [KnownFolder.SavedSearches] = new("7D1D3A04-DEBB-4115-95CF-2F29DA2920DA")
        };

        public static string GetPath(KnownFolder knownFolder)
        {
            return SHGetKnownFolderPath(_guids[knownFolder], 0);
        }

        [DllImport("shell32", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
        private static extern string SHGetKnownFolderPath(
            [MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags,
            nint hToken = 0);
    } 
}

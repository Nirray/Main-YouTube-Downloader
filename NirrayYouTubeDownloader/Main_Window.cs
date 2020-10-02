using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
//using VideoLibrary;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Drawing;
using YoutubeExplode;
using System.Collections.Generic;
using System.Diagnostics;
using YoutubeExplode.Videos.Streams;
using Xabe.FFmpeg;
using System.Linq;
namespace NirrayYouTubeDownloader
{
    public partial class NirrayYouTubeParser : Form
    {
        public NirrayYouTubeParser()
        {
            InitializeComponent();
        }
        public bool downloadComplete = false;
        public static class English
        {
            public static string success_mp3 = "MP3 file created successfully: ";
            public static string wait_button = "Pause";
        }
        public static class Polish
        {
            public static string success_mp3 = "Pomyślnie utworzono plik MP3: ";
            public static string wait_button = "Wstrzymaj";
            public static string checking = "Sprawdzanie: ";
            public static string bgtask = "Aktualnie w tle: ";
            public static string fileexist = " > Plik już istnieje.";
            public static string filebroken = " > Wygląda na to, że plik jest uszkodzony.";
            public static string mbsize = "Przewidywany rozmiar: ";
            public static string stage = "Stan: ";
            public static string succes = "Pomyślnie: ";
            public static string failed = "Niepomyślnie: ";
            public static string missingno = "Nie można rozpocząć pobierania ponieważ brakuje potrzebnych plików aplikacji.";
            public static string needadmin = "Nie można zapisać pliku. Uruchom program jako administrator.";
            public static string errorocc = "Wystąpił błąd przy próbie pozyskania danych bezprośrednio w aplikacji.";
            public static string notavail = "Ten film nie jest dostępny w Twoim kraju.";
            public static string cannotdownload = " nie można pozyskać danych.";
        }
        public static class Variables
        {
            public static int left = 0;
            public static int cannot_download = 0;
            public static int can_download = 0;
            public static int status = 0;
            public static long total_size_download = 0;
            public static long total_size_all = 0;
            public static int max_thread = 0;
            public static string current_title = "";
            public static string current_exten = "";
            public static string appPath = Application.StartupPath;
            public static string total = "1";
        }
        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }
            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }
        private void NewWayToConvertMp3(string filename, string extension)
        {
            Variables.status = 3;
            DownloadFromDirectLink.Enabled = false;
            DownloadListButton.Enabled = false;
            plikToolStripMenuItem.Enabled = false;
            Action onCompleted = () =>
            {
                Information_container.AppendText("Pomyślnie utworzono plik MP3: " + filename + ".mp3" + Environment.NewLine);
                if (PauseButton.Text == "Wstrzymaj")
                {
                    Variables.status = 0;
                }
                else
                {
                    Variables.status = 3;
                }
                
                if (Variables.status == 3)
                {
                    downloadComplete = false;
                }
                else
                {
                    downloadComplete = true;
                }
                DownloadFromDirectLink.Enabled = true;
                DownloadListButton.Enabled = true;
                plikToolStripMenuItem.Enabled = true;
            };

            var thread = new Thread(
              async () =>
              {
                  try
                  {
                      FFmpeg.SetExecutablesPath(Application.StartupPath + "\\lib\\", "ffmpeg", "ffprobe");
                      string filePath = Path.Combine(Variables.appPath, "download", extension, filename + "." + extension);
                      string folderpath = Path.Combine(Variables.appPath, "download", extension);
                      if (!Directory.Exists(folderpath))
                      {
                          Directory.CreateDirectory(folderpath);
                      }
                      string outp = Path.Combine(Variables.appPath, "download", "mp3", filename + ".mp3");
                      IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(filePath);
                      IStream audioStream = mediaInfo.AudioStreams.FirstOrDefault()
                          ?.SetCodec(AudioCodec.mp3)
                          ?.SetBitrate(192000);

                      if (File.Exists(outp))
                      {
                          //Information_container.AppendText("Plik: " + filename + ".mp3 już istnieje" + Environment.NewLine);
                      }
                      else
                      {
                          await FFmpeg.Conversions.New()
                              .AddStream(audioStream)
                              .SetOutput(outp)
                              .Start();
                      }
                  }
                  finally
                  {
                      onCompleted();
                  }
              });
            thread.Start();
        }

        private void CreateMainDirectory(string directory)
        {
            string webm = directory + "\\download\\webm\\";
            string mp3 = directory + "\\download\\mp3\\";
            if (!Directory.Exists(webm))
            {
                Directory.CreateDirectory(webm);
            }
            if (!Directory.Exists(mp3))
            {
                Directory.CreateDirectory(mp3);
            }
        }

        private async Task StartDownloadAsync(string url)
        {
            try
            {
                while (!downloadComplete)
                {
                    Application.DoEvents();
                }
                while (Variables.status == 3)
                {
                    Application.DoEvents();
                }
                plikToolStripMenuItem.Enabled = false;
                CreateMainDirectory(Variables.appPath);
                CookieWebClient client = new CookieWebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Client_DownloadFileCompleted);
                DownloadFromDirectLink.Enabled = false;
                DownloadListButton.Enabled = false;
                var youtube = new YoutubeClient();
                var video = await youtube.Videos.GetAsync(url);
                var video_id = video.Id;
                string title = video.Title;
                client.QueryString.Add("title", title);
                string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
                title = r.Replace(title, "");
                Variables.current_title = title;
                Progress_text.Text = "Sprawdzanie: " + title;
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video_id);
                var streamInfo = streamManifest.GetAudioOnly().WithHighestBitrate();
                Variables.max_thread += 1;
                backgroundthreads.Text = "Aktualnie w tle: " + Variables.max_thread.ToString();
                DateTime localDate = DateTime.Now;
                if (streamInfo != null)
                {
                    Variables.current_exten = streamInfo.Container.ToString();
                    string path = Path.Combine(Variables.appPath, "download\\" + streamInfo.Container + "\\" + title + "." + streamInfo.Container);
                    if (ToolBox_DoNotDownloadAlready.Checked == true && File.Exists(path))
                    {
                        var length = new FileInfo(path).Length;
                        Information_container.AppendText("[" + localDate + "]" + " > " + title + " > Plik już istnieje." + Environment.NewLine);
                        if (length != streamInfo.Size.TotalBytes)
                        {
                            Information_container.AppendText("[" + localDate + "]" + " > " + title + " > Wygląda na to, że plik jest uszkodzony." + Environment.NewLine);
                            Variables.total_size_download += streamInfo.Size.TotalBytes;
                            whatsize.Text = "Przewidywany rozmiar: " + SizeSuffix(Variables.total_size_download).ToString();
                            client.DownloadFileAsync(new Uri(streamInfo.Url), path);
                        }
                        else
                        {
                            DownloadFromDirectLink.Enabled = true;
                            DownloadListButton.Enabled = true;
                            plikToolStripMenuItem.Enabled = true;
                            Variables.left += 1;
                            Variables.can_download += 1;
                            Variables.max_thread -= 1;
                            backgroundthreads.Text = "Aktualnie w tle: " + Variables.max_thread.ToString();
                            counter.Text = "Stan: " + Variables.left.ToString() + "/" + Variables.total;
                            Can_download_text.Text = "Pomyślnie: " + Variables.can_download.ToString() + "/" + Variables.total;
                            Cannot_download_text.Text = "Niepomyślnie: " + Variables.cannot_download.ToString() + "/" + Variables.total;
                            return;
                        }
                    }
                    else
                    {
                        Variables.total_size_download += streamInfo.Size.TotalBytes;
                        whatsize.Text = "Przewidywany rozmiar: " + SizeSuffix(Variables.total_size_download).ToString();
                        client.DownloadFileAsync(new Uri(streamInfo.Url), path);
                    }
                }             
                Information_container.AppendText("[" + localDate + "]" + " > " + title + Environment.NewLine);
                if (speedDownloadToolStripMenuItem.Checked == true)
                {
                    downloadComplete = true;
                }
                else
                {
                    downloadComplete = false;
                }
                
                
            }
            catch (FileNotFoundException)
            {
                Progress_text.Text = "Nie można rozpocząć pobierania ponieważ brakuje potrzebnych plików aplikacji.";
            }
            catch (InvalidOperationException)
            {
                Progress_text.Text = "Nie można zapisać pliku. Uruchom program jako administrator.";
                DownloadFromDirectLink.Enabled = true;
                DownloadListButton.Enabled = true;
                plikToolStripMenuItem.Enabled = true;
                Variables.left += 1;
                Variables.cannot_download += 1;
                counter.Text = "Stan: " + Variables.left.ToString() + "/" + Variables.total;
                Can_download_text.Text = "Pomyślnie: " + Variables.can_download.ToString() + "/" + Variables.total;
                Cannot_download_text.Text = "Niepomyślnie: " + Variables.cannot_download.ToString() + "/" + Variables.total;
            }
            catch (System.Reflection.TargetInvocationException)
            {
                Progress_text.Text = "Wystąpił błąd przy próbie pozyskania danych bezprośrednio w aplikacji.";
                DownloadFromDirectLink.Enabled = true;
                DownloadListButton.Enabled = true;
                plikToolStripMenuItem.Enabled = true;
                Variables.left += 1;
                Variables.cannot_download += 1;
                counter.Text = "Stan: " + Variables.left.ToString() + "/" + Variables.total;
                Can_download_text.Text = "Pomyślnie: " + Variables.can_download.ToString() + "/" + Variables.total;
                Cannot_download_text.Text = "Niepomyślnie: " + Variables.cannot_download.ToString() + "/" + Variables.total;
            }
            catch (YoutubeExplode.Exceptions.VideoUnplayableException)
            {
                DateTime localDate = DateTime.Now;
                Information_container.Select(Information_container.TextLength, 0);
                Information_container.SelectionColor = Color.Red;
                Information_container.AppendText("[" + localDate + "]" + " > " + Variables.current_title + "> " + "Ten film nie jest dostępny w Twoim kraju." + Environment.NewLine);
                Information_container.SelectionColor = Color.Black;
                Progress_text.Text = "Ten film nie jest dostępny w Twoim kraju.";
                DownloadFromDirectLink.Enabled = true;
                DownloadListButton.Enabled = true;
                plikToolStripMenuItem.Enabled = true;
                Variables.left += 1;
                Variables.cannot_download += 1;
                counter.Text = "Stan: " + Variables.left.ToString() + "/" + Variables.total;
                Can_download_text.Text = "Pomyślnie: " + Variables.can_download.ToString() + "/" + Variables.total;
                Cannot_download_text.Text = "Niepomyślnie: " + Variables.cannot_download.ToString() + "/" + Variables.total;
            }
            catch (YoutubeExplode.Exceptions.RequestLimitExceededException)
            {
                DateTime localDate = DateTime.Now;
                Information_container.Select(Information_container.TextLength, 0);
                Information_container.SelectionColor = Color.Red;
                Information_container.AppendText("[" + localDate + "]" + " > " + Variables.current_title + "> " + "Nie można pobrać tego pliku z tego adresu IP." + Environment.NewLine);
                Information_container.SelectionColor = Color.Black;
                Progress_text.Text = "Nie można pobrać tego pliku z tego adresu IP.";
                DownloadFromDirectLink.Enabled = true;
                DownloadListButton.Enabled = true;
                plikToolStripMenuItem.Enabled = true;
                Variables.left += 1;
                Variables.cannot_download += 1;
                counter.Text = "Stan: " + Variables.left.ToString() + "/" + Variables.total;
                Can_download_text.Text = "Pomyślnie: " + Variables.can_download.ToString() + "/" + Variables.total;
                Cannot_download_text.Text = "Niepomyślnie: " + Variables.cannot_download.ToString() + "/" + Variables.total;
            }
            catch (Exception e)
            {
                DateTime localDate = DateTime.Now;
                Information_container.Select(Information_container.TextLength, 0);
                Information_container.SelectionColor = Color.Red;
                Information_container.AppendText("[" + localDate + "]" + " > " + Variables.current_title + " nie można pozyskać danych." + Environment.NewLine);
                Information_container.SelectionColor = Color.Black;
                Information_container.AppendText(e.ToString());
                Progress_text.Text = "Wystąpił błąd przy próbie pozyskania zawartości bezpośrednio z YouTube.";
                DownloadFromDirectLink.Enabled = true;
                DownloadListButton.Enabled = true;
                plikToolStripMenuItem.Enabled = true;
                Variables.left += 1;
                Variables.cannot_download += 1;
                counter.Text = "Stan: " + Variables.left.ToString() + "/" + Variables.total;
                Can_download_text.Text = "Pomyślnie: " + Variables.can_download.ToString() + "/" + Variables.total;
                Cannot_download_text.Text = "Niepomyślnie: " + Variables.cannot_download.ToString() + "/" + Variables.total;
            }

        }
        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            string title = ((WebClient)(sender)).QueryString["title"];
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            title = r.Replace(title, "");
            this.BeginInvoke((MethodInvoker)delegate {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                Progress_text.Text = "(" + title + ") Pobrano: " + e.BytesReceived + " / " + e.TotalBytesToReceive + ".";
                progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
                DownloadFromDirectLink.Enabled = false;
                DownloadListButton.Enabled = false;
                plikToolStripMenuItem.Enabled = false;
            });
        }
        void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string title = ((System.Net.WebClient)(sender)).QueryString["title"];
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            title = r.Replace(title, "");
            DateTime localDate = DateTime.Now;
            this.BeginInvoke((MethodInvoker)delegate {
                Variables.max_thread -= 1;
                backgroundthreads.Text = "Aktualnie w tle: " + Variables.max_thread.ToString();
                Progress_text.Text = title + " > pobieranie zakończone.";
                Information_container.Select(Information_container.TextLength, 0);
                Information_container.SelectionColor = Color.Green;
                Information_container.AppendText("[" + localDate + "]" + " > " + title + " > pomyślnie zapisano plik." + Environment.NewLine);
                Information_container.SelectionColor = Color.Black;
                progressBar1.Value = 100;
            });
            if (ToolBox_ConvertToMp3.Checked == true)
            {
                string latest_ext = Variables.current_exten;
                Information_container.AppendText("[" + localDate + "]" + " > " + title + " > konwertowanie do MP3." + Environment.NewLine);
                NewWayToConvertMp3(title, latest_ext);
                DownloadFromDirectLink.Enabled = false;
                DownloadListButton.Enabled = false;
                plikToolStripMenuItem.Enabled = false;
            }
            else
            {
                DownloadFromDirectLink.Enabled = true;
                DownloadListButton.Enabled = true;
                plikToolStripMenuItem.Enabled = true;
            }
            Variables.left += 1;
            Variables.can_download += 1;
            if (PauseButton.Text == "Kontynuuj")
            {
                downloadComplete = false;
            }
            else
            {
                downloadComplete = true;
            }
            counter.Text = "Stan: " + Variables.left.ToString() + "/" + Variables.total;
            Can_download_text.Text = "Pomyślnie: " + Variables.can_download.ToString() + "/" + Variables.total;
            Cannot_download_text.Text = "Niepomyślnie: " + Variables.cannot_download.ToString() + "/" + Variables.total;
            
        }
        private async void CreateBigList(string[] lista_linkow_z_playlistami)
        {
            var total = 0;

            List<string> result = new List<string>();
            foreach (var playlist_link in lista_linkow_z_playlistami)
            {
                var youtube = new YoutubeClient();
                var playlist_metadata = await youtube.Playlists.GetAsync(playlist_link);
                var playlist = await youtube.Playlists.GetAsync(playlist_metadata.Id);
                var title = playlist.Title;
                string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
                title = r.Replace(title, "");
                var playlistVideos = await youtube.Playlists.GetVideosAsync(playlist.Id);
                
                foreach (var url in playlistVideos)
                {
                    if (countPlaylistDiscSpaceToolStripMenuItem.Checked == true)
                    {
                        try
                        {
                            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(url.Id);
                            var streamInfo = streamManifest.GetAudioOnly().WithHighestBitrate();
                            if (streamInfo != null)
                            {
                                Variables.total_size_all += streamInfo.Size.TotalBytes;
                                maxsizeof.Text = "Rozmiar: " + SizeSuffix(Variables.total_size_all).ToString();
                            }
                        }
                        catch (YoutubeExplode.Exceptions.VideoUnplayableException)
                        {
                            //
                        }
                        catch (Exception)
                        {

                        }
                    }
                    Information_container.AppendText("[" + url.Title + "]" + " > Playlista." + Environment.NewLine);
                    Information_container.AppendText("[" + url.Url + "]" + " > Playlista." + Environment.NewLine);
                    result.Add(url.Url);
                    total += 1;
                }
                
                Information_container.AppendText("Nowa playlista!" + Environment.NewLine);
            }
            downloadComplete = true;
            string[] new_playlist = result.ToArray();
            Information_container.AppendText("Rozmiar połączonej playlisty: " + total.ToString() + Environment.NewLine);
            Variables.total = total.ToString();
            counter.Text = "Stan: " + Variables.left.ToString() + "/" + Variables.total;
            foreach (var url in new_playlist)
            {
                await Task.Run(() => StartDownloadAsync(url));
                counter.Text = "Stan: " + Variables.left.ToString() + "/" + Variables.total;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog dialog = SelectListToDownloadFrom;
            string playlist_path = "";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                plikToolStripMenuItem.Enabled = false;
                playlist_path = SelectListToDownloadFrom.FileName;
                Variables.left = 0;
                Variables.can_download = 0;
                Variables.cannot_download = 0;
                Variables.total = "";
                Variables.current_title = "";
            }
            else
            {
                return;
            }
            string[] lines = File.ReadAllLines(playlist_path);
            Variables.total = lines.Length.ToString();
            DownloadFromDirectLink.Enabled = false;
            DownloadListButton.Enabled = false;
            plikToolStripMenuItem.Enabled = false;
            counter.Text = "Stan: " + Variables.left.ToString() + "/" + Variables.total;
            Can_download_text.Text = "Pomyślnie: " + Variables.can_download.ToString() + "/" + Variables.total;
            Cannot_download_text.Text = "Niepomyślnie: " + Variables.cannot_download.ToString() + "/" + Variables.total;
            RunDownloadForEachURL(lines);
        }
        private async void RunDownloadForEachURL(string[] toDownload)
        {
            foreach (var url in toDownload)
            {
                await Task.Run(() => StartDownloadAsync(url));
            }
        }

        private async void RunDownloadForPlaylist(string playlist_url)
        {
            var youtube = new YoutubeClient();
            var playlist_metadata = await youtube.Playlists.GetAsync(playlist_url);
            var playlist = await youtube.Playlists.GetAsync(playlist_metadata.Id);
            var title = playlist.Title;
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            title = r.Replace(title, "");
            var playlistVideos = await youtube.Playlists.GetVideosAsync(playlist.Id);
            List<string> temp_playlist = new List<string>();
            
            foreach (var url in playlistVideos)
            {
                Information_container.AppendText("[" + url.Title + "]" + " > Playlista." + Environment.NewLine);
                Information_container.AppendText("[" + url.Url + "]" + " > Playlista." + Environment.NewLine);
                temp_playlist.Add(url.Url);
            }
            downloadComplete = true;
            string[] new_playlist = temp_playlist.ToArray();
            Variables.total = new_playlist.Length.ToString();
            counter.Text = "Stan: " + Variables.left.ToString() + "/" + Variables.total;
            foreach (var url in new_playlist)
            {
                await Task.Run(() => StartDownloadAsync(url));
                counter.Text = "Stan: " + Variables.left.ToString() + "/" + Variables.total;
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS4014:Await.Warning")]
        private void DownloadFromDirectLink_Click(object sender, EventArgs e)
        {
            DateTime localDate = DateTime.Now;
            if (Checkbox_UseUrl.Checked == true)
            {
                try
                {
                    string url = DirectUrlContainer.Text;
                    url.ToLower();
                    Information_container.AppendText("[" + localDate + "]" + " > Pozyskiwanie zawartości." + Environment.NewLine);
                    if (!url.Contains("youtube"))
                    {
                        Progress_text.Text = "Adres do pobrania nie jest odnośnikiem do serwisu YouTube.";

                    }
                    else if (url.Contains("playlist"))
                    {
                        Variables.total = "0";
                        Variables.left = 0;
                        DownloadFromDirectLink.Enabled = false;
                        DownloadListButton.Enabled = false;
                        plikToolStripMenuItem.Enabled = false;
                        RunDownloadForPlaylist(url);

                    }
                    else
                    {
                        Variables.total = "0";
                        Variables.left = 0;
                        DownloadFromDirectLink.Enabled = false;
                        DownloadListButton.Enabled = false;
                        plikToolStripMenuItem.Enabled = false;
                        downloadComplete = true;
                        StartDownloadAsync(url);
                    }
                }
                catch (System.Reflection.TargetInvocationException)
                {
                }
            }
            if (Checkbox_UseCombine.Checked == true)
            {
                try
                {
                    var playlists = UserPlaylist.Text.Split('\n').ToList();
                    Information_container.AppendText("[" + localDate + "]" + " > Pozyskiwanie zawartości." + Environment.NewLine);
                    string[] new_playlist = playlists.ToArray();
                    CreateBigList(new_playlist);
                }
                catch (System.Reflection.TargetInvocationException)
                {
                }
            }
        }

        private void ToolBox_NewLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = NewSavePath;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Variables.appPath = dialog.SelectedPath;
                Variables.left = 0;
                Variables.can_download = 0;
                Variables.cannot_download = 0;
                Variables.total = "";
                Variables.current_title = "";
            }
            else
            {
                Variables.appPath = Application.StartupPath;
            }
            
        }

        private void DirectUrlContainer_DoubleClick(object sender, EventArgs e)
        {
            DirectUrlContainer.SelectAll();
        }

        private void informacjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About_Window About = new About_Window();
            About.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@Path.Combine(Variables.appPath, "download\\"));
            }
            catch (Win32Exception)
            {
                CreateMainDirectory(Variables.appPath);
                Process.Start(@Path.Combine(Variables.appPath, "download\\"));
            }
        }

        private void NirrayYouTubeParser_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);

        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100 &! Variables.left.Equals(Variables.total) && PauseButton.Text == "Kontynuuj")
            {
                downloadComplete = true;
            }
            if (Variables.status == 0)
            {
                Variables.status = 3;
                PauseButton.Text = "Kontynuuj";
                return;
            }
            else
            {
                Variables.status = 0;
                PauseButton.Text = "Wstrzymaj";
                return;
            }
        }

        private void Checkbox_UseUrl_CheckedChanged(object sender, EventArgs e)
        {
            if (Checkbox_UseCombine.Enabled == false)
            {
                Checkbox_UseCombine.Enabled = true;
                UserPlaylist.Enabled = true;
                return;
            }
            else
            {
                Checkbox_UseCombine.Enabled = false;
                UserPlaylist.Enabled = false;
                return;
            }
        }

        private void Checkbox_UseCombine_CheckedChanged(object sender, EventArgs e)
        {
            if (Checkbox_UseUrl.Enabled == false)
            {
                Checkbox_UseUrl.Enabled = true;
                DirectUrlContainer.Enabled = true;
                return;
            }
            else
            {
                Checkbox_UseUrl.Enabled = false;
                DirectUrlContainer.Enabled = false;
                return;
            }
        }

        private void speedDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void Information_container_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(Information_container.Text);
        }

        private void Information_container_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            Information_container.SelectionStart = Information_container.Text.Length;
            // scroll it automatically
            Information_container.ScrollToCaret();
        }

        private void countPlaylistDiscSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (countPlaylistDiscSpaceToolStripMenuItem.Checked == true)
            {
                maxsizeof.Visible = true;
            }
            else
            {
                maxsizeof.Visible = false;
            }
        }

        private void GitHubError_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Nirray/YouTubeDownloader/issues/new");
        }

        private void turboNoLimitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (turboNoLimitToolStripMenuItem.Checked == true)
            {
                Variables.status = 0;
                PauseButton.Text = "Wstrzymaj";
                speedimage.Visible = true;
                speedtext.Visible = true;
                PauseButton.Enabled = false;
                ToolBox_ConvertToMp3.Checked = false;
                ToolBox_ConvertToMp3.Enabled = false;
            }
            else
            {
                Variables.status = 0;
                PauseButton.Text = "Wstrzymaj";
                speedimage.Visible = false;
                speedtext.Visible = false;
                PauseButton.Enabled = true;
                ToolBox_ConvertToMp3.Checked = true;
                ToolBox_ConvertToMp3.Enabled = true;
            }
        }

        private void twenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
    
}
public class CookieWebClient : WebClient
{
    public CookieContainer CookieContainer { get; private set; }

    /// <summary>
    /// This will instanciate an internal CookieContainer.
    /// </summary>
    public CookieWebClient()
    {
        this.CookieContainer = new CookieContainer();
    }

    /// <summary>
    /// Use this if you want to control the CookieContainer outside this class.
    /// </summary>
    public CookieWebClient(CookieContainer cookieContainer)
    {
        this.CookieContainer = cookieContainer;
    }

    protected override WebRequest GetWebRequest(Uri address)
    {
        var request = base.GetWebRequest(address) as HttpWebRequest;
        if (request == null) return base.GetWebRequest(address);
        request.CookieContainer = CookieContainer;
        return request;
    }
}


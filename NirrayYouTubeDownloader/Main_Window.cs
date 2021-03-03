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
        public static class Variables
        {
            public static int left = 0;
            public static string language = "PL";
            public static int cannot_download = 0;
            public static int can_download = 0;
            public static int status = 0;
            public static long total_size_download = 0;
            public static long total_size_all = 0;
            public static int max_thread = 0;
            public static int no_more_than = 999;
            public static int bitrate = 320000;
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
            pobieranieToolStripMenuItem.Enabled = false;
            plikToolStripMenuItem.Enabled = false;
            void onCompleted()
            {
                Information_container.SelectionColor = Color.Purple;
                if (Variables.language == "PL")
                    Information_container.AppendText("Pomyślnie utworzono plik MP3: " + filename + ".mp3" + Environment.NewLine);
                else
                    Information_container.AppendText("Successfully created MP3 file: " + filename + ".mp3" + Environment.NewLine);
                Information_container.SelectionColor = Color.Black;
                if (PauseButton.Text == "Wstrzymaj" || PauseButton.Text == "Pause")
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
                DownloadListButton.Enabled = false; //true
                pobieranieToolStripMenuItem.Enabled = true;
                plikToolStripMenuItem.Enabled = true;
            }

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
                      if (!File.Exists(filePath))
                      {
                          return;
                      }
                      IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(filePath);
                      IStream audioStream = mediaInfo.AudioStreams.FirstOrDefault()
                          ?.SetCodec(AudioCodec.mp3)
                          ?.SetBitrate(Variables.bitrate);

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
                  catch (InvalidOperationException)
                  {
                      //
                  }
                  finally
                  {
                      try
                      {
                          onCompleted();
                      }
                      catch (InvalidOperationException)
                      {
                          // do nothing
                      }
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
                while (Variables.max_thread >= Variables.no_more_than)
                {
                    Application.DoEvents();
                }
                while (Variables.status == 3)
                {
                    Application.DoEvents();
                }
                pobieranieToolStripMenuItem.Enabled = false;
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
                if (Variables.language == "PL")
                    Progress_text.Text = "Sprawdzanie: " + title;
                else
                    Progress_text.Text = "Checking: " + title;
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video_id);
                var streamInfo = streamManifest.GetAudioOnly().WithHighestBitrate();
                Variables.max_thread += 1;
                if (Variables.language == "PL")
                    backgroundthreads.Text = "Aktualnie w tle: " + Variables.max_thread.ToString();
                else
                    backgroundthreads.Text = "Background download: " + Variables.max_thread.ToString();
                DateTime localDate = DateTime.Now;

                if (streamInfo != null)
                {
                    Variables.current_exten = streamInfo.Container.ToString();
                    string path = Path.Combine(Variables.appPath, "download\\" + streamInfo.Container + "\\" + title + "." + streamInfo.Container);
                    client.QueryString.Add("extension", streamInfo.Container.ToString());
                    client.QueryString.Add("can_convert", "yes");
                    if (ToolBox_DoNotDownloadAlready.Checked == true && File.Exists(path))
                    {
                        var length = new FileInfo(path).Length;
                        if (Variables.language == "PL")
                            Information_container.AppendText("[" + localDate + "]" + " > " + title + " > Plik już istnieje." + Environment.NewLine);
                        else
                            Information_container.AppendText("[" + localDate + "]" + " > " + title + " > File already exists." + Environment.NewLine);
                        if (length != streamInfo.Size.TotalBytes)
                        {
                            if (Variables.language == "PL")
                                Information_container.AppendText("[" + localDate + "]" + " > " + title + " > Wygląda na to, że plik jest uszkodzony." + Environment.NewLine);
                            else
                                Information_container.AppendText("[" + localDate + "]" + " > " + title + " > Corrupted file." + Environment.NewLine);
                            Variables.total_size_download += streamInfo.Size.TotalBytes;
                            whatsize.Text = "" + SizeSuffix(Variables.total_size_download).ToString();
                            client.DownloadFileAsync(new Uri(streamInfo.Url), path);
                        }
                        else
                        {
                            client.QueryString.Add("extension", streamInfo.Container.ToString());
                            DownloadFromDirectLink.Enabled = true;
                            DownloadListButton.Enabled = false; // true
                            pobieranieToolStripMenuItem.Enabled = true;
                            plikToolStripMenuItem.Enabled = true;
                            Variables.left += 1;
                            Variables.can_download += 1;
                            Variables.max_thread -= 1;
                            if (Variables.language == "PL")
                            {
                                backgroundthreads.Text = "Aktualnie w tle: " + Variables.max_thread.ToString();
                                counter.Text = "Aktualny: " + Variables.left.ToString() + "/" + Variables.total;
                                Can_download_text.Text = "Pomyślnie: " + Variables.can_download.ToString() + "/" + Variables.total;
                                Cannot_download_text.Text = "Niepomyślnie: " + Variables.cannot_download.ToString() + "/" + Variables.total;
                            }
                            else
                            {
                                backgroundthreads.Text = "Background download: " + Variables.max_thread.ToString();
                                counter.Text = "Current: " + Variables.left.ToString() + "/" + Variables.total;
                                Can_download_text.Text = "Successful: " + Variables.can_download.ToString() + "/" + Variables.total;
                                Cannot_download_text.Text = "Unsuccessful: " + Variables.cannot_download.ToString() + "/" + Variables.total;
                            }
                            
                            return;
                        }
                    }
                    else
                    {
                        Variables.total_size_download += streamInfo.Size.TotalBytes;
                        whatsize.Text = "" + SizeSuffix(Variables.total_size_download).ToString();
                        client.DownloadFileAsync(new Uri(streamInfo.Url), path);
                    }
                }
                Information_container.AppendText("[" + localDate + "]" + " > " + title + Environment.NewLine);
                if (speedimage.Visible == true)
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
                if (Variables.language == "PL")
                    Progress_text.Text = "Nie można rozpocząć pobierania ponieważ brakuje potrzebnych plików aplikacji.";
                else
                    Progress_text.Text = "Download could not start because necessary application files are missing.";
            }
            catch (InvalidOperationException)
            {
                if (Variables.language == "PL")
                    Progress_text.Text = "Nie można zapisać pliku. Uruchom program jako administrator.";
                else
                    Progress_text.Text = "File could not be saved. Run the program as administrator.";
                DownloadFromDirectLink.Enabled = true;
                DownloadListButton.Enabled = false; // true
                pobieranieToolStripMenuItem.Enabled = true;
                plikToolStripMenuItem.Enabled = true;
                Variables.left += 1;
                Variables.cannot_download += 1;
                if (Variables.language == "PL")
                {
                    counter.Text = "Aktualny: " + Variables.left.ToString() + "/" + Variables.total;
                    Can_download_text.Text = "Pomyślnie: " + Variables.can_download.ToString() + "/" + Variables.total;
                    Cannot_download_text.Text = "Niepomyślnie: " + Variables.cannot_download.ToString() + "/" + Variables.total;
                }
                else
                {
                    counter.Text = "Current: " + Variables.left.ToString() + "/" + Variables.total;
                    Can_download_text.Text = "Successful: " + Variables.can_download.ToString() + "/" + Variables.total;
                    Cannot_download_text.Text = "Unsuccessful: " + Variables.cannot_download.ToString() + "/" + Variables.total;
                }
            }
            catch (System.Reflection.TargetInvocationException)
            {
                if (Variables.language == "PL")
                    Progress_text.Text = "Wystąpił błąd przy próbie pozyskania danych bezprośrednio w aplikacji.";
                else
                    Progress_text.Text = "An error occurred while trying to get the data directly in the app.";
                DownloadFromDirectLink.Enabled = true;
                DownloadListButton.Enabled = false; // true
                pobieranieToolStripMenuItem.Enabled = true;
                plikToolStripMenuItem.Enabled = true;
                Variables.left += 1;
                Variables.cannot_download += 1;
                if (Variables.language == "PL")
                {
                    counter.Text = "Aktualny: " + Variables.left.ToString() + "/" + Variables.total;
                    Can_download_text.Text = "Pomyślnie: " + Variables.can_download.ToString() + "/" + Variables.total;
                    Cannot_download_text.Text = "Niepomyślnie: " + Variables.cannot_download.ToString() + "/" + Variables.total;
                }
                else
                {
                    counter.Text = "Current: " + Variables.left.ToString() + "/" + Variables.total;
                    Can_download_text.Text = "Successful: " + Variables.can_download.ToString() + "/" + Variables.total;
                    Cannot_download_text.Text = "Unsuccessful: " + Variables.cannot_download.ToString() + "/" + Variables.total;
                }
            }
            catch (YoutubeExplode.Exceptions.VideoUnplayableException)
            {
                DateTime localDate = DateTime.Now;
                Information_container.Select(Information_container.TextLength, 0);
                Information_container.SelectionColor = Color.Orange;
                if (Variables.language == "PL")
                    Information_container.AppendText("[" + localDate + "]" + " > " + Variables.current_title + "> " + "Ten film nie jest dostępny w Twoim kraju." + Environment.NewLine);
                else
                    Information_container.AppendText("[" + localDate + "]" + " > " + Variables.current_title + "> " + "This video is not available in your country." + Environment.NewLine);
                Information_container.SelectionColor = Color.Black;
                if (Variables.language == "PL")
                    Progress_text.Text = "Ten film nie jest dostępny w Twoim kraju.";
                else
                    Progress_text.Text = "This video is not available in your country.";

                DownloadFromDirectLink.Enabled = true;
                DownloadListButton.Enabled = false; // true
                pobieranieToolStripMenuItem.Enabled = true;
                plikToolStripMenuItem.Enabled = true;
                Variables.left += 1;
                Variables.cannot_download += 1;
                if (Variables.language == "PL")
                {
                    counter.Text = "Aktualny: " + Variables.left.ToString() + "/" + Variables.total;
                    Can_download_text.Text = "Pomyślnie: " + Variables.can_download.ToString() + "/" + Variables.total;
                    Cannot_download_text.Text = "Niepomyślnie: " + Variables.cannot_download.ToString() + "/" + Variables.total;
                }
                else
                {
                    counter.Text = "Current: " + Variables.left.ToString() + "/" + Variables.total;
                    Can_download_text.Text = "Successful: " + Variables.can_download.ToString() + "/" + Variables.total;
                    Cannot_download_text.Text = "Unsuccessful: " + Variables.cannot_download.ToString() + "/" + Variables.total;
                }
            }
            catch (YoutubeExplode.Exceptions.RequestLimitExceededException)
            {
                DateTime localDate = DateTime.Now;
                Information_container.Select(Information_container.TextLength, 0);
                Information_container.SelectionColor = Color.Red;
                if (Variables.language == "PL")
                    Information_container.AppendText("[" + localDate + "]" + " > " + Variables.current_title + "> " + "Nie można pobrać tego pliku z tego adresu IP." + Environment.NewLine);
                else
                    Information_container.AppendText("[" + localDate + "]" + " > " + Variables.current_title + "> " + "File cannot be retrieved from this IP address." + Environment.NewLine);
                Information_container.SelectionColor = Color.Black;
                if (Variables.language == "PL")
                    Progress_text.Text = "Nie można pobrać tego pliku z tego adresu IP.";
                else
                    Progress_text.Text = "File cannot be retrieved from this IP address.";
                DownloadFromDirectLink.Enabled = true;
                DownloadListButton.Enabled = false; // true
                pobieranieToolStripMenuItem.Enabled = true;
                plikToolStripMenuItem.Enabled = true;
                Variables.left += 1;
                Variables.cannot_download += 1;
                if (Variables.language == "PL")
                {
                    counter.Text = "Aktualny: " + Variables.left.ToString() + "/" + Variables.total;
                    Can_download_text.Text = "Pomyślnie: " + Variables.can_download.ToString() + "/" + Variables.total;
                    Cannot_download_text.Text = "Niepomyślnie: " + Variables.cannot_download.ToString() + "/" + Variables.total;
                }
                else
                {
                    counter.Text = "Current: " + Variables.left.ToString() + "/" + Variables.total;
                    Can_download_text.Text = "Successful: " + Variables.can_download.ToString() + "/" + Variables.total;
                    Cannot_download_text.Text = "Unsuccessful: " + Variables.cannot_download.ToString() + "/" + Variables.total;
                }
            }
            catch (Exception e)
            {
                DateTime localDate = DateTime.Now;
                Information_container.Select(Information_container.TextLength, 0);
                Information_container.SelectionColor = Color.Red;
                if (Variables.language == "PL")
                    Information_container.AppendText("[" + localDate + "]" + " > " + Variables.current_title + " nie można pozyskać danych." + Environment.NewLine);
                else
                    Information_container.AppendText("[" + localDate + "]" + " > " + Variables.current_title + " not available." + Environment.NewLine);
                Information_container.SelectionColor = Color.Black;
                Information_container.AppendText(e.ToString());
                if (Variables.language == "PL")
                    Progress_text.Text = "Wystąpił błąd przy próbie pozyskania zawartości bezpośrednio z YouTube.";
                else
                    Progress_text.Text = "An error occurred while trying to get content directly from YouTube.";
                DownloadFromDirectLink.Enabled = true;
                DownloadListButton.Enabled = false; // true
                pobieranieToolStripMenuItem.Enabled = true;
                plikToolStripMenuItem.Enabled = true;
                Variables.left += 1;
                Variables.cannot_download += 1;
                if (Variables.language == "PL")
                {
                    counter.Text = "Aktualny: " + Variables.left.ToString() + "/" + Variables.total;
                    Can_download_text.Text = "Pomyślnie: " + Variables.can_download.ToString() + "/" + Variables.total;
                    Cannot_download_text.Text = "Niepomyślnie: " + Variables.cannot_download.ToString() + "/" + Variables.total;
                }
                else
                {
                    counter.Text = "Current: " + Variables.left.ToString() + "/" + Variables.total;
                    Can_download_text.Text = "Successful: " + Variables.can_download.ToString() + "/" + Variables.total;
                    Cannot_download_text.Text = "Unsuccessful: " + Variables.cannot_download.ToString() + "/" + Variables.total;
                }
            }

        }
        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            string title = ((WebClient)(sender)).QueryString["title"];
            string convert = ((WebClient)(sender)).QueryString["can_convert"];
            string extension = ((WebClient)(sender)).QueryString["extension"];
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            title = r.Replace(title, "");
            string download_text = "";
            if (Variables.language == "PL")
            {
                download_text = "Pobrano: ";
            }
            else
            {
                download_text = "Downloaded: ";
            }
            this.BeginInvoke((MethodInvoker)delegate {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            Progress_text.Text = "(" + title + ") "+ download_text + e.BytesReceived + " / " + e.TotalBytesToReceive + ".";
            progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            DownloadFromDirectLink.Enabled = false;
            DownloadListButton.Enabled = false;
            pobieranieToolStripMenuItem.Enabled = false;
            plikToolStripMenuItem.Enabled = false;
            });
        }
        void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string title = ((WebClient)(sender)).QueryString["title"];
            string convert = ((WebClient)(sender)).QueryString["can_convert"];
            string extension = ((WebClient)(sender)).QueryString["extension"];
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            title = r.Replace(title, "");
            DateTime localDate = DateTime.Now;
            this.BeginInvoke((MethodInvoker)delegate {
                Variables.max_thread -= 1;
                if (Variables.language == "PL")
                {
                    backgroundthreads.Text = "Aktualnie w tle: " + Variables.max_thread.ToString();
                    Progress_text.Text = title + " > pobieranie zakończone.";
                    Information_container.Select(Information_container.TextLength, 0);
                    Information_container.SelectionColor = Color.DarkGreen;
                    Information_container.AppendText("[" + localDate + "]" + " > " + title + " > pomyślnie zapisano plik." + Environment.NewLine);
                    Information_container.SelectionColor = Color.Black;
                    progressBar1.Value = 100;
                }
                else
                {
                    backgroundthreads.Text = "Background download: " + Variables.max_thread.ToString();
                    Progress_text.Text = title + " > download complete.";
                    Information_container.Select(Information_container.TextLength, 0);
                    Information_container.SelectionColor = Color.DarkGreen;
                    Information_container.AppendText("[" + localDate + "]" + " > " + title + " > successfully saved the file." + Environment.NewLine);
                    Information_container.SelectionColor = Color.Black;
                    progressBar1.Value = 100;
                }
            });
            if (ToolBox_ConvertToMp3.Checked == true && convert.Equals("yes"))
            {
                string latest_ext = Variables.current_exten;
                if (Variables.language == "PL")
                    Information_container.AppendText("[" + localDate + "]" + " > " + title + " > konwertowanie do MP3 w toku." + Environment.NewLine);
                else
                    Information_container.AppendText("[" + localDate + "]" + " > " + title + " > converting to MP3 in progress." + Environment.NewLine);
                NewWayToConvertMp3(title, extension);
                DownloadFromDirectLink.Enabled = false;
                DownloadListButton.Enabled = false;
                plikToolStripMenuItem.Enabled = false;
                pobieranieToolStripMenuItem.Enabled = false;
            }
            else
            {
                DownloadFromDirectLink.Enabled = true;
                DownloadListButton.Enabled = false; //true
                plikToolStripMenuItem.Enabled = true;
                pobieranieToolStripMenuItem.Enabled = true;
            }
            Variables.left += 1;
            Variables.can_download += 1;
            if (PauseButton.Text == "Kontynuuj" || PauseButton.Text == "Continue")
            {
                downloadComplete = false;
            }
            else
            {
                downloadComplete = true;
            }
            if (Variables.language == "PL")
            {
                counter.Text = "Aktualny: " + Variables.left.ToString() + "/" + Variables.total;
                Can_download_text.Text = "Pomyślnie: " + Variables.can_download.ToString() + "/" + Variables.total;
                Cannot_download_text.Text = "Niepomyślnie: " + Variables.cannot_download.ToString() + "/" + Variables.total;
            }
            else
            {
                counter.Text = "Current: " + Variables.left.ToString() + "/" + Variables.total;
                Can_download_text.Text = "Successful: " + Variables.can_download.ToString() + "/" + Variables.total;
                Cannot_download_text.Text = "Unsuccessful: " + Variables.cannot_download.ToString() + "/" + Variables.total;
            }

        }
        private async void CreateBigList(string[] lista_linkow_z_playlistami)
        {
            if (alternativeSquadToolStripMenuItem.Checked == false)
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
                                    if (Variables.language == "PL")
                                        maxsizeof.Text = "Rozmiar: " + SizeSuffix(Variables.total_size_all).ToString();
                                    else
                                        maxsizeof.Text = "Size: " + SizeSuffix(Variables.total_size_all).ToString();
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
                        if (Variables.language == "PL")
                        {
                            Information_container.SelectionColor = Color.DarkGreen;
                            Information_container.AppendText("[" + url.Title + "]" + " > Playlista." + Environment.NewLine);
                            Information_container.SelectionColor = Color.DarkGreen;
                            Information_container.AppendText("[" + url.Url + "]" + " > Playlista." + Environment.NewLine);
                            Information_container.SelectionColor = Color.Black;
                            Information_container.SelectionColor = Color.DarkGreen;
                            Information_container.AppendText("Nowa playlista!" + Environment.NewLine);
                            Information_container.SelectionColor = Color.Black;
                        }
                        else
                        {
                            Information_container.SelectionColor = Color.DarkGreen;
                            Information_container.AppendText("[" + url.Title + "]" + " > Playlist." + Environment.NewLine);
                            Information_container.SelectionColor = Color.DarkGreen;
                            Information_container.AppendText("[" + url.Url + "]" + " > Playlist." + Environment.NewLine);
                            Information_container.SelectionColor = Color.Black;
                            Information_container.SelectionColor = Color.DarkGreen;
                            Information_container.AppendText("New playlist!" + Environment.NewLine);
                            Information_container.SelectionColor = Color.Black;
                        }
                        result.Add(url.Url);
                        total += 1;
                    }
                    
                }
                downloadComplete = true;
                string[] new_playlist = result.ToArray();
                Information_container.SelectionColor = Color.DarkGreen;
                if (Variables.language == "PL")
                    Information_container.AppendText("Rozmiar połączonej playlisty: " + total.ToString() + Environment.NewLine);
                else
                    Information_container.AppendText("Combined playlist size: " + total.ToString() + Environment.NewLine);
                Information_container.SelectionColor = Color.Black;
                Variables.total = total.ToString();
                if (Variables.language == "PL")
                    counter.Text = "Aktualny: " + Variables.left.ToString() + "/" + Variables.total;
                else
                    counter.Text = "Current: " + Variables.left.ToString() + "/" + Variables.total;
                foreach (var url in new_playlist)
                {
                    await Task.Run(() => StartDownloadAsync(url));
                    if (Variables.language == "PL")
                        counter.Text = "Aktualny: " + Variables.left.ToString() + "/" + Variables.total;
                    else
                        counter.Text = "Current: " + Variables.left.ToString() + "/" + Variables.total;
                }
            }
            else
            {
                List<string> result = new List<string>();
                foreach (var playlist_link in lista_linkow_z_playlistami)
                {
                    Information_container.AppendText(Environment.NewLine);
                    if (Variables.language == "PL")
                        Information_container.AppendText("Adres do playlisty: " + playlist_link);
                    else
                        Information_container.AppendText("Playlist URL: " + playlist_link);
                    Information_container.AppendText(Environment.NewLine);
                    using (CookieWebClient client = new CookieWebClient())
                    {
                        string htmlCode = client.DownloadString(playlist_link);
                        //Information_container.AppendText(htmlCode);
                        // foreach (Match match in Regex.Matches(htmlCode, "\"(?:videoId)([^\"]*)\"")) working regex to find videoId
                        string nirray_parse_youtubeID = "";
                        int total = 0;
                        foreach (Match match in Regex.Matches(htmlCode, "(?:\"videoId)\":\"..........."))
                        {
                            total += 1;
                            Information_container.AppendText(total.ToString() + ". https://www.youtube.com/watch?v=");
                            nirray_parse_youtubeID = match.ToString();
                            nirray_parse_youtubeID = nirray_parse_youtubeID.Replace("\"videoId\":\"", "");
                            Information_container.AppendText(nirray_parse_youtubeID);
                            Information_container.AppendText(Environment.NewLine);
                            downloadComplete = true;
                            result.Add("https://www.youtube.com/watch?v=" + nirray_parse_youtubeID);
                            //await Task.Run(() => StartDownloadAsync("https://www.youtube.com/watch?v="+ nirray_parse_youtubeID));
                            //counter.Text = "Stan: " + Variables.left.ToString() + "/" + Variables.total;
                        }
                    }
                }
                List<string> uniqueList = result.Distinct().ToList();
                string[] new_playlist = uniqueList.ToArray();
                downloadComplete = true;
                foreach (var url in new_playlist)
                {
                    await Task.Run(() => StartDownloadAsync(url));
                    if (Variables.language == "PL")
                        counter.Text = "Aktualny: " + Variables.left.ToString() + "/" + Variables.total;
                    else
                        counter.Text = "Current: " + Variables.left.ToString() + "/" + Variables.total;
                }
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
            pobieranieToolStripMenuItem.Enabled = false;
            if (Variables.language == "PL")
            {
                counter.Text = "Aktualny: " + Variables.left.ToString() + "/" + Variables.total;
                Can_download_text.Text = "Pomyślnie: " + Variables.can_download.ToString() + "/" + Variables.total;
                Cannot_download_text.Text = "Niepomyślnie: " + Variables.cannot_download.ToString() + "/" + Variables.total;
            }
            else
            {
                counter.Text = "Current: " + Variables.left.ToString() + "/" + Variables.total;
                Can_download_text.Text = "Successful: " + Variables.can_download.ToString() + "/" + Variables.total;
                Cannot_download_text.Text = "Unsuccessful: " + Variables.cannot_download.ToString() + "/" + Variables.total;
            }
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
            if (alternativeSquadToolStripMenuItem.Checked == false)
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
                    if (Variables.language == "PL")
                    {
                        Information_container.SelectionColor = Color.DarkGreen;
                        Information_container.AppendText("[" + url.Title + "]" + " > Playlista." + Environment.NewLine);
                        Information_container.SelectionColor = Color.DarkGreen;
                        Information_container.AppendText("[" + url.Url + "]" + " > Playlista." + Environment.NewLine);
                        Information_container.SelectionColor = Color.Black;
                    }
                    else
                    {
                        Information_container.SelectionColor = Color.DarkGreen;
                        Information_container.AppendText("[" + url.Title + "]" + " > Playlist." + Environment.NewLine);
                        Information_container.SelectionColor = Color.DarkGreen;
                        Information_container.AppendText("[" + url.Url + "]" + " > Playlist." + Environment.NewLine);
                        Information_container.SelectionColor = Color.Black;
                    }
                    temp_playlist.Add(url.Url);
                }
                downloadComplete = true;
                string[] new_playlist2 = temp_playlist.ToArray();
                Variables.total = new_playlist2.Length.ToString();
                if (Variables.language == "PL")
                    counter.Text = "Aktualny: " + Variables.left.ToString() + "/" + Variables.total;
                else
                    counter.Text = "Current: " + Variables.left.ToString() + "/" + Variables.total;
                foreach (var url in new_playlist2)
                {
                    await Task.Run(() => StartDownloadAsync(url));
                    if (Variables.language == "PL")
                        counter.Text = "Aktualny: " + Variables.left.ToString() + "/" + Variables.total;
                    else
                        counter.Text = "Current: " + Variables.left.ToString() + "/" + Variables.total;
                }
            }
            else
            {
                List<string> result = new List<string>();
                using (CookieWebClient client = new CookieWebClient())
                {
                    string htmlCode = client.DownloadString(DirectUrlContainer.Text);
                    //Information_container.AppendText(htmlCode);
                    // foreach (Match match in Regex.Matches(htmlCode, "\"(?:videoId)([^\"]*)\"")) working regex to find videoId
                    string nirray_parse_youtubeID = "";
                    int total = 0;
                    foreach (Match match in Regex.Matches(htmlCode, "(?:\"videoId)\":\"..........."))
                    {
                        total += 1;
                        Information_container.AppendText(total.ToString() + ". https://www.youtube.com/watch?v=");
                        nirray_parse_youtubeID = match.ToString();
                        nirray_parse_youtubeID = nirray_parse_youtubeID.Replace("\"videoId\":\"", "");
                        Information_container.AppendText(nirray_parse_youtubeID);
                        Information_container.AppendText(Environment.NewLine);
                        downloadComplete = true;
                        result.Add("https://www.youtube.com/watch?v=" + nirray_parse_youtubeID);
                        //await Task.Run(() => StartDownloadAsync("https://www.youtube.com/watch?v="+ nirray_parse_youtubeID));
                        //counter.Text = "Stan: " + Variables.left.ToString() + "/" + Variables.total;
                    }
                }
                List<string> uniqueList = result.Distinct().ToList();
                string[] new_playlist = uniqueList.ToArray();
                downloadComplete = true;
                foreach (var url in new_playlist)
                {
                    await Task.Run(() => StartDownloadAsync(url));
                    if (Variables.language == "PL")
                        counter.Text = "Aktualny: " + Variables.left.ToString() + "/" + Variables.total;
                    else
                        counter.Text = "Current: " + Variables.left.ToString() + "/" + Variables.total;
                }
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
                    if (Variables.language == "PL")
                        Information_container.AppendText("[" + localDate + "]" + " > Pozyskiwanie zawartości." + Environment.NewLine);
                    else
                        Information_container.AppendText("[" + localDate + "]" + " > Getting data." + Environment.NewLine);
                    if (!url.Contains("youtube"))
                    {
                        if (Variables.language == "PL")
                            Progress_text.Text = "Adres do pobrania nie jest odnośnikiem do serwisu YouTube.";
                        else
                            Progress_text.Text = "The download URL is not a link to YouTube.";

                    }
                    else if (url.Contains("playlist"))
                    {
                        Variables.total = "0";
                        Variables.left = 0;
                        pobieranieToolStripMenuItem.Enabled = false;
                        DownloadFromDirectLink.Enabled = false;
                        DownloadListButton.Enabled = false;
                        pobieranieToolStripMenuItem.Enabled = false;
                        plikToolStripMenuItem.Enabled = false;
                        RunDownloadForPlaylist(url);

                    }
                    else
                    {
                        Variables.total = "0";
                        Variables.left = 0;
                        pobieranieToolStripMenuItem.Enabled = false;
                        DownloadFromDirectLink.Enabled = false;
                        DownloadListButton.Enabled = false;
                        pobieranieToolStripMenuItem.Enabled = false;
                        plikToolStripMenuItem.Enabled = false;
                        downloadComplete = true;
                        Information_container.AppendText(Environment.NewLine);
                        Information_container.AppendText(url.ToString());
                        Information_container.AppendText(Environment.NewLine);
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
                    if (Variables.language == "PL")
                        Information_container.AppendText("[" + localDate + "]" + " > Pozyskiwanie zawartości." + Environment.NewLine);
                    else
                        Information_container.AppendText("[" + localDate + "]" + " > Getting data." + Environment.NewLine);
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
            if (Variables.language == "PL")
            {
                About_Window About = new About_Window();
                About.ShowDialog();
            }
            else
            {
                About_window_ENG AboutEng = new About_window_ENG();
                AboutEng.ShowDialog();
            }
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
            if (progressBar1.Value == 100 & !Variables.left.Equals(Variables.total) && (PauseButton.Text == "Kontynuuj" || PauseButton.Text == "Continue"))
            {
                downloadComplete = true;
            }
            if (Variables.status == 0)
            {
                Variables.status = 3;
                if (Variables.language == "PL")
                    PauseButton.Text = "Kontynuuj";
                else
                    PauseButton.Text = "Continue";
                return;
            }
            else
            {
                Variables.status = 0;
                if (Variables.language == "PL")
                    PauseButton.Text = "Wstrzymaj";
                else
                    PauseButton.Text = "Pause";
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

        private void Information_container_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                Clipboard.SetText(Information_container.Text);
            }
            catch (System.ArgumentNullException)
            {
                Clipboard.SetText(" ");
            }
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
            
            Variables.no_more_than = 999;
            if (turboNoLimitToolStripMenuItem.Checked == true)
            {
                DisableAll();
                turboNoLimitToolStripMenuItem.Checked = true;
                EnableSpeedo(true);
            }
            else
            {
                DisableAll();
                EnableSpeedo(false);

            }
        }
        public void EnableSpeedo(bool variable)
        {
            if (variable)
            {
                Variables.status = 0;
                if (Variables.language == "PL")
                    PauseButton.Text = "Wstrzymaj";
                else
                    PauseButton.Text = "Pause";
                speedDownloadToolStripMenuItem.Checked = true;
                ToolBox_ConvertToMp3.Enabled = false;
                speedimage.Visible = true;
                speedtext.Visible = true;
                PauseButton.Enabled = false;
            }
            else
            {
                ToolBox_ConvertToMp3.Enabled = true;
                speedDownloadToolStripMenuItem.Checked = false;
                speedimage.Visible = false;
                speedtext.Visible = false;
                PauseButton.Enabled = true;
            }
        }
        private void twenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (twenToolStripMenuItem.Checked == true)
            {
                DisableAll();
                twenToolStripMenuItem.Checked = true;
                Variables.no_more_than = 20;
                EnableSpeedo(true);
            }
            else
            {
                DisableAll();
                Variables.no_more_than = 999;
                EnableSpeedo(false);
            }
        }
        public void DisableAll()
        {
            ToolBox_ConvertToMp3.Checked = false;
            ToolBox_ConvertToMp3.Enabled = false;
            DisableAllBitrate();
            turboNoLimitToolStripMenuItem.Checked = false;
            toolStripMenuItem2.Checked = false;
            tenToolStripMenuItem.Checked = false;
            twenToolStripMenuItem.Checked = false;
            thirToolStripMenuItem.Checked = false;
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (toolStripMenuItem2.Checked == true)
            {
                DisableAll();
                toolStripMenuItem2.Checked = true;
                Variables.no_more_than = 5;
                EnableSpeedo(true);
            }
            else
            {
                DisableAll();
                Variables.no_more_than = 999;
                EnableSpeedo(false);
            }
            
        }

        private void tenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tenToolStripMenuItem.Checked == true)
            {
                DisableAll();
                tenToolStripMenuItem.Checked = true;
                Variables.no_more_than = 10;
                EnableSpeedo(true);
            }
            else
            {
                DisableAll();
                Variables.no_more_than = 999;
                EnableSpeedo(false);
            }
        }

        private void thirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (thirToolStripMenuItem.Checked == true)
            {
                DisableAll();
                thirToolStripMenuItem.Checked = true;
                Variables.no_more_than = 30;
                EnableSpeedo(true);
            }
            else
            {
                DisableAll();
                Variables.no_more_than = 999;
                EnableSpeedo(false);
            }
        }
        public void DisableAllBitrate()
        {
            mp3_32kb.Checked = false;
            mp3_96kb.Checked = false;
            mp3_128kb.Checked = false;
            mp3_160kb.Checked = false;
            mp3_192kb.Checked = false;
            mp3_224kb.Checked = false;
            mp3_256kb.Checked = false;
            mp3_272kb.Checked = false;
            mp3_always_best.Checked = false;
            ToolBox_ConvertToMp3.Checked = false;
        }
        private void Mp3_always_best_Click(object sender, EventArgs e)
        {
            if (mp3_always_best.Checked == true)
            {
                DisableAllBitrate();
                ToolBox_ConvertToMp3.Checked = true;
                mp3_always_best.Checked = true;
                Variables.bitrate = 320000;
            }
            else
            {
                DisableAllBitrate();
            }
        }

        private void mp3_32kb_Click(object sender, EventArgs e)
        {
            if (mp3_32kb.Checked == true)
            {
                DisableAllBitrate();
                ToolBox_ConvertToMp3.Checked = true;
                mp3_32kb.Checked = true;
                Variables.bitrate = 32000;
            }
            else
            {
                DisableAllBitrate();
            }
        }

        private void mp3_96kb_Click(object sender, EventArgs e)
        {
            if (mp3_96kb.Checked == true)
            {
                DisableAllBitrate();
                ToolBox_ConvertToMp3.Checked = true;
                mp3_96kb.Checked = true;
                Variables.bitrate = 96000;
            }
            else
            {
                DisableAllBitrate();
            }
        }

        private void mp3_128kb_Click(object sender, EventArgs e)
        {
            if (mp3_128kb.Checked == true)
            {
                DisableAllBitrate();
                ToolBox_ConvertToMp3.Checked = true;
                mp3_128kb.Checked = true;
                Variables.bitrate = 128000;
            }
            else
            {
                DisableAllBitrate();
            }
        }

        private void mp3_160kb_Click(object sender, EventArgs e)
        {
            if (mp3_160kb.Checked == true)
            {
                DisableAllBitrate();
                ToolBox_ConvertToMp3.Checked = true;
                mp3_160kb.Checked = true;
                Variables.bitrate = 160000;
            }
            else
            {
                DisableAllBitrate();
            }
        }

        private void mp3_192kb_Click(object sender, EventArgs e)
        {
            if (mp3_192kb.Checked == true)
            {
                DisableAllBitrate();
                ToolBox_ConvertToMp3.Checked = true;
                mp3_192kb.Checked = true;
                Variables.bitrate = 192000;
            }
            else
            {
                DisableAllBitrate();
            }
        }

        private void mp3_256kb_Click(object sender, EventArgs e)
        {
            if (mp3_256kb.Checked == true)
            {
                DisableAllBitrate();
                ToolBox_ConvertToMp3.Checked = true;
                mp3_256kb.Checked = true;
                Variables.bitrate = 256000;
            }
            else
            {
                DisableAllBitrate();
            }
        }

        private void mp3_272kb_Click(object sender, EventArgs e)
        {
            if (mp3_272kb.Checked == true)
            {
                DisableAllBitrate();
                ToolBox_ConvertToMp3.Checked = true;
                mp3_272kb.Checked = true;
                Variables.bitrate = 272000;
            }
            else
            {
                DisableAllBitrate();
            }
        }

        private void mp3_224kb_Click(object sender, EventArgs e)
        {
            if (mp3_224kb.Checked == true)
            {
                DisableAllBitrate();
                ToolBox_ConvertToMp3.Checked = true;
                mp3_224kb.Checked = true;
                Variables.bitrate = 224000;
            }
            else
            {
                DisableAllBitrate();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Information_container.SelectionStart = Information_container.Text.Length;
            // scroll it automatically
            Information_container.ScrollToCaret();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            using (CookieWebClient client = new CookieWebClient())
            {
                string htmlCode = client.DownloadString(UserPlaylist.Text);
                //Information_container.AppendText(htmlCode);
                // foreach (Match match in Regex.Matches(htmlCode, "\"(?:videoId)([^\"]*)\"")) working regex to find videoId
                string nirray_parse_youtubeID = "";
                foreach (Match match in Regex.Matches(htmlCode, "(?:\"videoId)\":\"..........."))
                {
                    Information_container.AppendText("https://www.youtube.com/watch?v=");
                    nirray_parse_youtubeID = match.ToString();
                    nirray_parse_youtubeID = nirray_parse_youtubeID.Replace("\"videoId\":\"", "");
                    Information_container.AppendText(nirray_parse_youtubeID);
                    Information_container.AppendText(Environment.NewLine);
                }
            }
        }

        private void alternativeSquadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Lang_Polish_Click(object sender, EventArgs e)
        {
            Variables.language = "PL";
            ToolTip.Active = true;
            Lang_English.Checked = false;
            Lang_Polish.Checked = true;
            translateToolStripMenuItem.Text = "Język";
            plikToolStripMenuItem.Text = "Plik";
            pobieranieToolStripMenuItem.Text = "Pobieranie";
            pomocToolStripMenuItem.Text = "Pomoc";
            ToolBox_ConvertToMp3.Text = "Automatycznie konwertuj do MP3";
            mp3_always_best.Text = "Zawsze najlepsza jakość";
            usuwajPlikiŹródłoweToolStripMenuItem.Text = "Usuwaj pliki źródłowe po pomyślnej konwersji";
            ToolBox_NewLocation.Text = "Wybierz inną lokalizacje zapisu";
            ToolBox_DoNotDownloadAlready.Text = "Nie pobieraj istniejących już plików";
            countPlaylistDiscSpaceToolStripMenuItem.Text = "Przelicz rozmiar przed pobraniem";
            speedDownloadToolStripMenuItem.Text = "Szybkie pobieranie";
            turboNoLimitToolStripMenuItem.Text = "Bez limitu";
            toolStripMenuItem2.Text = "5 jednocześnie";
            tenToolStripMenuItem.Text = "10 jednocześnie";
            twenToolStripMenuItem.Text = "20 jednocześnie";
            thirToolStripMenuItem.Text = "30 jednocześnie";
            własneUstawieniaToolStripMenuItem.Text = "Własne ustawienia";
            alternativeSquadToolStripMenuItem.Text = "Alternatywna składnia dla parsowania playlist (videoId)";
            informacjeToolStripMenuItem.Text = "Informacje";
            GitHubError.Text = "Zgłoś błąd";
            groupBox1.Text = "Stan";
            counter.Text = "Aktualny:";
            Can_download_text.Text = "Pomyślnie:";
            Cannot_download_text.Text = "Niepomyślnie:";
            DownloadFromDirectLink.Text = "Pobierz";
            PauseButton.Text = "Wstrzymaj";
            DownloadListButton.Text = "Pobierz z pliku tekstowego";
            speedtext.Text = "Szybkie pobieranie załączone";
            groupBox2.Text = "Adresy";
            Checkbox_UseUrl.Text = "Adres do pobrania";
            Checkbox_UseCombine.Text = "Połącz playlisty";
            backgroundthreads.Text = "Aktualnie w tle:";
            maxsizeof.Text = "Rozmiar:";
        }

        private void Lang_English_Click(object sender, EventArgs e)
        {
            Variables.language = "ENG";
            ToolTip.Active = false;
            Lang_English.Checked = true;
            Lang_Polish.Checked = false;
            translateToolStripMenuItem.Text = "Language";
            plikToolStripMenuItem.Text = "File";
            pobieranieToolStripMenuItem.Text = "Download";
            pomocToolStripMenuItem.Text = "Help";
            ToolBox_ConvertToMp3.Text = "Automatically convert to MP3";
            mp3_always_best.Text = "Always the best audio quality";
            usuwajPlikiŹródłoweToolStripMenuItem.Text = "Remove source files after successful conversion";
            ToolBox_NewLocation.Text = "Select a different save location";
            ToolBox_DoNotDownloadAlready.Text = "Skip existing files";
            countPlaylistDiscSpaceToolStripMenuItem.Text = "Check total size before start";
            speedDownloadToolStripMenuItem.Text = "Asynchronous download";
            turboNoLimitToolStripMenuItem.Text = "No limits";
            toolStripMenuItem2.Text = "5 at once";
            tenToolStripMenuItem.Text = "10 at once";
            twenToolStripMenuItem.Text = "20 at once";
            thirToolStripMenuItem.Text = "30 at once";
            własneUstawieniaToolStripMenuItem.Text = "Custom";
            alternativeSquadToolStripMenuItem.Text = "Alternative syntax for parsing playlists (videoId)";
            informacjeToolStripMenuItem.Text = "About";
            GitHubError.Text = "Report the problem";
            groupBox1.Text = "State";
            counter.Text = "Current:";
            Can_download_text.Text = "Successful:";
            Cannot_download_text.Text = "Unsuccessful:";
            DownloadFromDirectLink.Text = "Download";
            PauseButton.Text = "Pause";
            DownloadListButton.Text = "Download from text file";
            speedtext.Text = "Asynchronous download";
            groupBox2.Text = "URL";
            Checkbox_UseUrl.Text = "Video link";
            Checkbox_UseCombine.Text = "Combine playlists";
            backgroundthreads.Text = "Background download:";
            maxsizeof.Text = "Size:";
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


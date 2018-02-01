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
                    string webData = wc.DownloadString("http://cutenesss.xyz/SteamerTest.html");
                    if (!webData.ToUpperInvariant().Contains("online"))
                    {
                        Console.WriteLine("Works!");
                        Console.ReadKey();
                        // Do Something
                        if (!webData.ToUpperInvariant().Contains("ip:"))
                        {
                            string search = "qwertyuiopasdf";
                            string result = webData.Substring(webData.IndexOf(search) + search.Length);
                            result = result.Replace(@"</p>", "");
                            Console.WriteLine(result);
                            Console.ReadKey();

                        }
                    }
                    else
                    {
                        Console.WriteLine("Doesnt work!");
                        Console.ReadKey();
                    }
                }
                catch { }
            }
          
            }
        }
}

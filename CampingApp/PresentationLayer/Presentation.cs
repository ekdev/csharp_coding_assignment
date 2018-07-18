using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using CampingApp.BusinessLayer;

namespace CampingApp.PresentationLayer
{
    public static class Presentation
    {

        public static void DisplayToConsole(string computedPay)
        {
            Console.WriteLine(computedPay);
            Console.ReadLine();
        }

        public static void WriteToTextOutput(string computedPay, string path)
        {
            File.WriteAllText(computedPay, path);
        }

    }
}

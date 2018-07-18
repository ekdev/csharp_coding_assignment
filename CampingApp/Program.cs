using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using CampingApp.BusinessLayer;
using CampingApp.DataAccessLayer;
using CampingApp.PresentationLayer;

namespace CampingApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var computedPay = BusinessModels.ComputeDebitOrCredit();
            int length = BusinessModels.CountCampingGroups();
            string outputPath = Path.GetFullPath(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName
         + @"\Output\Input.txt.out");
            StringBuilder sb = new StringBuilder();


            for (int i = 1; i <= length; i++)
            {
                var listByGroup = computedPay.Where(f => f.CamperGroupIndex == i).Select(value => value.DebitOrCredit).ToList();

                foreach (var item in listByGroup)
                {
                    Presentation.DisplayToConsole(item.ToString());
                    sb.AppendLine(item.ToString());

                }

                if (length > i)
                {
                    //Console.WriteLine("");
                    Presentation.DisplayToConsole(" ");
                    sb.AppendLine(" ");

                }

            }

            Presentation.WriteToTextOutput(outputPath, sb.ToString());
        }

    }
}

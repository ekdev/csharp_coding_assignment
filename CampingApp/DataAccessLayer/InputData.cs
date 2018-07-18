using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampingApp.DataAccessLayer
{
    public class InputData
    {
        public int CamperGroupIndex { get; set; }
        public int CamperIndex { get; set; }
        public int ReceiptIndex { get; set; }
        public decimal ReceiptValue { get; set; }
    }
}

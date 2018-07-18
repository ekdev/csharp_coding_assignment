using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CampingApp.DataAccessLayer;


namespace CampingApp.BusinessLayer
{
    public static class BusinessModels
    {
        
        private static List<InputData> GetInputList()
        {
            DataAccess dataAccess = new DataAccess();
            List<InputData> parsedData = dataAccess.RetrieveIndexedValues();
            return parsedData;
        }


        public static int CountCampingGroups()
        {
            var indexedInputData = GetInputList();
            var lastElementNumber = indexedInputData.Last();
            int numberOfGroups = lastElementNumber.CamperGroupIndex;
            return numberOfGroups;

        }


        private static decimal ComputeExpectedValue(int camperGroup)
        {

            var indexedInputData = GetInputList();

            int indexCount = CountCampers(camperGroup);

            decimal summedValue = indexedInputData.Where(f => f.CamperGroupIndex == camperGroup).Sum(item => item.ReceiptValue);

            decimal expectedValue = summedValue / indexCount;

            return expectedValue;

        }

        private static int CountCampers(int camperGroup)
        {
            var indexedInputData = GetInputList();

            var selectedArray = indexedInputData.Where(f => f.CamperGroupIndex == camperGroup).Select(camper => camper.CamperIndex).ToList();

            int indexCount = selectedArray.Last();

            return indexCount;
        }

        public static List<PayData> ComputeDebitOrCredit()
        {
            var indexedInputData = GetInputList();
            int campingGroupSize = CountCampingGroups();

            List<PayData> payList = new List<PayData>();


            
            for (int i = 1; i <= campingGroupSize; i++)
            {
                decimal expectedVal = ComputeExpectedValue(i);

                for (int j = 1; j <= CountCampers(i); j++)
                {
                    decimal indivSum = indexedInputData.Where(f => f.CamperGroupIndex == i && f.CamperIndex == j).Sum(item => item.ReceiptValue);
                    PayData payData = new PayData();
                    payData.CamperGroupIndex = i;
                    payData.DebitOrCredit = Math.Round(expectedVal - indivSum,2,MidpointRounding.AwayFromZero);

                    payList.Add(payData);
                }
                
            }

            return payList;
        }
    }
}

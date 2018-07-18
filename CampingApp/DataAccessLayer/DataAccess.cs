using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace CampingApp.DataAccessLayer
{
    public class DataAccess
    {

        string inputFilePath = Path.GetFullPath(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName
         + @"\Input\Input.txt");

        private int ReturnLineInteger(int index) {

            return Int32.Parse(File.ReadLines(inputFilePath).ElementAt(index));
        }

        private decimal ReturnLineDecimal(int index) {
            return decimal.Parse(File.ReadLines(inputFilePath).ElementAt(index), CultureInfo.InvariantCulture.NumberFormat);
        }

        public int GetLastLine(string inputFilePath) {

            StreamReader inputFile = new StreamReader(inputFilePath);
            int lastLine = 0;
            int counter = 0;
            string line;

            while ((line = inputFile.ReadLine()) != null)
            {
                counter++;
                try
                {
                    if (decimal.Parse(line, CultureInfo.InvariantCulture.NumberFormat) == 0)
                    {
                        lastLine = counter;
                    }
                }
                catch (InvalidCastException ex)
                {
                    throw ex;
                }
            }
            return lastLine;
        }

        public List<InputData> RetrieveIndexedValues()
        {
            //get the first line and second line value which will be fixed regardless of data
            int firstLineValue = ReturnLineInteger(0);
            int secondLineValue = ReturnLineInteger(1);
            int lastLine = GetLastLine(inputFilePath);


            //initilize list class with multiple indices
            List<InputData> inputList = new List<InputData>();
       
            //setting default values of various indexes or counters
            int nextLine = 0;
            int lineCount = 0;
            int camperIndex = 1;
            int camperGroupIndex = 1;
            
            for (int currentLine = 0; currentLine < lastLine; currentLine++)
            {
                lineCount = currentLine + 1;

                //accessing the receipt count of the first camper of the first camping group
                if (currentLine == 1)
                {
                    //get the line number of the next camper's receipt counter
                    nextLine = currentLine + 1 + ReturnLineInteger(currentLine);
                    
                    //append current camper's indexed values to the list
                    for (int receiptIndex = 1; receiptIndex <= ReturnLineInteger(currentLine); receiptIndex++)

                    {
                        InputData inputData = new InputData();
                        inputData.CamperGroupIndex = camperGroupIndex;
                        inputData.CamperIndex = camperIndex;
                        inputData.ReceiptIndex = receiptIndex;
                        inputData.ReceiptValue = ReturnLineDecimal(lineCount);

                        inputList.Add(inputData);

                        lineCount++;
                        
                    }                                      
                }

                //accessing the next camper
                if (currentLine == nextLine && currentLine != 0)
                {
                    //get the line number of the next camper's receipt counter
                    nextLine = currentLine + 1 + ReturnLineInteger(currentLine);

                    //increase the index value of the camper
                    camperIndex++;

                    //append current camper's indexed values to the list
                    for (int receiptIndex = 1; receiptIndex <= ReturnLineInteger(currentLine); receiptIndex++)
                    {
                        InputData inputData = new InputData();
                        inputData.CamperGroupIndex = camperGroupIndex;
                        inputData.CamperIndex = camperIndex;
                        inputData.ReceiptIndex = receiptIndex;
                        inputData.ReceiptValue = ReturnLineDecimal(lineCount);

                        inputList.Add(inputData);

                        lineCount++;

                    }

                    //check whether the current camper is the last camper of the group
                    if (camperIndex == firstLineValue)
                    {
                        camperGroupIndex++;
                        firstLineValue = ReturnLineInteger(nextLine);
                        camperIndex = 0;
                        nextLine = nextLine + 1;

                        //check whether the current camper is the last camper of the final group and break the loop
                        if (nextLine == lastLine - 1)
                        {
                            break;
                        }
                    }
                }


            }
        
            return inputList;
        }

    }

}

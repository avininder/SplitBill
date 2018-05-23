﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitBill
{
    class Program
    {
        static void Main(string[] args)
        {

                string filename = "splitbill", filepath = "";
                int count = 0, index = 0, charges = 0;
                float sum = 0;
                string[] content;
                List<float> participantcontrib = new List<float>();
                List<string> participantshare = new List<string>();
            
                filepath = AppDomain.CurrentDomain.BaseDirectory + filename + ".txt";

                Console.WriteLine("Input File:  " + filepath);

                //read file contents               
                content = File.ReadAllLines(filepath).Where(x => !string.IsNullOrEmpty(x)).ToArray();
                while (Int32.Parse(content[index]) != 0)
                {
                    //Number of participants
                    count = Int32.Parse(content[index == 1 ? 0 : index]);
                    index++;
                    // Loop throught the participants
                    for (int i = 1; i <= count; i++)
                    {
                        // Group charges count
                        charges = Int32.Parse(content[index]);
                        index++;
                        //Loop through the selected group to read charges values 
                        for (int j = 1; j <= charges; j++)
                        {
                            //sum of each group receipt value
                            sum = sum + float.Parse(content[index]);
                            index++;
                        }

                        //store the receipt sum in the List 
                        participantcontrib.Add(sum);  
                        sum = 0;
                    }
                    //Loop through to calculate participant share 
                    for (int i = 0; i < count; i++)
                    {
                        participantshare.Add((participantcontrib.Average() - participantcontrib[i]).ToString("#,##0.00;(#,##0.00)"));                        
                    }

                    //clear the trip participants contribution list for next trip calculation
                    participantcontrib.Clear();
                    participantshare.Add("");
                }

                File.Delete(filepath + ".out");
                File.WriteAllLines(filepath + ".out", participantshare);
                Console.WriteLine("Output File:  " + filepath + ".out");  
            
        }         
    }
}

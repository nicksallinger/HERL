using System;
using System.Collections.Generic;
using System.IO;

namespace Project1
{
    
    class MainClass
    {
        //public static List<Subject> list;
        public static int numCols = 77;

        public static void Main(string[] args)
        {
            //no 55
            /* CASE 1:
             * This is for the case of 
             * getting all files in a directory and 
             * doing calculations on all of them
             */
            //initialized to 500 to not have to grow as more are added, will accomodate 100 subjects with 5 tests each          
            //list = new List<Subject>(500);
            //gets all files that end in .csv in named directory
            //TODO: change to current directory
            String[] allfiles = System.IO.Directory.GetFiles("/Users/nicholassallinger/HERL/RawData", "*.csv", System.IO.SearchOption.AllDirectories);
            StreamReader reader = new StreamReader("/Users/nicholassallinger/HERL/SetupTransferFrames.txt");

            //STATIC TRIALS
            String[] staticFiles = System.IO.Directory.GetFiles("/Users/nicholassallinger/HERL/RawData/StaticTrials", "*.csv", System.IO.SearchOption.AllDirectories);
            StreamReader staticrReader = new StreamReader("/Users/nicholassallinger/HERL/SetupTransferFrames.txt");

            Subject temp = null;
            String staticFile = null;
            int start = 0;
            int end = 0;


            Subject.SetupOutputFiles();
            //for all of the .csv files, creat a new Subject object initialized with the filename and frame data
            //if framedata is not available, initialized with 0,0
            for (int i = 0; i < 51; i++)
            {

                if (!reader.EndOfStream)
                {
                    String[] line = reader.ReadLine().Split();
                    try
                    {
                        start = Int32.Parse(line[0]);
                        end = Int32.Parse(line[1]);
                    }
                    catch (FormatException)
                    {

                    }
                    //Console.WriteLine(line[2]);
                    double mass = Double.Parse(line[2]);
                    double height = Double.Parse(line[3]);
                    double chestCircumference = Double.Parse(line[4]);
                    double waistCircumference = Double.Parse(line[5]);
                    double trunkLength = Double.Parse(line[6]);
                    double wheelchairHeight = Double.Parse(line[7]);

                    if (line[0] == "na") { start = 0; }
                    if (line[1] == "na") { end = 0; }
                    if (line[2] == "na") { mass = 0; }
                    if (line[3] == "na") { height = 0; }
                    if (line[4] == "na") { chestCircumference = 0; }
                    if (line[5] == "na") { waistCircumference = 0; }
                    if (line[6] == "na") { trunkLength = 0; }
                    if (line[7] == "na") { wheelchairHeight = 0; }


                    foreach (String s in staticFiles)
                    {
                        //54,53

                        char[] tempFile = allfiles[i].ToCharArray();
                        char[] staticTemp = s.ToCharArray();

                        //Console.WriteLine(tempFile[39].ToString() + tempFile[40].ToString() + "--" + s[52].ToString() + s[53].ToString());


                        if (tempFile[39] == s[52] && tempFile[40] == s[53])
                        {
                            staticFile = s;
                            break;
                        }

                    }
                    //Console.WriteLine("file = " + allfiles[i]);
                    //Console.WriteLine("staticFile " + staticFile);
                    temp = new Subject(allfiles[i], staticFile, start, end, mass, height, chestCircumference, waistCircumference, trunkLength, wheelchairHeight);
                }


                //Console.WriteLine("Added #{0}", i%5);
                //list.Add(temp);
                temp.OutputToTxt();
            }



            /*
            This is for a single subject
            */

            /*//CASE 2
            Console.WriteLine("Enter filename");
            String filename = Console.ReadLine();

            Console.WriteLine("Enter Start Frame: ");
            int start = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter End Frame: ");
            int end = int.Parse(Console.ReadLine());

            Subject subject = new Subject(filename, start, end);




            subject.OutputToTxt();

            */



            /*///CASE3: test first file
            Subject subject = new Subject("/Users/nicholassallinger/HERL/RawData/S01_LB01_P.csv",170,190);
            //Console.WriteLine(subject.SubjectNum);
            //subject.OutputToTxt();
            */


        }
    }
}

        
    

using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Project1
{
    /*
     * Transfer files must be named in format: S##_LB##_P.csv
     * Static files must be named in format: S##_ST##_P.csv
     * 
     * Sensitive to data that it is parsing, instead of putting "na" for uncollected data, please
     * use 0's in .csv files
     * 
     * If not data is available for a subject, please still include 5 rows of 0s, as the alignment of 5 tests
     * per subject 
     */
    public class MainCLS
    {
          public struct AnthroData
        {
            public String subNum;
            public int testNum;
            public int start;
            public int end;
            public double mass;
            public double height;
            public double RUA_Length;
            public double LUA_Length;
            public double R_Forearm_Length;
            public double L_Forearm_Length;
            public double chestCircumference;
            public double waistCircumference;
            public double trunkLength;
            public double R_Thigh_Length;
            public double L_Thigh_Length;
            public double R_Leg_Length;
            public double L_Leg_Length;
            public double R_Foot_Length;
            public double L_Foot_Length;
            public double wheelchairHeight;

        }

        public static readonly int numCols = 77;

        public static void Main(string[] args)
        {
            if (File.Exists(@"C:\Users\Kinect\Desktop\KinectFiles/Output/FrameData.txt"))
            {
                File.Delete(@"C:\Users\Kinect\Desktop\KinectFiles/Output/FrameData.txt");
            }
            //This all runs off of being in the directory *****
            // When this changes the index of checking the subject id # and test # will have to change according to the new one


            //Transfer files must be in this directory
            String[] allfiles = Directory.GetFiles(@"C:\Users\Kinect\Desktop\KinectFiles\LevelBenchTransfers\","*.csv");
            //frames and anthropometric data that is being read in must be in this file
            StreamReader reader = new StreamReader(@"C:\Users\Kinect\Desktop\KinectFiles\SetupTransferFrames.txt");

            //gets the static hold file to use for joint distance measurements, Static trials must be in a seperate folder, named below
            String[] staticFiles = Directory.GetFiles(@"C:\Users\Kinect\Desktop\KinectFiles\Static Trials\", "*.csv");
            
            //Create struct that will be sent
            AnthroData newSub;

            Subject temp = null;
            String staticFile = null;

            newSub.height = 0;
            newSub.start = 0;
            newSub.end = 0;
            newSub.mass = 0;
            newSub.height = 0;
            newSub.RUA_Length = 0;
            newSub.LUA_Length = 0;
            newSub.R_Forearm_Length = 0;
            newSub.L_Forearm_Length = 0;
            newSub.chestCircumference = 0;
            newSub.waistCircumference = 0;
            newSub.trunkLength = 0;
            newSub.R_Thigh_Length = 0;
            newSub.L_Thigh_Length = 0;
            newSub.R_Leg_Length = 0;
            newSub.L_Leg_Length = 0;
            newSub.R_Foot_Length = 0;
            newSub.L_Foot_Length = 0;
            newSub.wheelchairHeight = 0;
            //Sets up the output file header column
            Subject.SetupOutputFiles();

            //for all of the .csv files, create a new Subject object initialized with the filename and frame data

            //FOR ALL .csv files in that directory, do calculations. Make sure there are no other .csv files in the directory beside the transfers, not even static trials
            for (int i = 0; i < allfiles.Length; i++)
            {   //gets frame data
                if (!reader.EndOfStream)
                {
                    String[] line = reader.ReadLine().Split();
                    try
                    {
                        newSub.start = Int32.Parse(line[2]);
                        newSub.end = Int32.Parse(line[3]);
                    }
                    catch (FormatException)
                    {

                    }

                    /*
                     * Input file format:
                     * Subject# test# startFrame endFrame mass height RUA LUA RForearm LForearm chestCircumference waistCircumference trunkLength RThigh LThigh RLeg LLeg RFoot LFoot wcHeigh
                     */

                    newSub.subNum = line[0];
                    newSub.testNum = Int32.Parse(line[1]);
                    newSub.mass = Double.Parse(line[4]);
                    newSub.height = Double.Parse(line[5]);
                    newSub.RUA_Length = Double.Parse(line[6]);
                    newSub.LUA_Length = Double.Parse(line[7]);
                    newSub.R_Forearm_Length = Double.Parse(line[8]);
                    newSub.L_Forearm_Length = Double.Parse(line[9]);
                    newSub.chestCircumference = Double.Parse(line[10]);
                    newSub.waistCircumference = Double.Parse(line[11]);
                    newSub.trunkLength = Double.Parse(line[12]);
                    newSub.R_Thigh_Length = Double.Parse(line[13]);
                    newSub.L_Thigh_Length = Double.Parse(line[14]);
                    newSub.R_Leg_Length = Double.Parse(line[15]);
                    newSub.L_Leg_Length = Double.Parse(line[16]);
                    newSub.R_Foot_Length = Double.Parse(line[17]);
                    newSub.L_Foot_Length = Double.Parse(line[18]);
                    newSub.wheelchairHeight = Double.Parse(line[19]);

                    //TODO: need to come first
                    //if (line[0] == "na") { start = 0; }
                    //if (line[1] == "na") { end = 0; }
                    //if (line[2] == "na") { mass = 0; }
                    //if (line[3] == "na") { height = 0; }
                    //if (line[4] == "na") { chestCircumference = 0; }
                    //if (line[5] == "na") { waistCircumference = 0; }
                    //if (line[6] == "na") { trunkLength = 0; }
                    //if (line[7] == "na") { wheelchairHeight = 0; }

                    //matches the static hold file to accompanying subjects transfer files
                    //File name is very important
                    //TODO: create pattern matching that removes the need to look at entire directory listing

                    //This will need to change if the directory changes from /Users/nicholassallinger/HERL/RawData
                    String subNumtemp = null;

                    foreach (String s in staticFiles)
                    {

                        char[] tempFile = allfiles[i].ToCharArray();
                        char[] staticTemp = s.ToCharArray();

                        if (tempFile[57] == s[51] && tempFile[58] == s[52])
                        {
                           
                            staticFile = s;
                            subNumtemp = tempFile[51].ToString() + tempFile[52].ToString();
                            break;
                        }
                    }
                    temp = new Subject(allfiles[i], staticFile, newSub);

                }
                temp.OutputToTxt();

            }
        }
    }
}





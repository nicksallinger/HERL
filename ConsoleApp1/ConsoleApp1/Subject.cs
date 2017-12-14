//==========================
//Author: Nick Sallinger
//Create Date: 6/5/2017
//Last Modified: 12/14/2017
//Description: Class for calculating anthropometric data, TAI scores, and writing to output
//==========================

using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Project1;
using System.Linq;
using System.Collections.Generic;

namespace Project1
{
    //TODO: cleanup code
    //      create DLL
    //      do butterworth first, then send frame data to here?

    /*
     * Flow:    Calculate all variables in separate function, update global variables
     *          Print to files
     * 
     * There is heavy use of global variables, and impure functions (aka functions modifying global vars not passed in as params) because of time constraints
     * I had planned on cleaning it up, but it works as is, but can be hard to read.
     */

    public class Subject
    {

        //Output files
        static readonly String distanceOutputFile = @"C:\Users\Kinect\Desktop\KinectFiles/Output/JointDistance.csv";
        static readonly String variableOutputFile = @"C:\Users/Kinect/Desktop/KinectFiles/Output/Variables.csv";
        static readonly String TAIOutputFile = @"C:/Users/Kinect/Desktop/KinectFiles/Output/TAIScores.csv";
        static readonly String comparisonOutputFile = @"C:/Users/Kinect/Desktop/KinectFiles/Output/TAIComparison.csv";
        static readonly String frameDataFile = @"C:\Users\Kinect\Desktop\KinectFiles/Output/FrameData.txt";
        static readonly String filterOutputFile = @"C:\Users\Kinect\Desktop\KinectFiles/Output/FilteredData.csv";




        //num rows is unique to each subject, num cols is not
        private int numRows;
        private int numRowsStatic;
        private String subjectNum;
        private String testNum;
        private String[][] data;
        private Double[] doubleData; //For use in Butterworth Filter
        private String[][] staticData;
        private String directory;
        private String staticFile;
        private int startFrame;
        private int endFrame;
        private int tempTestNum;

        //Joint movement fields
        private double spineBase_x;
        private double spineBase_y_trail;
        private double hipLeft_x;
        private double hipLeft_y_trail;
        private double hipRight_x;
        private double hipRight_y_trail;
        private double kneeLeftDistanceTransfer;
        private double kneeRightDistanceTransfer;
        private double spineBase_y_maxmin_transfer;
        private double hipRight_maxmin_transfer;
        private double hipLeft_maxmin_transfer;
        private double footLeftDistanceTransfer;
        private double footRightDistanceTransfer;
        private double ankleLeftDistanceTransfer;
        private double ankleRightDistanceTransfer;
        private double hipLeftDistanceTransfer;
        private double hipRightDistanceTransfer;
        private double spineDistanceTransfer;
        private double hipAngleAtMin;
        private double hipAngleAtMax;
        private double shoulderAngleAtMin;
        private double shoulderAngleAtMax;
        private double kneeLeftDistanceSetting;
        private double kneeRightDistanceSetting;
        private double footLeftDistanceSetting;
        private double footRightDistanceSetting;
        private double ankleLeftDistanceSetting;
        private double ankleRightDistanceSetting;
        private double hipLeftDistanceSetting;
        private double hipRightDistanceSetting;
        private double spineDistanceSetting;
        private double hipAngleStart_setting;
        private double shoulderAngleStart_setting;
        private double hipAngleatEnd_setting;
        private double shoulderAngleatEnd_setting;

        //Joint distance fields, if it starts as manual it is taken in as a param from .txt
        //others set with methods

        private double rightHandTipSize;
        private double leftHandTipSize;
        private double rightHandThumbSize;
        private double leftHandThumbSize;
        private double rightUpperArmLength;
        private double leftUpperArmLength;
        private double rightForearmLength;
        private double leftForearmLength;
        private double shoulderLength;
        private double hipLength;
        private double rightThighLength;
        private double leftThighLength;
        private double rightLegLength;
        private double leftLegLength;
        private double rightFootLength;
        private double leftFootLength;
        private double mass;
        private double height;
        private double trunkLength;
        private double manualRUA_Length;
        private double manualLUA_Length;
        private double manualR_Forearm_Length;
        private double manualL_Forearm_Length;
        private double manualChestCircumference;
        private double manualWaistCircumference;
        private double manualTrunkLength;
        private double manualR_Thigh_Length;
        private double manualL_Thigh_Length;
        private double manualR_Leg_Length;
        private double manualL_Leg_Length;
        private double manualR_Foot_Length;
        private double manualL_Foot_Length;
        private double manualWheelchairHeight;

        //TAI FIELDS
        /*
         * CUT VALUES ARE CHANGED HERE
         */
        private static double CutValue1_1 = 0.9;
        private static double CutValue1_2 = 0.5;
        private static double CutValue1_3 = 0.84;
        private static double CutValue1_6 = .77;
        private static double CutValue1_7 = .918;
        private static double CutValue1_1Calculated = 0.79;
        private static double CutValue1_2Calculated = 0.91;
        private static double CutValue1_3Calculated = 0.74;
        private static double CutValue1_6Calculated = .5;
        private static double CutValue1_7Calculated = .85;


        //Output of TAI calculations
        private int TAI1_1;
        private int TAI1_2;
        private int TAI1_3;
        private int TAI1_6;
        private int TAI1_7;
        private int calculatedTAI1_1;
        private int calculatedTAI1_2;
        private int calculatedTAI1_3;
        private int calculatedTAI1_6;
        private int calculatedTAI1_7;

        //struct passed in with anthropometric data
        public Subject(String directory, String staticFile, MainCLS.AnthroData anthroData)
        {
            startFrame = anthroData.start;
            endFrame = anthroData.end;

            this.directory = directory;
            this.staticFile = staticFile;
            this.tempTestNum = anthroData.testNum;
            this.mass = anthroData.mass;
            this.height = anthroData.height;
            this.manualRUA_Length = anthroData.RUA_Length;
            this.manualLUA_Length = anthroData.LUA_Length;
            this.manualR_Forearm_Length = anthroData.R_Forearm_Length;
            this.manualL_Forearm_Length = anthroData.L_Forearm_Length;
            this.manualChestCircumference = anthroData.chestCircumference;
            this.manualWaistCircumference = anthroData.waistCircumference;
            this.manualTrunkLength = anthroData.trunkLength;
            this.manualR_Thigh_Length = anthroData.R_Thigh_Length;
            this.manualL_Thigh_Length = anthroData.L_Thigh_Length;
            this.manualR_Leg_Length = anthroData.R_Leg_Length;
            this.manualL_Leg_Length = anthroData.L_Leg_Length;
            this.manualR_Foot_Length = anthroData.R_Foot_Length;
            this.manualL_Foot_Length = anthroData.L_Foot_Length;
            this.manualWheelchairHeight = anthroData.wheelchairHeight;
            //There was too much going on in the constructor
            Initialize();
            

        }

        private void Initialize()
        {
           Console.WriteLine("Static File = " + staticFile);
            data = ParseCSV(directory);
            staticData = ParseStaticCSV(staticFile);
            subjectNum = GetSubjectNumber();
            testNum = GetTestNumber();
            if (Convert.ToInt32(testNum) != tempTestNum)
            {
                Console.WriteLine("DATA MISALIGNED IN SUBJECT CLASS");
            }

            //Calculate the start frame using Butterworth filter
            //OUTPUT OF THIS SHOULD BE USED FOR THE CALCULATIONS IN THE FUTURE, BUT IT IS NOT REFINED ENOUGH YET
            CalculateStartFrame(data);

            //only do measurements once per subject, must ensure that there is a corresponding transfer 1
            if ((staticFile != null) & Convert.ToInt32(testNum) == 1)
            {
                rightUpperArmLength = CalculateRightUpperArmLength();
                leftUpperArmLength = CalculateLeftUpperArmLength();
                rightForearmLength = CalculateRightForearmLength();
                leftForearmLength = CalculateLeftForearmLength();
                shoulderLength = CalculateShoulderLength();
                hipLength = CalculateHipLength();
                rightThighLength = CalculateRightThighLength();
                leftThighLength = CalculateLeftThighLength();
                rightLegLength = CalculateRightLegLength();
                leftLegLength = CalculateLeftLegLength();
                rightFootLength = CalculateRightFootLength();
                leftFootLength = CalculateLeftFootLength();
                trunkLength = CalculateTrunkLength();
            }

            //if start = end, dont do frame data			
            if (startFrame != 0 && endFrame != 0)
            {
                spineBase_x = CalculateSpineBaseX();
                Console.WriteLine(spineBase_x);
                spineBase_y_trail = CalculateSpineBaseY();
                hipLeft_x = CalculateHipLeftX();
                hipLeft_y_trail = CalculateHipLeftY();
                hipRight_x = CalculateHipRightX();
                hipRight_y_trail = CalculateHipRightY();

                //setup phase
                kneeLeftDistanceSetting = CalculateKneeLeftDistance(1, startFrame);
                kneeRightDistanceSetting = CalculateKneeRightDistance(1, startFrame);
                footLeftDistanceSetting = CalculateFootLeftDistance(1, startFrame);
                footRightDistanceSetting = CalculateFootRightDistance(1, startFrame);
                ankleLeftDistanceSetting = CalculatAnkleLeftDistance(1, startFrame);
                ankleRightDistanceSetting = CalculateAnkleRightDistance(1, startFrame);
                spineDistanceSetting = CalculateSpineDistance(1, startFrame);
                hipRightDistanceSetting = CalculateHipRightDistance(1, startFrame);
                hipLeftDistanceSetting = CalculateHipLeftDistance(1, startFrame);
                hipAngleStart_setting = CalculateHipAngle(1);
                shoulderAngleStart_setting = CalculateShoulderAngle(1);
                hipAngleatEnd_setting = CalculateHipAngle(startFrame);
                shoulderAngleatEnd_setting = CalculateShoulderAngle(startFrame);

                //transfer phase
                kneeLeftDistanceTransfer = CalculateKneeLeftDistance(startFrame, endFrame);
                kneeRightDistanceTransfer = CalculateKneeRightDistance(startFrame, endFrame);
                footLeftDistanceTransfer = CalculateFootLeftDistance(startFrame, endFrame);
                footRightDistanceTransfer = CalculateFootRightDistance(startFrame, endFrame);
                ankleLeftDistanceTransfer = CalculatAnkleLeftDistance(startFrame, endFrame);
                ankleRightDistanceTransfer = CalculateAnkleRightDistance(startFrame, endFrame);
                spineDistanceTransfer = CalculateSpineDistance(startFrame, endFrame);
                hipRightDistanceTransfer = CalculateHipRightDistance(startFrame, endFrame);
                hipLeftDistanceTransfer = CalculateHipLeftDistance(startFrame, endFrame);
                shoulderAngleAtMin = CalculateShoulderAngle(startFrame);
                shoulderAngleAtMax = CalculateShoulderAngle(endFrame);
                hipAngleAtMin = CalculateHipAngle(startFrame);
                hipAngleAtMax = CalculateHipAngle(endFrame);
                hipLeft_maxmin_transfer = CalculateHipLeftMaxMin(startFrame, endFrame);
                hipRight_maxmin_transfer = CalculateHipRightMaxMin(startFrame, endFrame);
                spineBase_y_maxmin_transfer = CalculateSpineBaseYMaxMin(startFrame, endFrame);
                rightHandTipSize = CalculateRightHandTipSize(startFrame, endFrame);
                leftHandTipSize = CalculateLeftHandTipSize(startFrame, endFrame);
                rightHandThumbSize = CalculateRightHandThumbSize(startFrame, endFrame);
                leftHandThumbSize = CalculateLeftHandThumbSize(startFrame, endFrame);

                TAICalculations();
            }

            else
            {
                //for if there is no frame data
                kneeLeftDistanceTransfer = 0;
                kneeRightDistanceTransfer = 0;
                footLeftDistanceTransfer = 0;
                footRightDistanceTransfer = 0;
                ankleLeftDistanceTransfer = 0;
                ankleRightDistanceTransfer = 0;
                shoulderAngleAtMin = 0;
                shoulderAngleAtMax = 0;
                hipAngleAtMin = 0;
                hipAngleAtMax = 0;
                hipRightDistanceTransfer = 0;
                hipLeftDistanceTransfer = 0;
                ankleLeftDistanceTransfer = 0;
                ankleRightDistanceTransfer = 0;
            }
            
        }

        /// <summary>
        /// Parses the csv file into a 2d array of STRINGS.
        /// </summary>
        /// <returns>2d String array</returns>
        /// <param name="dir">Dir, String format of location of current .csv</param>
        private String[][] ParseCSV(String dir)
        {
            if (File.Exists(dir))
            {
                TextFieldParser rowParser = new TextFieldParser(dir);
                while (!rowParser.EndOfData)
                {
                    rowParser.ReadLine();
                    numRows++;
                }
                rowParser.Close();
            }
            else
            {
                Environment.Exit(0);
            }
            data = new string[numRows][];
            TextFieldParser parser = new TextFieldParser(dir);
            parser.SetDelimiters(",");
            parser.TrimWhiteSpace = true;
            for (int i = 0; i < numRows; i++)
            {
                String[] temp = parser.ReadFields();
                data[i] = temp;
            }
            return data;
        }

        /// <summary>
        /// Parses the static csv to be used in joint distance measurements
        /// </summary>
        /// <returns>The static csv.</returns>
        /// <param name="dir">location of file.</param>
		private String[][] ParseStaticCSV(String dir)
        {
            //Console.WriteLine("dir in parse = " + dir);
            if (File.Exists(dir))
            {
                TextFieldParser rowParser = new TextFieldParser(dir);
                while (!rowParser.EndOfData)
                {
                    rowParser.ReadLine();
                    numRowsStatic++;
                }
                rowParser.Close();
            }
            else
            {
                Console.WriteLine("Static file does not exist");
                Environment.Exit(0);
            }
            staticData = new string[numRowsStatic][];
            TextFieldParser parser = new TextFieldParser(dir);
            parser.SetDelimiters(",");
            parser.TrimWhiteSpace = true;
            for (int i = 0; i < numRowsStatic; i++)
            {
                String[] temp = parser.ReadFields();
                staticData[i] = temp;
            }
            return staticData;
        }


        /// <summary>
        /// Gets the subject number from filename.
        /// </summary>
        /// <returns>The subject number.</returns>
        private String GetSubjectNumber()
        {
            char[] subInfo = directory.ToCharArray();
            int subIndex = 0;
            //int endIndex = 0;
            //Console.WriteLine("0 = {0}",subInfo[0]);
            for (int i = 0; i < subInfo.Length; i++)
            {
                if (subInfo[i].Equals('S'))
                {
                    subIndex = i + 1;
                    break;
                }
            }
            //Console.WriteLine("SubjectNumber = " + subInfo[subIndex] + subInfo[subIndex + 1]);

            String returnString = subInfo[subIndex].ToString() + subInfo[subIndex + 1].ToString();

            //Frame data is incorrect
            if (startFrame >= endFrame)
            {
                returnString = returnString + "*";

            }
            //Console.WriteLine("Subject Number: {0}", returnString);
            return returnString;
        }


        /// <summary>
        /// Gets the test number. This could have been easier to just use direct indexing instead of using pattern matching, but this
        /// allows for the filename format to vary
        /// </summary>
        /// <returns>The test number.</returns>
        private String GetTestNumber()
        {
            char[] testInfo = directory.ToCharArray();
            int testIndex = 0;

            for (int i = 50; i < testInfo.Length; i++)
            {
                if (testInfo[i].Equals('B'))
                {
                    testIndex = i + 1;
                    break;
                }
            }
            Console.WriteLine("Test Number = " + testInfo[testIndex] + testInfo[testIndex + 1]);
            String returnString = testInfo[testIndex].ToString() + testInfo[testIndex + 1].ToString();
            //Console.WriteLine("Test number: {0}",returnString);

            return returnString;
        }

        /// <summary>
        /// Initialize output files
        /// </summary>
        public static void SetupOutputFiles()
        {
            if (File.Exists(distanceOutputFile))
            {
                File.Delete(distanceOutputFile);
            }
            System.IO.StreamWriter jointwr = new System.IO.StreamWriter(distanceOutputFile, true);
            jointwr.WriteLine("Subject ID, rightHandTipSize, leftHandTipSize, rightHandThumbSize," +
               "LeftHandThumbSize, R_UA,L_UA, R_FA, L_FA," +
               "2xShoulder, 2xhip, R_th, L_th, R_Lg," +
               "L_lg, R_ft, L_ft, Trunk_Length");


            jointwr.Close();
            if (File.Exists(variableOutputFile))
            {
                File.Delete(variableOutputFile);
            }
            System.IO.StreamWriter varwr = new System.IO.StreamWriter(variableOutputFile, true);
            //varwr.WriteLine("Subject ID, Test Number, Dx_SpineBase_X, SpineBase_Y_trail, HipLeft_X," +
            //"HipLeft_Y_trail, HipRight_X, HipRight_Y_trail, SpineBase_Y_maxmin_transfer, hipRight_maxmin_transfer," +
            //"HipLeft_maxmin_transfer, kneeLeftDistance, kneeRightDistance,footLeftDistance, FootRightDistance," +
            //"ankleLeftDistance, ankleRightDistance, hipLeftDistance, hipRightDistance,spineDistance," +
            //"hipAngleAtMin, hipAngleAtMax, shoulderAngleAtMin, shoulderAngleAtMax");


            varwr.WriteLine("SubjectNum,TestNum,spineBaseX, hipleftX, hipRightX, SpineBaseY_trail,HipLeftY_trail,HipRightY_trail,tKneeLeftDistance_setting,FootLeftDistance_setting," +
                            "AnkleLeftDistance_setting,HipLeftDistance_setting,SpineDistance_setting,HipAngleStart_setting,ShoulderAngleStart_setting," +
                            "KneeRightDistance_setting,FootRightDistance_setting,AnkleRightDistance_setting,HipRightDistance_setting,HipAngleatEnd_setting," +
                            "ShoulderAngleatEnd_setting,SpineBaseYMax_Min_transfer,HipRightYMax_Min_transfer,HipLeftYMax_Min_transfer");


            varwr.Close();
            if (File.Exists(TAIOutputFile))
            {
                File.Delete(TAIOutputFile);
            }
            System.IO.StreamWriter taiwr = new System.IO.StreamWriter(TAIOutputFile, true);
            taiwr.WriteLine("SubjectNum, Test number, TAI1_1, TAI1_2,TAI1_3," +
                           " TAI1_6,TAI1_7");
            taiwr.Close();

            if (File.Exists(comparisonOutputFile))
            {
                File.Delete(comparisonOutputFile);
            }
            System.IO.StreamWriter compwr = new System.IO.StreamWriter(comparisonOutputFile, true);
            compwr.WriteLine("SubjectNum, Test number, TAI1_1, Calculated TAI1_1, " +
                            "TAI1_2,CalcualteTAI1_2,TAI1_3, CalculatedTAI1_3," +
                           " TAI1_6,CalcualteTAI1_6,TAI1_7,CalcualteTAI1_7");
            compwr.Close();

        }

        /// <summary>
        /// Returns a 2D array of doubles, used for filtering purposes
        /// </summary>
        /// <param name="ar">2d string array that will be converted to 2d long array</param>
        /// <returns>2d long array</returns>
        public double[] ArrayToDouble(String[][] ar)
        {

            //TODO: Find a better way to do this
            int index = 0;
            doubleData = new Double[numRows];
            for(int i = 1; i < numRows; i++)
            {
              //for(int j = 1; j < ar[0].Length; j++)
                //{
                    doubleData[index] = Convert.ToDouble(ar[i][2]);
                   // Console.WriteLine(doubleData[index]);
                    index++;
                //}  
                
            }

            return doubleData;
        }


        /*******************************
         * ANTHROPOMETRIC CALCULATIONS
         ******************************/

        private double CalculateRightUpperArmLength()
        {
            //columns 26-29, 27-30,28-31
            double total = 0;

            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][26]) - Convert.ToDouble(staticData[i][29]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][27]) - Convert.ToDouble(staticData[i][30]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][28]) - Convert.ToDouble(staticData[i][31]), 2));

            }
            //Console.WriteLine(total);
            //Console.WriteLine(numRowsStatic);
            return total / (numRowsStatic - 1);

        }
        private double CalculateLeftUpperArmLength()
        {
            ////14-17
            //SQRT((O2-R2)^2+(P2-S2)^2+(Q2-T2)^2)
            double total = 0;

            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][14]) - Convert.ToDouble(staticData[i][17]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][15]) - Convert.ToDouble(staticData[i][18]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][16]) - Convert.ToDouble(staticData[i][19]), 2));

            }
            return total / (numRowsStatic - 1);

        }
        private double CalculateRightForearmLength()
        {
            //29-32
            //=SQRT((AD2-AG2)^2+(AE2-AH2)^2+(AF2-AI2)^2)
            double total = 0;

            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][29]) - Convert.ToDouble(staticData[i][32]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][30]) - Convert.ToDouble(staticData[i][33]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][31]) - Convert.ToDouble(staticData[i][34]), 2));

            }
            return total / (numRowsStatic - 1);

        }
        private double CalculateLeftForearmLength()
        {
            //17-20
            //= SQRT((R2 - U2) ^ 2 + (S2 - V2) ^ 2 + (T2 - W2) ^ 2)
            double total = 0;

            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][17]) - Convert.ToDouble(staticData[i][20]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][18]) - Convert.ToDouble(staticData[i][21]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][19]) - Convert.ToDouble(staticData[i][22]), 2));

            }

            return total / (numRowsStatic - 1);

        }
        private double CalculateShoulderLength()
        {
            //14-26
            //=2*SQRT((O2-AA2)^2+(P2-AB2)^2+(Q2-AC2)^2)
            double total = 0;

            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + 2 * Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][14]) - Convert.ToDouble(staticData[i][26]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][15]) - Convert.ToDouble(staticData[i][27]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][16]) - Convert.ToDouble(staticData[i][28]), 2));

            }

            return total / (numRowsStatic - 1);

        }
        private double CalculateHipLength()
        {
            //38-50
            //=2*SQRT((AM2-AY2)^2+(AN2-AZ2)^2+(AO2-BA2)^2)
            double total = 0;

            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + 2 * Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][38]) - Convert.ToDouble(staticData[i][50]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][39]) - Convert.ToDouble(staticData[i][51]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][40]) - Convert.ToDouble(staticData[i][52]), 2));

            }

            return total / (numRowsStatic - 1);

        }
        private double CalculateRightThighLength()
        {
            //50-53
            //=SQRT((AY2-BB2)/^2+(AZ2-BC2)^2+(BA2-BD2)^2)
            double total = 0;

            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][50]) - Convert.ToDouble(staticData[i][53]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][51]) - Convert.ToDouble(staticData[i][54]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][52]) - Convert.ToDouble(staticData[i][55]), 2));

            }

            return total / (numRowsStatic - 1);

        }
        private double CalculateLeftThighLength()
        {
            //38-41
            //=SQRT((AM2-AP2)^2+(AN2-AQ2)^2+(AO2-AR2)^2)
            double total = 0;

            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][38]) - Convert.ToDouble(staticData[i][41]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][39]) - Convert.ToDouble(staticData[i][42]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][40]) - Convert.ToDouble(staticData[i][43]), 2));

            }

            return total / (numRowsStatic - 1);

        }
        private double CalculateRightLegLength()
        {
            //56-53
            //=SQRT((BE2-BB2)^2+(BF2-BC2)^2+(BG2-BD2)^2)
            double total = 0;

            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][56]) - Convert.ToDouble(staticData[i][53]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][57]) - Convert.ToDouble(staticData[i][54]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][58]) - Convert.ToDouble(staticData[i][55]), 2));

            }

            return total / (numRowsStatic - 1);

        }
        private double CalculateLeftLegLength()
        {
            //44-41
            //=SQRT((AS2-AP2)^2+(AT2-AQ2)^2+(AU2-AR2)^2)
            double total = 0;

            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][44]) - Convert.ToDouble(staticData[i][41]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][45]) - Convert.ToDouble(staticData[i][42]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][46]) - Convert.ToDouble(staticData[i][43]), 2));

            }

            return total / (numRowsStatic - 1);

        }
        private double CalculateRightFootLength()
        {
            //56-59
            //=SQRT((BE2-BH2)^2+(BF2-BI2)^2+(BG2-BJ2)^2)
            double total = 0;

            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][56]) - Convert.ToDouble(staticData[i][59]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][57]) - Convert.ToDouble(staticData[i][60]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][58]) - Convert.ToDouble(staticData[i][61]), 2));

            }

            return total / (numRowsStatic - 1);

        }
        private double CalculateLeftFootLength()
        {
            //44-47
            //=SQRT((AS2-AV2)^2+(AT2-AW2)^2+(AU2-AX2)^2)
            double total = 0;

            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][44]) - Convert.ToDouble(staticData[i][47]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][45]) - Convert.ToDouble(staticData[i][48]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][46]) - Convert.ToDouble(staticData[i][49]), 2));

            }

            return total / (numRowsStatic - 1); ;

        }
        private double CalculateRightHandTipSize(int start, int end)
        {
            //RightHandTipSize = aj-bt + ak-bu + al-bv
            //35-71, 36-72, 37-73

            double total = 0;
            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][35]) - Convert.ToDouble(staticData[i][71]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][36]) - Convert.ToDouble(staticData[i][72]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][37]) - Convert.ToDouble(staticData[i][73]), 2));

            }

            return total / (numRowsStatic - 1); ;

        }
        private double CalculateLeftHandTipSize(int start, int end)
        {

            double total = 0;
            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][23]) - Convert.ToDouble(staticData[i][65]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][24]) - Convert.ToDouble(staticData[i][66]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][25]) - Convert.ToDouble(staticData[i][67]), 2));

            }

            return total / (numRowsStatic - 1); ;

        }
        private double CalculateRightHandThumbSize(int start, int end)
        {

            double total = 0;
            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][35]) - Convert.ToDouble(staticData[i][74]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][36]) - Convert.ToDouble(staticData[i][75]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][37]) - Convert.ToDouble(staticData[i][76]), 2));

            }

            return total / (numRowsStatic - 1); ;

        }
        private double CalculateLeftHandThumbSize(int start, int end)
        {
            double total = 0;
            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][23]) - Convert.ToDouble(staticData[i][68]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][24]) - Convert.ToDouble(staticData[i][69]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][25]) - Convert.ToDouble(staticData[i][70]), 2));

            }

            return total / (numRowsStatic - 1); ;

        }
        private double CalculateTrunkLength()
        {

            double total = 0;
            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][2]) - Convert.ToDouble(staticData[i][62]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][3]) - Convert.ToDouble(staticData[i][63]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][4]) - Convert.ToDouble(staticData[i][64]), 2));

            }

            return total / (numRowsStatic - 1); ;

        }


        /*******************************
        * MOTION CALCULATIONS
        ******************************/


        /// <summary>
        /// Calculates the spine base x.
        /// </summary>
        /// <returns>double spinebase_x</returns>
        private double CalculateSpineBaseX()
        {
            double max = 0.0000;
            double min = 0.0000;
            //Spine Base X = "<>ToString[Max[ctotal]-Min[ctotal]]
            //ctotal = Table[data[[3 + 77*(i - 1)]], {i, 2, Length[data]/77}];
            //Console.WriteLine("row c = {0}",data[0][2]);
            for (int i = 1; i < numRows; i++)
            {
                if (i == 1)
                {
                    max = Convert.ToDouble(data[i][2]);
                    min = Convert.ToDouble(data[i][2]);
                }
                else
                {
                    if (Convert.ToDouble(data[i][2]) >= max) { max = Convert.ToDouble(data[i][2]); }
                    if (Convert.ToDouble(data[i][2]) <= min) { min = Convert.ToDouble(data[i][2]); }
                }
            }
            Console.Write("spinebase_x = {0}", max-min);
            return (max - min);
        }


        /// <summary>
        /// Calculates the spine base y.
        /// </summary>
        /// <returns>The spine base y.</returns>
        private double CalculateSpineBaseY()
        {
            double max = 0.0000;
            double min = 0.0000;
            //Spine Base Y = "<>ToString[Max[dtotal]-Min[dtotal]]}
            //dtotal = Table[data[[4 + 77*(i - 1)]], {i, 2, Length[data]/77}];
            for (int i = 1; i < numRows; i++)
            {
                if (i == 1)
                {
                    max = Convert.ToDouble(data[i][3]);
                    min = Convert.ToDouble(data[i][3]);
                }
                else
                {
                    if (Convert.ToDouble(data[i][3]) >= max) { max = Convert.ToDouble(data[i][3]); }
                    if (Convert.ToDouble(data[i][3]) <= min) { min = Convert.ToDouble(data[i][3]); }
                }

            }
           //Console.Write("spinebase_y = {0}", max-min);
            return (max - min);
        }

        /// <summary>
        /// Calculates the hip left x.
        /// </summary>
        /// <returns>The hip left x.</returns>
        private double CalculateHipLeftX()
        {
            //"Hip Left X =" <> ToString[Max[amtotal] - Min[amtotal]]
            //am = 39-1
            double max = 0.0000;
            double min = 0.0000;
            for (int i = 1; i < numRows; i++)
            {
                if (i == 1)
                {
                    max = Convert.ToDouble(data[i][38]);
                    min = Convert.ToDouble(data[i][38]);
                }
                else
                {
                    if (Convert.ToDouble(data[i][38]) >= max) { max = Convert.ToDouble(data[i][38]); }
                    if (Convert.ToDouble(data[i][38]) <= min) { min = Convert.ToDouble(data[i][38]); }
                }

            }
            //Console.WriteLine("Hip Left X = {0}", max-min);

            return (max - min);
        }

        /// <summary>
        /// Calculates the hip left y.
        /// </summary>
        /// <returns>The hip left y.</returns>
        private double CalculateHipLeftY()
        {
            //"Hip Left Y =" <>f
            double max = 0.0000;
            double min = 0.0000;
            for (int i = 1; i < numRows; i++)
            {
                if (i == 1)
                {
                    max = Convert.ToDouble(data[i][39]);
                    min = Convert.ToDouble(data[i][39]);
                }
                else
                {
                    if (Convert.ToDouble(data[i][39]) >= max) { max = Convert.ToDouble(data[i][39]); }
                    if (Convert.ToDouble(data[i][39]) <= min) { min = Convert.ToDouble(data[i][39]); }
                }

            }
            //Console.WriteLine("Hip Left Y = {0}", max - min);
            return (max - min);
        }

        /// <summary>
        /// Calculates the hip right x.
        /// </summary>
        /// <returns>The hip right x.</returns>
        private double CalculateHipRightX()
        {
            //Hip Right X = <>ToString[Max[hiprxtotal]-Min[hiprxtotal]]
            //col = 50
            double max = 0.0000;
            double min = 0.0000;
            for (int i = 1; i < numRows; i++)
            {
                if (i == 1)
                {
                    max = Convert.ToDouble(data[i][50]);
                    min = Convert.ToDouble(data[i][50]);
                }
                else
                {
                    if (Convert.ToDouble(data[i][50]) >= max) { max = Convert.ToDouble(data[i][50]); }
                    if (Convert.ToDouble(data[i][50]) <= min) { min = Convert.ToDouble(data[i][50]); }
                }

            }

            //Console.WriteLine("Hip right X = {0}", max - min);

            return (max - min);
        }

        /// <summary>
        /// Calculates the hip right y.
        /// </summary>
        /// <returns>The hip right y.</returns>
        private double CalculateHipRightY()
        {
            //Hip Right Y = "<>ToString[Max[hiprytotal]-Min[hiprytotal]]},
            //col = 51
            double max = 0.0000;
            double min = 0.0000;
            for (int i = 1; i < numRows; i++)
            {
                if (i == 1)
                {
                    max = Convert.ToDouble(data[i][51]);
                    min = Convert.ToDouble(data[i][51]);
                }
                else
                {
                    if (Convert.ToDouble(data[i][51]) >= max) { max = Convert.ToDouble(data[i][51]); }
                    if (Convert.ToDouble(data[i][51]) <= min) { min = Convert.ToDouble(data[i][51]); }
                }

            }

            //Console.WriteLine("Hip right Y = {0}", max - min);
            return (max - min);
        }

        private double CalculateSpineBaseYMaxMin(int start, int end)
        {
            //d max - d min
            double max = 0.0000;
            double min = 0.0000;
            for (int i = start; i < end; i++)
            {
                if (i == start)
                {
                    max = Convert.ToDouble(data[i][3]);
                    min = Convert.ToDouble(data[i][3]);
                }
                else
                {
                    if (Convert.ToDouble(data[i][3]) >= max) { max = Convert.ToDouble(data[i][3]); }
                    if (Convert.ToDouble(data[i][3]) <= min) { min = Convert.ToDouble(data[i][3]); }
                }
            }
            return (max - min);
        }

        private double CalculateHipRightMaxMin(int start, int end)
        {
            //az max-min
            double max = 0.0000;
            double min = 0.0000;
            for (int i = start; i < end; i++)
            {
                if (i == start)
                {
                    max = Convert.ToDouble(data[i][51]);
                    min = Convert.ToDouble(data[i][51]);
                }
                else
                {
                    if (Convert.ToDouble(data[i][51]) >= max) { max = Convert.ToDouble(data[i][51]); }
                    if (Convert.ToDouble(data[i][51]) <= min) { min = Convert.ToDouble(data[i][51]); }
                }

            }
            return (max - min);
        }

        private double CalculateHipLeftMaxMin(int start, int end)
        {
            ////an max - an min
            double max = 0.0000;
            double min = 0.0000;
            for (int i = start; i < end; i++)
            {
                if (i == start)
                {
                    max = Convert.ToDouble(data[i][39]);
                    min = Convert.ToDouble(data[i][39]);
                }
                else
                {
                    if (Convert.ToDouble(data[i][39]) >= max) { max = Convert.ToDouble(data[i][39]); }
                    if (Convert.ToDouble(data[i][39]) <= min) { min = Convert.ToDouble(data[i][39]); }
                }

            }
            return (max - min);
            //return (Convert.ToDouble(data[end][39]) - Convert.ToDouble(data[start][39]));
        }


        private double CalculateKneeLeftDistance(int start, int end)
        {
            //Knee Left Distance = Sqrt[(aptotal[[MAX]] - aptotal[[MIN]])^2 + (aqtotal[[MAX]] - aqtotal[[MIN]])^2 + (artotal[[MAX]] - artotal[[MIN]])^2]
            //aptotal = Table[data[[42 + 77 * (i - 1)]], { i, 2, Length[data] / 77}];
            //aqtotal = Table[data[[43 + 77*(i - 1)]], {i, 2, Length[data]/77}];
            //artotal = Table[data[[44 + 77 * (i - 1)]], { i, 2, Length[data] / 77}];

            Console.WriteLine("start frame = " + start);
            Console.WriteLine("end fram = " + end);
            double apMax = Convert.ToDouble(data[end][41]); //ap = col 42
            double apMin = Convert.ToDouble(data[start][41]);
            double aqMax = Convert.ToDouble(data[end][42]); //aq = col 43
            double aqMin = Convert.ToDouble(data[start][42]);
            double arMax = Convert.ToDouble(data[end][43]); //ar = col 44
            double arMin = Convert.ToDouble(data[start][43]);

            return (Math.Sqrt(
                Math.Pow(apMax - apMin, 2) +
                Math.Pow(aqMax - aqMin, 2) +
                Math.Pow(arMax - arMin, 2)
                        )
                   );

        }

        private double CalculateKneeRightDistance(int start, int end)
        {
            //Knee Right Distance = "<>ToString[Sqrt[(bbtotal[[MAX]] - bbtotal[[MIN]])^2 + (bctotal[[MAX]] - bctotal[[MIN]])^2 + (bdtotal[[MAX]] - bdtotal[[MIN]])^2]]

            double bbMax = Convert.ToDouble(data[end][53]); //ap = col 42
            double bbMin = Convert.ToDouble(data[start][53]);
            double bcMax = Convert.ToDouble(data[end][54]);
            double bcMin = Convert.ToDouble(data[start][54]);
            double bdMax = Convert.ToDouble(data[end][55]);
            double bdMin = Convert.ToDouble(data[start][55]);

            return (Math.Sqrt(Math.Pow(bbMax - bbMin, 2) + Math.Pow(bcMax - bcMin, 2)
                              + Math.Pow(bdMax - bdMin, 2)));
        }


        private double CalculateFootLeftDistance(int start, int end)
        {
            //Foot Left Distance = SqrtBox[betotal[(MAX] - betotal[MIN])^2 + (bftotal[MAX] - bftotal[MIN])^2 + (bgtotal[MAX] - bgtotal[MIN])^2]
            double beMax = Convert.ToDouble(data[end][47]);
            double beMin = Convert.ToDouble(data[start][47]);
            double bfMax = Convert.ToDouble(data[end][48]);
            double bfMin = Convert.ToDouble(data[start][48]);
            double bgMax = Convert.ToDouble(data[end][49]);
            double bgMin = Convert.ToDouble(data[start][49]);


            return (Math.Sqrt(Math.Pow(beMax - beMin, 2) + Math.Pow(bfMax - bfMin, 2) +
                     Math.Pow(bgMax - bgMin, 2)));
        }
        private double CalculateFootRightDistance(int start, int end)
        {
            //Right Distance = Sqrt[(bhtotal[[MAX]] - bhtotal[[MIN]])^2 + bitotal[[MAX]] - bitotal[[MIN]])^2 + (bjtotal[[MAX]] - bjtotal[[MIN]])^2]]

            double bhMax = Convert.ToDouble(data[end][59]);
            double bhMin = Convert.ToDouble(data[start][59]);
            double biMax = Convert.ToDouble(data[end][60]);
            double biMin = Convert.ToDouble(data[start][60]);
            double bjMax = Convert.ToDouble(data[end][61]);
            double bjMin = Convert.ToDouble(data[start][61]);

            //Console.WriteLine("Foot right dist : {0}", (Math.Sqrt(Math.Pow(bhMax - bhMin, 2) + Math.Pow(biMax - biMin, 2)
            //+ Math.Pow(bjMax - bjMin, 2))));

            return (Math.Sqrt(Math.Pow(bhMax - bhMin, 2) + Math.Pow(biMax - biMin, 2)
                              + Math.Pow(bjMax - bjMin, 2)));


        }

        private double CalculatAnkleLeftDistance(int start, int end)
        {

            //Ankle Left Distance = Sqrt(anlxtotal[MAX] - anlxtotal[MIN])^2 + (anlytotal[(MAX] - anlytotal[MIN])^2 + ([anlztotal[MAX] - anlztotal[MIN])^2
            double anlxMax = Convert.ToDouble(data[end][44]);
            double anlxMin = Convert.ToDouble(data[start][44]);
            double anlyMax = Convert.ToDouble(data[end][45]);
            double anlyMin = Convert.ToDouble(data[start][45]);
            double anlzMax = Convert.ToDouble(data[end][46]);
            double anlzMin = Convert.ToDouble(data[start][46]);


            return (Math.Sqrt(Math.Pow(anlxMax - anlxMin, 2) + Math.Pow(anlyMax - anlyMin, 2) + Math.Pow(anlzMax - anlzMin, 2)));
        }


        private double CalculateAnkleRightDistance(int start, int end)
        {         //Ankle Right Distance =Sqrt[(anrxtotal[[MAX]] - anrxtotal[[MIN]])^2 + (anrytotal[[MAX]] - anrytotal[[MIN]])^2 + (anrztotal[[MAX]] - anrztotal[[MIN]])^2]]}

            double ankleXMax = Convert.ToDouble(data[end][56]);
            double ankleXMin = Convert.ToDouble(data[start][56]);
            double ankleYMax = Convert.ToDouble(data[end][57]);
            double ankleYMin = Convert.ToDouble(data[start][57]);
            double ankleZMax = Convert.ToDouble(data[end][58]);
            double ankleZMin = Convert.ToDouble(data[start][58]);

            return Math.Sqrt(Math.Pow(ankleXMax - ankleXMin, 2) + Math.Pow(ankleYMax - ankleYMin, 2) + Math.Pow(ankleZMax - ankleZMin, 2));
        }

        private double CalculateHipLeftDistance(int start, int end)
        {//"Hip Left Distance = Sqrt[(hiplxtotal[[MAX]] - hiplxtotal[[MIN]])^ 2 + (hiplytotal[[MAX]] - hiplytotal[[MIN]])^ 2 + (hiplztotal[[MAX]] - hiplztotal[[MIN]])^ 2]],

            double hipTotalXMax = Convert.ToDouble(data[end][38]);
            double hipTotalXMin = Convert.ToDouble(data[start][38]);
            double hipTotalYMax = Convert.ToDouble(data[end][39]);
            double hipTotalYMin = Convert.ToDouble(data[start][39]);
            double hipTotalZMax = Convert.ToDouble(data[end][40]);
            double hipTotalZMin = Convert.ToDouble(data[start][40]);


            return Math.Sqrt(Math.Pow(hipTotalXMax - hipTotalXMin, 2) + Math.Pow(hipTotalYMax - hipTotalYMin, 2) + Math.Pow(hipTotalZMax - hipTotalZMin, 2));
        }

        private double CalculateHipRightDistance(int start, int end)
        {//Spine Distance = [Sqrt]((spinextotal[[MAX]] - spinextotal[[MIN]])^ 2 + (spineytotal[[MAX]] - spineytotal[[MIN]])^ 2 + (spineztotal[[MAX]] - spineztotal[[MIN]])^ 2)]},


            double XMax = Convert.ToDouble(data[end][50]);
            double XMin = Convert.ToDouble(data[start][50]);
            double YMax = Convert.ToDouble(data[end][51]);
            double YMin = Convert.ToDouble(data[start][51]);
            double ZMax = Convert.ToDouble(data[end][52]);
            double ZMin = Convert.ToDouble(data[start][52]);


            return Math.Sqrt(Math.Pow(XMax - XMin, 2) + Math.Pow(YMax - YMin, 2) + Math.Pow(ZMax - ZMin, 2));
        }

        private double CalculateHipAngle(int frame)
        {


            double hipRightXMax = Convert.ToDouble(data[frame][50]);
            double hipLeftXMax = Convert.ToDouble(data[frame][38]);
            double hipRightYMax = Convert.ToDouble(data[frame][51]);
            double hipLeftYMax = Convert.ToDouble(data[frame][39]);
            double hipRightZMax = Convert.ToDouble(data[frame][52]);
            double hipLeftZMax = Convert.ToDouble(data[frame][40]);


            //Hip Angle at Max = [180/[Pi]ArcCos[(hiprxtotal[[MAX]]-hiplxtotal[[MAX]])/[Sqrt]((hiprxtotal[[MAX]]-hiplxtotal[[MAX]])^2+(hiprytotal[[MAX]]-hiplytotal[[MAX]])^2+(hiprztotal[[MAX]]-hiplztotal[[MAX]])^2))]]},
            return ((180 / Math.PI) * Math.Acos((hipRightXMax - hipLeftXMax) / (Math.Sqrt(Math.Pow(hipRightXMax - hipLeftXMax, 2) + Math.Pow(hipRightYMax - hipLeftYMax, 2)
                                                                                    + Math.Pow(hipRightZMax - hipLeftZMax, 2)))));
        }
        private double CalculateHipAngleAtMin(int start, int end)
        {

            //	"Hip Angle at Min = [180 /\[Pi] ArcCos[(hiprxtotal[[MIN]] - hiplxtotal[[MIN]])///Sqrt[(hiprxtotal[[MIN]] -//hiplxtotal[[MIN]])^ 2 + (hiprytotal[[MIN]] -
            //hiplytotal[[MIN]])^ 2 + (hiprztotal[[MIN]] - //hiplztotal[[MIN]])^ 2]]]
            double hipRightXMin = Convert.ToDouble(data[start][50]);
            double hipLeftXMin = Convert.ToDouble(data[start][38]);
            double hipRightYMin = Convert.ToDouble(data[start][51]);
            double hipLeftYMin = Convert.ToDouble(data[start][39]);
            double hipRightZMin = Convert.ToDouble(data[start][52]);
            double hipLeftZMin = Convert.ToDouble(data[start][40]);

            return ((180 / Math.PI) * Math.Acos((hipRightXMin - hipLeftXMin) / Math.Sqrt(Math.Pow(hipRightXMin - hipLeftXMin, 2) + Math.Pow(hipRightYMin - hipLeftYMin, 2)
                                                                                   + Math.Pow(hipRightZMin - hipLeftZMin, 2))));
        }
        private double CalculateShoulderAngle(int frame)
        {

            //Shoulder Angle at Max =[180/\[Pi]ArcCos[(srxtotal[[MAX]] - //slxtotal[[MAX]])/Sqrt[(srxtotal[[MAX]] - slxtotal[[MAX]])^2 + \//(srytotal[[MAX]] - 
            //slytotal[[MAX]])^2 + (srztotal[[MAX]] - \//slztotal[[MAX]])^2]
            double shoulderRightXMax = Convert.ToDouble(data[frame][26]);
            double shoulderLeftXMax = Convert.ToDouble(data[frame][14]);
            double shoulderRightYMax = Convert.ToDouble(data[frame][27]);
            double shoulderLeftYMax = Convert.ToDouble(data[frame][15]);
            double shoulderRightZMax = Convert.ToDouble(data[frame][28]);
            double shoulderLeftZMax = Convert.ToDouble(data[frame][16]);


            return ((180 / Math.PI) * Math.Acos((shoulderRightXMax - shoulderLeftXMax) / Math.Sqrt(Math.Pow(shoulderRightXMax - shoulderLeftXMax, 2) + Math.Pow(shoulderRightYMax - shoulderLeftYMax, 2)
                                                                                       + Math.Pow(shoulderRightZMax - shoulderLeftZMax, 2))));
        }
        private double CalculateShoulderAngleAtMin(int start, int end)
        {


            //Shoulder Angle at Min = [\!\(\*FractionBox[\(180\), \//\(\[Pi]\)]\)ArcCos[\!\(\*FractionBox[\(srxtotal[\([\)\(MIN\)\(]\)] - \
            //slxtotal[\([\)\(MIN\)\(]\)]\), \//SqrtBox[\(\*SuperscriptBox[\((srxtotal[\([\)\(MIN\)\(]\)] - slxtotal[\
            //\([\)\(MIN\)\(]\)])\), \(2\)] + \//\*SuperscriptBox[\((srytotal[\([\)\(MIN\)\(]\)] - slytotal[\([\)\(MIN\
            //\)\(]\)])\), \(2\)] + \*SuperscriptBox[\((srztotal[\([\)\(MIN\)\(]\)] \//- slztotal[\([\)\(MIN\)\(]\)])\), \(2\)]\)]]\)]],\[IndentingNewLine]" \
            double shoulderRightXMin = Convert.ToDouble(data[start][26]);
            double shoulderLeftXMin = Convert.ToDouble(data[start][14]);
            double shoulderRightYMin = Convert.ToDouble(data[start][27]);
            double shoulderLeftYMin = Convert.ToDouble(data[start][15]);
            double shoulderRightZMin = Convert.ToDouble(data[start][28]);
            double shoulderLeftZMin = Convert.ToDouble(data[start][16]);


            return ((180 / Math.PI) * Math.Acos((shoulderRightXMin - shoulderLeftXMin)
                                                / Math.Sqrt(Math.Pow(shoulderRightXMin - shoulderLeftXMin, 2)
                                                            + Math.Pow(shoulderRightYMin - shoulderLeftYMin, 2)
                                                                                       + Math.Pow(shoulderRightZMin - shoulderLeftZMin, 2))));
        }

        private double CalculateSpineDistance(int start, int end)
        {
            //	"Spine Distance = " <>
            //ToString[\[Sqrt]((spinextotal[[MAX]] -
            //spinextotal[[MIN]])^ 2 + (spineytotal[[MAX]] -
            //spineytotal[[MIN]])^ 2 + (spineztotal[[MAX]] -
            //spineztotal[[MIN]])^ 2)]},

            //spinextotal = Table[data[[3 + 77 * (i - 1)]], { i, 2, Length[data] / 77}];
            //spineytotal = Table[data[[4 + 77 * (i - 1)]], { i, 2, Length[data] / 77}];
            //spineztotal = Table[data[[5 + 77 * (i - 1)]], { i, 2, Length[data] / 77}];

            double spineXMin = Convert.ToDouble(data[start][2]);
            double spineXMax = Convert.ToDouble(data[end][2]);
            double spineYMin = Convert.ToDouble(data[start][3]);
            double spineYMax = Convert.ToDouble(data[end][3]);
            double spineZMin = Convert.ToDouble(data[start][4]);
            double spineZMax = Convert.ToDouble(data[end][4]);


            return Math.Sqrt(Math.Pow(spineXMax - spineXMin, 2) +
                             Math.Pow(spineYMax - spineYMin, 2) +
                             Math.Pow(spineZMax - spineZMin, 2));
        }

        /**************************************************************
        * MANUAL TAI CALCULATIONS
        **************************************************************/

        double PredictedValue(double score)
        {
            double predictedValue = (Math.Pow(2.718281828, score) / (1 + Math.Pow(2.718281828, score)));
            return predictedValue;
        }

        void TAICalculations()
        {

            TAI1_1 = Calculate1_1();
            TAI1_2 = Calculate1_2();
            TAI1_3 = Calculate1_3();
            TAI1_6 = Calculate1_6();
            TAI1_7 = Calculate1_7();

            calculatedTAI1_1 = Calculate1_1Calculated();
            calculatedTAI1_2 = Calculate1_2Calculated();
            calculatedTAI1_3 = Calculate1_3Calculated();
            calculatedTAI1_6 = Calculate1_6Calculated();
            calculatedTAI1_7 = Calculate1_7Calculated();

        }

        public void OutputToTxt()
        {
            OutputTAI();
            OutputVariables();
            OutputJointDistance();
            OutputComparison();
        }


        private int Calculate1_1()
        {
            //CHANGE VARIABLES HERE
            double Height_cm_1_var = 0.15382898;
            double R_Upper_Arm_Length_13_var = -0.63851325;
            double L_Upper_Arm_Length_14_var = 0.90774276;
            double R_Forearm_Length_15_var = -0.39967018;
            double L_Forearm_Length_16_var = -0.08388424;
            double Chest_Circumference_17_var = 0.11348068;
            double R_Thigh_Length_20_var = -0.20833378;
            double L_Thigh_Length_21_var = 0.08918904;
            double R_Leg_Length_22_var = -0.42055221;
            double L_Leg_Length_23_var = 0.4231421;
            double R_Foot_Length_24_var = 0.60398815;
            double L_Foot_Length_25_var = -0.87942671;
            double Dx_SpineBase_var = -0.0232257;
            double Dx_HipLeft_var = 0.00360027;
            double Dx_HipRightVar = 0.01617653;
            double constant = -20.91526416;


            double score = (Height_cm_1_var * height)
                + (R_Upper_Arm_Length_13_var * manualRUA_Length)
                + (L_Upper_Arm_Length_14_var * manualLUA_Length)
                + (R_Forearm_Length_15_var * manualR_Forearm_Length)
                + (L_Forearm_Length_16_var * manualL_Forearm_Length)
                + (Chest_Circumference_17_var * manualChestCircumference)
                + (R_Thigh_Length_20_var * manualR_Thigh_Length)
                + (L_Thigh_Length_21_var * manualL_Thigh_Length)
                + (R_Leg_Length_22_var * manualR_Leg_Length)
                + (L_Leg_Length_23_var * manualL_Leg_Length)
                + (R_Foot_Length_24_var * manualR_Foot_Length)
                + (L_Foot_Length_25_var * manualL_Foot_Length)
                + (spineBase_x * Dx_SpineBase_var)
                + (hipLeft_x * Dx_HipLeft_var)
                + (hipRight_x * Dx_HipRightVar)
                + (constant);

            double predicted = PredictedValue(score);

            if (score >= CutValue1_1)
            {
                return 1;
            }
            else return 0;
        }

        private int Calculate1_2()
        {
            double mass_kg_0_var = -0.04928772;
            double height_cm_1_var = 0.26648226;
            double R_Upper_Arm_Length_13_var = 0.88849528;
            double L_Upper_Arm_Length_14_var = -1.8446127;
            double R_Forearm_Length_15_var = 0.6526423;
            double L_Forearm_Length_16_var = -0.155697;
            double chest_Circumference_17_var = -0.02023203;
            double waist_Circumference_18_var = 0.12776029;
            double trunk_Length_19_var = 0.04646773;
            double R_Thigh_Length_20_var = 0.02227374;
            double L_Thigh_Length_21_var = -0.36827157;
            double R_Leg_Length_22_var = 0.81850244;
            double L_Leg_Length_23_var = -0.70104958;
            double R_Foot_Length_24_var = -0.33555256;
            double L_Foot_Length_25_var = -0.71928095;
            double wheelchair_Height_26_var = -0.59641335;
            double hipAngleStart_setting_var = -0.04608105;
            double shoulderAngleStart_setting_var = 0.41847492;
            double hipAngleatEnd_setting_var = 0.03173129;
            double shoulderAngleatEnd_setting_var = 0.07229982;
            double constant = 27.38307299;



            double score = (mass_kg_0_var * mass) +
                (height_cm_1_var * height) +
                (R_Upper_Arm_Length_13_var * manualRUA_Length) +
                (L_Upper_Arm_Length_14_var * manualLUA_Length) +
                (R_Forearm_Length_15_var * manualR_Forearm_Length) +
                (L_Forearm_Length_16_var * manualL_Forearm_Length) +
                (chest_Circumference_17_var * manualChestCircumference) +
                (waist_Circumference_18_var * manualWaistCircumference) +
                (trunk_Length_19_var * manualTrunkLength) +
                (R_Thigh_Length_20_var * manualR_Thigh_Length) +
                (L_Thigh_Length_21_var * manualL_Thigh_Length) +
                (R_Leg_Length_22_var * manualR_Leg_Length) +
                (L_Leg_Length_23_var * manualL_Leg_Length) +
                (R_Foot_Length_24_var * manualR_Foot_Length) +
                (L_Foot_Length_25_var * manualL_Foot_Length) +
                (wheelchair_Height_26_var * manualWheelchairHeight) +
                (hipAngleStart_setting_var * hipAngleStart_setting) +
                (shoulderAngleStart_setting_var * shoulderAngleStart_setting) +
                (hipAngleatEnd_setting_var * hipAngleatEnd_setting) +
                (shoulderAngleatEnd_setting_var * shoulderAngleatEnd_setting) +
                constant;

            double predicted = PredictedValue(score);

            if (predicted >= CutValue1_2)
            {
                return 1;
            }
            else return 0;
        }

        private int Calculate1_3()
        {

            double Height_cm_1_var = -0.18696058;
            double R_Upper_Arm_Length_13_var = 0.78834246;
            double L_Upper_Arm_Length_14_var = -0.79709102;
            double R_Forearm_Length_15_var = 0.15184063;
            double L_Forearm_Length_16_var = 0.113007;
            double Trunk_Length_19_var = -0.00213037;

            double R_Thigh_Length_20_var = -0.13713118;
            double L_Thigh_Length_21_var = 0.10147534;
            double R_Leg_Length_22_var = 0.14925063;
            double L_Leg_Length_23_var = 0.3111351;
            double R_Foot_Length_24_var = 0.27375557;
            double L_Foot_Length_25_var = 0.14546865;
            double Wheelchair_Height_26_var = 0.09157182;
            double SpineBaseYMax_Min_transfer = 0.02614024;
            double HipRightYMax_Min_transfer = -0.03141481;
            double HipLeftYMax_Min_transfer = 0.00314057;
            double Constant = -3.4057322;

            double score = (Height_cm_1_var * height) +
                (R_Upper_Arm_Length_13_var * manualRUA_Length) +
                (L_Upper_Arm_Length_14_var * manualLUA_Length) +
                (R_Forearm_Length_15_var * manualR_Forearm_Length) +
                (L_Forearm_Length_16_var * manualL_Forearm_Length) +
                (Trunk_Length_19_var * manualTrunkLength) +
                (R_Thigh_Length_20_var * manualR_Thigh_Length) +
                (L_Thigh_Length_21_var * manualL_Thigh_Length) +
                (R_Leg_Length_22_var * manualR_Leg_Length) +
                (L_Leg_Length_23_var * manualL_Leg_Length) +
                (R_Foot_Length_24_var * manualR_Foot_Length) +
                (L_Foot_Length_25_var * manualL_Foot_Length) +
                (Wheelchair_Height_26_var * manualWheelchairHeight) +
                (SpineBaseYMax_Min_transfer * spineBase_y_maxmin_transfer) +
                (HipRightYMax_Min_transfer * hipRight_maxmin_transfer) +
                (HipLeftYMax_Min_transfer * hipLeft_maxmin_transfer) +
                (Constant);

            double predicted = PredictedValue(score);

            if (predicted > CutValue1_3)
            {
                return 1;
            }
            else return 0;
        }


        private int Calculate1_6()
        {
            double R_Thigh_Length_20_var = -0.10829751;
            double L_Thigh_Length_21_var = 0.12033438;
            double R_Leg_Length_22_var = 0.37786696;
            double L_Leg_Length_23_var = -0.20195438;
            double R_Foot_Length_24_var = 0.36607806;
            double L_Foot_Length_25_var = -0.74628473;
            double wheelchair_Height_26_var = 0.04876667;
            double kneeLeftDistance_setting_var = 0.00194906;
            double footLeftDistance_setting_var = 0.00280199;
            double ankleLeftDistance_setting_var = 0.00336979;
            double hipLeftDistance_setting_var = -0.00613064;
            double kneeRightDistance_setting_var = 0.00295072;
            double footRightDistance_setting_var = 0.00575878;
            double ankleRightDistance_setting_var = -0.0036614;
            double hipRightDistance_setting_var = 0.00037899;
            double constant = -3.42103519;

            double score = (kneeLeftDistance_setting_var * kneeLeftDistanceSetting) +
                (footLeftDistance_setting_var * footLeftDistanceSetting) +
                (ankleLeftDistance_setting_var * ankleLeftDistanceSetting) +
                (hipLeftDistance_setting_var * hipLeftDistanceSetting) +
                (kneeRightDistance_setting_var * kneeRightDistanceSetting) +
                (footRightDistance_setting_var * footRightDistanceSetting) +
                (ankleRightDistance_setting_var * ankleRightDistanceSetting) +
                (hipRightDistance_setting_var * hipRightDistanceSetting) +
                (R_Thigh_Length_20_var * manualR_Thigh_Length) +
                (L_Thigh_Length_21_var * manualL_Thigh_Length) +
                (R_Leg_Length_22_var * manualR_Leg_Length) +
                (L_Leg_Length_23_var * manualL_Leg_Length) +
                (R_Foot_Length_24_var * manualR_Foot_Length) +
                (L_Foot_Length_25_var * manualL_Foot_Length) +
                (wheelchair_Height_26_var * manualWheelchairHeight) +
                (constant);

            double predicted = PredictedValue(score);

            if (predicted >= CutValue1_6)
            {
                return 1;
            }
            else return 0;
        }

        private int Calculate1_7()
        {
            double mass_cm_var = 0.03076941;
            double height_cm_1_var = 0.32912701;
            double Waist_Circumference_18 = -0.03226058;
            double R_Thigh_Length_20_var = -0.52102881;
            double L_Thigh_Length_21_var = 0.2968281;
            double R_Leg_Length_22_var = 0.09914581;
            double L_Leg_Length_23_var = -0.93442214;
            double R_Foot_Length_24_var = 0.47831851;
            double L_Foot_Length_25_var = -0.8041212;
            double Wheelchair_Height_26 = -0.11026051;
            double HipLeftDistance_setting_var = 0.08220832;
            double SpineDistance_setting_var = -0.16043269;
            double HipRightDistance_setting_var = 0.09495352;
            double constant = 2.58767077;

            double score = (mass_cm_var * mass)
                + (height_cm_1_var * height)
                + (Waist_Circumference_18 * manualWaistCircumference)
                + (R_Thigh_Length_20_var * manualR_Thigh_Length)
                + (L_Thigh_Length_21_var * manualL_Thigh_Length)
                + (R_Leg_Length_22_var * manualR_Leg_Length)
                + (L_Leg_Length_23_var * manualL_Leg_Length)
                + (R_Foot_Length_24_var * manualR_Foot_Length)
                + (L_Foot_Length_25_var * manualL_Foot_Length)
                + (Wheelchair_Height_26 * manualWheelchairHeight)
                + (HipLeftDistance_setting_var * hipLeftDistanceSetting)
                + (SpineDistance_setting_var * spineDistanceSetting)
                + (HipRightDistance_setting_var * hipRightDistanceSetting)
                + (constant);

            double predicted = PredictedValue(score);

            if (predicted <= CutValue1_7)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        /**************************
         * CALCULATED TAI USING CALCULATIONS
         **************************/


        private int Calculate1_1Calculated()
        {
            //CHANGE VARIABLES HERE
            double R_UA_K5 = 0.24063169;
            double L_UA_K6 = -0.12539795;
            double R_FA_K7 = 0.06301006;
            double L_FA_K8 = -0.06148205;
            double Shoulderx2_K9 = 0.00624972;
            double Hipx2_K10 = 0.0028934;
            double R_TH_K11 = -0.0021076;
            double L_TH_K12 = -0.04124432;
            double R_LG_K13 = -0.02988738;
            double L_LG_K14 = -0.02562975;
            double R_FT_K15 = -0.03782619;
            double L_FT_K16 = 0.02196743;
            double Trunk_K17 = -0.01320004;
            double Dx_SpineBase = 0.07326064;
            double Dx_HipLeft = -0.06208951;
            double Dx_HipRight = -0.02078857;
            double constant = 21.66142096;


            double score = (R_UA_K5 * rightUpperArmLength)
                + (L_UA_K6 * leftUpperArmLength)
                + (R_FA_K7 * rightForearmLength)
                + (L_FA_K8 * leftForearmLength)
                + (Shoulderx2_K9 * shoulderLength)
                + (Hipx2_K10 * hipLength)
                + (R_TH_K11 * rightThighLength)
                + (L_TH_K12 * leftThighLength)
                + (R_LG_K13 * rightLegLength)
                + (L_LG_K14 * leftLegLength)
                + (R_FT_K15 * rightFootLength)
                + (L_FT_K16 * leftFootLength)
                + (Trunk_K17 * trunkLength)
                + (Dx_SpineBase * spineBase_x)
                + (hipLeft_x * Dx_HipLeft)
                + (hipRight_x * Dx_HipRight)
                + (constant);

            double predicted = PredictedValue(score);

            if (predicted >= CutValue1_1Calculated)
            {
                return 1;
            }
            else return 0;
        }

        private int Calculate1_2Calculated()
        {
            double Shoulderx2_K9 = -0.0038128;
            double Hipx2_K10 = -0.01394787;
            double Trunk_K17 = -0.01505327;
            double HipAngleStart_setting = 0.022669;
            double ShoulderAngleStart_setting = 0.22414344;
            double ShoulderAngleatEnd_setting = 0.08751414;
            double SpineBaseYMax_Min_transfer = 0.00984001;
            double Constant = 9.54620554;

            double score = (Shoulderx2_K9 * shoulderLength)
                + (Hipx2_K10 * hipLength)
                + (Trunk_K17 * trunkLength)
                + (HipAngleStart_setting * hipAngleStart_setting)
                + (ShoulderAngleStart_setting * shoulderAngleStart_setting)
                + (ShoulderAngleatEnd_setting * shoulderAngleatEnd_setting)
                + (SpineBaseYMax_Min_transfer * spineBase_y_maxmin_transfer)
                + (Constant);

            double predicted = PredictedValue(score);

            if (predicted >= CutValue1_2Calculated)
            {
                return 1;
            }
            else return 0;
        }

        private int Calculate1_3Calculated()
        {
            double Trunk_K17 = 0.0069863;
            double R_TH_K11 = 0.0345286;
            double L_TH_K12 = -0.06233773;
            double R_LG_K13 = -0.00481606;
            double L_LG_K14 = -0.00347575;
            double R_FT_K15 = 0.03923893;
            double L_FT_K16 = 0.00376211;
            double SpineBaseYMax_Min_transfer = 0.02698458;
            double HipRightYMax_Min_transfer = -0.03206215;
            double HipLeftYMax_Min_transfer = -0.00074859;
            double constant = 8.41452987;

            double score = (Trunk_K17 * trunkLength)
                + (R_TH_K11 * rightThighLength)
                + (L_TH_K12 * leftThighLength)
                + (R_LG_K13 * rightLegLength)
                + (L_LG_K14 * leftLegLength)
                + (R_FT_K15 * rightFootLength)
                + (L_FT_K16 * leftFootLength)
                + (SpineBaseYMax_Min_transfer * spineBase_y_maxmin_transfer)
                + (HipRightYMax_Min_transfer * hipRight_maxmin_transfer)
                + (HipLeftYMax_Min_transfer * hipLeft_maxmin_transfer)
                + (constant);

            double predicted = PredictedValue(score);

            //Console.WriteLine("TAI1_3:");
            //Console.WriteLine(Trunk_K17 +"*"+ trunkLength);
            //Console.WriteLine(R_TH_K11 + "*" + rightThighLength);
            //Console.WriteLine(L_TH_K12 + "*" + leftThighLength);
            //Console.WriteLine(R_LG_K13 + "*" + rightLegLength);
            //Console.WriteLine(L_LG_K14 + "*" + leftLegLength);
            //Console.WriteLine(R_FT_K15 + "*" + rightFootLength);
            //Console.WriteLine(L_FT_K16 + "*" + leftFootLength);
            //Console.WriteLine(SpineBaseYMax_Min_transfer + "*" + spineBase_y_maxmin_transfer);
            //Console.WriteLine(HipRightYMax_Min_transfer + "*" + hipRight_maxmin_transfer);
            //Console.WriteLine(HipLeftYMax_Min_transfer + "*" + hipLeft_maxmin_transfer);
            //Console.WriteLine(constant);
            //Console.WriteLine("1_3 = " +score);

            if (predicted > CutValue1_3Calculated)
            {
                return 1;
            }
            else return 0;
        }


        private int Calculate1_6Calculated()
        {
            double R_TH_K11 = 0.0059389;
            double L_TH_K12 = -0.00894108;
            double R_LG_K13 = -0.00033082;
            double L_LG_K14 = -0.00136794;
            double R_FT_K15 = 0.01426417;
            double L_FT_K16 = 0.00047937;
            double KneeLeftDistance_setting = 0.00001499;
            double FootLeftDistance_setting = -0.00086909;
            double AnkleLeftDistance_setting = 0.0031352;
            double KneeRightDistance_setting = 0.00047609;
            double FootRightDistance_setting = 0.00591098;
            double AnkleRightDistance_setting = -0.00270803;
            double constant = -0.15026301;


            double score = (R_TH_K11 * rightThighLength)
                + (L_TH_K12 * leftThighLength)
                + (R_LG_K13 * rightLegLength)
                + (L_LG_K14 * leftLegLength)
                + (R_FT_K15 * rightFootLength)
                + (L_FT_K16 * leftFootLength)
                + (KneeLeftDistance_setting * kneeRightDistanceSetting)
                + (FootLeftDistance_setting * footLeftDistanceSetting)
                + (AnkleLeftDistance_setting * ankleLeftDistanceSetting)
                + (KneeRightDistance_setting * kneeRightDistanceSetting)
                + (FootRightDistance_setting * footRightDistanceSetting)
                + (AnkleRightDistance_setting * ankleRightDistanceSetting)
                + (constant);

            double predicted = PredictedValue(score);

            //         Console.WriteLine("TAI1_6:");
            //         Console.WriteLine(R_TH_K11 + "*" + rightThighLength);
            //Console.WriteLine(L_TH_K12 + "*" + leftThighLength);
            //Console.WriteLine(R_LG_K13 + "*" + rightLegLength);
            //Console.WriteLine(L_LG_K14 + "*" + leftLegLength);
            //Console.WriteLine(R_FT_K15 + "*" + rightFootLength);
            //Console.WriteLine(L_FT_K16 + "*" + leftFootLength);
            //Console.WriteLine(KneeLeftDistance_setting + "*" + kneeRightDistanceSetting);
            //Console.WriteLine(FootLeftDistance_setting + "*" + footLeftDistanceSetting);
            //Console.WriteLine(AnkleLeftDistance_setting + "*" + ankleLeftDistanceSetting);
            //Console.WriteLine(KneeRightDistance_setting + "*" + kneeRightDistanceSetting);
            //Console.WriteLine(FootRightDistance_setting + "*" + footRightDistanceSetting);
            //Console.WriteLine(AnkleRightDistance_setting + "*" + ankleRightDistanceSetting);
            //Console.WriteLine(constant);
            //Console.WriteLine("1_6 = " + score);

            if (predicted >= CutValue1_6Calculated)
            {
                return 1;
            }
            else return 0;
        }

        private int Calculate1_7Calculated()
        {
            double R_TH_K11 = 0.008796;
            double L_TH_K12 = -0.03443874;
            double R_LG_K13 = 0.01023397;
            double L_LG_K14 = -0.01109249;
            double R_FT_K15 = 0.08846907;
            double L_FT_K16 = 0.01033782;
            double Trunk_K17 = -0.02073699;
            double HipLeftDistance_setting = 0.07799101;
            double SpineDistance_setting = -0.16977133;
            double HipRightDistance_setting = 0.09750411;
            double Constant = 9.86741084;

            double score = (R_TH_K11 * rightThighLength)
                + (L_TH_K12 * leftThighLength)
                + (R_LG_K13 * rightLegLength)
                + (L_LG_K14 * leftLegLength)
                + (R_FT_K15 * rightFootLength)
                + (L_FT_K16 * leftFootLength)
                + (Trunk_K17 * trunkLength)
                + (HipLeftDistance_setting * hipLeftDistanceSetting)
                + (SpineDistance_setting * spineDistanceSetting)
                + (HipRightDistance_setting * hipRightDistanceSetting)
                + (Constant);

            double predicted = PredictedValue(score);

            //Console.WriteLine("TAI1_7:");
            //Console.WriteLine(R_TH_K11 +"*"+ rightThighLength);
            //Console.WriteLine(L_TH_K12 + "*" + leftThighLength);
            //Console.WriteLine(R_LG_K13 + "*" + rightLegLength);
            //Console.WriteLine(L_LG_K14 + "*" + leftLegLength);
            //Console.WriteLine(R_FT_K15 + "*" + rightFootLength);
            //Console.WriteLine(L_FT_K16 + "*" + leftFootLength);
            //Console.WriteLine(Trunk_K17 + "*" + trunkLength);
            //Console.WriteLine(HipLeftDistance_setting + "*" + hipLeftDistanceSetting);
            //Console.WriteLine(SpineDistance_setting + "*" + spineDistanceSetting);
            //Console.WriteLine(HipRightDistance_setting + "*" + hipRightDistanceSetting);
            //Console.WriteLine(Constant);
            //Console.WriteLine("1_7 = " + score);

            if (predicted <= CutValue1_7Calculated)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }




        /**********************************
         * OUTPUTS
         * ********************************/


        /// <summary>
        /// Outputs file named OutputTAI.csv, containing TAI scores for each subject
        /// </summary>
        public void OutputTAI()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(TAIOutputFile, true);
            //TODO: put star if something is wrong


            file.WriteLine("{0},{1},{2},{3},{4},{5},{6}",
                           subjectNum, testNum, TAI1_1,
                           TAI1_2, TAI1_3, TAI1_6, TAI1_7);
            file.Close();

        }

        /// <summary>
        /// Outputs JointDistance.csv
        /// </summary>
        public void OutputJointDistance()
        {

            if (Convert.ToInt32(testNum) == 1)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(distanceOutputFile, true);

                file.WriteLine("{0},{1},{2},{3},{4}," +
                               "{5},{6},{7},{8},{9}," +
                               "{10},{11},{12},{13},{14}," +
                               "{15},{16},{17}",
                               subjectNum, rightHandTipSize, leftHandTipSize, rightHandThumbSize,
                               leftHandThumbSize, rightUpperArmLength, leftUpperArmLength, rightForearmLength, leftForearmLength,
                               shoulderLength, hipLength, rightThighLength, leftThighLength, rightLegLength,
                               leftLegLength, rightFootLength, leftFootLength, trunkLength);
                file.Close();
            }
        }

        /// <summary>
        /// Outputs the Variables.csv
        /// </summary>
        public void OutputVariables()
        {

            System.IO.StreamWriter file = new System.IO.StreamWriter(variableOutputFile, true);
            file.WriteLine("{0},{1},{2},{3},{4}," +
                           "{5},{6},{7},{8},{9}," +
                           "{10},{11},{12},{13},{14}," +
                           "{15},{16},{17},{18},{19}," +
                           "{20},{21},{22},{23}",
                           subjectNum, testNum, spineBase_x, hipLeft_x, hipRight_x, spineBase_y_trail, hipLeft_y_trail, hipRight_y_trail, kneeLeftDistanceSetting, footLeftDistanceSetting,
                           ankleLeftDistanceSetting, hipLeftDistanceSetting, spineDistanceSetting, hipAngleStart_setting, shoulderAngleStart_setting,
                           kneeRightDistanceSetting, footRightDistanceSetting, ankleRightDistanceSetting, hipRightDistanceSetting, hipAngleatEnd_setting,
                           shoulderAngleatEnd_setting, spineBase_y_maxmin_transfer, hipRight_maxmin_transfer, hipLeft_maxmin_transfer);

            file.Close();
        }

        /// <summary>
        /// Outputs the comparison file. Mostyl for testing purposes
        /// </summary>
		public void OutputComparison()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(comparisonOutputFile, true);
            file.WriteLine("{0},{1},{2},{3},{4}," +
                           "{5},{6},{7},{8},{9}," +
                           "{10},{11}",
                           subjectNum, testNum, TAI1_1, calculatedTAI1_1, TAI1_2, calculatedTAI1_2, TAI1_3, calculatedTAI1_3, TAI1_6, calculatedTAI1_6,
                          TAI1_7, calculatedTAI1_7);

            file.Close();
        }

        private double CalculateStartFrame(string[][] data)
        {

            //filter -data
            //get maxima and its location
            //do huge if statement


            doubleData = ArrayToDouble(data);

            double[] negDoubleData = new double[doubleData.Length];
            

            for (int i = 0; i < doubleData.Length; i++)
            {
                negDoubleData[i] = (doubleData[i] * -1);
            }


            double[] spineDiff = new double[doubleData.Length];
            for (int i = 1; i < doubleData.Length - 1; i++)
            {
                spineDiff[i - 1] = ((doubleData[i] * -1) - (doubleData[i - 1] * -1));
            }

            //foreach (double value in doubleData)
            //{
            //    Console.WriteLine("DD : " + value);
            //}
            

            double[] temp = new double[doubleData.Length];
            for(int i = 0; i < doubleData.Length; i++)
            {
                temp[i] = doubleData[i];
            }

            //This was changing the output of doubleData for some reason
            // thats why it is sent a temp copy
            double cutoffFreq = .65;
            double deltaTimeInSec = .03;
            double[] filteredData = Butterworth(negDoubleData, deltaTimeInSec, cutoffFreq);



            double maxima = filteredData.Max();
            int maxIndex = filteredData.ToList().IndexOf(maxima);
            if (subjectNum == "01" & testNum == "01")
            {
                System.IO.StreamWriter file2 = new System.IO.StreamWriter(filterOutputFile, true);

                for (int i = 0; i < numRows; i++)
                {
                    file2.WriteLine(filteredData[i]);
                }

                file2.Close();
            }






            //locs: 210
            //locs: 242
            //pks1: 340.421
            //pks1: 266.141
            //Console.WriteLine("locs = " + maxIndex);
            //Console.WriteLine("pks = " + maxima);
            //foreach (double value in spineDiff)
            //{
            //    Console.WriteLine("SD : " + value);
            //}

            int a =1;
            int start = 0;
            for(int c = 0; c < maxIndex; c++)
            {
                try
                {
                    if (Math.Abs(spineDiff[c]) < 10)
                    {
                        if (Math.Abs(spineDiff[c + 1]) < 10)
                        {
                            if (Math.Abs(spineDiff[c + 2]) < 10)
                            {
                                if (Math.Abs(spineDiff[c + 3]) < 10)
                                {
                                    if (Math.Abs(spineDiff[c + 4]) < 10)
                                    {
                                        if (Math.Abs(spineDiff[c + 5]) < 10)
                                        {
                                            if (doubleData[c + 4] > -50)
                                            {
                                                start = c + 6;
                                                a++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch(Exception e)
                {

                }
               

            }

            int startPoint = start;
            int endPoint = maxIndex;

            System.IO.StreamWriter file = new System.IO.StreamWriter(frameDataFile, true);
            file.WriteLine(subjectNum + " " + testNum +
                " " + startPoint + " " + endPoint);
            file.Close();


            




            return 0;
        }
        private double CalculateEndFrame()
        {

            return 0;
        }



        //--------------------------------------------------------------------------
        // This function returns the data filtered. Converted to C# 2 July 2014.
        // Original source written in VBA for Microsoft Excel, 2000 by Sam Van
        // Wassenbergh (University of Antwerp), 6 june 2007.
        //--------------------------------------------------------------------------
        public static double[] Butterworth(double[] indata, double deltaTimeinsec, double CutOff)
        {
            if (indata == null) return null;
            if (CutOff == 0) return indata;

            double Samplingrate = 1 / deltaTimeinsec;
            long dF2 = indata.Length - 1;        // The data range is set with dF2
            double[] Dat2 = new double[dF2 + 4]; // Array with 4 extra points front and back
            double[] data = indata; // Ptr., changes passed data

            // Copy indata to Dat2
            for (long r = 0; r < dF2; r++)
            {
                Dat2[2 + r] = indata[r];
            }
            Dat2[1] = Dat2[0] = indata[0];
            Dat2[dF2 + 3] = Dat2[dF2 + 2] = indata[dF2];

            const double pi = 3.14159265358979;
            double wc = Math.Tan(CutOff * pi / Samplingrate);
            double k1 = 1.414213562 * wc; // Sqrt(2) * wc
            double k2 = wc * wc;
            double a = k2 / (1 + k1 + k2);
            double b = 2 * a;
            double c = a;
            double k3 = b / k2;
            double d = -2 * a + k3;
            double e = 1 - (2 * a) - k3;

            // RECURSIVE TRIGGERS - ENABLE filter is performed (first, last points constant)
            double[] DatYt = new double[dF2 + 4];
            DatYt[1] = DatYt[0] = indata[0];
            for (long s = 2; s < dF2 + 2; s++)
            {
                DatYt[s] = a * Dat2[s] + b * Dat2[s - 1] + c * Dat2[s - 2]
                           + d * DatYt[s - 1] + e * DatYt[s - 2];
            }
            DatYt[dF2 + 3] = DatYt[dF2 + 2] = DatYt[dF2 + 1];

            // FORWARD filter
            double[] DatZt = new double[dF2 + 2];
            DatZt[dF2] = DatYt[dF2 + 2];
            DatZt[dF2 + 1] = DatYt[dF2 + 3];
            for (long t = -dF2 + 1; t <= 0; t++)
            {
                DatZt[-t] = a * DatYt[-t + 2] + b * DatYt[-t + 3] + c * DatYt[-t + 4]
                            + d * DatZt[-t + 1] + e * DatZt[-t + 2];
            }

            // Calculated points copied for return
            for (long p = 0; p < dF2; p++)
            {
                data[p] = DatZt[p];
            }

            return data;
        }

    }


}

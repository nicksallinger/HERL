using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using OfficeOpenXml;

namespace Project1
{
    public class Subject
    {
        //make all data private, put print into Subject.cs, ENCAPUSLATION

        static String distanceOutputFile = "/Users/nicholassallinger/HERL/RawData/Output/JointDistance.csv";
        static String variableOutputFile = "/Users/nicholassallinger/HERL/RawData/Output/Variables.csv";
        static String TAIOutputFile = "/Users/nicholassallinger/HERL/RawData/Output/TAIScores.csv";




        //num rows is unique to each subject, num cols is not
        private int numRows;
        private int numRowsStatic;
        private String subjectNum;
        private String testNum;
        private String[][] data;
        private String[][] staticData;
        private String directory;
        private String staticFile;
        private int startFrame;
        private int endFrame;
        private double spineBase_x;
        private double spineBase_y;
        private double hipLeft_x;
        private double hipLeft_y;
        private double hipRight_x;
        private double hipRight_y;
        private double kneeLeftDistance;
        private double kneeRightDistance;
        private double spineBase_y_maxmin;
        private double hipRight_maxmin; //WRONG
        private double hipLeft_maxmin; //WRONG
        private double footLeftDistance;
        private double footRightDistance;
        private double ankleLeftDistance;
        private double ankleRightDistance;
        private double hipLeftDistance;
        private double hipRightDistance;
        private double spineDistance;
        private double hipAngleAtMin;
        private double hipAngleAtMax;
        private double shoulderAngleAtMin;
        private double shoulderAngleAtMax;
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
        private double chestCircumference;
        private double waistCircumference;
        private double trunkLength;
        private double wheelchairHeight;

        private static double CutValue1_1 = 0.5;
        private static double CutValue1_2 = 0.5;
        private static double CutValue1_3 = 0.5;
        private static double CutValue1_6 = -0.761;
        private static double CutValue1_7 = 1.67;

        private double TAI1_1;
        private double TAI1_2;
        private double TAI1_3;
        private double TAI1_6;
        private double TAI1_7;

        //These public getters arent really necessary, just something I thought I might need but never did
        public double ShoulderAngleAtMax { get => ShoulderAngleAtMax; }
        public double ShoulderAngleAtMin { get => shoulderAngleAtMin; }
        public string SubjectNum { get => subjectNum; }
        public string TestNum { get => testNum; }
        public double SpineBase_x { get => spineBase_x; }
        public double SpineBase_y { get => spineBase_y; }
        public double HipLeft_x { get => hipLeft_x; }
        public double HipLeft_y { get => hipLeft_y; }
        public double HipRight_x { get => hipRight_x; }
        public double HipRight_y { get => hipRight_y; }
        public double KneeLeftDistance { get => kneeLeftDistance; }
        public double HipAngleAtMax { get => hipAngleAtMax; }
        public double HipAngleAtMin { get => hipAngleAtMin; }
        public double FootRightDistance { get => footRightDistance; }
        public double HipRightDistance { get => hipRightDistance; }
        public double KneeRightDistance { get => kneeRightDistance; }
        public double SpineBase_y_maxmin { get => spineBase_y_maxmin; }
        public double HipRight_maxmin { get => hipRight_maxmin; }
        public double AnkleLeftDistance { get => ankleLeftDistance; }
        public double HipLeft_maxmin { get => hipLeft_maxmin; }
        public double HipLeftDistance { get => hipLeftDistance; }
        public double FootLeftDistance { get => footLeftDistance; }
        public double AnkleRightDistance { get => ankleRightDistance; }
        public double RightHandTipSize { get => rightHandTipSize; }
        public double LeftHandTipSize { get => leftHandTipSize; }
        public double RightHandThumbSize { get => rightHandThumbSize; }
        public double LeftHandThumbSize { get => leftHandThumbSize; }


        //Used when there is no data for start and end frames
        public Subject(String directory) : this(directory, 0, 0)
        {

        }

        public Subject(String directory, int start, int end)
        {
            startFrame = start;
            endFrame = end;
            this.directory = directory;
            Initialize();

        }

		public Subject(String directory, String staticFile, int start, int end, double mass, double height, double chestCircumference, double waistCircumference, double trunkLength, double wheelchairHeight)
		{
			startFrame = start;
			endFrame = end;
			this.directory = directory;
            this.staticFile = staticFile;
            this.mass = mass;
            this.height = height;
            this.chestCircumference = chestCircumference;
            this.waistCircumference = waistCircumference;
            this.trunkLength = trunkLength;
            this.wheelchairHeight = wheelchairHeight;
			Initialize();
		}

        private void Initialize()
        {
            data = ParseCSV(directory);
            staticData = ParseStaticCSV(staticFile);

            //SetupOutputFiles();

            subjectNum = GetSubjectNumber();
            testNum = GetTestNumber();
            spineBase_x = CalculateSpineBaseX();
            spineBase_y = CalculateSpineBaseY();
            hipLeft_x = CalculateHipLeftX();
            hipLeft_y = CalculateHipLeftY();
            hipRight_x = CalculateHipRightX();
            hipRight_y = CalculateHipRightY();

            if (staticFile != null)
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
            }

            if (startFrame != 0 && endFrame != 0)
            {
                kneeLeftDistance = CalculateKneeLeftDistance(startFrame, endFrame);
                kneeRightDistance = CalculateKneeRightDistance(startFrame, endFrame);
                footLeftDistance = CalculateFootLeftDistance(startFrame, endFrame);
                footRightDistance = CalculateFootRightDistance(startFrame, endFrame);
                ankleLeftDistance = CalculatAnkleLeftDistance(startFrame, endFrame);
                ankleRightDistance = CalculateAnkleRightDistance(startFrame, endFrame);
                shoulderAngleAtMin = CalculateShoulderAngleAtMin(startFrame, endFrame);
                shoulderAngleAtMax = CalculateShoulderAngleAtMax(startFrame, endFrame);
                hipAngleAtMin = CalculateHipAngleAtMin(startFrame, endFrame);
                hipAngleAtMax = CalculateHipAngleAtMax(startFrame, endFrame);
                hipRightDistance = CalculateHipRightDistance(startFrame, endFrame);
                hipLeftDistance = CalculateHipLeftDistance(startFrame, endFrame);
                hipLeft_maxmin = CalculateHipLeftMaxMin(startFrame, endFrame);
                hipRight_maxmin = CalculateHipRightMaxMin(startFrame, endFrame);
                spineBase_y_maxmin = CalculateSpineBaseYMaxMin(startFrame, endFrame);
                spineDistance = CalculateSpineDistance(startFrame, endFrame);
                rightHandTipSize = CalculateRightHandTipSize(startFrame, endFrame);
                leftHandTipSize = CalculateLeftHandTipSize(startFrame, endFrame);
                rightHandThumbSize = CalculateRightHandThumbSize(startFrame, endFrame);
                leftHandThumbSize = CalculateLeftHandThumbSize(startFrame, endFrame);

                TAICalculations();
            }
            else
            {
                //for if there is no frame data
                kneeLeftDistance = 0;
                kneeRightDistance = 0;
                footLeftDistance = 0;
                footRightDistance = 0;
                ankleLeftDistance = 0;
                ankleRightDistance = 0;
                shoulderAngleAtMin = 0;
                shoulderAngleAtMax = 0;
                hipAngleAtMin = 0;
                hipAngleAtMax = 0;
                hipRightDistance = 0;
                hipLeftDistance = 0;
                ankleLeftDistance = 0;
                ankleRightDistance = 0;
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
                //Console.WriteLine("File exists");
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
                Console.WriteLine("File does not exist");
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

		private String[][] ParseStaticCSV(String dir)
		{
			if (File.Exists(dir))
			{
				//Console.WriteLine("File exists");
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
            String returnString = subInfo[subIndex].ToString() + subInfo[subIndex+1].ToString();
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

            for (int i = 0; i < testInfo.Length; i++)
            {
                if (testInfo[i].Equals('B'))
                {
                    testIndex = i + 1;
                }
            }

            String returnString = testInfo[testIndex].ToString() + testInfo[testIndex + 1].ToString();
            //Console.WriteLine("Test number: {0}",returnString);

            return returnString;
            //return "no";
        }

        public static void SetupOutputFiles(){
            System.IO.StreamWriter jointwr = new System.IO.StreamWriter(distanceOutputFile,true);
			jointwr.WriteLine("SubjectNum, Test number, rightHandTipSize, leftHandTipSize, rightHandThumbSize," +
			   "LeftHandThumbSize, RightUpperArmLength,LeftUpperArmLength, RightForearmLength, LeftForearmLength," +
			   "ShoulderLength, HipLength, RightThighLength, LeftThighLength, RightLegLength," +
			   "LeftLegLength, RightFootLength, LeftFootLength");
            jointwr.Close();
            System.IO.StreamWriter varwr = new System.IO.StreamWriter(variableOutputFile,true);
			varwr.WriteLine("SpineBase_X, SpineBase_Y, HipLeft_X," +
						   "HipLeft_Y, HipRight_X, HipRight_Y, SpineBase_Y_maxmin, hipRight_maxmin," +
						   "HipLeft_maxmin, kneeLeftDistance, kneeRightDistance,footLeftDistance, FootRightDistance," +
						   "ankleLeftDistance, ankleRightDistance, hipLeftDistance, hipRightDistance,spineDistance," +
						   "hipAngleAtMin, hipAngleAtMax, shoulderAngleAtMin, shoulderAngleAtMax");
            varwr.Close();
            System.IO.StreamWriter taiwr = new System.IO.StreamWriter(TAIOutputFile,true);
			taiwr.WriteLine("SubjectNum, Test number, TAI1_1, TAI1_2,TAI1_3," +
						   " TAI1_6,TAI1_7");
			taiwr.Close();

        }

        /*******************************
         * ANTHROPOMETRIC CALCULATIONS
         ******************************/

        private double CalculateRightUpperArmLength(){
            //26-29, 27-30,28-31
            double total = 0;
          
            for (int i = 1; i < numRowsStatic; i++)
            {
                total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][26]) - Convert.ToDouble(staticData[i][29]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][27]) - Convert.ToDouble(staticData[i][30]), 2) +
                                  Math.Pow(Convert.ToDouble(staticData[i][28]) - Convert.ToDouble(staticData[i][31]), 2));
             
            }
            return total/numRowsStatic;

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
			return total / numRowsStatic;

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
			return total / numRowsStatic;

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
			
			return total / numRowsStatic;

		}
		private double CalculateShoulderLength()
		{
			//14-26
			//=2*SQRT((O2-AA2)^2+(P2-AB2)^2+(Q2-AC2)^2)
			double total = 0;

			for (int i = 1; i < numRowsStatic; i++)
			{
				total = total + 2*Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][14]) - Convert.ToDouble(staticData[i][26]), 2) +
								  Math.Pow(Convert.ToDouble(staticData[i][15]) - Convert.ToDouble(staticData[i][27]), 2) +
								  Math.Pow(Convert.ToDouble(staticData[i][16]) - Convert.ToDouble(staticData[i][28]), 2));

			}
           
			return total / numRowsStatic;

        }
		private double CalculateHipLength()
		{
			//38-50
			//=2*SQRT((AM2-AY2)^2+(AN2-AZ2)^2+(AO2-BA2)^2)
			double total = 0;

			for (int i = 1; i < numRowsStatic; i++)
			{
				total = total + 2*Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][38]) - Convert.ToDouble(staticData[i][50]), 2) +
								  Math.Pow(Convert.ToDouble(staticData[i][39]) - Convert.ToDouble(staticData[i][51]), 2) +
								  Math.Pow(Convert.ToDouble(staticData[i][40]) - Convert.ToDouble(staticData[i][52]), 2));

			}
			
			return total / numRowsStatic;

		}
		private double CalculateRightThighLength()
		{
			//50-53
			//=SQRT((AY2-BB2)/^2+(AZ2-BC2)^2+(BA2-BD2)^2)
			double total = 0;

            for (int i = 1; i < numRowsStatic; i++){
				total = total + Math.Sqrt(Math.Pow(Convert.ToDouble(staticData[i][50]) - Convert.ToDouble(staticData[i][53]), 2) +
								  Math.Pow(Convert.ToDouble(staticData[i][51]) - Convert.ToDouble(staticData[i][54]), 2) +
								  Math.Pow(Convert.ToDouble(staticData[i][52]) - Convert.ToDouble(staticData[i][55]), 2));

			}
			
            return total / numRowsStatic;

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
			
			return total / numRowsStatic;

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
			
			return total / numRowsStatic;

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
	
			return total / numRowsStatic;

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
			
			return total / numRowsStatic;

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
			
			return total / numRowsStatic; ;

		}


        private double CalculateRightHandTipSize(int start, int end)
        {
            //RightHandTipSize = aj-bt + ak-bu + al-bv
            //35-71, 36-72, 37-73
            double handX = 0;
            double handTipX = 0;
            double handY = 0;
            double handTipY = 0;
            double handZ = 0;
            double handTipZ = 0;

            for (int i = 1; i < numRowsStatic; i++) {
                
			handX = handX + Convert.ToDouble(staticData[i][35]);
			handTipX = handTipX + Convert.ToDouble(staticData[i][71]);
			handY = handY + Convert.ToDouble(staticData[i][36]);
			handTipY = handTipY + Convert.ToDouble(staticData[i][72]);
			handZ = handZ + Convert.ToDouble(staticData[i][37]);
			handTipZ = handTipZ + Convert.ToDouble(staticData[i][73]);
            
            }


            return Math.Sqrt(Math.Pow(handX/numRowsStatic - handTipY/numRowsStatic, 2) + Math.Pow(handY/numRowsStatic - handTipY/numRowsStatic, 2)
							 + Math.Pow(handZ/numRowsStatic - handTipZ/numRowsStatic, 2));
		}

		private double CalculateLeftHandTipSize(int start, int end)
		{
			double handX = 0;
			double handTipX = 0;
			double handY = 0;
			double handTipY = 0;
			double handZ = 0;
			double handTipZ = 0;


			for (int i = 1; i < numRowsStatic; i++)
			{
				handX = handX + Convert.ToDouble(staticData[i][23]);
				handTipX = handTipX + Convert.ToDouble(staticData[i][65]);
				handY = handY + Convert.ToDouble(staticData[i][24]);
				handTipY = handTipY + Convert.ToDouble(staticData[i][66]);
				handZ = handZ + Convert.ToDouble(staticData[i][25]);
				handTipZ = handTipZ + Convert.ToDouble(staticData[i][67]);
			}

            return Math.Sqrt(Math.Pow(handX/numRowsStatic - handTipY/numRowsStatic, 2) + Math.Pow(handY/numRowsStatic - handTipY/numRowsStatic, 2)
                             + Math.Pow(handZ/numRowsStatic - handTipZ/numRowsStatic, 2));

		}
		private double CalculateRightHandThumbSize(int start, int end)
		{

			double handX = 0;
			double thumbX = 0;
			double handY = 0;
			double thumbY = 0;
			double handZ = 0;
			double thumbZ = 0;


            for (int i = 1; i < numRowsStatic; i++)
            {
                handX = Convert.ToDouble(staticData[i][23]);
                thumbX = Convert.ToDouble(staticData[i][65]);
                handY = Convert.ToDouble(staticData[i][24]);
                thumbY = Convert.ToDouble(staticData[i][66]);
                handZ = Convert.ToDouble(staticData[i][25]);
                thumbZ = Convert.ToDouble(staticData[i][67]);
            }

            return Math.Sqrt(Math.Pow(handX/numRowsStatic - thumbX/numRowsStatic, 2) + Math.Pow(handY/numRowsStatic - thumbY/numRowsStatic, 2)
							 + Math.Pow(handZ/numRowsStatic - thumbZ/numRowsStatic, 2));

		}
		private double CalculateLeftHandThumbSize(int start, int end)
		{
			double handX = 0;
			double thumbX = 0;
			double handY = 0;
			double thumbY = 0;
			double handZ = 0;
			double thumbZ = 0;


            for (int i = 1; i < numRowsStatic; i++)
            {
                handX = Convert.ToDouble(staticData[i][23]);
                thumbX = Convert.ToDouble(staticData[i][68]);
                handY = Convert.ToDouble(staticData[i][24]);
                thumbY = Convert.ToDouble(staticData[i][69]);
                handZ = Convert.ToDouble(staticData[i][25]);
                thumbZ = Convert.ToDouble(staticData[i][60]);
            }
			return Math.Sqrt(Math.Pow(handX/numRowsStatic - thumbX/numRowsStatic, 2) + Math.Pow(handY/numRowsStatic - thumbY/numRowsStatic, 2)
								 + Math.Pow(handZ/numRowsStatic - thumbZ/numRowsStatic, 2)); ;

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
			//Console.Write("spinebase_x = {0}", max-min);
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
            //Write("spinebase_y = {0}", max-min);

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
            return (max - min);
        }

        private double CalculateHipLeftMaxMin(int start, int end)
        {
            ////an max - an min
            //double max = 0.0000;
            //double min = 0.0000;
            //for (int i = start; i < end; i++)
            //{
            //    if (i == 1)
            //    {
            //        max = Convert.ToDouble(data[i][39]);
            //        min = Convert.ToDouble(data[i][39]);
            //    }
            //    else
            //    {
            //        if (Convert.ToDouble(data[i][39]) >= max) { max = Convert.ToDouble(data[i][51]); }
            //        if (Convert.ToDouble(data[i][39]) <= min) { min = Convert.ToDouble(data[i][51]); }
            //    }

            //}
            //return (max - min);
            return (Convert.ToDouble(data[end][39]) - Convert.ToDouble(data[start][39]));
        }


        private double CalculateKneeLeftDistance(int start, int end)
        {
            //Knee Left Distance = Sqrt[(aptotal[[MAX]] - aptotal[[MIN]])^2 + (aqtotal[[MAX]] - aqtotal[[MIN]])^2 + (artotal[[MAX]] - artotal[[MIN]])^2]
            //aptotal = Table[data[[42 + 77 * (i - 1)]], { i, 2, Length[data] / 77}];
            //aqtotal = Table[data[[43 + 77*(i - 1)]], {i, 2, Length[data]/77}];
            //artotal = Table[data[[44 + 77 * (i - 1)]], { i, 2, Length[data] / 77}];

            //Console.WriteLine(end);
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
        private double CalculateHipAngleAtMax(int start, int end)
        {


            double hipRightXMax = Convert.ToDouble(data[end][50]);
            double hipLeftXMax = Convert.ToDouble(data[end][38]);
            double hipRightYMax = Convert.ToDouble(data[end][51]);
            double hipLeftYMax = Convert.ToDouble(data[end][39]);
            double hipRightZMax = Convert.ToDouble(data[end][52]);
            double hipLeftZMax = Convert.ToDouble(data[end][40]);


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
        private double CalculateShoulderAngleAtMax(int start, int end)
        {

            //Shoulder Angle at Max =[180/\[Pi]ArcCos[(srxtotal[[MAX]] - //slxtotal[[MAX]])/Sqrt[(srxtotal[[MAX]] - slxtotal[[MAX]])^2 + \//(srytotal[[MAX]] - 
            //slytotal[[MAX]])^2 + (srztotal[[MAX]] - \//slztotal[[MAX]])^2]
            double shoulderRightXMax = Convert.ToDouble(data[end][26]);
            double shoulderLeftXMax = Convert.ToDouble(data[end][14]);
            double shoulderRightYMax = Convert.ToDouble(data[end][27]);
            double shoulderLeftYMax = Convert.ToDouble(data[end][15]);
            double shoulderRightZMax = Convert.ToDouble(data[end][28]);
            double shoulderLeftZMax = Convert.ToDouble(data[end][16]);


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
        * TAI CALCULATIONS
        **************************************************************/

		private int Calculate1_1(double height, double RUALength, double LUALength, double RForearmLength, double LForearmLength,
                                double TrunkLength, double RThighLength, double LThighLength, double RLegLength,
                                double LLegLength, double RFootLength, double LFootLength)
		{
            //CHANGE VARIABLES HERE
            double Height_cm_1_var = .041;
			double R_Upper_Arm_Length_13_var = -.557;
			double L_Upper_Arm_Length_14_var = .711;
			double R_Forearm_Length_15_var = .001;
			double L_Forearm_Length_16_var = -.399;
			double Trunk_Length_19_var = .176;
			double R_Thigh_Length_20_var = -.169;
			double L_Thigh_Length_21_var = .181;
			double R_Leg_Length_22_var = -.307;
			double L_Leg_Length_23_var = .518;
			double R_Foot_Length_24_var = .210;
			double L_Foot_Length_25_var = -.447;
			double Dx_SpineBase_var = -.010;			
			double Dx_HipLeft_var = -.006;			
			double Dx_HipRightVar = .009;
			double constant = -10.096;

			
			TAI1_1 = (Height_cm_1_var * height)
                + (R_Upper_Arm_Length_13_var * RUALength)
                + (L_Upper_Arm_Length_14_var * LUALength)
                + (R_Forearm_Length_15_var * RForearmLength)
                + (L_Forearm_Length_16_var * LForearmLength)
                + (Trunk_Length_19_var * TrunkLength)
                + (R_Thigh_Length_20_var * RThighLength)
                + (L_Thigh_Length_21_var * LThighLength)
                + (R_Leg_Length_22_var * RLegLength)
                + (L_Leg_Length_23_var * LLegLength)
                + (R_Foot_Length_24_var * RFootLength)
                + (L_Foot_Length_25_var * LFootLength)
				+ (spineBase_x * Dx_SpineBase_var)
				+ (hipLeft_x * Dx_HipLeft_var)
				+ (hipRight_x * Dx_HipRightVar)
				+ (constant);

            if(TAI1_1 >= CutValue1_1){
                return 1;
            }
            else return 0;
		}

		private int Calculate1_2(double mass, double height, double RUALength, double LUALength, double RForearmLength,
                                double LForearmLength, double chestCircumference, double waistCircumference, double trunkLength,
                                double RThighLength, double LThighLength, double RLegLength, double LLegLength,
                                double RFootLength, double LFootLength, double wheelchairHeight)
		{

			double mass_kg_0_var = -.048;
           	double height_cm_1_var = .270;
			double R_Upper_Arm_Length_13_var = .861;
			double L_Upper_Arm_Length_14_var = -1.783;
			double R_Forearm_Length_15_var = .618;
			double L_Forearm_Length_16_var = -.155;
			double chest_Circumference_17_var = -.027;
			double waist_Circumference_18_var = .129;
			double trunk_Length_19_var = .044;
			double R_Thigh_Length_20_var = .003;
			double L_Thigh_Length_21_var = -.333;
			double R_Leg_Length_22_var = .791;
			double L_Leg_Length_23_var = -.705;
			double R_Foot_Length_24_var = -.338;
			double L_Foot_Length_25_var = -.684;
			double wheelchair_Height_26_var = -.561;
			double hipAngleStart_setting_var = -.057;
			double shoulderAngleStart_setting_var = .405;
			double hipAngleatEnd_setting_var = .028;
			double shoulderAngleatEnd_setting_var = .073;
			double constant = 25.305;


			TAI1_2 = (mass_kg_0_var * mass) +
                (height_cm_1_var * height) +
                (R_Upper_Arm_Length_13_var * RUALength) +
			    (L_Upper_Arm_Length_14_var * LUALength) +
                (R_Forearm_Length_15_var * RForearmLength) +
                (L_Forearm_Length_16_var * LForearmLength) +
                (chest_Circumference_17_var * chestCircumference) +
                (waist_Circumference_18_var * waistCircumference) +
                (trunk_Length_19_var * trunkLength) +
                (R_Thigh_Length_20_var * RThighLength) +
                (L_Thigh_Length_21_var * LThighLength) +
                (R_Leg_Length_22_var * RLegLength) +
                (L_Leg_Length_23_var * LLegLength) +
                (R_Foot_Length_24_var * RFootLength) +
                (L_Foot_Length_25_var * LLegLength) +
                (wheelchair_Height_26_var * wheelchairHeight) +
                (hipAngleStart_setting_var * hipAngleAtMin) +
                (shoulderAngleStart_setting_var * shoulderAngleAtMin) +
                (hipAngleatEnd_setting_var * hipAngleAtMax) +
                (shoulderAngleatEnd_setting_var * shoulderAngleAtMax) +
				constant;
			//TODO: Verify these are the right variables

			if(TAI1_2 >= CutValue1_2){
                return 1;
            }
			else return 0;
		}

		private int Calculate1_3(double mass, double height, double RUALength, double LUALength, double RForearmLength, 
                                 double LForearmLength, double chestCircumference, double waistCircumference, double trunkLength, 
                                 double RThighLength, double LThighLength, double RLegLength, double LLegLength,
								  double RFootLength, double LFootLength, double wheelchairHeight)
		{

			double Mass_kg_0_var = .171;
			double Height_cm_1_var = -.443;
			double R_Upper_Arm_Length_13_var = 1.620;
			double L_Upper_Arm_Length_14_var = -1.688;
			double R_Forearm_Length_15_var = 1.201;
			double L_Forearm_Length_16_var = -1.235;
			double Chest_Circumference_17_var = -.037;
			double Waist_Circumference_18_var = -.176;
			double Trunk_Length_19_var = .149;
			double R_Thigh_Length_20_var = -.006;
			double L_Thigh_Length_21_var = .313;
			double R_Leg_Length_22_var = 1.071;
			double L_Leg_Length_23_var = .176;
			double R_Foot_Length_24_var = -1.502;
			double L_Foot_Length_25_var = 1.954;
			double Wheelchair_Height_26_var = .104;
			double SpineBaseY_trail_var = .019;
			double HipLeftY_trail_var = .001;
			double HipRightY_trail_var = -.030;
			double Constant = 4.690;

			double result = (Mass_kg_0_var * mass) +
                (Height_cm_1_var * height) +
				(R_Upper_Arm_Length_13_var * RUALength) +
                (L_Upper_Arm_Length_14_var * LUALength) +
                (R_Forearm_Length_15_var * RForearmLength) +
                (L_Forearm_Length_16_var * LForearmLength) +
                (Chest_Circumference_17_var * chestCircumference) +
                (Waist_Circumference_18_var * waistCircumference) +
                (Trunk_Length_19_var * trunkLength) +
                (R_Thigh_Length_20_var * RThighLength) +
                (L_Thigh_Length_21_var * LThighLength) +
                (R_Leg_Length_22_var * RLegLength) +
                (L_Leg_Length_23_var * LLegLength) +
                (R_Foot_Length_24_var * RFootLength) +
                (L_Foot_Length_25_var * LFootLength) +
                (Wheelchair_Height_26_var * wheelchairHeight) +
                (SpineBaseY_trail_var * spineBase_y) +
                (HipLeftY_trail_var * hipLeft_y) +
                (HipRightY_trail_var * hipRight_y) +
				(Constant);

            if(result > CutValue1_3){
                return 1;
            }
            else return 0;
		}


		private int Calculate1_6(double RThighLength, double LThighLength, double RLegLength, double LLegLength,
                                 double RFootLength, double LFootLength, double wheelchairHeight)
		{
			double kneeLeftDistance_setting_var = .00340;
			double footLeftDistance_setting_var = .00711;
			double ankleLeftDistance_setting_var = -.00366;
			double hipLeftDistance_setting_var = -.00059;
			double kneeRightDistance_setting_var = -.00030;
			double footRightDistance_setting_var = .00094;
			double ankleRightDistance_setting_var = .00102;
			double hipRightDistance_setting_var = -.00400;
			double R_Thigh_Length_20_var = -.33944;
			double L_Thigh_Length_21_var = .25505;
			double R_Leg_Length_22_var = .74918;
			double L_Leg_Length_23_var = -.69679;
			double R_Foot_Length_24_var = -.27614;
			double L_Foot_Length_25_var = .10554;
			double wheelchair_Height_26_var = .07388;
			double constant = .25251;

            //TODO: find where these vars come from
            TAI1_6 = (kneeLeftDistance_setting_var * kneeLeftDistance) +
                    (footLeftDistance_setting_var * footLeftDistance) +
                (ankleLeftDistance_setting_var * ankleLeftDistance) +
                (hipLeftDistance_setting_var * hipLeftDistance) +
                (kneeRightDistance_setting_var * kneeRightDistance) +
                (footRightDistance_setting_var * footRightDistance) +
                (ankleRightDistance_setting_var * ankleRightDistance) +
                (hipRightDistance_setting_var * hipRightDistance) +
                (R_Thigh_Length_20_var * RThighLength) +
                (L_Thigh_Length_21_var * LThighLength) +
                (R_Leg_Length_22_var * RLegLength) +
                (L_Leg_Length_23_var * LLegLength) +
                (R_Foot_Length_24_var * RFootLength) +
                (L_Foot_Length_25_var * LFootLength) +
                (wheelchair_Height_26_var * wheelchairHeight) +
			    (constant);

            if(TAI1_6 >= CutValue1_6){
                return 1;
            }
            else return 0;
		}

		private int Calculate1_7(double height, double RThighLength, double LThighLength, double RLegLength,
                                 double LLegLength, double RFootLength, double LFootLength)
		{
			double hipLeftDistance_setting_var = -.009;
			double spineDistance_setting_var = .014;
			double hipRightDistance_setting_var = -.008;
			double height_cm_1_var = -.194;
			double R_Thigh_Length_20_var = .585;
			double L_Thigh_Length_21_var = -.415;
			double R_Leg_Length_22_var = -.157;
			double L_Leg_Length_23_var = .754;
			double R_Foot_Length_24_var = -.167;
			double L_Foot_Length_25_var = .151;
			double constant = 1.289;

            TAI1_7 = (hipLeftDistance_setting_var * hipLeftDistance) +
                (spineDistance_setting_var * spineDistance) +
                (hipRightDistance_setting_var * hipRightDistance) +
                (height_cm_1_var * height) +
                (R_Thigh_Length_20_var * RThighLength) +
                (L_Thigh_Length_21_var * LThighLength) +
                (R_Leg_Length_22_var * RLegLength) +
                (L_Leg_Length_23_var * LLegLength) +
                (R_Foot_Length_24_var * RFootLength) +
                (L_Foot_Length_25_var * LFootLength) +
			    constant;

            //Console.WriteLine(hipLeftDistance_setting_var + "*" + hipLeftDistance + "+");
            //Console.WriteLine(spineDistance_setting_var + "*" + spineDistance + "+");
            //Console.WriteLine(hipRightDistance_setting_var + "*" + hipRightDistance + "+");
            //Console.WriteLine(height_cm_1_var + "*" + height + "+");
            //Console.WriteLine(R_Thigh_Length_20_var + "*" + RThighLength + "+");
            //Console.WriteLine(L_Thigh_Length_21_var + "*" + LThighLength + "+");
            //Console.WriteLine(R_Leg_Length_22_var + "*" + RLegLength + "+");
            //Console.WriteLine(L_Leg_Length_23_var + "*" + LLegLength + "+");
            //Console.WriteLine(R_Foot_Length_24_var + "*" + RFootLength + "+");
            //Console.WriteLine(L_Foot_Length_25_var + "*" + LFootLength + "+");
                   //Console.WriteLine(constant);
            
            //Console.WriteLine("TAI = " + TAI1_7);

            if(TAI1_7 <= CutValue1_7){
                return 1;
            }
            else {
                return 0;
            }
                
		}

        void TAICalculations(){

            //Console.WriteLine("Enter Height in cm: ");
            //double height = Double.Parse(Console.ReadLine());

            //Console.WriteLine("Enter mass: ");
            //double mass = double.Parse(Console.ReadLine());

            //Console.WriteLine("Enter Right upper arm length ");


            //double RUALength = Double.Parse(Console.ReadLine());

            //Console.WriteLine("Enter Left upper arm length: ");
            //double LUALength = Double.Parse(Console.ReadLine());

            //Console.WriteLine("Enter right forearm length: ");
            //double RForearmLength = Double.Parse(Console.ReadLine());

            //Console.WriteLine("Enter left forearm length: ");
            //double LForearmLength = Double.Parse(Console.ReadLine());

            //Console.WriteLine("Enter trunk length: ");
            //double trunkLength = Double.Parse(Console.ReadLine());

            //Console.WriteLine("Enter right thigh length: ");
            //double RThighLength = Double.Parse(Console.ReadLine());

            //Console.WriteLine("Enter left thigh length: ");
            //double LThighLength = Double.Parse(Console.ReadLine());

            //Console.WriteLine("Enter right leg length: ");
            //double RLegLength = Double.Parse(Console.ReadLine());

            //Console.WriteLine("Enter left leg length: ");
            //double LLegLength = Double.Parse(Console.ReadLine());

            //Console.WriteLine("Enter right food length: ");
            //double RFootLength = Double.Parse(Console.ReadLine());

            //Console.WriteLine("Enter left food length: ");
            //double LFootLength = Double.Parse(Console.ReadLine());

            //Console.WriteLine("Enter chest circumference:");
            //double chestCircumference = double.Parse(Console.ReadLine());

            //Console.WriteLine("Enter waist circumference:");
            //double waistCircumference = double.Parse(Console.ReadLine());

            //Console.WriteLine("Enter wheelchair hieght:");
            //double wheelchairHeight = double.Parse(Console.ReadLine());



            //double height = 175.26;
            //double mass = 82.63;
            //double RUALength = 35;
            //double LUALength = 35;
            //double RForearmLength = 30;
            //double LForearmLength = 29;
            //double trunkLength = 57;
            //double RThighLength = 42;
            //double LThighLength = 45;
            //double RLegLength = 43;
            //double LLegLength = 45;
            //double RFootLength = 22;
            //double LFootLength = 21;
            //double chestCircumference = 117;
            //double waistCircumference = 109;
            //double wheelchairHeight = 57;

            TAI1_1 = Calculate1_1(height,rightUpperArmLength,leftUpperArmLength,rightForearmLength,leftForearmLength,trunkLength,rightThighLength,
                                  leftThighLength,rightLegLength,leftLegLength,rightFootLength,leftFootLength);

            TAI1_2 = Calculate1_2(mass, height, rightUpperArmLength, leftUpperArmLength, rightForearmLength,
                                leftForearmLength, chestCircumference, waistCircumference, trunkLength,
                                  rightThighLength, leftThighLength, rightLegLength, leftLegLength,
								rightFootLength, leftFootLength, wheelchairHeight);
            
            TAI1_3 = Calculate1_3(mass, height, rightUpperArmLength, leftUpperArmLength, rightForearmLength, leftForearmLength, chestCircumference,
                                  waistCircumference, trunkLength, rightThighLength, leftThighLength, rightLegLength, leftLegLength,
                                  rightFootLength, leftFootLength, wheelchairHeight);

            TAI1_6 = Calculate1_6(rightThighLength, leftThighLength, rightLegLength, leftLegLength, rightFootLength, leftFootLength, wheelchairHeight);

            TAI1_7 = Calculate1_7(height, rightThighLength, leftThighLength, rightLegLength, leftLegLength, rightFootLength, leftFootLength);

		}



        public void OutputToTxt(){
            //OutputTAI();
            //OutputVariables();
            OutputJointDistance();
        }


        /// <summary>
        /// Outputs a CSV file
        /// </summary>
        public void OutputTAI()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(TAIOutputFile,true);
            file.WriteLine("{0},{1},{2},{3},{4},{5},{6}",
                           subjectNum, testNum, TAI1_1,
                           TAI1_2, TAI1_3, TAI1_6, TAI1_7);
            file.Close();
        }


        public void OutputJointDistance(){           
            System.IO.StreamWriter file = new System.IO.StreamWriter(distanceOutputFile,true);
			file.WriteLine("{0},{1},{2},{3},{4}," +
						   "{5},{6},{7},{8},{9}," +
						   "{10},{11},{12},{13},{14}," +
						   "{15},{16},{17}",
						   subjectNum, testNum, spineBase_x, spineBase_y, hipLeft_x,
						   hipLeft_y, hipRight_x, hipRight_y, spineBase_y_maxmin, hipRight_maxmin,
						   hipLeft_maxmin, kneeLeftDistance, kneeRightDistance, footLeftDistance, footRightDistance,
						   ankleLeftDistance, ankleRightDistance, hipLeftDistance, hipRightDistance, spineDistance,
						   hipAngleAtMin, hipAngleAtMax, shoulderAngleAtMin, shoulderAngleAtMax);
            file.Close();
		}

        public void OutputVariables(){
            System.IO.StreamWriter file = new System.IO.StreamWriter(variableOutputFile,true);
            file.WriteLine("{0},{1},{2},{3},{4}," +
                           "{5},{6},{7},{8},{9}," +                        
                           "{10},{11},{12},{13},{14}," +                        
                           " {15},{16},{17},{18},{19}," +                          
                           "{20},{21}",
                           spineBase_x, spineBase_y, hipLeft_x, hipLeft_y, hipRight_x,
                           hipRight_y, spineBase_y_maxmin, hipRight_maxmin, hipLeft_maxmin, kneeLeftDistance,
                           kneeRightDistance, footLeftDistance, footRightDistance, ankleLeftDistance, ankleRightDistance,
                           hipLeftDistance, hipRightDistance, spineDistance,hipAngleAtMin, hipAngleAtMax, 
                           shoulderAngleAtMin, shoulderAngleAtMax);
            file.Close();
        }
    }
}

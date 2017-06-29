using System;
namespace Project1
{

    public class TAICalculations
    {
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
        Subject subject;

    
        public TAICalculations(Subject s)
        {
            this.subject = s;
        }



        private int Calculate1_1()
		{
            /*
             *TODO: store anthropometric data in subject class?
             *      or calculate in different class
             */


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

            //TODO: Unfinished, need anthropometric data
            TAI1_1 = (Height_cm_1_var)
                + (R_Upper_Arm_Length_13_var)
                + (L_Upper_Arm_Length_14_var)
                + (R_Forearm_Length_15_var)
                + (L_Forearm_Length_16_var)
                + (Trunk_Length_19_var)
                + (R_Thigh_Length_20_var)
                + (L_Thigh_Length_21_var)
                + (R_Leg_Length_22_var)
                + (L_Leg_Length_23_var)
                + (R_Foot_Length_24_var)
                + (L_Foot_Length_25_var)
                + (subject.SpineBase_x*Dx_SpineBase_var)
                + (subject.HipLeft_x*Dx_HipLeft_var)
                + (subject.HipRight_x*Dx_HipRightVar)
                + (constant);

            //TODO: logic
			return 0;
		}

		private int Calculate1_2()
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

            TAI1_2 = (mass_kg_0_var) +
			(height_cm_1_var) +
			(R_Upper_Arm_Length_13_var) +
			(L_Upper_Arm_Length_14_var) +
			(R_Forearm_Length_15_var) +
			(L_Forearm_Length_16_var) +
			(chest_Circumference_17_var) +
			(waist_Circumference_18_var) +
			(trunk_Length_19_var) +
			(R_Thigh_Length_20_var) +
			(L_Thigh_Length_21_var) +
			(R_Leg_Length_22_var) +
			(L_Leg_Length_23_var) +
			(R_Foot_Length_24_var) +
			(L_Foot_Length_25_var) +
			(wheelchair_Height_26_var) +
			(hipAngleStart_setting_var) +
			(shoulderAngleStart_setting_var) +
			(hipAngleatEnd_setting_var) +
			(shoulderAngleatEnd_setting_var) +
                constant;

            //TODO: logic
			return 0;
		}

        private int Calculate1_3(){

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

            TAI1_3 = (Mass_kg_0_var) +
                (Height_cm_1_var) +
                (R_Upper_Arm_Length_13_var) +
                (L_Upper_Arm_Length_14_var) +
                (R_Forearm_Length_15_var) +
                (L_Forearm_Length_16_var) +
                (Chest_Circumference_17_var) +
                (Waist_Circumference_18_var) +
                (Trunk_Length_19_var) +
                (R_Thigh_Length_20_var) +
                (L_Thigh_Length_21_var) +
                (R_Leg_Length_22_var) +
                (L_Leg_Length_23_var) +
                (R_Foot_Length_24_var) +
                (L_Foot_Length_25_var) +
                (Wheelchair_Height_26_var) +
                (SpineBaseY_trail_var) +
                (HipLeftY_trail_var) +
                (HipRightY_trail_var) +
                (Constant);

            //TODO: cut value logic

			return 0;
        }


        private int Calculate1_6(){
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

            TAI1_6 = (kneeLeftDistance_setting_var) +
            (hipLeftDistance_setting_var) + 
            (footLeftDistance_setting_var) + 
            (ankleLeftDistance_setting_var) +
            (hipLeftDistance_setting_var) + 
            (kneeRightDistance_setting_var) + 
            (footRightDistance_setting_var) + 
            (ankleRightDistance_setting_var) + 
            (hipRightDistance_setting_var) + 
            (R_Thigh_Length_20_var) + 
            (L_Thigh_Length_21_var) + 
            (R_Leg_Length_22_var) + 
            (L_Leg_Length_23_var) + 
            (R_Foot_Length_24_var) + 
            (L_Foot_Length_25_var) + 
            (wheelchair_Height_26_var) + 
            (constant);

            //TODO: logic

			return 0;
        }
        private int Calculate1_7(){
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

            TAI1_7 = (hipLeftDistance_setting_var) +
            (spineDistance_setting_var) + 
            (hipRightDistance_setting_var) +
            (height_cm_1_var) + 
            (R_Thigh_Length_20_var) + 
            (L_Thigh_Length_21_var) + 
            (R_Leg_Length_22_var) + 
            (L_Leg_Length_23_var) + 
            (R_Foot_Length_24_var) + 
            (L_Foot_Length_25_var) + 
            constant;

            //TODO: logic

			return 0;
        }

    }

}

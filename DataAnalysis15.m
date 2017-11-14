%% This program takes in a custom-named series of data (something like the following...
%  C:\Users\TYB18\Documents\Lin Wei\raw data Kinect\kinect_WSC1\S1_LB1_P
%  You MUST have the same naming setup. ..................WSC#\S#_LB#_P,
%  where # means the current subject or trial.  Watch out for zeros and
%  misspelled words in the source files.  Or just find the copy I modified
%  for this code.  Lin should have it.

%  This program works specifically when we're searching for Level Bench (LB)
%  files.

%  Set up to analyze based upon timepoints, not the full data file.  So, we
%  can parse down according to start of transfer, end of transfer, start of
%  reverse transfer, end of reverse transfer.

% Made by Tyler Brown

%% Read in data for parsing through time intervals

MatrixTimePeriods = xlsread('C:\Users\Kinect\Desktop\Tyler code_Kinect\TimePointsForAllNVWG1.xlsx','TimePoints');

  NumberRowsTime=size(MatrixTimePeriods,1);
  NumberColumnsTime=size(MatrixTimePeriods,2);
  
%  TrialTimePeriod = zeros(NumberRowsTime,1);
%  FirstTimePeriod = zeros(NumberRowsTime,1);
%  SecondTimePeriod = zeros(NumberRowsTime,1);
%  ThirdTimePeriod = zeros(NumberRowsTime,1);
%  FourthTimePeriod = zeros(NumberRowsTime,1);
  
  TrialTimePeriod = MatrixTimePeriods(:,1);
  FirstTimePeriod = MatrixTimePeriods(:,2);
  SecondTimePeriod = MatrixTimePeriods(:,4);
  ThirdTimePeriod = MatrixTimePeriods(:,6);
  FourthTimePeriod = MatrixTimePeriods(:,8);
  
  
  % The number of time periods is two because we're looking at the transfer
  % to the bench and then the transfer back to the chair.  Each section
  % alone is one "time period".

%% Initialize all matrices about to be made

numtrials = 1;
numsubjects = 7;
totaltests=numtrials*numsubjects;
Trials=(1:totaltests)';
ConstantFilePath = 'C:\Users\Kinect\Desktop\Tyler code_Kinect\2016NVWG\Kinect_NVWG';
% DataIn = zeros(numtrials,1);

InitialAngleShoulderLeftPOETransfer = zeros(numtrials,numsubjects);
MaxAngleShoulderLeftPOETransfer = zeros(numtrials,numsubjects);
MinAngleShoulderLeftPOETransfer = zeros(numtrials,numsubjects);
ROMAngleShoulderLeftPOETransfer = zeros(numtrials,numsubjects);
AveAngleShoulderLeftPOETransfer = zeros(numtrials,numsubjects);

InitialAngleShoulderRightPOETransfer = zeros(numtrials,numsubjects);
MaxAngleShoulderRightPOETransfer = zeros(numtrials,numsubjects);
MinAngleShoulderRightPOETransfer = zeros(numtrials,numsubjects);
ROMAngleShoulderRightPOETransfer = zeros(numtrials,numsubjects);
AveAngleShoulderRightPOETransfer = zeros(numtrials,numsubjects);

InitialAngleShoulderLeftPOEReverseTransfer = zeros(numtrials,numsubjects);
MaxAngleShoulderLeftPOEReverseTransfer = zeros(numtrials,numsubjects);
MinAngleShoulderLeftPOEReverseTransfer = zeros(numtrials,numsubjects);
ROMAngleShoulderLeftPOEReverseTransfer = zeros(numtrials,numsubjects);
AveAngleShoulderLeftPOEReverseTransfer = zeros(numtrials,numsubjects);

InitialAngleShoulderRightPOEReverseTransfer = zeros(numtrials,numsubjects);
MaxAngleShoulderRightPOEReverseTransfer = zeros(numtrials,numsubjects);
MinAngleShoulderRightPOEReverseTransfer = zeros(numtrials,numsubjects);
ROMAngleShoulderRightPOEReverseTransfer = zeros(numtrials,numsubjects);
AveAngleShoulderRightPOEReverseTransfer = zeros(numtrials,numsubjects);

InitialAngleShoulderLeftElevationTransfer = zeros(numtrials,numsubjects);
MaxAngleShoulderLeftElevationTransfer = zeros(numtrials,numsubjects);
MinAngleShoulderLeftElevationTransfer = zeros(numtrials,numsubjects);
ROMAngleShoulderLeftElevationTransfer = zeros(numtrials,numsubjects);
AveAngleShoulderLeftElevationTransfer = zeros(numtrials,numsubjects);

InitialAngleShoulderRightElevationTransfer = zeros(numtrials,numsubjects);
MaxAngleShoulderRightElevationTransfer = zeros(numtrials,numsubjects);
MinAngleShoulderRightElevationTransfer = zeros(numtrials,numsubjects);
ROMAngleShoulderRightElevationTransfer = zeros(numtrials,numsubjects);
AveAngleShoulderRightElevationTransfer = zeros(numtrials,numsubjects);

InitialAngleShoulderLeftElevationReverseTransfer = zeros(numtrials,numsubjects);
MaxAngleShoulderLeftElevationReverseTransfer = zeros(numtrials,numsubjects);
MinAngleShoulderLeftElevationReverseTransfer = zeros(numtrials,numsubjects);
ROMAngleShoulderLeftElevationReverseTransfer = zeros(numtrials,numsubjects);
AveAngleShoulderLeftElevationReverseTransfer = zeros(numtrials,numsubjects);

InitialAngleShoulderRightElevationReverseTransfer = zeros(numtrials,numsubjects);
MaxAngleShoulderRightElevationReverseTransfer = zeros(numtrials,numsubjects);
MinAngleShoulderRightElevationReverseTransfer = zeros(numtrials,numsubjects);
ROMAngleShoulderRightElevationReverseTransfer = zeros(numtrials,numsubjects);
AveAngleShoulderRightElevationReverseTransfer = zeros(numtrials,numsubjects);

InitialAngleElbowLeftFlexionTransfer = zeros(numtrials,numsubjects);
MaxAngleElbowLeftFlexionTransfer = zeros(numtrials,numsubjects);
MinAngleElbowLeftFlexionTransfer = zeros(numtrials,numsubjects);
ROMAngleElbowLeftFlexionTransfer = zeros(numtrials,numsubjects);
AveAngleElbowLeftFlexionTransfer = zeros(numtrials,numsubjects);

InitialAngleElbowRightFlexionTransfer = zeros(numtrials,numsubjects);
MaxAngleElbowRightFlexionTransfer = zeros(numtrials,numsubjects);
MinAngleElbowRightFlexionTransfer = zeros(numtrials,numsubjects);
ROMAngleElbowRightFlexionTransfer = zeros(numtrials,numsubjects);
AveAngleElbowRightFlexionTransfer = zeros(numtrials,numsubjects);

InitialAngleElbowLeftFlexionReverseTransfer = zeros(numtrials,numsubjects);
MaxAngleElbowLeftFlexionReverseTransfer = zeros(numtrials,numsubjects);
MinAngleElbowLeftFlexionReverseTransfer = zeros(numtrials,numsubjects);
ROMAngleElbowLeftFlexionReverseTransfer = zeros(numtrials,numsubjects);
AveAngleElbowLeftFlexionReverseTransfer = zeros(numtrials,numsubjects);

InitialAngleElbowRightFlexionReverseTransfer = zeros(numtrials,numsubjects);
MaxAngleElbowRightFlexionReverseTransfer = zeros(numtrials,numsubjects);
MinAngleElbowRightFlexionReverseTransfer = zeros(numtrials,numsubjects);
ROMAngleElbowRightFlexionReverseTransfer = zeros(numtrials,numsubjects);
AveAngleElbowRightFlexionReverseTransfer = zeros(numtrials,numsubjects);

InitialAngleWristLeftFlexionTransfer = zeros(numtrials,numsubjects);
MaxAngleWristLeftFlexionTransfer = zeros(numtrials,numsubjects);
MinAngleWristLeftFlexionTransfer = zeros(numtrials,numsubjects);
ROMAngleWristLeftFlexionTransfer = zeros(numtrials,numsubjects);
AveAngleWristLeftFlexionTransfer = zeros(numtrials,numsubjects);

InitialAngleWristRightFlexionTransfer = zeros(numtrials,numsubjects);
MaxAngleWristRightFlexionTransfer = zeros(numtrials,numsubjects);
MinAngleWristRightFlexionTransfer = zeros(numtrials,numsubjects);
ROMAngleWristRightFlexionTransfer = zeros(numtrials,numsubjects);
AveAngleWristRightFlexionTransfer = zeros(numtrials,numsubjects);

InitialAngleWristLeftFlexionReverseTransfer = zeros(numtrials,numsubjects);
MaxAngleWristLeftFlexionReverseTransfer = zeros(numtrials,numsubjects);
MinAngleWristLeftFlexionReverseTransfer = zeros(numtrials,numsubjects);
ROMAngleWristLeftFlexionReverseTransfer = zeros(numtrials,numsubjects);
AveAngleWristLeftFlexionReverseTransfer = zeros(numtrials,numsubjects);

InitialAngleWristRightFlexionReverseTransfer = zeros(numtrials,numsubjects);
MaxAngleWristRightFlexionReverseTransfer = zeros(numtrials,numsubjects);
MinAngleWristRightFlexionReverseTransfer = zeros(numtrials,numsubjects);
ROMAngleWristRightFlexionReverseTransfer = zeros(numtrials,numsubjects);
AveAngleWristRightFlexionReverseTransfer = zeros(numtrials,numsubjects);

InitialAngleTrunkFlexionTransfer = zeros(numtrials,numsubjects);
MaxAngleTrunkFlexionTransfer = zeros(numtrials,numsubjects);
MinAngleTrunkFlexionTransfer = zeros(numtrials,numsubjects);
ROMAngleTrunkFlexionTransfer = zeros(numtrials,numsubjects);
AveAngleTrunkFlexionTransfer = zeros(numtrials,numsubjects);

InitialAngleTrunkFlexionReverseTransfer = zeros(numtrials,numsubjects);
MaxAngleTrunkFlexionReverseTransfer = zeros(numtrials,numsubjects);
MinAngleTrunkFlexionReverseTransfer = zeros(numtrials,numsubjects);
ROMAngleTrunkFlexionReverseTransfer = zeros(numtrials,numsubjects);
AveAngleTrunkFlexionReverseTransfer = zeros(numtrials,numsubjects);

InitialAngleTrunkYHeadTransfer = zeros(numtrials,numsubjects);
MaxAngleTrunkYHeadTransfer = zeros(numtrials,numsubjects);
MinAngleTrunkYHeadTransfer = zeros(numtrials,numsubjects);
ROMAngleTrunkYHeadTransfer = zeros(numtrials,numsubjects);
AveAngleTrunkYHeadTransfer = zeros(numtrials,numsubjects);

InitialAngleTrunkYHeadReverseTransfer = zeros(numtrials,numsubjects);
MaxAngleTrunkYHeadReverseTransfer = zeros(numtrials,numsubjects);
MinAngleTrunkYHeadReverseTransfer = zeros(numtrials,numsubjects);
ROMAngleTrunkYHeadReverseTransfer = zeros(numtrials,numsubjects);
AveAngleTrunkYHeadReverseTransfer = zeros(numtrials,numsubjects);

InitialAngleTrunkYMidSpineTransfer = zeros(numtrials,numsubjects);
MaxAngleTrunkYMidSpineTransfer = zeros(numtrials,numsubjects);
MinAngleTrunkYMidSpineTransfer = zeros(numtrials,numsubjects);
ROMAngleTrunkYMidSpineTransfer = zeros(numtrials,numsubjects);
AveAngleTrunkYMidSpineTransfer = zeros(numtrials,numsubjects);

InitialAngleTrunkYMidSpineReverseTransfer = zeros(numtrials,numsubjects);
MaxAngleTrunkYMidSpineReverseTransfer = zeros(numtrials,numsubjects);
MinAngleTrunkYMidSpineReverseTransfer = zeros(numtrials,numsubjects);
ROMAngleTrunkYMidSpineReverseTransfer = zeros(numtrials,numsubjects);
AveAngleTrunkYMidSpineReverseTransfer = zeros(numtrials,numsubjects);

InitialAngleTrunkBendingRawTransfer = zeros(numtrials,numsubjects);
MaxAngleTrunkBendingRawTransfer = zeros(numtrials,numsubjects);
MinAngleTrunkBendingRawTransfer = zeros(numtrials,numsubjects);
ROMAngleTrunkBendingRawTransfer = zeros(numtrials,numsubjects);
AveAngleTrunkBendingRawTransfer = zeros(numtrials,numsubjects);  
  
InitialAngleTrunkBendingRawReverseTransfer = zeros(numtrials,numsubjects);
MaxAngleTrunkBendingRawReverseTransfer = zeros(numtrials,numsubjects);
MinAngleTrunkBendingRawReverseTransfer = zeros(numtrials,numsubjects);
ROMAngleTrunkBendingRawReverseTransfer = zeros(numtrials,numsubjects);
AveAngleTrunkBendingRawReverseTransfer = zeros(numtrials,numsubjects);

InitialAngleWristLeftFlexionTipTransfer = zeros(numtrials,numsubjects);
MaxAngleWristLeftFlexionTipTransfer = zeros(numtrials,numsubjects);
MinAngleWristLeftFlexionTipTransfer = zeros(numtrials,numsubjects);
ROMAngleWristLeftFlexionTipTransfer = zeros(numtrials,numsubjects);
AveAngleWristLeftFlexionTipTransfer = zeros(numtrials,numsubjects);
  
InitialAngleWristLeftFlexionTipReverseTransfer = zeros(numtrials,numsubjects);
MaxAngleWristLeftFlexionTipReverseTransfer = zeros(numtrials,numsubjects);
MinAngleWristLeftFlexionTipReverseTransfer = zeros(numtrials,numsubjects);
ROMAngleWristLeftFlexionTipReverseTransfer = zeros(numtrials,numsubjects);
AveAngleWristLeftFlexionTipReverseTransfer = zeros(numtrials,numsubjects);

InitialAngleWristRightFlexionTipTransfer = zeros(numtrials,numsubjects);
MaxAngleWristRightFlexionTipTransfer = zeros(numtrials,numsubjects);
MinAngleWristRightFlexionTipTransfer = zeros(numtrials,numsubjects);
ROMAngleWristRightFlexionTipTransfer = zeros(numtrials,numsubjects);
AveAngleWristRightFlexionTipTransfer = zeros(numtrials,numsubjects);
  
InitialAngleWristRightFlexionTipReverseTransfer = zeros(numtrials,numsubjects);
MaxAngleWristRightFlexionTipReverseTransfer = zeros(numtrials,numsubjects);
MinAngleWristRightFlexionTipReverseTransfer = zeros(numtrials,numsubjects);
ROMAngleWristRightFlexionTipReverseTransfer = zeros(numtrials,numsubjects);
AveAngleWristRightFlexionTipReverseTransfer = zeros(numtrials,numsubjects);

InitialAngleShoulderToWristLeftElevationTransfer = zeros(numtrials,numsubjects);
MaxAngleShoulderToWristLeftElevationTransfer = zeros(numtrials,numsubjects);
MinAngleShoulderToWristLeftElevationTransfer = zeros(numtrials,numsubjects);
ROMAngleShoulderToWristLeftElevationTransfer = zeros(numtrials,numsubjects);
AveAngleShoulderToWristLeftElevationTransfer = zeros(numtrials,numsubjects);
  
InitialAngleShoulderToWristLeftElevationReverseTransfer = zeros(numtrials,numsubjects);
MaxAngleShoulderToWristLeftElevationReverseTransfer = zeros(numtrials,numsubjects);
MinAngleShoulderToWristLeftElevationReverseTransfer = zeros(numtrials,numsubjects);
ROMAngleShoulderToWristLeftElevationReverseTransfer = zeros(numtrials,numsubjects);
AveAngleShoulderToWristLeftElevationReverseTransfer = zeros(numtrials,numsubjects);
  
InitialAngleShoulderToWristRightElevationTransfer = zeros(numtrials,numsubjects);
MaxAngleShoulderToWristRightElevationTransfer = zeros(numtrials,numsubjects);
MinAngleShoulderToWristRightElevationTransfer = zeros(numtrials,numsubjects);
ROMAngleShoulderToWristRightElevationTransfer = zeros(numtrials,numsubjects);
AveAngleShoulderToWristRightElevationTransfer = zeros(numtrials,numsubjects);
  
InitialAngleShoulderToWristRightElevationReverseTransfer = zeros(numtrials,numsubjects);
MaxAngleShoulderToWristRightElevationReverseTransfer = zeros(numtrials,numsubjects);
MinAngleShoulderToWristRightElevationReverseTransfer = zeros(numtrials,numsubjects);
ROMAngleShoulderToWristRightElevationReverseTransfer = zeros(numtrials,numsubjects);
AveAngleShoulderToWristRightElevationReverseTransfer = zeros(numtrials,numsubjects);
  
  
InitialAngleShoulderToWristLeftPOETransfer = zeros(numtrials,numsubjects);
MaxAngleShoulderToWristLeftPOETransfer  = zeros(numtrials,numsubjects);
MinAngleShoulderToWristLeftPOETransfer  = zeros(numtrials,numsubjects);
ROMAngleShoulderToWristLeftPOETransfer  = zeros(numtrials,numsubjects);
AveAngleShoulderToWristLeftPOETransfer  = zeros(numtrials,numsubjects);
  
InitialAngleShoulderToWristLeftPOEReverseTransfer  = zeros(numtrials,numsubjects);
MaxAngleShoulderToWristLeftPOEReverseTransfer  = zeros(numtrials,numsubjects);
MinAngleShoulderToWristLeftPOEReverseTransfer  = zeros(numtrials,numsubjects);
ROMAngleShoulderToWristLeftPOEReverseTransfer  = zeros(numtrials,numsubjects);
AveAngleShoulderToWristLeftPOEReverseTransfer  = zeros(numtrials,numsubjects);
  
InitialAngleShoulderToWristRightPOETransfer  = zeros(numtrials,numsubjects);
MaxAngleShoulderToWristRightPOETransfer  = zeros(numtrials,numsubjects);
MinAngleShoulderToWristRightPOETransfer  = zeros(numtrials,numsubjects);
ROMAngleShoulderToWristRightPOETransfer  = zeros(numtrials,numsubjects);
AveAngleShoulderToWristRightPOETransfer  = zeros(numtrials,numsubjects);
  
InitialAngleShoulderToWristRightPOEReverseTransfer  = zeros(numtrials,numsubjects);
MaxAngleShoulderToWristRightPOEReverseTransfer  = zeros(numtrials,numsubjects);
MinAngleShoulderToWristRightPOEReverseTransfer  = zeros(numtrials,numsubjects);
ROMAngleShoulderToWristRightPOEReverseTransfer  = zeros(numtrials,numsubjects);
AveAngleShoulderToWristRightPOEReverseTransfer  = zeros(numtrials,numsubjects);
  
InitialAngleTrunkRotationTVTransfer  = zeros(numtrials,numsubjects);
MaxAngleTrunkRotationTVTransfer  = zeros(numtrials,numsubjects);
MinAngleTrunkRotationTVTransfer  = zeros(numtrials,numsubjects);
ROMAngleTrunkRotationTVTransfer  = zeros(numtrials,numsubjects);
AveAngleTrunkRotationTVTransfer  = zeros(numtrials,numsubjects);  
  
InitialAngleTrunkRotationTVReverseTransfer  = zeros(numtrials,numsubjects);
MaxAngleTrunkRotationTVReverseTransfer  = zeros(numtrials,numsubjects);
MinAngleTrunkRotationTVReverseTransfer  = zeros(numtrials,numsubjects);
ROMAngleTrunkRotationTVReverseTransfer  = zeros(numtrials,numsubjects);
AveAngleTrunkRotationTVReverseTransfer  = zeros(numtrials,numsubjects);
 
InitialAngleTrunkBendingFrontalTransfer  = zeros(numtrials,numsubjects);
MaxAngleTrunkBendingFrontalTransfer  = zeros(numtrials,numsubjects);
MinAngleTrunkBendingFrontalTransfer  = zeros(numtrials,numsubjects);
ROMAngleTrunkBendingFrontalTransfer  = zeros(numtrials,numsubjects);
AveAngleTrunkBendingFrontalTransfer  = zeros(numtrials,numsubjects);
  
InitialAngleTrunkBendingFrontalReverseTransfer  = zeros(numtrials,numsubjects);
MaxAngleTrunkBendingFrontalReverseTransfer  = zeros(numtrials,numsubjects);
MinAngleTrunkBendingFrontalReverseTransfer  = zeros(numtrials,numsubjects);
ROMAngleTrunkBendingFrontalReverseTransfer  = zeros(numtrials,numsubjects);
AveAngleTrunkBendingFrontalReverseTransfer  = zeros(numtrials,numsubjects);

InitialAreaHandGripLeftTransfer = zeros(numtrials,numsubjects);
MaxAreaHandGripLeftTransfer = zeros(numtrials,numsubjects);
MinAreaHandGripLeftTransfer = zeros(numtrials,numsubjects);
ROMAreaHandGripLeftTransfer = zeros(numtrials,numsubjects);
AveAreaHandGripLeftTransfer = zeros(numtrials,numsubjects);
  
InitialAreaHandGripLeftReverseTransfer = zeros(numtrials,numsubjects);
MaxAreaHandGripLeftReverseTransfer = zeros(numtrials,numsubjects);
MinAreaHandGripLeftReverseTransfer = zeros(numtrials,numsubjects);
ROMAreaHandGripLeftReverseTransfer = zeros(numtrials,numsubjects);
AveAreaHandGripLeftReverseTransfer = zeros(numtrials,numsubjects);
  
InitialAreaHandGripRightTransfer = zeros(numtrials,numsubjects);
MaxAreaHandGripRightTransfer = zeros(numtrials,numsubjects);
MinAreaHandGripRightTransfer = zeros(numtrials,numsubjects);
ROMAreaHandGripRightTransfer = zeros(numtrials,numsubjects);
AveAreaHandGripRightTransfer = zeros(numtrials,numsubjects);
  
InitialAreaHandGripRightReverseTransfer = zeros(numtrials,numsubjects);
MaxAreaHandGripRightReverseTransfer = zeros(numtrials,numsubjects);
MinAreaHandGripRightReverseTransfer = zeros(numtrials,numsubjects);
ROMAreaHandGripRightReverseTransfer = zeros(numtrials,numsubjects);
AveAreaHandGripRightReverseTransfer = zeros(numtrials,numsubjects);

InitialHeadHipVelocityTransfer  = zeros(numtrials,numsubjects);
MaxHeadHipVelocityTransfer  = zeros(numtrials,numsubjects);
MinHeadHipVelocityTransfer  = zeros(numtrials,numsubjects);
ROMHeadHipVelocityTransfer  = zeros(numtrials,numsubjects);
AveHeadHipVelocityTransfer  = zeros(numtrials,numsubjects);
  
InitialHeadHipVelocityReverseTransfer  = zeros(numtrials,numsubjects);
MaxHeadHipVelocityReverseTransfer  = zeros(numtrials,numsubjects);
MinHeadHipVelocityReverseTransfer  = zeros(numtrials,numsubjects);
ROMHeadHipVelocityReverseTransfer  = zeros(numtrials,numsubjects);
AveHeadHipVelocityReverseTransfer  = zeros(numtrials,numsubjects);

InitialHeadHipAccelTransfer  = zeros(numtrials,numsubjects);
MaxHeadHipAccelTransfer  = zeros(numtrials,numsubjects);
MinHeadHipAccelTransfer  = zeros(numtrials,numsubjects);
ROMHeadHipAccelTransfer  = zeros(numtrials,numsubjects);
AveHeadHipAccelTransfer  = zeros(numtrials,numsubjects);
  
InitialHeadHipAccelReverseTransfer  = zeros(numtrials,numsubjects);
MaxHeadHipAccelReverseTransfer  = zeros(numtrials,numsubjects);
MinHeadHipAccelReverseTransfer  = zeros(numtrials,numsubjects);
ROMHeadHipAccelReverseTransfer  = zeros(numtrials,numsubjects);
AveHeadHipAccelReverseTransfer  = zeros(numtrials,numsubjects);



count = 1;

for i = 1:numsubjects
VariableFilePath = sprintf('%s%d%s%d', ConstantFilePath, i,'\S',i,'_LB');

for j = 1:numtrials
  myfilename = sprintf('%s%d%s', VariableFilePath ,j,'_P.csv');
  M = xlsread(myfilename);
  
  %% Acquire Major Matrix Attributes
  NumberRows=size(M,1);
  NumberColumns=size(M,2);
  
  %% Gather position point data
  time=(M(:,1));
timeTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),1));
timeReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),1));
timeTransferForHeadHip=(M((FirstTimePeriod(count)-11):SecondTimePeriod(count),1));
timeReverseTransferForHeadHip=(M((ThirdTimePeriod(count)-11):FourthTimePeriod(count),1));
  
SpineBaseXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),2));
SpineBaseXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),2));
SpineBaseYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),3));
SpineBaseYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),3));
SpineBaseZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),4));
SpineBaseZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),4));

SpineBaseXTransferForHeadHip=(M((FirstTimePeriod(count)-11):SecondTimePeriod(count),2));
SpineBaseXReverseTransferForHeadHip=(M((ThirdTimePeriod(count)-11):FourthTimePeriod(count),2));
SpineBaseYTransferForHeadHip=(M((FirstTimePeriod(count)-11):SecondTimePeriod(count),3));
SpineBaseYReverseTransferForHeadHip=(M((ThirdTimePeriod(count)-11):FourthTimePeriod(count),3));
SpineBaseZTransferForHeadHip=(M((FirstTimePeriod(count)-11):SecondTimePeriod(count),4));
SpineBaseZReverseTransferForHeadHip=(M((ThirdTimePeriod(count)-11):FourthTimePeriod(count),4));

SpineMidXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),5));
SpineMidXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),5));
SpineMidYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),6));
SpineMidYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),6));
SpineMidZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),7));
SpineMidZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),7));

NeckXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),8));
NeckXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),8));
NeckYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),9));
NeckYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),9));
NeckZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),10));
NeckZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),10));

HeadXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),11));
HeadXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),11));
HeadYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),12));
HeadYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),12));
HeadZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),13));
HeadZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),13));

HeadXTransferForHeadHip=(M((FirstTimePeriod(count)-11):SecondTimePeriod(count),11));
HeadXReverseTransferForHeadHip=(M((ThirdTimePeriod(count)-11):FourthTimePeriod(count),11));
HeadYTransferForHeadHip=(M((FirstTimePeriod(count)-11):SecondTimePeriod(count),12));
HeadYReverseTransferForHeadHip=(M((ThirdTimePeriod(count)-11):FourthTimePeriod(count),12));
HeadZTransferForHeadHip=(M((FirstTimePeriod(count)-11):SecondTimePeriod(count),13));
HeadZReverseTransferForHeadHip=(M((ThirdTimePeriod(count)-11):FourthTimePeriod(count),13));

ShoulderLeftXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),14));
ShoulderLeftXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),14));
ShoulderLeftYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),15));
ShoulderLeftYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),15));
ShoulderLeftZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),16));
ShoulderLeftZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),16));

ElbowLeftXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),17));
ElbowLeftXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),17));
ElbowLeftYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),18));
ElbowLeftYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),18));
ElbowLeftZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),19));
ElbowLeftZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),19));

WristLeftXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),20));
WristLeftXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),20));
WristLeftYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),21));
WristLeftYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),21));
WristLeftZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),22));
WristLeftZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),22));

HandLeftXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),23));
HandLeftXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),23));
HandLeftYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),24));
HandLeftYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),24));
HandLeftZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),25));
HandLeftZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),25));

ShoulderRightXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),26));
ShoulderRightXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),26));
ShoulderRightYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),27));
ShoulderRightYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),27));
ShoulderRightZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),28));
ShoulderRightZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),28));

ElbowRightXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),29));
ElbowRightXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),29));
ElbowRightYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),30));
ElbowRightYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),30));
ElbowRightZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),31));
ElbowRightZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),31));

WristRightXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),32));
WristRightXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),32));
WristRightYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),33));
WristRightYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),33));
WristRightZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),34));
WristRightZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),34));

HandRightXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),35));
HandRightXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),35));
HandRightYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),36));
HandRightYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),36));
HandRightZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),37));
HandRightZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),37));

HipLeftXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),38));
HipLeftXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),38));
HipLeftYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),39));
HipLeftYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),39));
HipLeftZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),40));
HipLeftZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),40));

KneeLeftXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),41));
KneeLeftXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),41));
KneeLeftYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),42));
KneeLeftYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),42));
KneeLeftZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),43));
KneeLeftZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),43));

AnkleLeftXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),44));
AnkleLeftXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),44));
AnkleLeftYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),45));
AnkleLeftYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),45));
AnkleLeftZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),46));
AnkleLeftZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),46));

FootLeftXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),47));
FootLeftXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),47));
FootLeftYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),48));
FootLeftYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),48));
FootLeftZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),49));
FootLeftZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),49));

HipRightXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),50));
HipRightXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),50));
HipRightYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),51));
HipRightYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),51));
HipRightZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),52));
HipRightZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),52));

KneeRightXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),53));
KneeRightXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),53));
KneeRightYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),54));
KneeRightYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),54));
KneeRightZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),55));
KneeRightZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),55));

AnkleRightXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),56));
AnkleRightXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),56));
AnkleRightYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),57));
AnkleRightYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),57));
AnkleRightZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),58));
AnkleRightZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),58));

FootRightXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),59));
FootRightXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),59));
FootRightYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),60));
FootRightYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),60));
FootRightZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),61));
FootRightZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),61));

SpineShoulderXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),62));
SpineShoulderXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),62));
SpineShoulderYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),63));
SpineShoulderYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),63));
SpineShoulderZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),64));
SpineShoulderZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),64));

HandTipLeftXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),65));
HandTipLeftXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),65));
HandTipLeftYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),66));
HandTipLeftYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),66));
HandTipLeftZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),67));
HandTipLeftZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),67));

ThumbLeftXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),68));
ThumbLeftXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),68));
ThumbLeftYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),69));
ThumbLeftYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),69));
ThumbLeftZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),70));
ThumbLeftZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),70));

HandTipRightXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),71));
HandTipRightXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),71));
HandTipRightYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),72));
HandTipRightYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),72));
HandTipRightZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),73));
HandTipRightZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),73));

ThumbRightXTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),74));
ThumbRightXReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),74));
ThumbRightYTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),75));
ThumbRightYReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),75));
ThumbRightZTransfer=(M((FirstTimePeriod(count)-9):SecondTimePeriod(count),76));
ThumbRightZReverseTransfer=(M((ThirdTimePeriod(count)-9):FourthTimePeriod(count),76));
  
  %% Assign vectors from positions
  
VectorX=zeros(NumberRows,3);
VectorX(:,1)=1;
VectorY=zeros(NumberRows,3);
VectorY(:,2)=1;
VectorZ=zeros(NumberRows,3);
VectorZ(:,3)=1;

% VectorTrunkTransfer=[(SpineShoulderXTransfer-SpineBaseXTransfer),(SpineShoulderYTransfer-SpineBaseYTransfer),(SpineShoulderZTransfer-SpineBaseZ1)];
% VectorTrunkReverseTransfer=[(SpineShoulderXReverseTransfer-SpineBaseXReverseTransfer),(SpineShoulderYReverseTransfer-SpineBaseYReverseTransfer),(SpineShoulderZReverseTransfer-SpineBaseZReverseTransfer)];
% VectorTrunk was re-defined due to SpineBase instability
VectorTrunkTransfer=[(SpineShoulderXTransfer-SpineMidXTransfer),(SpineShoulderYTransfer-SpineMidYTransfer),(SpineShoulderZTransfer-SpineMidZTransfer)];
VectorTrunkReverseTransfer=[(SpineShoulderXReverseTransfer-SpineMidXReverseTransfer),(SpineShoulderYReverseTransfer-SpineMidYReverseTransfer),(SpineShoulderZReverseTransfer-SpineMidZReverseTransfer)];
VectorShoulderLeftTransfer=[(ShoulderLeftXTransfer-SpineShoulderXTransfer),(ShoulderLeftYTransfer-SpineShoulderYTransfer),(ShoulderLeftZTransfer-SpineShoulderZTransfer)];
VectorShoulderLeftReverseTransfer=[(ShoulderLeftXReverseTransfer-SpineShoulderXReverseTransfer),(ShoulderLeftYReverseTransfer-SpineShoulderYReverseTransfer),(ShoulderLeftZReverseTransfer-SpineShoulderZReverseTransfer)];
VectorShoulderRightTransfer=[(ShoulderRightXTransfer-SpineShoulderXTransfer),(ShoulderRightYTransfer-SpineShoulderYTransfer),(ShoulderRightZTransfer-SpineShoulderZTransfer)];
VectorShoulderRightReverseTransfer=[(ShoulderRightXReverseTransfer-SpineShoulderXReverseTransfer),(ShoulderRightYReverseTransfer-SpineShoulderYReverseTransfer),(ShoulderRightZReverseTransfer-SpineShoulderZReverseTransfer)];
VectorShoulderAcrossTransfer=[(ShoulderRightXTransfer-ShoulderLeftXTransfer),(ShoulderRightYTransfer-ShoulderLeftYTransfer),(ShoulderRightZTransfer-ShoulderLeftZTransfer)];
VectorShoulderAcrossReverseTransfer=[(ShoulderRightXReverseTransfer-ShoulderLeftXReverseTransfer),(ShoulderRightYReverseTransfer-ShoulderLeftYReverseTransfer),(ShoulderRightZReverseTransfer-ShoulderLeftZReverseTransfer)];
VectorHipAcrossTransfer=[(HipRightXTransfer-HipLeftXTransfer),(HipRightYTransfer-HipLeftYTransfer),(HipRightZTransfer-HipLeftZTransfer)];
VectorHipAcrossReverseTransfer=[(HipRightXReverseTransfer-HipLeftXReverseTransfer),(HipRightYReverseTransfer-HipLeftYReverseTransfer),(HipRightZReverseTransfer-HipLeftZReverseTransfer)];
VectorUpperArmLeftTransfer=[(ElbowLeftXTransfer-ShoulderLeftXTransfer),(ElbowLeftYTransfer-ShoulderLeftYTransfer),(ElbowLeftZTransfer-ShoulderLeftZTransfer)];
VectorUpperArmLeftReverseTransfer=[(ElbowLeftXReverseTransfer-ShoulderLeftXReverseTransfer),(ElbowLeftYReverseTransfer-ShoulderLeftYReverseTransfer),(ElbowLeftZReverseTransfer-ShoulderLeftZReverseTransfer)];
VectorUpperArmRightTransfer=[(ElbowRightXTransfer-ShoulderRightXTransfer),(ElbowRightYTransfer-ShoulderRightYTransfer),(ElbowRightZTransfer-ShoulderRightZTransfer)];
VectorUpperArmRightReverseTransfer=[(ElbowRightXReverseTransfer-ShoulderRightXReverseTransfer),(ElbowRightYReverseTransfer-ShoulderRightYReverseTransfer),(ElbowRightZReverseTransfer-ShoulderRightZReverseTransfer)];
VectorForearmLeftTransfer=[(WristLeftXTransfer-ElbowLeftXTransfer),(WristLeftYTransfer-ElbowLeftYTransfer),(WristLeftZTransfer-ElbowLeftZTransfer)];
VectorForearmLeftReverseTransfer=[(WristLeftXReverseTransfer-ElbowLeftXReverseTransfer),(WristLeftYReverseTransfer-ElbowLeftYReverseTransfer),(WristLeftZReverseTransfer-ElbowLeftZReverseTransfer)];
VectorForearmRightTransfer=[(WristRightXTransfer-ElbowRightXTransfer),(WristRightYTransfer-ElbowRightYTransfer),(WristRightZTransfer-ElbowRightZTransfer)];
VectorForearmRightReverseTransfer=[(WristRightXReverseTransfer-ElbowRightXReverseTransfer),(WristRightYReverseTransfer-ElbowRightYReverseTransfer),(WristRightZReverseTransfer-ElbowRightZReverseTransfer)];
VectorShoulderToWristLeftTransfer=[(WristLeftXTransfer-ShoulderLeftXTransfer),(WristLeftYTransfer-ShoulderLeftYTransfer),(WristLeftZTransfer-ShoulderLeftZTransfer)];
VectorShoulderToWristLeftReverseTransfer=[(WristLeftXReverseTransfer-ShoulderLeftXReverseTransfer),(WristLeftYReverseTransfer-ShoulderLeftYReverseTransfer),(WristLeftZReverseTransfer-ShoulderLeftZReverseTransfer)];
VectorShoulderToWristRightTransfer=[(WristRightXTransfer-ShoulderRightXTransfer),(WristRightYTransfer-ShoulderRightYTransfer),(WristRightZTransfer-ShoulderRightZTransfer)];
VectorShoulderToWristRightReverseTransfer=[(WristRightXReverseTransfer-ShoulderRightXReverseTransfer),(WristRightYReverseTransfer-ShoulderRightYReverseTransfer),(WristRightZReverseTransfer-ShoulderRightZReverseTransfer)];
VectorHandLeftTransfer=[(HandLeftXTransfer-WristLeftXTransfer),(HandLeftYTransfer-WristLeftYTransfer),(HandLeftZTransfer-WristLeftZTransfer)];
VectorHandLeftReverseTransfer=[(HandLeftXReverseTransfer-WristLeftXReverseTransfer),(HandLeftYReverseTransfer-WristLeftYReverseTransfer),(HandLeftZReverseTransfer-WristLeftZReverseTransfer)];
VectorHandRightTransfer=[(HandRightXTransfer-WristRightXTransfer),(HandRightYTransfer-WristRightYTransfer),(HandRightZTransfer-WristRightZTransfer)];
VectorHandRightReverseTransfer=[(HandRightXReverseTransfer-WristRightXReverseTransfer),(HandRightYReverseTransfer-WristRightYReverseTransfer),(HandRightZReverseTransfer-WristRightZReverseTransfer)];
VectorWristToThumbLeftTransfer=[(ThumbLeftXTransfer-WristLeftXTransfer),(ThumbLeftYTransfer-WristLeftYTransfer),(ThumbLeftZTransfer-WristLeftZTransfer)];
VectorWristToThumbLeftReverseTransfer=[(ThumbLeftXReverseTransfer-WristLeftXReverseTransfer),(ThumbLeftYReverseTransfer-WristLeftYReverseTransfer),(ThumbLeftZReverseTransfer-WristLeftZReverseTransfer)];
VectorWristToThumbRightTransfer=[(ThumbRightXTransfer-WristRightXTransfer),(ThumbRightYTransfer-WristRightYTransfer),(ThumbRightZTransfer-WristRightZTransfer)];
VectorWristToThumbRightReverseTransfer=[(ThumbRightXReverseTransfer-WristRightXReverseTransfer),(ThumbRightYReverseTransfer-WristRightYReverseTransfer),(ThumbRightZReverseTransfer-WristRightZReverseTransfer)];
VectorWristToHandTipLeftTransfer=[(HandTipLeftXTransfer-WristLeftXTransfer),(HandTipLeftYTransfer-WristLeftYTransfer),(HandTipLeftZTransfer-WristLeftZTransfer)];
VectorWristToHandTipLeftReverseTransfer=[(HandTipLeftXReverseTransfer-WristLeftXReverseTransfer),(HandTipLeftYReverseTransfer-WristLeftYReverseTransfer),(HandTipLeftZReverseTransfer-WristLeftZReverseTransfer)];
VectorWristToHandTipRightTransfer=[(HandTipRightXTransfer-WristRightXTransfer),(HandTipRightYTransfer-WristRightYTransfer),(HandTipRightZTransfer-WristRightZTransfer)];
VectorWristToHandTipRightReverseTransfer=[(HandTipRightXReverseTransfer-WristRightXReverseTransfer),(HandTipRightYReverseTransfer-WristRightYReverseTransfer),(HandTipRightZReverseTransfer-WristRightZReverseTransfer)];
VectorThighLeftTransfer=[(KneeLeftXTransfer-HipLeftXTransfer),(KneeLeftYTransfer-HipLeftYTransfer),(KneeLeftZTransfer-HipLeftZTransfer)];
VectorThighLeftReverseTransfer=[(KneeLeftXReverseTransfer-HipLeftXReverseTransfer),(KneeLeftYReverseTransfer-HipLeftYReverseTransfer),(KneeLeftZReverseTransfer-HipLeftZReverseTransfer)];
VectorThighRightTransfer=[(KneeRightXTransfer-HipRightXTransfer),(KneeRightYTransfer-HipRightYTransfer),(KneeRightZTransfer-HipRightZTransfer)];
VectorThighRightReverseTransfer=[(KneeRightXReverseTransfer-HipRightXReverseTransfer),(KneeRightYReverseTransfer-HipRightYReverseTransfer),(KneeRightZReverseTransfer-HipRightZReverseTransfer)];
VectorHeadHipTransfer=[(HeadXTransfer-SpineBaseXTransfer),(HeadYTransfer-SpineBaseYTransfer),(HeadZTransfer-SpineBaseZTransfer)];
VectorHeadHipReverseTransfer=[(HeadXReverseTransfer-SpineBaseXReverseTransfer),(HeadYReverseTransfer-SpineBaseYReverseTransfer),(HeadZReverseTransfer-SpineBaseZReverseTransfer)];
VectorMidSpineHipTransfer=[(SpineMidXTransfer-SpineBaseXTransfer),(SpineMidYTransfer-SpineBaseYTransfer),(SpineMidZTransfer-SpineBaseZTransfer)];
VectorMidSpineHipReverseTransfer=[(SpineMidXReverseTransfer-SpineBaseXReverseTransfer),(SpineMidYReverseTransfer-SpineBaseYReverseTransfer),(SpineMidZReverseTransfer-SpineBaseZReverseTransfer)];

VectorHeadHipTransferForHeadHip=[(HeadXTransferForHeadHip-SpineBaseXTransferForHeadHip),(HeadYTransferForHeadHip-SpineBaseYTransferForHeadHip),(HeadZTransferForHeadHip-SpineBaseZTransferForHeadHip)];
VectorHeadHipReverseTransferForHeadHip=[(HeadXReverseTransferForHeadHip-SpineBaseXReverseTransferForHeadHip),(HeadYReverseTransferForHeadHip-SpineBaseYReverseTransferForHeadHip),(HeadZReverseTransferForHeadHip-SpineBaseZReverseTransferForHeadHip)];

% VectorHeadHip=[(HeadX-SpineBaseX),(HeadY-SpineBaseY),(HeadZ-SpineBaseZ)];
% VectorMidSpineHip=[(SpineMidX-SpineBaseX),(SpineMidY-SpineBaseY),(SpineMidZ-SpineBaseZ)];

VectorTrunkAnteriorTransfer=cross(VectorTrunkTransfer,VectorShoulderAcrossTransfer);
VectorTrunkAnteriorReverseTransfer=cross(VectorTrunkReverseTransfer,VectorShoulderAcrossReverseTransfer);
%% The Head-Hip Velocity and Acceleration Calculations  
% HeadHipVelocityTransfer=zeros(1,NumberRowsTransfer-1);
% HeadHipVelocityReverseTransfer=zeros(1,NumberRowsReverseTransfer-1);
% 
% count2=count+1;
% HeadHipVelocityTransfer(count)=norm(VectorHeadHipTransfer(count2,1:3)-VectorHeadHipTransfer(count,1:3))/(time(count2)-time(count));
% HeadHipVelocityReverseTransfer(count)=norm(VectorHeadHipReverseTransfer(count2,1:3)-VectorHeadHipReverseTransfer(count,1:3))/(time(count2)-time(count));
% 
% HeadHipAccelTransfer=zeros(1,numel(HeadHipVelocityTransfer)-1);
% HeadHipAccelReverseTransfer=zeros(1,numel(HeadHipVelocityReverseTransfer)-1);
% 
% HeadHipAccelTransfer(count)=(HeadHipVelocityTransfer(count2)-HeadHipVelocityTransfer(count))/(time(count2)-time(count));
% HeadHipAccelReverseTransfer(count)=(HeadHipVelocityReverseTransfer(count2)-HeadHipVelocityReverseTransfer(count))/(time(count2)-time(count));


  
  %% Initialize Angle Data Storage
  NumberRowsTransfer = numel((FirstTimePeriod(count)-9):SecondTimePeriod(count));
  NumberRowsReverseTransfer = numel((ThirdTimePeriod(count)-9):FourthTimePeriod(count));
  
  
AngleShoulderLeftPOETransfer=zeros(NumberRowsTransfer,1);
AngleShoulderRightPOETransfer=zeros(NumberRowsTransfer,1);
AngleShoulderLeftPOEReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleShoulderRightPOEReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleShoulderLeftElevationTransfer=zeros(NumberRowsTransfer,1);
AngleShoulderRightElevationTransfer=zeros(NumberRowsTransfer,1);
AngleShoulderLeftElevationReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleShoulderRightElevationReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleElbowLeftFlexionTransfer=zeros(NumberRowsTransfer,1);
AngleElbowRightFlexionTransfer=zeros(NumberRowsTransfer,1);
AngleElbowLeftFlexionReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleElbowRightFlexionReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleWristLeftFlexionTransfer=zeros(NumberRowsTransfer,1);
AngleWristRightFlexionTransfer=zeros(NumberRowsTransfer,1);
AngleWristLeftFlexionReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleWristRightFlexionReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleTrunkFlexionTransfer=zeros(NumberRowsTransfer,1);
AngleTrunkFlexionReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleTrunkYHeadTransfer=zeros(NumberRowsTransfer,1);
AngleTrunkYHeadReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleTrunkYMidSpineTransfer=zeros(NumberRowsTransfer,1);
AngleTrunkYMidSpineReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleTrunkBendingRawTransfer=zeros(NumberRowsTransfer,1);
AngleTrunkBendingRawReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleWristLeftFlexionTipTransfer=zeros(NumberRowsTransfer,1);
AngleWristLeftFlexionTipReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleWristRightFlexionTipTransfer=zeros(NumberRowsTransfer,1);
AngleWristRightFlexionTipReverseTransfer=zeros(NumberRowsReverseTransfer,1);

AngleShoulderToWristLeftElevationTransfer=zeros(NumberRowsTransfer,1);
AngleShoulderToWristRightElevationTransfer=zeros(NumberRowsTransfer,1);
AngleShoulderToWristLeftElevationReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleShoulderToWristRightElevationReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleShoulderToWristLeftPOETransfer=zeros(NumberRowsTransfer,1);
AngleShoulderToWristRightPOETransfer=zeros(NumberRowsTransfer,1);
AngleShoulderToWristLeftPOEReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleShoulderToWristRightPOEReverseTransfer=zeros(NumberRowsReverseTransfer,1);

VectorShoulderAcrossTVTransfer=zeros(NumberRowsTransfer,3);
VectorShoulderAcrossTVReverseTransfer=zeros(NumberRowsReverseTransfer,3);
VectorHipAcrossTVTransfer=zeros(NumberRowsTransfer,3);
VectorHipAcrossTVReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleTrunkRotationTVTransfer=zeros(1,NumberRowsTransfer);
AngleTrunkRotationTVReverseTransfer=zeros(NumberRowsReverseTransfer,1);
VectorShoulderAcrossFrontalTransfer=zeros(NumberRowsTransfer,3);
VectorShoulderAcrossFrontalReverseTransfer=zeros(NumberRowsReverseTransfer,1);
VectorHipAcrossFrontalTransfer=zeros(NumberRowsTransfer,3);
VectorHipAcrossFrontalReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AngleTrunkBendingFrontalTransfer=zeros(1,NumberRowsTransfer);
AngleTrunkBendingFrontalReverseTransfer=zeros(NumberRowsReverseTransfer,1);

AreaHandGripLeftTransfer=zeros(1,NumberRowsTransfer);
AreaHandGripRightTransfer=zeros(1,NumberRowsTransfer);
AreaHandGripLeftReverseTransfer=zeros(NumberRowsReverseTransfer,1);
AreaHandGripRightReverseTransfer=zeros(NumberRowsReverseTransfer,1);



%% Collect head-hip velocity and acceleration data

% HeadHipVelocityTransfer=zeros(1,NumberRowsTransfer-1);
% HeadHipVelocityReverseTransfer=zeros(1,NumberRowsReverseTransfer-1);
% 
% for i=1:NumberRowsTransfer-1
% m=i+1;
% HeadHipVelocityTransfer(i)=norm(VectorHeadHipTransfer(m,1:3)-VectorHeadHipTransfer(i,1:3))/(time(m)-time(i));
% end
% 
% for i=1:NumberRowsReverseTransfer-1
% m=i+1;
% HeadHipVelocityReverseTransfer(i)=norm(VectorHeadHipReverseTransfer(m,1:3)-VectorHeadHipReverseTransfer(i,1:3))/(time(m)-time(i));
% end
% 
% 
% HeadHipAccelTransfer=zeros(1,numel(HeadHipVelocity)-1);
% HeadHipAccelReverseTransfer=zeros(1,numel(HeadHipVelocity)-1);
% 
% 
% for i=1:numel(HeadHipVelocity)-1
% m=i+1;
% HeadHipAccel(i)=(HeadHipVelocity(m)-HeadHipVelocity(i))/(time(m)-time(i));
% end

HeadHipVelocityTransfer=zeros(1,NumberRowsTransfer+2);
HeadHipVelocityReverseTransfer=zeros(1,NumberRowsReverseTransfer+2);
HeadHipAccelTransfer=zeros(1,NumberRowsTransfer+2);
HeadHipAccelReverseTransfer=zeros(1,NumberRowsReverseTransfer+2);

for p=1:NumberRowsTransfer+1
q=p+1;
HeadHipVelocityTransfer(q)=norm(VectorHeadHipTransferForHeadHip(q,1:3)-VectorHeadHipTransferForHeadHip(p,1:3))/(timeTransferForHeadHip(q)-timeTransferForHeadHip(p));
end

for p=1:NumberRowsReverseTransfer+1
q=p+1;
HeadHipVelocityReverseTransfer(q)=norm(VectorHeadHipReverseTransferForHeadHip(q,1:3)-VectorHeadHipReverseTransferForHeadHip(p,1:3))/(timeReverseTransferForHeadHip(q)-timeReverseTransferForHeadHip(p));
end

for p=1:NumberRowsTransfer
q=p+1;
r=q+1;
HeadHipAccelTransfer(r)=(HeadHipVelocityTransfer(r)-HeadHipVelocityTransfer(q))/(timeTransferForHeadHip(r)-timeTransferForHeadHip(q));
end

for p=1:NumberRowsReverseTransfer
q=p+1;
r=q+1;
HeadHipAccelReverseTransfer(r)=(HeadHipVelocityReverseTransfer(r)-HeadHipVelocityReverseTransfer(q))/(timeReverseTransferForHeadHip(r)-timeReverseTransferForHeadHip(q));
end

HeadHipVelocityTransfer=HeadHipVelocityTransfer(3:end);
HeadHipVelocityReverseTransfer=HeadHipVelocityReverseTransfer(3:end);
HeadHipAccelTransfer=HeadHipAccelTransfer(3:end);
HeadHipAccelReverseTransfer=HeadHipAccelReverseTransfer(3:end);

  %% Fill in Angle Data
  for k=1:NumberRowsTransfer
      
  AngleShoulderLeftPOETransfer(k) = acosd(dot(VectorTrunkAnteriorTransfer(k,1:3), VectorUpperArmLeftTransfer(k,1:3)) / (norm(VectorTrunkAnteriorTransfer(k,1:3)) * norm(VectorUpperArmLeftTransfer(k,1:3))));
  AngleShoulderRightPOETransfer(k) = acosd(dot(VectorTrunkAnteriorTransfer(k,1:3), VectorUpperArmRightTransfer(k,1:3)) / (norm(VectorTrunkAnteriorTransfer(k,1:3)) * norm(VectorUpperArmRightTransfer(k,1:3))));
  AngleShoulderLeftElevationTransfer(k) = acosd(dot(-VectorTrunkTransfer(k,1:3), VectorUpperArmLeftTransfer(k,1:3)) / (norm(-VectorTrunkTransfer(k,1:3)) * norm(VectorUpperArmLeftTransfer(k,1:3))));
  AngleShoulderRightElevationTransfer(k) = acosd(dot(-VectorTrunkTransfer(k,1:3), VectorUpperArmRightTransfer(k,1:3)) / (norm(-VectorTrunkTransfer(k,1:3)) * norm(VectorUpperArmRightTransfer(k,1:3))));
  AngleElbowLeftFlexionTransfer(k) = acosd(dot(VectorUpperArmLeftTransfer(k,1:3), VectorForearmLeftTransfer(k,1:3)) / (norm(VectorUpperArmLeftTransfer(k,1:3)) * norm(VectorForearmLeftTransfer(k,1:3))));
  AngleElbowRightFlexionTransfer(k) = acosd(dot(VectorUpperArmRightTransfer(k,1:3), VectorForearmRightTransfer(k,1:3)) / (norm(VectorUpperArmRightTransfer(k,1:3)) * norm(VectorForearmRightTransfer(k,1:3))));
  AngleWristLeftFlexionTransfer(k) = acosd(dot(VectorForearmLeftTransfer(k,1:3), VectorHandLeftTransfer(k,1:3)) / (norm(VectorForearmLeftTransfer(k,1:3)) * norm(VectorHandLeftTransfer(k,1:3))));
  AngleWristRightFlexionTransfer(k) = acosd(dot(VectorForearmRightTransfer(k,1:3), VectorHandRightTransfer(k,1:3)) / (norm(VectorForearmRightTransfer(k,1:3)) * norm(VectorHandRightTransfer(k,1:3))));
  AngleTrunkFlexionTransfer(k) = acosd(dot(VectorTrunkTransfer(k,1:3), VectorThighLeftTransfer(k,1:3)) / (norm(VectorTrunkTransfer(k,1:3)) * norm(VectorThighLeftTransfer(k,1:3))));
  AngleTrunkYHeadTransfer(k) = acosd(dot(VectorY(k,1:3), VectorHeadHipTransfer(k,1:3)) / (norm(VectorY(k,1:3)) * norm(VectorHeadHipTransfer(k,1:3))));
  AngleTrunkYMidSpineTransfer(k) = acosd(dot(VectorY(k,1:3), VectorMidSpineHipTransfer(k,1:3)) / (norm(VectorY(k,1:3)) * norm(VectorMidSpineHipTransfer(k,1:3))));
  AngleTrunkBendingRawTransfer(k) = acosd(dot(VectorShoulderAcrossTransfer(k,1:3), VectorHipAcrossTransfer(k,1:3)) / (norm(VectorShoulderAcrossTransfer(k,1:3)) * norm(VectorHipAcrossTransfer(k,1:3))));
  AngleWristLeftFlexionTipTransfer(k) = acosd(dot(VectorForearmLeftTransfer(k,1:3), VectorWristToHandTipLeftTransfer(k,1:3)) / (norm(VectorForearmLeftTransfer(k,1:3)) * norm(VectorWristToHandTipLeftTransfer(k,1:3))));
  AngleWristRightFlexionTipTransfer(k) = acosd(dot(VectorForearmRightTransfer(k,1:3), VectorWristToHandTipRightTransfer(k,1:3)) / (norm(VectorForearmRightTransfer(k,1:3)) * norm(VectorWristToHandTipRightTransfer(k,1:3))));
  AngleShoulderToWristLeftElevationTransfer(k) = acosd(dot(-VectorTrunkTransfer(k,1:3), VectorShoulderToWristLeftTransfer(k,1:3)) / (norm(-VectorTrunkTransfer(k,1:3)) * norm(VectorShoulderToWristLeftTransfer(k,1:3))));
  AngleShoulderToWristRightElevationTransfer(k) = acosd(dot(-VectorTrunkTransfer(k,1:3), VectorShoulderToWristRightTransfer(k,1:3)) / (norm(-VectorTrunkTransfer(k,1:3)) * norm(VectorShoulderToWristRightTransfer(k,1:3))));
  AngleShoulderToWristLeftPOETransfer(k) = acosd(dot(VectorTrunkAnteriorTransfer(k,1:3), VectorShoulderToWristLeftTransfer(k,1:3)) / (norm(VectorTrunkAnteriorTransfer(k,1:3)) * norm(VectorShoulderToWristLeftTransfer(k,1:3))));
  AngleShoulderToWristRightPOETransfer(k) = acosd(dot(VectorTrunkAnteriorTransfer(k,1:3), VectorShoulderToWristRightTransfer(k,1:3)) / (norm(VectorTrunkAnteriorTransfer(k,1:3)) * norm(VectorShoulderToWristRightTransfer(k,1:3))));
  
  
  VectorShoulderAcrossTVTransfer(k,1:3) = VectorShoulderAcrossTransfer(k,1:3) -(dot(VectorShoulderAcrossTransfer(k,1:3),VectorTrunkTransfer(k,1:3))/(norm(VectorTrunkTransfer(k,1:3)))^2)*VectorTrunkTransfer(k,1:3);
  VectorHipAcrossTVTransfer(k,1:3) = VectorHipAcrossTransfer(k,1:3) -(dot(VectorHipAcrossTransfer(k,1:3),VectorTrunkTransfer(k,1:3))/(norm(VectorTrunkTransfer(k,1:3)))^2)*VectorTrunkTransfer(k,1:3);
  AngleTrunkRotationTVTransfer(k)=acosd(dot(VectorShoulderAcrossTVTransfer(k,1:3), VectorHipAcrossTVTransfer(k,1:3)) / (norm(VectorShoulderAcrossTVTransfer(k,1:3)) * norm(VectorHipAcrossTVTransfer(k,1:3))));

  VectorShoulderAcrossFrontalTransfer(k,1:3) = VectorShoulderAcrossTransfer(k,1:3) -(dot(VectorShoulderAcrossTransfer(k,1:3),VectorTrunkAnteriorTransfer(k,1:3))/(norm(VectorTrunkAnteriorTransfer(k,1:3)))^2)*VectorTrunkAnteriorTransfer(k,1:3);
  VectorHipAcrossFrontalTransfer(k,1:3) = VectorHipAcrossTransfer(k,1:3) -(dot(VectorHipAcrossTransfer(k,1:3),VectorTrunkAnteriorTransfer(k,1:3))/(norm(VectorTrunkAnteriorTransfer(k,1:3)))^2)*VectorTrunkAnteriorTransfer(k,1:3);
  AngleTrunkBendingFrontalTransfer(k)=acosd(dot(VectorShoulderAcrossFrontalTransfer(k,1:3), VectorHipAcrossFrontalTransfer(k,1:3)) / (norm(VectorShoulderAcrossFrontalTransfer(k,1:3)) * norm(VectorHipAcrossFrontalTransfer(k,1:3))));
  
  AreaHandGripLeftTransfer(k) = 0.5*norm(cross(VectorWristToThumbLeftTransfer(k,1:3), VectorWristToHandTipLeftTransfer(k,1:3)));
  AreaHandGripRightTransfer(k) = 0.5*norm(cross(VectorWristToThumbRightTransfer(k,1:3), VectorWristToHandTipRightTransfer(k,1:3)));
 
  end
  
  for m=1:NumberRowsReverseTransfer
  AngleShoulderLeftPOEReverseTransfer(m) = acosd(dot(VectorTrunkAnteriorReverseTransfer(m,1:3), VectorUpperArmLeftReverseTransfer(m,1:3)) / (norm(VectorTrunkAnteriorReverseTransfer(m,1:3)) * norm(VectorUpperArmLeftReverseTransfer(m,1:3))));  
  AngleShoulderRightPOEReverseTransfer(m) = acosd(dot(VectorTrunkAnteriorReverseTransfer(m,1:3), VectorUpperArmRightReverseTransfer(m,1:3)) / (norm(VectorTrunkAnteriorReverseTransfer(m,1:3)) * norm(VectorUpperArmRightReverseTransfer(m,1:3))));  
  AngleShoulderLeftElevationReverseTransfer(m) = acosd(dot(-VectorTrunkReverseTransfer(m,1:3), VectorUpperArmLeftReverseTransfer(m,1:3)) / (norm(-VectorTrunkReverseTransfer(m,1:3)) * norm(VectorUpperArmLeftReverseTransfer(m,1:3))));  
  AngleShoulderRightElevationReverseTransfer(m) = acosd(dot(-VectorTrunkReverseTransfer(m,1:3), VectorUpperArmRightReverseTransfer(m,1:3)) / (norm(-VectorTrunkReverseTransfer(m,1:3)) * norm(VectorUpperArmRightReverseTransfer(m,1:3))));  
  AngleElbowLeftFlexionReverseTransfer(m) = acosd(dot(VectorUpperArmLeftReverseTransfer(m,1:3), VectorForearmLeftReverseTransfer(m,1:3)) / (norm(VectorUpperArmLeftReverseTransfer(m,1:3)) * norm(VectorForearmLeftReverseTransfer(m,1:3))));
  AngleElbowRightFlexionReverseTransfer(m) = acosd(dot(VectorUpperArmRightReverseTransfer(m,1:3), VectorForearmRightReverseTransfer(m,1:3)) / (norm(VectorUpperArmRightReverseTransfer(m,1:3)) * norm(VectorForearmRightReverseTransfer(m,1:3))));
  AngleWristLeftFlexionReverseTransfer(m) = acosd(dot(VectorForearmLeftReverseTransfer(m,1:3), VectorHandLeftReverseTransfer(m,1:3)) / (norm(VectorForearmLeftReverseTransfer(m,1:3)) * norm(VectorHandLeftReverseTransfer(m,1:3))));
  AngleWristRightFlexionReverseTransfer(m) = acosd(dot(VectorForearmRightReverseTransfer(m,1:3), VectorHandRightReverseTransfer(m,1:3)) / (norm(VectorForearmRightReverseTransfer(m,1:3)) * norm(VectorHandRightReverseTransfer(m,1:3))));
  AngleTrunkFlexionReverseTransfer(m) = acosd(dot(VectorTrunkReverseTransfer(m,1:3), VectorThighLeftReverseTransfer(m,1:3)) / (norm(VectorTrunkReverseTransfer(m,1:3)) * norm(VectorThighLeftReverseTransfer(m,1:3))));
  AngleTrunkYHeadReverseTransfer(m) = acosd(dot(VectorY(m,1:3), VectorHeadHipReverseTransfer(m,1:3)) / (norm(VectorY(m,1:3)) * norm(VectorHeadHipReverseTransfer(m,1:3))));
  AngleTrunkYMidSpineReverseTransfer(m) = acosd(dot(VectorY(m,1:3), VectorMidSpineHipReverseTransfer(m,1:3)) / (norm(VectorY(m,1:3)) * norm(VectorMidSpineHipReverseTransfer(m,1:3))));
  AngleTrunkBendingRawReverseTransfer(m) = acosd(dot(VectorShoulderAcrossReverseTransfer(m,1:3), VectorHipAcrossReverseTransfer(m,1:3)) / (norm(VectorShoulderAcrossReverseTransfer(m,1:3)) * norm(VectorHipAcrossReverseTransfer(m,1:3))));
  AngleWristLeftFlexionTipReverseTransfer(m) = acosd(dot(VectorForearmLeftReverseTransfer(m,1:3), VectorWristToHandTipLeftReverseTransfer(m,1:3)) / (norm(VectorForearmLeftReverseTransfer(m,1:3)) * norm(VectorWristToHandTipLeftReverseTransfer(m,1:3))));
  AngleWristRightFlexionTipReverseTransfer(m) = acosd(dot(VectorForearmRightReverseTransfer(m,1:3), VectorWristToHandTipRightReverseTransfer(m,1:3)) / (norm(VectorForearmRightReverseTransfer(m,1:3)) * norm(VectorWristToHandTipRightReverseTransfer(m,1:3))));
  AngleShoulderToWristLeftElevationReverseTransfer(m) = acosd(dot(-VectorTrunkReverseTransfer(m,1:3), VectorShoulderToWristLeftReverseTransfer(m,1:3)) / (norm(-VectorTrunkReverseTransfer(m,1:3)) * norm(VectorShoulderToWristLeftReverseTransfer(m,1:3))));
  AngleShoulderToWristRightElevationReverseTransfer(m) = acosd(dot(-VectorTrunkReverseTransfer(m,1:3), VectorShoulderToWristRightReverseTransfer(m,1:3)) / (norm(-VectorTrunkReverseTransfer(m,1:3)) * norm(VectorShoulderToWristRightReverseTransfer(m,1:3))));
  AngleShoulderToWristLeftPOEReverseTransfer(m) = acosd(dot(VectorTrunkAnteriorReverseTransfer(m,1:3), VectorShoulderToWristLeftReverseTransfer(m,1:3)) / (norm(VectorTrunkAnteriorReverseTransfer(m,1:3)) * norm(VectorShoulderToWristLeftReverseTransfer(m,1:3))));
  AngleShoulderToWristRightPOEReverseTransfer(m) = acosd(dot(VectorTrunkAnteriorReverseTransfer(m,1:3), VectorShoulderToWristRightReverseTransfer(m,1:3)) / (norm(VectorTrunkAnteriorReverseTransfer(m,1:3)) * norm(VectorShoulderToWristRightReverseTransfer(m,1:3))));
  
  VectorShoulderAcrossTVReverseTransfer(m,1:3) = VectorShoulderAcrossReverseTransfer(m,1:3) -(dot(VectorShoulderAcrossReverseTransfer(m,1:3),VectorTrunkReverseTransfer(m,1:3))/(norm(VectorTrunkReverseTransfer(m,1:3)))^2)*VectorTrunkReverseTransfer(m,1:3);
  VectorHipAcrossTVReverseTransfer(m,1:3) = VectorHipAcrossReverseTransfer(m,1:3) -(dot(VectorHipAcrossReverseTransfer(m,1:3),VectorTrunkReverseTransfer(m,1:3))/(norm(VectorTrunkReverseTransfer(m,1:3)))^2)*VectorTrunkReverseTransfer(m,1:3);
  AngleTrunkRotationTVReverseTransfer(m)=acosd(dot(VectorShoulderAcrossTVReverseTransfer(m,1:3), VectorHipAcrossTVReverseTransfer(m,1:3)) / (norm(VectorShoulderAcrossTVReverseTransfer(m,1:3)) * norm(VectorHipAcrossTVReverseTransfer(m,1:3))));

  VectorShoulderAcrossFrontalReverseTransfer(m,1:3) = VectorShoulderAcrossReverseTransfer(m,1:3) -(dot(VectorShoulderAcrossReverseTransfer(m,1:3),VectorTrunkAnteriorReverseTransfer(m,1:3))/(norm(VectorTrunkAnteriorReverseTransfer(m,1:3)))^2)*VectorTrunkAnteriorReverseTransfer(m,1:3);
  VectorHipAcrossFrontalReverseTransfer(m,1:3) = VectorHipAcrossReverseTransfer(m,1:3) -(dot(VectorHipAcrossReverseTransfer(m,1:3),VectorTrunkAnteriorReverseTransfer(m,1:3))/(norm(VectorTrunkAnteriorReverseTransfer(m,1:3)))^2)*VectorTrunkAnteriorReverseTransfer(m,1:3);
  AngleTrunkBendingFrontalReverseTransfer(m)=acosd(dot(VectorShoulderAcrossFrontalReverseTransfer(m,1:3), VectorHipAcrossFrontalReverseTransfer(m,1:3)) / (norm(VectorShoulderAcrossFrontalReverseTransfer(m,1:3)) * norm(VectorHipAcrossFrontalReverseTransfer(m,1:3))));
  
  AreaHandGripLeftReverseTransfer(m) = 0.5*norm(cross(VectorWristToThumbLeftReverseTransfer(m,1:3), VectorWristToHandTipLeftReverseTransfer(m,1:3)));
  AreaHandGripRightReverseTransfer(m) = 0.5*norm(cross(VectorWristToThumbRightReverseTransfer(m,1:3), VectorWristToHandTipRightReverseTransfer(m,1:3)));
  
  end
  %% Create Matrices where each row is a series of trials for a single subject.  The number of columns is the number of subjects.

  %sum(SelectedAngleLeftElbowFlexion(1:3))/3 
  
  %Left Shoulder POE   AngleShoulderPOE
  InitialAngleShoulderLeftPOETransfer(j,i) = sum(AngleShoulderLeftPOETransfer(1:10))/10;
  MaxAngleShoulderLeftPOETransfer(j,i) = max(AngleShoulderLeftPOETransfer(10:end));
  MinAngleShoulderLeftPOETransfer(j,i) = min(AngleShoulderLeftPOETransfer(10:end));
  ROMAngleShoulderLeftPOETransfer(j,i) = MaxAngleShoulderLeftPOETransfer(j,i) - MinAngleShoulderLeftPOETransfer(j,i);
  AveAngleShoulderLeftPOETransfer(j,i) = sum(AngleShoulderLeftPOETransfer(10:end))/numel(AngleShoulderLeftPOETransfer(10:end));
  
  InitialAngleShoulderLeftPOEReverseTransfer(j,i) = sum(AngleShoulderLeftPOEReverseTransfer(1:10))/10;
  MaxAngleShoulderLeftPOEReverseTransfer(j,i) = max(AngleShoulderLeftPOEReverseTransfer(10:end));
  MinAngleShoulderLeftPOEReverseTransfer(j,i) = min(AngleShoulderLeftPOEReverseTransfer(10:end));
  ROMAngleShoulderLeftPOEReverseTransfer(j,i) = MaxAngleShoulderLeftPOEReverseTransfer(j,i) - MinAngleShoulderLeftPOEReverseTransfer(j,i);
  AveAngleShoulderLeftPOEReverseTransfer(j,i) = sum(AngleShoulderLeftPOEReverseTransfer(10:end))/numel(AngleShoulderLeftPOEReverseTransfer(10:end));
  
  %Right Shoulder POE   AngleShoulderPOE
  InitialAngleShoulderRightPOETransfer(j,i) = sum(AngleShoulderRightPOETransfer(1:10))/10;
  MaxAngleShoulderRightPOETransfer(j,i) = max(AngleShoulderRightPOETransfer(10:end));
  MinAngleShoulderRightPOETransfer(j,i) = min(AngleShoulderRightPOETransfer(10:end));
  ROMAngleShoulderRightPOETransfer(j,i) = MaxAngleShoulderRightPOETransfer(j,i) - MinAngleShoulderRightPOETransfer(j,i);
  AveAngleShoulderRightPOETransfer(j,i) = sum(AngleShoulderRightPOETransfer(10:end))/numel(AngleShoulderRightPOETransfer(10:end));
  
  InitialAngleShoulderRightPOEReverseTransfer(j,i) = sum(AngleShoulderRightPOEReverseTransfer(1:10))/10;
  MaxAngleShoulderRightPOEReverseTransfer(j,i) = max(AngleShoulderRightPOEReverseTransfer(10:end));
  MinAngleShoulderRightPOEReverseTransfer(j,i) = min(AngleShoulderRightPOEReverseTransfer(10:end));
  ROMAngleShoulderRightPOEReverseTransfer(j,i) = MaxAngleShoulderRightPOEReverseTransfer(j,i) - MinAngleShoulderRightPOEReverseTransfer(j,i);
  AveAngleShoulderRightPOEReverseTransfer(j,i) = sum(AngleShoulderRightPOEReverseTransfer(10:end))/numel(AngleShoulderRightPOEReverseTransfer(10:end));
  
  %Left Shoulder Elevation AngleShoulderElevation
  InitialAngleShoulderLeftElevationTransfer(j,i) = sum(AngleShoulderLeftElevationTransfer(1:10))/10;
  MaxAngleShoulderLeftElevationTransfer(j,i) = max(AngleShoulderLeftElevationTransfer(10:end));
  MinAngleShoulderLeftElevationTransfer(j,i) = min(AngleShoulderLeftElevationTransfer(10:end));
  ROMAngleShoulderLeftElevationTransfer(j,i) = MaxAngleShoulderLeftElevationTransfer(j,i) - MinAngleShoulderLeftElevationTransfer(j,i);
  AveAngleShoulderLeftElevationTransfer(j,i) = sum(AngleShoulderLeftElevationTransfer(10:end))/numel(AngleShoulderLeftElevationTransfer(10:end));
  
  InitialAngleShoulderLeftElevationReverseTransfer(j,i) = sum(AngleShoulderLeftElevationReverseTransfer(1:10))/10;
  MaxAngleShoulderLeftElevationReverseTransfer(j,i) = max(AngleShoulderLeftElevationReverseTransfer(10:end));
  MinAngleShoulderLeftElevationReverseTransfer(j,i) = min(AngleShoulderLeftElevationReverseTransfer(10:end));
  ROMAngleShoulderLeftElevationReverseTransfer(j,i) = MaxAngleShoulderLeftElevationReverseTransfer(j,i) - MinAngleShoulderLeftElevationReverseTransfer(j,i);
  AveAngleShoulderLeftElevationReverseTransfer(j,i) = sum(AngleShoulderLeftElevationReverseTransfer(10:end))/numel(AngleShoulderLeftElevationReverseTransfer(10:end));
  
  %Right Shoulder Elevation AngleShoulderElevation
  InitialAngleShoulderRightElevationTransfer(j,i) = sum(AngleShoulderRightElevationTransfer(1:10))/10;
  MaxAngleShoulderRightElevationTransfer(j,i) = max(AngleShoulderRightElevationTransfer(10:end));
  MinAngleShoulderRightElevationTransfer(j,i) = min(AngleShoulderRightElevationTransfer(10:end));
  ROMAngleShoulderRightElevationTransfer(j,i) = MaxAngleShoulderRightElevationTransfer(j,i) - MinAngleShoulderRightElevationTransfer(j,i);
  AveAngleShoulderRightElevationTransfer(j,i) = sum(AngleShoulderRightElevationTransfer(10:end))/numel(AngleShoulderRightElevationTransfer(10:end));
  
  InitialAngleShoulderRightElevationReverseTransfer(j,i) = sum(AngleShoulderRightElevationReverseTransfer(1:10))/10;
  MaxAngleShoulderRightElevationReverseTransfer(j,i) = max(AngleShoulderRightElevationReverseTransfer(10:end));
  MinAngleShoulderRightElevationReverseTransfer(j,i) = min(AngleShoulderRightElevationReverseTransfer(10:end));
  ROMAngleShoulderRightElevationReverseTransfer(j,i) = MaxAngleShoulderRightElevationReverseTransfer(j,i) - MinAngleShoulderRightElevationReverseTransfer(j,i);
  AveAngleShoulderRightElevationReverseTransfer(j,i) = sum(AngleShoulderRightElevationReverseTransfer(10:end))/numel(AngleShoulderRightElevationReverseTransfer(10:end));
  
  
  % Left Elbow Flexion AngleElbowFlexion
  InitialAngleElbowLeftFlexionTransfer(j,i) = sum(AngleElbowLeftFlexionTransfer(1:10))/10;
  MaxAngleElbowLeftFlexionTransfer(j,i) = max(AngleElbowLeftFlexionTransfer(10:end));
  MinAngleElbowLeftFlexionTransfer(j,i) = min(AngleElbowLeftFlexionTransfer(10:end));
  ROMAngleElbowLeftFlexionTransfer(j,i) = MaxAngleElbowLeftFlexionTransfer(j,i) - MinAngleElbowLeftFlexionTransfer(j,i);
  AveAngleElbowLeftFlexionTransfer(j,i) = sum(AngleElbowLeftFlexionTransfer(10:end))/numel(AngleElbowLeftFlexionTransfer(10:end));
  
  InitialAngleElbowLeftFlexionReverseTransfer(j,i) = sum(AngleElbowLeftFlexionReverseTransfer(1:10))/10;
  MaxAngleElbowLeftFlexionReverseTransfer(j,i) = max(AngleElbowLeftFlexionReverseTransfer(10:end));
  MinAngleElbowLeftFlexionReverseTransfer(j,i) = min(AngleElbowLeftFlexionReverseTransfer(10:end));
  ROMAngleElbowLeftFlexionReverseTransfer(j,i) = MaxAngleElbowLeftFlexionReverseTransfer(j,i) - MinAngleElbowLeftFlexionReverseTransfer(j,i);
  AveAngleElbowLeftFlexionReverseTransfer(j,i) = sum(AngleElbowLeftFlexionReverseTransfer(10:end))/numel(AngleElbowLeftFlexionReverseTransfer(10:end));
  
  % Right Elbow Flexion AngleElbowFlexion
  InitialAngleElbowRightFlexionTransfer(j,i) = sum(AngleElbowRightFlexionTransfer(1:10))/10;
  MaxAngleElbowRightFlexionTransfer(j,i) = max(AngleElbowRightFlexionTransfer(10:end));
  MinAngleElbowRightFlexionTransfer(j,i) = min(AngleElbowRightFlexionTransfer(10:end));
  ROMAngleElbowRightFlexionTransfer(j,i) = MaxAngleElbowRightFlexionTransfer(j,i) - MinAngleElbowRightFlexionTransfer(j,i);
  AveAngleElbowRightFlexionTransfer(j,i) = sum(AngleElbowRightFlexionTransfer(10:end))/numel(AngleElbowRightFlexionTransfer(10:end));
  
  InitialAngleElbowRightFlexionReverseTransfer(j,i) = sum(AngleElbowRightFlexionReverseTransfer(1:10))/10;
  MaxAngleElbowRightFlexionReverseTransfer(j,i) = max(AngleElbowRightFlexionReverseTransfer(10:end));
  MinAngleElbowRightFlexionReverseTransfer(j,i) = min(AngleElbowRightFlexionReverseTransfer(10:end));
  ROMAngleElbowRightFlexionReverseTransfer(j,i) = MaxAngleElbowRightFlexionReverseTransfer(j,i) - MinAngleElbowRightFlexionReverseTransfer(j,i);
  AveAngleElbowRightFlexionReverseTransfer(j,i) = sum(AngleElbowRightFlexionReverseTransfer(10:end))/numel(AngleElbowRightFlexionReverseTransfer(10:end));
  
  % Left Wrist Flexion AngleWristFlexion
  InitialAngleWristLeftFlexionTransfer(j,i) = sum(AngleWristLeftFlexionTransfer(1:10))/10;
  MaxAngleWristLeftFlexionTransfer(j,i) = max(AngleWristLeftFlexionTransfer(10:end));
  MinAngleWristLeftFlexionTransfer(j,i) = min(AngleWristLeftFlexionTransfer(10:end));
  ROMAngleWristLeftFlexionTransfer(j,i) = MaxAngleWristLeftFlexionTransfer(j,i) - MinAngleWristLeftFlexionTransfer(j,i);
  AveAngleWristLeftFlexionTransfer(j,i) = sum(AngleWristLeftFlexionTransfer(10:end))/numel(AngleWristLeftFlexionTransfer(10:end));  
  
  InitialAngleWristLeftFlexionReverseTransfer(j,i) = sum(AngleWristLeftFlexionReverseTransfer(1:10))/10;
  MaxAngleWristLeftFlexionReverseTransfer(j,i) = max(AngleWristLeftFlexionReverseTransfer(10:end));
  MinAngleWristLeftFlexionReverseTransfer(j,i) = min(AngleWristLeftFlexionReverseTransfer(10:end));
  ROMAngleWristLeftFlexionReverseTransfer(j,i) = MaxAngleWristLeftFlexionReverseTransfer(j,i) - MinAngleWristLeftFlexionReverseTransfer(j,i);
  AveAngleWristLeftFlexionReverseTransfer(j,i) = sum(AngleWristLeftFlexionReverseTransfer(10:end))/numel(AngleWristLeftFlexionReverseTransfer(10:end));
  
  % Right Wrist Flexion AngleWristFlexion
  InitialAngleWristRightFlexionTransfer(j,i) = sum(AngleWristRightFlexionTransfer(1:10))/10;
  MaxAngleWristRightFlexionTransfer(j,i) = max(AngleWristRightFlexionTransfer(10:end));
  MinAngleWristRightFlexionTransfer(j,i) = min(AngleWristRightFlexionTransfer(10:end));
  ROMAngleWristRightFlexionTransfer(j,i) = MaxAngleWristRightFlexionTransfer(j,i) - MinAngleWristRightFlexionTransfer(j,i);
  AveAngleWristRightFlexionTransfer(j,i) = sum(AngleWristRightFlexionTransfer(10:end))/numel(AngleWristRightFlexionTransfer(10:end));  
  
  InitialAngleWristRightFlexionReverseTransfer(j,i) = sum(AngleWristRightFlexionReverseTransfer(1:10))/10;
  MaxAngleWristRightFlexionReverseTransfer(j,i) = max(AngleWristRightFlexionReverseTransfer(10:end));
  MinAngleWristRightFlexionReverseTransfer(j,i) = min(AngleWristRightFlexionReverseTransfer(10:end));
  ROMAngleWristRightFlexionReverseTransfer(j,i) = MaxAngleWristRightFlexionReverseTransfer(j,i) - MinAngleWristRightFlexionReverseTransfer(j,i);
  AveAngleWristRightFlexionReverseTransfer(j,i) = sum(AngleWristRightFlexionReverseTransfer(10:end))/numel(AngleWristRightFlexionReverseTransfer(10:end));
  
  % Trunk Flexion (Angle between trunk and thigh)  AngleTrunkFlexion
  InitialAngleTrunkFlexionTransfer(j,i) = sum(AngleTrunkFlexionTransfer(1:10))/10;
  MaxAngleTrunkFlexionTransfer(j,i) = max(AngleTrunkFlexionTransfer(10:end));
  MinAngleTrunkFlexionTransfer(j,i) = min(AngleTrunkFlexionTransfer(10:end));
  ROMAngleTrunkFlexionTransfer(j,i) = MaxAngleTrunkFlexionTransfer(j,i) - MinAngleTrunkFlexionTransfer(j,i);
  AveAngleTrunkFlexionTransfer(j,i) = sum(AngleTrunkFlexionTransfer(10:end))/numel(AngleTrunkFlexionTransfer(10:end));    
  
  InitialAngleTrunkFlexionReverseTransfer(j,i) = sum(AngleTrunkFlexionReverseTransfer(1:10))/10;
  MaxAngleTrunkFlexionReverseTransfer(j,i) = max(AngleTrunkFlexionReverseTransfer(10:end));
  MinAngleTrunkFlexionReverseTransfer(j,i) = min(AngleTrunkFlexionReverseTransfer(10:end));
  ROMAngleTrunkFlexionReverseTransfer(j,i) = MaxAngleTrunkFlexionReverseTransfer(j,i) - MinAngleTrunkFlexionReverseTransfer(j,i);
  AveAngleTrunkFlexionReverseTransfer(j,i) = sum(AngleTrunkFlexionReverseTransfer(10:end))/numel(AngleTrunkFlexionReverseTransfer(10:end));    
  
  
  % Trunk Flexion (Y-vector to Head)   AngleTrunkFlexion
  InitialAngleTrunkYHeadTransfer(j,i) = sum(AngleTrunkYHeadTransfer(1:10))/10;
  MaxAngleTrunkYHeadTransfer(j,i) = max(AngleTrunkYHeadTransfer(10:end));
  MinAngleTrunkYHeadTransfer(j,i) = min(AngleTrunkYHeadTransfer(10:end));
  ROMAngleTrunkYHeadTransfer(j,i) = MaxAngleTrunkYHeadTransfer(j,i) - MinAngleTrunkYHeadTransfer(j,i);
  AveAngleTrunkYHeadTransfer(j,i) = sum(AngleTrunkYHeadTransfer(10:end))/numel(AngleTrunkYHeadTransfer(10:end));    
  
  InitialAngleTrunkYHeadReverseTransfer(j,i) = sum(AngleTrunkYHeadReverseTransfer(1:10))/10;
  MaxAngleTrunkYHeadReverseTransfer(j,i) = max(AngleTrunkYHeadReverseTransfer(10:end));
  MinAngleTrunkYHeadReverseTransfer(j,i) = min(AngleTrunkYHeadReverseTransfer(10:end));
  ROMAngleTrunkYHeadReverseTransfer(j,i) = MaxAngleTrunkYHeadReverseTransfer(j,i) - MinAngleTrunkYHeadReverseTransfer(j,i);
  AveAngleTrunkYHeadReverseTransfer(j,i) = sum(AngleTrunkYHeadReverseTransfer(10:end))/numel(AngleTrunkYHeadReverseTransfer(10:end));    
  
  
  % Trunk Flexion (Y-vector to SpineMid)  AngleTrunkFlexion
  InitialAngleTrunkYMidSpineTransfer(j,i) = sum(AngleTrunkYMidSpineTransfer(1:10))/10;
  MaxAngleTrunkYMidSpineTransfer(j,i) = max(AngleTrunkYMidSpineTransfer(10:end));
  MinAngleTrunkYMidSpineTransfer(j,i) = min(AngleTrunkYMidSpineTransfer(10:end));
  ROMAngleTrunkYMidSpineTransfer(j,i) = MaxAngleTrunkYMidSpineTransfer(j,i) - MinAngleTrunkYMidSpineTransfer(j,i);
  AveAngleTrunkYMidSpineTransfer(j,i) = sum(AngleTrunkYMidSpineTransfer(10:end))/numel(AngleTrunkYMidSpineTransfer(10:end));    
  
  InitialAngleTrunkYMidSpineReverseTransfer(j,i) = sum(AngleTrunkYMidSpineReverseTransfer(1:10))/10;
  MaxAngleTrunkYMidSpineReverseTransfer(j,i) = max(AngleTrunkYMidSpineReverseTransfer(10:end));
  MinAngleTrunkYMidSpineReverseTransfer(j,i) = min(AngleTrunkYMidSpineReverseTransfer(10:end));
  ROMAngleTrunkYMidSpineReverseTransfer(j,i) = MaxAngleTrunkYMidSpineReverseTransfer(j,i) - MinAngleTrunkYMidSpineReverseTransfer(j,i);
  AveAngleTrunkYMidSpineReverseTransfer(j,i) = sum(AngleTrunkYMidSpineReverseTransfer(10:end))/numel(AngleTrunkYMidSpineReverseTransfer(10:end));    
  
  
  % Trunk Bending (Pure Angle Between ShoulderAcross and HipAcross)
  % AngleTrunkBendingRaw
  InitialAngleTrunkBendingRawTransfer(j,i) = sum(AngleTrunkBendingRawTransfer(1:10))/10;
  MaxAngleTrunkBendingRawTransfer(j,i) = max(AngleTrunkBendingRawTransfer(10:end));
  MinAngleTrunkBendingRawTransfer(j,i) = min(AngleTrunkBendingRawTransfer(10:end));
  ROMAngleTrunkBendingRawTransfer(j,i) = MaxAngleTrunkBendingRawTransfer(j,i) - MinAngleTrunkBendingRawTransfer(j,i);
  AveAngleTrunkBendingRawTransfer(j,i) = sum(AngleTrunkBendingRawTransfer(10:end))/numel(AngleTrunkBendingRawTransfer(10:end));    
  
  InitialAngleTrunkBendingRawReverseTransfer(j,i) = sum(AngleTrunkBendingRawReverseTransfer(1:10))/10;
  MaxAngleTrunkBendingRawReverseTransfer(j,i) = max(AngleTrunkBendingRawReverseTransfer(10:end));
  MinAngleTrunkBendingRawReverseTransfer(j,i) = min(AngleTrunkBendingRawReverseTransfer(10:end));
  ROMAngleTrunkBendingRawReverseTransfer(j,i) = MaxAngleTrunkBendingRawReverseTransfer(j,i) - MinAngleTrunkBendingRawReverseTransfer(j,i);
  AveAngleTrunkBendingRawReverseTransfer(j,i) = sum(AngleTrunkBendingRawReverseTransfer(10:end))/numel(AngleTrunkBendingRawReverseTransfer(10:end)); 
  
  
  
  % Trunk Rotation Transverse (TV Plane Angle Between ShoulderAcross and HipAcross)  AngleTrunkRotationTVTransfer
  InitialAngleTrunkRotationTVTransfer(j,i) = sum(AngleTrunkRotationTVTransfer(1:10))/10;
  MaxAngleTrunkRotationTVTransfer(j,i) = max(AngleTrunkRotationTVTransfer(10:end));
  MinAngleTrunkRotationTVTransfer(j,i) = min(AngleTrunkRotationTVTransfer(10:end));
  ROMAngleTrunkRotationTVTransfer(j,i) = MaxAngleTrunkRotationTVTransfer(j,i) - MinAngleTrunkRotationTVTransfer(j,i);
  AveAngleTrunkRotationTVTransfer(j,i) = sum(AngleTrunkRotationTVTransfer(10:end))/numel(AngleTrunkRotationTVTransfer(10:end));    
  
  InitialAngleTrunkRotationTVReverseTransfer(j,i) = sum(AngleTrunkRotationTVReverseTransfer(1:10))/10;
  MaxAngleTrunkRotationTVReverseTransfer(j,i) = max(AngleTrunkRotationTVReverseTransfer(10:end));
  MinAngleTrunkRotationTVReverseTransfer(j,i) = min(AngleTrunkRotationTVReverseTransfer(10:end));
  ROMAngleTrunkRotationTVReverseTransfer(j,i) = MaxAngleTrunkRotationTVReverseTransfer(j,i) - MinAngleTrunkRotationTVReverseTransfer(j,i);
  AveAngleTrunkRotationTVReverseTransfer(j,i) = sum(AngleTrunkRotationTVReverseTransfer(10:end))/numel(AngleTrunkRotationTVReverseTransfer(10:end));    
  
  
  % Trunk Bending Frontal (Frontal Angle Between ShoulderAcross and HipAcross)  AngleTrunkBendingFrontalTransfer
  InitialAngleTrunkBendingFrontalTransfer(j,i) = sum(AngleTrunkBendingFrontalTransfer(1:10))/10;
  MaxAngleTrunkBendingFrontalTransfer(j,i) = max(AngleTrunkBendingFrontalTransfer(10:end));
  MinAngleTrunkBendingFrontalTransfer(j,i) = min(AngleTrunkBendingFrontalTransfer(10:end));
  ROMAngleTrunkBendingFrontalTransfer(j,i) = MaxAngleTrunkBendingFrontalTransfer(j,i) - MinAngleTrunkBendingFrontalTransfer(j,i);
  AveAngleTrunkBendingFrontalTransfer(j,i) = sum(AngleTrunkBendingFrontalTransfer(10:end))/numel(AngleTrunkBendingFrontalTransfer(10:end));    
  
  InitialAngleTrunkBendingFrontalReverseTransfer(j,i) = sum(AngleTrunkBendingFrontalReverseTransfer(1:10))/10;
  MaxAngleTrunkBendingFrontalReverseTransfer(j,i) = max(AngleTrunkBendingFrontalReverseTransfer(10:end));
  MinAngleTrunkBendingFrontalReverseTransfer(j,i) = min(AngleTrunkBendingFrontalReverseTransfer(10:end));
  ROMAngleTrunkBendingFrontalReverseTransfer(j,i) = MaxAngleTrunkBendingFrontalReverseTransfer(j,i) - MinAngleTrunkBendingFrontalReverseTransfer(j,i);
  AveAngleTrunkBendingFrontalReverseTransfer(j,i) = sum(AngleTrunkBendingFrontalReverseTransfer(10:end))/numel(AngleTrunkBendingFrontalReverseTransfer(10:end));      
  
  % Left Wrist Tip Flexion AngleWristLeftFlexionTip
  InitialAngleWristLeftFlexionTipTransfer(j,i) = sum(AngleWristLeftFlexionTipTransfer(1:10))/10;
  MaxAngleWristLeftFlexionTipTransfer(j,i) = max(AngleWristLeftFlexionTipTransfer(10:end));
  MinAngleWristLeftFlexionTipTransfer(j,i) = min(AngleWristLeftFlexionTipTransfer(10:end));
  ROMAngleWristLeftFlexionTipTransfer(j,i) = MaxAngleWristLeftFlexionTipTransfer(j,i) - MinAngleWristLeftFlexionTipTransfer(j,i);
  AveAngleWristLeftFlexionTipTransfer(j,i) = sum(AngleWristLeftFlexionTipTransfer(10:end))/numel(AngleWristLeftFlexionTipTransfer(10:end));
  
  InitialAngleWristLeftFlexionTipReverseTransfer(j,i) = sum(AngleWristLeftFlexionTipReverseTransfer(1:10))/10;
  MaxAngleWristLeftFlexionTipReverseTransfer(j,i) = max(AngleWristLeftFlexionTipReverseTransfer(10:end));
  MinAngleWristLeftFlexionTipReverseTransfer(j,i) = min(AngleWristLeftFlexionTipReverseTransfer(10:end));
  ROMAngleWristLeftFlexionTipReverseTransfer(j,i) = MaxAngleWristLeftFlexionTipReverseTransfer(j,i) - MinAngleWristLeftFlexionTipReverseTransfer(j,i);
  AveAngleWristLeftFlexionTipReverseTransfer(j,i) = sum(AngleWristLeftFlexionTipReverseTransfer(10:end))/numel(AngleWristLeftFlexionTipReverseTransfer(10:end));
  
  % Right Wrist Tip Flexion AngleWristRightFlexionTip
  InitialAngleWristRightFlexionTipTransfer(j,i) = sum(AngleWristRightFlexionTipTransfer(1:10))/10;
  MaxAngleWristRightFlexionTipTransfer(j,i) = max(AngleWristRightFlexionTipTransfer(10:end));
  MinAngleWristRightFlexionTipTransfer(j,i) = min(AngleWristRightFlexionTipTransfer(10:end));
  ROMAngleWristRightFlexionTipTransfer(j,i) = MaxAngleWristRightFlexionTipTransfer(j,i) - MinAngleWristRightFlexionTipTransfer(j,i);
  AveAngleWristRightFlexionTipTransfer(j,i) = sum(AngleWristRightFlexionTipTransfer(10:end))/numel(AngleWristRightFlexionTipTransfer(10:end));
  
  InitialAngleWristRightFlexionTipReverseTransfer(j,i) = sum(AngleWristRightFlexionTipReverseTransfer(1:10))/10;
  MaxAngleWristRightFlexionTipReverseTransfer(j,i) = max(AngleWristRightFlexionTipReverseTransfer(10:end));
  MinAngleWristRightFlexionTipReverseTransfer(j,i) = min(AngleWristRightFlexionTipReverseTransfer(10:end));
  ROMAngleWristRightFlexionTipReverseTransfer(j,i) = MaxAngleWristRightFlexionTipReverseTransfer(j,i) - MinAngleWristRightFlexionTipReverseTransfer(j,i);
  AveAngleWristRightFlexionTipReverseTransfer(j,i) = sum(AngleWristRightFlexionTipReverseTransfer(10:end))/numel(AngleWristRightFlexionTipReverseTransfer(10:end));
  
  
  % Left Angle Shoulder to Wrist Elevation AngleShoulderToWristLeftElevation
  InitialAngleShoulderToWristLeftElevationTransfer(j,i) = sum(AngleShoulderToWristLeftElevationTransfer(1:10))/10;
  MaxAngleShoulderToWristLeftElevationTransfer(j,i) = max(AngleShoulderToWristLeftElevationTransfer(10:end));
  MinAngleShoulderToWristLeftElevationTransfer(j,i) = min(AngleShoulderToWristLeftElevationTransfer(10:end));
  ROMAngleShoulderToWristLeftElevationTransfer(j,i) = MaxAngleShoulderToWristLeftElevationTransfer(j,i) - MinAngleShoulderToWristLeftElevationTransfer(j,i);
  AveAngleShoulderToWristLeftElevationTransfer(j,i) = sum(AngleShoulderToWristLeftElevationTransfer(10:end))/numel(AngleShoulderToWristLeftElevationTransfer(10:end));
  
  InitialAngleShoulderToWristLeftElevationReverseTransfer(j,i) = sum(AngleShoulderToWristLeftElevationReverseTransfer(1:10))/10;
  MaxAngleShoulderToWristLeftElevationReverseTransfer(j,i) = max(AngleShoulderToWristLeftElevationReverseTransfer(10:end));
  MinAngleShoulderToWristLeftElevationReverseTransfer(j,i) = min(AngleShoulderToWristLeftElevationReverseTransfer(10:end));
  ROMAngleShoulderToWristLeftElevationReverseTransfer(j,i) = MaxAngleShoulderToWristLeftElevationReverseTransfer(j,i) - MinAngleShoulderToWristLeftElevationReverseTransfer(j,i);
  AveAngleShoulderToWristLeftElevationReverseTransfer(j,i) = sum(AngleShoulderToWristLeftElevationReverseTransfer(10:end))/numel(AngleShoulderToWristLeftElevationReverseTransfer(10:end));
  
  % Right Angle Shoulder to Wrist Elevation AngleShoulderToWristLeftElevation
  InitialAngleShoulderToWristRightElevationTransfer(j,i) = sum(AngleShoulderToWristRightElevationTransfer(1:10))/10;
  MaxAngleShoulderToWristRightElevationTransfer(j,i) = max(AngleShoulderToWristRightElevationTransfer(10:end));
  MinAngleShoulderToWristRightElevationTransfer(j,i) = min(AngleShoulderToWristRightElevationTransfer(10:end));
  ROMAngleShoulderToWristRightElevationTransfer(j,i) = MaxAngleShoulderToWristRightElevationTransfer(j,i) - MinAngleShoulderToWristRightElevationTransfer(j,i);
  AveAngleShoulderToWristRightElevationTransfer(j,i) = sum(AngleShoulderToWristRightElevationTransfer(10:end))/numel(AngleShoulderToWristRightElevationTransfer(10:end));
  
  InitialAngleShoulderToWristRightElevationReverseTransfer(j,i) = sum(AngleShoulderToWristRightElevationReverseTransfer(1:10))/10;
  MaxAngleShoulderToWristRightElevationReverseTransfer(j,i) = max(AngleShoulderToWristRightElevationReverseTransfer(10:end));
  MinAngleShoulderToWristRightElevationReverseTransfer(j,i) = min(AngleShoulderToWristRightElevationReverseTransfer(10:end));
  ROMAngleShoulderToWristRightElevationReverseTransfer(j,i) = MaxAngleShoulderToWristRightElevationReverseTransfer(j,i) - MinAngleShoulderToWristRightElevationReverseTransfer(j,i);
  AveAngleShoulderToWristRightElevationReverseTransfer(j,i) = sum(AngleShoulderToWristRightElevationReverseTransfer(10:end))/numel(AngleShoulderToWristRightElevationReverseTransfer(10:end));
  
  
  % Left Angle Shoulder to Wrist POE AngleShoulderToWristLeftPOE
  InitialAngleShoulderToWristLeftPOETransfer(j,i) = sum(AngleShoulderToWristLeftPOETransfer(1:10))/10;
  MaxAngleShoulderToWristLeftPOETransfer(j,i) = max(AngleShoulderToWristLeftPOETransfer(10:end));
  MinAngleShoulderToWristLeftPOETransfer(j,i) = min(AngleShoulderToWristLeftPOETransfer(10:end));
  ROMAngleShoulderToWristLeftPOETransfer(j,i) = MaxAngleShoulderToWristLeftPOETransfer(j,i) - MinAngleShoulderToWristLeftPOETransfer(j,i);
  AveAngleShoulderToWristLeftPOETransfer(j,i) = sum(AngleShoulderToWristLeftPOETransfer(10:end))/numel(AngleShoulderToWristLeftPOETransfer(10:end));
  
  InitialAngleShoulderToWristLeftPOEReverseTransfer(j,i) = sum(AngleShoulderToWristLeftPOEReverseTransfer(1:10))/10;
  MaxAngleShoulderToWristLeftPOEReverseTransfer(j,i) = max(AngleShoulderToWristLeftPOEReverseTransfer(10:end));
  MinAngleShoulderToWristLeftPOEReverseTransfer(j,i) = min(AngleShoulderToWristLeftPOEReverseTransfer(10:end));
  ROMAngleShoulderToWristLeftPOEReverseTransfer(j,i) = MaxAngleShoulderToWristLeftPOEReverseTransfer(j,i) - MinAngleShoulderToWristLeftPOEReverseTransfer(j,i);
  AveAngleShoulderToWristLeftPOEReverseTransfer(j,i) = sum(AngleShoulderToWristLeftPOEReverseTransfer(10:end))/numel(AngleShoulderToWristLeftPOEReverseTransfer(10:end));
  
  % Right Angle Shoulder to Wrist POE AngleShoulderToWristRightPOE
  InitialAngleShoulderToWristRightPOETransfer(j,i) = sum(AngleShoulderToWristRightPOETransfer(1:10))/10;
  MaxAngleShoulderToWristRightPOETransfer(j,i) = max(AngleShoulderToWristRightPOETransfer(10:end));
  MinAngleShoulderToWristRightPOETransfer(j,i) = min(AngleShoulderToWristRightPOETransfer(10:end));
  ROMAngleShoulderToWristRightPOETransfer(j,i) = MaxAngleShoulderToWristRightPOETransfer(j,i) - MinAngleShoulderToWristRightPOETransfer(j,i);
  AveAngleShoulderToWristRightPOETransfer(j,i) = sum(AngleShoulderToWristRightPOETransfer(10:end))/numel(AngleShoulderToWristRightPOETransfer(10:end));
  
  InitialAngleShoulderToWristRightPOEReverseTransfer(j,i) = sum(AngleShoulderToWristRightPOEReverseTransfer(1:10))/10;
  MaxAngleShoulderToWristRightPOEReverseTransfer(j,i) = max(AngleShoulderToWristRightPOEReverseTransfer(10:end));
  MinAngleShoulderToWristRightPOEReverseTransfer(j,i) = min(AngleShoulderToWristRightPOEReverseTransfer(10:end));
  ROMAngleShoulderToWristRightPOEReverseTransfer(j,i) = MaxAngleShoulderToWristRightPOEReverseTransfer(j,i) - MinAngleShoulderToWristRightPOEReverseTransfer(j,i);
  AveAngleShoulderToWristRightPOEReverseTransfer(j,i) = sum(AngleShoulderToWristRightPOEReverseTransfer(10:end))/numel(AngleShoulderToWristRightPOEReverseTransfer(10:end));
  
  
  % Left Hand Grip Area AreaHandGripLeft
  InitialAreaHandGripLeftTransfer(j,i) = sum(AreaHandGripLeftTransfer(1:10))/10;
  MaxAreaHandGripLeftTransfer(j,i) = max(AreaHandGripLeftTransfer(10:end));
  MinAreaHandGripLeftTransfer(j,i) = min(AreaHandGripLeftTransfer(10:end));
  ROMAreaHandGripLeftTransfer(j,i) = MaxAreaHandGripLeftTransfer(j,i) - MinAreaHandGripLeftTransfer(j,i);
  AveAreaHandGripLeftTransfer(j,i) = sum(AreaHandGripLeftTransfer(10:end))/numel(AreaHandGripLeftTransfer(10:end));
  
  InitialAreaHandGripLeftReverseTransfer(j,i) = sum(AreaHandGripLeftReverseTransfer(1:10))/10;
  MaxAreaHandGripLeftReverseTransfer(j,i) = max(AreaHandGripLeftReverseTransfer(10:end));
  MinAreaHandGripLeftReverseTransfer(j,i) = min(AreaHandGripLeftReverseTransfer(10:end));
  ROMAreaHandGripLeftReverseTransfer(j,i) = MaxAreaHandGripLeftReverseTransfer(j,i) - MinAreaHandGripLeftReverseTransfer(j,i);
  AveAreaHandGripLeftReverseTransfer(j,i) = sum(AreaHandGripLeftReverseTransfer(10:end))/numel(AreaHandGripLeftReverseTransfer(10:end));
  
  % Right Angle Shoulder to Wrist POE AngleShoulderToWristRightPOE
  InitialAreaHandGripRightTransfer(j,i) = sum(AreaHandGripRightTransfer(1:10))/10;
  MaxAreaHandGripRightTransfer(j,i) = max(AreaHandGripRightTransfer(10:end));
  MinAreaHandGripRightTransfer(j,i) = min(AreaHandGripRightTransfer(10:end));
  ROMAreaHandGripRightTransfer(j,i) = MaxAreaHandGripRightTransfer(j,i) - MinAreaHandGripRightTransfer(j,i);
  AveAreaHandGripRightTransfer(j,i) = sum(AreaHandGripRightTransfer(10:end))/numel(AreaHandGripRightTransfer(10:end));
  
  InitialAreaHandGripRightReverseTransfer(j,i) = sum(AreaHandGripRightReverseTransfer(1:10))/10;
  MaxAreaHandGripRightReverseTransfer(j,i) = max(AreaHandGripRightReverseTransfer(10:end));
  MinAreaHandGripRightReverseTransfer(j,i) = min(AreaHandGripRightReverseTransfer(10:end));
  ROMAreaHandGripRightReverseTransfer(j,i) = MaxAreaHandGripRightReverseTransfer(j,i) - MinAreaHandGripRightReverseTransfer(j,i);
  AveAreaHandGripRightReverseTransfer(j,i) = sum(AreaHandGripRightReverseTransfer(10:end))/numel(AreaHandGripRightReverseTransfer(10:end));
  
  
  
  % Head Hip Velocity HeadHipVelocity
  InitialHeadHipVelocityTransfer(j,i) = sum(HeadHipVelocityTransfer(1:10))/10;
  MaxHeadHipVelocityTransfer(j,i) = max(HeadHipVelocityTransfer(10:end));
  MinHeadHipVelocityTransfer(j,i) = min(HeadHipVelocityTransfer(10:end));
  ROMHeadHipVelocityTransfer(j,i) = MaxHeadHipVelocityTransfer(j,i) - MinHeadHipVelocityTransfer(j,i);
  AveHeadHipVelocityTransfer(j,i) = sum(HeadHipVelocityTransfer(10:end))/numel(HeadHipVelocityTransfer(10:end));
  
  InitialHeadHipVelocityReverseTransfer(j,i) = sum(HeadHipVelocityReverseTransfer(1:10))/10;
  MaxHeadHipVelocityReverseTransfer(j,i) = max(HeadHipVelocityReverseTransfer(10:end));
  MinHeadHipVelocityReverseTransfer(j,i) = min(HeadHipVelocityReverseTransfer(10:end));
  ROMHeadHipVelocityReverseTransfer(j,i) = MaxHeadHipVelocityReverseTransfer(j,i) - MinHeadHipVelocityReverseTransfer(j,i);
  AveHeadHipVelocityReverseTransfer(j,i) = sum(HeadHipVelocityReverseTransfer(10:end))/numel(HeadHipVelocityReverseTransfer(10:end));
  
  % Head Hip Acceleration HeadHipAccelTransfer
  InitialHeadHipAccelTransfer(j,i) = sum(HeadHipAccelTransfer(1:10))/10;
  MaxHeadHipAccelTransfer(j,i) = max(HeadHipAccelTransfer(10:end));
  MinHeadHipAccelTransfer(j,i) = min(HeadHipAccelTransfer(10:end));
  ROMHeadHipAccelTransfer(j,i) = MaxHeadHipAccelTransfer(j,i) - MinHeadHipAccelTransfer(j,i);
  AveHeadHipAccelTransfer(j,i) = sum(HeadHipAccelTransfer(10:end))/numel(HeadHipAccelTransfer(10:end));
  
  InitialHeadHipAccelReverseTransfer(j,i) = sum(HeadHipAccelReverseTransfer(1:10))/10;
  MaxHeadHipAccelReverseTransfer(j,i) = max(HeadHipAccelReverseTransfer(10:end));
  MinHeadHipAccelReverseTransfer(j,i) = min(HeadHipAccelReverseTransfer(10:end));
  ROMHeadHipAccelReverseTransfer(j,i) = MaxHeadHipAccelReverseTransfer(j,i) - MinHeadHipAccelReverseTransfer(j,i);
  AveHeadHipAccelReverseTransfer(j,i) = sum(HeadHipAccelReverseTransfer(10:end))/numel(HeadHipAccelReverseTransfer(10:end));
 
  
  count = count+1;
end
end

%% Convert the Resultant Matrices (5 rows (# of trials) and 31 columns (# of subjects)) into
% Matrices to display the results (155 rows (# of total trials) and 1
% column (the parameter tested))
TableShoulderLeftPOETransfer=[reshape(InitialAngleShoulderLeftPOETransfer,[],1) reshape(MaxAngleShoulderLeftPOETransfer,[],1) reshape(MinAngleShoulderLeftPOETransfer,[],1) reshape(ROMAngleShoulderLeftPOETransfer,[],1) reshape(AveAngleShoulderLeftPOETransfer,[],1)];
TableShoulderRightPOETransfer=[reshape(InitialAngleShoulderRightPOETransfer,[],1) reshape(MaxAngleShoulderRightPOETransfer,[],1) reshape(MinAngleShoulderRightPOETransfer,[],1) reshape(ROMAngleShoulderRightPOETransfer,[],1) reshape(AveAngleShoulderRightPOETransfer,[],1)];
TableShoulderLeftPOEReverseTransfer=[reshape(InitialAngleShoulderLeftPOEReverseTransfer,[],1) reshape(MaxAngleShoulderLeftPOEReverseTransfer,[],1) reshape(MinAngleShoulderLeftPOEReverseTransfer,[],1) reshape(ROMAngleShoulderLeftPOEReverseTransfer,[],1) reshape(AveAngleShoulderLeftPOEReverseTransfer,[],1)];
TableShoulderRightPOEReverseTransfer=[reshape(InitialAngleShoulderRightPOEReverseTransfer,[],1) reshape(MaxAngleShoulderRightPOEReverseTransfer,[],1) reshape(MinAngleShoulderRightPOEReverseTransfer,[],1) reshape(ROMAngleShoulderRightPOEReverseTransfer,[],1) reshape(AveAngleShoulderRightPOEReverseTransfer,[],1)];
TableShoulderLeftElevationTransfer=[reshape(InitialAngleShoulderLeftElevationTransfer,[],1) reshape(MaxAngleShoulderLeftElevationTransfer,[],1) reshape(MinAngleShoulderLeftElevationTransfer,[],1) reshape(ROMAngleShoulderLeftElevationTransfer,[],1) reshape(AveAngleShoulderLeftElevationTransfer,[],1)];
TableShoulderRightElevationTransfer=[reshape(InitialAngleShoulderRightElevationTransfer,[],1) reshape(MaxAngleShoulderRightElevationTransfer,[],1) reshape(MinAngleShoulderRightElevationTransfer,[],1) reshape(ROMAngleShoulderRightElevationTransfer,[],1) reshape(AveAngleShoulderRightElevationTransfer,[],1)];
TableShoulderLeftElevationReverseTransfer=[reshape(InitialAngleShoulderLeftElevationReverseTransfer,[],1) reshape(MaxAngleShoulderLeftElevationReverseTransfer,[],1) reshape(MinAngleShoulderLeftElevationReverseTransfer,[],1) reshape(ROMAngleShoulderLeftElevationReverseTransfer,[],1) reshape(AveAngleShoulderLeftElevationReverseTransfer,[],1)];
TableShoulderRightElevationReverseTransfer=[reshape(InitialAngleShoulderRightElevationReverseTransfer,[],1) reshape(MaxAngleShoulderRightElevationReverseTransfer,[],1) reshape(MinAngleShoulderRightElevationReverseTransfer,[],1) reshape(ROMAngleShoulderRightElevationReverseTransfer,[],1) reshape(AveAngleShoulderRightElevationReverseTransfer,[],1)];
TableElbowLeftFlexionTransfer=[reshape(InitialAngleElbowLeftFlexionTransfer,[],1) reshape(MaxAngleElbowLeftFlexionTransfer,[],1) reshape(MinAngleElbowLeftFlexionTransfer,[],1) reshape(ROMAngleElbowLeftFlexionTransfer,[],1) reshape(AveAngleElbowLeftFlexionTransfer,[],1)];
TableElbowRightFlexionTransfer=[reshape(InitialAngleElbowRightFlexionTransfer,[],1) reshape(MaxAngleElbowRightFlexionTransfer,[],1) reshape(MinAngleElbowRightFlexionTransfer,[],1) reshape(ROMAngleElbowRightFlexionTransfer,[],1) reshape(AveAngleElbowRightFlexionTransfer,[],1)];
TableElbowLeftFlexionReverseTransfer=[reshape(InitialAngleElbowLeftFlexionReverseTransfer,[],1) reshape(MaxAngleElbowLeftFlexionReverseTransfer,[],1) reshape(MinAngleElbowLeftFlexionReverseTransfer,[],1) reshape(ROMAngleElbowLeftFlexionReverseTransfer,[],1) reshape(AveAngleElbowLeftFlexionReverseTransfer,[],1)];
TableElbowRightFlexionReverseTransfer=[reshape(InitialAngleElbowRightFlexionReverseTransfer,[],1) reshape(MaxAngleElbowRightFlexionReverseTransfer,[],1) reshape(MinAngleElbowRightFlexionReverseTransfer,[],1) reshape(ROMAngleElbowRightFlexionReverseTransfer,[],1) reshape(AveAngleElbowRightFlexionReverseTransfer,[],1)];
TableWristLeftFlexionTransfer=[reshape(InitialAngleWristLeftFlexionTransfer,[],1) reshape(MaxAngleWristLeftFlexionTransfer,[],1) reshape(MinAngleWristLeftFlexionTransfer,[],1) reshape(ROMAngleWristLeftFlexionTransfer,[],1) reshape(AveAngleWristLeftFlexionTransfer,[],1)];
TableWristRightFlexionTransfer=[reshape(InitialAngleWristRightFlexionTransfer,[],1) reshape(MaxAngleWristRightFlexionTransfer,[],1) reshape(MinAngleWristRightFlexionTransfer,[],1) reshape(ROMAngleWristRightFlexionTransfer,[],1) reshape(AveAngleWristRightFlexionTransfer,[],1)];
TableWristLeftFlexionReverseTransfer=[reshape(InitialAngleWristLeftFlexionReverseTransfer,[],1) reshape(MaxAngleWristLeftFlexionReverseTransfer,[],1) reshape(MinAngleWristLeftFlexionReverseTransfer,[],1) reshape(ROMAngleWristLeftFlexionReverseTransfer,[],1) reshape(AveAngleWristLeftFlexionReverseTransfer,[],1)];
TableWristRightFlexionReverseTransfer=[reshape(InitialAngleWristRightFlexionReverseTransfer,[],1) reshape(MaxAngleWristRightFlexionReverseTransfer,[],1) reshape(MinAngleWristRightFlexionReverseTransfer,[],1) reshape(ROMAngleWristRightFlexionReverseTransfer,[],1) reshape(AveAngleWristRightFlexionReverseTransfer,[],1)];
TableTrunkFlexionTransfer=[reshape(InitialAngleTrunkFlexionTransfer,[],1) reshape(MaxAngleTrunkFlexionTransfer,[],1) reshape(MinAngleTrunkFlexionTransfer,[],1) reshape(ROMAngleTrunkFlexionTransfer,[],1) reshape(AveAngleTrunkFlexionTransfer,[],1)];
TableTrunkFlexionReverseTransfer=[reshape(InitialAngleTrunkFlexionReverseTransfer,[],1) reshape(MaxAngleTrunkFlexionReverseTransfer,[],1) reshape(MinAngleTrunkFlexionReverseTransfer,[],1) reshape(ROMAngleTrunkFlexionReverseTransfer,[],1) reshape(AveAngleTrunkFlexionReverseTransfer,[],1)];
TableTrunkYHeadTransfer=[reshape(InitialAngleTrunkYHeadTransfer,[],1) reshape(MaxAngleTrunkYHeadTransfer,[],1) reshape(MinAngleTrunkYHeadTransfer,[],1) reshape(ROMAngleTrunkYHeadTransfer,[],1) reshape(AveAngleTrunkYHeadTransfer,[],1)];
TableTrunkYHeadReverseTransfer=[reshape(InitialAngleTrunkYHeadReverseTransfer,[],1) reshape(MaxAngleTrunkYHeadReverseTransfer,[],1) reshape(MinAngleTrunkYHeadReverseTransfer,[],1) reshape(ROMAngleTrunkYHeadReverseTransfer,[],1) reshape(AveAngleTrunkYHeadReverseTransfer,[],1)];
TableTrunkYMidSpineTransfer=[reshape(InitialAngleTrunkYMidSpineTransfer,[],1) reshape(MaxAngleTrunkYMidSpineTransfer,[],1) reshape(MinAngleTrunkYMidSpineTransfer,[],1) reshape(ROMAngleTrunkYMidSpineTransfer,[],1) reshape(AveAngleTrunkYMidSpineTransfer,[],1)];
TableTrunkYMidSpineReverseTransfer=[reshape(InitialAngleTrunkYMidSpineReverseTransfer,[],1) reshape(MaxAngleTrunkYMidSpineReverseTransfer,[],1) reshape(MinAngleTrunkYMidSpineReverseTransfer,[],1) reshape(ROMAngleTrunkYMidSpineReverseTransfer,[],1) reshape(AveAngleTrunkYMidSpineReverseTransfer,[],1)];

% These were added recently.
TableTrunkBendingRawTransfer=[reshape(InitialAngleTrunkBendingRawTransfer,[],1) reshape(MaxAngleTrunkBendingRawTransfer,[],1) reshape(MinAngleTrunkBendingRawTransfer,[],1) reshape(ROMAngleTrunkBendingRawTransfer,[],1) reshape(AveAngleTrunkBendingRawTransfer,[],1)];
TableTrunkBendingRawReverseTransfer=[reshape(InitialAngleTrunkBendingRawReverseTransfer,[],1) reshape(MaxAngleTrunkBendingRawReverseTransfer,[],1) reshape(MinAngleTrunkBendingRawReverseTransfer,[],1) reshape(ROMAngleTrunkBendingRawReverseTransfer,[],1) reshape(AveAngleTrunkBendingRawReverseTransfer,[],1)];
TableTrunkRotationTVTransfer=[reshape(InitialAngleTrunkRotationTVTransfer,[],1) reshape(MaxAngleTrunkRotationTVTransfer,[],1) reshape(MinAngleTrunkRotationTVTransfer,[],1) reshape(ROMAngleTrunkRotationTVTransfer,[],1) reshape(AveAngleTrunkRotationTVTransfer,[],1)];
TableTrunkRotationTVReverseTransfer=[reshape(InitialAngleTrunkRotationTVReverseTransfer,[],1) reshape(MaxAngleTrunkRotationTVReverseTransfer,[],1) reshape(MinAngleTrunkRotationTVReverseTransfer,[],1) reshape(ROMAngleTrunkRotationTVReverseTransfer,[],1) reshape(AveAngleTrunkRotationTVReverseTransfer,[],1)];
TableTrunkBendingFrontalTransfer=[reshape(InitialAngleTrunkBendingFrontalTransfer,[],1) reshape(MaxAngleTrunkBendingFrontalTransfer,[],1) reshape(MinAngleTrunkBendingFrontalTransfer,[],1) reshape(ROMAngleTrunkBendingFrontalTransfer,[],1) reshape(AveAngleTrunkBendingFrontalTransfer,[],1)];
TableTrunkBendingFrontalReverseTransfer=[reshape(InitialAngleTrunkBendingFrontalReverseTransfer,[],1) reshape(MaxAngleTrunkBendingFrontalReverseTransfer,[],1) reshape(MinAngleTrunkBendingFrontalReverseTransfer,[],1) reshape(ROMAngleTrunkBendingFrontalReverseTransfer,[],1) reshape(AveAngleTrunkBendingFrontalReverseTransfer,[],1)];
TableWristLeftFlexionTipTransfer=[reshape(InitialAngleWristLeftFlexionTipTransfer,[],1) reshape(MaxAngleWristLeftFlexionTipTransfer,[],1) reshape(MinAngleWristLeftFlexionTipTransfer,[],1) reshape(ROMAngleWristLeftFlexionTipTransfer,[],1) reshape(AveAngleWristLeftFlexionTipTransfer,[],1)];
TableWristLeftFlexionTipReverseTransfer=[reshape(InitialAngleWristLeftFlexionTipReverseTransfer,[],1) reshape(MaxAngleWristLeftFlexionTipReverseTransfer,[],1) reshape(MinAngleWristLeftFlexionTipReverseTransfer,[],1) reshape(ROMAngleWristLeftFlexionTipReverseTransfer,[],1) reshape(AveAngleWristLeftFlexionTipReverseTransfer,[],1)];
TableWristRightFlexionTipTransfer=[reshape(InitialAngleWristRightFlexionTipTransfer,[],1) reshape(MaxAngleWristRightFlexionTipTransfer,[],1) reshape(MinAngleWristRightFlexionTipTransfer,[],1) reshape(ROMAngleWristRightFlexionTipTransfer,[],1) reshape(AveAngleWristRightFlexionTipTransfer,[],1)];
TableWristRightFlexionTipReverseTransfer=[reshape(InitialAngleWristRightFlexionTipReverseTransfer,[],1) reshape(MaxAngleWristRightFlexionTipReverseTransfer,[],1) reshape(MinAngleWristRightFlexionTipReverseTransfer,[],1) reshape(ROMAngleWristRightFlexionTipReverseTransfer,[],1) reshape(AveAngleWristRightFlexionTipReverseTransfer,[],1)];

TableShoulderToWristLeftElevationTransfer=[reshape(InitialAngleShoulderToWristLeftElevationTransfer,[],1) reshape(MaxAngleShoulderToWristLeftElevationTransfer,[],1) reshape(MinAngleShoulderToWristLeftElevationTransfer,[],1) reshape(ROMAngleShoulderToWristLeftElevationTransfer,[],1) reshape(AveAngleShoulderToWristLeftElevationTransfer,[],1)];
TableShoulderToWristLeftElevationReverseTransfer=[reshape(InitialAngleShoulderToWristLeftElevationReverseTransfer,[],1) reshape(MaxAngleShoulderToWristLeftElevationReverseTransfer,[],1) reshape(MinAngleShoulderToWristLeftElevationReverseTransfer,[],1) reshape(ROMAngleShoulderToWristLeftElevationReverseTransfer,[],1) reshape(AveAngleShoulderToWristLeftElevationReverseTransfer,[],1)];
TableShoulderToWristRightElevationTransfer=[reshape(InitialAngleShoulderToWristRightElevationTransfer,[],1) reshape(MaxAngleShoulderToWristRightElevationTransfer,[],1) reshape(MinAngleShoulderToWristRightElevationTransfer,[],1) reshape(ROMAngleShoulderToWristRightElevationTransfer,[],1) reshape(AveAngleShoulderToWristRightElevationTransfer,[],1)];
TableShoulderToWristRightElevationReverseTransfer=[reshape(InitialAngleShoulderToWristRightElevationReverseTransfer,[],1) reshape(MaxAngleShoulderToWristRightElevationReverseTransfer,[],1) reshape(MinAngleShoulderToWristRightElevationReverseTransfer,[],1) reshape(ROMAngleShoulderToWristRightElevationReverseTransfer,[],1) reshape(AveAngleShoulderToWristRightElevationReverseTransfer,[],1)];
TableShoulderToWristLeftPOETransfer=[reshape(InitialAngleShoulderToWristLeftPOETransfer,[],1) reshape(MaxAngleShoulderToWristLeftPOETransfer,[],1) reshape(MinAngleShoulderToWristLeftPOETransfer,[],1) reshape(ROMAngleShoulderToWristLeftPOETransfer,[],1) reshape(AveAngleShoulderToWristLeftPOETransfer,[],1)];
TableShoulderToWristLeftPOEReverseTransfer=[reshape(InitialAngleShoulderToWristLeftPOEReverseTransfer,[],1) reshape(MaxAngleShoulderToWristLeftPOEReverseTransfer,[],1) reshape(MinAngleShoulderToWristLeftPOEReverseTransfer,[],1) reshape(ROMAngleShoulderToWristLeftPOEReverseTransfer,[],1) reshape(AveAngleShoulderToWristLeftPOEReverseTransfer,[],1)];
TableShoulderToWristRightPOETransfer=[reshape(InitialAngleShoulderToWristRightPOETransfer,[],1) reshape(MaxAngleShoulderToWristRightPOETransfer,[],1) reshape(MinAngleShoulderToWristRightPOETransfer,[],1) reshape(ROMAngleShoulderToWristRightPOETransfer,[],1) reshape(AveAngleShoulderToWristRightPOETransfer,[],1)];
TableShoulderToWristRightPOEReverseTransfer=[reshape(InitialAngleShoulderToWristRightPOEReverseTransfer,[],1) reshape(MaxAngleShoulderToWristRightPOEReverseTransfer,[],1) reshape(MinAngleShoulderToWristRightPOEReverseTransfer,[],1) reshape(ROMAngleShoulderToWristRightPOEReverseTransfer,[],1) reshape(AveAngleShoulderToWristRightPOEReverseTransfer,[],1)];

TableAreaHandGripLeftTransfer=[reshape(InitialAreaHandGripLeftTransfer,[],1) reshape(MaxAreaHandGripLeftTransfer,[],1) reshape(MinAreaHandGripLeftTransfer,[],1) reshape(ROMAreaHandGripLeftTransfer,[],1) reshape(AveAreaHandGripLeftTransfer,[],1)];
TableAreaHandGripLeftReverseTransfer=[reshape(InitialAreaHandGripLeftReverseTransfer,[],1) reshape(MaxAreaHandGripLeftReverseTransfer,[],1) reshape(MinAreaHandGripLeftReverseTransfer,[],1) reshape(ROMAreaHandGripLeftReverseTransfer,[],1) reshape(AveAreaHandGripLeftReverseTransfer,[],1)];
TableAreaHandGripRightTransfer=[reshape(InitialAreaHandGripRightTransfer,[],1) reshape(MaxAreaHandGripRightTransfer,[],1) reshape(MinAreaHandGripRightTransfer,[],1) reshape(ROMAreaHandGripRightTransfer,[],1) reshape(AveAreaHandGripRightTransfer,[],1)];
TableAreaHandGripRightReverseTransfer=[reshape(InitialAreaHandGripRightReverseTransfer,[],1) reshape(MaxAreaHandGripRightReverseTransfer,[],1) reshape(MinAreaHandGripRightReverseTransfer,[],1) reshape(ROMAreaHandGripRightReverseTransfer,[],1) reshape(AveAreaHandGripRightReverseTransfer,[],1)];

TableHeadHipVelocityTransfer=[reshape(InitialHeadHipVelocityTransfer,[],1) reshape(MaxHeadHipVelocityTransfer,[],1) reshape(MinHeadHipVelocityTransfer,[],1) reshape(ROMHeadHipVelocityTransfer,[],1) reshape(AveHeadHipVelocityTransfer,[],1)];
TableHeadHipVelocityReverseTransfer=[reshape(InitialHeadHipVelocityReverseTransfer,[],1) reshape(MaxHeadHipVelocityReverseTransfer,[],1) reshape(MinHeadHipVelocityReverseTransfer,[],1) reshape(ROMHeadHipVelocityReverseTransfer,[],1) reshape(AveHeadHipVelocityReverseTransfer,[],1)];
TableHeadHipAccelTransfer=[reshape(InitialHeadHipAccelTransfer,[],1) reshape(MaxHeadHipAccelTransfer,[],1) reshape(MinHeadHipAccelTransfer,[],1) reshape(ROMHeadHipAccelTransfer,[],1) reshape(AveHeadHipAccelTransfer,[],1)];
TableHeadHipAccelReverseTransfer=[reshape(InitialHeadHipAccelReverseTransfer,[],1) reshape(MaxHeadHipAccelReverseTransfer,[],1) reshape(MinHeadHipAccelReverseTransfer,[],1) reshape(ROMHeadHipAccelReverseTransfer,[],1) reshape(AveHeadHipAccelReverseTransfer,[],1)];

warning('off','MATLAB:xlswrite:AddSheet')

%% Create new Excel file with all of the results.  Each sheet contains data for a different joint angle.
newfilename='Kinect Analysis Full TimePointsNVWG.xlsx';
Names={'Trial','Initial Position','Max Angle','Min Angle','ROM(Max-Min)','Average Angle'};

xlswrite(newfilename,Names,'ShoulderLeftPOET','A1');
xlswrite(newfilename,Trials,'ShoulderLeftPOET','A2');
xlswrite(newfilename,TableShoulderLeftPOETransfer,'ShoulderLeftPOET','B2');

xlswrite(newfilename,Names,'ShoulderRightPOET','A1');
xlswrite(newfilename,Trials,'ShoulderRightPOET','A2');
xlswrite(newfilename,TableShoulderRightPOETransfer,'ShoulderRightPOET','B2');

xlswrite(newfilename,Names,'ShoulderLeftPOEReverseT','A1');
xlswrite(newfilename,Trials,'ShoulderLeftPOEReverseT','A2');
xlswrite(newfilename,TableShoulderLeftPOEReverseTransfer,'ShoulderLeftPOEReverseT','B2');

xlswrite(newfilename,Names,'ShoulderRightPOEReverseT','A1');
xlswrite(newfilename,Trials,'ShoulderRightPOEReverseT','A2');
xlswrite(newfilename,TableShoulderRightPOEReverseTransfer,'ShoulderRightPOEReverseT','B2');

xlswrite(newfilename,Names,'ShoulderLeftElevationT','A1');
xlswrite(newfilename,Trials,'ShoulderLeftElevationT','A2');
xlswrite(newfilename,TableShoulderLeftElevationTransfer,'ShoulderLeftElevationT','B2');

xlswrite(newfilename,Names,'ShoulderRightElevationT','A1');
xlswrite(newfilename,Trials,'ShoulderRightElevationT','A2');
xlswrite(newfilename,TableShoulderRightElevationTransfer,'ShoulderRightElevationT','B2');

xlswrite(newfilename,Names,'ShoulderLeftElevationReverseT','A1');
xlswrite(newfilename,Trials,'ShoulderLeftElevationReverseT','A2');
xlswrite(newfilename,TableShoulderLeftElevationReverseTransfer,'ShoulderLeftElevationReverseT','B2');

xlswrite(newfilename,Names,'ShoulderRightElevationRT','A1');
xlswrite(newfilename,Trials,'ShoulderRightElevationRT','A2');
xlswrite(newfilename,TableShoulderRightElevationReverseTransfer,'ShoulderRightElevationRT','B2');

xlswrite(newfilename,Names,'ElbowLeftFlexionT','A1');
xlswrite(newfilename,Trials,'ElbowLeftFlexionT','A2');
xlswrite(newfilename,TableElbowLeftFlexionTransfer,'ElbowLeftFlexionT','B2');

xlswrite(newfilename,Names,'ElbowRightFlexionT','A1');
xlswrite(newfilename,Trials,'ElbowRightFlexionT','A2');
xlswrite(newfilename,TableElbowRightFlexionTransfer,'ElbowRightFlexionT','B2');

xlswrite(newfilename,Names,'ElbowLeftFlexionReverseT','A1');
xlswrite(newfilename,Trials,'ElbowLeftFlexionReverseT','A2');
xlswrite(newfilename,TableElbowLeftFlexionReverseTransfer,'ElbowLeftFlexionReverseT','B2');

xlswrite(newfilename,Names,'ElbowRightFlexionReverseT','A1');
xlswrite(newfilename,Trials,'ElbowRightFlexionReverseT','A2');
xlswrite(newfilename,TableElbowRightFlexionReverseTransfer,'ElbowRightFlexionReverseT','B2');

xlswrite(newfilename,Names,'WristLeftFlexionT','A1');
xlswrite(newfilename,Trials,'WristLeftFlexionT','A2');
xlswrite(newfilename,TableWristLeftFlexionTransfer,'WristLeftFlexionT','B2');

xlswrite(newfilename,Names,'WristRightFlexionT','A1');
xlswrite(newfilename,Trials,'WristRightFlexionT','A2');
xlswrite(newfilename,TableWristRightFlexionTransfer,'WristRightFlexionT','B2');

xlswrite(newfilename,Names,'WristLeftFlexionReverseT','A1');
xlswrite(newfilename,Trials,'WristLeftFlexionReverseT','A2');
xlswrite(newfilename,TableWristLeftFlexionReverseTransfer,'WristLeftFlexionReverseT','B2');

xlswrite(newfilename,Names,'WristRightFlexionReverseT','A1');
xlswrite(newfilename,Trials,'WristRightFlexionReverseT','A2');
xlswrite(newfilename,TableWristRightFlexionReverseTransfer,'WristRightFlexionReverseT','B2');

xlswrite(newfilename,Names,'TrunkFlexionT','A1');
xlswrite(newfilename,Trials,'TrunkFlexionT','A2');
xlswrite(newfilename,TableTrunkFlexionTransfer,'TrunkFlexionT','B2');

xlswrite(newfilename,Names,'TrunkFlexionReverseT','A1');
xlswrite(newfilename,Trials,'TrunkFlexionReverseT','A2');
xlswrite(newfilename,TableTrunkFlexionReverseTransfer,'TrunkFlexionReverseT','B2');

xlswrite(newfilename,Names,'TrunkYHeadT','A1');
xlswrite(newfilename,Trials,'TrunkYHeadT','A2');
xlswrite(newfilename,TableTrunkYHeadTransfer,'TrunkYHeadT','B2');

xlswrite(newfilename,Names,'TrunkYHeadReverseT','A1');
xlswrite(newfilename,Trials,'TrunkYHeadReverseT','A2');
xlswrite(newfilename,TableTrunkYHeadReverseTransfer,'TrunkYHeadReverseT','B2');

xlswrite(newfilename,Names,'TrunkYMidSpineT','A1');
xlswrite(newfilename,Trials,'TrunkYMidSpineT','A2');
xlswrite(newfilename,TableTrunkYMidSpineTransfer,'TrunkYMidSpineT','B2');

xlswrite(newfilename,Names,'TrunkYMidSpineReverseT','A1');
xlswrite(newfilename,Trials,'TrunkYMidSpineReverseT','A2');
xlswrite(newfilename,TableTrunkYMidSpineReverseTransfer,'TrunkYMidSpineReverseT','B2');


% New stuff

xlswrite(newfilename,Names,'TrunkBendingRawT','A1');
xlswrite(newfilename,Trials,'TrunkBendingRawT','A2');
xlswrite(newfilename,TableTrunkBendingRawTransfer,'TrunkBendingRawT','B2');

xlswrite(newfilename,Names,'TrunkBendingRawReverseT','A1');
xlswrite(newfilename,Trials,'TrunkBendingRawReverseT','A2');
xlswrite(newfilename,TableTrunkBendingRawReverseTransfer,'TrunkBendingRawReverseT','B2');

xlswrite(newfilename,Names,'TrunkRotationTVT','A1');
xlswrite(newfilename,Trials,'TrunkRotationTVT','A2');
xlswrite(newfilename,TableTrunkRotationTVTransfer,'TrunkRotationTVT','B2');

xlswrite(newfilename,Names,'TrunkRotationTVReverseT','A1');
xlswrite(newfilename,Trials,'TrunkRotationTVReverseT','A2');
xlswrite(newfilename,TableTrunkRotationTVReverseTransfer,'TrunkRotationTVReverseT','B2');

xlswrite(newfilename,Names,'TrunkBendingFrontalT','A1');
xlswrite(newfilename,Trials,'TrunkBendingFrontalT','A2');
xlswrite(newfilename,TableTrunkBendingFrontalTransfer,'TrunkBendingFrontalT','B2');

xlswrite(newfilename,Names,'TrunkBendingFrontalReverseT','A1');
xlswrite(newfilename,Trials,'TrunkBendingFrontalReverseT','A2');
xlswrite(newfilename,TableTrunkBendingFrontalReverseTransfer,'TrunkBendingFrontalReverseT','B2');

xlswrite(newfilename,Names,'WristLeftFlexionTipT','A1');
xlswrite(newfilename,Trials,'WristLeftFlexionTipT','A2');
xlswrite(newfilename,TableWristLeftFlexionTipTransfer,'WristLeftFlexionTipT','B2');

xlswrite(newfilename,Names,'WristLeftFlexionTipReverseT','A1');
xlswrite(newfilename,Trials,'WristLeftFlexionTipReverseT','A2');
xlswrite(newfilename,TableWristLeftFlexionTipReverseTransfer,'WristLeftFlexionTipReverseT','B2');

xlswrite(newfilename,Names,'WristRightFlexionTipT','A1');
xlswrite(newfilename,Trials,'WristRightFlexionTipT','A2');
xlswrite(newfilename,TableWristRightFlexionTipTransfer,'WristRightFlexionTipT','B2');

xlswrite(newfilename,Names,'WristRightFlexionTipReverseT','A1');
xlswrite(newfilename,Trials,'WristRightFlexionTipReverseT','A2');
xlswrite(newfilename,TableWristRightFlexionTipReverseTransfer,'WristRightFlexionTipReverseT','B2');

xlswrite(newfilename,Names,'ShoulderToWristLeftElevationT','A1');
xlswrite(newfilename,Trials,'ShoulderToWristLeftElevationT','A2');
xlswrite(newfilename,TableShoulderToWristLeftElevationTransfer,'ShoulderToWristLeftElevationT','B2');

xlswrite(newfilename,Names,'ShoulderToWristLeftElevationRT','A1');
xlswrite(newfilename,Trials,'ShoulderToWristLeftElevationRT','A2');
xlswrite(newfilename,TableShoulderToWristLeftElevationReverseTransfer,'ShoulderToWristLeftElevationRT','B2');

xlswrite(newfilename,Names,'ShoulderToWristRightElevationT','A1');
xlswrite(newfilename,Trials,'ShoulderToWristRightElevationT','A2');
xlswrite(newfilename,TableShoulderToWristRightElevationTransfer,'ShoulderToWristRightElevationT','B2');

xlswrite(newfilename,Names,'ShoulderToWristRightElevationRT','A1');
xlswrite(newfilename,Trials,'ShoulderToWristRightElevationRT','A2');
xlswrite(newfilename,TableShoulderToWristRightElevationReverseTransfer,'ShoulderToWristRightElevationRT','B2');

xlswrite(newfilename,Names,'ShoulderToWristLeftPOET','A1');
xlswrite(newfilename,Trials,'ShoulderToWristLeftPOET','A2');
xlswrite(newfilename,TableShoulderToWristLeftPOETransfer,'ShoulderToWristLeftPOET','B2');

xlswrite(newfilename,Names,'ShoulderToWristLeftPOEReverseT','A1');
xlswrite(newfilename,Trials,'ShoulderToWristLeftPOEReverseT','A2');
xlswrite(newfilename,TableShoulderToWristLeftPOEReverseTransfer,'ShoulderToWristLeftPOEReverseT','B2');

xlswrite(newfilename,Names,'ShoulderToWristRightPOET','A1');
xlswrite(newfilename,Trials,'ShoulderToWristRightPOET','A2');
xlswrite(newfilename,TableShoulderToWristRightPOETransfer,'ShoulderToWristRightPOET','B2');

xlswrite(newfilename,Names,'ShoulderToWristRightPOEReverseT','A1');
xlswrite(newfilename,Trials,'ShoulderToWristRightPOEReverseT','A2');
xlswrite(newfilename,TableShoulderToWristRightPOEReverseTransfer,'ShoulderToWristRightPOEReverseT','B2');

xlswrite(newfilename,Names,'AreaHandGripLeftT','A1');
xlswrite(newfilename,Trials,'AreaHandGripLeftT','A2');
xlswrite(newfilename,TableAreaHandGripLeftTransfer,'AreaHandGripLeftT','B2');

xlswrite(newfilename,Names,'AreaHandGripLeftRT','A1');
xlswrite(newfilename,Trials,'AreaHandGripLeftRT','A2');
xlswrite(newfilename,TableAreaHandGripLeftReverseTransfer,'AreaHandGripLeftRT','B2');

xlswrite(newfilename,Names,'AreaHandGripRightT','A1');
xlswrite(newfilename,Trials,'AreaHandGripRightT','A2');
xlswrite(newfilename,TableAreaHandGripRightTransfer,'AreaHandGripRightT','B2');

xlswrite(newfilename,Names,'AreaHandGripRightRT','A1');
xlswrite(newfilename,Trials,'AreaHandGripRightRT','A2');
xlswrite(newfilename,TableAreaHandGripRightReverseTransfer,'AreaHandGripRightRT','B2');

xlswrite(newfilename,Names,'HeadHipVelocityTransfer','A1');
xlswrite(newfilename,Trials,'HeadHipVelocityTransfer','A2');
xlswrite(newfilename,TableHeadHipVelocityTransfer,'HeadHipVelocityTransfer','B2');

xlswrite(newfilename,Names,'HeadHipVelocityRT','A1');
xlswrite(newfilename,Trials,'HeadHipVelocityRT','A2');
xlswrite(newfilename,TableHeadHipVelocityReverseTransfer,'HeadHipVelocityRT','B2');

xlswrite(newfilename,Names,'HeadHipAccelTransfer','A1');
xlswrite(newfilename,Trials,'HeadHipAccelTransfer','A2');
xlswrite(newfilename,TableHeadHipAccelTransfer,'HeadHipAccelTransfer','B2');

xlswrite(newfilename,Names,'HeadHipAccelRT','A1');
xlswrite(newfilename,Trials,'HeadHipAccelRT','A2');
xlswrite(newfilename,TableHeadHipAccelReverseTransfer,'HeadHipAccelRT','B2');

%xlswrite(newfilename,InitialAngleElbowFlexion,'Analysis','B2');
%xlswrite(newfilename,MaxAngleElbowFlexion,'Analysis','C2');
%xlswrite(newfilename,MinAngleElbowFlexion,'Analysis','D2');
%xlswrite(newfilename,ROMAngleElbowFlexion,'Analysis','E2');
%xlswrite(newfilename,AveAngleElbowFlexion,'Analysis','F2');

%filename = fullinputname;
%Names2={'Elapsed Milliseconds','Shoulder POE','Smooth Shoulder POE','Shoulder Elevation','Elbow Flexion', 'Wrist Flexion', 'Trunk Flexion'};
%xlswrite(newfilename,Names2,'Angles','A1')
%xlswrite(newfilename,M2,'Angles','A2')
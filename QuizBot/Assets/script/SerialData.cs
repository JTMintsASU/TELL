//This class is used to hold data when it is transferred from app cache memory to local data files
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class SerialData
{
    public string sRecordId;
    public string sTeacherID;
    public string sAssessorID;
    public string sChildID;
    public string sClassroomID;

    //Vocab storage
    public double[] sGradeVocabExp;
    public double[] sGradeVocabRec;
    public double[] sGradeVocabTotal;
    public List<List<bool>> sIndividualExpressiveList;
    public List<List<bool>> sIndividualExpressiveFlagList;
    public List<List<bool>> sIndividualReceptiveList;
    public List<List<bool>> sIndividualReceptiveFlagList;
    public List<List<string>> sIndividualResponses;

    //LNI Storage
    [FormerlySerializedAs("sLearnedLetterNames")] public bool[] sLearnedLetterNamesLNI;
    public AdaptiveResponse[,] sIndividual_LNI;

    //LSI Storage
    public bool[] sLearnedLetterNamesLSI;
    public AdaptiveResponse[,] sIndividual_LSI;
    
    //BS Storage
    public AdaptiveResponse[,] sIndividual_BS;
    public string[,] sIndividual_BSChildResponse;
    public Tuple<double, double>[] final_BSscores;

    public static SerialData convertToSerialData(UsersDetails usersDetails)
    {
        SerialData serialData = new SerialData();
        List<List<bool>> expressiveList = new List<List<bool>>(){Capacity = 6};;
        List<List<bool>> expressiveFlagList = new List<List<bool>>(){Capacity = 6};;
        List<List<bool>> receptiveList = new List<List<bool>>(){Capacity = 6};;
        List<List<bool>> receptiveFlagList = new List<List<bool>>(){Capacity = 6};;
        List<List<string>> individualResponses = new List<List<string>>(){Capacity = 6};;
        
        foreach (var redCapRecord in usersDetails.users)
        {
            if (redCapRecord.recordID != null && redCapRecord.recordID != 0)
                serialData.sRecordId = redCapRecord.recordID.ToString();
            if (!string.IsNullOrEmpty(redCapRecord.assessorID))
                serialData.sAssessorID = redCapRecord.assessorID;
            if (!string.IsNullOrEmpty(redCapRecord.childID))
                serialData.sChildID = redCapRecord.childID;
            if (!string.IsNullOrEmpty(redCapRecord.classroomID))
                serialData.sClassroomID = redCapRecord.classroomID;
            if (!string.IsNullOrEmpty(redCapRecord.teacherID))
                serialData.sTeacherID = redCapRecord.teacherID;

            // Responsible for storing vocab related data
            if (redCapRecord.vocabSession != null && redCapRecord.vocabSession != 0)
            {
                List<bool> sessionExpressiveList = new List<bool>{false, false, false, false, false, false};
                List<bool> sessionExpressiveFlagList = new List<bool>{false, false, false, false, false, false};
                List<bool> sessionReceptiveList = new List<bool>{false, false, false, false, false, false};
                List<bool> sessionReceptiveFlagList = new List<bool>{false, false, false, false, false, false};
                List<string> sessionIndividualResponses = new List<string>{"", "", "", "", "", ""};
                if (serialData.sGradeVocabExp == null)
                    serialData.sGradeVocabExp = new double[6] { -1, -1, -1, -1, -1, -1 };
                if (serialData.sGradeVocabRec == null)
                    serialData.sGradeVocabRec = new double[6] { -1, -1, -1, -1, -1, -1 };
                if (serialData.sGradeVocabTotal == null)
                    serialData.sGradeVocabTotal = new double[6] { -1, -1, -1, -1, -1, -1 };
                
                if (redCapRecord.q1Expressive != null)
                    sessionExpressiveList[0] = getTrueFalse(redCapRecord.q1Expressive);
                if (redCapRecord.q2Expressive != null)
                    sessionExpressiveList[1] = getTrueFalse(redCapRecord.q2Expressive);
                if (redCapRecord.q3Expressive != null)
                    sessionExpressiveList[2] = getTrueFalse(redCapRecord.q3Expressive);
                if (redCapRecord.q4Expressive != null)
                    sessionExpressiveList[3] = getTrueFalse(redCapRecord.q4Expressive);
                if (redCapRecord.q5Expressive != null)
                    sessionExpressiveList[4] = getTrueFalse(redCapRecord.q5Expressive);
                if (redCapRecord.q6Expressive != null)
                    sessionExpressiveList[5] = getTrueFalse(redCapRecord.q6Expressive);
                
                if (redCapRecord.q1Receptive != null)
                    sessionReceptiveList[0] = getTrueFalse(redCapRecord.q1Receptive);
                if (redCapRecord.q2Receptive != null)
                    sessionReceptiveList[1] = getTrueFalse(redCapRecord.q2Receptive);
                if (redCapRecord.q3Receptive != null)
                    sessionReceptiveList[2] = getTrueFalse(redCapRecord.q3Receptive);
                if (redCapRecord.q4Receptive != null)
                    sessionReceptiveList[3] = getTrueFalse(redCapRecord.q4Receptive);
                if (redCapRecord.q5Receptive != null)
                    sessionReceptiveList[4] = getTrueFalse(redCapRecord.q5Receptive);
                if (redCapRecord.q6Receptive != null)
                    sessionReceptiveList[5] = getTrueFalse(redCapRecord.q6Receptive);
                
                if (redCapRecord.q1ExpressiveFlag != null)
                    sessionExpressiveFlagList[0] = getTrueFalse(redCapRecord.q1ExpressiveFlag);
                if (redCapRecord.q2ExpressiveFlag != null)
                    sessionExpressiveFlagList[1] = getTrueFalse(redCapRecord.q2ExpressiveFlag);
                if (redCapRecord.q3ExpressiveFlag != null)
                    sessionExpressiveFlagList[2] = getTrueFalse(redCapRecord.q3ExpressiveFlag);
                if (redCapRecord.q4ExpressiveFlag != null)
                    sessionExpressiveFlagList[3] = getTrueFalse(redCapRecord.q4ExpressiveFlag);
                if (redCapRecord.q5ExpressiveFlag != null)
                    sessionExpressiveFlagList[4] = getTrueFalse(redCapRecord.q5ExpressiveFlag);
                if (redCapRecord.q6ExpressiveFlag != null)
                    sessionExpressiveFlagList[5] = getTrueFalse(redCapRecord.q6ExpressiveFlag);
                
                if (redCapRecord.q1ReceptiveFlag != null)
                    sessionReceptiveFlagList[0] = getTrueFalse(redCapRecord.q1ReceptiveFlag);
                if (redCapRecord.q2ReceptiveFlag != null)
                    sessionReceptiveFlagList[1] = getTrueFalse(redCapRecord.q2ReceptiveFlag);
                if (redCapRecord.q3ReceptiveFlag != null)
                    sessionReceptiveFlagList[2] = getTrueFalse(redCapRecord.q3ReceptiveFlag);
                if (redCapRecord.q4ReceptiveFlag != null)
                    sessionReceptiveFlagList[3] = getTrueFalse(redCapRecord.q4ReceptiveFlag);
                if (redCapRecord.q5ReceptiveFlag != null)
                    sessionReceptiveFlagList[4] = getTrueFalse(redCapRecord.q5ReceptiveFlag);
                if (redCapRecord.q6ReceptiveFlag != null)
                    sessionReceptiveFlagList[5] = getTrueFalse(redCapRecord.q6ReceptiveFlag);
                
                if (!string.IsNullOrEmpty(redCapRecord.q1Solution))
                    sessionIndividualResponses[0] = redCapRecord.q1Solution;
                if (!string.IsNullOrEmpty(redCapRecord.q2Solution))
                    sessionIndividualResponses[1] = redCapRecord.q2Solution;
                if (!string.IsNullOrEmpty(redCapRecord.q3Solution))
                    sessionIndividualResponses[2] = redCapRecord.q3Solution;
                if (!string.IsNullOrEmpty(redCapRecord.q4Solution))
                    sessionIndividualResponses[3] = redCapRecord.q4Solution;
                if (!string.IsNullOrEmpty(redCapRecord.q5Solution))
                    sessionIndividualResponses[4] = redCapRecord.q5Solution;
                if (!string.IsNullOrEmpty(redCapRecord.q6Solution))
                    sessionIndividualResponses[5] = redCapRecord.q6Solution;
                
                while (expressiveList.Count < redCapRecord.vocabSession)
                    expressiveList.Add(new List<bool>());
                expressiveList[redCapRecord.vocabSession.Value-1] = sessionExpressiveList;
                
                while (receptiveList.Count < redCapRecord.vocabSession)
                    receptiveList.Add(new List<bool>());
                receptiveList[redCapRecord.vocabSession.Value-1] = sessionReceptiveList;
                
                while (expressiveFlagList.Count < redCapRecord.vocabSession)
                    expressiveFlagList.Add(new List<bool>());
                expressiveFlagList[redCapRecord.vocabSession.Value-1] = sessionExpressiveFlagList;
                
                while (receptiveFlagList.Count < redCapRecord.vocabSession)
                    receptiveFlagList.Add(new List<bool>());
                receptiveFlagList[redCapRecord.vocabSession.Value-1] = sessionReceptiveFlagList;
                
                while (individualResponses.Count < redCapRecord.vocabSession)
                    individualResponses.Add(new List<string>());
                individualResponses[redCapRecord.vocabSession.Value-1] = sessionIndividualResponses;

                addExpressiveAndReceptiveScore(sessionExpressiveList, 
                                               sessionReceptiveList, 
                                               redCapRecord.vocabSession.Value, 
                                               serialData);
            }

            //Populate sLearnedLetterNames
            serialData.sLearnedLetterNamesLNI = new bool[26] {false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false};
            if (redCapRecord.LNI_A != null)
                serialData.sLearnedLetterNamesLNI[0] = getTrueFalse(redCapRecord.LNI_A);
            if (redCapRecord.LNI_B != null)
                serialData.sLearnedLetterNamesLNI[1] = getTrueFalse(redCapRecord.LNI_B);
            if (redCapRecord.LNI_C != null)
                serialData.sLearnedLetterNamesLNI[2] = getTrueFalse(redCapRecord.LNI_C);
            if (redCapRecord.LNI_D != null)
                serialData.sLearnedLetterNamesLNI[3] = getTrueFalse(redCapRecord.LNI_D);
            if (redCapRecord.LNI_E != null)
                serialData.sLearnedLetterNamesLNI[4] = getTrueFalse(redCapRecord.LNI_E);
            if (redCapRecord.LNI_F != null)
                serialData.sLearnedLetterNamesLNI[5] = getTrueFalse(redCapRecord.LNI_F);
            if (redCapRecord.LNI_G != null)
                serialData.sLearnedLetterNamesLNI[6] = getTrueFalse(redCapRecord.LNI_G);
            if (redCapRecord.LNI_H != null)
                serialData.sLearnedLetterNamesLNI[7] = getTrueFalse(redCapRecord.LNI_H);
            if (redCapRecord.LNI_I != null)
                serialData.sLearnedLetterNamesLNI[8] = getTrueFalse(redCapRecord.LNI_I);
            if (redCapRecord.LNI_J != null)
                serialData.sLearnedLetterNamesLNI[9] = getTrueFalse(redCapRecord.LNI_J);
            if (redCapRecord.LNI_K != null)
                serialData.sLearnedLetterNamesLNI[10] = getTrueFalse(redCapRecord.LNI_K);
            if (redCapRecord.LNI_L != null)
                serialData.sLearnedLetterNamesLNI[11] = getTrueFalse(redCapRecord.LNI_L);
            if (redCapRecord.LNI_M != null)
                serialData.sLearnedLetterNamesLNI[12] = getTrueFalse(redCapRecord.LNI_M);
            if (redCapRecord.LNI_N != null)
                serialData.sLearnedLetterNamesLNI[13] = getTrueFalse(redCapRecord.LNI_N);
            if (redCapRecord.LNI_O != null)
                serialData.sLearnedLetterNamesLNI[14] = getTrueFalse(redCapRecord.LNI_O);
            if (redCapRecord.LNI_P != null)
                serialData.sLearnedLetterNamesLNI[15] = getTrueFalse(redCapRecord.LNI_P);
            if (redCapRecord.LNI_Q != null)
                serialData.sLearnedLetterNamesLNI[16] = getTrueFalse(redCapRecord.LNI_Q);
            if (redCapRecord.LNI_R != null)
                serialData.sLearnedLetterNamesLNI[17] = getTrueFalse(redCapRecord.LNI_R);
            if (redCapRecord.LNI_S != null)
                serialData.sLearnedLetterNamesLNI[18] = getTrueFalse(redCapRecord.LNI_S);
            if (redCapRecord.LNI_T != null)
                serialData.sLearnedLetterNamesLNI[19] = getTrueFalse(redCapRecord.LNI_T);
            if (redCapRecord.LNI_U != null)
                serialData.sLearnedLetterNamesLNI[20] = getTrueFalse(redCapRecord.LNI_U);
            if (redCapRecord.LNI_V != null)
                serialData.sLearnedLetterNamesLNI[21] = getTrueFalse(redCapRecord.LNI_V);
            if (redCapRecord.LNI_W != null)
                serialData.sLearnedLetterNamesLNI[22] = getTrueFalse(redCapRecord.LNI_W);
            if (redCapRecord.LNI_X != null)
                serialData.sLearnedLetterNamesLNI[23] = getTrueFalse(redCapRecord.LNI_X);
            if (redCapRecord.LNI_Y != null)
                serialData.sLearnedLetterNamesLNI[24] = getTrueFalse(redCapRecord.LNI_Y);
            if (redCapRecord.LNI_Z != null)
                serialData.sLearnedLetterNamesLNI[25] = getTrueFalse(redCapRecord.LNI_Z);


            //Populate sIndividual_LNI
            serialData.sIndividual_LNI = new AdaptiveResponse[26,6];
            if (redCapRecord.lnirSessionNumber != null && redCapRecord.lnirSessionNumber != 0)
            {
                int lniTime = (int)redCapRecord.lnirSessionNumber-1;
                if (redCapRecord.rLNI_A != null)
                    serialData.sIndividual_LNI[0, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_A;
                if (redCapRecord.rLNI_B != null)
                    serialData.sIndividual_LNI[1, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_B;
                if (redCapRecord.rLNI_C != null)
                    serialData.sIndividual_LNI[2, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_C;
                if (redCapRecord.rLNI_D != null)
                    serialData.sIndividual_LNI[3, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_D;
                if (redCapRecord.rLNI_E != null)
                    serialData.sIndividual_LNI[4, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_E;
                if (redCapRecord.rLNI_F != null)
                    serialData.sIndividual_LNI[5, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_F;
                if (redCapRecord.rLNI_G != null)
                    serialData.sIndividual_LNI[6, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_G;
                if (redCapRecord.rLNI_H != null)
                    serialData.sIndividual_LNI[7, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_H;
                if (redCapRecord.rLNI_I != null)
                    serialData.sIndividual_LNI[8, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_I;
                if (redCapRecord.rLNI_J != null)
                    serialData.sIndividual_LNI[9, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_J;
                if (redCapRecord.rLNI_K != null)
                    serialData.sIndividual_LNI[10, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_K;
                if (redCapRecord.rLNI_L != null)
                    serialData.sIndividual_LNI[11, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_L;
                if (redCapRecord.rLNI_M != null)
                    serialData.sIndividual_LNI[12, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_M;
                if (redCapRecord.rLNI_N != null)
                    serialData.sIndividual_LNI[13, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_N;
                if (redCapRecord.rLNI_O != null)
                    serialData.sIndividual_LNI[14, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_O;
                if (redCapRecord.rLNI_P != null)
                    serialData.sIndividual_LNI[15, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_P;
                if (redCapRecord.rLNI_Q != null)
                    serialData.sIndividual_LNI[16, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_Q;
                if (redCapRecord.rLNI_R != null)
                    serialData.sIndividual_LNI[17, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_R;
                if (redCapRecord.rLNI_S != null)
                    serialData.sIndividual_LNI[18, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_S;
                if (redCapRecord.rLNI_T != null)
                    serialData.sIndividual_LNI[19, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_T;
                if (redCapRecord.rLNI_U != null)
                    serialData.sIndividual_LNI[20, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_U;
                if (redCapRecord.rLNI_V != null)
                    serialData.sIndividual_LNI[21, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_V;
                if (redCapRecord.rLNI_W != null)
                    serialData.sIndividual_LNI[22, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_W;
                if (redCapRecord.rLNI_X != null)
                    serialData.sIndividual_LNI[23, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_X;
                if (redCapRecord.rLNI_Y != null)
                    serialData.sIndividual_LNI[24, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_Y;
                if (redCapRecord.rLNI_Z != null)
                    serialData.sIndividual_LNI[25, lniTime] = (AdaptiveResponse)(int)redCapRecord.rLNI_Z;
            }
            
            
            //---------
            
            //Populate sLearnedLetterNames
            serialData.sLearnedLetterNamesLSI = new bool[26] {false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false};
            if (redCapRecord.LSI_A != null)
                serialData.sLearnedLetterNamesLSI[0] = getTrueFalse(redCapRecord.LSI_A);
            if (redCapRecord.LSI_B != null)
                serialData.sLearnedLetterNamesLSI[1] = getTrueFalse(redCapRecord.LSI_B);
            if (redCapRecord.LSI_C != null)
                serialData.sLearnedLetterNamesLSI[2] = getTrueFalse(redCapRecord.LSI_C);
            if (redCapRecord.LSI_D != null)
                serialData.sLearnedLetterNamesLSI[3] = getTrueFalse(redCapRecord.LSI_D);
            if (redCapRecord.LSI_E != null)
                serialData.sLearnedLetterNamesLSI[4] = getTrueFalse(redCapRecord.LSI_E);
            if (redCapRecord.LSI_F != null)
                serialData.sLearnedLetterNamesLSI[5] = getTrueFalse(redCapRecord.LSI_F);
            if (redCapRecord.LSI_G != null)
                serialData.sLearnedLetterNamesLSI[6] = getTrueFalse(redCapRecord.LSI_G);
            if (redCapRecord.LSI_H != null)
                serialData.sLearnedLetterNamesLSI[7] = getTrueFalse(redCapRecord.LSI_H);
            if (redCapRecord.LSI_I != null)
                serialData.sLearnedLetterNamesLSI[8] = getTrueFalse(redCapRecord.LSI_I);
            if (redCapRecord.LSI_J != null)
                serialData.sLearnedLetterNamesLSI[9] = getTrueFalse(redCapRecord.LSI_J);
            if (redCapRecord.LSI_K != null)
                serialData.sLearnedLetterNamesLSI[10] = getTrueFalse(redCapRecord.LSI_K);
            if (redCapRecord.LSI_L != null)
                serialData.sLearnedLetterNamesLSI[11] = getTrueFalse(redCapRecord.LSI_L);
            if (redCapRecord.LSI_M != null)
                serialData.sLearnedLetterNamesLSI[12] = getTrueFalse(redCapRecord.LSI_M);
            if (redCapRecord.LSI_N != null)
                serialData.sLearnedLetterNamesLSI[13] = getTrueFalse(redCapRecord.LSI_N);
            if (redCapRecord.LSI_O != null)
                serialData.sLearnedLetterNamesLSI[14] = getTrueFalse(redCapRecord.LSI_O);
            if (redCapRecord.LSI_P != null)
                serialData.sLearnedLetterNamesLSI[15] = getTrueFalse(redCapRecord.LSI_P);
            if (redCapRecord.LSI_Q != null)
                serialData.sLearnedLetterNamesLSI[16] = getTrueFalse(redCapRecord.LSI_Q);
            if (redCapRecord.LSI_R != null)
                serialData.sLearnedLetterNamesLSI[17] = getTrueFalse(redCapRecord.LSI_R);
            if (redCapRecord.LSI_S != null)
                serialData.sLearnedLetterNamesLSI[18] = getTrueFalse(redCapRecord.LSI_S);
            if (redCapRecord.LSI_T != null)
                serialData.sLearnedLetterNamesLSI[19] = getTrueFalse(redCapRecord.LSI_T);
            if (redCapRecord.LSI_U != null)
                serialData.sLearnedLetterNamesLSI[20] = getTrueFalse(redCapRecord.LSI_U);
            if (redCapRecord.LSI_V != null)
                serialData.sLearnedLetterNamesLSI[21] = getTrueFalse(redCapRecord.LSI_V);
            if (redCapRecord.LSI_W != null)
                serialData.sLearnedLetterNamesLSI[22] = getTrueFalse(redCapRecord.LSI_W);
            if (redCapRecord.LSI_X != null)
                serialData.sLearnedLetterNamesLSI[23] = getTrueFalse(redCapRecord.LSI_X);
            if (redCapRecord.LSI_Y != null)
                serialData.sLearnedLetterNamesLSI[24] = getTrueFalse(redCapRecord.LSI_Y);
            if (redCapRecord.LSI_Z != null)
                serialData.sLearnedLetterNamesLSI[25] = getTrueFalse(redCapRecord.LSI_Z);


            //Populate sIndividual_LNI
            serialData.sIndividual_LSI = new AdaptiveResponse[26,6];
            if (redCapRecord.lsirSessionNumber != null && redCapRecord.lsirSessionNumber != 0)
            {
                int lsiTime = (int)redCapRecord.lsirSessionNumber-1;
                if (redCapRecord.rLSI_A != null)
                    serialData.sIndividual_LSI[0, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_A;
                if (redCapRecord.rLSI_B != null)
                    serialData.sIndividual_LSI[1, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_B;
                if (redCapRecord.rLSI_C != null)
                    serialData.sIndividual_LSI[2, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_C;
                if (redCapRecord.rLSI_D != null)
                    serialData.sIndividual_LSI[3, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_D;
                if (redCapRecord.rLSI_E != null)
                    serialData.sIndividual_LSI[4, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_E;
                if (redCapRecord.rLSI_F != null)
                    serialData.sIndividual_LSI[5, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_F;
                if (redCapRecord.rLSI_G != null)
                    serialData.sIndividual_LSI[6, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_G;
                if (redCapRecord.rLSI_H != null)
                    serialData.sIndividual_LSI[7, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_H;
                if (redCapRecord.rLSI_I != null)
                    serialData.sIndividual_LSI[8, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_I;
                if (redCapRecord.rLSI_J != null)
                    serialData.sIndividual_LSI[9, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_J;
                if (redCapRecord.rLSI_K != null)
                    serialData.sIndividual_LSI[10, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_K;
                if (redCapRecord.rLSI_L != null)
                    serialData.sIndividual_LSI[11, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_L;
                if (redCapRecord.rLSI_M != null)
                    serialData.sIndividual_LSI[12, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_M;
                if (redCapRecord.rLSI_N != null)
                    serialData.sIndividual_LSI[13, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_N;
                if (redCapRecord.rLSI_O != null)
                    serialData.sIndividual_LSI[14, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_O;
                if (redCapRecord.rLSI_P != null)
                    serialData.sIndividual_LSI[15, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_P;
                if (redCapRecord.rLSI_Q != null)
                    serialData.sIndividual_LSI[16, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_Q;
                if (redCapRecord.rLSI_R != null)
                    serialData.sIndividual_LSI[17, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_R;
                if (redCapRecord.rLSI_S != null)
                    serialData.sIndividual_LSI[18, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_S;
                if (redCapRecord.rLSI_T != null)
                    serialData.sIndividual_LSI[19, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_T;
                if (redCapRecord.rLSI_U != null)
                    serialData.sIndividual_LSI[20, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_U;
                if (redCapRecord.rLSI_V != null)
                    serialData.sIndividual_LSI[21, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_V;
                if (redCapRecord.rLSI_W != null)
                    serialData.sIndividual_LSI[22, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_W;
                if (redCapRecord.rLSI_X != null)
                    serialData.sIndividual_LSI[23, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_X;
                if (redCapRecord.rLSI_Y != null)
                    serialData.sIndividual_LSI[24, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_Y;
                if (redCapRecord.rLSI_Z != null)
                    serialData.sIndividual_LSI[25, lsiTime] = (AdaptiveResponse)(int)redCapRecord.rLSI_Z;
            }
        }
        

        serialData.sIndividualExpressiveList = expressiveList;
        serialData.sIndividualExpressiveFlagList = expressiveFlagList;
        serialData.sIndividualReceptiveList = receptiveList;
        serialData.sIndividualReceptiveFlagList = receptiveFlagList;
        serialData.sIndividualResponses = individualResponses;

        return serialData;
    }

    // Function responsible for returning bool truth value
    private static bool getTrueFalse(int? value)
    {
        return value != null && value == 1;
    }

    // Function responsible for calculating expressive and receptive percentage values
    private static void addExpressiveAndReceptiveScore(List<bool> sessionExpressiveList, 
                                                        List<bool> sessionReceptiveList,
                                                        int vocabSession,
                                                        SerialData serialData)
    {
        int index = vocabSession - 1;
        int score_expressive = sessionExpressiveList.Count(item => item);
        int score_receptive = sessionReceptiveList.Count(item => item);
        
        double totalQuestions = DataManager.vocabularyTotalQuestions;
        serialData.sGradeVocabExp[index] = (score_expressive / totalQuestions) * 100;
        serialData.sGradeVocabRec[index] = (score_receptive / totalQuestions) * 100;
        
        int score_total = score_expressive + score_receptive;
        serialData.sGradeVocabTotal[index] = (score_total / (totalQuestions * 2)) * 100;
    }
}

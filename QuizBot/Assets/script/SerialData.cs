//This class is used to hold data when it is transferred from app cache memory to local data files
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class SerialData
{
    public string sRecordId;
    public string sTeacherID;
    public string sAssessorID;
    public string sChildID;
    public string sClassroomID;

    //Grade storage
    public double[] sGradeVocabExp;
    public double[] sGradeVocabRec;
    public double[] sGradeVocabTotal;
    public List<List<bool>> sIndividualExpressiveList;
    public List<List<bool>> sIndividualExpressiveFlagList;
    public List<List<bool>> sIndividualReceptiveList;
    public List<List<bool>> sIndividualReceptiveFlagList;
    public List<List<string>> sIndividualResponses;
    
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
        }

        serialData.sIndividualExpressiveList = expressiveList;
        serialData.sIndividualExpressiveFlagList = expressiveFlagList;
        serialData.sIndividualReceptiveList = receptiveList;
        serialData.sIndividualReceptiveFlagList = receptiveFlagList;
        serialData.sIndividualResponses = individualResponses;
        return serialData;
    }

    private static bool getTrueFalse(int? value)
    {
        return value != null && value == 1;
    }

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

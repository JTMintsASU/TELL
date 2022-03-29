using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

[Serializable]
// DTO - Credentials
// Represents a sample record in "Credentials" Instrument in RedCap
public class RedCapRecord
{
    [JsonProperty("record_id", NullValueHandling = NullValueHandling.Ignore)]
    public int recordID;
    [JsonProperty("teacher_id", NullValueHandling = NullValueHandling.Ignore)] [CanBeNull]
    public string teacherID = null;
    [JsonProperty("assessor_id", NullValueHandling = NullValueHandling.Ignore)] [CanBeNull]
    public string assessorID = null;
    [JsonProperty("child_id", NullValueHandling = NullValueHandling.Ignore)] [CanBeNull]
    public string childID = null;
    [JsonProperty("classroom_id", NullValueHandling = NullValueHandling.Ignore)] [CanBeNull]
    public string classroomID = null;

    [JsonProperty("redcap_repeat_instrument", NullValueHandling = NullValueHandling.Ignore)] [CanBeNull]
    public string redcapRepeatInstrument = null;
    [JsonProperty("redcap_repeat_instance", NullValueHandling = NullValueHandling.Ignore)]
    public int? redcapRepeatInstance = null;
    
    [JsonProperty("vocab_session", NullValueHandling = NullValueHandling.Ignore)]
    public int? vocabSession = null;
    
    [JsonProperty("q1_expressive", NullValueHandling = NullValueHandling.Ignore)]
    public int? q1Expressive = null;
    [JsonProperty("q1_receptive", NullValueHandling = NullValueHandling.Ignore)]
    public int? q1Receptive = null;
    [JsonProperty("q1_expressive_flag", NullValueHandling = NullValueHandling.Ignore)]
    public int? q1ExpressiveFlag = null;
    [JsonProperty("q1_receptive_flag", NullValueHandling = NullValueHandling.Ignore)]
    public int? q1ReceptiveFlag = null;
    [JsonProperty("q1_solution", NullValueHandling = NullValueHandling.Ignore)] [CanBeNull]
    public string q1Solution = null;
    
    [JsonProperty("q2_expressive", NullValueHandling = NullValueHandling.Ignore)]
    public int? q2Expressive = null;
    [JsonProperty("q2_receptive", NullValueHandling = NullValueHandling.Ignore)]
    public int? q2Receptive = null;
    [JsonProperty("q2_expressive_flag", NullValueHandling = NullValueHandling.Ignore)]
    public int? q2ExpressiveFlag = null;
    [JsonProperty("q2_receptive_flag", NullValueHandling = NullValueHandling.Ignore)]
    public int? q2ReceptiveFlag = null;
    [JsonProperty("q2_solution", NullValueHandling = NullValueHandling.Ignore)] [CanBeNull]
    public string q2Solution = null;
    
    [JsonProperty("q3_expressive", NullValueHandling = NullValueHandling.Ignore)]
    public int? q3Expressive = null;
    [JsonProperty("q3_receptive", NullValueHandling = NullValueHandling.Ignore)]
    public int? q3Receptive = null;
    [JsonProperty("q3_expressive_flag", NullValueHandling = NullValueHandling.Ignore)]
    public int? q3ExpressiveFlag = null;
    [JsonProperty("q3_receptive_flag", NullValueHandling = NullValueHandling.Ignore)]
    public int? q3ReceptiveFlag = null;
    [JsonProperty("q3_solution", NullValueHandling = NullValueHandling.Ignore)] [CanBeNull]
    public string q3Solution = null;
    
    [JsonProperty("q4_expressive", NullValueHandling = NullValueHandling.Ignore)]
    public int? q4Expressive = null;
    [JsonProperty("q4_receptive", NullValueHandling = NullValueHandling.Ignore)]
    public int? q4Receptive = null;
    [JsonProperty("q4_expressive_flag", NullValueHandling = NullValueHandling.Ignore)]
    public int? q4ExpressiveFlag = null;
    [JsonProperty("q4_receptive_flag", NullValueHandling = NullValueHandling.Ignore)]
    public int? q4ReceptiveFlag = null;
    [JsonProperty("q4_solution", NullValueHandling = NullValueHandling.Ignore)] [CanBeNull]
    public string q4Solution = null;
    
    [JsonProperty("q5_expressive", NullValueHandling = NullValueHandling.Ignore)]
    public int? q5Expressive = null;
    [JsonProperty("q5_receptive", NullValueHandling = NullValueHandling.Ignore)]
    public int? q5Receptive = null;
    [JsonProperty("q5_expressive_flag", NullValueHandling = NullValueHandling.Ignore)]
    public int? q5ExpressiveFlag = null;
    [JsonProperty("q5_receptive_flag", NullValueHandling = NullValueHandling.Ignore)]
    public int? q5ReceptiveFlag = null;
    [JsonProperty("q5_solution", NullValueHandling = NullValueHandling.Ignore)] [CanBeNull]
    public string q5Solution = null;
    
    [JsonProperty("q6_expressive", NullValueHandling = NullValueHandling.Ignore)]
    public int? q6Expressive = null;
    [JsonProperty("q6_receptive", NullValueHandling = NullValueHandling.Ignore)]
    public int? q6Receptive = null;
    [JsonProperty("q6_expressive_flag", NullValueHandling = NullValueHandling.Ignore)]
    public int? q6ExpressiveFlag = null;
    [JsonProperty("q6_receptive_flag", NullValueHandling = NullValueHandling.Ignore)]
    public int? q6ReceptiveFlag = null;
    [JsonProperty("q6_solution", NullValueHandling = NullValueHandling.Ignore)] [CanBeNull]
    public string q6Solution = null;
    

    public static List<RedCapRecord> convertToRedCapRecord(SerialData inputData)
    {
        List<RedCapRecord> redCapRecords = new List<RedCapRecord>();
        List<RedCapRecord> vocabularyRedCapRecords = new List<RedCapRecord>();
        
        int recordID = String.IsNullOrEmpty(inputData.sRecordId) ? int.MaxValue : int.Parse(inputData.sRecordId);
        string assessorID = inputData.sAssessorID;
        string childID = inputData.sChildID;
        string classroomID = inputData.sClassroomID;
        string teacherID = inputData.sTeacherID;
        
        
        RedCapRecord credentialRedCapRecord = new RedCapRecord();
        credentialRedCapRecord.recordID = recordID;
        credentialRedCapRecord.assessorID = assessorID;
        credentialRedCapRecord.childID = childID;
        credentialRedCapRecord.classroomID = classroomID;
        credentialRedCapRecord.teacherID = teacherID;
        redCapRecords.Add(credentialRedCapRecord);
        
        
        // Populate solution (Vocabulary) data
        for (int responseIndex = 0; inputData.sIndividualResponses != null && 
                                    responseIndex < inputData.sIndividualResponses.Count; responseIndex++)
        {
            RedCapRecord redCapRecord;
            List<string> sessionData = inputData.sIndividualResponses[responseIndex];
            if (vocabularyRedCapRecords.Count - 1 < responseIndex)
                vocabularyRedCapRecords.Add(new RedCapRecord());
            
            redCapRecord = vocabularyRedCapRecords[responseIndex];
            redCapRecord.vocabSession = responseIndex + 1;
            redCapRecord.recordID = recordID;
            redCapRecord.redcapRepeatInstrument = "vocabulary";
            redCapRecord.redcapRepeatInstance = redCapRecord.vocabSession;
            if (sessionData.Count > 0)
                redCapRecord.q1Solution = sessionData[0];
            if (sessionData.Count > 1)
                redCapRecord.q2Solution = sessionData[1];
            if (sessionData.Count > 2)
                redCapRecord.q3Solution = sessionData[2];
            if (sessionData.Count > 3)
                redCapRecord.q4Solution = sessionData[3];
            if (sessionData.Count > 4)
                redCapRecord.q5Solution = sessionData[4];
            if (sessionData.Count > 5)
                redCapRecord.q6Solution = sessionData[5];
        }
        
        // Populate expressive (Vocabulary) data
        for (int responseIndex = 0; inputData.sIndividualExpressiveList != null && 
                                    responseIndex < inputData.sIndividualExpressiveList.Count; responseIndex++)
        {
            RedCapRecord redCapRecord;
            List<bool> sessionData = inputData.sIndividualExpressiveList[responseIndex];
            if (vocabularyRedCapRecords.Count - 1 < responseIndex)
                vocabularyRedCapRecords.Add(new RedCapRecord());
            
            redCapRecord = vocabularyRedCapRecords[responseIndex];
            redCapRecord.vocabSession = responseIndex + 1;
            redCapRecord.recordID = recordID;
            redCapRecord.redcapRepeatInstrument = "vocabulary";
            redCapRecord.redcapRepeatInstance = redCapRecord.vocabSession;
            if (sessionData.Count > 0)
                redCapRecord.q1Expressive = getBinaryTrueFalse(sessionData[0]);
            if (sessionData.Count > 1)
                redCapRecord.q2Expressive = getBinaryTrueFalse(sessionData[1]);
            if (sessionData.Count > 2)
                redCapRecord.q3Expressive = getBinaryTrueFalse(sessionData[2]);
            if (sessionData.Count > 3)
                redCapRecord.q4Expressive = getBinaryTrueFalse(sessionData[3]);
            if (sessionData.Count > 4)
                redCapRecord.q5Expressive = getBinaryTrueFalse(sessionData[4]);
            if (sessionData.Count > 5)
                redCapRecord.q6Expressive = getBinaryTrueFalse(sessionData[5]);
        }
        
        // Populate receptive (Vocabulary) data
        for (int responseIndex = 0; inputData.sIndividualReceptiveList != null && 
                                    responseIndex < inputData.sIndividualReceptiveList.Count; responseIndex++)
        {
            RedCapRecord redCapRecord;
            List<bool> sessionData = inputData.sIndividualReceptiveList[responseIndex];
            if (vocabularyRedCapRecords.Count - 1 < responseIndex)
                vocabularyRedCapRecords.Add(new RedCapRecord());
            
            redCapRecord = vocabularyRedCapRecords[responseIndex];
            redCapRecord.vocabSession = responseIndex + 1;
            redCapRecord.recordID = recordID;
            redCapRecord.redcapRepeatInstrument = "vocabulary";
            redCapRecord.redcapRepeatInstance = redCapRecord.vocabSession;
            if (sessionData.Count > 0)
                redCapRecord.q1Receptive = getBinaryTrueFalse(sessionData[0]);
            if (sessionData.Count > 1)
                redCapRecord.q2Receptive = getBinaryTrueFalse(sessionData[1]);
            if (sessionData.Count > 2)
                redCapRecord.q3Receptive = getBinaryTrueFalse(sessionData[2]);
            if (sessionData.Count > 3)
                redCapRecord.q4Receptive = getBinaryTrueFalse(sessionData[3]);
            if (sessionData.Count > 4)
                redCapRecord.q5Receptive = getBinaryTrueFalse(sessionData[4]);
            if (sessionData.Count > 5)
                redCapRecord.q6Receptive = getBinaryTrueFalse(sessionData[5]);
        }
        
        // Populate expressive flag (Vocabulary) data
        for (int responseIndex = 0; inputData.sIndividualExpressiveFlagList != null && 
                                    responseIndex < inputData.sIndividualExpressiveFlagList.Count; responseIndex++)
        {
            RedCapRecord redCapRecord;
            List<bool> sessionData = inputData.sIndividualExpressiveFlagList[responseIndex];
            if (vocabularyRedCapRecords.Count - 1 < responseIndex)
                vocabularyRedCapRecords.Add(new RedCapRecord());
            
            redCapRecord = vocabularyRedCapRecords[responseIndex];
            redCapRecord.vocabSession = responseIndex + 1;
            redCapRecord.recordID = recordID;
            redCapRecord.redcapRepeatInstrument = "vocabulary";
            redCapRecord.redcapRepeatInstance = redCapRecord.vocabSession;
            if (sessionData.Count > 0)
                redCapRecord.q1ExpressiveFlag = getBinaryTrueFalse(sessionData[0]);
            if (sessionData.Count > 1)
                redCapRecord.q2ExpressiveFlag = getBinaryTrueFalse(sessionData[1]);
            if (sessionData.Count > 2)
                redCapRecord.q3ExpressiveFlag = getBinaryTrueFalse(sessionData[2]);
            if (sessionData.Count > 3)
                redCapRecord.q4ExpressiveFlag = getBinaryTrueFalse(sessionData[3]);
            if (sessionData.Count > 4)
                redCapRecord.q5ExpressiveFlag = getBinaryTrueFalse(sessionData[4]);
            if (sessionData.Count > 5)
                redCapRecord.q6ExpressiveFlag = getBinaryTrueFalse(sessionData[5]);
        }
        
        // Populate receptive flag (Vocabulary) data
        for (int responseIndex = 0; inputData.sIndividualReceptiveFlagList != null && 
                                    responseIndex < inputData.sIndividualReceptiveFlagList.Count; responseIndex++)
        {
            RedCapRecord redCapRecord;
            List<bool> sessionData = inputData.sIndividualReceptiveFlagList[responseIndex];
            if (vocabularyRedCapRecords.Count - 1 < responseIndex)
                vocabularyRedCapRecords.Add(new RedCapRecord());
            
            redCapRecord = vocabularyRedCapRecords[responseIndex];
            redCapRecord.vocabSession = responseIndex + 1;
            redCapRecord.recordID = recordID;
            redCapRecord.redcapRepeatInstrument = "vocabulary";
            redCapRecord.redcapRepeatInstance = redCapRecord.vocabSession;
            if (sessionData.Count > 0)
                redCapRecord.q1ReceptiveFlag = getBinaryTrueFalse(sessionData[0]);
            if (sessionData.Count > 1)
                redCapRecord.q2ReceptiveFlag = getBinaryTrueFalse(sessionData[1]);
            if (sessionData.Count > 2)
                redCapRecord.q3ReceptiveFlag = getBinaryTrueFalse(sessionData[2]);
            if (sessionData.Count > 3)
                redCapRecord.q4ReceptiveFlag = getBinaryTrueFalse(sessionData[3]);
            if (sessionData.Count > 4)
                redCapRecord.q5ReceptiveFlag = getBinaryTrueFalse(sessionData[4]);
            if (sessionData.Count > 5)
                redCapRecord.q6ReceptiveFlag = getBinaryTrueFalse(sessionData[5]);
        }
        
        redCapRecords.AddRange(vocabularyRedCapRecords);
        return redCapRecords;
    }

    public static int getBinaryTrueFalse(bool value)
    {
        return value ? 1 : 0;
    }
}

[Serializable]
public class UsersDetails
{
    public List<RedCapRecord> users = new List<RedCapRecord>();
}
using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
// Represents a sample record in RedCap across all instruments
public class RedCapRecord
{
    [JsonProperty("record_id", NullValueHandling = NullValueHandling.Ignore)]
    public int? recordID = null;
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

    //Letter Name Identification - Student Progress
    [JsonProperty("lni_a", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_A = null;
    [JsonProperty("lni_b", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_B = null;
    [JsonProperty("lni_c", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_C = null;
    [JsonProperty("lni_d", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_D = null;
    [JsonProperty("lni_e", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_E = null;
    [JsonProperty("lni_f", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_F = null;
    [JsonProperty("lni_g", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_G = null;
    [JsonProperty("lni_h", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_H = null;
    [JsonProperty("lni_i", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_I = null;
    [JsonProperty("lni_j", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_J = null;
    [JsonProperty("lni_k", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_K = null;
    [JsonProperty("lni_l", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_L = null;
    [JsonProperty("lni_m", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_M = null;
    [JsonProperty("lni_n", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_N = null;
    [JsonProperty("lni_o", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_O = null;
    [JsonProperty("lni_p", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_P = null;
    [JsonProperty("lni_q", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_Q = null;
    [JsonProperty("lni_r", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_R = null;
    [JsonProperty("lni_s", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_S = null;
    [JsonProperty("lni_t", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_T = null;
    [JsonProperty("lni_u", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_U = null;
    [JsonProperty("lni_v", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_V = null;
    [JsonProperty("lni_w", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_W = null;
    [JsonProperty("lni_x", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_X = null;
    [JsonProperty("lni_y", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_Y = null;
    [JsonProperty("lni_z", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LNI_Z = null;

    //Fields for Letter Name Recognition - Results instrument
    [JsonProperty("lnir_session_no", NullValueHandling = NullValueHandling.Ignore)]
    public int? lnirSessionNumber = null;
    [JsonProperty("lnir_a", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_A = null;
    [JsonProperty("lnir_b", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_B = null;
    [JsonProperty("lnir_c", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_C = null;
    [JsonProperty("lnir_d", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_D = null;
    [JsonProperty("lnir_e", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_E = null;
    [JsonProperty("lnir_f", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_F = null;
    [JsonProperty("lnir_g", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_G = null;
    [JsonProperty("lnir_h", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_H = null;
    [JsonProperty("lnir_i", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_I = null;
    [JsonProperty("lnir_j", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_J = null;
    [JsonProperty("lnir_k", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_K = null;
    [JsonProperty("lnir_l", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_L = null;
    [JsonProperty("lnir_m", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_M = null;
    [JsonProperty("lnir_n", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_N = null;
    [JsonProperty("lnir_o", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_O = null;
    [JsonProperty("lnir_p", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_P = null;
    [JsonProperty("lnir_q", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_Q = null;
    [JsonProperty("lnir_r", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_R = null;
    [JsonProperty("lnir_s", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_S = null;
    [JsonProperty("lnir_t", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_T = null;
    [JsonProperty("lnir_u", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_U = null;
    [JsonProperty("lnir_v", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_V = null;
    [JsonProperty("lnir_w", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_W = null;
    [JsonProperty("lnir_x", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_X = null;
    [JsonProperty("lnir_y", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_Y = null;
    [JsonProperty("lnir_z", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLNI_Z = null;
    

    //Letter Name Identification - Student Progress
    [JsonProperty("lsi_a", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_A = null;
    [JsonProperty("lsi_b", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_B = null;
    [JsonProperty("lsi_c", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_C = null;
    [JsonProperty("lsi_d", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_D = null;
    [JsonProperty("lsi_e", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_E = null;
    [JsonProperty("lsi_f", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_F = null;
    [JsonProperty("lsi_g", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_G = null;
    [JsonProperty("lsi_h", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_H = null;
    [JsonProperty("lsi_i", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_I = null;
    [JsonProperty("lsi_j", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_J = null;
    [JsonProperty("lsi_k", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_K = null;
    [JsonProperty("lsi_l", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_L = null;
    [JsonProperty("lsi_m", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_M = null;
    [JsonProperty("lsi_n", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_N = null;
    [JsonProperty("lsi_o", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_O = null;
    [JsonProperty("lsi_p", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_P = null;
    [JsonProperty("lsi_q", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_Q = null;
    [JsonProperty("lsi_r", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_R = null;
    [JsonProperty("lsi_s", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_S = null;
    [JsonProperty("lsi_t", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_T = null;
    [JsonProperty("lsi_u", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_U = null;
    [JsonProperty("lsi_v", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_V = null;
    [JsonProperty("lsi_w", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_W = null;
    [JsonProperty("lsi_x", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_X = null;
    [JsonProperty("lsi_y", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_Y = null;
    [JsonProperty("lsi_z", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? LSI_Z = null;

    //Fields for Letter Name Recognition - Results instrument
    [JsonProperty("lsir_session_no", NullValueHandling = NullValueHandling.Ignore)]
    public int? lsirSessionNumber = null;
    [JsonProperty("lsir_a", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_A = null;
    [JsonProperty("lsir_b", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_B = null;
    [JsonProperty("lsir_c", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_C = null;
    [JsonProperty("lsir_d", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_D = null;
    [JsonProperty("lsir_e", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_E = null;
    [JsonProperty("lsir_f", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_F = null;
    [JsonProperty("lsir_g", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_G = null;
    [JsonProperty("lsir_h", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_H = null;
    [JsonProperty("lsir_i", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_I = null;
    [JsonProperty("lsir_j", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_J = null;
    [JsonProperty("lsir_k", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_K = null;
    [JsonProperty("lsir_l", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_L = null;
    [JsonProperty("lsir_m", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_M = null;
    [JsonProperty("lsir_n", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_N = null;
    [JsonProperty("lsir_o", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_O = null;
    [JsonProperty("lsir_p", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_P = null;
    [JsonProperty("lsir_q", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_Q = null;
    [JsonProperty("lsir_r", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_R = null;
    [JsonProperty("lsir_s", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_S = null;
    [JsonProperty("lsir_t", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_T = null;
    [JsonProperty("lsir_u", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_U = null;
    [JsonProperty("lsir_v", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_V = null;
    [JsonProperty("lsir_w", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_W = null;
    [JsonProperty("lsir_x", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_X = null;
    [JsonProperty("lsir_y", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_Y = null;
    [JsonProperty("lsir_z", NullValueHandling = NullValueHandling.Ignore)]
    [CanBeNull]
    public int? rLSI_Z = null;


    //Seems like the idea here is to create a big list of RedcapRecords
    //that match the db field names so they can be serialized later
    public static List<RedCapRecord> convertToRedCapRecord(SerialData inputData)
    {
        List<RedCapRecord> redCapRecords = new List<RedCapRecord>();
        List<RedCapRecord> vocabularyRedCapRecords = new List<RedCapRecord>();
        
        int recordID = String.IsNullOrEmpty(inputData.sRecordId) ? int.MaxValue : int.Parse(inputData.sRecordId);
        string assessorID = inputData.sAssessorID;
        string childID = inputData.sChildID;
        string classroomID = inputData.sClassroomID;
        string teacherID = inputData.sTeacherID;
        
        // Populate fields for Credentials Instrument in RedCap
        RedCapRecord credentialRedCapRecord = new RedCapRecord();
        credentialRedCapRecord.recordID = recordID;
        credentialRedCapRecord.assessorID = assessorID;
        credentialRedCapRecord.childID = childID;
        credentialRedCapRecord.classroomID = classroomID;
        credentialRedCapRecord.teacherID = teacherID;
        redCapRecords.Add(credentialRedCapRecord);
        
        
        // Populate fields for Vocabulary Instrument (field-solution) in RedCap
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
        
        // Populate fields for Vocabulary Instrument (field-expressive) in RedCap
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
        
        // Populate fields for Vocabulary Instrument (field-receptive) in RedCap
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
        
        // Populate fields for Vocabulary Instrument (field-expressive flag) in RedCap
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
        
        // Populate fields for Vocabulary Instrument (field-receptive flag) in RedCap
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

        //Create Letter Name Identification - Student Progress records
        RedCapRecord lniSpRecord = new();

        lniSpRecord.recordID = recordID;
        lniSpRecord.redcapRepeatInstrument = "letter_name_identification_student_progress";
        lniSpRecord.redcapRepeatInstance = 1;
        //go through each element of sLearnedLetterNames and map to flag
        //handle null case for data w/o letternames
        if(inputData.sLearnedLetterNamesLNI is null)
        {
            inputData.sLearnedLetterNamesLNI = new bool[26];
        }
        lniSpRecord.LNI_A = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[0]);
        lniSpRecord.LNI_B = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[1]);
        lniSpRecord.LNI_C = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[2]);
        lniSpRecord.LNI_D = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[3]);
        lniSpRecord.LNI_E = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[4]);
        lniSpRecord.LNI_F = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[5]);
        lniSpRecord.LNI_G = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[6]);
        lniSpRecord.LNI_H = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[7]);
        lniSpRecord.LNI_I = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[8]);
        lniSpRecord.LNI_J = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[9]);
        lniSpRecord.LNI_K = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[10]);
        lniSpRecord.LNI_L = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[11]);
        lniSpRecord.LNI_M = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[12]);
        lniSpRecord.LNI_N = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[13]);
        lniSpRecord.LNI_O = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[14]);
        lniSpRecord.LNI_P = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[15]);
        lniSpRecord.LNI_Q = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[16]);
        lniSpRecord.LNI_R = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[17]);
        lniSpRecord.LNI_S = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[18]);
        lniSpRecord.LNI_T = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[19]);
        lniSpRecord.LNI_U = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[20]);
        lniSpRecord.LNI_V = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[21]);
        lniSpRecord.LNI_W = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[22]);
        lniSpRecord.LNI_X = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[23]);
        lniSpRecord.LNI_Y = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[24]);
        lniSpRecord.LNI_Z = getBinaryTrueFalse(inputData.sLearnedLetterNamesLNI[25]);

        redCapRecords.Add(lniSpRecord);

        //Create Letter Name Recognition - Results
        // Populate fields for Vocabulary Instrument (field-receptive flag) in RedCap
        List<RedCapRecord> lniRedCapRecords = new();
        for (int lnrRIndex = 0; inputData.sIndividual_LNI != null &&
                                    lnrRIndex < inputData.sIndividual_LNI.GetLength(1); lnrRIndex++)
        {
            //Headers, credentials, etc.
            RedCapRecord lniRRecord;
            AdaptiveResponse[,] lniRSD = inputData.sIndividual_LNI;
            if (lniRedCapRecords.Count - 1 < lnrRIndex)
                lniRedCapRecords.Add(new RedCapRecord());

            lniRRecord = lniRedCapRecords[lnrRIndex];
            lniRRecord.lnirSessionNumber = lnrRIndex + 1;
            lniRRecord.recordID = recordID;
            lniRRecord.redcapRepeatInstrument = "letter_name_identification_results";
            lniRRecord.redcapRepeatInstance = lniRRecord.lnirSessionNumber;

            //Actual data values
            //Not using nullproofing on sessiondata
            //Since AdaptiveResponse(enum) is non-nullable
            //And the 0 value is significant in grading
            lniRRecord.rLNI_A = (int)lniRSD[0, lnrRIndex];
            lniRRecord.rLNI_B = (int)lniRSD[1, lnrRIndex];
            lniRRecord.rLNI_C = (int)lniRSD[2, lnrRIndex];
            lniRRecord.rLNI_D = (int)lniRSD[3, lnrRIndex];
            lniRRecord.rLNI_E = (int)lniRSD[4, lnrRIndex];
            lniRRecord.rLNI_F = (int)lniRSD[5, lnrRIndex];
            lniRRecord.rLNI_G = (int)lniRSD[6, lnrRIndex];
            lniRRecord.rLNI_H = (int)lniRSD[7, lnrRIndex];
            lniRRecord.rLNI_I = (int)lniRSD[8, lnrRIndex];
            lniRRecord.rLNI_J = (int)lniRSD[9, lnrRIndex];
            lniRRecord.rLNI_K = (int)lniRSD[10, lnrRIndex];
            lniRRecord.rLNI_L = (int)lniRSD[11, lnrRIndex];
            lniRRecord.rLNI_M = (int)lniRSD[12, lnrRIndex];
            lniRRecord.rLNI_N = (int)lniRSD[13, lnrRIndex];
            lniRRecord.rLNI_O = (int)lniRSD[14, lnrRIndex];
            lniRRecord.rLNI_P = (int)lniRSD[15, lnrRIndex];
            lniRRecord.rLNI_Q = (int)lniRSD[16, lnrRIndex];
            lniRRecord.rLNI_R = (int)lniRSD[17, lnrRIndex];
            lniRRecord.rLNI_S = (int)lniRSD[18, lnrRIndex];
            lniRRecord.rLNI_T = (int)lniRSD[19, lnrRIndex];
            lniRRecord.rLNI_U = (int)lniRSD[20, lnrRIndex];//PROBLEM HERE OR BELOW
            lniRRecord.rLNI_V = (int)lniRSD[21, lnrRIndex];
            lniRRecord.rLNI_W = (int)lniRSD[22, lnrRIndex];
            lniRRecord.rLNI_X = (int)lniRSD[23, lnrRIndex];
            lniRRecord.rLNI_Y = (int)lniRSD[24, lnrRIndex];
            lniRRecord.rLNI_Z = (int)lniRSD[25, lnrRIndex];
        }
        redCapRecords.AddRange(lniRedCapRecords);


        // ------
        //Create Sound Name Identification - Student Progress records
        RedCapRecord lsiSpRecord = new();

        lsiSpRecord.recordID = recordID;
        lsiSpRecord.redcapRepeatInstrument = "letter_sound_identification_student_progress";
        lsiSpRecord.redcapRepeatInstance = 1;
        //go through each element of sLearnedLetterNames and map to flag
        //handle null case for data w/o letternames
        if(inputData.sLearnedLetterNamesLSI is null)
        {
            inputData.sLearnedLetterNamesLSI = new bool[26];
        }
        lsiSpRecord.LSI_A = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[0]);
        lsiSpRecord.LSI_B = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[1]);
        lsiSpRecord.LSI_C = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[2]);
        lsiSpRecord.LSI_D = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[3]);
        lsiSpRecord.LSI_E = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[4]);
        lsiSpRecord.LSI_F = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[5]);
        lsiSpRecord.LSI_G = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[6]);
        lsiSpRecord.LSI_H = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[7]);
        lsiSpRecord.LSI_I = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[8]);
        lsiSpRecord.LSI_J = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[9]);
        lsiSpRecord.LSI_K = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[10]);
        lsiSpRecord.LSI_L = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[11]);
        lsiSpRecord.LSI_M = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[12]);
        lsiSpRecord.LSI_N = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[13]);
        lsiSpRecord.LSI_O = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[14]);
        lsiSpRecord.LSI_P = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[15]);
        lsiSpRecord.LSI_Q = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[16]);
        lsiSpRecord.LSI_R = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[17]);
        lsiSpRecord.LSI_S = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[18]);
        lsiSpRecord.LSI_T = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[19]);
        lsiSpRecord.LSI_U = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[20]);
        lsiSpRecord.LSI_V = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[21]);
        lsiSpRecord.LSI_W = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[22]);
        lsiSpRecord.LSI_X = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[23]);
        lsiSpRecord.LSI_Y = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[24]);
        lsiSpRecord.LSI_Z = getBinaryTrueFalse(inputData.sLearnedLetterNamesLSI[25]);

        redCapRecords.Add(lsiSpRecord);

        //Create Letter Name Recognition - Results
        // Populate fields for Vocabulary Instrument (field-receptive flag) in RedCap
        List<RedCapRecord> lsiRedCapRecords = new();
        for (int lsrRIndex = 0; inputData.sIndividual_LSI != null &&
                                lsrRIndex < inputData.sIndividual_LSI.GetLength(1); lsrRIndex++)
        {
            //Headers, credentials, etc.
            RedCapRecord lsiRRecord;
            AdaptiveResponse[,] lsiRSD = inputData.sIndividual_LSI;
            if (lsiRedCapRecords.Count - 1 < lsrRIndex)
                lsiRedCapRecords.Add(new RedCapRecord());

            lsiRRecord = lsiRedCapRecords[lsrRIndex];
            lsiRRecord.lsirSessionNumber = lsrRIndex + 1;
            lsiRRecord.recordID = recordID;
            lsiRRecord.redcapRepeatInstrument = "letter_sound_identification_results";
            lsiRRecord.redcapRepeatInstance = lsiRRecord.lsirSessionNumber;

            //Actual data values
            //Not using nullproofing on sessiondata
            //Since AdaptiveResponse(enum) is non-nullable
            //And the 0 value is significant in grading
            lsiRRecord.rLSI_A = (int)lsiRSD[0, lsrRIndex];
            lsiRRecord.rLSI_B = (int)lsiRSD[1, lsrRIndex];
            lsiRRecord.rLSI_C = (int)lsiRSD[2, lsrRIndex];
            lsiRRecord.rLSI_D = (int)lsiRSD[3, lsrRIndex];
            lsiRRecord.rLSI_E = (int)lsiRSD[4, lsrRIndex];
            lsiRRecord.rLSI_F = (int)lsiRSD[5, lsrRIndex];
            lsiRRecord.rLSI_G = (int)lsiRSD[6, lsrRIndex];
            lsiRRecord.rLSI_H = (int)lsiRSD[7, lsrRIndex];
            lsiRRecord.rLSI_I = (int)lsiRSD[8, lsrRIndex];
            lsiRRecord.rLSI_J = (int)lsiRSD[9, lsrRIndex];
            lsiRRecord.rLSI_K = (int)lsiRSD[10, lsrRIndex];
            lsiRRecord.rLSI_L = (int)lsiRSD[11, lsrRIndex];
            lsiRRecord.rLSI_M = (int)lsiRSD[12, lsrRIndex];
            lsiRRecord.rLSI_N = (int)lsiRSD[13, lsrRIndex];
            lsiRRecord.rLSI_O = (int)lsiRSD[14, lsrRIndex];
            lsiRRecord.rLSI_P = (int)lsiRSD[15, lsrRIndex];
            lsiRRecord.rLSI_Q = (int)lsiRSD[16, lsrRIndex];
            lsiRRecord.rLSI_R = (int)lsiRSD[17, lsrRIndex];
            lsiRRecord.rLSI_S = (int)lsiRSD[18, lsrRIndex];
            lsiRRecord.rLSI_T = (int)lsiRSD[19, lsrRIndex];
            lsiRRecord.rLSI_U = (int)lsiRSD[20, lsrRIndex];//PROBLEM HERE OR BELOW
            lsiRRecord.rLSI_V = (int)lsiRSD[21, lsrRIndex];
            lsiRRecord.rLSI_W = (int)lsiRSD[22, lsrRIndex];
            lsiRRecord.rLSI_X = (int)lsiRSD[23, lsrRIndex];
            lsiRRecord.rLSI_Y = (int)lsiRSD[24, lsrRIndex];
            lsiRRecord.rLSI_Z = (int)lsiRSD[25, lsrRIndex];
        }
        redCapRecords.AddRange(lsiRedCapRecords);
        
        
        return redCapRecords;
    }

    // Function responsible for returning int truth value
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
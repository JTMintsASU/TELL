
// This class is responsible for storing all values that would be required in a RedCap API call. 
public class RedCapRequest
{
    public string token { get; set; }
    public string content { get; set; }
    public string action { get; set; }
    public string format { get; set; }
    public string type { get; set; }
    public string overwriteBehavior { get; set; }
    public string forceAutoNumber { get; set; }
    public string data { get; set; }
    public string returnContent { get; set; }
    public string returnFormat { get; set; }
    public string fields { get; set; }
    public string form_0 { get; set; }
    public string filterLogic { get; set; }
    public string fields_0 { get; set; }
}
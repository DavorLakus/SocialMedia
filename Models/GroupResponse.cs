using System;
using System.Text.RegularExpressions;

public class GroupResponse
{
    public Group data { get; set; }
}

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Keyword> Keywords { get; set; }
}

public class Keyword
{
    public int Id { get; set; }
    private string _name;

    public string Name
    {
        get { return _name; }
        set { _name = RemoveGuid(value); }
    }

    // Function to remove the GUID part using regex
    private string RemoveGuid(string input)
    {
        // Define a regex pattern to match the GUID part in brackets
        string pattern = @"\s*\(\b[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}\b\)\s*";

        // Use regex to replace the matched pattern with an empty string
        string result = Regex.Replace(input, pattern, "");

        return result;
    }
}
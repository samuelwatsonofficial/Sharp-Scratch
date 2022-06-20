namespace SrAtCh;

public class Scripting
{
    public static List<String> startupCode = new List<string>();
    public static List<String> physicsCode = new List<string>();
    public static readonly Dictionary<string,string> event_dictionary = new Dictionary<string,string>
    {
        {"whenflagclicked",""},
        {"",""},
    };
    public static readonly Dictionary<string,string> procedures_dictionary = new Dictionary<string,string>
    {
        {"definition",""},
        {"prototype",""},
        {"",""},
        {"",""},
        {"",""},
        {"",""},
        {"",""},
    };
    public static readonly Dictionary<string,string> data_dictionary = new Dictionary<string,string>
    {
        {"setvariableto",""},
        {"",""},
        {"",""},
        {"",""},
        {"",""},
        {"",""},
        {"",""},
    };
    
    public static string LowestInScope()
    {
        
        return "empty";
    }
    public static string CreateStatement(string statement, string function)
    {
        
        return "empty";
    }
    public static void OpCode(string input)
    {
        //the string optype is the part of the string before the character '_' which spesifies what type the opcode is
        string optype = input.Substring(0, input.IndexOf("_")); 
        string optype2 = input.Substring(input.IndexOf("_"), input.Length);
        switch (optype)
        {
            case "event":
                switch (optype2)
                {
                    case "whenflagclicked":
                        //will need to implement this at some point
                        startupCode.Add("example function");
                        break;
                    default:
                        Console.WriteLine($"this event {input} has not been implemented yet");
                        break;
                }
                break;
            case "procedures":
                switch (optype2)
                {
                    case "call":
                        
                        break;
                    default:
                        Console.WriteLine($"this procedure {input} has not been implemented yet");
                        break;
                }
                break;
            case "data":
                switch (optype2)
                {
                    case "setvariableto":
                        
                        break;
                    default:
                        Console.WriteLine($"this data {input} has not been implemented yet");
                        break;
                }
                break;
            default:
                Console.WriteLine($"this behavior {input} has not been implemented yet");
                break;
        }
        
        
    }

    
}
namespace SrAtCh;

using System.Text.Json;
using System.Text.Json.Serialization;
public class JsonClass
{

    //Console.WriteLine("Hello, World!");
    
    //Console.Write(all);;
    
    public class all
    {
        public List<Target> targets {get; set;}
        public List<Monitor> monitors {get; set;}
        public List<Extention> extentions {get; set;}
        public Meta meta { get; set; }
    }

    public class Target
    {
        public class Variables
        {
            public class Variable
            {
                public string name { get; set; }
                public double value { get; set; }
            }
            public List<Variable> variables { get; set; }
        }
    }

    public class Monitor
    {

    }

    public class Extention
    {

    }
    public class Meta
    { 
        public string semver { get; set; }
        public string vm { get; set; }
        public string agent { get; set; }
    }
}
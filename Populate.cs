using System.Diagnostics;
using SrAtCh;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.IO.Compression;
using Svg;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace SrAtCh;

public class Populate
{
    
    private static string temp = File.ReadAllText(@"./extract/project.json");
    private static JsonNode json = JsonObject.Parse(temp);

    public static (List<dynamic>?, List<string>?) PopulateVariables()
    {
        List<dynamic> publicVariables = new List<dynamic>();
        List<string> publicVariableNames = new List<string>();
        bool stageFound = false;
        for (int i = 0; i < json["targets"].AsArray().Count; i++)
        {
            if (stageFound && bool.Parse(json["targets"].AsArray()[i]["isStage"].ToString()))
            {
                continue;
            }

            if (bool.Parse(json["targets"].AsArray()[i]["isStage"].ToString()))
            {
                stageFound = true;
            }

            if (((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToString() != "{}" &&
                ((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[0].ToString() != null)
            {
                for (int x = 0; x < ((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray().Length; x++)
                {
                    bool tryFloat = float.TryParse(((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1]
                        .ToString(),out float tempFloat);
                    if (tryFloat)
                    {
                        publicVariableNames.Add(
                            ((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[0]
                            .ToString().Replace(" ", "_"));
                        publicVariables.Add(tempFloat);
                    }
                    else
                    {
                        bool tryBool = Boolean.TryParse(
                            ((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1]
                            .ToString(),out bool tempBool);
                        if (tryBool)
                        {
                            publicVariableNames.Add(
                                ((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[0]
                                .ToString().Replace(" ", "_"));
                            publicVariables.Add(tempBool);
                        }
                        else
                        {
                            publicVariableNames.Add(
                                ((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[0]
                                .ToString().Replace(" ", "_"));
                            publicVariables.Add(
                                ((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1]
                                .ToString());
                        }
                    }
                    

                    Console.WriteLine("variablenaem: " +
                                      ((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[0]
                                      .ToString());
                }

                return (publicVariables, publicVariableNames);
            }
            return (null, null);    
        }
        //the program should never get here 
        Console.WriteLine("!!!!!!!!!!!EMPTY!!!!!!!!!!!");
        Environment.Exit(0);
        return (null, null);
    }

    public static List<Asset> PopulateAssets()
    {
        List<Asset> assets = new List<Asset>();
        for (int i = 0; i<((json["targets"]).AsArray().Count); i++)
        {
            assets.Add(new Asset(i));
        }
        return assets;
    }


}
using System.Collections;
using System.Drawing.Imaging;
using SrAtCh;
using static SrAtCh.JsonClass;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.IO.Compression;
using Svg;
using System.Drawing;
namespace SrAtCh;

public class Get
{
    private static string temp = File.ReadAllText(@"./extract/project.json");
    private static JsonNode json = JsonObject.Parse(temp);
    

    public static List<dynamic> Getvariables(int i)
    {
        var dynlist= new List<dynamic>();
        
        Console.WriteLine(i.ToString()+" "+bool.Parse(json["targets"].AsArray()[i]["isStage"].ToString()));
        Console.WriteLine(i.ToString()+" "+(((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray().Length == 0).ToString());
        if (((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray().Length != 0 && !bool.Parse(json["targets"].AsArray()[i]["isStage"].ToString()))
        {
            for (int x = 0; x < ((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray().Length; x++)
            {
                try
                {
                    Console.WriteLine("1");
                    Console.WriteLine(float.Parse(
                        ((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1]
                        .ToString()));
                    dynlist
                        .Add(((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[0]
                            .ToString().Replace(" ", "_"));
                    dynlist
                        .Add(((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1]
                            .ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("2");
                    //Console.WriteLine(e);
                    try
                    {
                        Console.WriteLine("3");
                        Console.WriteLine(Boolean.Parse(
                            ((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1]
                            .ToString()));
                        dynlist
                            .Add(((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[0]
                                .ToString().Replace(" ", "_"));
                        dynlist
                            .Add(((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1]
                                .ToString());
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("4");
                        Console.WriteLine(dynlist.Count);
                        dynlist
                            .Add(((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[0]
                                .ToString().Replace(" ", "_"));
                        dynlist
                            .Add(((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1]
                                .ToString());
                    }
                }

                Console.WriteLine("5");
                Console.WriteLine("variablevalue: " +
                                  ((JsonObject)json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1]
                                  .ToString());
            }
            
        }
        return dynlist;
    }

    public static string GetName(int i)
    {
        string name = (json["targets"][i]["name"]).ToString();
        Console.WriteLine("name: "+(json["targets"][i]["name"]).ToString());
        return name;
    }
    
    public static List<Asset.Costume> GetCostumes(int i)
    {
        var costumeList = new List<Asset.Costume>();
        if (json["targets"][i]["costumes"].ToString() != "{}" &&
            (json["targets"][i]["costumes"]).ToString() != null)
        {
            for (int x = 0; x < json["targets"][i]["costumes"].AsArray().Count; x++)
            {
                Console.WriteLine(json["targets"][i]["costumes"][x]["name"].ToString());
                costumeList.Add(new Asset.Costume());
                costumeList[costumeList.Count-1].assetId=json["targets"][i]["costumes"][x]["assetId"].ToString();
                /*
                if (json["targets"][i]["costumes"][x]["dataFormat"].ToString() == "svg")
                {
                    var svgDocument = SvgDocument.Open(@"./extract/"+costumeList[costumeList.Count-1].assetId+".svg");
                    using (var smallBitmap = svgDocument.Draw())
                    {
                        var width = smallBitmap.Width;
                        var height = smallBitmap.Height;
                        if (width != 2000)// I resize my bitmap
                        {
                            width = 2000;
                            height = 2000/smallBitmap.Width*height;
                        }

                        using (var bitmap = svgDocument.Draw(width, height))//I render again
                        {
                            bitmap.Save(costumeList.name+".png", ImageFormat.Png);
                        }
                    }
                }
                */
                costumeList[costumeList.Count-1].name=json["targets"][i]["costumes"][x]["name"].ToString();
                costumeList[costumeList.Count-1].rotationCentreX=float.Parse(json["targets"][i]["costumes"][x]["rotationCenterX"].ToString());
                costumeList[costumeList.Count-1].rotationCentreY=float.Parse(json["targets"][i]["costumes"][x]["rotationCenterX"].ToString());
                Console.WriteLine(costumeList[0].name);
            }
            /*
            for (int i = 0; i < json["targets"].AsArray().Count; i++)
        {
            
        }
        */

        }

        return costumeList;
    }
}
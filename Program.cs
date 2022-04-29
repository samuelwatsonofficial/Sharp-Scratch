// See https://aka.ms/new-console-template for more information

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
List<dynamic> publicVariables = new List<dynamic>();
List<string> publicVariableNames = new List<string>();
List<Asset> assets = new List<Asset>();
//System.IO.Compression.ZipFile.ExtractToDirectory(@"./Scratch Project.sb3",@"./extract");

var origin = File.ReadAllText(@"./extract/project.json");

var json = JsonObject.Parse(origin);
var h = ((JsonObject) json["targets"][0]["variables"]).AsEnumerable().First();
PopulateNames();
PopulateVariables();
PopulateCostumes();
Console.WriteLine(" spacer");






void PopulateVariables()
{
    bool stageFound = false;
    for (int i = 0; i<json["targets"].AsArray().Count;i++)
    {
        if (stageFound && bool.Parse(json["targets"].AsArray()[i]["isStage"].ToString()))
        {
            break;
        }
        if (bool.Parse(json["targets"].AsArray()[i]["isStage"].ToString()))
        {
            stageFound = true;
        }
        if (((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToString() != "{}" && ((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[0].ToString() != null)
        {
            
            for (int x = 0; x < ((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray().Length; x++) 
            {
                try
                {
                            
                    Console.WriteLine(float.Parse(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1].ToString()));
                    publicVariableNames.Add(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[0].ToString().Replace(" ","_"));
                    publicVariables.Add(float.Parse(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1].ToString()));
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);
                    try
                    {
                        Console.WriteLine(Boolean.Parse(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1].ToString()));
                        publicVariableNames.Add(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[0].ToString().Replace(" ","_"));
                        publicVariables.Add(bool.Parse(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1].ToString()));
                    }
                    catch (Exception exception)
                    {
                        //Console.WriteLine(exception);
                        publicVariableNames.Add(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[0].ToString().Replace(" ","_"));
                        publicVariables.Add(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1].ToString());
                    }
                }
                Console.WriteLine(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1].ToString());
            }
            return;
        }
        for (int x = 0; x < ((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray().Length; x++)
        {
            try
            {
                Console.WriteLine(float.Parse(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1].ToString()));
                assets[assets.Count-1].variables.Add(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[0].ToString().Replace(" ","_"));
                assets[assets.Count-1].variables.Add(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1].ToString());
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                try
                {
                    Console.WriteLine(Boolean.Parse(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1].ToString()));
                    assets[assets.Count-1].variables.Add(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[0].ToString().Replace(" ","_"));
                    assets[assets.Count-1].variables.Add(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1].ToString());
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    assets[assets.Count-1].variables.Add(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[0].ToString().Replace(" ","_"));
                    assets[assets.Count-1].variables.Add(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1].ToString());
                }
            }
            Console.WriteLine(((JsonObject) json["targets"][i]["variables"]).AsEnumerable().ToArray()[x].Value[1].ToString());
        }
    }
}


void PopulateNames()
{
    for (int i = 0; i < json["targets"].AsArray().Count; i++)
    {
        assets.Add(new Asset());
        assets[assets.Count-1].name=(json["targets"][i]["name"]).ToString();
        Console.WriteLine((json["targets"][i]["name"]).ToString());
    }
}

void PopulateCostumes()
{
    for (int i = 0; i < json["targets"].AsArray().Count; i++)
    {
        if (json["targets"][i]["costumes"].ToString() != "{}" &&
            (json["targets"][i]["costumes"]).ToString() != null)
        {
            assets[i].costumes = new List<Asset.Costume>();
            for (int x = 0; x < json["targets"][i]["costumes"].AsArray().Count; x++)
            {
                Console.WriteLine(assets[i].name);
                assets[i].costumes.Add(new Asset.Costume());
                assets[i].costumes[assets[i].costumes.Count-1].assetId=json["targets"][i]["costumes"][x]["assetId"].ToString();
                if (json["targets"][i]["costumes"][x]["dataFormat"].ToString() == "svg")
                {
                    var svgDocument = SvgDocument.Open(@"./extract/"+assets[i].costumes[assets[i].costumes.Count-1].assetId+".svg");
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
                            bitmap.Save(assets[i].name+".png", ImageFormat.Png);
                        }
                    }
                }
                assets[i].costumes[assets[i].costumes.Count-1].name=json["targets"][i]["costumes"][x]["name"].ToString();
                assets[i].costumes[assets[i].costumes.Count-1].rotationCentreX=float.Parse(json["targets"][i]["costumes"][x]["rotationCenterX"].ToString());
                assets[i].costumes[assets[i].costumes.Count-1].rotationCentreY=float.Parse(json["targets"][i]["costumes"][x]["rotationCenterX"].ToString());
                Console.WriteLine(assets[i].costumes[0].name);
            }
        }

    }
}
class Asset
{
    public string? name;
    public List<dynamic> variables;
    public List<dynamic>? lists;
    public List<string>? broadcasts;
    public int? currentCostume;
    public List<Costume>? costumes;
        
    public class Costume
    {
        public string? assetId;
        public string? name;
        public float? rotationCentreX;
        public float? rotationCentreY;

    }

    private dynamic? value;
}
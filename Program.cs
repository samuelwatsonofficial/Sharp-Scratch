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

List<String> startupCode = new List<string>();
startupCode.Add(
    @"using Godot;
using System;

public class Sprite : Godot.Sprite
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
");
startupCode.Add(@"
    }
/");

List<String> physicsCode = new List<string>();
physicsCode.Add(
    @"public override void _Process(float delta)
{
      
");
physicsCode.Add(
@"}
}");
/*
Console.WriteLine(File.Exists(@"./extract/project.json"));
if (File.Exists(@"./extract/project.json"))
{
    File.Delete(@"./extract");
    Directory.Delete(@"./extract");
    Console.WriteLine("deleted");
}
*/
//System.IO.Compression.ZipFile.ExtractToDirectory(@"./Scratch Project.sb3",@"./extract");

var origin = File.ReadAllText(@"./extract/project.json");
var json = JsonObject.Parse(origin);
var h = ((JsonObject) json["targets"][0]["variables"]).AsEnumerable().First();
List<Asset> assets = Populate.PopulateAssets();

//PopulateNames();
var temp =Populate.PopulateVariables();
List<dynamic> publicVariables = temp.Item1;
List<string> publicVariableNames = temp.Item2;
Console.WriteLine("variables");
foreach (var each in publicVariables)
{
    Console.WriteLine(each);
}
Console.WriteLine("variables");

Console.WriteLine("names");
foreach (var each in publicVariableNames)
{
    Console.WriteLine(each);
}
Console.WriteLine("names");




Console.WriteLine(" spacer");








void PopulateNames()
{
    for (int i = 0; i < json["targets"].AsArray().Count; i++)
    {
        
        //assets.Add(new Asset());
        
    }
}

public class Asset
{
    public string name;
    public int index;
    public List<dynamic>? variables;
    public List<dynamic>? lists;
    public List<string>? broadcasts;
    public int? currentCostume;
    public List<Costume>? costumes;

    public Asset(int index)
    {
        Console.WriteLine("getting variables...");
        this.variables=Get.Getvariables(index);
        Console.WriteLine("getting names...");
        this.name = Get.GetName(index);
        Console.WriteLine("getting costumes...");
        this.costumes = Get.GetCostumes(index);
    }

    // public Asset()
    // {
    //     variables = Get.GetVariables(0);
    // }
    public class Costume
    {
        public string? assetId;
        public string? name;
        public float? rotationCentreX;
        public float? rotationCentreY;

    }
    private dynamic? value;
}
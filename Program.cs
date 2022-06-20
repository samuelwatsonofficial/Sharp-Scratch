// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Drawing.Imaging;
using SrAtCh;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.IO.Compression;
using Svg;
using System.Drawing;


/*
 extraction code commented out temporarily due to needing root to replace files
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
List<Asset> assets = Populate.PopulateAssets();

var temp =Populate.PopulateVariables();
List<dynamic> publicVariables = temp.Item1;
List<string> publicVariableNames = temp.Item2;
foreach (var each in publicVariables)
{
    Console.WriteLine(each);
}


foreach (var each in publicVariableNames)
{
    Console.WriteLine(each);
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
        this.variables=Get.Variables(index);
        Console.WriteLine("getting name...");
        this.name = Get.Name(index);
        Console.WriteLine("getting costumes...");
        this.costumes = Get.GetCostumes(index);
        this.index = index;
        //this.physicsCode = Scripting
    }
    public class Costume
    {
        public string? assetId;
        public string? name;
        public float? rotationCentreX;
        public float? rotationCentreY;

     }
    
//     //method for adding code to the starting method of godot
//     
//     
//     void AppendPhysics(string method)
//     {
//         physicsCode.Insert(1,method);
//     }
//     public List<String> startupCode = new List<string>(){
//         @"using Godot;
//     using System;
//
//     public class Sprite : Godot.Sprite
//     {
//         // Called when the node enters the scene tree for the first time.
//         public override void _Ready()
//         {
//             
//     ",@"
//         }
//     /"};
//     public List<String> physicsCode = new List<string>(){@"public override void _Process(float delta)
//         {
//               
//         ",@"}
//         }"};

}
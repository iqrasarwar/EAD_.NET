// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
//download entity frame work
//frameworkcore.sql server
//use same version
//framework.tools
//db first
//scaffolding command
//serch for ef core reverse enfineering
//ms offical document
//open pakage manager console.
//entity frame work design
//scaffold-DBContext connection string  -> to create classes and dbcontex
using efCore;
using System.Configuration;


var context = new EFCoreDBContext();
/*var context = new EFCoreDB();*/
Product p = new Product();
p.Id = 2;
p.Name = "Mobile";
//CREaTE
context.Products.Add(p); //saved to context memory
context.SaveChanges(); //saved to db
//ef will create insert query and add data 
//UPDaTE
var produt = context.Products.FirstOrDefault(p => p.Id == 1);
var produt1 = context.Products.First();
produt.Name = "laptop"; //change in memory
context.SaveChanges(); //change in db
//REMOVE
//var produt11 = context.Products.Where(p => p.Id == 1);
//context.Products.Remove(context.Products.Where(p => p.Id == 1));
//context.SaveChanges();
//retrive
        var queryb = context.Products.Where(p => p.Id == 1);
Console.WriteLine(queryb);

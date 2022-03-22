/*
 * LINQ       -> Language Integreted -> Generalized Syntax
Language Integreted Query
ORM Mapper
till now we have studied db query
string queries -> no error at compile time -> error at runtime
integrate query capibility in language - > part of lanuage -> verify at compile time
data source can be XML FILES, LIST, ARRAY LIST , DICTIONARY , COLLECTION CLASSES.... they can be many other than db.
Layer of Data Sources : RDBMS, COLLCETION CLASSES, XML, ... XYZ
Different way to retirve data from different sources. SQL , obj, files etc
make the syntax same for all the data sources -> same way to query -> this facility is provided by LNIQ
to make general syntax(LINQ) compatible with specific data source we have a special layer called LINQ Providers or LINQ Enable Data Sources
this layer performs -> LINQ TO SQL, LINQ TO XML , LINQ TO COLLECTION CLASSESS
Conversion on RunTime but Error on compile time

*/
using System;
using System.Linq;
namespace linqClass1
{
    class linqq
    {
        static void Main()
        {
            string[] cities = { "Lahore", "Islamabad", "okara","kpk" };
            string[] cities2 = { "A", "B", "C" };
            /*
             * 2 WAYS
             * METHOD SYNTAX   -> QUERY SYNTAX
             */
            var query = cities.Where(NameOfLength4);
            foreach (var c in query)
            {
                Console.WriteLine(c);
            }
        }
        static bool NameOfLength4(string cityName)
        {
            return cityName.Length > 4;
        }
    }
}

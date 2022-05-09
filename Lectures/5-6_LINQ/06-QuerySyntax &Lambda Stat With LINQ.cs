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
    class student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Marks { get; set; }

    }

    class linqq
    {
        static void Main()
        {
            // STEP 1 : define data source
            string[] cities = { "Lahore", "Islamabad", "okara","kpk" };
            string[] cities2 = { "A", "B", "C" };
            /*
             * 2 WAYS 
             * METHOD SYNTAX  & QUERY SYNTAX
             */
            /* METHOD SYNTAX  */
            // STEP 2 : define query -> no data exists here
            Console.WriteLine("API/METHOD Syntx");
            var query = cities.Where(NameOfLength4); //nameoflength4 is not called here
            //STEP 3 : query is excuted -> data comes here
            foreach (var c in query) //conditions check function is called here
            {
                Console.WriteLine(c);
            }
            /*
             * You Can Use LAMBDA STATMENT here 
             */
            Console.WriteLine("LAMBDA STATMENT");
            query = cities
                .Where((string cityName) => cityName=="kpk")
                .Select(x=>x);   //to filter the attributes or columns we need to select
            //receive in anonymus type 
            foreach (var c in query) 
            {
                Console.WriteLine(c);
            }
            /*
             * You Can Use LAMBDA EXPRESSION here 
             */
            Console.WriteLine("LAMBDA EXPRESSION");
            query = cities.
                Where((string cityName) => { return cityName == "Lahore"; })
                .Select(x => x);
            foreach (var c in query)
            {
                Console.WriteLine(c);
            }
            /* QUERY SYNTAX */
            Console.WriteLine("QUERY SYNTAX");
            query = from n in cities
                    select n;
            query = from n in cities
                    where n.Length > 4
                    select n;
            foreach (var c in query)
            {
                 Console.WriteLine(c);
            }
            student[] arr = { new student() { Id = 1, Name = "iqra", Marks = 23 }, new student() { Id = 2, Name = "iqra", Marks = 23 }, new student() { Id = 3, Name = "iqra", Marks = 23 } };
            var q1 = (from n in arr
                    where n.Name.Length > 3
                 select new
                {
                    n.Id,
                    n.Marks,
                    n.Name
                }).ToList();
            var q = (from n in arr
                    where n.Name.Length > 3
                    select (new Tuple<int, int, string>(n.Id, n.Marks, n.Name))).ToList();
            Console.WriteLine("OBJS");
            foreach (var i in q1)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("TUPLE");
            foreach (Tuple<int,int,string> i in q)
            {
                Console.WriteLine(i);
            }

        }

        static bool NameOfLength4(string cityName)
        {
            return cityName.Length > 4;
        }
    }
}
/*
 STEPS TO EXECUTE LINQ QUERY:
// STEP 1 : define data source
// STEP 2 : define query -> no data exists here
//STEP 3 : query is excuted -> data comes here
*/

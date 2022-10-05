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
using System.Collections;
namespace linqClass1
{
    /* class student
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
             */
    /*
              * 2 WAYS 
              * METHOD SYNTAX  & QUERY SYNTAX
              */
    /* METHOD SYNTAX  */
    /*
    // STEP 2 : define query -> no data exists here
    Console.WriteLine("API/METHOD Syntx");
    var query = cities.Where(NameOfLength4); //nameoflength4 is not called here
    //STEP 3 : query is excuted -> data comes here
    foreach (var c in query) //conditions check function is called here
    {
        Console.WriteLine(c);
    }
    */
    /*
     * You Can Use LAMBDA STATMENT here 
     */
    /*
    Console.WriteLine("LAMBDA STATMENT");
    query = cities
        .Where((string cityName) => cityName=="kpk")
        .Select(x=>x);   //to filter the attributes or columns we need to select
    //receive in anonymus type 
    foreach (var c in query) 
    {
        Console.WriteLine(c);
    }
    */
    /*
     * You Can Use LAMBDA EXPRESSION here 
     */
    /*
    Console.WriteLine("LAMBDA EXPRESSION");
    query = cities.
        Where((string cityName) => { return cityName == "Lahore"; })
        .Select(x => x);
    foreach (var c in query)
    {
        Console.WriteLine(c);
    }
    */
    /* QUERY SYNTAX *//*
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
}*/

    // Describes a book in the book list:
    public struct Book
    {
        public string Title; // Title of the book.
        public string Author; // Author of the book.
        public decimal Price; // Price of the book.
        public bool Paperback; // Is it paperback?
        public Book(string title, string author, decimal price, bool paperBack)
        {
            Title = title;
            Author = author;
            Price = price;
            Paperback = paperBack;
        }
    }
    // Declare a delegate type for processing a book:
    public delegate void ProcessBookCallback(Book book);
    // Maintains a book database.
    public class BookDB
    {
        // List of all books in the database:
        ArrayList list = new ArrayList();
        // Add a book to the database:
        public void AddBook(string title, string author, decimal price, bool paperBack)
        {
            list.Add(new Book(title, author, price, paperBack));
        }
        // Call a passed-in delegate on each paperback book to process it:
        public void ProcessPaperbackBooks(ProcessBookCallback processBook)
        {
            foreach (Book b in list)
            {
               // if (b.Paperback)
                    // Calling the delegate:
                    processBook(b);
            }
        }
    }

    // Using the Bookstore classes:

    // Class to total and average prices of books:
    class PriceTotaller
    {
        int countBooks = 0;
        decimal priceBooks = 0.0m;
        internal void AddBookToTotal(Book book)
        {
            Console.WriteLine("count is " + countBooks + " now.");
            countBooks += 1;
            priceBooks += book.Price;
        }
        public decimal AveragePrice()
        {
            return priceBooks / countBooks;
        }
    }


    // Class to test the book database:
    class Test
    {
        // Print the title of the book.
        static void PrintTitle(Book b)
        {
            Console.WriteLine($" {b.Title}");
        }
    
        // Execution starts here.
        static void Main()
        {
            BookDB bookDB = new BookDB();
            // Initialize the database with some books:
            AddBooks(bookDB);
            // Print all the titles of paperbacks:
            Console.WriteLine("Paperback Book Titles:");
            // Create a new delegate object associated with the static
            // method Test.PrintTitle:
            bookDB.ProcessPaperbackBooks(PrintTitle);
            // Get the average price of a paperback by using
            // a PriceTotaller object:
            PriceTotaller totaller = new PriceTotaller();
            // Create a new delegate object associated with the nonstatic
            // method AddBookToTotal on the object totaller:
            bookDB.ProcessPaperbackBooks(totaller.AddBookToTotal);
            Console.WriteLine("Average Paperback Book Price: ${0:#.##}",
            totaller.AveragePrice());
        }
        // Initialize the book database with some test books:
        static void AddBooks(BookDB bookDB)
        {
            bookDB.AddBook("The C Programming Language", "Brian W. Kernighan and Dennis M. Ritchie", 19.95m,
            true);
            bookDB.AddBook("The Unicode Standard 2.0", "The Unicode Consortium", 39.95m, true);
            bookDB.AddBook("The MS-DOS Encyclopedia", "Ray Duncan", 129.95m, false);
            bookDB.AddBook("Dogbert's Clues for the Clueless", "Scott Adams", 12.00m, true);
        }
    }
}
/*
 STEPS TO EXECUTE LINQ QUERY:
// STEP 1 : define data source
// STEP 2 : define query -> no data exists here
//STEP 3 : query is excuted -> data comes here
*/

//  ***********************************
// CONCEPT OF aSP.NET


//envoirment setup
var builder = WebApplication.CreateBuilder(args);
//obj of web app
var app = builder.Build();

//async prog is used
app.Use(async (context, next) =>
{
    //request and response both should exist here
    //and helper to move forward in the context
    //next is used for this purpose.
    //we receive response and request through get.
    context.Response.ContentType = "text/plain";
    await context.Response.WriteAsync("pakistan\n"); // to generate response
    await next(); // to move to next
});


app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("welcome to clss\n"); // to generate response
    await next(); // to move to next
});
//default middle ware is here
app.MapGet("/", () => "Hello World!");
//"/" base url
//return hello world
app.Run();


// Additional libraries
using Microsoft.EntityFrameworkCore;
using log_regPranctice.Models;  //name of your project_FolderName.Models;     always otherwise is not going to work

// the variable Builder is an  instance of the WebApplication class
// Creates builder (also part of boilerplate code for web apps)
var builder = WebApplication.CreateBuilder(args);
//  Creates the db connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Adds database connection - must be before app.Build();
builder.Services.AddDbContext<DB>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// we are adding a so-called "service" into our application's Service Container. This makes the specified service available to the rest of your application in a number of ways
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();  // AddHttpContextAccessor gives our views direct access to session so that session data doesn't need to be repeatedly passed into the ViewBag.
builder.Services.AddSession();  // add this line before calling the builder.Build() method
// build is  
var app = builder.Build();
// we need to use  this  after we set up the app variable to be Builder to implement MVC on its entirety
app.UseSession();   // to use session
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
// "Pattern" is what allows us to specify methods of our route in our Controller files
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();

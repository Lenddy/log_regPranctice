/* 
Disabled Warning: "Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable."
We can disable this safely because we know the framework will assign non-null values when it constructs this class for us.
*/
#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
// the MyContext class representing a session with our MySQL database, allowing us to query for or save data
namespace log_regPranctice.Models; //same as the name space that you give in the program file
// this change from MyContext to DB
public class DB: DbContext{

    public DB(DbContextOptions options):base(options){}
     // the "Monsters" table name will come from the DbSet property name
/*name of you table goes in between the allegator mouths <name of table>
this is bacicaly adding a propertie in to you DB class*/
    public DbSet<Users> user {get; set;}
//if you have 2  tables you must add//   public DbSet<Monster> Monster {get; set;}     and also create 2 models
}
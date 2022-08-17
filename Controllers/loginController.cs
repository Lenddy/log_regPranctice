using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using log_regPranctice.Models;
using Microsoft.AspNetCore.Identity;

namespace log_regPranctice.Controllers;

public class login : Controller
{
    private DB db;

    public login(DB context){
        db = context;
    } 

    private int? uid{
        get{
            return HttpContext.Session.GetInt32("uid");
        }
    }

    private bool loggein{
        get{
            return uid != null;
        }
    }

    [HttpGet("/")]
    public IActionResult index(){
        return View("index");
    }
    [HttpPost("/register")]
    public IActionResult register(Users newuser){
        // checking validations
        if(ModelState.IsValid){
            // checking that there is no one with the same email
            if(db.user .Any(u => u.Email == newuser.Email)){
                // adding a new validation in case  some one has the same email 
                ModelState.AddModelError("Email","email is already taken ");
            }
        }
        // cheking again  if the user pass the validations
        if(ModelState.IsValid == false){
            //redirecting to the same page if the user does not pass the validations
            return index();
        }
        
        // hashing the pass word
        PasswordHasher<Users> hash  = new PasswordHasher<Users>();
        newuser.Password = hash.HashPassword(newuser,newuser.Password);
        db.user.Add(newuser);
        db.SaveChanges();
        HttpContext.Session.SetInt32("uid",newuser.UserId);
        HttpContext.Session.SetString("uname", newuser.fullName());
        return RedirectToAction("home");
        }

        [HttpPost("/login")]
        public IActionResult loginUser(Login login){
            if(ModelState.IsValid == false){
                return index();
            }
        
        Users? someUser = db.user.FirstOrDefault(u=>u.Email == login.LoginEmail);
        if(someUser == null){
            ModelState.AddModelError("LoginEmail","email/password does not match");
            return index();
        }
        // checking for the password 
        PasswordHasher<Login> hash  = new PasswordHasher<Login>();
        PasswordVerificationResult checkedHash = hash.VerifyHashedPassword(login,someUser.Password,login.LoginPassword);
        if (checkedHash == 0){
            ModelState.AddModelError("LoginPassword","email/password does not match");
        }
        // if there are no errors 
        HttpContext.Session.SetInt32("uid",someUser.UserId);
        HttpContext.Session.SetString("uname", someUser.fullName());
        return RedirectToAction("home");
        }


    [HttpGet("/home")]
    public IActionResult home(){
        if(!loggein){
            return RedirectToAction("index");
        }
        return View("home");
    }

    [HttpPost("/logout")]
    public IActionResult logout(){
        HttpContext.Session.Clear();
        return Redirect("/");
    }
}

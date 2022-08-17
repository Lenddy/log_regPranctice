#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//your namespace
namespace log_regPranctice.Models;    //must be the same that is on you program file 
//classname
[NotMapped]
public class Login
{
    [Required]
    [EmailAddress]
    [Display(Name= "Email")]
    public string LoginEmail { get; set; }
//change the field as needed
    [Required]
    [Display(Name= "Password")]
    [DataType(DataType.Password)]
    public string LoginPassword { get; set; } 
}
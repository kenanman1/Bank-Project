using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
/// <summary>
/// Represents User class
/// </summary>
[Index("Email", IsUnique = true, Name = "Email_Index")]
public class User : Users
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public BankAccount BankAccount { get; set; }
    public List<BankAmountHistory> amountHistory { get; set; }

}
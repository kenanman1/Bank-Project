using System.ComponentModel.DataAnnotations;

public class BankAccount
{
    public User User { get; set; }
    [Key]
    public int UserId { get; set; }
    public double Amount { get; set; } = 0;
    public int Percent { get; protected set; } = 5;
}
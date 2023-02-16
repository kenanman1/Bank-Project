using System.ComponentModel.DataAnnotations;

public class BankAccountVIP
{
    public VIP_User VIP_User { get; set; }
    [Key]
    public int VIP_UserId { get; set; }
    public double Amount { get; set; } = 100;
    public int Percent { get; protected set; } = 10;
}
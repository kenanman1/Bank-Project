public class BankAmountHistory 
{
    public int Id { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public double Amount { get; set; }
    public DateTime Time { get; set; }
    public string AmountType { get; set; }
}
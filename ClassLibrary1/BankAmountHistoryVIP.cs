public class BankAmountHistoryVIP
{
    public int Id { get; set; }
    public VIP_User User { get; set; }
    public int VIP_UserId { get; set; }
    public double Amount { get; set; }
    public DateTime Time { get; set; }
    public string AmountType { get; set; }
}
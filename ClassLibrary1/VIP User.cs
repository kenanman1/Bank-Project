using Microsoft.EntityFrameworkCore;
/// <summary>
/// Represents VIP User class
/// </summary>
[Index("VIP_Number", IsUnique = true, Name = "VIP_Index")]
public class VIP_User : Users
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string VIP_Number { get; set; }
    public BankAccountVIP AccountVIP { get; set; }
    public List<BankAmountHistoryVIP> amountHistoryVIP { get; set; }
}
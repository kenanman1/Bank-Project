using ClassLibrary1;
using DB;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Text.Json;

/// <summary>
/// Provides work with a database such as creating, searching, etc.
/// </summary>
public class DataBase
{
    #region Properties
    public enum UserType { Simple, VIP };
    private UserCreator Creator;
    #endregion
    public void CreateUser(UserType? userType, string name, string surname, string mailOrCode, string password)
    {
        using AppContextDB context = new();

        if (userType == UserType.Simple)
        {
            MailAddress? mailAddress = null;
            MailAddress.TryCreate(mailOrCode, out mailAddress);
            var find = context.Users.FirstOrDefault(p => p.Email == mailAddress.Address);
            if (find == null)
            {
                Creator = new NoVIPUser(name, surname, mailOrCode, password);
                Users users = Creator.GetUser();
                context.Add(users);
                context.SaveChanges();

                CreateAccount(userType, (users as User).Id);

                MessageBox.Show("User was created", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("User exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else
        {
            var find = context.UsersVIP.FirstOrDefault(p => p.VIP_Number == mailOrCode);
            if (find == null)
            {
                Creator = new VIPUserCreator(name, surname, mailOrCode, password);
                Users users = Creator.GetUser();
                context.Add(users);
                context.SaveChanges();

                CreateAccount(userType, (users as VIP_User).Id);

                MessageBox.Show("User was created", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("User exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
    private void CreateAccount(UserType? type, int id)
    {
        using AppContextDB contextDB = new();
        if (type == UserType.Simple)
        {
            BankAccount account = new BankAccount() { UserId = id };
            contextDB.Accounts.Add(account);
        }
        else
        {
            BankAccountVIP accountVIP = new BankAccountVIP() { VIP_UserId = id };
            contextDB.VIPAccounts.Add(accountVIP);
        }
        contextDB.SaveChanges();
    }
    public bool CheckUser(UserType type, string mailOrCode, string password)
    {
        bool check = true;
        using AppContextDB contextDB = new AppContextDB();
        contextDB.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        switch (type)
        {
            case UserType.Simple:
                var find = contextDB.Users.FirstOrDefault(p => p.Email == mailOrCode && p.Password == password);
                if (find == null)
                    check = false;
                break;
            case UserType.VIP:
                var findVIP = contextDB.UsersVIP.FirstOrDefault(p => p.VIP_Number == mailOrCode && p.Password == password);
                if (findVIP == null)
                    check = false;
                break;
            default:
                break;
        }
        return check;
    }
    public Users FindUser(UserType userType, string mail)
    {
        using AppContextDB contextDB = new AppContextDB();
        contextDB.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        if (userType == UserType.Simple)
        {
            var user = contextDB.Users.Include(p => p.BankAccount).FirstOrDefault(p => p.Email == mail);
            User user1 = new User
            {
                Email = user.Email,
                Id = user.Id,
                BankAccount = user.BankAccount,
                Name = user.Name,
                Surname = user.Surname
            };
            return user1;
        }
        else
        {
            var user = contextDB.UsersVIP.Include(p => p.AccountVIP).FirstOrDefault(p => p.VIP_Number == mail);
            VIP_User vIP_User = new VIP_User()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                VIP_Number = user.VIP_Number,
                AccountVIP = user.AccountVIP
            };
            return vIP_User;
        }
    }
    public List<string> GetAmountHistories(UserType userType)
    {
        using AppContextDB contextDB = new AppContextDB();
        contextDB.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        if (userType == UserType.Simple)
        {
            var userId = contextDB.Users.FirstOrDefault(p => p.Email == Settings1.Default.MailOrCode);
            var list = contextDB.amountHistory.Where(p => p.UserId == userId.Id).ToList();
            List<string> result = new List<string>();
            foreach (var item in list)
            {
                result.Add($"Amount - {item.Amount}; Type - {item.AmountType}; Time - {item.Time}");
            }
            return result;
        }
        else
        {
            var userId = contextDB.UsersVIP.FirstOrDefault(p => p.VIP_Number == Settings1.Default.MailOrCode);
            var list = contextDB.amountHistoryVIPs.Where(p => p.VIP_UserId == userId.Id).ToList();
            List<string> result = new List<string>();

            foreach (var item in list)
            {
                result.Add($"Amount - {item.Amount}; Type - {item.AmountType}; Time - {item.Time}");

            }
            return result;
        }
    }
    protected internal void MakeDeposit(UserType type, double sum)
    {
        using AppContextDB contextDB = new AppContextDB();
        sum = Math.Round(sum, 2);
        if (type == UserType.Simple)
        {
            var user = contextDB.Users.Include(p => p.BankAccount).FirstOrDefault(p => p.Email == Settings1.Default.MailOrCode);
            user.BankAccount.Amount += sum;
        }
        else
        {
            var user = contextDB.UsersVIP.Include(p => p.AccountVIP).FirstOrDefault(p => p.VIP_Number == Settings1.Default.MailOrCode);
            double total = (sum / 100) * 1;
            sum += total;
            user.AccountVIP.Amount += sum;
        }
        AddDepositHistory(type, sum, "Deposit");
        contextDB.SaveChanges();
    }
    private void AddDepositHistory(UserType type, double sum, string amountType)
    {
        using AppContextDB contextDB = new AppContextDB();
        if (type == UserType.Simple)
        {
            var user = contextDB.Users.FirstOrDefault(p => p.Email == Settings1.Default.MailOrCode);
            var amountHistory = new BankAmountHistory() { Amount = sum, Time = DateTime.Now, UserId = user.Id, AmountType = amountType };
            contextDB.amountHistory.Add(amountHistory);
        }
        else
        {
            var user = contextDB.UsersVIP.FirstOrDefault(p => p.VIP_Number == Settings1.Default.MailOrCode);
            var amountHistoryVIP = new BankAmountHistoryVIP() { Amount = sum, Time = DateTime.Now, VIP_UserId = user.Id, AmountType = amountType };
            contextDB.amountHistoryVIPs.Add(amountHistoryVIP);

        }
        contextDB.SaveChanges();
    }
    public bool MakeWithdraw(UserType type, double sum)
    {
        using AppContextDB contextDB = new AppContextDB();


        if (type == UserType.Simple)
        {
            var user = contextDB.Users.Include(p => p.BankAccount).FirstOrDefault(p => p.Email == Settings1.Default.MailOrCode);
            if (user.BankAccount.Amount < sum)
                return false;
            user.BankAccount.Amount -= sum;
        }
        else
        {
            var user = contextDB.UsersVIP.Include(p => p.AccountVIP).FirstOrDefault(p => p.VIP_Number == Settings1.Default.MailOrCode);
            if(user.AccountVIP.Amount < sum)
                return false;
            user.AccountVIP.Amount -= sum;
        }
        AddDepositHistory(type, sum, "Withdraw");
        contextDB.SaveChanges();
        return true;
    }
}
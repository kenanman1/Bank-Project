using ClassLibrary1;
using Microsoft.EntityFrameworkCore;
namespace DB;

public class AppContextDB : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<VIP_User> UsersVIP { get; set; }
    public DbSet<BankAccount> Accounts { get; set; }
    public DbSet<BankAccountVIP> VIPAccounts { get; set; }
    public DbSet<BankAmountHistory> amountHistory { get; set; }
    public DbSet<BankAmountHistoryVIP> amountHistoryVIPs { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Settings1.Default.stringConnection);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveMaxLength(30);
        configurationBuilder.Properties<DateTime>().HaveColumnType("datetime");
        configurationBuilder.Properties<double>().HaveColumnType("decimal(9,2)");
    }
}

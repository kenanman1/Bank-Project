public abstract class UserCreator
{
    public UserCreator(string name, string surname, string emailOrCode, string password)
    {
        Name = name;
        Surname = surname;
        EmailOrCode = emailOrCode;
        Password = password;
    }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string EmailOrCode { get; set; }
    public string Password { get; set; }
    public abstract Users GetUser();
}
public class NoVIPUser : UserCreator
{
    public NoVIPUser(string name, string surname, string emailOrCode, string password)
        : base(name, surname, emailOrCode, password)
    {
    }

    public override Users GetUser()
    {
        return new User { Email = EmailOrCode, Name = Name, Password = Password, Surname = Surname };
    }
}
public class VIPUserCreator : UserCreator
{
    public VIPUserCreator(string name, string surname, string emailOrCode, string password)
        : base(name, surname, emailOrCode, password)
    {

    }
    public override Users GetUser()
    {
        return new VIP_User { Name = Name, VIP_Number = EmailOrCode, Password = Password, Surname = Surname };
    }
}
namespace Example03;

public interface IService
{
    User CreateUser();
}

public class Service : IService
{
    public User CreateUser()
    {
        return new User
        {
            Login = "admin",
            Password = "123",
            FirstName = "John",
            LastName = "Snow",
        };
    }
}
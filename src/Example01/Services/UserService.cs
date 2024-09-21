using Example01.Models;

namespace Example01.Services;

public interface IUserService
{
    User GetUserById(int userId);
}

public class UserService : IUserService
{
    public User GetUserById(int userId)
    {
        return new User
        {
            Id = userId,
            Login = "admin",
            Password = "123",
            FirstName = "John",
            LastName = "Snow",
        };
    }
}
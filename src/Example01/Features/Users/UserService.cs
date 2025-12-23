namespace Example01.Features.Users;

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
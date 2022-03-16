namespace Common.Authentication
{
    public interface IAuthenticatedUser
    {
        int Id { get; }
    }

    public class AuthenticatedUser : IAuthenticatedUser
    {
        public int Id { get { return 1; } }
    }
}

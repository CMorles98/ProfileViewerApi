namespace ProfileViewer.Domain.Services.Base
{
    public interface IServiceManager
    {
        public IUserService UserService { get; }
        public IAuthService AuthService { get; }
    }
}

namespace ProfileViewer.Domain.Repositories
{
    public interface IRepositoryManager
    {
        IRoleRepository RoleRepository { get; }
        IRoleClaimsRepository RoleClaimsRepository { get; }
        IUserRepository UserRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IUserClaimsRepository UserClaimsRepository { get; }
        Task Commit();
    }
}

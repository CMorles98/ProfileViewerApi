using ProfileViewer.Domain.Repositories;
using ProfileViewer.Infrastructure.Context;

namespace ProfileViewer.Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ProfileViewerContext _context;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IUserRoleRepository> _userRoleRepository;
        private readonly Lazy<IUserClaimsRepository> _userClaimsRepository;
        private readonly Lazy<IRoleRepository> _roleRepository;
        private readonly Lazy<IRoleClaimsRepository> _roleClaimsRepository;

        public RepositoryManager(ProfileViewerContext context)
        {
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
            _userRoleRepository = new Lazy<IUserRoleRepository>(() => new UserRoleRepository(context));
            _userClaimsRepository = new Lazy<IUserClaimsRepository>(() => new UserClaimsRepository(context));
            _roleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(context));
            _roleClaimsRepository = new Lazy<IRoleClaimsRepository>(() => new RoleClaimsRepository(context));
            _context = context;
        }

        public IUserRepository UserRepository => _userRepository.Value;
        public IUserRoleRepository UserRoleRepository => _userRoleRepository.Value;
        public IUserClaimsRepository UserClaimsRepository => _userClaimsRepository.Value;
        public IRoleRepository RoleRepository => _roleRepository.Value;
        public IRoleClaimsRepository RoleClaimsRepository => _roleClaimsRepository.Value;
        public Task Commit() => _context.SaveChangesAsync();
    }
}

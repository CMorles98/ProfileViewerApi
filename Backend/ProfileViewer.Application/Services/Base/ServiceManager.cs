using AutoMapper;
using Serilog;
using ProfileViewer.Domain.Authentication;
using ProfileViewer.Domain.Repositories;
using ProfileViewer.Domain.Services;
using ProfileViewer.Domain.Services.Base;
using ProfileViewer.Domain.Validators.Base;

namespace ProfileViewer.Application.Services.Base
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _UserService;
        private readonly Lazy<IAuthService> _AuthService; 
        public ServiceManager(
            IRepositoryManager repository, 
            IMapper mapper, 
            IValidatorManager validator,
            IJwtManager jwtManager,
            ILogger logger)
        {
            _UserService = new Lazy<IUserService>(() => new UserService(repository, mapper, validator));
            _AuthService = new Lazy<IAuthService>(() => new AuthService(repository, mapper, validator, jwtManager, logger)); ;
        }
        public IUserService UserService => _UserService.Value;
        public IAuthService AuthService => _AuthService.Value;

    }
}

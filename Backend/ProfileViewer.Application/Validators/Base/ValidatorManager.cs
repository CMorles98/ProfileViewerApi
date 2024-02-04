using AutoMapper;
using ProfileViewer.Domain.Localization;
using ProfileViewer.Domain.Logger;
using ProfileViewer.Domain.Repositories;
using ProfileViewer.Domain.Validators;
using ProfileViewer.Domain.Validators.Base;

namespace ProfileViewer.Application.Validators.Base
{
    public class ValidatorManager : IValidatorManager
    {
        private readonly Lazy<IUserValidator> _userValidator;
        private readonly Lazy<IAuthValidator> _authValidator;
        public ValidatorManager(
            IRepositoryManager repository, 
            IMapper mapper, 
            ILoggerManager logger, 
            ILocalizationManager localization)
        {
            _userValidator = new Lazy<IUserValidator>(() => new UserValidator(repository,mapper,logger,localization));
            _authValidator = new Lazy<IAuthValidator>(() => new AuthValidator(repository,mapper,logger,localization));
        }
        public IUserValidator UserValidator => _userValidator.Value;
        public IAuthValidator AuthValidator => _authValidator.Value;
    }
}

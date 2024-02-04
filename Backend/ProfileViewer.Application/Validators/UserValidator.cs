using AutoMapper;
using ProfileViewer.Application.Exceptions;
using ProfileViewer.Domain.DTOs.Users;
using ProfileViewer.Domain.Localization;
using ProfileViewer.Domain.Logger;
using ProfileViewer.Domain.Repositories;
using ProfileViewer.Domain.Validators;
using ProfileViewer.Domain.Entities;
using ProfileViewer.Domain.Validators.Common;
using System.Text.RegularExpressions;
using ProfileViewer.Domain.Enums;

namespace ProfileViewer.Application.Validators
{
    public class UserValidator(IRepositoryManager repository, IMapper mapper, ILoggerManager logger, ILocalizationManager localization) : IUserValidator
    {
        private readonly ILocalizationManager _localization = localization;
        private readonly IRepositoryManager _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ILoggerManager _logger = logger;

        public async Task<User> Validate(EditUserDto dto, Guid id)
        {
            if (Equals(id, Guid.Empty))
            {
                string exceptionMsg = string.Format(_localization.Localize("ErrorRequiredField"), nameof(UserDto.Id));
                _logger.LogError(exceptionMsg);
                throw new BadRequestException(exceptionMsg);
            }

            if (!Regex.IsMatch(dto.Email, ValidatorRegex.emailRegex))
            {
                string exceptionMsg = _localization.Localize("ErrorBadEmail");
                _logger.LogError(exceptionMsg);
                throw new UnprocessableEntityException(exceptionMsg);
            }

            var user = await _repository.UserRepository.Get(x => Equals(x.Id, id));

            if (user is null)
            {
                string exceptionMsg = string.Format(_localization.Localize("ErrorCouldNotFoundRecord"),nameof(IdentityEnum.User));
                _logger.LogError(exceptionMsg);
                throw new NotFoundException(exceptionMsg);
            }

            var userRole = await _repository.UserRoleRepository.Get(x => x.UserId.Equals(user.Id));

            if (userRole is null)
            {
                string exceptionMsg = string.Format(_localization.Localize("ErrorCouldNotFoundRecord"), nameof(IdentityEnum.UserRole));
                _logger.LogError(exceptionMsg);
                throw new SystemException();
            }

            var roleExists = await _repository.RoleRepository.Exists(x => x.Id.Equals(userRole.RoleId));

            if (!roleExists)
            {
                string exceptionMsg = string.Format(_localization.Localize("ErrorCouldNotFoundRecord"), nameof(IdentityEnum.Role));
                _logger.LogError(exceptionMsg);
                throw new SystemException();
            }

            return user;
        }

        public async Task<UserDto> Validate(Guid id)
        {
            if (Equals(id, Guid.Empty))
            {
                string exceptionMsg = string.Format(_localization.Localize("ErrorRequiredField"), nameof(UserDto.Id));
                _logger.LogError(exceptionMsg);
                throw new BadRequestException(exceptionMsg);
            }

            var user = await _repository.UserRepository.Get(x => Equals(x.Id, id), asNoTracking: true);

            if (user is null)
            {
                string exceptionMsg = string.Format(_localization.Localize("ErrorCouldNotFoundRecord"), nameof(IdentityEnum.User));
                _logger.LogError(exceptionMsg);
                throw new NotFoundException(exceptionMsg);
            }

            var userRole = await _repository.UserRoleRepository.Get(x => x.UserId.Equals(user.Id));

            if (userRole is null)
            {
                string exceptionMsg = string.Format(_localization.Localize("ErrorCouldNotFoundRecord"), nameof(IdentityEnum.UserRole));
                _logger.LogError(exceptionMsg);
                throw new SystemException();
            }

            var role = await _repository.RoleRepository.Get(x => x.Id.Equals(userRole.RoleId));

            if (role is null)
            {
                string exceptionMsg = string.Format(_localization.Localize("ErrorCouldNotFoundRecord"), nameof(IdentityEnum.Role));
                _logger.LogError(exceptionMsg);
                throw new SystemException();
            }

            var userDto = _mapper.Map<UserDto>(user);
            userDto.RoleName = role.Name;

            return userDto;
        }

    }
}

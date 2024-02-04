using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using ProfileViewer.Application.Exceptions;
using ProfileViewer.Domain.DTOs.Auth;
using ProfileViewer.Domain.DTOs.Users;
using ProfileViewer.Domain.Entities;
using ProfileViewer.Domain.Localization;
using ProfileViewer.Domain.Logger;
using ProfileViewer.Domain.Repositories;
using ProfileViewer.Domain.Validators;
using ProfileViewer.Domain.Validators.Common;

namespace ProfileViewer.Application.Validators
{
    public class AuthValidator(IRepositoryManager repository, IMapper mapper, ILoggerManager logger, ILocalizationManager localization) : IAuthValidator
    {
        private readonly IRepositoryManager _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ILoggerManager _logger = logger;
        private readonly ILocalizationManager _localization = localization;

        public async Task Validate(RegisterDto dto)
        {


            if (!Regex.IsMatch(dto.Email, ValidatorRegex.emailRegex))
            {
                string exceptionMsg = _localization.Localize("ErrorBadEmail");
                _logger.LogError(exceptionMsg);
                throw new BadRequestException(exceptionMsg);
            }

            if (!Regex.IsMatch(dto.Password, ValidatorRegex.passwordRegex))
            {
                string exceptionMsg = _localization.Localize("ErrorBadPassword");
                _logger.LogError(exceptionMsg);
                throw new BadRequestException(exceptionMsg);
            }

            var filters = _mapper.Map<UserFiltersDto>(dto);

            var user = await _repository.UserRepository.Get(x => x.Email!.Contains(dto.Email));
            
            if (user is not null)
            {
                string exceptionMsg = _localization.Localize("ErrorRecordExists");
                _logger.LogError(exceptionMsg);
                throw new UnprocessableEntityException(exceptionMsg);
            }
        }

        public Task Validate(RefreshTokenDto dto)
        {
            if (string.IsNullOrEmpty(dto.Token))
            { 
                string exceptionMsg = string.Format(_localization.Localize("ErrorRequiredField"), nameof(RefreshTokenDto.Token));
                _logger.LogError(exceptionMsg);
                throw new BadRequestException(exceptionMsg);
            }

            return Task.CompletedTask;
        }

        public async Task<UserDto> Validate(LoginDto dto)
        {
            if (!Regex.IsMatch(dto.Email, ValidatorRegex.emailRegex))
            {
                string exceptionMsg = _localization.Localize("ErrorBadEmail");
                _logger.LogError(exceptionMsg);
                throw new BadRequestException(exceptionMsg);
            }

            if (!Regex.IsMatch(dto.Password, ValidatorRegex.passwordRegex))
            {
                string exceptionMsg = _localization.Localize("ErrorBadPassword");
                _logger.LogError(exceptionMsg);
                throw new BadRequestException(exceptionMsg);
            }

            var filters = _mapper.Map<UserFiltersDto>(dto);

            var user = await _repository.UserRepository.Get(x => x.Email!.Contains(dto.Email));

            if (user is null)
            {
                string exceptionMsg = _localization.Localize("ErrorInvalidCredentials");
                _logger.LogError(exceptionMsg);
                throw new BadRequestException(exceptionMsg);
            }

            var passwordHasher = new PasswordHasher<User>();
            var passwordValidationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, dto.Password);

            if (Equals(passwordValidationResult, PasswordVerificationResult.Failed))
            {
                string exceptionMsg = _localization.Localize("ErrorInvalidCredentials");
                _logger.LogError(exceptionMsg);
                throw new BadRequestException(exceptionMsg);
            }

            var userRole = await _repository.UserRoleRepository.Get(x => x.UserId.Equals(user.Id));

            if (userRole is null)
            {
                _logger.LogError($"There is no roles associated to user with id: {user.Id}");
                throw new SystemException();
            }

            var role = await _repository.RoleRepository.Get(x => x.Id.Equals(userRole.RoleId));

            if (role is null)
            {
                string exceptionMsg = string.Format(_localization.Localize("ErrorCouldNotFoundRole"), user.Email);
                _logger.LogError(exceptionMsg);
                throw new NotFoundException(exceptionMsg);
            }

            var userDto = _mapper.Map<UserDto>(user);
            userDto.RoleName = role.Name;
            return userDto;
        }
    }
}

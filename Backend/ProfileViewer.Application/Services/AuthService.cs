using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Serilog;
using ProfileViewer.Domain.Authentication;
using ProfileViewer.Domain.DTOs.Auth;
using ProfileViewer.Domain.Entities;
using ProfileViewer.Domain.Repositories;
using ProfileViewer.Domain.Services;
using ProfileViewer.Domain.Validators.Base;

namespace ProfileViewer.Application.Services
{
    public class AuthService(IRepositoryManager repository, IMapper mapper, IValidatorManager validator, IJwtManager jwtManager, ILogger logger) : IAuthService
    {
        private readonly IRepositoryManager _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidatorManager _validator = validator;
        private readonly IJwtManager _jwtManager = jwtManager;
        private readonly ILogger _logger = logger;

        public async Task<AuthResponseDto> Login(LoginDto dto)
        {
            var user = await _validator.AuthValidator.Validate(dto);

            var token = _jwtManager.GenerateToken(user.Id);

            return new AuthResponseDto(true, token, user.Id);

        }

        public async Task<AuthResponseDto> RefreshToken(RefreshTokenDto dto)
        {
            await _validator.AuthValidator.Validate(dto);

            var token = _jwtManager.RefreshToken(dto.Token);

            return new AuthResponseDto(true, token);
        }

        public async Task<AuthResponseDto> Register(RegisterDto dto)
        {
            await _validator.AuthValidator.Validate(dto);

            var user = _mapper.Map<User>(dto);

            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, dto.Password);

            await _repository.UserRepository.Create(user);

            await _repository.Commit();

            await SetAdminRole(user.Id);

            var role = await _repository.UserRoleRepository.Get(x => Equals(x.UserId, user.Id));

            var token = _jwtManager.GenerateToken(user.Id);

            return new AuthResponseDto(true, token, user.Id);
        }

        private async Task SetAdminRole(Guid userId)
        {
            //TODO: DEFINE ROLE ASSIGNMENT PROCESS
            var adminRole = await _repository.RoleRepository.Get(x => x.Name == "ADMIN");

            if ((adminRole?.Id ?? Guid.Empty).Equals(Guid.Empty))
            {
                _logger.Error("There is no ADMIN role");
                throw new SystemException();
            }

            await _repository.UserRoleRepository.Create(new IdentityUserRole<Guid>
            {
                UserId = userId,
                RoleId = adminRole.Id
            });

            var permissions = await _repository.RoleClaimsRepository.GetAll(x => x.RoleId.Equals(adminRole.Id));

            var userClaims = permissions.Select(x => new IdentityUserClaim<Guid>()
            {
                ClaimType = x.ClaimType,
                ClaimValue = x.ClaimValue,
                UserId = userId
            });

            await _repository.UserClaimsRepository.CreateMultiple(userClaims);
            await _repository.Commit();
        }
    }
}

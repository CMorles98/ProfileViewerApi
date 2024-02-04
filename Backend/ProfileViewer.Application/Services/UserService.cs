using AutoMapper;
using System.Linq.Expressions;
using ProfileViewer.Application.Pagination;
using ProfileViewer.Domain.DTOs.Base;
using ProfileViewer.Domain.DTOs.Users;
using ProfileViewer.Domain.Entities;
using ProfileViewer.Domain.Pagination;
using ProfileViewer.Domain.Repositories;
using ProfileViewer.Domain.Services;
using ProfileViewer.Domain.Validators.Base;

namespace ProfileViewer.Application.Services
{
    public class UserService(IRepositoryManager repository, IMapper mapper, IValidatorManager validator) : IUserService
    {
        private readonly IRepositoryManager _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidatorManager _validator = validator;

        public async Task Delete(Guid id)
        {
            var user = await _validator.UserValidator.Validate(id);
            
            var userClaims = await _repository.UserClaimsRepository.GetAll(x => x.UserId.Equals(user.Id));
            await _repository.UserClaimsRepository.DeleteMultiple(userClaims);
            await _repository.Commit();

            var userRoles = await _repository.UserRoleRepository.GetAll(x => x.UserId.Equals(user.Id));
            await _repository.UserRoleRepository.DeleteMultiple(userRoles);
            await _repository.Commit();

            _repository.UserRepository.Delete(
                _mapper.Map<User>(user)); 
            await _repository.Commit();
        }

        public async Task<PagedResult<ListUserDto>> GetAll(UserFiltersDto filters)
        {
            filters.AsNoTracking = true;

            Expression<Func<User, bool>> filterExpression = x =>
            (string.IsNullOrEmpty(filters.Email) || x.Email!.Contains(filters.Email));

            int totalCount = await _repository.UserRepository.GetTotalCount(filterExpression, filters.AsNoTracking) ;

            var pagedList = await _repository.UserRepository.GetAll(filterExpression, filters.AsNoTracking, new PaginationFilter(filters.PageNumber, filters.PageSize, filters.Sorting));

            var mappedList = _mapper.Map<IEnumerable<ListUserDto>>(pagedList);

            return PaginationManager<ListUserDto>.CreatePagedResult(mappedList, totalCount, filters.PageNumber, filters.PageSize); 
        }

        public async Task<UserDto> GetById(Guid id)
        {
            return await _validator.UserValidator.Validate(id);
        }

        public async Task<UserDto> Update(EditUserDto dto, Guid id)
        {
            var user = await _validator.UserValidator.Validate(dto, id);

            _mapper.Map(dto, user);
            
            await _repository.Commit();

            return await GetById(id);
        }
    }
}

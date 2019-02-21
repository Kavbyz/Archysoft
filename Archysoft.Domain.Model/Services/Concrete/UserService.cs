using System.Linq;
using Archysoft.Data.Repositories.Abstract;
using Archysoft.Domain.Model.Extensions;
using Archysoft.Domain.Model.Model.Common;
using Archysoft.Domain.Model.Model.Users;
using Archysoft.Domain.Model.Services.Abstract;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Archysoft.Domain.Model.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public SearchResult<UserGridModel> Get(BaseFilter filter)
        {
            var users = _userRepository.GetReadonly().Include(x => x.Profile).FilterUsers(filter).Select(x => _mapper.Map<UserGridModel>(x));
            var searchResult = users.BaseFilter(filter);

            return searchResult;
        }
    }
}

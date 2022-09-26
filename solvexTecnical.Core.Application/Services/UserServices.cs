using AutoMapper;
using solvexTecnical.Core.Application.DTOs;
using solvexTecnical.Core.Application.Interfaces.IRespositories;
using solvexTecnical.Core.Application.Interfaces.IServicies;
using solvexTecnical.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace solvexTecnical.Core.Application.Services
{
    public class UsersServices : CommonServices<Users, UserDTO>, IUserServices
    {
        private readonly IMapper _mapper;
        private readonly IUsersRepository _userRepository;
        public UsersServices(IUsersRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _mapper = mapper;
            _userRepository = repo;
        }
    }
}

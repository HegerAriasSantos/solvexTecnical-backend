using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using solvexTecnical.Core.Application.Interfaces.IServicies;
using solvexTecnical.Core.Application.Interfaces.IRespositories;

namespace solvexTecnical.Core.Application.Services
{
    public class CommonServices<T, DTO> : ICommonService<T, DTO>
        where T : class
        where DTO : class
    {
        private readonly ICommonRepository<T> _repo;
        private readonly IMapper _mapper;
        public CommonServices(ICommonRepository<T> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public virtual async Task<DTO> Add(DTO DTO)
        {
            T t = _mapper.Map<T>(DTO);
            t = await _repo.AddAsync(t);
            DTO dto = _mapper.Map<DTO>(t);
            return dto;
        }

        public virtual async Task Update(DTO DTO, int id)
        {
            T t = _mapper.Map<T>(DTO);
            await _repo.UpdateAsync(t, id);
        }

        public virtual async Task<List<DTO>> GetAllDTO()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<List<DTO>>(entities);
        }

        public virtual async Task<DTO> GetByIdSaveDTO(int id)
        {
            T t = await _repo.GetByIdAsync(id);
            DTO dto = _mapper.Map<DTO>(t);
            return dto;
        }

        public virtual async Task<DTO> GetByIdDTO(int id)
        {
            T t = await _repo.GetByIdAsync(id);
            DTO dto = _mapper.Map<DTO>(t);
            return dto;
        }

        public virtual async Task Delete(int id)
        {
            T t = await _repo.GetByIdAsync(id);
            await _repo.DeleteAsync(t, id);
        }

        public virtual async Task<List<DTO>> GetAllWithIncludesAsync(List<string> props)
        {
            var entities = await _repo.GetAllWithIncludesAsync(props);
            return _mapper.Map<List<DTO>>(entities);
        }

        public virtual async Task<DTO> GetByIdWithIncludeAsync(int id, List<string> props, List<string> colls)
        {
            T entities = await _repo.GetByIdWithIncludeAsync(id, props, colls);
            return _mapper.Map<DTO>(entities);
        }
    }
}

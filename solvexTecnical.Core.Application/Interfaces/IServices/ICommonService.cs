using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace solvexTecnical.Core.Application.Interfaces.IServicies
{
    public interface ICommonService<T, DTO>
        where T : class
        where DTO : class
    {
        Task<DTO> Add(DTO dTO);
        Task Update(DTO dTO, int id);
        Task Delete(int id);    
        Task<List<DTO>> GetAllDTO();
        Task<DTO> GetByIdSaveDTO(int id);
        Task<DTO> GetByIdDTO(int id);
        Task<List<DTO>> GetAllWithIncludesAsync(List<string> props);
        Task<DTO> GetByIdWithIncludeAsync(int id, List<string> props, List<string> colls);
    }
}

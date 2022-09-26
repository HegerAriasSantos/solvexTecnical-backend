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
    public class BrandServices : CommonServices<ProductsBrands, BrandDTO>, IBrandService
    {
        private readonly IMapper _mapper;
        private readonly IProductsBrandsRepository _productsBrandsRepository;
        public BrandServices(IProductsBrandsRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _mapper = mapper;
            _productsBrandsRepository = repo;
        }
    }
}

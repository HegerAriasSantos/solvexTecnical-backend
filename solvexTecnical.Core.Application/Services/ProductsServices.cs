using AutoMapper;
using solvexTecnical.Core.Application.DTOs;
using solvexTecnical.Core.Application.Interfaces.IRespositories;
using solvexTecnical.Core.Application.Interfaces.IServicies;
using solvexTecnical.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace solvexTecnical.Core.Application.Services
{
    public class ProductsServices : CommonServices<FinalProducts, FinalProductDTO>, IProductsServices
    {
        private readonly IMapper _mapper;
        private readonly IProductsRepository _productsRepository;
        private readonly IFinalProductsRepository _finalProductsRepository;
        private readonly ISuperMarketRepository _superMarketRepository;
        private readonly IProductsBrandsRepository _brandsRepository;
        public ProductsServices(IProductsRepository repo, IFinalProductsRepository finalProductsRepository , ISuperMarketRepository superMarketRepository, IProductsBrandsRepository productsBrandsRepository, IMapper mapper) : base(finalProductsRepository, mapper)
        {
            _mapper = mapper;
            _productsRepository = repo;
            _finalProductsRepository = finalProductsRepository;
            _superMarketRepository = superMarketRepository;
            _brandsRepository = productsBrandsRepository;
        }

        public async Task<FinalProductDTO> AddProduct(FinalProductDTO finalProductDTO)
        {
            var product = _mapper.Map<FinalProductDTO,Products >(finalProductDTO);
            var result = await _productsRepository.AddAsync(product);

            finalProductDTO.ProductId = result.Id;
            var finalProduct = _mapper.Map<FinalProductDTO,FinalProducts>(finalProductDTO);
            var finalProductResult = await _finalProductsRepository.AddAsync(finalProduct);
            finalProductDTO.Id = finalProductResult.ProductId;
            finalProductDTO.SuperMarketId = product.SuperMarketId;

            return finalProductDTO;
        }
       

        public async Task<List<FinalProductDTO>> GetAllProducts()
        {
            var finalProduct = await _finalProductsRepository.GetAllAsync();
            var finalProductDTO = _mapper.Map<List<FinalProducts>, List<FinalProductDTO>>(finalProduct);

            foreach (var item in finalProductDTO)
            {
                var product = await _productsRepository.GetByIdAsync(item.ProductId);
                item.Name = product.Name;
                item.SuperMarketId = product.SuperMarketId;

                var brand = await _brandsRepository.GetByIdAsync(item.BrandId);
                item.Brand = _mapper.Map<ProductsBrands, BrandDTO>(brand);

            }
            return finalProductDTO;
        }

        public async Task<FinalProductDTO> SetName(FinalProductDTO finalProductDTO)
        {
            var product = await _productsRepository.GetByIdAsync(finalProductDTO.ProductId);
            product.Name = finalProductDTO.Name;
            await _productsRepository.UpdateAsync(product, finalProductDTO.ProductId);
            return finalProductDTO;

        }
        public async Task<FinalProductDTO> GetOne(int id)
        {
            var finalProduct = await _finalProductsRepository.GetByIdAsync(id);
            var product = await _productsRepository.GetByIdAsync(finalProduct.ProductId);
            var brand = await _brandsRepository.GetByIdAsync(finalProduct.BrandId);
            var finalProductDTO = _mapper.Map<FinalProducts, FinalProductDTO>(finalProduct);
            finalProductDTO.SuperMarketId = product.SuperMarketId;
            finalProductDTO.Name = product.Name;
            finalProductDTO.Brand = _mapper.Map<ProductsBrands, BrandDTO>(brand);
            return finalProductDTO;

        }
        public async Task<FinalProductDTO> GetOneForShoppingList(int id)
        {
            var finalProduct = await _finalProductsRepository.GetOneForListById(id);
            var product = await _productsRepository.GetByIdForListAsync(finalProduct.ProductId);
            var brand = await _brandsRepository.GetByIdForListAsync(finalProduct.BrandId);
            var finalProductDTO = _mapper.Map<FinalProducts, FinalProductDTO>(finalProduct);
            finalProductDTO.SuperMarketId = product.SuperMarketId;
            finalProductDTO.Name = product.Name;
            finalProductDTO.Brand = _mapper.Map<ProductsBrands, BrandDTO>(brand);
            return finalProductDTO;

        }
    }
}

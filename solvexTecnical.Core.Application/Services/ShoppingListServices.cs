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
    public class ShoppingListServices : CommonServices<ShoppingList, ShoppingListDTO>, IShoppingList
    {
        private readonly IMapper _mapper;
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListProductsRepository _shoppingListProductsRepository;
        private readonly ISuperMarketRepository _superMarketRepository;
        private readonly IProductsServices _productsServices;
        private readonly IUsersRepository _usersRepository;
        public ShoppingListServices(IShoppingListRepository repo,IShoppingListProductsRepository shoppingListProductsRepository , IProductsServices productsServices, ISuperMarketRepository super, IUsersRepository usersRepository, IMapper mapper) : base(repo, mapper)
        {
            _mapper = mapper;
            _shoppingListRepository = repo;
            _shoppingListProductsRepository = shoppingListProductsRepository;
            _productsServices = productsServices;
            _superMarketRepository = super;
            _usersRepository = usersRepository;
        }

        public async Task<ShoppingListDTO> AddShoppingList(ShoppingListDTO shoppingList)
        {
            var shoppingListEntity = _mapper.Map<ShoppingList>(shoppingList);
            var shoppingListAdded =  await _shoppingListRepository.AddAsync(shoppingListEntity);
            var shoppingListDTO = _mapper.Map<ShoppingList, ShoppingListDTO>(shoppingListAdded);
            shoppingListDTO.Products = new List<FinalProductDTO>();
            decimal totalPrice = 0;

            foreach (var item in  shoppingList.ProductsIds)
            {
                var product = await _productsServices.GetOne(item);
                var shoppingItem = await _shoppingListProductsRepository.AddAsync(
                    new ShoppingListProducts
                    {
                        FinalProductId = product.Id,
                        ShoppingListId = shoppingListEntity.Id
                    });
                shoppingListDTO.Products.Add(_mapper.Map<FinalProductDTO>(product));
                totalPrice += product.Price;
            }
            shoppingListAdded.TotalPrice = totalPrice;
            shoppingListDTO.TotalPrice = totalPrice;
            
            await _shoppingListRepository.UpdateAsync(shoppingListAdded, shoppingListAdded.Id);

            return _mapper.Map<ShoppingListDTO>(shoppingListDTO);
        }
        public async Task<List<ShoppingListDTO>> GetAllByUser(int userId)
        {
            var shoppingLists = await _shoppingListRepository.GetAllShoppingListByUser(userId);
            var shoppingListDTOs = _mapper.Map<List<ShoppingList>, List<ShoppingListDTO>>(shoppingLists);

            foreach (var item in shoppingListDTOs)
            {
                var superMarket  = await _superMarketRepository.GetOneByShoppingList(item.SuperMarketId);
                item.SuperMarketName = superMarket.Name;
                var User = await _usersRepository.GetByIdAsync(item.UserId);
                item.User = _mapper.Map<UserDTO>(User);
                item.Products = new List<FinalProductDTO>();
                var products = await _shoppingListProductsRepository.GetAllByShoppingListId(item.Id);
                foreach (var ProductItem in products)
                {
                    var product = await _productsServices.GetOneForShoppingList(ProductItem.FinalProductId); 
                    item.Products.Add(_mapper.Map<FinalProductDTO>(product));
                }
            }
            return shoppingListDTOs;
        }

        public async Task<List<ShoppingListDTO>> GetAllShoppingLists()
        {
            var shoppingLists = await _shoppingListRepository.GetAllAsync();
            var shoppingListDTOs = _mapper.Map<List<ShoppingList>, List<ShoppingListDTO>>(shoppingLists);

            foreach (var item in shoppingListDTOs)
            {
                var superMarket = await _superMarketRepository.GetOneByShoppingList(item.SuperMarketId);
                item.SuperMarketName = superMarket.Name;
                var User = await _usersRepository.GetByIdAsync(item.UserId);
                item.User = _mapper.Map<UserDTO>(User);
                item.Products = new List<FinalProductDTO>();
                var products = await _shoppingListProductsRepository.GetAllByShoppingListId(item.Id);
                foreach (var ProductItem in products)
                {
                    var product = await _productsServices.GetOneForShoppingList(ProductItem.FinalProductId);
                    item.Products.Add(_mapper.Map<FinalProductDTO>(product));
                }
            }
            return shoppingListDTOs;
        }

    }
}

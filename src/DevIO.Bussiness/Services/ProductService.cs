using DevIO.Bussiness.Interfaces;
using DevIO.Bussiness.Interfaces.Repository;
using DevIO.Bussiness.Interfaces.Service;
using DevIO.Bussiness.Models;
using DevIO.Bussiness.Models.Validations;
using System;
using System.Threading.Tasks;

namespace DevIO.Bussiness.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUser _user;

        public ProductService(
            IProductRepository productRepository,
            INotificator notificator, 
            IUser user) : base(notificator)
        {
            _productRepository = productRepository;
            _user = user;
        }

        public async Task Insert(Product product)
        {
            if (!await RunValidation(new ProductValidation(), product))
                return;

            product.CreateDate = DateTime.Now;
            product.Supplier = null;

            await _productRepository.Insert(product);
        }

        public async Task Update(Product product)
        {
            if (!await RunValidation(new ProductValidation(), product))
                return;

            await _productRepository.Update(product);
        }

        public async Task Delete(Guid id)
        {
            await _productRepository.Delete(id);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}

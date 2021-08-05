using DevIO.Bussiness.Interfaces;
using DevIO.Bussiness.Interfaces.Repository;
using DevIO.Bussiness.Interfaces.Service;
using DevIO.Bussiness.Models;
using DevIO.Bussiness.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Bussiness.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;

        public SupplierService(
            ISupplierRepository supplierRepository,
            IAddressRepository addressRepository,
            INotificator notificator) : base(notificator)
        {
            _supplierRepository = supplierRepository;
            _addressRepository = addressRepository;
        }

        public async Task<bool> Insert(Supplier supplier)
        {
            if (!await RunValidation(new SupplierValidation(_supplierRepository), supplier)
                || !await RunValidation(new AddressValidation(), supplier.Address))
                return false;

            await _supplierRepository.Insert(supplier);
            return true;
        }

        public async Task<bool> Update(Supplier supplier)
        {
            if (!await RunValidation(new SupplierValidation(_supplierRepository), supplier))
                return false;

            await _supplierRepository.Update(supplier);
            return true;
        }

        public async Task UpdateAddress(Address address)
        {
            if (!await RunValidation(new AddressValidation(), address))
                return;

            await _addressRepository.Update(address);
        }

        public async Task<bool> Delete(Guid id)
        {
            bool hasProducts = _supplierRepository.GetSupplierProductsAdress(id).Result.Products.Any();
            if (hasProducts)
            {
                Notificar("O fornecedor possui produtos cadastrados.");
                return false;
            }

            var address = await _addressRepository.GetAddressBySupplier(id);
            if (address != null)
                await _addressRepository.Delete(address.Id);

            await _supplierRepository.Delete(id);
            return true;
        }

        public void Dispose()
        {
            _addressRepository?.Dispose();
            _supplierRepository?.Dispose();
        }
    }
}

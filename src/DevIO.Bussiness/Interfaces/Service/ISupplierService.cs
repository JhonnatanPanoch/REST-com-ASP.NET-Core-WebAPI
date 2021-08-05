using DevIO.Bussiness.Models;
using System;
using System.Threading.Tasks;

namespace DevIO.Bussiness.Interfaces.Service
{
    public interface ISupplierService : IDisposable
    {
        Task<bool> Insert(Supplier supplier);
        Task<bool> Update(Supplier supplier);
        Task<bool> Delete(Guid id);
        Task UpdateAddress(Address address);
    }
}

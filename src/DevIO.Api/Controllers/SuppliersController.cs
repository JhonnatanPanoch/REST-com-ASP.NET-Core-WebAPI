using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Bussiness.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevIO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : MainController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _map;

        public SuppliersController(
            ISupplierRepository supplierRepository,
            IMapper map)
        {
            _supplierRepository = supplierRepository;
            _map = map;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierViewModel>>> GetAll()
        {
            IEnumerable<SupplierViewModel> supplier = _map.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.List());
            return Ok(supplier);
        }
    }
}

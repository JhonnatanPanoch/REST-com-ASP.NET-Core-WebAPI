using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Bussiness.Interfaces.Repository;
using DevIO.Bussiness.Interfaces.Service;
using DevIO.Bussiness.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevIO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : MainController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _map;

        public SuppliersController(
            ISupplierRepository supplierRepository,
            IMapper map,
            ISupplierService supplierService)
        {
            _supplierRepository = supplierRepository;
            _map = map;
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierViewModel>>> GetAll()
        {
            IEnumerable<SupplierViewModel> supplier = _map.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.List());

            if (supplier == null)
                return NotFound();

            return Ok(supplier);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SupplierViewModel>> GetById(Guid id)
        {
            SupplierViewModel supplier = _map.Map<SupplierViewModel>(await _supplierRepository.GetSupplierProductsAdress(id));
            return Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<SupplierViewModel>> Create(SupplierViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Supplier model = _map.Map<Supplier>(viewModel);
            bool success = await _supplierService.Insert(model);

            if (!success)
                return BadRequest();

            return Ok(model);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<SupplierViewModel>> Update(Guid id, SupplierViewModel viewModel)
        {
            if (id != viewModel.Id || !ModelState.IsValid)
                return BadRequest();

            Supplier model = _map.Map<Supplier>(viewModel);
            bool success = await _supplierService.Update(model);

            if (!success)
                return BadRequest();

            return Ok(model);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<SupplierViewModel>> Delete(Guid id)
        {
            SupplierViewModel supplier = _map.Map<SupplierViewModel>(await _supplierRepository.Get(id));
            if (supplier == null)
                return NotFound();

            bool success = await _supplierService.Delete(id);
            if (!success)
                return BadRequest();

            return Ok(supplier);
        }
    }
}

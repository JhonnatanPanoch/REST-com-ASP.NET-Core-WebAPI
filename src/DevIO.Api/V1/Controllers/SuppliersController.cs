using AutoMapper;
using DevIO.Api.Controllers;
using DevIO.Api.Extensions;
using DevIO.Api.ViewModels;
using DevIO.Bussiness.Interfaces;
using DevIO.Bussiness.Interfaces.Repository;
using DevIO.Bussiness.Interfaces.Service;
using DevIO.Bussiness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevIO.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SuppliersController : MainController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _map;

        public SuppliersController(
            ISupplierRepository supplierRepository,
            IMapper map,
            ISupplierService supplierService,
            INotificator notificator, 
            IAddressRepository addressRepository,
            IUser user) : base(notificator, user)
        {
            _supplierRepository = supplierRepository;
            _map = map;
            _supplierService = supplierService;
            _addressRepository = addressRepository;
        }

        [AllowAnonymous]
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
        
        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<SupplierViewModel>> Create(SupplierViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _supplierService.Insert(_map.Map<Supplier>(viewModel));

            return CustomResponse(viewModel);
        }

        [HttpPut("{id:guid}")]
        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        public async Task<ActionResult<SupplierViewModel>> Update(Guid id, SupplierViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                NotifyError("Há uma diferença de informações na requisição.");
                return CustomResponse(viewModel);
            }

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _supplierService.Update(_map.Map<Supplier>(viewModel));

            return CustomResponse(viewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<SupplierViewModel>> Delete(Guid id)
        {
            SupplierViewModel supplierViewModel = _map.Map<SupplierViewModel>(await _supplierRepository.Get(id));
            if (supplierViewModel == null)
                return NotFound();

            await _supplierService.Delete(id);

            return CustomResponse(supplierViewModel);
        }

        [HttpGet("get-address/{id:guid}")]
        public async Task<ActionResult<AddressViewModel>> GetAddressById(Guid id)
        {
            AddressViewModel addressViewModel = _map.Map<AddressViewModel>(await _addressRepository.Get(id));
            return addressViewModel;
        }

        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("atualizar-endereco/{id:guid}")]
        public async Task<ActionResult<AddressViewModel>> UpdateAddress(Guid id, AddressViewModel addressViewModel)
        {
            if (id != addressViewModel.Id)
            {
                NotifyError("Há uma diferença de informações na requisição.");
                return CustomResponse(addressViewModel);
            }

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _addressRepository.Update(_map.Map<Address>(addressViewModel));

            return CustomResponse(addressViewModel);

        }
    }
}

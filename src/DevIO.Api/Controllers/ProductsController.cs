using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Bussiness.Interfaces;
using DevIO.Bussiness.Interfaces.Repository;
using DevIO.Bussiness.Interfaces.Service;
using DevIO.Bussiness.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DevIO.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProductsController : MainController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(
            INotificator notificator,
            IMapper map,
            IProductService roductService,
            IProductRepository productRepository) : base(notificator)
        {
            _mapper = map;
            _productService = roductService;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            IEnumerable<ProductViewModel> productsWithSuppliers = _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsSupplier());
            return productsWithSuppliers;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> ObterPorId(Guid id)
        {
            ProductViewModel productViewModel = await GetProduct(id);

            if (productViewModel == null)
                return NotFound();

            return CustomResponse(productViewModel);
        }

        [HttpDelete("{id}:guid")]
        public async Task<ActionResult<ProductViewModel>> Delete(Guid id)
        {
            ProductViewModel productViewModel = await GetProduct(id);

            if (productViewModel == null)
                return NotFound();

            await _productService.Delete(id);

            return CustomResponse(productViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> Create(ProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ActionResult handler = CustomResponse(ModelState);
                return handler;
            }

            try
            {
                viewModel.Image = Guid.NewGuid() + "_" + viewModel.Image;
                if (!UploadFile(viewModel.ImageUpload, viewModel.Image))
                    return CustomResponse();

                await _productService.Insert(_mapper.Map<Product>(viewModel));
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
            }

            return CustomResponse(viewModel);
        }

        /// Adicionar com opção de imagem muito grande.
        [RequestSizeLimit(410267659)]
        //[DisableRequestSizeLimit]
        [HttpPost("createLargeImage")]
        public async Task<ActionResult<ProductViewModel>> CreateAlternativo(ProductImageViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ActionResult handler = CustomResponse(ModelState);
                return handler;
            }

            try
            {
                string imgPrefix = Guid.NewGuid() + "_";
                if (!await UploadFileAlternative(viewModel.ImageUpload, imgPrefix))
                    return CustomResponse();

                viewModel.Image = imgPrefix + viewModel.ImageUpload.FileName;

                await _productService.Insert(_mapper.Map<Product>(viewModel));
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
            }

            return CustomResponse(viewModel);
        }

        // Exemplo para receber um request de imagem mto grande
        [RequestSizeLimit(410267659)]
        //[DisableRequestSizeLimit]
        [HttpPost("testRequestImage")]
        public ActionResult AdicionarImagem(IFormFile file)
        {
            return Ok(file);
        }

        private bool UploadFile(string file, string imgName)
        {
            if (string.IsNullOrEmpty(file))
            {
                NotifyError("Forneça uma imagem para este produto!");
                return false;
            }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/app/demo-webapi/src/assets", imgName);
            if (System.IO.File.Exists(filePath))
            {
                NotifyError("Já existe um arquivo com este nome!");
                return false;
            }

            byte[] imageDataByteArray = Convert.FromBase64String(file);
            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

            return true;
        }

        private async Task<bool> UploadFileAlternative(IFormFile file, string imgPrefix)
        {
            if (file == null || file.Length == 0)
            {
                NotifyError("Forneça uma imagem para este produto!");
                return false;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/app/demo-webapi/src/assets", imgPrefix + file.FileName);

            if (System.IO.File.Exists(path))
            {
                NotifyError("Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }

        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetProductSupplier(id));
        }
    }
}

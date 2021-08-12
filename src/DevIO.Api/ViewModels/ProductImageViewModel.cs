using DevIO.Api.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevIO.Api.ViewModels
{
    /// <summary>
    /// Viewmodel para envio de imagens grandes
    /// </summary>
    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "product")]
    public class ProductImageViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DisplayName("Fornecedor")]
        public Guid SupplierId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        [DisplayName("Descrição")]
        public string Description { get; set; }

        public string Image { get; set; }

        [DisplayName("Imagem do Produto")]
        public IFormFile ImageUpload { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DisplayName("Valor")]
        public decimal Value { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; }

        [DisplayName("Ativo?")]
        public bool Active { get; set; }

        public string SupplierName { get; set; }

        //public SupplierViewModel Supplier { get; set; }
        //public IEnumerable<SupplierViewModel> Suppliers { get; set; }
    }
}

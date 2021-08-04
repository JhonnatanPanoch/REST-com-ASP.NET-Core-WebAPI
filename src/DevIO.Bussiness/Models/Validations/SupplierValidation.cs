﻿using DevIO.Bussiness.Enumerators;
using DevIO.Bussiness.Interfaces.Repository;
using FluentValidation;
using System;
using System.Linq;

namespace DevIO.Bussiness.Models.Validations
{
    public class SupplierValidation : AbstractValidator<Supplier>
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierValidation(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;

            // Validação de nome
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            // Validações de documentos
            When(x => x.SupplierType == SupplierType.PHYSICAL, () =>
            {
                RuleFor(s => s.Document.Length)
                    .Equal(ValidationCpf.CpfLenght).WithMessage("O campo documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}");

                RuleFor(f => ValidationCpf.Validate(f.Document))
                    .Equal(true).WithMessage("O documento fornecido é inválido.");
            });
            When(x => x.SupplierType == SupplierType.LEGAL, () =>
            {
                RuleFor(s => s.Document.Length)
                    .Equal(ValidationCnpj.CnpjLenght).WithMessage("O campo documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}");

                RuleFor(f => ValidationCnpj.Validate(f.Document))
                    .Equal(true).WithMessage("O documento fornecido é inválido.");
            });

            RuleFor(x => IsDuplicated(x))
                .NotEqual(true).WithMessage("Já existe um fornecedor com este documento informado.");
        }

        private bool IsDuplicated(Supplier supplier)
        {
            var isDuplicated = _supplierRepository.List(l => l.Document == supplier.Document && l.Id != supplier.Id).Result.ToList().Any();
            return isDuplicated;
        }

        public class ValidationCpf
        {
            public const int CpfLenght = 11;
            public static bool Validate(string cpf)
            {
                return true;
            }
        }

        public class ValidationCnpj
        {
            public const int CnpjLenght = 14;
            public static bool Validate(string cpf)
            {
                return true;
            }
        }

        public class Utils
        {
            public static string OnlyNumbers(string value)
            {
                var onlyNumber = "";
                foreach (var item in value)
                {
                    if (char.IsDigit(item))
                    {
                        onlyNumber += item;
                    }
                }

                return onlyNumber.Trim();
            }
        }
    }
}

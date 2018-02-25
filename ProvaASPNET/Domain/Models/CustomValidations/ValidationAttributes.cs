using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain.Utils;

namespace Domain.Models.CustomValidations
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ValidCpf : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                string valor = value.ToString().Replace(".", "");

                valor = valor.Replace("-", "");

                var numeros = valor.ToCharArray().Select(x=> int.Parse(x.ToString())).ToList();

                var result = Validations.ValidCpf(numeros);
                if (result)
                    return ValidationResult.Success;

                return new ValidationResult("CPF inválido");
            }
            else
            {
                return new ValidationResult("CPF não pode ser vazio.");
            }
        }
    }
}
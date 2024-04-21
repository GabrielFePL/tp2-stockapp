using FluentAssertions;
using StockApp.Domain.Entities;
using StockApp.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StockApp.Domain.Test
{
    public class ProductUnitTest
    {
        #region Testes Positivos
        [Fact(DisplayName = "Create Product With Valid State")]
        public void CreateProduct_WithValidParameters_ResultValidState()
        {
            Action action = () => new Product(1, "Product Name", "Product Descrption", 10.45m, 10, "imageUrl", 1);
            action.Should().NotThrow<StockApp.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product With Valid Null State Stock")]
        public void CreateProduct_WithNullParameters_ResultValidStateStock()
        {
            Action action = () => new Product(1, "Product Name", "Product Descrption", 10.45m, null, "imageUrl", 1);
            action.Should().NotThrow<StockApp.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product With Valid Null State Stock Equals Zero")]
        public void CreateProduct_WithNullParameters_ResultValidStateStockEqualsZero()
        {
            Product product = new Product(1, "Product Name", "Product Descrption", 10.45m, null, "imageUrl", 1);
            product.Stock.Should().Be(0);
        }
        #endregion

        #region Testes Negativos
        [Fact(DisplayName = "Create Product With Invalid State Id")]
        public void CreateProduct_WithInvalidParameters_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product Descrption", 10.45m, 10, "imageUrl", 1);
            action.Should().Throw <StockApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Id value.");
        }

        [Fact(DisplayName = "Create Product With Null State Name")]
        public void CreateProduct_WithNullParameters_DomainExceptionInvalidName()
        {
            Action action = () => new Product(1, null, "Product Descrption", 10.45m, 10, "imageUrl", 1);
            action.Should().Throw<StockApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name, name is required.");
        }

        [Fact(DisplayName = "Create Product With Invalid State Short Name")]
        public void CreateProduct_WithInvalidParameters_DomainExceptionInvalidShortName()
        {
            Action action = () => new Product(1, "Pr", "Product Descrption", 10.45m, 10, "imageUrl", 1);
            action.Should().Throw<StockApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum 3 characters.");
        }

        [Fact(DisplayName = "Create Product With Invalid State Long Name")]
        public void CreateProduct_WithInvalidParameters_DomainExceptionInvalidLongName()
        {
            Action action = () => new Product(1, new string('P', 101), "Product Descrption", 10.45m, 10, "imageUrl", 1);
            action.Should().Throw<StockApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name, too long, maximum 100 characters.");
        }

        [Fact(DisplayName = "Create Product With Null State Desciption")]
        public void CreateProduct_WithNullParameters_DomainExceptionInvalidDescription()
        {
            Action action = () => new Product(1, "ProductName", null, 10.45m, 10, "imageUrl", 1);
            action.Should().Throw<StockApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid description, description is required.");
        }

        [Fact(DisplayName = "Create Product With Invalid State Short Desciption")]
        public void CreateProduct_WithInvalidParameters_DomainExceptionInvalidShortDescription()
        {
            Action action = () => new Product(1, "ProductName", "Desc", 10.45m, 10, "imageUrl", 1);
            action.Should().Throw<StockApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid description, too short, minimum 5 characters.");
        }

        [Fact(DisplayName = "Create Product With Invalid State Long Desciption")]
        public void CreateProduct_WithInvalidParameters_DomainExceptionInvalidLongDescription()
        {
            Action action = () => new Product(1, "ProductName", new string('D', 201), 10.45m, 10, "imageUrl", 1);
            action.Should().Throw<StockApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid description, too long, maximum 200 characters.");
        }

        [Fact(DisplayName = "Create Product With Null State Price")]
        public void CreateProduct_WithNullPrice_DomainExceptionNullPrice()
        {
            Action action = () => new Product(1, "Product Name", "Product Descrption", null, 10, "imageUrl", 1);
            action.Should().Throw<DomainExceptionValidation>()
                .WithMessage("Invalid price, price is required.");
        }

        [Fact(DisplayName = "Create Product With Invalid State Price")]
        public void CreateProduct_WithInvalidParameters_DomainExceptionInvalidPrice()
        {
            Action action = () => new Product(1, "ProductName", "Description", -1.45m, 10, "imageUrl", 1);
            action.Should().Throw<StockApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid price negative value.");
        }

        [Fact(DisplayName = "Create Product With Invalid State Decimal Places Price")]
        public void CreateProduct_WithInvalidParameters_DomainExceptionInvalidDecimalPlacesPrice()
        {
            Action action = () => new Product(1, "ProductName", "Description", 1.555m, 10, "imageUrl", 1);
            action.Should().Throw<StockApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid price, price must have exactly 2 decimal places.");
        }

        [Fact(DisplayName = "Create Product With Invalid State Long Price")]
        public void CreateProduct_WithInvalidParameters_DomainExceptionInvalidLongPrice()
        {
            Action action = () => new Product(1, "ProductName", "Description", 10000000.00m, 10, "imageUrl", 1);
            action.Should().Throw<StockApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid price, too long, maximum value 9999999.99.");
        }

        [Fact(DisplayName = "Create Product With Invalid State Stock")]
        public void CreateProduct_WithInvalidParameters_DomainExceptionInvalidStock()
        {
            Action action = () => new Product(1, "ProductName", "Description", 5.55m, -1, "imageUrl", 1);
            action.Should().Throw<StockApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid stock negative value.");
        }

        [Fact(DisplayName = "Create Product With Invalid State Long Image")]
        public void CreateProduct_WithInvalidParameters_DomainExceptionInvalidLongImage()
        {
            Action action = () => new Product(1, "ProductName", "Description", 5.55m, 1, new string('I', 251), 1);
            action.Should().Throw<StockApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid image name, too long, maximum 250 characters.");
        }

        [Fact(DisplayName = "Create Product With Null State Image")]
        public void CreateProduct_WithNullParameters_DomainExceptionInvalidImage()
        {
            Action action = () => new Product(1, "ProductName", "Description", 5.55m, 1, null, 1);
            action.Should().Throw<StockApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name, name is required.");
        }
        #endregion
    }
}

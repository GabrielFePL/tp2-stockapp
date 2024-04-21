using StockApp.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Domain.Entities
{
    public class Product
    {
        #region Atributos
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set;}
        public string Image { get; set; }
        public int CategoryId { get; set; }
        #endregion

        #region Construtores
        public Product(string name, string description, decimal? price, int? stock, string image, int categoryId)
        {
            CategoryId = categoryId;
            ValidateDomain(name, description, price, stock, image);
        }

        public Product(int id, string name, string description, decimal? price, int? stock, string image, int categoryId)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            Id = id;
            CategoryId = categoryId;
            ValidateDomain(name, description, price, stock, image);
        }

        public Category Category { get; set; }
        #endregion

        #region Validação
        private void ValidateDomain(string name, string description, decimal? price, int? stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Invalid name, name is required.");

            DomainExceptionValidation.When(name.Length < 3,
                "Invalid name, too short, minimum 3 characters.");

            DomainExceptionValidation.When(name.Length > 100,
                "Invalid name, too long, maximum 100 characters.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description),
                "Invalid description, description is required.");

            DomainExceptionValidation.When(description.Length < 5,
                "Invalid description, too short, minimum 5 characters.");

            DomainExceptionValidation.When(description.Length > 200,
                "Invalid description, too long, maximum 200 characters.");

            DomainExceptionValidation.When(price == null, "Invalid price, price is required.");

            DomainExceptionValidation.When(price < 0, "Invalid price negative value.");

            DomainExceptionValidation.When(decimal.Round(price.Value, 2) != price,
                "Invalid price, price must have exactly 2 decimal places.");

            DomainExceptionValidation.When(price > 9999999.99m, 
                "Invalid price, too long, maximum value 9999999.99.");

            DomainExceptionValidation.When(stock < 0, 
                "Invalid stock negative value.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(image),
                "Invalid name, name is required.");

            DomainExceptionValidation.When(image.Length > 250, 
                "Invalid image name, too long, maximum 250 characters.");

            Name = name;

            Description = description;

            Price = price;

            Stock = stock == null ? 0 : stock.Value;

            Image = image;
            #endregion
        }
    }
}
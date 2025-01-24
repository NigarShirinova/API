using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Features.Product.Commands.CreateProduct;

namespace UnitTests.MockData.Product.CreateProductHandler
{
    public static class CreateProductHandlerMockData
    {
        public static CreateProductCommand CreateProductCommandV1 = new CreateProductCommand
        {
            Name = "Mehsul",
            Description = "Description",
            Price = 10,
            Quantity = 1,
            Type = Common.Constants.ProductType.New

        };

        public static CreateProductCommand CreateProductCommandV2 = new CreateProductCommand
        {
            Name = "Mehsul",
            Description = "Description",
            Price = 10,
            Quantity = 1,
            Type = Common.Constants.ProductType.New,
            Photo = "salam"
        };
    }
}

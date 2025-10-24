using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Interfaces.Validators;
using Grocery.Core.Models;
using Grocery.Core.Services;
using Grocery.Core.Validators;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCore.Services
{
	public class ProductServiceTests
	{
		private IProductService _productService;
		private IProductRepository _productRepository;
		private IProductValidator _productValidator;
		
		private const int MAX_NAME_LENGTH = 80;

		[SetUp]
		public void Setup()
		{

			var repositoryMock = new Mock<IProductRepository>();
			repositoryMock
				.Setup(x => x.Update(It.IsAny<Product>()))
				.Returns(It.IsAny<Product>);

			_productRepository = repositoryMock.Object;
			_productValidator = new ProductValidator();
			_productService = new ProductService(_productRepository, _productValidator);
		}

		[Test]
		public void TestProductIsInvalidThrowsError()
		{
			var product = new Product(1, "", -2, new DateTime(), -5);

			// Assert that it throws any Exception.
			Assert.That(() => _productService.Update(product), Throws.Exception);
		}

		[Test]
		public void TestProductIsValidReturnsProduct()
		{
			var product = new Product(1, "Milk", 200, DateTime.UtcNow, (decimal) 0.95);

			Assert.DoesNotThrow(() => _productService.Update(product));
		}
	}
}

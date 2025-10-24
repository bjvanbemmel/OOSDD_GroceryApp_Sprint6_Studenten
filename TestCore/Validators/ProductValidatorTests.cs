using Grocery.Core.Exceptions;
using Grocery.Core.Interfaces.Validators;
using Grocery.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCore.Validators
{
	public class ProductValidatorTests
	{
		private IProductValidator _productValidator;
		private const int MAX_NAME_LENGTH = 80;

		[SetUp]
		public void Setup()
		{
			_productValidator = new ProductValidator();
		}

		[TestCase("")]
		public void TestProductHasEmptyNameThrowsError(string name)
		{
			Assert.Throws<ProductNameIsEmpty>(() => _productValidator.NameIsValid(name));
		}

		[TestCase("Hello, World!")]
		public void TestProductHasNonEmptyNameReturnsTrue(string name)
		{
			Assert.True(_productValidator.NameIsValid(name));
		}

		[Test]
		public void TestProductHasNameExceedingMaximumLengthThrowsError()
		{
			var nameExceedingLimit = new String('x', MAX_NAME_LENGTH + 1);

			Assert.Throws<ProductNameExceedsAllowedLength>(() => _productValidator.NameIsValid(nameExceedingLimit));
		}

		[TestCase("I am well below the limit!")]
		public void TestProductHasNameWithinMaximumLength(string name)
		{
			Assert.True(_productValidator.NameIsValid(name));
		}

		[TestCase(-2)]
		public void TestProductStockBelowMinimumThrowsError(int stock)
		{
			Assert.Throws<ProductStockBelowMinimum>(() => _productValidator.StockIsValid(stock));
		}

		[TestCase(0)]
		[TestCase(200)]
		public void TestProductStockAboveMinimumReturnsTrue(int stock)
		{
			Assert.True(_productValidator.StockIsValid(stock));
		}

		[Test]
		public void TestProductShelfLifeIsZeroValueThrowsError()
		{
			var zeroDate = new DateTime();
			Assert.Throws<ProductShelfLifeIsZero>(() => _productValidator.ShelfLifeIsValid(zeroDate));
		}

		[Test]
		public void TestProductShelfLifeIsUtcNowReturnsTrue()
		{
			var now = DateTime.UtcNow;
			Assert.True(_productValidator.ShelfLifeIsValid(now));
		}

		[TestCase(-4)]
		public void TestProductPriceBelowMinimumThrowsError(decimal price)
		{
			Assert.Throws<ProductPriceBelowMinimum>(() => _productValidator.PriceIsValid(price));
		}

		[TestCase(0)]
		[TestCase(64)]
		[TestCase(4.20)]
		public void TestProductPriceAboveMinimumReturnsTrue(decimal price)
		{
			Assert.True(_productValidator.PriceIsValid(price));
		}
	}
}

using Grocery.Core.Exceptions;
using Grocery.Core.Interfaces.Validators;
using Grocery.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Core.Validators
{
	public class ProductValidator : IProductValidator
	{
		const uint MAX_NAME_LENGTH = 80;
		const uint MIN_STOCK = 0;
		const uint MIN_PRICE = 0;

		public bool NameIsValid(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ProductNameIsEmpty();
			}

			if (name.Length > MAX_NAME_LENGTH)
			{
				throw new ProductNameExceedsAllowedLength();
			}

			return true;
		}

		public bool StockIsValid(int stock)
		{
			if (stock < MIN_STOCK)
			{
				throw new ProductStockBelowMinimum();
			}

			return true;
		}

		public bool ShelfLifeIsValid(DateTime shelfLife)
		{
			if (shelfLife == default)
			{
				throw new ProductShelfLifeIsZero();
			}

			return true;
		}

		public bool PriceIsValid(decimal price)
		{
			if (price < MIN_PRICE)
			{
				throw new ProductPriceBelowMinimum();
			}

			return true;
		}

		public bool Validate(Product? product)
		{
			if (product == null)
			{
				return false;
			}

			NameIsValid(product.Name);
			StockIsValid(product.Stock);
			ShelfLifeIsValid(product.ShelfLife);
			PriceIsValid(product.Price);
			
			return true;
		}
	}
}

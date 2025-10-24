using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Core.Exceptions
{
	public class ProductNameIsEmpty : Exception
	{
		public ProductNameIsEmpty() {}
	}

	public class ProductNameExceedsAllowedLength : Exception
	{
		public ProductNameExceedsAllowedLength() { }
	}

	public class ProductStockBelowMinimum : Exception
	{
		public ProductStockBelowMinimum() { }
	}

	public class ProductShelfLifeIsZero : Exception
	{
		public ProductShelfLifeIsZero() { }
	}

	public class ProductPriceBelowMinimum : Exception
	{
		public ProductPriceBelowMinimum() { }
	}
}

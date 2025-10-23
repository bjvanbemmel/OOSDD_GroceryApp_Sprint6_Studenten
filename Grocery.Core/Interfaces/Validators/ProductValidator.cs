using Grocery.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Core.Interfaces.Validators
{
	public interface IProductValidator
	{
		public bool NameIsValid(string name);
		public bool StockIsvalid(int stock);
		public bool ShelfLifeIsValid(DateTime shelfLife);
		public bool PriceIsValid(decimal price);
		public bool Validate(Product? product);
	}
}

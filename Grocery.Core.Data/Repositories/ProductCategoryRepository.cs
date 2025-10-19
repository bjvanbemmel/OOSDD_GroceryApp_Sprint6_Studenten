using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;
using Microsoft.Data.Sqlite;

namespace Grocery.Core.Data.Repositories
{
    public class ProductCategoryRepository : DatabaseConnection, IProductCategoryRepository
    {
        private readonly List<ProductCategory> productCategories = [];

        public ProductCategoryRepository()
        {
            CreateTable(@"CREATE TABLE IF NOT EXISTS ProductCategory (
                [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                [CategoryId] INTEGER NOT NULL,
                [ProductId] INTEGER NOT NULL,

                FOREIGN KEY ([CategoryId])
                    REFERENCES Category ([Id])
                FOREIGN KEY ([ProductId])
                    REFERENCES Product ([Id])

                UNIQUE([CategoryId], [ProductId])
            )");

            List<string> insertQueries = [
                @"INSERT OR IGNORE INTO ProdutCategory(CategoryId, ProductId) VALUES (3, 1)",    
                @"INSERT OR IGNORE INTO ProdutCategory(CategoryId, ProductId) VALUES (3, 2)",    
                @"INSERT OR IGNORE INTO ProdutCategory(CategoryId, ProductId) VALUES (2, 3)",    
                @"INSERT OR IGNORE INTO ProdutCategory(CategoryId, ProductId) VALUES (5, 4)",
			];

            InsertMultipleWithTransaction(insertQueries);
            GetAll();
        }

        public ProductCategory Add(ProductCategory item)
        {
			string insertQuery = $"INSERT INTO ProductCategory(CategoryId, ProductId) VALUES(@CategoryId, @ProductId) Returning RowId;";

			OpenConnection();
			using (SqliteCommand command = new(insertQuery, Connection))
			{
				command.Parameters.AddWithValue("CategoryId", item.CategoryId);
				command.Parameters.AddWithValue("ProductId", item.ProductId);

				item.Id = Convert.ToInt32(command.ExecuteScalar());
			}

			CloseConnection();
			return item;
        }

        public List<ProductCategory> GetAll()
        {
			productCategories.Clear();
			string selectQuery = "SELECT Id, GroceryListId, ProductId FROM ProductCategory";

			OpenConnection();
			using (SqliteCommand command = new(selectQuery, Connection))
			{
				SqliteDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					int id = reader.GetInt32(0);
					int categoryId= reader.GetInt32(1);
					int productId = reader.GetInt32(2);

					productCategories.Add(new(id, categoryId, productId));
				}
			}
			CloseConnection();
			return productCategories;
        }
    }
}

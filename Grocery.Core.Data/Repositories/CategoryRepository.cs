using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;
using Microsoft.Data.Sqlite;

namespace Grocery.Core.Data.Repositories
{
    public class CategoryRepository : DatabaseConnection, ICategoryRepository
    {
        private readonly List<Category> categories = [];
        public CategoryRepository()
        {
            CreateTable(@"CREATE TABLE IF NOT EXISTS Category (
                [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                [Name] NVARCHAR(40) NOT NULL UNIQUE
            )");

            List<string> insertQueries = [
                @"INSERT OR IGNORE INTO Category (Name) VALUES ('Groente')",
                @"INSERT OR IGNORE INTO Category (Name) VALUES ('Bakkerij')",
                @"INSERT OR IGNORE INTO Category (Name) VALUES ('Zuivel')",
                @"INSERT OR IGNORE INTO Category (Name) VALUES ('Conserven')",
                @"INSERT OR IGNORE INTO Category (Name) VALUES ('Ontbijt')",
			];

            InsertMultipleWithTransaction(insertQueries);
            GetAll();
        }

        public Category? Get(int id)
        {
			Category? category = null;
			string selectQuery = $"SELECT Id, Name FROM Category Where Id = {id}";

			OpenConnection();
			using (SqliteCommand command = new(selectQuery, Connection))
			{
				SqliteDataReader reader = command.ExecuteReader();

				if(reader.Read())
				{
					int _id = reader.GetInt32(0);
					string name = reader.GetString(1);
					category = new(id, name);
				}
			}

			CloseConnection();
			return category;
        }

        public List<Category> GetAll()
        {
			categories.Clear();
			string selectQuery = "SELECT Id, Name FROM Category";

			OpenConnection();
			using (SqliteCommand command = new(selectQuery, Connection))
			{
				SqliteDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					int id = reader.GetInt32(0);
					string name = reader.GetString(1);
					categories.Add(new(id, name));
				}
			}

			CloseConnection();
			return categories;
        }
    }
}

using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;
using Microsoft.Data.Sqlite;

namespace Grocery.Core.Data.Repositories
{
    public class ClientRepository : DatabaseConnection, IClientRepository
    {
        private readonly List<Client> clients = [];

        public ClientRepository()
        {
            CreateTable(@"CREATE TABLE IF NOT EXISTS Client (
                [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                [Name] NVARCHAR(80) NOT NULL,
                [Email] NVARCHAR(255) UNIQUE NOT NULL,
                [Password] NVARCHAR(255) NOT NULL,
                [Role] INTEGER NOT NULL DEFAULT 0
            )");

            List<string> insertQueries = [
				@"INSERT OR IGNORE INTO Client(Name, Email, Password, Role) VALUES ('A.J. Kwak', 'user3@mail.com', 'sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=', 1)",
				@"INSERT OR IGNORE INTO Client(Name, Email, Password, Role) VALUES ('M.J. Curie', 'user1@mail.com', 'IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=', 0)",
				@"INSERT OR IGNORE INTO Client(Name, Email, Password, Role) VALUES ('H.H. Hermans', 'user2@mail.com', 'dOk+X+wt+MA9uIniRGKDFg==.QLvy72hdG8nWj1FyL75KoKeu4DUgu5B/HAHqTD2UFLU=', 0)",
			];

            InsertMultipleWithTransaction(insertQueries);
            GetAll();
        }

        public Client? Get(string email)
        {
			string selectQuery = $"SELECT Id, Name, Email, Password, Role FROM Client WHERE Email = '{email}'";
            Client? client = null;

			OpenConnection();
			using (SqliteCommand command = new(selectQuery, Connection))
			{
				SqliteDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					int id = reader.GetInt32(0);
					string name = reader.GetString(1);
					string _email = reader.GetString(2);
                    string password = reader.GetString(3);
                    Role role = reader.GetInt32(4) switch
                    {
                        0 => Role.None,
                        1 => Role.Admin,
                        _ => throw new Exception("Undefined role stored in database"),
                    };

                    client = new(id, name, email, password, role);
				}
			}

			CloseConnection();
            return client;
        }

        public Client? Get(int id)
        {
			string selectQuery = $"SELECT Id, Name, Email, Password, Role FROM Client WHERE Id = {id}";
			Client? client = null;

			OpenConnection();
			using (SqliteCommand command = new(selectQuery, Connection))
			{
				SqliteDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					int _id = reader.GetInt32(0);
					string name = reader.GetString(1);
					string email = reader.GetString(2);
					string password = reader.GetString(3);
					Role role = reader.GetInt32(4) switch
					{
						0 => Role.None,
						1 => Role.Admin,
						_ => throw new Exception("Undefined role stored in database"),
					};

					client = new(id, name, email, password, role);
				}
			}

			CloseConnection();
			return client;
		}

        public List<Client> GetAll()
        {
			clients.Clear();
			string selectQuery = "SELECT Id, Name, Email, Password, Role FROM Client";

			OpenConnection();
			using (SqliteCommand command = new(selectQuery, Connection))
			{
				SqliteDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					int _id = reader.GetInt32(0);
					string name = reader.GetString(1);
					string email = reader.GetString(2);
					string password = reader.GetString(3);
					Role role = reader.GetInt32(4) switch
					{
						0 => Role.None,
						1 => Role.Admin,
						_ => throw new Exception("Undefined role stored in database"),
					};

					clients.Add(new(_id, name, email, password, role));
				}
			}

			CloseConnection();
			return clients;
        }
    }
}

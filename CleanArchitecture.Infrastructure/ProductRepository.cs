using CleanArchitecture.Application;
using CleanArchitecture.Domain;
using System.Data.SqlClient;

namespace CleanArchitecture.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;                
        }
        public async Task Add(Product product)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("INSERT INTO Products (Name, ProductCategory, Description, Price,SKU, Code) VALUES (@Name, @ProductCategory, @Description, @Price, @SKU, @Code)", connection);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@ProductCategory", product.ProductCategory);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@SKU", product.SKU);
                    command.Parameters.AddWithValue("@Code", product.Code);
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex) { 
              var message= ex.Message;
            }
        }

        public async Task Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Products WHERE ProductId= @ProductId", connection);
                command.Parameters.AddWithValue("@ProductId", id);
                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<Product> GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Products WHERE ProductId = @ProductId", connection);
                command.Parameters.AddWithValue("@ProductId", id);

                connection.Open();

                using (var reader =await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        return new Product
                        {
                            ProductId = (int)reader["ProductId"],
                            Name = (string)reader["Name"],
                            ProductCategory = (int)reader["ProductCategory"],
                            Description = (string)reader["Description"],
                            Price = (decimal)reader["Price"],
                            SKU = (string)reader["SKU"],
                            Code = (string)reader["Code"]
                        };
                    }
                    else {
                        return new Product();
                    }

                }

            }
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products= new List<Product>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Products", connection);
                connection.Open();

                using (var reader = await command.ExecuteReaderAsync()) { 
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductId = (int)reader["ProductId"],
                            Name = (string)reader["Name"],
                            ProductCategory = (int)reader["ProductCategory"],
                            Description = (string)reader["Description"],
                            Price = (decimal)reader["Price"],
                            SKU = (string)reader["SKU"],
                            Code = (string)reader["Code"]
                        });

                    }
                
                }

            }

            return products;
        }
        
        public async Task Update(Product product)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("Update Products SET Name=@Name, ProductCategory=@ProductCategory, Description=@Description, Price=@Price,SKU=@SKU, Code=@Code WHERE ProductId = @ProductId ", connection);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@ProductCategory", product.ProductCategory);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@SKU", product.SKU);
                    command.Parameters.AddWithValue("@Code", product.Code);
                    command.Parameters.AddWithValue("@ProductId", product.ProductId);
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex) { 
               var message= ex.Message;  
            }
        }
    }
}

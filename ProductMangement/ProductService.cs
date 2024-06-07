using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ProductMangement
{
    public class ProductService : IProductRepository
    {
        private string connectionString="Server=127.0.0.1;Database=prodb;User=root;Password=";
        public void Create(Product product)
        {
            // throw new NotImplementedException();
            using(MySqlConnection conn = new MySqlConnection(connectionString)){
                conn.Open();
                string query = "insert into products(name,price,description) values(@name,@price,@description)";
                MySqlCommand cmd = new MySqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@name",product.Name);
                cmd.Parameters.AddWithValue("@price",product.Price);
                cmd.Parameters.AddWithValue("@description",product.Description);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            // throw new NotImplementedException();
            List<Product> products = new List<Product>();
            //using thay cho try...catch
            using(MySqlConnection conn = new MySqlConnection(connectionString)){
                conn.Open();
                string query = "select * from products";
                MySqlCommand cmd = new MySqlCommand(query,conn);
                using(var read = cmd.ExecuteReader()){
                    while(read.Read()){
                        products.Add(new Product{
                            Id = read.GetInt32("id"),
                            Name = read.GetString("name"),
                            Price = read.GetDecimal("price"),
                            Description = read.GetString("description"),
                        });
                    }
                }
                return products;
            }
        }

        public Product Read(int id)
        {
            // throw new NotImplementedException();
            Product product = null;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM products WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        product = new Product
                        {
                            Id = read.GetInt32("id"),
                            Name = read.GetString("name"),
                            Price = read.GetDecimal("price"),
                            Description = read.GetString("description")
                        };
                    }
                }
            }
            return product;
        }

        public void Update(Product product)
        {
            // throw new NotImplementedException();
             using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE products SET name = @name, price = @price, description = @description WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@id", product.Id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
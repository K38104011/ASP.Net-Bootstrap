using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    public class ADOProductRepository : IProductRepository
    {
        public IQueryable<Product> Products
        {
            get {
                var dataTable = new DataTable();

                using (var connection = DbConnection.GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT * FROM Products", connection))
                    {
                        dataTable.Load(command.ExecuteReader());
                    }
                }

                var productList = dataTable.AsEnumerable().
                    Select(p => new Product { 
                        ProductID = p.Field<int>("ProductID"),
                        Name = p.Field<string>("Name"),
                        Description = p.Field<string>("Description"),
                        Category = p.Field<string>("Category"),
                        Price = p.Field<decimal>("Price"),
                        ImageData = p.Field<byte[]>("ImageData"),
                        ImageMimeType = p.Field<string>("ImageMimeType")
                    });

                return productList.AsQueryable<Product>();
            }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                using (var connection = DbConnection.GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand(
                        "INSERT INTO Products (Name, Description, Category, Price, ImageData, ImageMimeType) " +
                        "VALUES (@Name, @Description, @Category, @Price, @ImageData, @ImageMimeType)", connection))
                    {
                        command.Parameters.AddRange(new SqlParameter[] {
                            new SqlParameter("Name", product.Name),
                            new SqlParameter("Description", product.Description),
                            new SqlParameter("Category", product.Category),
                            new SqlParameter("Price", product.Price),
                            new SqlParameter("ImageData", SqlDbType.VarBinary, -1),
                            new SqlParameter("ImageMimeType", (object)product.ImageMimeType ?? DBNull.Value)
                        });

                        command.Parameters["ImageData"].Value = (object)product.ImageData ?? DBNull.Value;

                        command.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                Product dbEntry = this.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
                if (dbEntry != null)
                {
                    using (var connection = DbConnection.GetConnection())
                    {
                        connection.Open();
                        using (var command = new SqlCommand("UPDATE Products SET Name = @Name, Description = @Description, " +
                            "Category = @Category, Price = @Price, ImageData = @ImageData, ImageMimeType = @ImageMimeType " + 
                            "WHERE ProductID = @ProductID", connection))
                        {
                            command.Parameters.AddRange(new SqlParameter[] {
                                new SqlParameter("ProductID", dbEntry.ProductID),
                                new SqlParameter("Name", product.Name),
                                new SqlParameter("Description", product.Description),
                                new SqlParameter("Category", product.Category),
                                new SqlParameter("Price", product.Price),
                                new SqlParameter("ImageData", SqlDbType.VarBinary, -1),
                                new SqlParameter("ImageMimeType", (object)product.ImageMimeType ?? DBNull.Value)
                            });

                            command.Parameters["ImageData"].Value = (object)product.ImageData ?? DBNull.Value;

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public Product DeleteProduct(int productId)
        {
            Product dbEntry = this.Products.FirstOrDefault(p => p.ProductID == productId);

            if (dbEntry != null)
            {
                using (var connection = DbConnection.GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE Products WHERE ProductID = @ProductID", connection))
                    {
                        command.Parameters.Add(new SqlParameter("ProductID", productId));
                        command.ExecuteNonQuery();
                    }
                }
            }
            
            return dbEntry;
        }
    }
}

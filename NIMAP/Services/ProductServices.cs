using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using NIMAP.Models;
using System.Data;

namespace NIMAP.Services
{
    public class ProductServices : IProductServices
    {
        string cs;


        public ProductServices()
        {
            this.cs = "Data source=(LocalDb)\\MSSQLLocalDB;Database=TestDB;Integrated security=true";

        }

        public async Task<int> AddCategory(int NewCategoryId, string categoryName)
        {
            try
            {

                using (var connection = new SqlConnection(this.cs))
                {
                    using (var command = new SqlCommand("AddCategory", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        command.Parameters.AddWithValue("@NewCategoryId", NewCategoryId);
                        command.Parameters.AddWithValue("@CategoryName", categoryName);

                        await connection.OpenAsync();

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public async Task<int> AddProduct(Product product)
        {
            try
            {

                using (var connection = new SqlConnection(this.cs))
                {
                    using (var command = new SqlCommand("AddProduct", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        command.Parameters.AddWithValue("@ProductId", product.ProductId);
                        command.Parameters.AddWithValue("@ProductName", product.ProductName);
                        command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public DataSet GetAllProducts(int current_page, int pageSize)
        {
            try
            {
                var dataSet = new DataSet();

                using (var connection = new SqlConnection(this.cs))
                {
                    using (var command = new SqlCommand("GetAllProducts", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PageNo", current_page);
                        command.Parameters.AddWithValue("@PageSize", pageSize);

                        var adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet); // Fills the DataSet with the result of the stored procedure
                    }
                }

                return dataSet;

            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public async Task<List<DropDownSource>> GetCategoryDropDown()
        {
            try
            {
                var Categories = new List<DropDownSource>();

                using (var connection = new SqlConnection(this.cs))
                {
                    using (var command = new SqlCommand("GetCategoryDropDown", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync(); // Open the connection asynchronously

                        using (var reader = await command.ExecuteReaderAsync()) // Execute query and get SqlDataReader
                        {
                            while (await reader.ReadAsync()) // Read each row asynchronously
                            {
                                var Category = new DropDownSource
                                {
                                    Text = reader.GetString(reader.GetOrdinal("Text")),
                                    Value = reader.GetString(reader.GetOrdinal("Value")),

                                };
                                Categories.Add(Category); // Add each product to the list
                            }
                        }
                    }
                }
                return Categories;
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }



        public async Task<bool> DeleteProducts(int productId)
        {
            try
            {

                using (var connection = new SqlConnection(this.cs))
                {
                    using (var command = new SqlCommand("DeleteProducts", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        command.Parameters.AddWithValue("@ProductId", productId);

                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public async Task<Product> GetProductDetails(int productId)
        {
            try
            {
                Product product = new Product();

                using (var connection = new SqlConnection(this.cs))
                {
                    using (var command = new SqlCommand("GetProductDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductId", productId);

                        await connection.OpenAsync(); // Open the connection asynchronously

                        using (var reader = await command.ExecuteReaderAsync()) // Execute query and get SqlDataReader
                        {
                            while (await reader.ReadAsync()) // Read each row asynchronously
                            {

                                product.ProductId = reader.GetInt32(reader.GetOrdinal("ProductId"));
                                product.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                                product.CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));


                            }
                        }
                    }
                }
                return product;
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public DataSet GetCategoryList()
        {
            try
            {
                var dataSet = new DataSet();

                using (var connection = new SqlConnection(this.cs))
                {
                    using (var command = new SqlCommand("GetCategoryList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //command.Parameters.AddWithValue("@CategoryId", categoryId);

                        var adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataSet); // Fills the DataSet with the result of the stored procedure
                    }
                }

                return dataSet;

            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public async Task<Category> EditCategory(int categoryId)
        {
            try
            {
                Category Category = new Category();

                using (var connection = new SqlConnection(this.cs))
                {
                    using (var command = new SqlCommand("EditCategory", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CategoryId", categoryId);

                        await connection.OpenAsync(); // Open the connection asynchronously

                        using (var reader = await command.ExecuteReaderAsync()) // Execute query and get SqlDataReader
                        {
                            while (await reader.ReadAsync()) // Read each row asynchronously
                            {

                                Category.CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                                Category.CategoryName = reader.GetString(reader.GetOrdinal("CategoryName"));


                            }
                        }
                    }
                }
                return Category;
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public async Task<bool> DeleteCategory(int CategoryId)
        {
            try
            {

                using (var connection = new SqlConnection(this.cs))
                {
                    using (var command = new SqlCommand("DeleteCategory", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        command.Parameters.AddWithValue("@CategoryId", CategoryId);

                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public async Task<bool> CheckCategoryUsed(int CategoryId)
        {
            try
            {
                int CategoryUsed = 0;
                using (var connection = new SqlConnection(this.cs))
                {
                    using (var command = new SqlCommand("CheckCategoryUsed", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        command.Parameters.AddWithValue("@CategoryId", CategoryId);

                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync()) // Execute query and get SqlDataReader
                        {
                            while (await reader.ReadAsync()) // Read each row asynchronously
                            {

                                CategoryUsed = reader.GetInt32(reader.GetOrdinal("Cnt"));

                            }
                        }
                    }
                }
                return CategoryUsed > 0;
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
    }
}

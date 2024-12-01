using NIMAP.Models;
using System.Data;

namespace NIMAP.Services
{
    public interface IProductServices
    {
        Task<int> AddCategory(int newCategoryId, string categoryName);
        Task<int> AddProduct(Product product);
        DataSet GetAllProducts(int current_page, int pageSize);
        Task<List<DropDownSource>> GetCategoryDropDown();
        Task<bool> DeleteProducts(int productId);
        Task<Product> GetProductDetails(int productId);
        DataSet GetCategoryList();
        Task<Category> EditCategory(int categoryId);
        Task<bool> DeleteCategory(int categoryId);
        Task<bool> CheckCategoryUsed(int categoryId);
    }
}

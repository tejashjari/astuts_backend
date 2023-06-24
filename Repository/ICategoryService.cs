using astute.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial interface ICategoryService
    {
        Task<int> InsertCategory(Category_Master category_Master);
        Task<int> UpdateCategory(Category_Master category_Master);
        Task<int> DeleteCategory(int id);
        Task<Category_Master> GetCategoryById(int catId);
        Task<IList<Category_Master>> GetCategoriesByColId(int colId);

        #region Category Value
        Task<int> InsertCategoryValue(Category_Value category_Value);
        Task<int> UpdateCategoryValue(Category_Value category_Value);
        Task<int> DeleteCategoryValue(int id);
        Task<Category_Value> GetCategoryValueByCatValId(int catValId);
        Task<IList<CategoryValueModel>> GetCategoryValuesByCatId(int catId);
        #endregion
    }
}

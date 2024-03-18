using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTwebAPI.Models;

namespace RESTwebAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly List<Category> _categories;

        public CategoryService()
        {
            _categories = new List<Category>
{
    new Category { CategoryId = 1, CategoryName = "Електроніка" },
    new Category { CategoryId = 2, CategoryName = "Комп'ютери та ноутбуки" },
    new Category { CategoryId = 3, CategoryName = "Побутова техніка" },
    new Category { CategoryId = 4, CategoryName = "Смартфони та аксесуари" },
    new Category { CategoryId = 5, CategoryName = "Телевізори та аудіотехніка" },
    new Category { CategoryId = 6, CategoryName = "Ігрові консолі та відеоігри" },
    new Category { CategoryId = 7, CategoryName = "Побутова хімія та прибирання" },
    new Category { CategoryId = 8, CategoryName = "Меблі та предмети декору" },
    new Category { CategoryId = 9, CategoryName = "Спортивні товари та активний відпочинок" },
    new Category { CategoryId = 10, CategoryName = "Книги та навчальні матеріали" },
};

        }

        public async Task<ResponseModel<Category>> AddCategoryAsync(Category category)
        {
            category.CategoryId = _categories.Any() ? _categories.Max(c => c.CategoryId) + 1 : 1;
            _categories.Add(category);
            return new ResponseModel<Category>(category, true, "Category added successfully.");
        }

        public async Task<ResponseModel<Category>> DeleteCategoryAsync(int id)
        {
            var category = _categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
                return new ResponseModel<Category>(null, false, $"Category with id {id} not found.");

            _categories.Remove(category);
            return new ResponseModel<Category>(category, true, $"Category with id {id} deleted successfully.");
        }

        public async Task<ResponseModel<IEnumerable<Category>>> GetAllCategoriesAsync()
        {
            return new ResponseModel<IEnumerable<Category>>(_categories, true, "Successfully retrieved all categories.");
        }

        public async Task<ResponseModel<Category>> GetCategoryAsync(int id)
        {
            var category = _categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
                return new ResponseModel<Category>(null, false, $"Category with id {id} not found.");

            return new ResponseModel<Category>(category, true, $"Successfully retrieved category with id {id}.");
        }

        public async Task<ResponseModel<Category>> UpdateCategoryAsync(int id, Category categoryUpdate)
        {
            var category = _categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
                return new ResponseModel<Category>(null, false, $"Category with id {id} not found.");

            category.CategoryName = categoryUpdate.CategoryName;
            return new ResponseModel<Category>(category, true, $"Category with id {id} updated successfully.");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SnacksUdemy.Repository;

namespace SnacksUdemy.Components
{
    public class CategoryMenu: ViewComponent
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var category = _categoryRepository.Categories.OrderBy(
                p => p.CategoryName);
            return View(category);
        }
    }
}

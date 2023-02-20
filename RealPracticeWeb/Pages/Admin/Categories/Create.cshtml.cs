using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracticeWithVideo.DataAccess.Data;
using RealPractice.Models;
using RealProject.DataAccess.Repository.IRepository;

namespace PracticeWithVideo.Pages.Admin.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
        
    {
        private readonly IUnitOfWork _unitOfWork;
        public Category Category { get; set; }
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPost() //agar balaye  "public Category category { get; set; }" az [BindProperty] estefade konim dg niaz nis in vorodi dashte bashe
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "The DisplayOrder cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                 _unitOfWork.Category.Add(Category);
                 _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}

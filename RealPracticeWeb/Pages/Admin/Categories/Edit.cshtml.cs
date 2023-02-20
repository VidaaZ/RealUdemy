using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracticeWithVideo.DataAccess.Data;
using RealPractice.Models;
using RealProject.DataAccess.Repository.IRepository;

namespace PracticeWithVideo.Pages.Admin.Categories
{
    [BindProperties]
    public class EditModel : PageModel
        
    {
        private readonly IUnitOfWork _unitOfWork;
        public Category Category { get; set; }
        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       

        
        public void OnGet(int id)
        {
            Category = _unitOfWork.Category.GetFirstOrDefault(u=>u.Id==id);
        } 
        public async Task<IActionResult> OnPost() //agar balaye  "public Category category { get; set; }" az [BindProperty] estefade konim dg niaz nis in vorodi dashte bashe
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "The DisplayOrder cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
               _unitOfWork.Category.Update(Category);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}

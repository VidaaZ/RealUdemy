using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracticeWithVideo.DataAccess.Data;
using RealPractice.Models;
using RealProject.DataAccess.Repository.IRepository;

namespace PracticeWithVideo.Pages.Admin.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel

    {
        private readonly IUnitOfWork _unitOfWork;
        public Category Category { get; set; }
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        


        public void OnGet(int id)
        {
            Category = _unitOfWork.Category.GetFirstOrDefault(u=>u.Id==id);
        }
        public async Task<IActionResult> OnPost() //agar balaye  "public Category category { get; set; }" az [BindProperty] estefade konim dg niaz nis in vorodi dashte bashe
        {


            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u=>u.Id==Category.Id);
            if (categoryFromDb != null)
            {
                _unitOfWork.Category.Remove(categoryFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Category deleted successfully";
                return RedirectToPage("Index");
            }




            return Page();
        }
    }
}

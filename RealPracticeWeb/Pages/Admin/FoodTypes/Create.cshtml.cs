using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracticeWithVideo.DataAccess.Data;
using RealPractice.Models;
using RealProject.DataAccess.Repository;
using RealProject.DataAccess.Repository.IRepository;

namespace PracticeWithVideo.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class CreateModel : PageModel
        
    {
        private readonly IUnitOfWork _unitOfWork;
        public FoodType FoodType { get; set; }
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPost() //agar balaye  "public Category category { get; set; }" az [BindProperty] estefade konim dg niaz nis in vorodi dashte bashe
        {
           
            if (ModelState.IsValid)
            {
                 _unitOfWork.FoodType.Add(FoodType);
                 _unitOfWork.Save();
                TempData["success"] = "FoodType created successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}

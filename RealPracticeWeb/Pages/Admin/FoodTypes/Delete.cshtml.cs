using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracticeWithVideo.DataAccess.Data;
using RealPractice.Models;
using RealProject.DataAccess.Repository.IRepository;

namespace PracticeWithVideo.Pages.Admin.FoodTypes;

[BindProperties]
public class DeleteModel : PageModel

{
    private readonly IUnitOfWork _unitOfWork;
    public FoodType FoodType { get; set; }
    public DeleteModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    


    public void OnGet(int id)
    {
        FoodType = _unitOfWork.FoodType.GetFirstOrDefault(u=>u.Id==id);
    }
    public async Task<IActionResult> OnPost() //agar balaye  "public Category category { get; set; }" az [BindProperty] estefade konim dg niaz nis in vorodi dashte bashe
    {


        var foodTypeFromDb = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == FoodType.Id);
        if (foodTypeFromDb != null)
        {
            _unitOfWork.FoodType.Remove(foodTypeFromDb);
            _unitOfWork.Save(); 
            TempData["success"] = "FoodType deleted successfully";
            return RedirectToPage("Index");
        }




        return Page();
    }
}

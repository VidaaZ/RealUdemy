 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PracticeWithVideo.DataAccess.Data;
using RealPractice.Models;
using RealProject.DataAccess.Repository.IRepository;
using System.Diagnostics.CodeAnalysis;

namespace PracticeWithVideo.Pages.Admin.FoodTypes;
[BindProperties]
public class IndexModel : PageModel
{
    private readonly IUnitOfWork _unitOfWork;
   public IEnumerable<FoodType> FoodTypes { get; set; }
    public IndexModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }



    public void OnGet()
    {
        //if (Categories != null)
        // Categories = await _db.Category.ToListAsync(); 
        FoodTypes = _unitOfWork.FoodType.GetAll();
    }

}
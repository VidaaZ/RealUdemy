 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PracticeWithVideo.DataAccess.Data;
using RealPractice.Models;
using RealProject.DataAccess.Repository.IRepository;
using System.Diagnostics.CodeAnalysis;

namespace PracticeWithVideo.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IEnumerable<Category> Categories { get; set; } //chera IEnumebrable??
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            //if (Categories != null)
            // Categories = await _db.Category.ToListAsync(); 
            Categories = _unitOfWork.Category.GetAll();//this retrieve all list of categories
        }
    }
}

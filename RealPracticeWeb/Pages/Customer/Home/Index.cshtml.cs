using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealPractice.Models;
using RealProject.DataAccess.Repository.IRepository;

namespace PracticeWithVideo.Pages.Customer.Home
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //we want a list of all menuItems 
        public IEnumerable<MenuItem> MenuItemList { get; set; } //<MenuItem esme modele ke baraye entity MenuItem barname neveshtim,hala niaz darim ke in list ke sakhtim va esmesh MenuItem ro ba komake unitowork populate konim.
        
        public IEnumerable<Category> CategoryList { get; set; }
        
        public void OnGet()
        { //populate the above list with the help of _unitOfWork
            MenuItemList = _unitOfWork.MenuItem.GetAll(includeProperties: "Category,FoodType"); //listi ke be name MenuItemList dar bala sakhtim ro ba komake unitOfWOrk populate kardim va ma mikham category va foodtype ham shamel in populate shodan bashan ke intori az includeproperties estedade kardim
            //menuItemList was populated in GetHandler by the above line code
            CategoryList = _unitOfWork.Category.GetAll(orderby:u=>u.OrderBy(c=>c.DisplayOrder));
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealPractice.Models;
using RealPractice.Utility;
using RealProject.DataAccess.Repository.IRepository;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PracticeWithVideo.Pages.Customer.Home
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWOrk;
        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWOrk = unitOfWork;
        }
        //public MenuItem MenuItem { get; set; }
        //[Range(1,100,ErrorMessage ="Please select a count between 1 and 100")]
        //public int Count { get; set; }
        //chon shopping cart in 2ta proprty bala dare pas ono inja miarim ina dg pak mishe
        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }
        public void OnGet(int id)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCart = new()
            {
                ApplicationUserId = claim.Value,
                MenuItem = _unitOfWOrk.MenuItem.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,FoodType"),
                 MenuItemId = id //in hamon id ke dakhele OnGet receive mishe mirizim to menuItemId chon dar gheyr insorat man vaghti to Ui page besorate hidden tarif kardim age meghdar nadim null barmigardone
            };
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)

            {
                ShoppingCart shoppingcartFromDb = _unitOfWOrk.ShoppingCart.GetFirstOrDefault(
                  filter:  u => u.ApplicationUserId == ShoppingCart.ApplicationUserId &&
                    u.MenuItemId == ShoppingCart.MenuItemId);
                if (shoppingcartFromDb == null)
                {
                    _unitOfWOrk.ShoppingCart.Add(ShoppingCart);
                    _unitOfWOrk.Save();
                    HttpContext.Session.SetInt32(SD.SessionCart,
                        _unitOfWOrk.ShoppingCart.GetAll(u => u.ApplicationUserId == ShoppingCart.ApplicationUserId).ToList().Count);
                }
                else
                {
                    _unitOfWOrk.ShoppingCart.IncrementCount(shoppingcartFromDb, ShoppingCart.Count);
                }
                return RedirectToPage("Index");
            }
            return Page();
           
           
        }
    }
}


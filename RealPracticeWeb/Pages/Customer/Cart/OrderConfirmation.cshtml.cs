using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealPractice.Models;
using RealPractice.Utility;
using RealProject.DataAccess.Repository.IRepository;
using Stripe.Checkout;
namespace PracticeWithVideo.Pages.Customer.Cart
{
    public class OrderConfirmationModel : PageModel
    {


        private readonly IUnitOfWork _unitOfWork;
        public int OrderId { get; set; }
        public OrderConfirmationModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }
        public void OnGet(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id);
            if (orderHeader.SessionId != null)
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);
                if (session.PaymentStatus.ToLower()=="paid")
                {
                    orderHeader.Status = SD.StatusSubmitted;
                    orderHeader.PaymentIntentId = session.PaymentIntentId;
                    _unitOfWork.Save();
                    
                }
            }
            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == orderHeader.UserId).ToList();
            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            _unitOfWork.Save();
            OrderId = id;
        }
    }
}
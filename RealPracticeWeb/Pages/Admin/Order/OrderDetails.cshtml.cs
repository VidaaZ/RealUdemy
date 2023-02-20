using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealPractice.Models;
using RealPractice.Models.ViewModel;
using RealPractice.Utility;
using RealProject.DataAccess.Repository.IRepository;
using Stripe;


namespace PracticeWithVideo.Pages.Admin.Order
{
    public class OrderDetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderDetailVM OrderDetailVM { get; set; }
        public OrderDetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        

        
        public void OnGet(int id)
        {
            //populating OrderDetailVM
            OrderDetailVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "ApplicationUser"),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == id).ToList()
            };
        }
        public IActionResult OnPostOrderCompleted(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStaus(orderId, SD.StatusCompleted);
            _unitOfWork.Save();
            return RedirectToPage("OrderList");
        }
        public IActionResult OnPostOrderRefund(int orderId)

        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(o => o.Id == orderId);
            var options = new RefundCreateOptions
            {
                Reason = RefundReasons.RequestedByCustomer,
                PaymentIntent = orderHeader.PaymentIntentId
            };
            var service = new RefundService();
            Refund refund = service.Create(options);
            _unitOfWork.OrderHeader.UpdateStaus(orderId, SD.StatusRefunded);
            _unitOfWork.Save();
            return RedirectToPage("OrderList");
        }
        public IActionResult OnPostOrderCancell(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStaus(orderId, SD.StatusCancelled);
            _unitOfWork.Save();
            return RedirectToPage("OrderList");
        }

    }
}

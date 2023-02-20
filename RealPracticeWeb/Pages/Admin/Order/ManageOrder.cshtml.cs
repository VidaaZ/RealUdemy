using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealPractice.Models;
using RealPractice.Models.ViewModel;
using RealPractice.Utility;
using RealProject.DataAccess.Repository;
using RealProject.DataAccess.Repository.IRepository;

namespace PracticeWithVideo.Pages.Admin.Order
{
    [Authorize(Roles = $"{SD.ManagerRole},{SD.KitchenRole}")]
    public class ManageOrderModel : PageModel
    {
        
        private readonly IUnitOfWork _uniOfWork; //baraye ink betonim status ya har detail avaz konim
        public List<OrderDetailVM> OrderDetailVM { get; set; } //a properrty which retriev list of OrderDetailVM model ke dakhelesh data orderheader o orderdetails has
        public ManageOrderModel(IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;
        }
        public void OnGet()
        {
            //populate this orderDetailVM inside Get Handler
            
            OrderDetailVM = new();
            List<OrderHeader> orderHeaders = _uniOfWork.OrderHeader.GetAll(u => u.Status ==SD.StatusSubmitted || u.Status == SD.StatusInProcess).ToList();//inside ordeheader we retrieve all order wit submitted or inprocess status
             //we should retrieve all the orderHeaders and then needs a foreach loop to populate all the order details.we can use navigation property for a list and do one too many mapping.
                //we need to popuate list of OrderHeader first
                
            foreach(OrderHeader item in orderHeaders)
            {  //we want to load orderdetail for  each items in orderHeader
                OrderDetailVM individual = new OrderDetailVM()
                {
                    OrderHeader = item,//populate OrderHeader with item that we have already had
                    OrderDetails = _uniOfWork.OrderDetails.GetAll(u => u.OrderId == item.Id).ToList()
                };
                OrderDetailVM.Add(individual);
            }
       
        }
        //chon ke to view in page ma 3ta button az noe submit darim pas inja bayad 3ta page handler barye OnPost besazim
        public IActionResult OnPostOrderInProcess(int orderId)
        {
            _uniOfWork.OrderHeader.UpdateStaus(orderId, SD.StatusInProcess);
            _uniOfWork.Save();
            return RedirectToPage("ManageOrder");
        }
        public IActionResult OnPostOrderReady(int orderId)
        {
            _uniOfWork.OrderHeader.UpdateStaus(orderId, SD.StatusReady);
            _uniOfWork.Save();
            return RedirectToPage("ManageOrder");
        }
        public IActionResult OnPostOrderCancell(int orderId)
        {
            _uniOfWork.OrderHeader.UpdateStaus(orderId, SD.StatusCancelled);
            _uniOfWork.Save();
            return RedirectToPage("ManageOrder");
        }



    }
}

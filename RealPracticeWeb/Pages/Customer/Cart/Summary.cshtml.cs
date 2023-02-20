using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealPractice.Models;
using RealPractice.Utility;
using RealProject.DataAccess.Repository.IRepository;
using Stripe.Checkout;
using System.Security.Claims;





namespace PracticeWithVideo.Pages.Customer.Cart

{

    [Authorize] //in attribute neshon mide ke in safe faghat baraye userhaye authorized ghabele dasresi has
    [BindProperties]
    public class SummaryModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }

        public OrderHeader OrderHeader { get; set; }

        private readonly IUnitOfWork _unitOfWork;
        public SummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            OrderHeader = new OrderHeader();

        }
        public void OnGet()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity; //in id useri le logged in shode migire
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                    includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");
                foreach (var cartItem in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
                }
                ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
                OrderHeader.PickupName = applicationUser.FirstName + " " + applicationUser.LastName;
                OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
            }

        }
        public IActionResult OnPost()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value, includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");
                foreach (var cartItem in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
                }

                OrderHeader.Status = SD.StatusPending;
                OrderHeader.OrderDate = System.DateTime.Now;
                OrderHeader.UserId = claim.Value;
                OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString() + " " +
                    OrderHeader.PickUpTime.ToShortTimeString());
                _unitOfWork.OrderHeader.Add(OrderHeader);
                _unitOfWork.Save();


                foreach (var item in ShoppingCartList)
                {
                    OrderDetails orderDetails = new()  //creating OrderDetails obj
                    {  //These are the details that we want for orderdetails
                        MenuItemId = item.MenuItemId, //the detail that we are populated it
                        OrderId = OrderHeader.Id,
                        Name = item.MenuItem.Name,
                        Price = item.MenuItem.Price,
                        Count = item.Count
                    };
                    _unitOfWork.OrderDetails.Add(orderDetails);


                }
              
               /* _unitOfWork.ShoppingCart.RemoveRange(ShoppingCartList);*/ //remove shopping cart list after saving
                _unitOfWork.Save();

                //az inja ta akhar shabihe calling API
                var domain = "https://localhost:44332/"; //local domain url ke barname run miknm tp browser neshon mide copy paste mikonim inja
                var options = new SessionCreateOptions  //inja ke ye SessionCreateOptions ijad kardim baraye checkout toye payment
                {
                    LineItems = new List<SessionLineItemOptions>()



                ,
                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                    Mode = "payment",
                    SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={OrderHeader.Id}",
                    CancelUrl = domain + "customer/cart/index",

                };

                //add line items
                foreach (var item in ShoppingCartList) {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.MenuItem.Price * 100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.MenuItem.Name

                            },
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);

                }
                var service = new SessionService();
                Session session = service.Create(options);

                Response.Headers.Add("Location", session.Url);
                OrderHeader.SessionId = session.Id;
                OrderHeader.PaymentIntentId = session.PaymentIntentId;
                _unitOfWork.Save();
               
                return new StatusCodeResult(303);


            }
            return Page();

        }
    } }

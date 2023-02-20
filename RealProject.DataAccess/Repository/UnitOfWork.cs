using PracticeWithVideo.DataAccess.Data;
using RealProject.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealProject.DataAccess.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;  //chon toye UnitOfWOrk method save ke ba dbcontext kar mikone neveshtim pas bayad ba dependency injection dbcontect inject konim.
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            Category = new CategoryRepository(_applicationDbContext);
            FoodType = new FoodTypeRepository(_applicationDbContext);
            MenuItem = new MenuItemRepository(_applicationDbContext);
            ShoppingCart = new ShoppingCartRepository(_applicationDbContext);
            OrderHeader = new OrderHeaderRepository(_applicationDbContext);
            OrderDetails = new OrderDetailRepository(_applicationDbContext);
            ApplicationUser = new ApplicationUserRepository(_applicationDbContext);
        }
        public ICategoryRepository Category{get;private set;}
        public IFoodTypeRepository FoodType { get; private set; }
        public IMenuItemRepository MenuItem { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public  IOrderHeaderRepository OrderHeader { get; private set; }
        public  IOrderDetailRepository OrderDetails { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }

        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
        
        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}

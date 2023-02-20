using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealProject.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository Category { get; } //we want to access our categoryrepository using UnitOfWork repository so we define it as get only
        IFoodTypeRepository FoodType { get; }
        IMenuItemRepository MenuItem { get; }
        IShoppingCartRepository ShoppingCart { get;  }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetails { get; }
        IApplicationUserRepository ApplicationUser { get; }
        void Save();
    }
}

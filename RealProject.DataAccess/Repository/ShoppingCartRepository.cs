using Microsoft.EntityFrameworkCore;
using PracticeWithVideo.DataAccess.Data;
using RealPractice.Models;
using RealProject.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealProject.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
    
        public ShoppingCartRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
         
        }

        public int IDecrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count -= count;
            _db.SaveChanges();
            return shoppingCart.Count;
        }
        
        public int IncrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            _db.SaveChanges();

            return shoppingCart.Count;
        }
    }
    }


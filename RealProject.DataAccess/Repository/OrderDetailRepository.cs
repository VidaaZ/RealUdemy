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
    public class OrderDetailRepository : Repository<OrderDetails>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;
    
        public OrderDetailRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
         
        }

     

        public void Update(OrderDetails obj)
        {
            _db.OrderDetails.Update(obj);
            
        }
    }
}

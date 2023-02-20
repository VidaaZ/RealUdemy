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
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;
    
        public OrderHeaderRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
         
        }

     

        public void Update(OrderHeader obj)
        {
            _db.OrderHeader.Update(obj); //it modify all properties in OrderHeader bar asase id va update mikone automatically

            
            
        }

        public void UpdateStaus(int id, string status)
        {
            var orderFromDb = _db.OrderHeader.FirstOrDefault(u => u.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.Status = status;
            }
        }
    }
}

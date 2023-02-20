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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
    
        public CategoryRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
         
        }

     

        public void Update(Category category)
        {
            var objFromDb = _db.Category.FirstOrDefault(u => u.Id == category.Id);
            objFromDb.Name = category.Name;
            objFromDb.DisplayOrder = category.DisplayOrder; 
            
        }
    }
}

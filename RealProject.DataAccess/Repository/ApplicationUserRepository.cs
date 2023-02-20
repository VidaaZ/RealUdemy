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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
    
        public ApplicationUserRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
         
        }

     

        
    }
}

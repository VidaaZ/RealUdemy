using RealPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealProject.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository:IRepository<OrderHeader>
    {
        void Update(OrderHeader obj); //in raveshe update zamani estefade mishe bejate firanddefault ke bekhay hame property haro update koni
        void UpdateStaus(int id, string status);
        
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPractice.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }  //this irderHeaderId ke ba estefade az on beyne in 2ta jadvale(OrderHeader va OrderDetails) map konid
        [ForeignKey("OrderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }
        [Required]
        public int  MenuItemId{ get; set; }
        [ForeignKey("MenuItemId")]
        public virtual MenuItem MenuItem { get; set; } 
        public int Count { get; set; }
        [Required]
        public double Price { get; set; }
        public string Name { get; set; }

        //*note:farz konid shoma har zaman ke bekhayd name menuitem namayesh bedid bayad az ye navigation property estefade konid va name menuitem ra az inja migirid,vali chizi ke masalan vas in ijad concern mikone gheymate on item mas ,masalan farz konid zamani ke 
        //user order mizane gheymat $10 va hamon zaman gheymato shoma be 12$ taghir midid age bekhayd ke price be halate navigation property ke be menuitem vabastas begirid in update sorat nmigire pas miayd name va price be sorate jodagane to orderdetails barash property tarif mikonid,natije migitim
        //ke masalan price o name ke yekam critical va emakn niaz be taghir darano nmiaym be menuItem ke ye navigation property begirim va be sorate mostaghel to hamin model barashon property tarif mikonim.
        
    }
}

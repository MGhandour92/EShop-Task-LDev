using DAL.Entities;
using DAL.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public class OrderLineRepo : GenericRepo<OrderLine>, IOrderLineRepo
    {
        public OrderLineRepo(AppDBContext context) : base(context)
        {
        }
    }
}

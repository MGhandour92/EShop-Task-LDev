using DAL.Entities;
using DAL.Interfaces;
using DAL.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class OrderHeaderRepo : GenericRepo<OrderHeader>, IOrderHeaderRepo
    {
        public OrderHeaderRepo(AppDBContext context) : base(context)
        {
        }
    }
}

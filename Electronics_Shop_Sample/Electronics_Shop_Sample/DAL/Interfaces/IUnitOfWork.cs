using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Main
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepo CategoryRepo { get; }
        IProductRepo ProductRepo { get; }
        IOrderHeaderRepo OrderHeaderRepo { get; }
        IOrderLineRepo OrderLineRepo { get; }

        int Complete();
    }
}

using DAL.Repos;
using DAL.Interfaces;
using DAL.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Main
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;

        public ICategoryRepo CategoryRepo { get; private set; }
        public IProductRepo ProductRepo { get; private set; }
        public IOrderHeaderRepo OrderHeaderRepo { get; private set; }
        public IOrderLineRepo OrderLineRepo { get; private set; }


        public UnitOfWork(AppDBContext context)
        {
            _context = context;
            CategoryRepo = new CategoryRepo(_context);
            ProductRepo = new ProductRepo(_context);
            OrderHeaderRepo = new OrderHeaderRepo(_context);
            OrderLineRepo = new OrderLineRepo(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

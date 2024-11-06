using BanSach.DataAcess.Data;
using BanSach.DataAcess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.DataAcess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }

        public ICoverTypeRePository coverType { get; private set; }

        public IProductRepository Product { get; private set; }





        //tạo 1 biến 
        private readonly ApplicationDbContext _db;


        //hàm khởi tạo
        public UnitOfWork(ApplicationDbContext db) 
        { 
            _db = db;
            Category = new CategoryRepository(_db);
            coverType = new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);

        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

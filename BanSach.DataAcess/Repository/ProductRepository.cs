using BanSach.DataAcess.Data;
using BanSach.DataAcess.Repository.IRepository;
using BanSach.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.DataAcess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        //tạo 1 biến 
        private readonly ApplicationDbContext _db;
    

        //hàm khởi tạo
        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
          
        
        }
      /*   public void Save()
        {
            _db.SaveChanges();
        }*/

        /*override lại hàm update*/
        public void Update(Product product)
        {
           var obj = _db.Products.FirstOrDefault(u => u.Id == product.Id);
            if(obj != null)
            {
                obj.Name = product.Name;
                obj.ISBN = product.ISBN;
                obj.Description = product.Description;
                obj.Price50 = product.Price50;
                obj.Price100 = product.Price100;              
                obj.Author = product.Author;
                obj.CoverTypeId = product.CoverTypeId;
                obj.CategoryId = product.CategoryId;
                if(product.ImageUrl != null)
                {
                    obj.ImageUrl = product.ImageUrl;
                }
            }
            _db.Products.Update(product);
        }

        
    }
}

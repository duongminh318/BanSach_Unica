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
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRePository
    {
        //tạo 1 biến 
        private readonly ApplicationDbContext _db;
    

        //hàm khởi tạo
        public CoverTypeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        
        }
      /*   public void Save()
        {
            _db.SaveChanges();
        }*/

        public void Update(CoverType coverType)
        {
           _db.CoverTypes.Update(coverType);
        }
    }
}

using BanSach.DataAcess.Data;
using BanSach.DataAcess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.DataAcess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        //tạo 1 biến 
        private readonly ApplicationDbContext _db;
        internal DbSet<T> DbSet;

        //hàm khởi tạo
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.DbSet = _db.Set<T>();
        }

        public void Add(T entity)
        { 
          DbSet.Add(entity);
        }

        // include category, covertype


        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
           IQueryable<T> query = DbSet;
            if(includeProperties != null)
            {
                foreach(var item in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = DbSet;
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
           DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
           DbSet.RemoveRange(entity);
        }
    }
}

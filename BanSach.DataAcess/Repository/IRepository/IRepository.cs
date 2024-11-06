using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.DataAcess.Repository.IRepository
{
    // khai báo các method dùng chung
    public interface IRepository<T> where T : class
    {
        // lấy dữ liệu
        

        
        
        // lấy category dựa vào Id (phần tử ID duy  nhật)
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);
        // lấy tất cả

        IEnumerable<T> GetAll(string? includeProperties = null);
        // add 
        void Add(T entity);
        void Remove(T entity);
        
        // xoá nhiều category
        void RemoveRange(IEnumerable<T> entity);

    }
}

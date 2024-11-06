using BanSach.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.DataAcess.Repository.IRepository
{
    public interface ICoverTypeRePository : IRepository<CoverType>
    {
        void Update(CoverType coverType);

        /*void Save();*/


    }
}

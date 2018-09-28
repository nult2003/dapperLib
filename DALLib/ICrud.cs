using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLib
{
    public interface ICrud<T, in TU>
    {
        List<T> GetAll();
        T GetSpecific(TU id);

        bool CreateOrUpdate(T element);
        bool Delete(TU id);
    }
}

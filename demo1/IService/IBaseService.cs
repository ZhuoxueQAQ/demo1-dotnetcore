using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo1.IService
{
    public interface IBaseService<T> where T:class
    {
        Task<bool> DeleteByPrimaryKey(object key);
        Task<T> QueryByPrimaryKey(object key);
        Task<List<T>> QueryAll();
        Task<bool> Add(T model);
        Task<bool> Update(T model);
    }
}

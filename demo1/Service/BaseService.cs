using System.Threading.Tasks;
using demo1.IRepository;
using demo1.Repository;
using demo1.IService;
using System.Collections.Generic;
using demo1.Model;

namespace demo1.Service
{
    public class BaseService<T>: IBaseService<T> where T:class, new()
    {
        public IBaseRepository<T> baseDal = new BaseRepository<T>();
        public async Task<bool> Add(T model)
        {
            return await baseDal.Add(model);
        }
        public async Task<bool> DeleteByPrimaryKey(object key)
        {
            return await baseDal.DeleteByPrimaryKey(key);
        }
        public async Task<T> QueryByPrimaryKey(object key)
        {
            return await baseDal.QueryByPrimaryKey(key);
        }

        public async Task<List<T>> QueryAll()
        {
            return await baseDal.QueryAll();
        }

        public async Task<bool> Update(T model)
        {
            return await baseDal.Update(model);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            return await baseDal.UpdatePost(post);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using demo1.Model;

namespace demo1.IRepository
{
    /// <summary>
    /// 这里定义了一个泛型基类接口，泛型必须是一个类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T: class
    {
        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> QueryByPrimaryKey(object key);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<List<T>> QueryAll();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Add(T model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Update(T model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> DeleteByPrimaryKey(object key);

        Task<bool> UpdatePost(Post post);

    }
}

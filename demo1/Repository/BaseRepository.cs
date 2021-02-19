using System.Collections.Generic;
using System.Threading.Tasks;
using demo1.IRepository;
using demo1.Model;

namespace demo1.Repository
{
    public class BaseRepository<T>: DbContext<T>, IBaseRepository<T> where T: class, new()
    {
        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Add(T model)
        {
            // 插入返回自增列
            // db继承自DbContext的SqlSugerClient
            var i = await Db.Insertable(model).ExecuteReturnBigIdentityAsync();
            //返回的i是long类型,这里你可以根据你的业务需要进行处理
            return i > 0;
        }
        /// <summary>
        /// 根据ID删除(这里是批量删除)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByPrimaryKey(object key)
        {
            // i为删除操作后受影响的行数
            var i = await Db.Deleteable<T>().In(key).ExecuteCommandAsync();
            return i > 0;
        }

        /// <summary>
        /// 根据ID查询一条数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> QueryByPrimaryKey(object key)
        {
            // 根据主键查询，返回查询对象
            return await Db.Queryable<T>().InSingleAsync(key);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> QueryAll()
        {
            // 异步查询对象数组
            return await Db.Queryable<T>().ToListAsync();
            
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Update(T model)
        {
            //这种方式会以model中主键的值为条件进行更新
            var i = await Db.Updateable(model).ExecuteCommandAsync();
            return i > 0;
        }

        public async Task<bool> UpdatePost(Post post)
        {
            // 不更新创建日期
            var i = await Db.Updateable(post).IgnoreColumns( it => new { it.createdDate }).ExecuteCommandAsync();
            return i > 0;
        }

    }
}

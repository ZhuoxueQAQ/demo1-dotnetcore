using demo1.Model;
using SqlSugar;
using System;
using System.Linq;

public class DbContext<T> where T : class, new()
{
    //注意：不能写成静态的
    public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
    public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来操作当前表的数据
    public SimpleClient<Post> PostDb { get { return new SimpleClient<Post>(Db); } }
    public DbContext()
    {
        // 数据库对象
        Db = new SqlSugarClient(new ConnectionConfig()
        {
            ConnectionString = "server=localhost;database=blogdb;user=root;pwd=192837;port=3306;",

            DbType = DbType.MySql,

            InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息

            IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了

        });
        //调式代码 用来打印SQL 
        Db.Aop.OnLogExecuting = (sql, pars) =>
        {
            Console.WriteLine(sql + "\r\n" +
                Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
            Console.WriteLine();
        };

    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
    //自已扩展更多方法 
}



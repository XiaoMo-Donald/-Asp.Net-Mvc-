using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Second.Website.IDAL
{
    /// <summary>
    /// 增删改查 基础类(接口)
    /// </summary>
    /// <typeparam name="T">具体的实体</typeparam>
    public interface IBaseDal<T>
    {

        //功能1：添加数据
        //函数名 AddEntity
        //参数  一个实体
        //功能  将一个用户信息添加到数据库中
        //返回值 返回所添加的信息
        bool AddEntity(T entity);

        //功能2：删除数据
        //函数名 DeleteEntity
        //参数  一个用户信息
        //功能  从数据库中删除该用户的信息
        //返回值 删除成功或失败
        bool DeleteEntity(T entity);

        //功能3：更新数据
        //函数名 EditEntity
        //参数  一个用户信息
        //功能  把一个用户的信息更新到数据库中
        //返回值 更新成功或失败
        bool EditEntity(T entity);

        //功能4：查询数据
        //函数名 LoadEntity
        //参数  查询条件lambda表达式
        //功能  根据查询条件查询数据库
        //返回值 返回查询结果
        IQueryable<T> LoadEntity(Expression<Func<T, bool>> whereLambda);

        //功能5：查询数据按分页参数返回指定范围内的数据查询列表
        //函数名 LoadPageEntity
        //参数  页索引号（pageIndex）
        //每页显示记录数（pageSize）
        //输出参数（总页数 ） totalCount
        //查询条件lambda
        //排序依据lambda
        //排序标志 升序或降序
        //功能  根据查询条件查询数据库，返回总记录数和指定范围内的数据
        //返回值 返回指定范围内的数据
        IQueryable<T> LoadPageEntity<TKey>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> keySelector, bool isAsc);

    }
}

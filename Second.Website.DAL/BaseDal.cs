using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Second.Website.Model;

namespace Second.Website.DAL
{ 
    /// <summary>
  /// 增删改查 基础类(泛型封装)
  /// </summary>
  /// <typeparam name="T">具体的实体</typeparam>
    public class BaseDal<T> where T : class, new() //where T : class   对T的类型进行约束 当前为类 类型 new()已经实例化（系统默认就行不需要再次实例化）
    {

        SecondWebsiteDB db = new SecondWebsiteDB();
        /// <summary>
        /// 功能1：添加数据
        /// </summary>
        /// <param name="entity">T 实体类型的对象名称</param>
        /// <returns>bool</returns>
        public bool AddEntity(T entity)
        {
            ////1.写法1
            //db.UserInfo.Add(userInfo);
            //2.写法2
            db.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;

            return db.SaveChanges()>0;
        }

        /// <summary>
        /// 功能2：删除数据
        /// </summary>
        /// <param name="entity">T 实体类型的对象名称</param>
        /// <returns>bool</returns>
        public bool DeleteEntity(T entity)
        {
            ////1.写法1
            //db.UserInfo.Remove(userInfo);
            //2.写法2
            db.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            return db.SaveChanges() > 0;

        }
 
        /// <summary>
        /// 功能3：更新数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns>bool</returns>
        public bool EditEntity(T entity)
        {
            db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges() > 0;

        }

        /// <summary>
        /// 功能4：根据lambda表达式查询数据
        /// </summary>
        /// <param name="whereLambda">入口参数（写lambda表达式）</param>
        /// <returns>IQueryable<T>数据集</returns>
        public IQueryable<T> LoadEntity(Expression<Func<T, bool>> whereLambda)
        {
            ////var userInfoList = db.UserInfo.Where<UserInfo>(whereLambda);
            //return list;
            return db.Set<T>().Where<T>(whereLambda);
        }
 
        /// <summary>
        /// 功能5：查询数据按分页参数返回指定范围内的数据查询列表
        /// </summary>
        /// <typeparam name="TKey">表达式类型</typeparam>
        /// <param name="pageIndex">页的索引号</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="totalCount">页数总数</param>
        /// <param name="whereLambda">查询条件Lambda</param>
        /// <param name="keySelector"></param>
        /// <param name="isAsc">升序或降序 bool值 true升序 false降序</param>
        /// <returns>IQueryable<T>数据集</returns>
        public IQueryable<T> LoadPageEntity<TKey>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> keySelector, bool isAsc)
        {
            //计算总记录数
            //int recordCount = db.UserInfo.Where<UserInfo>(whereLambda).Count();
            int recordCount = db.Set<T>().Where<T>(whereLambda).Count();

            //计算总页数
            totalCount = Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            //限制pageIndex的范围，不能小于1也不能大于总页数
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > totalCount ? totalCount : pageIndex;
            //查询指定页面的数据
            //var userListq = db.UserInfo.Where<UserInfo>(whereLambda);
            var userListq = db.Set<T>().Where<T>(whereLambda);
            if (isAsc)
            {
                userListq = userListq.OrderBy<T, TKey>(keySelector).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                userListq = userListq.OrderByDescending<T, TKey>(keySelector).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }

            return userListq;

        }
    }
}
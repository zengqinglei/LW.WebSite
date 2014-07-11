using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region 命名空间
using LW.EFEntity;
using System.Data;
using System.Linq.Expressions;
using System.Data.SqlClient;
#endregion

namespace LW.Business
{
    public class B_Base : MySqlEntities
    {
        #region 公共查询--sql查询数据集
        public DataSet SqlDataSet(string sql, SqlParameter[] sp = null, Array maps = null)
        {
            SqlConnection conn = new SqlConnection(this.Database.Connection.ConnectionString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand(sql, conn);
            if (sp != null)
            {
                cmd.Parameters.AddRange(sp);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            if (maps != null)
            {
                da.TableMappings.AddRange(maps);
            }
            DataSet ds = new DataSet();
            da.Fill(ds);

            da.Dispose();
            conn.Close();
            conn.Dispose();

            return ds;
        }
        #endregion

        #region 公共交互--新增单个实体
        public T Insert<T>(T entity) where T : class
        {
            this.Set<T>().Add(entity);
            this.SaveChanges();

            return entity;
        }
        #endregion

        #region 公共交互--修改单个实体
        public T Update<T>(T entity) where T : class
        {
            this.Set<T>().Attach(entity);
            this.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            this.SaveChanges();

            return entity;
        }
        #endregion

        #region 公共交互--删除单个实体
        public void Delete<T>(T entity) where T : class
        {
            this.Set<T>().Remove(entity);
            this.SaveChanges();
        }
        #endregion

        #region 公共交互--查询单个实体 by 主键集合
        public T Find<T>(params object[] keyValues) where T : class
        {
            return this.Set<T>().Find(keyValues);
        }
        #endregion

        #region 公共交互--查询实体集合 by 条件
        public List<T> FindAll<T>(Expression<Func<T, bool>> conditions = null) where T : class
        {
            var query = this.Set<T>().AsQueryable();
            if (conditions != null)
            {
                query = query.Where(conditions);
            }

            return query.ToList();
        }
        #endregion

        #region 公共交互--分页查询实体集合 by 条件 排序
        public List<T> FindAllByPage<T, S>(int page, int rows, out int total, KeyValuePair<bool, Expression<Func<T, S>>>? orderBy = null, Expression<Func<T, bool>> conditions = null) where T : class
        {
            var query = this.Set<T>().AsQueryable();
            if (conditions != null)
            {
                query = query.Where(conditions);
            }
            if (orderBy.HasValue)
            {
                if (orderBy.Value.Key)
                {
                    query = query.OrderBy(orderBy.Value.Value);
                }
                else
                {
                    query = query.OrderByDescending(orderBy.Value.Value);
                }
            }
            total = query.Count();

            return query.ToList();
        }
        #endregion
    }
}

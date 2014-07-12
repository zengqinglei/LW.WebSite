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
        public void Delete<T>(params object[] keyValues) where T : class
        {
            var entity = this.Set<T>().Find(keyValues);
            if (entity != null)
            {
                Delete<T>(entity);
            }
        }
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
        public List<T> FindAllByPage<T>(int page, int rows, out int total, string sort, string order, Expression<Func<T, bool>> conditions = null) where T : class
        {
            var query = this.Set<T>().AsQueryable();
            if (conditions != null)
            {
                query = query.Where(conditions);
            }
            total = query.Count();
            query = query.OrderBy(sort, order);

            return query.Skip((page - 1) * rows).Take(rows).ToList();
        }
        #endregion
    }

    #region 排序拓展
    public static class QueryExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sort, string order = "asc")
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (String.IsNullOrEmpty(sort))
            {
                return source;
            }

            ParameterExpression parameter = Expression.Parameter(source.ElementType, String.Empty);
            MemberExpression property = Expression.Property(parameter, sort);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = order.Equals("asc", StringComparison.OrdinalIgnoreCase) ? "OrderBy" : "OrderByDescending";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                new Type[] { source.ElementType, property.Type },
                                                source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
    #endregion

    #region 多条件拓展
    public static class ExpressionExtensions
    {

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body = Expression.And(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body = Expression.Or(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
    }
    internal class ParameterReplacer : ExpressionVisitor
    {
        public ParameterReplacer(ParameterExpression paramExpr)
        {
            this.ParameterExpression = paramExpr;
        }

        public ParameterExpression ParameterExpression { get; private set; }

        public Expression Replace(Expression expr)
        {
            return this.Visit(expr);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return this.ParameterExpression;
        }
    }
    #endregion
}

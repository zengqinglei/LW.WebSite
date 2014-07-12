using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region 命名空间
using LW.EFEntity;
using System.Linq.Expressions;
using LW.ViewModels.AdminSite;
using AutoMapper;
#endregion

namespace LW.Business
{
    public class B_User : B_Base
    {
        #region 构造函数--配置AutoMapper
        public B_User()
        {
            Mapper.CreateMap<users, VM_User>();
        }
        #endregion

        #region 客户管理--查询 by userid
        public VM_User GetOne(int userid)
        {
            return Mapper.Map<users, VM_User>(base.Find<users>(userid));
        }
        #endregion

        #region 客户管理--查询 分页 排序 条件
        public List<VM_User> GetListPage(int page, int rows, out int total, string sort = null, string order = null,
            DateTime? regBeginTime = null, DateTime? regEndTime = null, string nickname = null, string usermail = null)
        {
            Expression<Func<users, bool>> where = m => true;
            if (regBeginTime.HasValue)
            {
                where = where.And(m => m.addtime >= regBeginTime);
            }
            if (regEndTime.HasValue)
            {
                where = where.And(m => m.addtime < regEndTime);
            }
            if (!string.IsNullOrEmpty(nickname))
            {
                where = where.And(m => m.nickname.Contains(nickname));
            }
            if (!string.IsNullOrEmpty(usermail))
            {
                where = where.And(m => m.usermail.Contains(usermail));
            }
            if (string.IsNullOrEmpty(sort))
            {
                sort = "addtime";
            }
            if (string.IsNullOrEmpty(order))
            {
                order = "desc";
            }
            return Mapper.Map<List<users>, List<VM_User>>(base.FindAllByPage<users>(page, rows, out total, sort, order, where));
        }
        #endregion

        #region 客户管理--删除 by userid
        public void Delete(int userid)
        {
            base.Delete<users>(userid);
        }
        #endregion
    }
}

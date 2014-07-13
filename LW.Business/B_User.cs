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
using LW.Utility;
#endregion

namespace LW.Business
{
    public class B_User : B_Base
    {
        #region 构造函数--配置AutoMapper
        public B_User()
        {
            Mapper.CreateMap<users, VM_User>()
                .ForMember(dto => dto.password, opt => opt.MapFrom(entity => string.Empty));
            Mapper.CreateMap<VM_User, users>()
                .ForMember(dto => dto.password, opt => opt.MapFrom(entity => entity.password ?? EDHelper.MD5Encrypt("123456")))
                .ForMember(dto => dto.addtime, opt => opt.MapFrom(entity => entity.addtime ?? DateTime.Now))
                .ForMember(dto => dto.invitecode, opt => opt.MapFrom(entity => entity.invitecode ?? "无"))
                .ForMember(dto => dto.know_way, opt => opt.MapFrom(entity => entity.know_way ?? "无"));
        }
        #endregion

        #region 客户管理--查询 by userid
        public VM_User GetOne(int userid)
        {
            return Mapper.Map<users, VM_User>(base.Find<users>(userid));
        }
        #endregion

        #region 客户管理--if exists by nickname
        public bool ExistNickname(string nickname)
        {
            if (base.FindAll<users>(m => m.nickname == nickname).Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 客户管理--查询 分页 排序 条件
        public List<VM_User> GetListPage(int page, int rows, out int total, string sort = null, string order = null,
            DateTime? addBeginTime = null, DateTime? addEndTime = null, string nickname = null, string usermail = null)
        {
            Expression<Func<users, bool>> where = m => true;
            if (addBeginTime.HasValue)
            {
                where = where.And(m => m.addtime >= addBeginTime);
            }
            if (addEndTime.HasValue)
            {
                where = where.And(m => m.addtime < addEndTime);
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

        #region 客户管理--新增
        public VM_User Add(VM_User vmUser)
        {
            var emUser = Mapper.Map<VM_User, users>(vmUser);

            return Mapper.Map<users, VM_User>(base.Insert(emUser));
        }
        #endregion

        #region 客户管理--修改
        public VM_User Update(VM_User vmUser)
        {
            var emUser = base.Find<users>(vmUser.userid);
            emUser.nickname = vmUser.nickname;
            emUser.usermail = vmUser.usermail;
            emUser.state = vmUser.state;
            emUser.if_super = vmUser.if_super;
            emUser.is_mobile = vmUser.is_mobile;
            emUser.is_solution = vmUser.is_solution;
            emUser.is_spreader = vmUser.is_spreader;
            emUser.know_way = vmUser.know_way ?? "无";

            return Mapper.Map<users, VM_User>(base.Update(emUser));
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

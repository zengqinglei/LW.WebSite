using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region 命名空间
using LW.ViewModels.AdminSite;
using LW.EFEntity;
using System.Data.Entity;
using AutoMapper;
using System.Linq.Expressions;
#endregion

namespace LW.Business
{
    public class B_Box : B_Base
    {
        #region 构造函数--配置AutoMapper
        public B_Box()
        {
            Mapper.CreateMap<box, VM_Box>();
            Mapper.CreateMap<VM_Box, box>();
        }
        #endregion

        #region 产品盒子--查询 by boxid
        public VM_Box GetOne(int boxid)
        {
            return Mapper.Map<box, VM_Box>(base.Find<box>(boxid));
        }
        #endregion

        #region 产品盒子--类别查询
        public List<VM_Box> GetListPage(int page, int rows, out int total, string sort = null, string order = null,
            DateTime? addBeginTime = null, DateTime? addEndTime = null, string name = null)
        {
            Expression<Func<box, bool>> where = m => true;
            if (addBeginTime.HasValue)
            {
                where = where.And(m => m.addtime >= addBeginTime);
            }
            if (addEndTime.HasValue)
            {
                where = where.And(m => m.addtime < addEndTime);
            }
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.name.Contains(name));
            }
            if (string.IsNullOrEmpty(sort))
            {
                sort = "addtime";
            }
            if (string.IsNullOrEmpty(order))
            {
                order = "desc";
            }
            return Mapper.Map<List<box>, List<VM_Box>>(base.FindAllByPage<box>(page, rows, out total, sort, order, where));
        }
        #endregion

        #region 产品盒子--新增
        public VM_Box Add(VM_Box vmBox)
        {
            var emBox = Mapper.Map<VM_Box, box>(vmBox);

            return Mapper.Map<box, VM_Box>(base.Insert(emBox));
        }
        #endregion

        #region 产品盒子--修改
        public VM_Box Update(VM_Box vmBox)
        {
            var emBox = base.Find<box>(vmBox.boxid);

            return Mapper.Map<box, VM_Box>(base.Update(emBox));
        }
        #endregion

        #region 产品盒子--删除
        public void Delete(int boxid)
        {
            base.Delete<box>(boxid);
        }
        #endregion
    }
}

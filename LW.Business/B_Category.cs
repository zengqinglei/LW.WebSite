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
#endregion

namespace LW.Business
{
    public class B_Category : B_Base
    {
        #region 构造函数--配置AutoMapper
        public B_Category()
        {
            Mapper.CreateMap<category, VM_Category>();
            Mapper.CreateMap<VM_Category, category>();
        }
        #endregion

        #region 产品类别--查询 by cid
        public VM_Category GetOne(int cid)
        {
            return Mapper.Map<category, VM_Category>(base.Find<category>(cid));
        }
        #endregion

        #region 产品类别--类别查询
        public List<VM_Category> GetChilds(int pcid, bool hasChild = false)
        {
            var vmCategorys = Mapper.Map<List<category>, List<VM_Category>>(base.FindAll<category>(m => m.pcid == pcid));
            if (hasChild && vmCategorys != null)
            {
                for (int i = 0; i < vmCategorys.Count; i++)
                {
                    var vmCategory = vmCategorys[i];
                    vmCategory.children = GetChilds(vmCategory.cid, hasChild);
                    if (vmCategory.children.Count > 0)
                    {
                        vmCategory.state = "closed";
                    }
                }
            }
            return vmCategorys;
        }
        #endregion

        #region 产品类别--删除
        public void Delete(int cid)
        {
            base.Delete<category>(cid);
        }
        #endregion
    }
}

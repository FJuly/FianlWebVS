using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class T_RoleAct : BaseBLL<MODEL.T_RoleAct>, IBLL.IRoleActBLL
    {
        public int Add(List<MODEL.T_RoleAct> list)
        {
            return idal.AddList(list);
        }
    }
}

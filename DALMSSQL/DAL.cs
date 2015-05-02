 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DALMSSQL
{
	public partial class T_DepartmentDAL : BaseDAL<MODEL.T_Department>,IDAL.IDepartmentDAL
    {
    }
	public partial class T_MemberInformationDAL : BaseDAL<MODEL.T_MemberInformation>,IDAL.IMemberInformationDAL
    {
    }
	public partial class T_OgnizationActDAL : BaseDAL<MODEL.T_OgnizationAct>,IDAL.IOgnizationActDAL
    {
    }
	public partial class T_OnDutyDAL : BaseDAL<MODEL.T_OnDuty>,IDAL.IOnDutyDAL
    {
    }
	public partial class T_OrganizationDAL : BaseDAL<MODEL.T_Organization>,IDAL.IOrganizationDAL
    {
    }
	public partial class T_PermissionDAL : BaseDAL<MODEL.T_Permission>,IDAL.IPermissionDAL
    {
    }
	public partial class T_ProjectInformationDAL : BaseDAL<MODEL.T_ProjectInformation>,IDAL.IProjectInformationDAL
    {
    }
	public partial class T_ProjectParticipationDAL : BaseDAL<MODEL.T_ProjectParticipation>,IDAL.IProjectParticipationDAL
    {
    }
	public partial class T_ProjectTypeDAL : BaseDAL<MODEL.T_ProjectType>,IDAL.IProjectTypeDAL
    {
    }
	public partial class T_ProjPhaseDAL : BaseDAL<MODEL.T_ProjPhase>,IDAL.IProjPhaseDAL
    {
    }
	public partial class T_RoleDAL : BaseDAL<MODEL.T_Role>,IDAL.IRoleDAL
    {
    }
	public partial class T_RoleActDAL : BaseDAL<MODEL.T_RoleAct>,IDAL.IRoleActDAL
    {
    }
	public partial class T_RolePermissionDAL : BaseDAL<MODEL.T_RolePermission>,IDAL.IRolePermissionDAL
    {
    }
	public partial class T_TaskInformationDAL : BaseDAL<MODEL.T_TaskInformation>,IDAL.ITaskInformationDAL
    {
    }
	public partial class T_TaskParticipationDAL : BaseDAL<MODEL.T_TaskParticipation>,IDAL.ITaskParticipationDAL
    {
    }
	public partial class T_TaskTypeDAL : BaseDAL<MODEL.T_TaskType>,IDAL.ITaskTypeDAL
    {
    }
	public partial class T_TechnicaLevelDAL : BaseDAL<MODEL.T_TechnicaLevel>,IDAL.ITechnicaLevelDAL
    {
    }
}
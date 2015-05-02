 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
	public partial class T_Department : BaseBLL<MODEL.T_Department>,IBLL.IDepartmentBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IDepartmentDAL;
		}
    }

	public partial class T_MemberInformation : BaseBLL<MODEL.T_MemberInformation>,IBLL.IMemberInformationBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IMemberInformationDAL;
		}
    }

	public partial class T_OgnizationAct : BaseBLL<MODEL.T_OgnizationAct>,IBLL.IOgnizationActBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IOgnizationActDAL;
		}
    }

	public partial class T_OnDuty : BaseBLL<MODEL.T_OnDuty>,IBLL.IOnDutyBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IOnDutyDAL;
		}
    }

	public partial class T_Organization : BaseBLL<MODEL.T_Organization>,IBLL.IOrganizationBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IOrganizationDAL;
		}
    }

	public partial class T_Permission : BaseBLL<MODEL.T_Permission>,IBLL.IPermissionBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IPermissionDAL;
		}
    }

	public partial class T_ProjectInformation : BaseBLL<MODEL.T_ProjectInformation>,IBLL.IProjectInformationBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IProjectInformationDAL;
		}
    }

	public partial class T_ProjectParticipation : BaseBLL<MODEL.T_ProjectParticipation>,IBLL.IProjectParticipationBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IProjectParticipationDAL;
		}
    }

	public partial class T_ProjectType : BaseBLL<MODEL.T_ProjectType>,IBLL.IProjectTypeBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IProjectTypeDAL;
		}
    }

	public partial class T_ProjPhase : BaseBLL<MODEL.T_ProjPhase>,IBLL.IProjPhaseBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IProjPhaseDAL;
		}
    }

	public partial class T_Role : BaseBLL<MODEL.T_Role>,IBLL.IRoleBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IRoleDAL;
		}
    }

	public partial class T_RoleAct : BaseBLL<MODEL.T_RoleAct>,IBLL.IRoleActBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IRoleActDAL;
		}
    }

	public partial class T_RolePermission : BaseBLL<MODEL.T_RolePermission>,IBLL.IRolePermissionBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IRolePermissionDAL;
		}
    }

	public partial class T_TaskInformation : BaseBLL<MODEL.T_TaskInformation>,IBLL.ITaskInformationBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.ITaskInformationDAL;
		}
    }

	public partial class T_TaskParticipation : BaseBLL<MODEL.T_TaskParticipation>,IBLL.ITaskParticipationBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.ITaskParticipationDAL;
		}
    }

	public partial class T_TaskType : BaseBLL<MODEL.T_TaskType>,IBLL.ITaskTypeBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.ITaskTypeDAL;
		}
    }

	public partial class T_TechnicaLevel : BaseBLL<MODEL.T_TechnicaLevel>,IBLL.ITechnicaLevelBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.ITechnicaLevelDAL;
		}
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBLL;
using BLL;

namespace BLL
{
	public partial class BLLSession:IBLL.IBLLSession
    {
		#region 01 业务接口 IT_DepartmentDAL
		IDepartmentBLL iDepartmentBLL;
		public IDepartmentBLL IDepartmentBLL
		{
			get
			{
				if(iDepartmentBLL==null)
					iDepartmentBLL= new T_Department();
				return iDepartmentBLL;
			}
			set
			{
				iDepartmentBLL= value;
			}
		}
		#endregion

		#region 02 业务接口 IT_MemberInformationDAL
		IMemberInformationBLL iMemberInformationBLL;
		public IMemberInformationBLL IMemberInformationBLL
		{
			get
			{
				if(iMemberInformationBLL==null)
					iMemberInformationBLL= new T_MemberInformation();
				return iMemberInformationBLL;
			}
			set
			{
				iMemberInformationBLL= value;
			}
		}
		#endregion

		#region 03 业务接口 IT_OgnizationActDAL
		IOgnizationActBLL iOgnizationActBLL;
		public IOgnizationActBLL IOgnizationActBLL
		{
			get
			{
				if(iOgnizationActBLL==null)
					iOgnizationActBLL= new T_OgnizationAct();
				return iOgnizationActBLL;
			}
			set
			{
				iOgnizationActBLL= value;
			}
		}
		#endregion

		#region 04 业务接口 IT_OnDutyDAL
		IOnDutyBLL iOnDutyBLL;
		public IOnDutyBLL IOnDutyBLL
		{
			get
			{
				if(iOnDutyBLL==null)
					iOnDutyBLL= new T_OnDuty();
				return iOnDutyBLL;
			}
			set
			{
				iOnDutyBLL= value;
			}
		}
		#endregion

		#region 05 业务接口 IT_OrganizationDAL
		IOrganizationBLL iOrganizationBLL;
		public IOrganizationBLL IOrganizationBLL
		{
			get
			{
				if(iOrganizationBLL==null)
					iOrganizationBLL= new T_Organization();
				return iOrganizationBLL;
			}
			set
			{
				iOrganizationBLL= value;
			}
		}
		#endregion

		#region 06 业务接口 IT_PermissionDAL
		IPermissionBLL iPermissionBLL;
		public IPermissionBLL IPermissionBLL
		{
			get
			{
				if(iPermissionBLL==null)
					iPermissionBLL= new T_Permission();
				return iPermissionBLL;
			}
			set
			{
				iPermissionBLL= value;
			}
		}
		#endregion

		#region 07 业务接口 IT_ProjectInformationDAL
		IProjectInformationBLL iProjectInformationBLL;
		public IProjectInformationBLL IProjectInformationBLL
		{
			get
			{
				if(iProjectInformationBLL==null)
					iProjectInformationBLL= new T_ProjectInformation();
				return iProjectInformationBLL;
			}
			set
			{
				iProjectInformationBLL= value;
			}
		}
		#endregion

		#region 08 业务接口 IT_ProjectParticipationDAL
		IProjectParticipationBLL iProjectParticipationBLL;
		public IProjectParticipationBLL IProjectParticipationBLL
		{
			get
			{
				if(iProjectParticipationBLL==null)
					iProjectParticipationBLL= new T_ProjectParticipation();
				return iProjectParticipationBLL;
			}
			set
			{
				iProjectParticipationBLL= value;
			}
		}
		#endregion

		#region 09 业务接口 IT_ProjectTypeDAL
		IProjectTypeBLL iProjectTypeBLL;
		public IProjectTypeBLL IProjectTypeBLL
		{
			get
			{
				if(iProjectTypeBLL==null)
					iProjectTypeBLL= new T_ProjectType();
				return iProjectTypeBLL;
			}
			set
			{
				iProjectTypeBLL= value;
			}
		}
		#endregion

		#region 10 业务接口 IT_ProjPhaseDAL
		IProjPhaseBLL iProjPhaseBLL;
		public IProjPhaseBLL IProjPhaseBLL
		{
			get
			{
				if(iProjPhaseBLL==null)
					iProjPhaseBLL= new T_ProjPhase();
				return iProjPhaseBLL;
			}
			set
			{
				iProjPhaseBLL= value;
			}
		}
		#endregion

		#region 11 业务接口 IT_RoleDAL
		IRoleBLL iRoleBLL;
		public IRoleBLL IRoleBLL
		{
			get
			{
				if(iRoleBLL==null)
					iRoleBLL= new T_Role();
				return iRoleBLL;
			}
			set
			{
				iRoleBLL= value;
			}
		}
		#endregion

		#region 12 业务接口 IT_RoleActDAL
		IRoleActBLL iRoleActBLL;
		public IRoleActBLL IRoleActBLL
		{
			get
			{
				if(iRoleActBLL==null)
					iRoleActBLL= new T_RoleAct();
				return iRoleActBLL;
			}
			set
			{
				iRoleActBLL= value;
			}
		}
		#endregion

		#region 13 业务接口 IT_RolePermissionDAL
		IRolePermissionBLL iRolePermissionBLL;
		public IRolePermissionBLL IRolePermissionBLL
		{
			get
			{
				if(iRolePermissionBLL==null)
					iRolePermissionBLL= new T_RolePermission();
				return iRolePermissionBLL;
			}
			set
			{
				iRolePermissionBLL= value;
			}
		}
		#endregion

		#region 14 业务接口 IT_TaskInformationDAL
		ITaskInformationBLL iTaskInformationBLL;
		public ITaskInformationBLL ITaskInformationBLL
		{
			get
			{
				if(iTaskInformationBLL==null)
					iTaskInformationBLL= new T_TaskInformation();
				return iTaskInformationBLL;
			}
			set
			{
				iTaskInformationBLL= value;
			}
		}
		#endregion

		#region 15 业务接口 IT_TaskParticipationDAL
		ITaskParticipationBLL iTaskParticipationBLL;
		public ITaskParticipationBLL ITaskParticipationBLL
		{
			get
			{
				if(iTaskParticipationBLL==null)
					iTaskParticipationBLL= new T_TaskParticipation();
				return iTaskParticipationBLL;
			}
			set
			{
				iTaskParticipationBLL= value;
			}
		}
		#endregion

		#region 16 业务接口 IT_TaskTypeDAL
		ITaskTypeBLL iTaskTypeBLL;
		public ITaskTypeBLL ITaskTypeBLL
		{
			get
			{
				if(iTaskTypeBLL==null)
					iTaskTypeBLL= new T_TaskType();
				return iTaskTypeBLL;
			}
			set
			{
				iTaskTypeBLL= value;
			}
		}
		#endregion

		#region 17 业务接口 IT_TechnicaLevelDAL
		ITechnicaLevelBLL iTechnicaLevelBLL;
		public ITechnicaLevelBLL ITechnicaLevelBLL
		{
			get
			{
				if(iTechnicaLevelBLL==null)
					iTechnicaLevelBLL= new T_TechnicaLevel();
				return iTechnicaLevelBLL;
			}
			set
			{
				iTechnicaLevelBLL= value;
			}
		}
		#endregion

    }

}
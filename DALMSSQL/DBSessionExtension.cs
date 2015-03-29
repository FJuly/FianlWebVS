
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;

namespace DALMSSQL
{
	public partial class DBSession:IDAL.IDBSession
    {
		#region 01 数据接口 IDepartmentDAL
		IDepartmentDAL iDepartmentDAL;
		public IDepartmentDAL IDepartmentDAL
		{
			get
			{
				if(iDepartmentDAL==null)
					iDepartmentDAL= new T_DepartmentDAL();
				return iDepartmentDAL;
			}
			set
			{
				iDepartmentDAL= value;
			}
		}
		#endregion

		#region 02 数据接口 IMemberInformationDAL
		IMemberInformationDAL iMemberInformationDAL;
		public IMemberInformationDAL IMemberInformationDAL
		{
			get
			{
				if(iMemberInformationDAL==null)
					iMemberInformationDAL= new T_MemberInformationDAL();
				return iMemberInformationDAL;
			}
			set
			{
				iMemberInformationDAL= value;
			}
		}
		#endregion

		#region 03 数据接口 IOrganizationDAL
		IOrganizationDAL iOrganizationDAL;
		public IOrganizationDAL IOrganizationDAL
		{
			get
			{
				if(iOrganizationDAL==null)
					iOrganizationDAL= new T_OrganizationDAL();
				return iOrganizationDAL;
			}
			set
			{
				iOrganizationDAL= value;
			}
		}
		#endregion

		#region 04 数据接口 IPermissionDAL
		IPermissionDAL iPermissionDAL;
		public IPermissionDAL IPermissionDAL
		{
			get
			{
				if(iPermissionDAL==null)
					iPermissionDAL= new T_PermissionDAL();
				return iPermissionDAL;
			}
			set
			{
				iPermissionDAL= value;
			}
		}
		#endregion

		#region 05 数据接口 IProjectInformationDAL
		IProjectInformationDAL iProjectInformationDAL;
		public IProjectInformationDAL IProjectInformationDAL
		{
			get
			{
				if(iProjectInformationDAL==null)
					iProjectInformationDAL= new T_ProjectInformationDAL();
				return iProjectInformationDAL;
			}
			set
			{
				iProjectInformationDAL= value;
			}
		}
		#endregion

		#region 06 数据接口 IProjectParticipationDAL
		IProjectParticipationDAL iProjectParticipationDAL;
		public IProjectParticipationDAL IProjectParticipationDAL
		{
			get
			{
				if(iProjectParticipationDAL==null)
					iProjectParticipationDAL= new T_ProjectParticipationDAL();
				return iProjectParticipationDAL;
			}
			set
			{
				iProjectParticipationDAL= value;
			}
		}
		#endregion

		#region 07 数据接口 IProjectTypeDAL
		IProjectTypeDAL iProjectTypeDAL;
		public IProjectTypeDAL IProjectTypeDAL
		{
			get
			{
				if(iProjectTypeDAL==null)
					iProjectTypeDAL= new T_ProjectTypeDAL();
				return iProjectTypeDAL;
			}
			set
			{
				iProjectTypeDAL= value;
			}
		}
		#endregion

		#region 08 数据接口 IProjPhaseDAL
		IProjPhaseDAL iProjPhaseDAL;
		public IProjPhaseDAL IProjPhaseDAL
		{
			get
			{
				if(iProjPhaseDAL==null)
					iProjPhaseDAL= new T_ProjPhaseDAL();
				return iProjPhaseDAL;
			}
			set
			{
				iProjPhaseDAL= value;
			}
		}
		#endregion

		#region 09 数据接口 IRoleDAL
		IRoleDAL iRoleDAL;
		public IRoleDAL IRoleDAL
		{
			get
			{
				if(iRoleDAL==null)
					iRoleDAL= new T_RoleDAL();
				return iRoleDAL;
			}
			set
			{
				iRoleDAL= value;
			}
		}
		#endregion

		#region 10 数据接口 IRoleActDAL
		IRoleActDAL iRoleActDAL;
		public IRoleActDAL IRoleActDAL
		{
			get
			{
				if(iRoleActDAL==null)
					iRoleActDAL= new T_RoleActDAL();
				return iRoleActDAL;
			}
			set
			{
				iRoleActDAL= value;
			}
		}
		#endregion

		#region 11 数据接口 IRolePermissionDAL
		IRolePermissionDAL iRolePermissionDAL;
		public IRolePermissionDAL IRolePermissionDAL
		{
			get
			{
				if(iRolePermissionDAL==null)
					iRolePermissionDAL= new T_RolePermissionDAL();
				return iRolePermissionDAL;
			}
			set
			{
				iRolePermissionDAL= value;
			}
		}
		#endregion

		#region 12 数据接口 ITaskInformationDAL
		ITaskInformationDAL iTaskInformationDAL;
		public ITaskInformationDAL ITaskInformationDAL
		{
			get
			{
				if(iTaskInformationDAL==null)
					iTaskInformationDAL= new T_TaskInformationDAL();
				return iTaskInformationDAL;
			}
			set
			{
				iTaskInformationDAL= value;
			}
		}
		#endregion

		#region 13 数据接口 ITaskParticipationDAL
		ITaskParticipationDAL iTaskParticipationDAL;
		public ITaskParticipationDAL ITaskParticipationDAL
		{
			get
			{
				if(iTaskParticipationDAL==null)
					iTaskParticipationDAL= new T_TaskParticipationDAL();
				return iTaskParticipationDAL;
			}
			set
			{
				iTaskParticipationDAL= value;
			}
		}
		#endregion

		#region 14 数据接口 ITaskTypeDAL
		ITaskTypeDAL iTaskTypeDAL;
		public ITaskTypeDAL ITaskTypeDAL
		{
			get
			{
				if(iTaskTypeDAL==null)
					iTaskTypeDAL= new T_TaskTypeDAL();
				return iTaskTypeDAL;
			}
			set
			{
				iTaskTypeDAL= value;
			}
		}
		#endregion

		#region 15 数据接口 ITechnicaLevelDAL
		ITechnicaLevelDAL iTechnicaLevelDAL;
		public ITechnicaLevelDAL ITechnicaLevelDAL
		{
			get
			{
				if(iTechnicaLevelDAL==null)
					iTechnicaLevelDAL= new T_TechnicaLevelDAL();
				return iTechnicaLevelDAL;
			}
			set
			{
				iTechnicaLevelDAL= value;
			}
		}
		#endregion

    }

}
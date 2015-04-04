
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IDAL
{

	public partial interface IDepartmentDAL : IBaseDAL<MODEL.T_Department>
    {
    }

	public partial interface IMemberInformationDAL : IBaseDAL<MODEL.T_MemberInformation>
    {
    }

	public partial interface IOgnizationActDAL : IBaseDAL<MODEL.T_OgnizationAct>
    {
    }

	public partial interface IOrganizationDAL : IBaseDAL<MODEL.T_Organization>
    {
    }

	public partial interface IPermissionDAL : IBaseDAL<MODEL.T_Permission>
    {
    }

	public partial interface IProjectInformationDAL : IBaseDAL<MODEL.T_ProjectInformation>
    {
    }

	public partial interface IProjectParticipationDAL : IBaseDAL<MODEL.T_ProjectParticipation>
    {
    }

	public partial interface IProjectTypeDAL : IBaseDAL<MODEL.T_ProjectType>
    {
    }

	public partial interface IProjPhaseDAL : IBaseDAL<MODEL.T_ProjPhase>
    {
    }

	public partial interface IRoleDAL : IBaseDAL<MODEL.T_Role>
    {
    }

	public partial interface IRoleActDAL : IBaseDAL<MODEL.T_RoleAct>
    {
    }

	public partial interface IRolePermissionDAL : IBaseDAL<MODEL.T_RolePermission>
    {
    }

	public partial interface ITaskInformationDAL : IBaseDAL<MODEL.T_TaskInformation>
    {
    }

	public partial interface ITaskParticipationDAL : IBaseDAL<MODEL.T_TaskParticipation>
    {
    }

	public partial interface ITaskTypeDAL : IBaseDAL<MODEL.T_TaskType>
    {
    }

	public partial interface ITechnicaLevelDAL : IBaseDAL<MODEL.T_TechnicaLevel>
    {
    }


}
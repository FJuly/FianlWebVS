
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL
{
	public partial interface IDBSession
    {
		IDepartmentDAL IDepartmentDAL{get;set;}
		IMemberInformationDAL IMemberInformationDAL{get;set;}
		IOgnizationActDAL IOgnizationActDAL{get;set;}
		IOrganizationDAL IOrganizationDAL{get;set;}
		IPermissionDAL IPermissionDAL{get;set;}
		IProjectInformationDAL IProjectInformationDAL{get;set;}
		IProjectParticipationDAL IProjectParticipationDAL{get;set;}
		IProjectTypeDAL IProjectTypeDAL{get;set;}
		IProjPhaseDAL IProjPhaseDAL{get;set;}
		IRoleDAL IRoleDAL{get;set;}
		IRoleActDAL IRoleActDAL{get;set;}
		IRolePermissionDAL IRolePermissionDAL{get;set;}
		ITaskInformationDAL ITaskInformationDAL{get;set;}
		ITaskParticipationDAL ITaskParticipationDAL{get;set;}
		ITaskTypeDAL ITaskTypeDAL{get;set;}
		ITechnicaLevelDAL ITechnicaLevelDAL{get;set;}
    }

}
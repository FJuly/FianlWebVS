
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLL
{
	public partial interface IBLLSession
    {
		IDepartmentBLL IDepartmentBLL{get;set;}
		IMemberInformationBLL IMemberInformationBLL{get;set;}
		IOgnizationActBLL IOgnizationActBLL{get;set;}
		IOnDutyBLL IOnDutyBLL{get;set;}
		IOrganizationBLL IOrganizationBLL{get;set;}
		IPermissionBLL IPermissionBLL{get;set;}
		IProjectInformationBLL IProjectInformationBLL{get;set;}
		IProjectParticipationBLL IProjectParticipationBLL{get;set;}
		IProjectTypeBLL IProjectTypeBLL{get;set;}
		IProjPhaseBLL IProjPhaseBLL{get;set;}
		IRoleBLL IRoleBLL{get;set;}
		IRoleActBLL IRoleActBLL{get;set;}
		IRolePermissionBLL IRolePermissionBLL{get;set;}
		ITaskInformationBLL ITaskInformationBLL{get;set;}
		ITaskParticipationBLL ITaskParticipationBLL{get;set;}
		ITaskTypeBLL ITaskTypeBLL{get;set;}
		ITechnicaLevelBLL ITechnicaLevelBLL{get;set;}
    }
}
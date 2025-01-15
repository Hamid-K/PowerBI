using System;
using System.Linq;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x0200003D RID: 61
	internal static class DataModelRoleExtensions
	{
		// Token: 0x06000262 RID: 610 RVA: 0x00010544 File Offset: 0x0000E744
		public static DataModelRole ToDataModelRole(this DataModelRoleEntity dataModelRoleEntity)
		{
			return new DataModelRole
			{
				ModelRoleId = dataModelRoleEntity.ModelRoleId,
				ModelRoleName = dataModelRoleEntity.ModelRoleName
			};
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00010564 File Offset: 0x0000E764
		public static DataModelRoleAssignment ToDataModelRoleAssignment(this DataModelRoleAssignmentEntity dataModelRoleAssignmentEntity)
		{
			DataModelRoleAssignment dataModelRoleAssignment = new DataModelRoleAssignment();
			dataModelRoleAssignment.GroupUserName = dataModelRoleAssignmentEntity.UserName;
			dataModelRoleAssignment.DataModelRoles = dataModelRoleAssignmentEntity.DataModelRoles.Select((DataModelRoleEntity r) => r.ModelRoleId).ToList<Guid>();
			return dataModelRoleAssignment;
		}
	}
}

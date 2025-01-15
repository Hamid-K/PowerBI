using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x0200000C RID: 12
	public interface ICatalogDataModelRoleAccessor
	{
		// Token: 0x060000C3 RID: 195
		Task<IList<DataModelRoleEntity>> GetDataModelRolesByItemAsync(Guid catalogItemId);

		// Token: 0x060000C4 RID: 196
		Task<int> AddDataModelRoleAsync(Guid itemId, Guid modelRoleId, string modelRoleName);

		// Token: 0x060000C5 RID: 197
		Task<int> UpdateDataModelRoleAsync(long dataModelRoleId, string modelRoleName);

		// Token: 0x060000C6 RID: 198
		Task<int> DeleteDataModelRoleByIdAsync(long dataModelRoleId);

		// Token: 0x060000C7 RID: 199
		Task<IList<DataModelRoleAssignmentEntity>> GetDataModelRoleAssignmentsByItemAsync(Guid catalogItemId);

		// Token: 0x060000C8 RID: 200
		Task<int> AddDataModelRoleAssignmentAsync(Guid userId, long dataModelRoleId);

		// Token: 0x060000C9 RID: 201
		Task<int> DeleteDataModelRoleAssignmentAsync(Guid userId, long dataModelRoleId);
	}
}

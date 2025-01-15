using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x0200000D RID: 13
	public interface ICatalogDataModelDataSourceAccessor
	{
		// Token: 0x060000CA RID: 202
		Task<IList<DataModelDataSourceEntity>> GetDataModelDataSourcesByItemAsync(Guid catalogItemId);

		// Token: 0x060000CB RID: 203
		Task<IList<DataModelDataSourceEntity>> GetDataModelDataSourcesByItemIdAndModelAsync(Guid catalogItemId, string modelConnectionName);

		// Token: 0x060000CC RID: 204
		Task<int> AddDataModelDataSourceAsync(Guid itemId, DataModelDataSourceEntity.DataModelDataSourceType dSType, DataModelDataSourceEntity.DataModelDataSourceKind dSKind, DataModelDataSourceEntity.DataModelDataSourceAuthType authType, byte[] connectionString, byte[] username, byte[] password, Guid userId, string modelConnectionName);

		// Token: 0x060000CD RID: 205
		Task<int> UpdateDataModelDataSourceAsync(Guid dataSourceId, DataModelDataSourceEntity.DataModelDataSourceAuthType authType, byte[] connectionString, byte[] username, byte[] password, Guid userId);

		// Token: 0x060000CE RID: 206
		Task<int> UpdateDataModelDataSourceConnectionNameAsync(Guid dataSourceId, string newModelConnectionName, byte[] connectionString);

		// Token: 0x060000CF RID: 207
		Task<int> DeleteDataModelDataSourceByIdAsync(Guid dataSourceId);
	}
}

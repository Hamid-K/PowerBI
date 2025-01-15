using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BIServer.Configuration.Catalog;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x02000027 RID: 39
	public class CatalogDataModelDataSourceAccessor : ICatalogDataModelDataSourceAccessor
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00006864 File Offset: 0x00004A64
		public async Task<IList<DataModelDataSourceEntity>> GetDataModelDataSourcesByItemAsync(Guid catalogItemId)
		{
			var <>f__AnonymousType = new
			{
				ItemID = catalogItemId
			};
			return await CatalogAccessFactory.QueryAsync<DataModelDataSourceEntity>("GetDataModelDataSourcesByItemID", <>f__AnonymousType);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000068AC File Offset: 0x00004AAC
		public async Task<IList<DataModelDataSourceEntity>> GetDataModelDataSourcesByItemIdAndModelAsync(Guid catalogItemId, string modelConnectionName)
		{
			var <>f__AnonymousType = new
			{
				ModelConnectionName = modelConnectionName,
				ItemId = catalogItemId
			};
			return await CatalogAccessFactory.QueryAsync<DataModelDataSourceEntity>("GetDataModelDataSourcesByItemIdAndModel", <>f__AnonymousType);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000068FC File Offset: 0x00004AFC
		public async Task<int> AddDataModelDataSourceAsync(Guid itemId, DataModelDataSourceEntity.DataModelDataSourceType dSType, DataModelDataSourceEntity.DataModelDataSourceKind dSKind, DataModelDataSourceEntity.DataModelDataSourceAuthType authType, byte[] connectionString, byte[] username, byte[] password, Guid userId, string modelConnectionName)
		{
			var <>f__AnonymousType = new
			{
				ItemID = itemId,
				DSType = dSType.ToString(),
				DSKind = dSKind.ToString(),
				AuthType = authType.ToString(),
				ConnectionString = connectionString,
				Username = username,
				Password = password,
				CreatedByID = userId,
				ModelConnectionName = modelConnectionName
			};
			return await CatalogAccessFactory.ExecuteAsync("AddDataModelDataSource", <>f__AnonymousType);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006988 File Offset: 0x00004B88
		public async Task<int> UpdateDataModelDataSourceAsync(Guid dataSourceId, DataModelDataSourceEntity.DataModelDataSourceAuthType authType, byte[] connectionString, byte[] username, byte[] password, Guid userId)
		{
			var <>f__AnonymousType = new
			{
				DataSourceID = dataSourceId,
				AuthType = authType.ToString(),
				ConnectionString = connectionString,
				Username = username,
				Password = password,
				ModifiedByID = userId
			};
			return await CatalogAccessFactory.ExecuteAsync("UpdateDataModelDataSourceByID", <>f__AnonymousType);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000069F8 File Offset: 0x00004BF8
		public async Task<int> DeleteDataModelDataSourceByIdAsync(Guid dataSourceId)
		{
			var <>f__AnonymousType = new
			{
				DataSourceID = dataSourceId
			};
			return await CatalogAccessFactory.ExecuteAsync("DeleteDataModelDataSourceByID", <>f__AnonymousType);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006A40 File Offset: 0x00004C40
		public async Task<int> UpdateDataModelDataSourceConnectionNameAsync(Guid dataSourceId, string newModelConnectionName, byte[] connectionString)
		{
			var <>f__AnonymousType = new
			{
				DataSourceID = dataSourceId,
				ModelConnectionName = newModelConnectionName,
				ConnectionString = connectionString
			};
			return await CatalogAccessFactory.ExecuteAsync("UpdateDataModelDataSourceConnectionName", <>f__AnonymousType);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.BIServer.Configuration.Catalog;
using Microsoft.BIServer.HostingEnvironment.Storage;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x02000026 RID: 38
	public class CatalogDataModelRoleAccessor : ICatalogDataModelRoleAccessor
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00006644 File Offset: 0x00004844
		public async Task<IList<DataModelRoleEntity>> GetDataModelRolesByItemAsync(Guid catalogItemId)
		{
			var <>f__AnonymousType = new
			{
				ItemID = catalogItemId
			};
			return await CatalogAccessFactory.QueryAsync<DataModelRoleEntity>("GetDataModelRolesByItemID", <>f__AnonymousType);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000668C File Offset: 0x0000488C
		public async Task<int> AddDataModelRoleAsync(Guid catalogItemId, Guid modelRoleId, string modelRoleName)
		{
			var <>f__AnonymousType = new
			{
				ItemID = catalogItemId,
				ModelRoleId = modelRoleId,
				ModelRoleName = modelRoleName
			};
			return await CatalogAccessFactory.ExecuteAsync("AddDataModelRole", <>f__AnonymousType);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000066E4 File Offset: 0x000048E4
		public async Task<int> UpdateDataModelRoleAsync(long dataModelRoleId, string modelRoleName)
		{
			var <>f__AnonymousType = new
			{
				DataModelRoleId = dataModelRoleId,
				ModelRoleName = modelRoleName
			};
			return await CatalogAccessFactory.ExecuteAsync("UpdateDataModelRoleByID", <>f__AnonymousType);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006734 File Offset: 0x00004934
		public async Task<int> DeleteDataModelRoleByIdAsync(long dataModelRoleId)
		{
			var <>f__AnonymousType = new
			{
				DataModelRoleId = dataModelRoleId
			};
			return await CatalogAccessFactory.ExecuteAsync("DeleteDataModelRoleByID", <>f__AnonymousType);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000677C File Offset: 0x0000497C
		public async Task<IList<DataModelRoleAssignmentEntity>> GetDataModelRoleAssignmentsByItemAsync(Guid catalogItemId)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("@ItemID", catalogItemId);
			List<DataModelRoleAssignmentEntity> dataModelRoleAssignments = new List<DataModelRoleAssignmentEntity>();
			using (MeteredSqlConnection connection = CatalogAccessFactory.NewConnection())
			{
				using (DbDataReader dbDataReader = await connection.ExecuteReaderAsync("GetDataModelRoleAssignmentsByItemID", dictionary, CommandBehavior.SingleResult))
				{
					Guid guid = Guid.Empty;
					DataModelRoleAssignmentEntity dataModelRoleAssignmentEntity = null;
					while (dbDataReader.Read())
					{
						Guid guid2 = dbDataReader.GetGuid(0);
						if (!guid2.Equals(guid))
						{
							dataModelRoleAssignmentEntity = new DataModelRoleAssignmentEntity
							{
								UserId = guid2,
								UserName = dbDataReader.GetString(1),
								DataModelRoles = new List<DataModelRoleEntity>()
							};
							dataModelRoleAssignments.Add(dataModelRoleAssignmentEntity);
							guid = guid2;
						}
						DataModelRoleEntity dataModelRoleEntity = new DataModelRoleEntity();
						dataModelRoleEntity.DataModelRoleId = dbDataReader.GetInt64(2);
						dataModelRoleEntity.ModelRoleId = dbDataReader.GetGuid(3);
						dataModelRoleEntity.ModelRoleName = dbDataReader.GetString(4);
						dataModelRoleAssignmentEntity.DataModelRoles.Add(dataModelRoleEntity);
					}
				}
			}
			MeteredSqlConnection connection = null;
			return dataModelRoleAssignments;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000067C4 File Offset: 0x000049C4
		public async Task<int> AddDataModelRoleAssignmentAsync(Guid userId, long dataModelRoleId)
		{
			var <>f__AnonymousType = new
			{
				UserId = userId,
				DataModelRoleId = dataModelRoleId
			};
			return await CatalogAccessFactory.ExecuteAsync("AddUserDataModelRole", <>f__AnonymousType);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006814 File Offset: 0x00004A14
		public async Task<int> DeleteDataModelRoleAssignmentAsync(Guid userId, long dataModelRoleId)
		{
			var <>f__AnonymousType = new
			{
				UserId = userId,
				DataModelRoleId = dataModelRoleId
			};
			return await CatalogAccessFactory.ExecuteAsync("DeleteUserDataModelRole", <>f__AnonymousType);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.ReportingServices.Interfaces;
using Model;

namespace Microsoft.ReportingServices.Portal.Interfaces.Repositories
{
	// Token: 0x02000094 RID: 148
	public interface ICatalogRepository
	{
		// Token: 0x0600046C RID: 1132
		IQueryable<CatalogItem> GetCatalogItems(IPrincipal userPrincipal);

		// Token: 0x0600046D RID: 1133
		IQueryable<T> GetCatalogItems<T>(IPrincipal userPrincipal) where T : CatalogItem;

		// Token: 0x0600046E RID: 1134
		IQueryable<T> GetCatalogItems<T>(IPrincipal userPrincipal, string path) where T : CatalogItem;

		// Token: 0x0600046F RID: 1135
		IEnumerable<CatalogItem> TraverseFolder(IPrincipal userPrincipal, string itemPath, bool recursive);

		// Token: 0x06000470 RID: 1136
		IEnumerable<T> TraverseFolder<T>(IPrincipal userPrincipal, string itemPath, bool recursive) where T : CatalogItem;

		// Token: 0x06000471 RID: 1137
		IQueryable<Role> GetCatalogRoles(IPrincipal userPrincipal);

		// Token: 0x06000472 RID: 1138
		ItemPolicy GetItemPolicy(IPrincipal userPrincipal, string path);

		// Token: 0x06000473 RID: 1139
		void SetItemPolicy(IPrincipal userPrincipal, string path, ItemPolicy itemPolicy);

		// Token: 0x06000474 RID: 1140
		CatalogItem CreateFolder(IPrincipal userPrincipal, string path, string name);

		// Token: 0x06000475 RID: 1141
		CatalogItem GetCatalogItem(IPrincipal userPrincipal, Guid key);

		// Token: 0x06000476 RID: 1142
		CatalogItem GetCatalogItem(IPrincipal userPrincipal, string path);

		// Token: 0x06000477 RID: 1143
		CatalogItemType GetCatalogItemTypeByGuid(IPrincipal userPrincipal, Guid id);

		// Token: 0x06000478 RID: 1144
		string GetCatalogItemDownloadFileName(CatalogItem item);

		// Token: 0x06000479 RID: 1145
		CatalogItem GetCatalogItemWithContent(IPrincipal userPrincipal, Guid key);

		// Token: 0x0600047A RID: 1146
		CatalogItem GetCatalogItemWithContent(IPrincipal userPrincipal, string path);

		// Token: 0x0600047B RID: 1147
		CatalogItem GetCatalogItemWithContentTrusted(IPrincipal userPrincipal, Guid key);

		// Token: 0x0600047C RID: 1148
		CatalogItem GetCatalogItemWithContentTrusted(IPrincipal userPrincipal, string path);

		// Token: 0x0600047D RID: 1149
		IQueryable<Property> GetItemProperties(IPrincipal userPrincipal, string path, IEnumerable<Property> requestedProperties);

		// Token: 0x0600047E RID: 1150
		IQueryable<Property> GetItemProperties(IPrincipal userPrincipal, Guid guid, IEnumerable<Property> requestedProperties);

		// Token: 0x0600047F RID: 1151
		Guid? GetCatalogItemGuidByPath(IPrincipal userPrincipal, string path);

		// Token: 0x06000480 RID: 1152
		void SetItemProperties(IPrincipal userPrincipal, string path, IEnumerable<Property> properties);

		// Token: 0x06000481 RID: 1153
		void SetItemPropertiesTrusted(IPrincipal userPrincipal, string path, IEnumerable<Property> properties);

		// Token: 0x06000482 RID: 1154
		bool Create(IPrincipal userPrincipal, CatalogItem catalogItem, out CatalogItem createdItem);

		// Token: 0x06000483 RID: 1155
		bool Create<T>(IPrincipal userPrincipal, T catalogItem, out T createdItem) where T : CatalogItem;

		// Token: 0x06000484 RID: 1156
		bool Update(IPrincipal userPrincipal, Guid key, CatalogItem catalogItem, string[] delta);

		// Token: 0x06000485 RID: 1157
		bool Update(IPrincipal userPrincipal, string path, CatalogItem catalogItem, string[] delta);

		// Token: 0x06000486 RID: 1158
		bool Delete(IPrincipal userPrincipal, Guid key);

		// Token: 0x06000487 RID: 1159
		bool Delete(IPrincipal userPrincipal, string path);

		// Token: 0x06000488 RID: 1160
		IList<ReportParameterDefinition> GetSimpleParameterDefinitions(IPrincipal userPrincipal, string reportpath);

		// Token: 0x06000489 RID: 1161
		IList<ReportParameterDefinition> GetReportParameterDefinitionsWithQuery(IPrincipal userPrincipal, string reportpath);

		// Token: 0x0600048A RID: 1162
		IEnumerable<ReportParameterDefinition> GetReportParameterDefinitionsWithQueryAndCurrentValues(IPrincipal userPrincipal, string reportpath, IEnumerable<ParameterValue> parameterValues);

		// Token: 0x0600048B RID: 1163
		void UpdateReportParameterDefinition(IPrincipal userPrincipal, string reportpath, IEnumerable<ReportParameterProperties> parameterProperties);

		// Token: 0x0600048C RID: 1164
		void UpdateReportParameterDefinition(IPrincipal userPrincipal, string reportpath, IEnumerable<ReportParameterDefinitionPatch> parameterProperties);

		// Token: 0x0600048D RID: 1165
		bool AddToFavorites(IPrincipal userPrincipal, Guid id);

		// Token: 0x0600048E RID: 1166
		bool RemoveFromFavorites(IPrincipal userPrincipal, Guid id);

		// Token: 0x0600048F RID: 1167
		IQueryable<CatalogItem> GetFavoriteItems(IPrincipal userPrincipal);

		// Token: 0x06000490 RID: 1168
		IQueryable<CatalogItem> GetDependentItems(IPrincipal userPrincipal, Guid key);

		// Token: 0x06000491 RID: 1169
		IQueryable<CatalogItem> GetDependentItems(IPrincipal userPrincipal, string path);

		// Token: 0x06000492 RID: 1170
		IQueryable<CatalogItem> SearchItems(IPrincipal userPrincipal, Guid key, string searchText);

		// Token: 0x06000493 RID: 1171
		IQueryable<CatalogItem> SearchItems(IPrincipal userPrincipal, string path, string searchText);

		// Token: 0x06000494 RID: 1172
		BulkOperationsResult DeleteItems(IPrincipal userPrincipal, IEnumerable<string> catalogItemPaths);

		// Token: 0x06000495 RID: 1173
		BulkOperationsResult MoveItems(IPrincipal userPrincipal, IEnumerable<string> catalogItemPaths, string targetPath);

		// Token: 0x06000496 RID: 1174
		CatalogItemAccessToken CreateCatalogItemAccessToken(IEncryptionService encryptionService, IPrincipal userPrincipal, Guid key);

		// Token: 0x06000497 RID: 1175
		DataSet GetDataSet(IPrincipal userPrincipal, Guid key);

		// Token: 0x06000498 RID: 1176
		DataSet GetDataSet(IPrincipal userPrincipal, string path);

		// Token: 0x06000499 RID: 1177
		void SetItemDataSets(IPrincipal principal, string itemPath, IEnumerable<DataSet> dataSets);

		// Token: 0x0600049A RID: 1178
		IList<DataSet> GetDataSetsForCatalogItem(IPrincipal userPrincipal, string path);

		// Token: 0x0600049B RID: 1179
		List<Subscription> GetSubscriptions(IPrincipal userPrincipal, string itemPath);

		// Token: 0x0600049C RID: 1180
		List<Subscription> GetSubscriptionsUsingDataSource(IPrincipal userPrincipal, string itemPath);

		// Token: 0x0600049D RID: 1181
		List<SubscriptionHistory> GetSubscriptionsHistory(Guid key);

		// Token: 0x0600049E RID: 1182
		List<CacheRefreshPlan> GetCacheRefreshPlans(IPrincipal userPrincipal, string itemPath);

		// Token: 0x0600049F RID: 1183
		List<CacheRefreshPlan> GetCacheRefreshPlansForPowerBIReport(IPrincipal userPrincipal, string itemPath);

		// Token: 0x060004A0 RID: 1184
		void SetItemDataSources(IPrincipal userPrincipal, string itemPath, IEnumerable<DataSource> dataSources);

		// Token: 0x060004A1 RID: 1185
		void SetItemDataSourcesTrusted(IPrincipal userPrincipal, string itemPath, IEnumerable<DataSource> dataSources);

		// Token: 0x060004A2 RID: 1186
		DataSource GetDataSource(IPrincipal userPrincipal, Guid key);

		// Token: 0x060004A3 RID: 1187
		DataSource GetDataSource(IPrincipal userPrincipal, string path);

		// Token: 0x060004A4 RID: 1188
		DataSourceCheckResult TestDataSource(IPrincipal userPrincipal, DataSource dataSource);

		// Token: 0x060004A5 RID: 1189
		DataSourceCheckResult TestDataSource(IPrincipal userPrincipal, Guid key);

		// Token: 0x060004A6 RID: 1190
		DataSourceCheckResult TestDataSource(IPrincipal userPrincipal, Guid key, string dataSourceName);

		// Token: 0x060004A7 RID: 1191
		IList<DataSource> GetDataSourcesForCatalogItem(IPrincipal userPrincipal, string path);

		// Token: 0x060004A8 RID: 1192
		IEnumerable<string> GetQueryFields(IPrincipal userPrincipal, DataSource datasource, Query query);

		// Token: 0x060004A9 RID: 1193
		byte[] GetDataSourcePasswordForSubscription(IPrincipal userPrincipal, Guid key);

		// Token: 0x060004AA RID: 1194
		DataRetrievalPlan GetDataRetrievalPlanFromCatalog(IPrincipal userPrincipal, Guid key);

		// Token: 0x060004AB RID: 1195
		CacheOptions GetItemCacheOptions(IPrincipal userPrincipal, string path);

		// Token: 0x060004AC RID: 1196
		void SetItemCacheOptions(IPrincipal userPrincipal, string path, CacheOptions cacheOptions);

		// Token: 0x060004AD RID: 1197
		List<ReportHistorySnapshot> GetReportHistorySnapshots(IPrincipal userPrincipal, string path);

		// Token: 0x060004AE RID: 1198
		List<HistorySnapshot> GetHistorySnapshots(IPrincipal userPrincipal, string path);

		// Token: 0x060004AF RID: 1199
		string CreateItemHistorySnapshot(IPrincipal userPrincipal, string path);

		// Token: 0x060004B0 RID: 1200
		bool DeleteItemHistorySnapshot(IPrincipal userPrincipal, string path, string historyId);

		// Token: 0x060004B1 RID: 1201
		bool DeleteItemHistorySnapshotByHistoryId(IPrincipal userPrincipal, string path, string historyId);

		// Token: 0x060004B2 RID: 1202
		ItemHistoryOptions GetItemHistoryOptions(IPrincipal userPrincipal, string path);

		// Token: 0x060004B3 RID: 1203
		Guid GetItemIdFromHistoryId(Guid historyId);

		// Token: 0x060004B4 RID: 1204
		ReportHistorySnapshotsOptions GetReportHistorySnapshotsOptions(IPrincipal userPrincipal, string path);

		// Token: 0x060004B5 RID: 1205
		void SetReportHistorySnapshotOptions(IPrincipal userPrincipal, string path, ReportHistorySnapshotsOptions reportHistorySnapshotOptions);

		// Token: 0x060004B6 RID: 1206
		void SetLinkedReportLink(IPrincipal userPrincipal, LinkedReport item, string link);

		// Token: 0x060004B7 RID: 1207
		void UpdateItemExecutionSnapshot(IPrincipal userPrincipal, string path);

		// Token: 0x060004B8 RID: 1208
		List<string> GetAllowedActions(IPrincipal userPrincipal, string path);

		// Token: 0x060004B9 RID: 1209
		string GetMyReportsPath(IPrincipal userPrincipal);

		// Token: 0x060004BA RID: 1210
		Comment GetComment(IPrincipal userPrincipal, long id);

		// Token: 0x060004BB RID: 1211
		IList<Comment> GetCommentsByItem(IPrincipal userPrincipal, Guid catalogItemId);

		// Token: 0x060004BC RID: 1212
		bool CreateComment(IPrincipal userPrincipal, Comment entity, out Comment createdEntity);

		// Token: 0x060004BD RID: 1213
		bool UpdateComment(IPrincipal userPrincipal, Comment entity, string[] delta);

		// Token: 0x060004BE RID: 1214
		bool DeleteComment(IPrincipal userPrincipal, long id, bool checkManager);

		// Token: 0x060004BF RID: 1215
		bool AddExecutionLogInfo(IPrincipal userPrincipal, ExecutionLogInfo executionLog);

		// Token: 0x060004C0 RID: 1216
		Guid GetUserIdFromName(string userName, AuthenticationType authType);

		// Token: 0x060004C1 RID: 1217
		Guid GetUserId(string userName, AuthenticationType authType);

		// Token: 0x060004C2 RID: 1218
		UserSettings GetUserSettings(Guid userId);

		// Token: 0x060004C3 RID: 1219
		bool UpdateUserSettings(UserSettings settings);

		// Token: 0x060004C4 RID: 1220
		bool IsUserContextOwner(IPrincipal userPrincipal, long id);

		// Token: 0x060004C5 RID: 1221
		long GetEmailAlertSubscriptionId(Guid userId, Guid itemId, string alertType);

		// Token: 0x060004C6 RID: 1222
		bool AddEmailAlertSubscription(Guid userId, Guid itemId, string alertType, out AlertSubscription createdAlertSubscription);

		// Token: 0x060004C7 RID: 1223
		bool DeleteEmailAlertSubscription(long id);

		// Token: 0x060004C8 RID: 1224
		void SetDataModelDataSourcesTrusted(IPrincipal userPrincipal, Guid itemId, IEnumerable<DataSource> dataSources, bool isOverwrite);

		// Token: 0x060004C9 RID: 1225
		DataSource GetDataSourceForTestConnection(IPrincipal userPrincipal, Guid itemId, Guid dataSourceId);

		// Token: 0x060004CA RID: 1226
		Task UpdateDataModelDataSourcesAsync(IPrincipal userPrincipal, Guid itemId, List<DataSource> dataSources);

		// Token: 0x060004CB RID: 1227
		Task DeleteDataModelDataSourcesTrustedAsync(List<DataSource> dataSources);

		// Token: 0x060004CC RID: 1228
		void SetDataModelRolesTrusted(Guid itemId, IEnumerable<DataModelRole> dataSources, bool isOverwrite);

		// Token: 0x060004CD RID: 1229
		Task<IList<DataModelRole>> GetDataModelRolesAsync(IPrincipal userPrincipal, Guid itemId);

		// Token: 0x060004CE RID: 1230
		Task<IList<DataModelRoleAssignment>> GetDataModelRoleAssignmentsAsync(IPrincipal userPrincipal, Guid itemId);

		// Token: 0x060004CF RID: 1231
		Task UpdateDataModelRoleAssignmentsAsync(IPrincipal userPrincipal, Guid itemId, List<DataModelRoleAssignment> dataModelRoleAssignments);

		// Token: 0x060004D0 RID: 1232
		string GetDataModelParameters(IPrincipal userPrincipal, Guid id);

		// Token: 0x060004D1 RID: 1233
		void SetDataModelParameters(IPrincipal userPrincple, Guid id, string parameters);

		// Token: 0x060004D2 RID: 1234
		void UploadPowerBIReport(IPrincipal userPrincipal, PowerBIReport pbiReport);
	}
}

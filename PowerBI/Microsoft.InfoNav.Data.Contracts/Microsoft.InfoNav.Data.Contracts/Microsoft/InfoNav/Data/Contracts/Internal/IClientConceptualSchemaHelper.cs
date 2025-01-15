using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200016C RID: 364
	public interface IClientConceptualSchemaHelper
	{
		// Token: 0x06000951 RID: 2385
		QueryableState GetEntityQueryableState(string entityName);

		// Token: 0x06000952 RID: 2386
		QueryableState GetPropertyQueryableState(string entityName, string propertyName);

		// Token: 0x06000953 RID: 2387
		void GetColumnMetadata(string entityName, string propertyName, out bool calculated, out GroupsMetadata groupMetadata, out BinsMetadata binMetadata);

		// Token: 0x06000954 RID: 2388
		bool IsCalculatedColumn(string entityName, string propertyName);

		// Token: 0x06000955 RID: 2389
		bool IsCalculatedTable(string entityName);

		// Token: 0x06000956 RID: 2390
		bool HasDirectQueryContent();

		// Token: 0x06000957 RID: 2391
		bool IsCloudRlsModel();

		// Token: 0x06000958 RID: 2392
		bool IsPushDataModel();

		// Token: 0x06000959 RID: 2393
		bool IsRealTimeModel();

		// Token: 0x0600095A RID: 2394
		bool SupportsQnA();

		// Token: 0x0600095B RID: 2395
		bool IsQnaEnabled();

		// Token: 0x0600095C RID: 2396
		bool SupportsFastRefresh();

		// Token: 0x0600095D RID: 2397
		bool SupportsInsights();

		// Token: 0x0600095E RID: 2398
		InsightsCapabilities GetInsightsCapabilities();

		// Token: 0x0600095F RID: 2399
		ModelLocation GetModelLocation();

		// Token: 0x06000960 RID: 2400
		bool SupportsCalculatedColumns();

		// Token: 0x06000961 RID: 2401
		bool SupportsGrouping();

		// Token: 0x06000962 RID: 2402
		bool CanDelete(string entityName, string propertyName);

		// Token: 0x06000963 RID: 2403
		bool CanRefreshTable(string entityName);

		// Token: 0x06000964 RID: 2404
		bool CanEditTableSource(string entityName);

		// Token: 0x06000965 RID: 2405
		bool CanRenameTable(string entityName);

		// Token: 0x06000966 RID: 2406
		bool CanDeleteTable(string entityName);

		// Token: 0x06000967 RID: 2407
		ClientConceptualEntityMode GetMode(string entityName);

		// Token: 0x06000968 RID: 2408
		bool CanDeleteHierarchy(string entityName, string hierarchyName);

		// Token: 0x06000969 RID: 2409
		bool CanDeleteHierarchyLevel(string entityName, string hierarchyName, string hierarchyLevelName);

		// Token: 0x0600096A RID: 2410
		bool CanEditMeasure(string entityName, string measureName);

		// Token: 0x0600096B RID: 2411
		bool CanEditStorageMode(string entityName);

		// Token: 0x0600096C RID: 2412
		DateTime? GetRefreshedTime(string entityName);

		// Token: 0x0600096D RID: 2413
		void GetDirectQueryResourceInfo(string entityName, out string sourceType, out string sourceName);

		// Token: 0x0600096E RID: 2414
		DataViewCapabilities GetDataViewCapabilities(string entityName);

		// Token: 0x0600096F RID: 2415
		bool CanEditChangeDetectionMeasure();

		// Token: 0x06000970 RID: 2416
		bool SupportChangeDetectionMeasureRefresh();
	}
}

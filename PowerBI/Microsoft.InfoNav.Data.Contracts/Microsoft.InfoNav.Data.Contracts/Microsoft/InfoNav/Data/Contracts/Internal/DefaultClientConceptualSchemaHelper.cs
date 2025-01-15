using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200016B RID: 363
	public sealed class DefaultClientConceptualSchemaHelper : IClientConceptualSchemaHelper
	{
		// Token: 0x06000930 RID: 2352 RVA: 0x00013378 File Offset: 0x00011578
		public DefaultClientConceptualSchemaHelper(bool isDirectQueryMode, bool isCloudRlsModel, bool isPushDataModel, bool isRealTimeModel, ModelLocation modelLocation, bool modelSupportsQna, InsightsCapabilities insightsCapabilities, bool supportChangeDetectionMeasureRefresh = false, bool supportsFastRefresh = true, bool isQnaEnabled = false)
		{
			this._isDirectQueryMode = isDirectQueryMode;
			this._isCloudRlsModel = isCloudRlsModel;
			this._isPushDataModel = isPushDataModel;
			this._isRealTimeModel = isRealTimeModel;
			this._modelLocation = modelLocation;
			this._modelSupportsQna = modelSupportsQna;
			this._isQnaEnabled = isQnaEnabled;
			this._insightsCapabilities = insightsCapabilities;
			this._supportChangeDetectionMeasureRefresh = supportChangeDetectionMeasureRefresh;
			this._supportsInsights = insightsCapabilities != null;
			this._supportsFastRefresh = supportsFastRefresh;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000133E3 File Offset: 0x000115E3
		public bool HasDirectQueryContent()
		{
			return this._isDirectQueryMode;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x000133EB File Offset: 0x000115EB
		public ModelLocation GetModelLocation()
		{
			return this._modelLocation;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x000133F3 File Offset: 0x000115F3
		public bool IsCloudRlsModel()
		{
			return this._isCloudRlsModel;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x000133FB File Offset: 0x000115FB
		public bool IsPushDataModel()
		{
			return this._isPushDataModel;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00013403 File Offset: 0x00011603
		public bool IsRealTimeModel()
		{
			return this._isRealTimeModel;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0001340B File Offset: 0x0001160B
		public bool SupportsQnA()
		{
			return this._modelSupportsQna;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00013413 File Offset: 0x00011613
		public bool IsQnaEnabled()
		{
			return this._isQnaEnabled;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001341B File Offset: 0x0001161B
		public bool SupportsFastRefresh()
		{
			return this._supportsFastRefresh;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00013423 File Offset: 0x00011623
		public bool SupportsInsights()
		{
			return this._supportsInsights;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001342B File Offset: 0x0001162B
		public InsightsCapabilities GetInsightsCapabilities()
		{
			return this._insightsCapabilities;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00013433 File Offset: 0x00011633
		public void GetColumnMetadata(string entityName, string propertyName, out bool calculated, out GroupsMetadata groupMetadata, out BinsMetadata binMetadata)
		{
			calculated = false;
			groupMetadata = null;
			binMetadata = null;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00013440 File Offset: 0x00011640
		public QueryableState GetEntityQueryableState(string entityName)
		{
			return new QueryableState(ClientConceptualQueryableState.Queryable, null);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00013449 File Offset: 0x00011649
		public QueryableState GetPropertyQueryableState(string entityName, string propertyName)
		{
			return new QueryableState(ClientConceptualQueryableState.Queryable, null);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00013452 File Offset: 0x00011652
		public bool IsCalculatedColumn(string entityName, string propertyName)
		{
			return false;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00013455 File Offset: 0x00011655
		public bool IsCalculatedTable(string entityName)
		{
			return false;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00013458 File Offset: 0x00011658
		public bool CanDelete(string entityName, string propertyName)
		{
			return true;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001345B File Offset: 0x0001165B
		public bool CanDeleteHierarchy(string entityName, string hierarchyName)
		{
			return true;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001345E File Offset: 0x0001165E
		public bool CanDeleteHierarchyLevel(string entityName, string hierarchyName, string hierarchyLevelName)
		{
			return true;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00013461 File Offset: 0x00011661
		public ClientConceptualEntityMode GetMode(string entityName)
		{
			return ClientConceptualEntityMode.Unknown;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00013464 File Offset: 0x00011664
		public bool CanDeleteTable(string entityName)
		{
			return true;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00013467 File Offset: 0x00011667
		public bool CanEditTableSource(string entityName)
		{
			return true;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001346A File Offset: 0x0001166A
		public bool CanRefreshTable(string entityName)
		{
			return true;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001346D File Offset: 0x0001166D
		public bool CanRenameTable(string entityName)
		{
			return true;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00013470 File Offset: 0x00011670
		public bool SupportsCalculatedColumns()
		{
			return true;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00013473 File Offset: 0x00011673
		public bool SupportsGrouping()
		{
			return true;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00013476 File Offset: 0x00011676
		public bool CanEditMeasure(string entityName, string measureName)
		{
			return true;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00013479 File Offset: 0x00011679
		public bool CanEditStorageMode(string entityName)
		{
			return true;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001347C File Offset: 0x0001167C
		public bool CanEditChangeDetectionMeasure()
		{
			return false;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001347F File Offset: 0x0001167F
		public bool SupportChangeDetectionMeasureRefresh()
		{
			return this._supportChangeDetectionMeasureRefresh;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00013488 File Offset: 0x00011688
		public DateTime? GetRefreshedTime(string entityName)
		{
			return null;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001349E File Offset: 0x0001169E
		public void GetDirectQueryResourceInfo(string entityName, out string sourceType, out string sourceName)
		{
			sourceType = null;
			sourceName = null;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000134A6 File Offset: 0x000116A6
		public DataViewCapabilities GetDataViewCapabilities(string entityName)
		{
			return null;
		}

		// Token: 0x04000538 RID: 1336
		private readonly bool _isDirectQueryMode;

		// Token: 0x04000539 RID: 1337
		private readonly bool _isCloudRlsModel;

		// Token: 0x0400053A RID: 1338
		private readonly bool _isPushDataModel;

		// Token: 0x0400053B RID: 1339
		private readonly bool _isRealTimeModel;

		// Token: 0x0400053C RID: 1340
		private readonly bool _modelSupportsQna;

		// Token: 0x0400053D RID: 1341
		private readonly bool _isQnaEnabled;

		// Token: 0x0400053E RID: 1342
		private readonly bool _supportsInsights;

		// Token: 0x0400053F RID: 1343
		private readonly ModelLocation _modelLocation;

		// Token: 0x04000540 RID: 1344
		private readonly InsightsCapabilities _insightsCapabilities;

		// Token: 0x04000541 RID: 1345
		private readonly bool _supportsFastRefresh;

		// Token: 0x04000542 RID: 1346
		private readonly bool _supportChangeDetectionMeasureRefresh;
	}
}

using System;
using System.Collections.Specialized;
using System.Data;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.Library.Soap2010;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000045 RID: 69
	internal interface IRSService
	{
		// Token: 0x0600027B RID: 635
		void AbortTransaction();

		// Token: 0x0600027C RID: 636
		void AddDataSets(Guid itemID, DataSetInfoCollection dataSets, string editSessionID);

		// Token: 0x0600027D RID: 637
		void AddDataSources(Guid itemID, DataSourceInfoCollection dataSources, string editSessionID);

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600027E RID: 638
		AddToFavoritesAction AddToFavoritesAction { get; }

		// Token: 0x0600027F RID: 639
		ModelSnapshot AllocateNewModelSnapshot(DateTime createdDate, string description);

		// Token: 0x06000280 RID: 640
		ReportSnapshot AllocateNewReportSnapshot(bool isPermanentSnapshot, ParameterInfoCollection parameters, DateTime createdDate, string description, ReportProcessingFlags processingFlags);

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000281 RID: 641
		// (set) Token: 0x06000282 RID: 642
		bool AllowEditSessionItemPaths { get; set; }

		// Token: 0x06000283 RID: 643
		void CancelBatch(Guid batchId);

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000284 RID: 644
		CancelJobAction CancelJobAction { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000285 RID: 645
		CatalogItemFactory CatalogItemFactory { get; }

		// Token: 0x06000286 RID: 646
		ExternalItemPath CatalogToExternal(CatalogItemPath source, bool noThrow);

		// Token: 0x06000287 RID: 647
		ExternalItemPath CatalogToExternal(CatalogItemPath source, int externalRootZone, bool noThrow);

		// Token: 0x06000288 RID: 648
		ExternalItemPath CatalogToExternal(string source, bool noThrow);

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000289 RID: 649
		ChangeDataSourceStateAction ChangeDataSourceStateAction { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600028A RID: 650
		ChangeSubscriptionOwnerAction ChangeSubscriptionOwnerAction { get; }

		// Token: 0x0600028B RID: 651
		DataSourceInfo CheckDataSourcePublishingCallback(string itemPath, out Guid catalogItemId);

		// Token: 0x0600028C RID: 652
		DataSourceInfoCollection CombineDataSources(DataSetInfoCollection dataSets, DataSourceInfoCollection existingDataSources);

		// Token: 0x0600028D RID: 653
		void ConnectStorage(Storage storage);

		// Token: 0x0600028E RID: 654
		CatalogItemContext ConstructItemContext(string path, bool allowEditSession);

		// Token: 0x0600028F RID: 655
		CatalogItemContext ConstructItemContext(string path, bool allowEditSession, string parameterName);

		// Token: 0x06000290 RID: 656
		void ConvertToIntermediate(byte[] definition, bool usePermanentSnapshot, ItemProperties properties, CatalogItemContext reportContext, DateTime currentDate, bool checkAccessForSharedDatasources, ReportProcessingFlags previousProcessingFlags, bool isInternalRepublish, bool isRdlx, out ReportSnapshot intermediateSnapshot, out ParameterInfoCollection parameters, out Warning[] warnings, out DataSourceInfoCollection dataSources, out DataSetInfoCollection dataSets, out PageProperties pageProperties, out byte[] dataCacheHash);

		// Token: 0x06000291 RID: 657
		void ConvertToIntermediate(byte[] definition, bool usePermanentSnapshot, ItemProperties properties, CatalogItemContext reportContext, DateTime currentDate, bool checkAccessForSharedDatasources, bool resolveTemporaryDataSource, DataSourceInfoCollection originalDataSources, DataSetInfoCollection originalDataSets, ReportProcessingFlags previousProcessingFlags, bool isInternalRepublish, bool isRdlx, out ReportSnapshot intermediateSnapshot, out ParameterInfoCollection parameters, out Warning[] warnings, out DataSourceInfoCollection dataSources, out DataSetInfoCollection dataSets, out PageProperties pageProperties, out byte[] dataCacheHash);

		// Token: 0x06000292 RID: 658
		ServerDataExtensionConnectionWrapper CreateAndOpenDataExtensionConnectionWrapper(CatalogItemContext dataSourceItemContext, DataSourceInfo dataSourceInfo, IDbConnectionPool connectionPool);

		// Token: 0x06000293 RID: 659
		Guid CreateBatch();

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000294 RID: 660
		CreateCacheRefreshPlanAction CreateCacheRefreshPlanAction { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000295 RID: 661
		CreateComponentAction CreateComponentAction { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000296 RID: 662
		CreateDataSetAction CreateDataSetAction { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000297 RID: 663
		CreateDataSourceAction CreateDataSourceAction { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000298 RID: 664
		CreateReportEditSessionAction CreateEditSessionAction { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000299 RID: 665
		CreateFolderAction CreateFolderAction { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600029A RID: 666
		CreateKpiAction CreateKpiAction { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600029B RID: 667
		CreateLinkedReportAction CreateLinkedReportAction { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600029C RID: 668
		CreateModelAction CreateModelAction { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600029D RID: 669
		UploadPowerBIReportAction UploadPowerBiReportAction { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600029E RID: 670
		CreateRdlxReportAction CreateRdlxReportAction { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600029F RID: 671
		CreateReportAction CreateReportAction { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002A0 RID: 672
		CreateResourceAction CreateResourceAction { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002A1 RID: 673
		CreateRoleAction CreateRoleAction { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002A2 RID: 674
		CreateScheduleAction CreateScheduleAction { get; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002A3 RID: 675
		CreateSnapshotAction CreateSnapshotAction { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002A4 RID: 676
		CreateSubscriptionAction CreateSubscriptionAction { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002A5 RID: 677
		DeleteItemAction DeleteItemAction { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002A6 RID: 678
		DeletePoliciesAction DeletePoliciesAction { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002A7 RID: 679
		DeleteRoleAction DeleteRoleAction { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002A8 RID: 680
		DeleteScheduleAction DeleteScheduleAction { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002A9 RID: 681
		DeleteSnapshotAction DeleteSnapshotAction { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002AA RID: 682
		DeleteHistorySnapshotAction DeleteHistorySnapshotAction { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002AB RID: 683
		DeleteSubscriptionAction DeleteSubscriptionAction { get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002AC RID: 684
		DisableSubscriptionAction DisableSubscriptionAction { get; }

		// Token: 0x060002AD RID: 685
		void DisconnectStorage();

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002AE RID: 686
		EnableSubscriptionAction EnableSubscriptionAction { get; }

		// Token: 0x060002AF RID: 687
		void EnsureAllowedAsSubitem(ItemType parentType, ItemType childType, byte[] secDesc, ExternalItemPath parent, string item);

		// Token: 0x060002B0 RID: 688
		void EnsureAllowedToEditItem(ItemType itemType, byte[] secDesc, ExternalItemPath itemPath, string itemName);

		// Token: 0x060002B1 RID: 689
		Microsoft.ReportingServices.Library.CatalogItem EnsureCacheRefreshPlanIsAllowed(string itemPath);

		// Token: 0x060002B2 RID: 690
		void EnsureCachingIsEnabled(CatalogItemContext itemContext);

		// Token: 0x060002B3 RID: 691
		void EnsureMyReportsExists();

		// Token: 0x060002B4 RID: 692
		void EnsureSecurityZone(string itemPath);

		// Token: 0x060002B5 RID: 693
		void EnsureSharePointServicesAccessible();

		// Token: 0x060002B6 RID: 694
		void EnsureSupportedEditionForSharePoint();

		// Token: 0x060002B7 RID: 695
		void EnsureValidDatabase();

		// Token: 0x060002B8 RID: 696
		void EnsureValidMimeType(string mimeType);

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002B9 RID: 697
		EventManager EventManager { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002BA RID: 698
		ReportExecutionCacheDb ExecCacheDb { get; }

		// Token: 0x060002BB RID: 699
		void ExecuteBatch(Guid batchId);

		// Token: 0x060002BC RID: 700
		void ExecuteNestedTransaction(Action<RSService> action);

		// Token: 0x060002BD RID: 701
		T ExecuteNestedTransaction<T>(Func<RSService, T> func);

		// Token: 0x060002BE RID: 702
		RSStream ExecuteQuery(ExternalItemPath modelName, string query, NameValueCollection parameters, int timeout, IDbConnectionPool connectionPool);

		// Token: 0x060002BF RID: 703
		string ExternalToCatalog(string source, bool noThrow);

		// Token: 0x060002C0 RID: 704
		CatalogItemPath ExternalToCatalogItemPath(ExternalItemPath source);

		// Token: 0x060002C1 RID: 705
		CatalogItemList FindItems(string folder, BooleanOperatorEnum operation, Property[] options, Microsoft.ReportingServices.Library.Soap2010.SearchCondition[] properties, ServerCompatLevel compatLevel);

		// Token: 0x060002C2 RID: 706
		CatalogItemList FindItems(string folder, string searchText);

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002C3 RID: 707
		FireEventAction FireEventAction { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002C4 RID: 708
		FlushCacheAction FlushCacheAction { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002C5 RID: 709
		GenerateModelAction GenerateModelAction { get; }

		// Token: 0x060002C6 RID: 710
		RuntimeDataSourceInfoCollection GetAllDataSources(BaseReportCatalogItem report);

		// Token: 0x060002C7 RID: 711
		void GetAllDataSources(BaseReportCatalogItem report, bool checkIfUsable, bool useServiceConnectionForRepublishing, out ReportSnapshot compiledDefinition, out RuntimeDataSourceInfoCollection runtimeDataSources, out RuntimeDataSetInfoCollection runtimeDataSets);

		// Token: 0x060002C8 RID: 712
		void GetAllDataSources(ClientRequest session, ReportProcessing repProc, CatalogItemContext reportContext, ReportSnapshot intermediateSnapshot, DataSourceInfoCollection thisReportDataSources, DataSetInfoCollection thisReportDataSets, out RuntimeDataSourceInfoCollection runtimeDataSources, out RuntimeDataSetInfoCollection runtimeDataSets);

		// Token: 0x060002C9 RID: 713
		void GetAllDataSources(ReportProcessing repProc, CatalogItemContext reportContext, ReportSnapshot intermediateSnapshot, DataSourceInfoCollection thisReportDataSources, DataSetInfoCollection thisReportDataSets, out RuntimeDataSourceInfoCollection runtimeDataSources, out RuntimeDataSetInfoCollection runtimeDataSets);

		// Token: 0x060002CA RID: 714
		void GetBatchSettingsForScheduleDefinition(ScheduleDefinitionOrReference scheduleData, out string typeString, out string stringScheduleData);

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002CB RID: 715
		GetCacheOptionsAction GetCacheOptionsAction { get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002CC RID: 716
		GetCacheRefreshPlanPropertiesAction GetCacheRefreshPlanPropertiesAction { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002CD RID: 717
		GetComponentDefinitionAction GetComponentDefinitionAction { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002CE RID: 718
		GetDataSetDefinitionAction GetDataSetDefinitionAction { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002CF RID: 719
		GetDataSetItemReferencesAction GetDataSetItemReferencesAction { get; }

		// Token: 0x060002D0 RID: 720
		ParameterInfoCollection GetDataSetParameters(ItemParameterDefinition parameterDefinition, NameValueCollection requestParameterValues, JobType jobType);

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002D1 RID: 721
		GetDataSetParametersAction GetDataSetParametersAction { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002D2 RID: 722
		GetDataSourceContentsAction GetDataSourceContentsAction { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002D3 RID: 723
		GetExcelWorkbookContentsAction GetExcelWorkbookContentsAction { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002D4 RID: 724
		GetExecutionOptionsAction GetExecutionOptionsAction { get; }

		// Token: 0x060002D5 RID: 725
		ExtensionParameter[] GetExtensionSettings(string extension);

		// Token: 0x060002D6 RID: 726
		Setting[] GetExtensionSettingsInternal(string extension);

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002D7 RID: 727
		GetItemDataSourcePromptsAction GetItemDataSourcePromptsAction { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002D8 RID: 728
		GetItemDataSourcesAction GetItemDataSourcesAction { get; }

		// Token: 0x060002D9 RID: 729
		ExternalItemPath GetItemLink(ExternalItemPath internalItemPath);

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002DA RID: 730
		GetItemTypeAction GetItemTypeAction { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002DB RID: 731
		GetKpiAction GetKpiAction { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002DC RID: 732
		GetModelDefinitionAction GetModelDefinitionAction { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002DD RID: 733
		GetModelItemPermissionsAction GetModelItemPermissionsAction { get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002DE RID: 734
		GetModelItemPoliciesAction GetModelItemPoliciesAction { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002DF RID: 735
		GetModelItemReferencesAction GetModelItemReferencesAction { get; }

		// Token: 0x060002E0 RID: 736
		RSService GetNewService();

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002E1 RID: 737
		GetPermissionsAction GetPermissionsAction { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002E2 RID: 738
		GetPoliciesAction GetPoliciesAction { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002E3 RID: 739
		GetPowerBIReportContentsAction GetPowerBIReportContentsAction { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002E4 RID: 740
		GetPropertiesAction GetPropertiesAction { get; }

		// Token: 0x060002E5 RID: 741
		byte[] GetRenderResource(CatalogItemContext itemContext, out string mimeType);

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002E6 RID: 742
		GetReportDefinitionAction GetReportDefinitionAction { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002E7 RID: 743
		GetReportHistoryOptionsAction GetReportHistoryOptionsAction { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002E8 RID: 744
		GetReportItemReferencesAction GetReportItemReferencesAction { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002E9 RID: 745
		GetReportLinkAction GetReportLinkAction { get; }

		// Token: 0x060002EA RID: 746
		ParameterInfoCollection GetReportParameterDefinitions(ClientRequest session, CatalogItemContext reportContext, bool forRendering);

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002EB RID: 747
		GetReportParametersAction GetReportParametersAction { get; }

		// Token: 0x060002EC RID: 748
		ParameterInfoCollection GetReportParametersForRendering(CatalogItemContext reportContext, Guid reportID, Guid linkID, DateTime snapshotExecutionDate, IStoredParameterSource storedParamSource, NameValueCollection values, RuntimeDataSourceInfoCollection allDataSources, RuntimeDataSetInfoCollection allDataSets, JobType jobType);

		// Token: 0x060002ED RID: 749
		void GetReportServerConfigInfo(bool scaleOut, out string[] machineNames, out string[] instanceNames, out string[] serviceAccountNames, out string[] reportServerUrlItems);

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002EE RID: 750
		GetReportServerConfigInfoAction GetReportServerConfigInfoAction { get; }

		// Token: 0x060002EF RID: 751
		int GetReportTimeout(ItemProperties properties);

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002F0 RID: 752
		GetResourceContentsAction GetResourceContentsAction { get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002F1 RID: 753
		GetRolePropertiesAction GetRolePropertiesAction { get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002F2 RID: 754
		GetSchedulePropertiesAction GetSchedulePropertiesAction { get; }

		// Token: 0x060002F3 RID: 755
		IRSStorage GetScopedStorage();

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002F4 RID: 756
		GetSnapshotLimitAction GetSnapshotLimitAction { get; }

		// Token: 0x060002F5 RID: 757
		string GetSubscriptionBatchXmlBlob(string id, string eventType, string subscriptionData, string description, string parameters, string extensionSettings, string dataSettings, string isDataDriven);

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002F6 RID: 758
		GetSubscriptionPropertiesAction GetSubscriptionPropertiesAction { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002F7 RID: 759
		GetSystemPermissionsAction GetSystemPermissionsAction { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002F8 RID: 760
		GetSystemPoliciesAction GetSystemPoliciesAction { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002F9 RID: 761
		GetSystemPropertiesAction GetSystemPropertiesAction { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002FA RID: 762
		GetUserModelAction GetUserModelAction { get; }

		// Token: 0x060002FB RID: 763
		RSStream GetUserModelStreamable(string modelPath, string perspectiveID);

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002FC RID: 764
		GetUserSettingsAction GetUserSettingsAction { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002FD RID: 765
		ReportProcessing.CreateDataExtensionInstance HowToCreateDataExtensionInstance { get; }

		// Token: 0x060002FE RID: 766
		void ImpersonateClient();

		// Token: 0x060002FF RID: 767
		void InitializeRdlChunkMapping();

		// Token: 0x06000300 RID: 768
		ParameterInfoCollection InternalGetDataSetParameters(ReportProcessing repProc, ItemParameterDefinition parameterDefinition, NameValueCollection requestParameters);

		// Token: 0x06000301 RID: 769
		ParameterInfoCollection InternalGetReportParametersForRendering(CatalogItemContext reportContext, Guid reportID, Guid linkID, DateTime snapshotExecutionDate, IStoredParameterSource storedParamSource, NameValueCollection values, RuntimeDataSourceInfoCollection allDataSources, RuntimeDataSetInfoCollection allDataSets);

		// Token: 0x06000302 RID: 770
		void InvalidateSubscription(Guid subscriptionID, InActiveFlags inactiveFlag, string status);

		// Token: 0x06000303 RID: 771
		bool IsSchedulerRunning();

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000304 RID: 772
		ListChildrenAction ListChildrenAction { get; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000305 RID: 773
		ListDependentItemsAction ListDependentItemsAction { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000306 RID: 774
		ListEventsAction ListEventsAction { get; }

		// Token: 0x06000307 RID: 775
		Microsoft.ReportingServices.Library.Soap2005.Extension[] ListExtensions(ExtensionTypeEnum type);

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000308 RID: 776
		ListFavoriteableItemsAction ListFavoriteableItemsAction { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000309 RID: 777
		ListHistoryAction ListHistoryAction { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600030A RID: 778
		ListHistorySnapshotsAction ListHistorySnapshotsAction { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600030B RID: 779
		ListModelItemChildrenAction ListModelItemChildrenAction { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600030C RID: 780
		ListModelPerspectivesAction ListModelPerspectivesAction { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600030D RID: 781
		ListParentsAction ListParentsAction { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600030E RID: 782
		ListRolesAction ListRolesAction { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600030F RID: 783
		ListRunningJobsAction ListRunningJobsAction { get; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000310 RID: 784
		ListScheduledReportsAction ListScheduledReportsAction { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000311 RID: 785
		ListSchedulesAction ListSchedulesAction { get; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000312 RID: 786
		ListSubscriptionsAction ListSubscriptionsAction { get; }

		// Token: 0x06000313 RID: 787
		SubscriptionImpl[] ListSubscriptionsUsingDataSource(string name);

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000314 RID: 788
		ListTasksAction ListTasksAction { get; }

		// Token: 0x06000315 RID: 789
		bool IsTrustedFileType(string fileName);

		// Token: 0x06000316 RID: 790
		bool IsTrustedContentType(string contentType);

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000317 RID: 791
		MoveItemAction MoveItemAction { get; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000318 RID: 792
		bool MyReportsEnabled { get; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000319 RID: 793
		string MyReportsRole { get; }

		// Token: 0x0600031A RID: 794
		Microsoft.ReportingServices.DataProcessing.IDbConnection OpenDataSourceConnection(DataSourceInfo dataSourceInfo, ReportProcessing.CreateDataExtensionInstance createDataExtensionInstanceFunction);

		// Token: 0x0600031B RID: 795
		Microsoft.ReportingServices.DataProcessing.IDbConnection OpenDataSourceConnection(DataSourceInfo dataSourceInfo, ReportProcessing.CreateDataExtensionInstance createDataExtensionInstanceFunction, bool isUnattendedExecution, bool unwrapConnection, string requestUserName, out global::System.Data.IDbConnection unwrappedConnection);

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600031C RID: 796
		PauseScheduleAction PauseScheduleAction { get; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600031D RID: 797
		string PhysicalMyReportsPath { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600031E RID: 798
		string PhysicalMyReportsPathSlash { get; }

		// Token: 0x0600031F RID: 799
		void PopulateAdditionalToken(string itemPath);

		// Token: 0x06000320 RID: 800
		Microsoft.ReportingServices.Library.Soap.DataSetDefinition PrepareQuery(Microsoft.ReportingServices.Library.Soap2005.DataSource dataSource, Microsoft.ReportingServices.Library.Soap.DataSetDefinition dataSet, out ReportParameter[] parameters, out bool changed);

		// Token: 0x06000321 RID: 801
		void ProcessingGetResource(ICatalogItemContext reportContextInterface, string imageUrl, out byte[] resource, out string mimeType, out bool registerExternalWarning, out bool registerInvalidSizeWarning);

		// Token: 0x06000322 RID: 802
		void ProcessingGetResource(ICatalogItemContext reportContextInterface, string imageUrl, bool alwaysThrowOnWebException, ExternalResourceAbortHelper abortHelper, out byte[] resource, out string mimeType, out bool registerExternalWarning, out bool registerInvalidSizeWarning);

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000323 RID: 803
		RSPropertyProvider PropertyProvider { get; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000324 RID: 804
		RdlChunkMapper RdlChunkMapper { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000325 RID: 805
		RegenerateModelAction RegenerateModelAction { get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000326 RID: 806
		RemoveAllModelItemPoliciesAction RemoveAllModelItemPoliciesAction { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000327 RID: 807
		RemoveFromFavoritesAction RemoveFromFavoritesAction { get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000328 RID: 808
		IRSRequestInspector RequestInspector { get; }

		// Token: 0x06000329 RID: 809
		void ResetRdlChunkMapping();

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600032A RID: 810
		ResumeScheduleAction ResumeScheduleAction { get; }

		// Token: 0x0600032B RID: 811
		void ResumeStreamCleanup();

		// Token: 0x0600032C RID: 812
		ScheduleDefinitionOrReference RetriveScheduleFromBatchStrings(string typeString, string scheduleData);

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600032D RID: 813
		ScheduleCoordinator SchedCoordinator { get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600032E RID: 814
		Security SecMgr { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600032F RID: 815
		RSServiceHelper ServiceHelper { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000330 RID: 816
		SetCacheOptionsAction SetCacheOptionsAction { get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000331 RID: 817
		SetCacheRefreshPlanPropertiesAction SetCacheRefreshPlanPropertiesAction { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000332 RID: 818
		SetComponentDefinitionAction SetComponentDefinitionAction { get; }

		// Token: 0x06000333 RID: 819
		void SetDatabaseConnectionSettings(ConnectionTransactionType transactionType, IsolationLevel defaultIsolationLevel);

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000334 RID: 820
		SetDataSetDefinitionAction SetDataSetDefinitionAction { get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000335 RID: 821
		SetDataSetItemReferencesAction SetDataSetItemReferencesAction { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000336 RID: 822
		SetDataSourceContentsAction SetDataSourceContentsAction { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000337 RID: 823
		SetDrillthroughReportsAction SetDrillthroughReportsAction { get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000338 RID: 824
		SetExcelWorkbookContentsAction SetExcelWorkbookContentsAction { get; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000339 RID: 825
		SetExecutionOptionsAction SetExecutionOptionsAction { get; }

		// Token: 0x0600033A RID: 826
		void SetExternalRoot(CatalogItemPath path);

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600033B RID: 827
		SetItemDataSourcesAction SetItemDataSourcesAction { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600033C RID: 828
		SetModelDefinitionAction SetModelDefinitionAction { get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600033D RID: 829
		SetModelItemPoliciesAction SetModelItemPoliciesAction { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600033E RID: 830
		SetModelItemReferencesAction SetModelItemReferencesAction { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600033F RID: 831
		SetPoliciesAction SetPoliciesAction { get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000340 RID: 832
		SetPowerBIReportContentsAction SetPowerBIReportContentsAction { get; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000341 RID: 833
		SetPropertiesAction SetPropertiesAction { get; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000342 RID: 834
		SetRdlxReportDefinitionAction SetRdlxReportDefinitionAction { get; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000343 RID: 835
		SetReportDefinitionAction SetReportDefinitionAction { get; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000344 RID: 836
		SetReportHistoryOptionsAction SetReportHistoryOptionsAction { get; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000345 RID: 837
		SetReportItemReferencesAction SetReportItemReferencesAction { get; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000346 RID: 838
		SetReportLinkAction SetReportLinkAction { get; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000347 RID: 839
		SetReportParametersAction SetReportParametersAction { get; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000348 RID: 840
		SetResourceContentsAction SetResourceContentsAction { get; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000349 RID: 841
		SetRolePropertiesAction SetRolePropertiesAction { get; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600034A RID: 842
		SetSchedulePropertiesAction SetSchedulePropertiesAction { get; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600034B RID: 843
		SetSnapshotLimitAction SetSnapshotLimitAction { get; }

		// Token: 0x0600034C RID: 844
		IDisposable SetStreamFactory(StreamFactoryBase streamFactory);

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600034D RID: 845
		SetSubscriptionPropertiesAction SetSubscriptionPropertiesAction { get; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600034E RID: 846
		SetSystemPoliciesAction SetSystemPoliciesAction { get; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600034F RID: 847
		SetSystemPropertiesAction SetSystemPropertiesAction { get; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000350 RID: 848
		SetUserSettingsAction SetUserSettingsAction { get; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000351 RID: 849
		IDBInterface Storage { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000352 RID: 850
		bool StoreRdlChunks { get; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000353 RID: 851
		StreamManager StreamManager { get; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000354 RID: 852
		SubscriptionManager SubscriptionManager { get; }

		// Token: 0x06000355 RID: 853
		void SuspendStreamCleanup();

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000356 RID: 854
		SystemResourceManager SystemResourceManager { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000357 RID: 855
		int SystemSnapshotLimit { get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000358 RID: 856
		TestConnectForDataSourceDefinitionAction TestConnectForDataSourceDefinitionAction { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000359 RID: 857
		TestConnectForItemDataSourceAction TestConnectForItemDataSourceAction { get; }

		// Token: 0x0600035A RID: 858
		void ThrowIfSchedulerNotRunning();

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600035B RID: 859
		UpdateExecutionSnapshotAction UpdateExecutionSnapshotAction { get; }

		// Token: 0x0600035C RID: 860
		void UpdateSubscriptionLastRunInfo(Guid subscriptionID, InActiveFlags stateFlag, DateTime lastRunTime, string status);

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600035D RID: 861
		UserContext UserContext { get; }

		// Token: 0x0600035E RID: 862
		string UserNameToFolderName(string user);

		// Token: 0x0600035F RID: 863
		ExtensionParameter[] ValidateExtensionSettings(string extension, ParameterValueOrFieldReference[] settings, string path);

		// Token: 0x06000360 RID: 864
		void ValidateSubscriptionParameters(ExternalItemPath path, ParameterValueOrFieldReference[] subscriptionParameters, bool isDataDriven);

		// Token: 0x06000361 RID: 865
		void WillDisconnectStorage();

		// Token: 0x06000362 RID: 866
		void WillDisconnectStorage(ConnectionManager connectionManager);

		// Token: 0x06000363 RID: 867
		int GetExternalRootZone(ExternalItemPath path);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000044 RID: 68
	internal interface IDBInterface : IRSStorage
	{
		// Token: 0x06000203 RID: 515
		void AddBatchRecord(Guid batchId, string userName, CatalogCommand action, string item, string itemParameterName, string parent, string parentParameterName, string param, string paramParameterName, bool boolParam, byte[] content, string properties);

		// Token: 0x06000204 RID: 516
		void AddDataSet(Guid itemID, DataSetInfo dataSet, IPathTranslator pathTranslator, string editSessionID, out Guid linkID, out byte[] linkSecDesc);

		// Token: 0x06000205 RID: 517
		void AddDataSource(Guid itemID, Guid subscriptionID, DataSourceInfo dataSource, IPathTranslator pathTranslator, string editSessionID, out Guid linkID, out ItemType linkType, out byte[] linkSecDesc);

		// Token: 0x06000206 RID: 518
		void AddExecutionLogEntry(string instanceName, CatalogItemPath report, DBInterface.RequestType requestType, string format, string parameters, DateTime timeStart, DateTime timeEnd, int percentDataRetrieval, int percentProcessing, int percentRendering, ExecutionLogExecType source, string status, long byteCount, long rowCount, string executionId, byte reportAction, string additionalInfo);

		// Token: 0x06000207 RID: 519
		bool AddHistoryRecord(Guid snapshotId, Guid reportId, DateTime snapshotDate, ReportSnapshot snapshotData, int SnapshotTransientRefcountChange);

		// Token: 0x06000208 RID: 520
		void AddReportToExecutionCache(Guid reportId, ReportSnapshot snapshotData, DateTime executionDateTime, bool useEditSessionTimeout, out DateTime expirationDateTime);

		// Token: 0x06000209 RID: 521
		bool AddToFavorites(string userName, Guid itemId);

		// Token: 0x0600020A RID: 522
		bool CatalogDeleteObject(CatalogItemPath objectName);

		// Token: 0x0600020B RID: 523
		bool CatalogGetAllProperties(ExternalItemPath objectName, out ItemProperties properties);

		// Token: 0x0600020C RID: 524
		bool CatalogGetObjectContent(CatalogItemPath objectName, out ItemType type, out byte[] objectContent, out Guid link, out string mimeType, out byte[] secDesc, out Guid itemID);

		// Token: 0x0600020D RID: 525
		bool CatalogObjectExists(CatalogItemPath objectName, out ItemType type, out Guid id, out int snapshotLimit, out byte[] secDesc, out int execOptions, out Guid snapshotId, out Guid linkID);

		// Token: 0x0600020E RID: 526
		void ChangeStateOfDataSource(Guid itemID, bool enable);

		// Token: 0x0600020F RID: 527
		bool CheckChildrenBeforeDelete(ExternalItemPath objectName, Security secMgr);

		// Token: 0x06000210 RID: 528
		int CleanAllHistories(int snapshotLimit);

		// Token: 0x06000211 RID: 529
		int CleanBatchRecords();

		// Token: 0x06000212 RID: 530
		int CleanExpiredContentCache();

		// Token: 0x06000213 RID: 531
		int CleanExpiredEditSessions();

		// Token: 0x06000214 RID: 532
		int CleanHistoryForReport(Guid reportId, int snapshotLimit);

		// Token: 0x06000215 RID: 533
		int CleanOrphanedPolicies();

		// Token: 0x06000216 RID: 534
		void CleanupCatalog();

		// Token: 0x06000217 RID: 535
		Guid CreateEditSession(CatalogItemPath itemPath, string editSessionID, string name, byte[] content, string description, Guid intermediateID, string propertyAsXml, string parameterAsXml, byte[] dataCacheHash);

		// Token: 0x06000218 RID: 536
		void CreateNewSnapshotVersion(ReportSnapshot oldSnapshot, ReportSnapshot newSnapshot);

		// Token: 0x06000219 RID: 537
		Guid CreateObject(Guid id, string shortName, CatalogItemPath fullPath, ExternalItemPath parentPath, Guid parentId, ItemType objectType, byte[] objectContent, Guid intermediateSnapshotID, Guid link, string linkPath, ItemProperties objectProperties, string parameters, byte[] createdBySid, string createdBy, DateTime creationDate, DateTime modificationDate, string mimeType);

		// Token: 0x0600021A RID: 538
		Guid CreateObject(Guid id, string shortName, CatalogItemPath fullPath, ExternalItemPath parentPath, Guid parentId, ItemType objectType, byte[] objectContent, Guid intermediateSnapshotID, Guid link, string linkPath, ItemProperties objectProperties, string parameters, byte[] createdBySid, string createdByName, DateTime creationDate, DateTime modificationDate, string mimeType, string subType, Guid componentId);

		// Token: 0x0600021B RID: 539
		Guid CreateObject(Guid id, string shortName, CatalogItemPath fullPath, ExternalItemPath parentPath, Guid parentId, ItemType objectType, byte[] objectContent, Guid intermediateSnapshotID, Guid link, string linkPath, ItemProperties objectProperties, string parameters, string createdBy, DateTime creationDate, DateTime modificationDate, string mimeType, string subType, Guid componentId);

		// Token: 0x0600021C RID: 540
		void CreateOrUpdateContentCache(Guid catalogItemID, int paramsHash, string effectiveParams, DBInterface.ContentCacheTypes contentType, int version, Stream content);

		// Token: 0x0600021D RID: 541
		void CreateRdlChunk(Guid itemId, ReportSnapshot snapshot, string chunkName);

		// Token: 0x0600021E RID: 542
		bool DeleteAllHistoryForReport(Guid reportId);

		// Token: 0x0600021F RID: 543
		bool DeleteBatchRecords(Guid batchId);

		// Token: 0x06000220 RID: 544
		void DeleteDataSets(Guid itemID);

		// Token: 0x06000221 RID: 545
		void DeleteDataSources(Guid itemID);

		// Token: 0x06000222 RID: 546
		void DeleteDrillthroughReport(Guid modelId, string entityId);

		// Token: 0x06000223 RID: 547
		bool DeleteHistoriesWithNoPolicy();

		// Token: 0x06000224 RID: 548
		bool DeleteHistoryRecord(Guid reportId, DateTime snapshotDate);

		// Token: 0x06000225 RID: 549
		bool DeleteHistoryRecord(Guid reportId, string historyId);

		// Token: 0x06000226 RID: 550
		bool DeleteObject(ExternalItemPath objectName);

		// Token: 0x06000227 RID: 551
		int ExpireExecutionLogEntries();

		// Token: 0x06000228 RID: 552
		bool FindFavoriteableItemsNonRecursive(ExternalItemPath objectName, out FavoriteableCatalogItemList children, Security secMgr, IPathTranslator pathTranslator, bool appendMyReports);

		// Token: 0x06000229 RID: 553
		bool FindFavoriteableItemsRecursive(ExternalItemPath objectName, out FavoriteableCatalogItemList children, Security secMgr, IPathTranslator pathTranslator, bool appendMyReports);

		// Token: 0x0600022A RID: 554
		bool FindItemsByDataSet(Guid dsItemID, out CatalogItemList reports, Security secMgr, IPathTranslator pathTranslator);

		// Token: 0x0600022B RID: 555
		bool FindItemsByDataSource(Guid dsItemID, out CatalogItemList reports, Security secMgr, IPathTranslator pathTranslator, bool recursive);

		// Token: 0x0600022C RID: 556
		bool FindObjectsByLink(Guid link, out CatalogItemList reports, Security secMgr, IPathTranslator pathTranslator);

		// Token: 0x0600022D RID: 557
		bool FindObjectsGeneral(ExternalItemPath namePrefix, BooleanOperatorEnum boolOperator, ItemSearchOptions options, ItemSearchConditions conditions, out CatalogItemList reports, Security secMgr, IPathTranslator pathTranslator);

		// Token: 0x0600022E RID: 558
		bool FindObjectsNonRecursive(ExternalItemPath objectName, out CatalogItemList children, Security secMgr, IPathTranslator pathTranslator, bool appendMyReports);

		// Token: 0x0600022F RID: 559
		bool FindObjectsRecursive(ExternalItemPath namePrefix, out CatalogItemList children, Security secMgr, IPathTranslator pathTranslator, bool appendMyReports);

		// Token: 0x06000230 RID: 560
		bool FindParents(ExternalItemPath objectName, out CatalogItemList parents, Security secMgr, IPathTranslator pathTranslator);

		// Token: 0x06000231 RID: 561
		int FlushContentCache(CatalogItemPath itemPath);

		// Token: 0x06000232 RID: 562
		SystemProperties GetAllConfigurationInfo();

		// Token: 0x06000233 RID: 563
		FavoriteableCatalogItemList GetAllFavoriteItems(Security secMgr, IPathTranslator pathTranslator);

		// Token: 0x06000234 RID: 564
		bool GetAllProperties(ExternalItemPath objectName, out ItemProperties properties, out Guid id, out Guid linkID, out ItemType type, out byte[] secDesc, out int executionOptions, out int snapshotLimit, out string subType);

		// Token: 0x06000235 RID: 565
		ArrayList GetBatchRecords(Guid batchId);

		// Token: 0x06000236 RID: 566
		bool GetCompiledDefinition(CatalogItemPath objectName, out ItemType type, out ReportSnapshot compiledDefinition, out Guid link, out string properties, out string description, out byte[] secDesc, out Guid reportID, out int execOptions, out ReportSnapshot linkedCompiledDefinition, out string linkedProperties, out string linkedDescription, out Guid executionSnapshotID);

		// Token: 0x06000237 RID: 567
		bool GetDataSetForExecution(Guid ItemId, string queryParametersXml, out bool foundInCache, out ReportSnapshot snapshotData, out bool cachingRequested, out ItemProperties properties);

		// Token: 0x06000238 RID: 568
		byte[] GetDataSourcePasswordForSubscription(Guid subscriptionId);

		// Token: 0x06000239 RID: 569
		DataSourceInfoCollection GetDataSources(Guid itemID);

		// Token: 0x0600023A RID: 570
		DataSourceInfoCollection GetDataSources(Guid itemID, out bool itemIDisModelID);

		// Token: 0x0600023B RID: 571
		DataSourceInfoCollection GetDataSourcesAndResolveModelLink(Guid itemID);

		// Token: 0x0600023C RID: 572
		CatalogItemPath GetDrillThroughReport(CatalogItemPath modelPath, string entityId, short drillType);

		// Token: 0x0600023D RID: 573
		ModelDrillthroughReport[] GetDrillthroughReports(Guid modelId, string entityId, IPathTranslator pathTranslator);

		// Token: 0x0600023E RID: 574
		List<LinkedReportCatalogItem> GetLinkedReports(RSService service, Guid link);

		// Token: 0x0600023F RID: 575
		bool GetObjectContent(ExternalItemPath objectName, out ItemType type, out byte[] objectContent, out Guid link, out string mimeType, out byte[] secDesc, out Guid itemID);

		// Token: 0x06000240 RID: 576
		string GetOneConfigurationInfo(string key);

		// Token: 0x06000241 RID: 577
		bool GetParameters(CatalogItemPath objectName, out ItemType type, out string parameters, out Guid id, out byte[] secDesc);

		// Token: 0x06000242 RID: 578
		bool GetParameters(CatalogItemPath objectName, out ItemType type, out string parameters, out Guid id, out byte[] secDesc, out Guid linkID, out int executionOptions);

		// Token: 0x06000243 RID: 579
		string GetParametersById(Guid objectID);

		// Token: 0x06000244 RID: 580
		CatalogItemPath GetPathById(Guid id);

		// Token: 0x06000245 RID: 581
		string GetDataModelParametersById(Guid catalogId);

		// Token: 0x06000246 RID: 582
		bool GetReportForExecution(CatalogItemPath objectName, string queryParametersXml, out bool foundInCache, out ItemType type, out ReportSnapshot intermediateSnapshot, out ReportSnapshot snapshotData, out Guid link, out string linkPath, out string properties, out string description, out byte[] secDesc, out Guid reportID, out int execOptions, out DateTime executionDateTime, out bool hasData, out bool cachingRequested, out DateTime expirationDateTime);

		// Token: 0x06000247 RID: 583
		bool GetReportParametersForExecution(CatalogItemPath itemPath, DateTime historyID, out Guid reportID, out ItemType itemType, out int executionOption, out byte[] secDescr, out string savedParametersXml, out ReportSnapshot compiledDefinition, out ReportSnapshot snapshotData, out Guid linkID, out DateTime executionDateTime);

		// Token: 0x06000248 RID: 584
		Guid GetRootItemId();

		// Token: 0x06000249 RID: 585
		DataSetInfoCollection GetSharedDataSets(Guid itemID);

		// Token: 0x0600024A RID: 586
		bool GetSnapshotFromHistory(CatalogItemPath objectName, DateTime snapshotDate, out Guid reportId, out ItemType type, out ReportSnapshot snapshotData, out string description, out string propertiesXml, out byte[] secDesc);

		// Token: 0x0600024B RID: 587
		ReportProcessingFlags GetSnapshotProcessingFlags(Guid snapshotId, bool isPermanentSnapshot);

		// Token: 0x0600024C RID: 588
		int GetSnapshotPromotedInfo(ReportSnapshot reportSnapshot, out bool hasDocMap, out PaginationMode paginationMode, out ReportProcessingFlags processingFlags);

		// Token: 0x0600024D RID: 589
		bool HasDataModelDataSources(Guid itemId);

		// Token: 0x0600024E RID: 590
		bool HasRelatedItem(Guid datasetId, int relatedItemType);

		// Token: 0x0600024F RID: 591
		Comment InsertComment(Comment comment);

		// Token: 0x06000250 RID: 592
		bool IsFavoriteItem(string userName, Guid itemId);

		// Token: 0x06000251 RID: 593
		bool IsSchedulerRunning();

		// Token: 0x06000252 RID: 594
		bool IsUserContextOwner(long id);

		// Token: 0x06000253 RID: 595
		ReportHistorySnapshot[] ListHistory(Guid reportId);

		// Token: 0x06000254 RID: 596
		HistorySnapshot[] ListHistorySnapshots(Guid reportId);

		// Token: 0x06000255 RID: 597
		HistorySnapshot[] ListHistorySnapshotsNoSize(Guid reportId);

		// Token: 0x06000256 RID: 598
		bool MoveObject(CatalogItemPath oldExternalPath, string newShortName, CatalogItemPath newExternalPath, Guid newParentId, bool renameOnly);

		// Token: 0x06000257 RID: 599
		bool ObjectExists(ExternalItemPath objectName);

		// Token: 0x06000258 RID: 600
		bool ObjectExists(ExternalItemPath objectName, out ItemType type);

		// Token: 0x06000259 RID: 601
		bool ObjectExists(ExternalItemPath objectName, out ItemType type, out byte[] secDesc);

		// Token: 0x0600025A RID: 602
		bool ObjectExists(ExternalItemPath objectName, out ItemType type, out Guid id, out byte[] secDesc);

		// Token: 0x0600025B RID: 603
		bool ObjectExists(ExternalItemPath objectName, out ItemType type, out Guid id, out byte[] secDesc, out Guid snapshotId);

		// Token: 0x0600025C RID: 604
		bool ObjectExists(ExternalItemPath objectName, out ItemType type, out Guid id, out int snapshotLimit);

		// Token: 0x0600025D RID: 605
		bool ObjectExists(ExternalItemPath objectName, out ItemType type, out Guid id, out int snapshotLimit, out byte[] secDesc);

		// Token: 0x0600025E RID: 606
		bool ObjectExists(ExternalItemPath objectName, out ItemType type, out Guid id, out int snapshotLimit, out byte[] secDesc, out int execOptions);

		// Token: 0x0600025F RID: 607
		bool ObjectExists(ExternalItemPath objectName, out ItemType type, out Guid id, out int snapshotLimit, out byte[] secDesc, out int execOptions, out Guid snapshotId, out Guid linkID);

		// Token: 0x06000260 RID: 608
		void PromoteSnapshotInfo(ReportSnapshot reportSnapshot, int pageCount, bool hasDocumentMap, PaginationMode paginationMode, ReportProcessingFlags processingFlags);

		// Token: 0x06000261 RID: 609
		bool RemoveFromFavorites(string userName, Guid itemId);

		// Token: 0x06000262 RID: 610
		void SetAllProperties(CatalogItem item, ItemProperties objectProperties, string modifiedBy, DateTime modifiedDate);

		// Token: 0x06000263 RID: 611
		void SetCacheLastUsed(ServerSnapshot snapshot);

		// Token: 0x06000264 RID: 612
		void SetConfigurationInfo(SystemProperties values);

		// Token: 0x06000265 RID: 613
		void SetDrillthroughReport(Guid reportId, Guid modelId, string entityId, short drillType);

		// Token: 0x06000266 RID: 614
		void SetHistoryLimit(CatalogItemPath item, int snapshotLimit);

		// Token: 0x06000267 RID: 615
		void SetLastModified(CatalogItemPath Item, string modifiedBy, DateTime modifiedDate);

		// Token: 0x06000268 RID: 616
		void SetObjectContent(CatalogItemPath objectName, ItemType objectType, byte[] objectContent, Guid intermediateSnapshotID, string parameters, Guid link, string mimeType);

		// Token: 0x06000269 RID: 617
		void SetObjectContent(CatalogItemPath objectName, ItemType objectType, byte[] objectContent, Guid intermediateSnapshotID, string parameters, Guid link, string mimeType, byte[] dataCacheHash);

		// Token: 0x0600026A RID: 618
		void SetObjectContent(CatalogItemPath objectName, ItemType objectType, byte[] objectContent, Guid intermediateSnapshotID, string parameters, Guid link, string mimeType, byte[] dataCacheHash, string subType, Guid componentID);

		// Token: 0x0600026B RID: 619
		void SetParameters(CatalogItemPath objectName, string parameters);

		// Token: 0x0600026C RID: 620
		void SetParametersById(Guid objectID, string parameters);

		// Token: 0x0600026D RID: 621
		void ThrowIfSchedulerNotRunning();

		// Token: 0x0600026E RID: 622
		bool TryGetContentCache(Guid catalogItemID, int paramsHash, DBInterface.ContentCacheTypes contentType, out byte[] content, out int version);

		// Token: 0x0600026F RID: 623
		bool TryGetContentCacheDetails(Guid catalogItemID, int paramsHash, DBInterface.ContentCacheTypes contentType, out int version);

		// Token: 0x06000270 RID: 624
		bool UpdateComment(Comment comment);

		// Token: 0x06000271 RID: 625
		Guid UpdateCompiledDefinition(CatalogItemPath itemPath, Guid oldSnapshotId, Guid newSnapshotId);

		// Token: 0x06000272 RID: 626
		void UpdateSnapshotPaginationInfo(ReportSnapshot reportSnapshot, int pageCount, PaginationMode paginationMode);

		// Token: 0x06000273 RID: 627
		void UpdateSnapshotReferences(ReportSnapshot oldSnapshot, ReportSnapshot newSnapshot, int transientRefCountModifier, out int updatedReferences);

		// Token: 0x06000274 RID: 628
		void UpdateUsernameFromSID();

		// Token: 0x06000275 RID: 629
		long CreateExtendedCatalogContent(ExtendedContentType extendedContentType, Stream stream, out long contentSize);

		// Token: 0x06000276 RID: 630
		void FinalizeNewExtendedCatalogContent(long id, Guid catalogItemId);

		// Token: 0x06000277 RID: 631
		long WriteExtendedCatalogContent(Guid id, ExtendedContentType extendedContentType, Stream stream, DateTime? modifiedDate = null);

		// Token: 0x06000278 RID: 632
		void WriteContentSize(Guid id, long contentSize);

		// Token: 0x06000279 RID: 633
		void WriteDataModelParameters(Guid id, string parameters);

		// Token: 0x0600027A RID: 634
		bool GetCatalogExtendedContentData(Guid catalogItemId, ExtendedContentType extendedContentType, out byte[] objectContent);
	}
}

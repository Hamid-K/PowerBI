using System;
using System.Data;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000109 RID: 265
	internal interface IExecutionDataProvider
	{
		// Token: 0x06000A89 RID: 2697
		ReportExecutionDefinition GetReportExecutionMetadata(ClientRequest session, CatalogItemContext reportContext, ParameterInfoCollection queryParameters);

		// Token: 0x06000A8A RID: 2698
		ReportExecutionDefinition GetHistorySnapshot(ClientRequest session, CatalogItemContext reportContext);

		// Token: 0x06000A8B RID: 2699
		ParameterInfoCollection GetParameters(ClientRequest session, CatalogItemContext reportContext);

		// Token: 0x06000A8C RID: 2700
		void GetAllRuntimeDataSourcesAndDataSets(ClientRequest session, ReportProcessing processingEngine, CatalogItemContext reportContext, ReportSnapshot intermediateSnapshot, DataSourceInfoCollection currentDataSources, DataSetInfoCollection currentDataSets, out RuntimeDataSourceInfoCollection runtimeDataSources, out RuntimeDataSetInfoCollection runtimeDataSets);

		// Token: 0x06000A8D RID: 2701
		int GetReportTimeout(ItemProperties reportProperties);

		// Token: 0x06000A8E RID: 2702
		ISubreportRetrieval CreateSubreportRetrievalContext(SnapshotManager snapshotManager);

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000A8F RID: 2703
		IGetResource ResourceCallback { get; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000A90 RID: 2704
		ReportProcessing.CreateDataExtensionInstance DataExtensionCallback { get; }

		// Token: 0x06000A91 RID: 2705
		ServerParameterStore CreateParameterStore();

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000A92 RID: 2706
		StreamManager StreamManager { get; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000A93 RID: 2707
		IDBInterface Storage { get; }

		// Token: 0x06000A94 RID: 2708
		IStorageAccess EnterStorageContext(IsolationLevel? isolationLevel);

		// Token: 0x06000A95 RID: 2709
		void SuspendStreamCleanup();

		// Token: 0x06000A96 RID: 2710
		void ResumeStreamCleanup();

		// Token: 0x06000A97 RID: 2711
		RSReportContext CreateReportContext(CatalogItemContext catalogItem);

		// Token: 0x06000A98 RID: 2712
		ReportProcessing CreateProcessingEngine();

		// Token: 0x06000A99 RID: 2713
		void FlushCache(Guid itemId, bool useCurrentConnection);

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000A9A RID: 2714
		UserContext UserContext { get; }

		// Token: 0x06000A9B RID: 2715
		void CheckAccess(byte[] securityDescriptor, ItemType itemType, ReportOperation operation, string catalogPath);

		// Token: 0x06000A9C RID: 2716
		IAdditionalToken GetAdditionalTokenInterface(CatalogItemContext catalogItem);

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000A9D RID: 2717
		IPathTranslator PathTranslator { get; }
	}
}

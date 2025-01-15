using System;
using System.Data;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000110 RID: 272
	internal class RSServiceDataProvider : IExecutionDataProvider
	{
		// Token: 0x06000AB0 RID: 2736 RVA: 0x000287B4 File Offset: 0x000269B4
		public RSServiceDataProvider(RSService service)
		{
			this.m_service = service;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000287C4 File Offset: 0x000269C4
		public ReportExecutionDefinition GetReportExecutionMetadata(ClientRequest session, CatalogItemContext reportContext, ParameterInfoCollection queryParameters)
		{
			this.SyncReport(reportContext.ItemPath);
			ReportExecutionDefinition reportExecutionDefinition = ReportExecutionDefinition.Load(reportContext, queryParameters, session, this.m_service, SecurityRequirements.GenerateForExecuteReport(this.m_service.SecMgr, this.m_service.UserName));
			reportExecutionDefinition.ResolveLinkedProperties(this.m_service);
			if (reportExecutionDefinition.Type == ItemType.LinkedReport)
			{
				session.SessionReport.Report.ReportDefinitionPath = reportExecutionDefinition.ReportDefinitionPath;
			}
			return reportExecutionDefinition;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00028834 File Offset: 0x00026A34
		public ReportExecutionDefinition GetHistorySnapshot(ClientRequest session, CatalogItemContext reportContext)
		{
			this.SyncReport(reportContext.ItemPath);
			ReportExecutionDefinition reportExecutionDefinition = ReportExecutionDefinition.LoadHistorySnapshot(reportContext, session, this.m_service, SecurityRequirements.GenerateForExecuteReport(this.m_service.SecMgr, this.m_service.UserName));
			this.m_service.Storage.Commit();
			return reportExecutionDefinition;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00028888 File Offset: 0x00026A88
		public void GetAllRuntimeDataSourcesAndDataSets(ClientRequest session, ReportProcessing processingEngine, CatalogItemContext reportContext, ReportSnapshot intermediateSnapshot, DataSourceInfoCollection currentDataSources, DataSetInfoCollection currentDataSets, out RuntimeDataSourceInfoCollection runtimeDataSources, out RuntimeDataSetInfoCollection runtimeDataSets)
		{
			this.SyncReport(reportContext.ItemPath);
			this.m_service.GetAllDataSources(session, processingEngine, reportContext, intermediateSnapshot, currentDataSources, currentDataSets, out runtimeDataSources, out runtimeDataSets);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000288B9 File Offset: 0x00026AB9
		public ParameterInfoCollection GetParameters(ClientRequest session, CatalogItemContext reportContext)
		{
			this.SyncReport(reportContext.ItemPath);
			ParameterInfoCollection reportParameterDefinitions = this.m_service.GetReportParameterDefinitions(session, reportContext, true);
			reportParameterDefinitions.ThrowIfNotValid();
			return reportParameterDefinitions;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000288DB File Offset: 0x00026ADB
		public int GetReportTimeout(ItemProperties reportProperties)
		{
			return this.m_service.GetReportTimeout(reportProperties);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000288E9 File Offset: 0x00026AE9
		public ISubreportRetrieval CreateSubreportRetrievalContext(SnapshotManager snapshotManager)
		{
			RSTrace.CatalogTrace.Assert(snapshotManager != null, "snapshotManager");
			return SubreportRetrieval.Create(snapshotManager, this.m_service);
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0002890A File Offset: 0x00026B0A
		public IGetResource ResourceCallback
		{
			get
			{
				return new ServerGetResourceForProcessing(this.m_service);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00028917 File Offset: 0x00026B17
		public ReportProcessing.CreateDataExtensionInstance DataExtensionCallback
		{
			get
			{
				return this.m_service.HowToCreateDataExtensionInstance;
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00028924 File Offset: 0x00026B24
		public ServerParameterStore CreateParameterStore()
		{
			return new ServerParameterStore(this.Storage.ConnectionManager);
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00028936 File Offset: 0x00026B36
		public StreamManager StreamManager
		{
			get
			{
				return this.m_service.StreamManager;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00028943 File Offset: 0x00026B43
		public IDBInterface Storage
		{
			get
			{
				return this.m_service.Storage;
			}
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00028950 File Offset: 0x00026B50
		public ReportProcessing CreateProcessingEngine()
		{
			return Global.GetProcessingEngine();
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00028958 File Offset: 0x00026B58
		public IStorageAccess EnterStorageContext(IsolationLevel? isolationLevel)
		{
			IStorageAccess storageAccess;
			if (isolationLevel != null)
			{
				storageAccess = new RSServiceStorageAccess(this.m_service, isolationLevel.Value);
			}
			else
			{
				storageAccess = new RSServiceStorageAccess(this.m_service);
			}
			return storageAccess;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00028992 File Offset: 0x00026B92
		public RSReportContext CreateReportContext(CatalogItemContext itemContext)
		{
			return new RSReportContext(this.m_service, itemContext);
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x000289A0 File Offset: 0x00026BA0
		public UserContext UserContext
		{
			get
			{
				return this.m_service.UserContext;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x000289AD File Offset: 0x00026BAD
		public Security SecurityManager
		{
			get
			{
				return this.m_service.SecMgr;
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000289BA File Offset: 0x00026BBA
		public void SuspendStreamCleanup()
		{
			this.m_service.SuspendStreamCleanup();
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000289C7 File Offset: 0x00026BC7
		public void ResumeStreamCleanup()
		{
			this.m_service.ResumeStreamCleanup();
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000289D4 File Offset: 0x00026BD4
		public void FlushCache(Guid itemId, bool useCurrentConnection)
		{
			if (useCurrentConnection)
			{
				this.m_service.ExecCacheDb.FlushCacheById(itemId);
				return;
			}
			ReportExecutionCacheDb reportExecutionCacheDb = new ReportExecutionCacheDb(this.m_service);
			reportExecutionCacheDb.ConnectionManager = new ConnectionManager();
			reportExecutionCacheDb.ConnectionManager.WillDisconnectStorage();
			try
			{
				reportExecutionCacheDb.FlushCacheById(itemId);
				reportExecutionCacheDb.ConnectionManager.CommitTransaction();
			}
			finally
			{
				reportExecutionCacheDb.ConnectionManager.DisconnectStorage();
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00028A48 File Offset: 0x00026C48
		public void CheckAccess(byte[] securityDescriptor, ItemType itemType, ReportOperation operation, string catalogPath)
		{
			if (securityDescriptor != null && !this.m_service.SecMgr.CheckAccess(itemType, securityDescriptor, operation, this.m_service.CatalogToExternal(catalogPath)))
			{
				throw new AccessDeniedException(this.m_service.UserContext.UserName, ErrorCode.rsAccessDenied);
			}
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00028A87 File Offset: 0x00026C87
		public IAdditionalToken GetAdditionalTokenInterface(CatalogItemContext catalogItem)
		{
			return new ServerAdditionalToken(this.m_service, catalogItem);
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00028A95 File Offset: 0x00026C95
		public IPathTranslator PathTranslator
		{
			get
			{
				return this.m_service;
			}
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00028A9D File Offset: 0x00026C9D
		private void SyncReport(ExternalItemPath reportPath)
		{
			if (!this.m_alreadySynced)
			{
				this.m_service.ServiceHelper.SyncToRSCatalog(reportPath);
				this.m_alreadySynced = true;
			}
		}

		// Token: 0x0400049F RID: 1183
		private bool m_alreadySynced;

		// Token: 0x040004A0 RID: 1184
		private readonly RSService m_service;
	}
}

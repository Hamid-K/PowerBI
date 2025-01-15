using System;
using System.Diagnostics;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000117 RID: 279
	internal sealed class RenderForEditSession : RenderForNewSession
	{
		// Token: 0x06000B2C RID: 2860 RVA: 0x000297E0 File Offset: 0x000279E0
		public RenderForEditSession(IExecutionDataProvider provider, ExecutionParameters execInfo)
			: base(provider, execInfo)
		{
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00029815 File Offset: 0x00027A15
		public override void SetCacheTargetSnapshot(ReportSnapshot snapshot)
		{
			this.m_cacheTarget = snapshot;
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool CachingRequested
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00029820 File Offset: 0x00027A20
		protected override RenderStrategyBase GetExecutionStrategy()
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "RenderForEditSession('{0}')", new object[] { base.RequestInfo.ReportContext.OriginalItemPath });
			if (this.ExecuteExistingSnapshot && this.IsDataSourceValidForCachedData())
			{
				return new RenderLiveCachedData(this);
			}
			return new RenderLiveEditSession(this);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00029874 File Offset: 0x00027A74
		protected override void TryDbCacheSnapshot(RenderStrategyBase strategy)
		{
			if (this.m_cacheTarget != null)
			{
				ReportSnapshot cacheTarget = this.m_cacheTarget;
				DateTime dateTime;
				base.DataProvider.Storage.AddReportToExecutionCache(this.ReportId, cacheTarget, this.ExecutionDateTime, true, out dateTime);
				strategy.SnapshotWasCached = true;
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x000298B8 File Offset: 0x00027AB8
		private bool IsDataSourceValidForCachedData()
		{
			RuntimeDataSourceInfoCollection runtimeDataSourceInfoCollection;
			RuntimeDataSetInfoCollection runtimeDataSetInfoCollection;
			base.DataProvider.GetAllRuntimeDataSourcesAndDataSets(base.RequestInfo.Session, base.ProcessingEngine, base.RequestInfo.ReportContext, this.IntermediateSnapshot, this.DataSources, this.DataSets, out runtimeDataSourceInfoCollection, out runtimeDataSetInfoCollection);
			return runtimeDataSourceInfoCollection.GoodForDataCaching();
		}

		// Token: 0x040004B1 RID: 1201
		private ReportSnapshot m_cacheTarget;
	}
}

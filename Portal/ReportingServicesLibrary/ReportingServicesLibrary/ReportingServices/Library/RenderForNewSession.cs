using System;
using System.Data;
using System.Diagnostics;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000114 RID: 276
	internal class RenderForNewSession : ReportExecutionBase
	{
		// Token: 0x06000AF3 RID: 2803 RVA: 0x000291B0 File Offset: 0x000273B0
		public RenderForNewSession(IExecutionDataProvider provider, ExecutionParameters execInfo)
			: base(provider, execInfo)
		{
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000291BA File Offset: 0x000273BA
		protected override IStorageAccess EnterStorageContext()
		{
			return base.DataProvider.EnterStorageContext(new IsolationLevel?(IsolationLevel.RepeatableRead));
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000291D4 File Offset: 0x000273D4
		protected override RenderStrategyBase GetExecutionStrategy()
		{
			string value = base.RequestInfo.ReportContext.OriginalItemPath.Value;
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "RenderForNewSession('{0}')", new object[] { value });
			RenderStrategyBase renderStrategyBase;
			if (this.ExecuteExistingSnapshot)
			{
				renderStrategyBase = new RenderFromSnapshot(this);
			}
			else if (this.IsRdceReport)
			{
				renderStrategyBase = new RenderRdceReport(this);
			}
			else
			{
				renderStrategyBase = new RenderLive(this);
			}
			if (RSTrace.CatalogTrace.IsTraceLevelEnabled(TraceLevel.Verbose))
			{
				string text;
				switch (renderStrategyBase.ExecutionType)
				{
				case ExecutionLogExecType.Live:
					text = "RenderLive";
					break;
				case ExecutionLogExecType.Cache:
					text = "RenderFromCache";
					break;
				case ExecutionLogExecType.Snapshot:
					text = "RenderFromSnapshot";
					break;
				case ExecutionLogExecType.History:
					text = "RenderHistory";
					break;
				case ExecutionLogExecType.AdHoc:
					text = "RenderAdHoc";
					break;
				case ExecutionLogExecType.Session:
					text = "RenderFromSessionSnapshot";
					break;
				case ExecutionLogExecType.Rdce:
					text = "RenderRdceReport";
					break;
				default:
					text = "Unknown";
					break;
				}
				RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "{0}('{1}')", new object[] { text, value });
			}
			base.DataProvider.Storage.Commit();
			return renderStrategyBase;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000292E4 File Offset: 0x000274E4
		protected override void TryDbCacheSnapshot(RenderStrategyBase strategy)
		{
			ReportSnapshot chunkTargetSnapshot = strategy.SnapshotManager.ChunkTargetSnapshot;
			if ((strategy.UsedUserProfile & UserProfileState.InQuery) == UserProfileState.InQuery)
			{
				ExecTrace.TraceVerbose("Unable to cache report due to user dependency in query.");
				return;
			}
			if (!DataSourceCatalogItem.GoodForUnattendedExecution(strategy.RuntimeDataSources))
			{
				ExecTrace.TraceVerbose("Unable to cache report due to data source option.");
				return;
			}
			RSTrace.CatalogTrace.Assert(chunkTargetSnapshot != null, "targetSnapshot");
			ExecTrace.TraceVerbose("Adding report to execution cache.");
			if ((strategy.UsedUserProfile & UserProfileState.InReport) != UserProfileState.None)
			{
				ExecTrace.TraceVerbose("Marking snapshot as dependent on user.");
				chunkTargetSnapshot.MarkAsDependentOnUser();
			}
			DateTime dateTime;
			base.DataProvider.Storage.AddReportToExecutionCache(this.ReportId, chunkTargetSnapshot, this.ExecutionDateTime, false, out dateTime);
			strategy.SnapshotWasCached = true;
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool ClearShowHideStateBeforeExecution
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00029389 File Offset: 0x00027589
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x00029397 File Offset: 0x00027597
		public override ParameterInfoCollection EffectiveParameters
		{
			get
			{
				this.GetReportMetadata();
				return this.m_effectiveParams;
			}
			set
			{
				this.GetReportMetadata();
				this.m_effectiveParams = value;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x000293A6 File Offset: 0x000275A6
		public override ReportSnapshot IntermediateSnapshot
		{
			get
			{
				this.GetReportMetadata();
				return this.m_executionData.DefinitionSnapshot;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x000293B9 File Offset: 0x000275B9
		public override ItemProperties ReportProperties
		{
			get
			{
				this.GetReportMetadata();
				return this.m_executionData.ReportProperties;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x000293CC File Offset: 0x000275CC
		public override bool ExecuteExistingSnapshot
		{
			get
			{
				this.GetReportMetadata();
				return this.m_executionData.HasData;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x000293DF File Offset: 0x000275DF
		public override string Description
		{
			get
			{
				this.GetReportMetadata();
				return this.m_executionData.Description;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x000293F2 File Offset: 0x000275F2
		public override DataSourceInfoCollection DataSources
		{
			get
			{
				this.GetReportMetadata();
				return this.m_executionData.DataSources;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00029405 File Offset: 0x00027605
		public override DataSetInfoCollection DataSets
		{
			get
			{
				this.GetReportMetadata();
				return this.m_executionData.DataSets;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00029418 File Offset: 0x00027618
		public override Guid ReportId
		{
			get
			{
				this.GetReportMetadata();
				return this.m_executionData.ReportId;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0002942B File Offset: 0x0002762B
		public override ReportSnapshot ExecutionSnapshot
		{
			get
			{
				this.GetReportMetadata();
				return this.m_executionData.SnapshotData;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x0002943E File Offset: 0x0002763E
		// (set) Token: 0x06000B03 RID: 2819 RVA: 0x00029451 File Offset: 0x00027651
		public override DateTime ExecutionDateTime
		{
			get
			{
				this.GetReportMetadata();
				return this.m_executionData.ExecutionDateTime;
			}
			set
			{
				this.GetReportMetadata();
				this.m_executionData.ExecutionDateTime = value;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00029465 File Offset: 0x00027665
		public override bool CachingRequested
		{
			get
			{
				this.GetReportMetadata();
				return this.m_executionData.CachingRequested && !base.IsHistoryExecution;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00029485 File Offset: 0x00027685
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x00029498 File Offset: 0x00027698
		public override DateTime ExpirationDateTime
		{
			get
			{
				this.GetReportMetadata();
				return this.m_executionData.ExpirationDateTime;
			}
			set
			{
				this.GetReportMetadata();
				this.m_executionData.ExpirationDateTime = value;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x000294AC File Offset: 0x000276AC
		public override bool FoundInCache
		{
			get
			{
				this.GetReportMetadata();
				return this.m_executionData.FoundInCache && !base.IsHistoryExecution;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x000294CC File Offset: 0x000276CC
		public override UserProfileState UserDependencies
		{
			get
			{
				this.GetReportMetadata();
				return this.m_executionData.UserDependencies;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x000294DF File Offset: 0x000276DF
		public override int PageCount
		{
			get
			{
				this.GetReportMetadata();
				return this.m_executionData.PaginationData.PageCount;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x000294F7 File Offset: 0x000276F7
		public override PaginationMode PaginationMode
		{
			get
			{
				this.GetReportMetadata();
				return this.m_executionData.PaginationData.Mode;
			}
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00029510 File Offset: 0x00027710
		private void GetReportMetadata()
		{
			if (this.m_retrievedReportMetadata)
			{
				return;
			}
			try
			{
				this.m_effectiveParams = this.GetReportParameterDefinitions();
				this.m_executionData = this.GetReportExecutionMetadata();
				RSTrace.CatalogTrace.Assert(this.m_executionData != null, "m_executionData");
			}
			finally
			{
				this.m_retrievedReportMetadata = true;
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00029574 File Offset: 0x00027774
		protected virtual ParameterInfoCollection GetReportParameterDefinitions()
		{
			return base.DataProvider.GetParameters(base.RequestInfo.Session, base.RequestInfo.ReportContext);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00029597 File Offset: 0x00027797
		protected virtual ReportExecutionDefinition GetReportExecutionMetadata()
		{
			return base.DataProvider.GetReportExecutionMetadata(base.RequestInfo.Session, base.RequestInfo.ReportContext, this.m_effectiveParams.GetQueryParameters());
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x000295C5 File Offset: 0x000277C5
		private bool IsRdceReport
		{
			get
			{
				this.GetReportMetadata();
				return this.m_executionData.IsRdceReport;
			}
		}

		// Token: 0x040004AD RID: 1197
		private bool m_retrievedReportMetadata;

		// Token: 0x040004AE RID: 1198
		private ReportExecutionDefinition m_executionData;

		// Token: 0x040004AF RID: 1199
		private ParameterInfoCollection m_effectiveParams;
	}
}

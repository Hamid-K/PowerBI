using System;
using System.Diagnostics;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000115 RID: 277
	internal class RenderForExistingSession : ReportExecutionBase
	{
		// Token: 0x06000B0F RID: 2831 RVA: 0x000291B0 File Offset: 0x000273B0
		public RenderForExistingSession(IExecutionDataProvider provider, ExecutionParameters execInfo)
			: base(provider, execInfo)
		{
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x000295D8 File Offset: 0x000277D8
		protected override IStorageAccess EnterStorageContext()
		{
			return base.DataProvider.EnterStorageContext(null);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x000295F9 File Offset: 0x000277F9
		protected override RenderStrategyBase GetExecutionStrategy()
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "RenderFromSession('{0}')", new object[] { base.RequestInfo.ReportContext.OriginalItemPath.Value });
			return new RenderFromSession(this);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected override void TryDbCacheSnapshot(RenderStrategyBase strategy)
		{
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected override bool ClearShowHideStateBeforeExecution
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0002962F File Offset: 0x0002782F
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x0002964B File Offset: 0x0002784B
		public override ParameterInfoCollection EffectiveParameters
		{
			get
			{
				return base.RequestInfo.Session.SessionReport.Report.EffectiveParams;
			}
			set
			{
				base.RequestInfo.Session.SessionReport.Report.EffectiveParams = value;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00029668 File Offset: 0x00027868
		public override ReportSnapshot IntermediateSnapshot
		{
			get
			{
				return base.RequestInfo.Session.SessionReport.Report.CompiledDefinition;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00029684 File Offset: 0x00027884
		public override ItemProperties ReportProperties
		{
			get
			{
				if (base.RequestInfo.Session.SessionReport.IsAdhocReport)
				{
					return new ItemProperties(string.Empty);
				}
				if (this.m_cachedReportContext == null)
				{
					this.m_cachedReportContext = base.DataProvider.CreateReportContext(base.RequestInfo.ReportContext);
					this.m_cachedReportContext.RetrieveProperties();
				}
				return this.m_cachedReportContext.Properties;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool ExecuteExistingSnapshot
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x000296ED File Offset: 0x000278ED
		public override string Description
		{
			get
			{
				return base.RequestInfo.Session.SessionReport.Report.Description;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00005C88 File Offset: 0x00003E88
		public override DataSourceInfoCollection DataSources
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00005C88 File Offset: 0x00003E88
		public override DataSetInfoCollection DataSets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00029709 File Offset: 0x00027909
		public override Guid ReportId
		{
			get
			{
				throw new InternalCatalogException("using ReportID within existing session");
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00029715 File Offset: 0x00027915
		public override ReportSnapshot ExecutionSnapshot
		{
			get
			{
				return base.RequestInfo.Session.SessionReport.Report.SnapshotData;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00029731 File Offset: 0x00027931
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x00029748 File Offset: 0x00027948
		public override DateTime ExecutionDateTime
		{
			get
			{
				return base.RequestInfo.Session.SessionReport.ExecutionDateTime;
			}
			set
			{
				base.RequestInfo.Session.SessionReport.ExecutionDateTime = value;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00005BEF File Offset: 0x00003DEF
		public override bool CachingRequested
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00029760 File Offset: 0x00027960
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x00029777 File Offset: 0x00027977
		public override DateTime ExpirationDateTime
		{
			get
			{
				return base.RequestInfo.Session.SessionReport.ExpirationDateTime;
			}
			set
			{
				base.RequestInfo.Session.SessionReport.ExpirationDateTime = value;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0002978F File Offset: 0x0002798F
		public override bool FoundInCache
		{
			get
			{
				return base.RequestInfo.Session.SessionReport.FoundInCache;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x000297A6 File Offset: 0x000279A6
		public override UserProfileState UserDependencies
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x000297B2 File Offset: 0x000279B2
		public override int PageCount
		{
			get
			{
				return base.RequestInfo.Session.SessionReport.PageCount;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x000297C9 File Offset: 0x000279C9
		public override PaginationMode PaginationMode
		{
			get
			{
				return base.RequestInfo.Session.SessionReport.PaginationMode;
			}
		}

		// Token: 0x040004B0 RID: 1200
		private RSReportContext m_cachedReportContext;
	}
}

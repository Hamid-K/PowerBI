using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007ED RID: 2029
	internal sealed class IncrementalDataPipelineManager : DataPipelineManager
	{
		// Token: 0x06007182 RID: 29058 RVA: 0x001D7CFC File Offset: 0x001D5EFC
		public IncrementalDataPipelineManager(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet)
			: base(odpContext, dataSet)
		{
		}

		// Token: 0x06007183 RID: 29059 RVA: 0x001D7D06 File Offset: 0x001D5F06
		protected override void InternalStartProcessing()
		{
			Global.Tracer.Assert(this.m_runtimeDataSource == null, "Cannot StartProcessing twice for the same pipeline manager");
			this.m_runtimeDataSource = new RuntimeDataSourceIncrementalDataProcessing(this.m_dataSet, this.m_odpContext);
			this.m_runtimeDataSource.Initialize();
		}

		// Token: 0x06007184 RID: 29060 RVA: 0x001D7D42 File Offset: 0x001D5F42
		protected override void InternalStopProcessing()
		{
			if (this.m_runtimeDataSource != null)
			{
				this.m_runtimeDataSource.Teardown();
				this.m_odpContext.ReportRuntime.CurrentScope = null;
			}
		}

		// Token: 0x06007185 RID: 29061 RVA: 0x001D7D68 File Offset: 0x001D5F68
		public override void Abort()
		{
			base.Abort();
			if (this.m_runtimeDataSource != null)
			{
				this.m_runtimeDataSource.Abort();
			}
		}

		// Token: 0x06007186 RID: 29062 RVA: 0x001D7D83 File Offset: 0x001D5F83
		public override void Advance()
		{
			this.m_runtimeDataSource.Advance();
		}

		// Token: 0x17002696 RID: 9878
		// (get) Token: 0x06007187 RID: 29063 RVA: 0x001D7D90 File Offset: 0x001D5F90
		public override IOnDemandScopeInstance GroupTreeRoot
		{
			get
			{
				return this.m_runtimeDataSource.GroupTreeRoot;
			}
		}

		// Token: 0x17002697 RID: 9879
		// (get) Token: 0x06007188 RID: 29064 RVA: 0x001D7D9D File Offset: 0x001D5F9D
		protected override RuntimeDataSource RuntimeDataSource
		{
			get
			{
				return this.m_runtimeDataSource;
			}
		}

		// Token: 0x04003A70 RID: 14960
		private RuntimeDataSourceIncrementalDataProcessing m_runtimeDataSource;
	}
}

using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007E9 RID: 2025
	internal abstract class AtomicDataPipelineManager : DataPipelineManager
	{
		// Token: 0x0600716F RID: 29039 RVA: 0x001D7B7D File Offset: 0x001D5D7D
		public AtomicDataPipelineManager(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet)
			: base(odpContext, dataSet)
		{
		}

		// Token: 0x06007170 RID: 29040 RVA: 0x001D7B87 File Offset: 0x001D5D87
		protected override void InternalStartProcessing()
		{
			Global.Tracer.Assert(this.m_runtimeDataSource == null, "Cannot StartProcessing twice for the same pipeline manager");
			this.m_runtimeDataSource = this.CreateDataSource();
			this.m_runtimeDataSource.ProcessSingleOdp();
			this.m_odpContext.CheckAndThrowIfAborted();
		}

		// Token: 0x06007171 RID: 29041
		protected abstract RuntimeDataSourceDataProcessing CreateDataSource();

		// Token: 0x06007172 RID: 29042 RVA: 0x001D7BC3 File Offset: 0x001D5DC3
		protected override void InternalStopProcessing()
		{
			this.m_runtimeDataSource = null;
		}

		// Token: 0x06007173 RID: 29043 RVA: 0x001D7BCC File Offset: 0x001D5DCC
		public override void Advance()
		{
		}

		// Token: 0x17002692 RID: 9874
		// (get) Token: 0x06007174 RID: 29044 RVA: 0x001D7BCE File Offset: 0x001D5DCE
		public override IOnDemandScopeInstance GroupTreeRoot
		{
			get
			{
				return this.m_runtimeDataSource.RuntimeDataSet.GroupTreeRoot;
			}
		}

		// Token: 0x17002693 RID: 9875
		// (get) Token: 0x06007175 RID: 29045 RVA: 0x001D7BE0 File Offset: 0x001D5DE0
		protected override RuntimeDataSource RuntimeDataSource
		{
			get
			{
				return this.m_runtimeDataSource;
			}
		}

		// Token: 0x04003A6E RID: 14958
		private RuntimeDataSourceDataProcessing m_runtimeDataSource;
	}
}

using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007EC RID: 2028
	internal sealed class DataSetAggregateDataPipelineManager : DataPipelineManager
	{
		// Token: 0x0600717B RID: 29051 RVA: 0x001D7C3D File Offset: 0x001D5E3D
		public DataSetAggregateDataPipelineManager(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet)
			: base(odpContext, dataSet)
		{
		}

		// Token: 0x0600717C RID: 29052 RVA: 0x001D7C48 File Offset: 0x001D5E48
		protected override void InternalStartProcessing()
		{
			Global.Tracer.Assert(this.m_runtimeDataSource == null, "Cannot StartProcessing twice for the same pipeline manager");
			this.m_runtimeDataSource = new RuntimeAggregationIncrementalDataSource(this.m_dataSet, this.m_odpContext);
			this.m_runtimeDataSource.Initialize();
			this.m_runtimeDataSource.CalculateDataSetAggregates();
		}

		// Token: 0x0600717D RID: 29053 RVA: 0x001D7C9A File Offset: 0x001D5E9A
		protected override void InternalStopProcessing()
		{
			if (this.m_runtimeDataSource != null)
			{
				this.m_runtimeDataSource.Teardown();
				this.m_odpContext.ReportRuntime.CurrentScope = null;
			}
		}

		// Token: 0x0600717E RID: 29054 RVA: 0x001D7CC0 File Offset: 0x001D5EC0
		public override void Advance()
		{
		}

		// Token: 0x17002694 RID: 9876
		// (get) Token: 0x0600717F RID: 29055 RVA: 0x001D7CC2 File Offset: 0x001D5EC2
		public override IOnDemandScopeInstance GroupTreeRoot
		{
			get
			{
				Global.Tracer.Assert(false, "DataSetAggregateDataPipelineManager GroupTreeRoot property must not be accessed");
				throw new NotImplementedException();
			}
		}

		// Token: 0x06007180 RID: 29056 RVA: 0x001D7CD9 File Offset: 0x001D5ED9
		public override void Abort()
		{
			base.Abort();
			if (this.RuntimeDataSource != null)
			{
				this.RuntimeDataSource.Abort();
			}
		}

		// Token: 0x17002695 RID: 9877
		// (get) Token: 0x06007181 RID: 29057 RVA: 0x001D7CF4 File Offset: 0x001D5EF4
		protected override RuntimeDataSource RuntimeDataSource
		{
			get
			{
				return this.m_runtimeDataSource;
			}
		}

		// Token: 0x04003A6F RID: 14959
		private RuntimeAggregationIncrementalDataSource m_runtimeDataSource;
	}
}

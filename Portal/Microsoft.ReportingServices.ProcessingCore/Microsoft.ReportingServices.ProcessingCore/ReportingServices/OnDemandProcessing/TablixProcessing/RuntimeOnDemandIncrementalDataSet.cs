using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008F4 RID: 2292
	internal sealed class RuntimeOnDemandIncrementalDataSet : RuntimeIncrementalDataSetWithProcessingController
	{
		// Token: 0x06007E3C RID: 32316 RVA: 0x00208C7B File Offset: 0x00206E7B
		public RuntimeOnDemandIncrementalDataSet(DataSource dataSource, DataSet dataSet, DataSetInstance dataSetInstance, OnDemandProcessingContext odpContext)
			: base(dataSource, dataSet, dataSetInstance, odpContext)
		{
		}

		// Token: 0x06007E3D RID: 32317 RVA: 0x00208C88 File Offset: 0x00206E88
		public void Advance()
		{
			try
			{
				bool flag;
				while (this.ReadAndProcessOneRow(out flag) && !this.m_odpContext.StateManager.ShouldStopPipelineAdvance(!flag))
				{
				}
			}
			catch (Exception)
			{
				this.CleanupForException();
				this.FinalCleanup();
				throw;
			}
		}

		// Token: 0x06007E3E RID: 32318 RVA: 0x00208CD8 File Offset: 0x00206ED8
		private bool ReadAndProcessOneRow(out bool isAggregateRow)
		{
			isAggregateRow = false;
			int num;
			RecordRow recordRow = base.ReadOneRow(out num);
			if (recordRow == null)
			{
				return false;
			}
			isAggregateRow = recordRow.IsAggregateRow;
			this.m_dataProcessingController.NextRow(recordRow, num, true, base.HasServerAggregateMetadata);
			return true;
		}
	}
}

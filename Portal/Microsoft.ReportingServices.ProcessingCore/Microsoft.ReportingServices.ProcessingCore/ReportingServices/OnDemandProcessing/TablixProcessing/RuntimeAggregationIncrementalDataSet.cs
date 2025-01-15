using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008F5 RID: 2293
	internal sealed class RuntimeAggregationIncrementalDataSet : RuntimeIncrementalDataSetWithProcessingController
	{
		// Token: 0x06007E3F RID: 32319 RVA: 0x00208D13 File Offset: 0x00206F13
		public RuntimeAggregationIncrementalDataSet(DataSource dataSource, DataSet dataSet, DataSetInstance dataSetInstance, OnDemandProcessingContext odpContext)
			: base(dataSource, dataSet, dataSetInstance, odpContext)
		{
		}

		// Token: 0x06007E40 RID: 32320 RVA: 0x00208D20 File Offset: 0x00206F20
		public void CalculateDataSetAggregates()
		{
			try
			{
				int num;
				RecordRow recordRow = base.ReadOneRow(out num);
				if (recordRow != null)
				{
					this.m_dataProcessingController.NextRow(recordRow, num, true, base.HasServerAggregateMetadata);
				}
				this.m_dataProcessingController.AllRowsRead();
			}
			catch (Exception)
			{
				this.CleanupForException();
				this.FinalCleanup();
				throw;
			}
		}

		// Token: 0x17002913 RID: 10515
		// (get) Token: 0x06007E41 RID: 32321 RVA: 0x00208D7C File Offset: 0x00206F7C
		protected override bool ShouldCancelCommandDuringCleanup
		{
			get
			{
				return false;
			}
		}
	}
}

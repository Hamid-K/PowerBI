using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200080F RID: 2063
	internal sealed class RuntimeLiveDataReaderDataSet : RuntimeIncrementalDataSet
	{
		// Token: 0x060072A0 RID: 29344 RVA: 0x001DCA59 File Offset: 0x001DAC59
		internal RuntimeLiveDataReaderDataSet(DataSource dataSource, DataSet dataSet, DataSetInstance dataSetInstance, OnDemandProcessingContext odpContext)
			: base(dataSource, dataSet, dataSetInstance, odpContext)
		{
		}

		// Token: 0x060072A1 RID: 29345 RVA: 0x001DCA68 File Offset: 0x001DAC68
		internal RecordRow ReadNextRow()
		{
			RecordRow recordRow;
			try
			{
				int num;
				recordRow = base.ReadOneRow(out num);
			}
			catch (Exception)
			{
				this.CleanupForException();
				this.FinalCleanup();
				throw;
			}
			return recordRow;
		}

		// Token: 0x060072A2 RID: 29346 RVA: 0x001DCAA0 File Offset: 0x001DACA0
		protected override void InitializeBeforeProcessingRows(bool aReaderExtensionsSupported)
		{
		}

		// Token: 0x060072A3 RID: 29347 RVA: 0x001DCAA2 File Offset: 0x001DACA2
		protected override void ProcessExtendedPropertyMappings()
		{
		}
	}
}

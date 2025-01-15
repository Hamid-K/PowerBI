using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200080D RID: 2061
	internal sealed class RuntimeIdcIncrementalDataSource : RuntimeIncrementalDataSource
	{
		// Token: 0x06007297 RID: 29335 RVA: 0x001DC8F5 File Offset: 0x001DAAF5
		internal RuntimeIdcIncrementalDataSource(DataSet dataSet, OnDemandProcessingContext odpContext)
			: base(odpContext.ReportDefinition, dataSet, odpContext)
		{
		}

		// Token: 0x06007298 RID: 29336 RVA: 0x001DC908 File Offset: 0x001DAB08
		protected override List<RuntimeDataSet> CreateRuntimeDataSets()
		{
			DataSetInstance dataSetInstance = new DataSetInstance(this.m_dataSet);
			this.m_runtimeDataSet = new RuntimeIdcIncrementalDataSet(base.DataSourceDefinition, this.m_dataSet, dataSetInstance, base.OdpContext);
			return new List<RuntimeDataSet>(1) { this.m_runtimeDataSet };
		}

		// Token: 0x06007299 RID: 29337 RVA: 0x001DC954 File Offset: 0x001DAB54
		public bool SetupNextRow()
		{
			bool flag;
			try
			{
				flag = this.m_runtimeDataSet.GetNextRow() != null;
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
				this.FinalCleanup();
				throw;
			}
			return flag;
		}

		// Token: 0x170026D6 RID: 9942
		// (get) Token: 0x0600729A RID: 29338 RVA: 0x001DC994 File Offset: 0x001DAB94
		protected override RuntimeIncrementalDataSet RuntimeDataSet
		{
			get
			{
				return this.m_runtimeDataSet;
			}
		}

		// Token: 0x04003AC2 RID: 15042
		private RuntimeIdcIncrementalDataSet m_runtimeDataSet;
	}
}

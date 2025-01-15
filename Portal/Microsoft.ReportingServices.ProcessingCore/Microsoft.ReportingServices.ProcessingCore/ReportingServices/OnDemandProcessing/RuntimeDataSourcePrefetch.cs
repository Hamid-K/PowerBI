using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000803 RID: 2051
	internal sealed class RuntimeDataSourcePrefetch : RuntimeAtomicDataSource
	{
		// Token: 0x06007265 RID: 29285 RVA: 0x001DBFE4 File Offset: 0x001DA1E4
		internal RuntimeDataSourcePrefetch(Report report, ReportInstance reportInstance, DataSource dataSource, OnDemandProcessingContext processingContext, bool mergeTransactions)
			: base(report, dataSource, processingContext, mergeTransactions)
		{
			this.m_reportInstance = reportInstance;
		}

		// Token: 0x170026C6 RID: 9926
		// (get) Token: 0x06007266 RID: 29286 RVA: 0x001DBFF9 File Offset: 0x001DA1F9
		protected override bool AllowConcurrentProcessing
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170026C7 RID: 9927
		// (get) Token: 0x06007267 RID: 29287 RVA: 0x001DBFFC File Offset: 0x001DA1FC
		protected override bool CreatesDataChunks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170026C8 RID: 9928
		// (get) Token: 0x06007268 RID: 29288 RVA: 0x001DC000 File Offset: 0x001DA200
		internal override bool NoRows
		{
			get
			{
				bool flag = this.m_initialNoRowsState;
				if (this.m_runtimeDataSets != null)
				{
					foreach (RuntimeDataSet runtimeDataSet in this.m_runtimeDataSets)
					{
						if (!runtimeDataSet.UsedOnlyInParameters)
						{
							flag &= runtimeDataSet.NoRows;
						}
					}
				}
				return flag;
			}
		}

		// Token: 0x06007269 RID: 29289 RVA: 0x001DC070 File Offset: 0x001DA270
		protected override List<RuntimeDataSet> CreateRuntimeDataSets()
		{
			int count = base.DataSourceDefinition.DataSets.Count;
			List<RuntimeDataSet> list = new List<RuntimeDataSet>(count);
			for (int i = 0; i < count; i++)
			{
				DataSet dataSet = base.DataSourceDefinition.DataSets[i];
				RuntimeDataSet runtimeDataSet = null;
				if (!dataSet.UsedOnlyInParameters)
				{
					this.m_initialNoRowsState = true;
				}
				if (!dataSet.UsedOnlyInParameters || base.DataSourceDefinition.Transaction)
				{
					if (base.OdpContext.InSubreport && base.OdpContext.FoundExistingSubReportInstance)
					{
						DataSetInstance dataSetInstance = base.OdpContext.GetDataSetInstance(dataSet);
						this.m_initialNoRowsState &= dataSetInstance.NoRows;
					}
					else
					{
						DataSetInstance dataSetInstance2 = new DataSetInstance(dataSet);
						this.m_reportInstance.SetDataSetInstance(dataSetInstance2);
						if (dataSet.IndexInCollection == base.ReportDefinition.FirstDataSetIndexToProcess && !dataSet.UsedOnlyInParameters)
						{
							runtimeDataSet = new RuntimeOnDemandDataSet(base.DataSourceDefinition, dataSet, dataSetInstance2, base.OdpContext, true, true, true);
						}
						else
						{
							bool flag = !dataSet.UsedOnlyInParameters;
							runtimeDataSet = new RuntimePrefetchDataSet(base.DataSourceDefinition, dataSet, dataSetInstance2, base.OdpContext, true, flag);
						}
					}
				}
				if (runtimeDataSet != null)
				{
					list.Add(runtimeDataSet);
				}
			}
			return list;
		}

		// Token: 0x04003AB5 RID: 15029
		private readonly ReportInstance m_reportInstance;

		// Token: 0x04003AB6 RID: 15030
		private bool m_initialNoRowsState;
	}
}

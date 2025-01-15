using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000806 RID: 2054
	internal sealed class RuntimeDataSourceParameters : RuntimeAtomicDataSource
	{
		// Token: 0x06007274 RID: 29300 RVA: 0x001DC34A File Offset: 0x001DA54A
		internal RuntimeDataSourceParameters(Microsoft.ReportingServices.ReportIntermediateFormat.Report report, Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource, OnDemandProcessingContext processingContext, int parameterDataSetIndex, ReportParameterDataSetCache aCache)
			: base(report, dataSource, processingContext, false)
		{
			Global.Tracer.Assert(parameterDataSetIndex != -1, "Parameter DataSet index must be specified when processing parameters");
			this.m_parameterDataSetIndex = parameterDataSetIndex;
			this.m_paramDataCache = aCache;
		}

		// Token: 0x06007275 RID: 29301 RVA: 0x001DC380 File Offset: 0x001DA580
		protected override List<RuntimeDataSet> CreateRuntimeDataSets()
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = base.DataSourceDefinition.DataSets[this.m_parameterDataSetIndex];
			DataSetInstance dataSetInstance = new DataSetInstance(dataSet);
			this.m_runtimeDataSet = new RuntimeParameterDataSet(base.DataSourceDefinition, dataSet, dataSetInstance, base.OdpContext, true, this.m_paramDataCache);
			return new List<RuntimeDataSet>(1) { this.m_runtimeDataSet };
		}

		// Token: 0x170026CC RID: 9932
		// (get) Token: 0x06007276 RID: 29302 RVA: 0x001DC3DD File Offset: 0x001DA5DD
		internal override bool NoRows
		{
			get
			{
				return base.CheckNoRows(this.m_runtimeDataSet);
			}
		}

		// Token: 0x04003AB9 RID: 15033
		private RuntimeParameterDataSet m_runtimeDataSet;

		// Token: 0x04003ABA RID: 15034
		private readonly int m_parameterDataSetIndex;

		// Token: 0x04003ABB RID: 15035
		private readonly ReportParameterDataSetCache m_paramDataCache;
	}
}

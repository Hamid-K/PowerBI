using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000805 RID: 2053
	internal sealed class RuntimeDataSourceFullDataProcessing : RuntimeDataSourceDataProcessing
	{
		// Token: 0x06007270 RID: 29296 RVA: 0x001DC2C6 File Offset: 0x001DA4C6
		internal RuntimeDataSourceFullDataProcessing(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, OnDemandProcessingContext processingContext)
			: base(dataSet, processingContext)
		{
		}

		// Token: 0x170026CB RID: 9931
		// (get) Token: 0x06007271 RID: 29297 RVA: 0x001DC2D0 File Offset: 0x001DA4D0
		protected override bool NeedsExecutionLogging
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007272 RID: 29298 RVA: 0x001DC2D4 File Offset: 0x001DA4D4
		protected override RuntimeOnDemandDataSet CreateRuntimeDataSet()
		{
			OnDemandProcessingContext odpContext = base.OdpContext;
			DataSetInstance dataSetInstance = odpContext.CurrentReportInstance.GetDataSetInstance(this.m_dataSet, odpContext);
			if (odpContext.IsTablixProcessingComplete(this.m_dataSet.IndexInCollection))
			{
				Global.Tracer.Trace(TraceLevel.Warning, "Tablix processing is being attempted multiple times on DataSet '{0}'.", new object[] { this.m_dataSet.Name });
			}
			return new RuntimeOnDemandDataSet(base.DataSourceDefinition, this.m_dataSet, dataSetInstance, odpContext, false, true, true);
		}

		// Token: 0x06007273 RID: 29299 RVA: 0x001DC348 File Offset: 0x001DA548
		protected override void OpenInitialConnectionAndTransaction()
		{
		}
	}
}

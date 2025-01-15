using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007BD RID: 1981
	internal sealed class DataSetImpl : Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.DataSet
	{
		// Token: 0x06007057 RID: 28759 RVA: 0x001D4374 File Offset: 0x001D2574
		internal DataSetImpl(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSetDef, DataSetInstance dataSetInstance, DateTime reportExecutionTime)
		{
			this.m_dataSet = dataSetDef;
			this.Update(dataSetInstance, reportExecutionTime);
		}

		// Token: 0x17002647 RID: 9799
		// (get) Token: 0x06007058 RID: 28760 RVA: 0x001D438C File Offset: 0x001D258C
		public override string CommandText
		{
			get
			{
				string text = null;
				if (this.m_dataSetInstance != null)
				{
					text = this.m_dataSetInstance.CommandText;
				}
				if (text == null && this.m_dataSet.Query != null && this.m_dataSet.Query.CommandText != null && !this.m_dataSet.Query.CommandText.IsExpression && this.m_dataSet.Query.CommandText.Value != null)
				{
					text = this.m_dataSet.Query.CommandText.Value.ToString();
				}
				return text;
			}
		}

		// Token: 0x17002648 RID: 9800
		// (get) Token: 0x06007059 RID: 28761 RVA: 0x001D441C File Offset: 0x001D261C
		public override string RewrittenCommandText
		{
			get
			{
				string text = null;
				if (this.m_dataSetInstance != null)
				{
					text = this.m_dataSetInstance.RewrittenCommandText;
				}
				return text;
			}
		}

		// Token: 0x17002649 RID: 9801
		// (get) Token: 0x0600705A RID: 28762 RVA: 0x001D4440 File Offset: 0x001D2640
		public override DateTime ExecutionTime
		{
			get
			{
				return this.m_dataSetExecutionTime;
			}
		}

		// Token: 0x0600705B RID: 28763 RVA: 0x001D4448 File Offset: 0x001D2648
		internal void Update(DataSetInstance dataSetInstance, DateTime reportExecutionTime)
		{
			this.m_dataSetInstance = dataSetInstance;
			if (dataSetInstance != null)
			{
				this.m_dataSetExecutionTime = dataSetInstance.GetQueryExecutionTime(reportExecutionTime);
				return;
			}
			this.m_dataSetExecutionTime = reportExecutionTime;
		}

		// Token: 0x04003A06 RID: 14854
		private Microsoft.ReportingServices.ReportIntermediateFormat.DataSet m_dataSet;

		// Token: 0x04003A07 RID: 14855
		private DataSetInstance m_dataSetInstance;

		// Token: 0x04003A08 RID: 14856
		private DateTime m_dataSetExecutionTime;
	}
}

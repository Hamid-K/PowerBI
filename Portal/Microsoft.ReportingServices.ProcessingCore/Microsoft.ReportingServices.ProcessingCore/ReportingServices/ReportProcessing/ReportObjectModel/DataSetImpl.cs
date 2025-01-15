using System;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x02000799 RID: 1945
	internal sealed class DataSetImpl : DataSet
	{
		// Token: 0x06006C59 RID: 27737 RVA: 0x001B7728 File Offset: 0x001B5928
		internal DataSetImpl(DataSet dataSetDef)
		{
			this.m_dataSet = dataSetDef;
		}

		// Token: 0x170025B6 RID: 9654
		// (get) Token: 0x06006C5A RID: 27738 RVA: 0x001B7737 File Offset: 0x001B5937
		public override string CommandText
		{
			get
			{
				return this.m_dataSet.Query.CommandTextValue;
			}
		}

		// Token: 0x170025B7 RID: 9655
		// (get) Token: 0x06006C5B RID: 27739 RVA: 0x001B7749 File Offset: 0x001B5949
		public override string RewrittenCommandText
		{
			get
			{
				return this.m_dataSet.Query.RewrittenCommandText;
			}
		}

		// Token: 0x170025B8 RID: 9656
		// (get) Token: 0x06006C5C RID: 27740 RVA: 0x001B775B File Offset: 0x001B595B
		public override DateTime ExecutionTime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x04003676 RID: 13942
		private DataSet m_dataSet;
	}
}

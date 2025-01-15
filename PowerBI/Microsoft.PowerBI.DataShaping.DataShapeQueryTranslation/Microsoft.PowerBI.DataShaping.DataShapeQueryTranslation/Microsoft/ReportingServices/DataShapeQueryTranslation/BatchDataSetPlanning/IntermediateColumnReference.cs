using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000191 RID: 401
	internal class IntermediateColumnReference
	{
		// Token: 0x06000DC8 RID: 3528 RVA: 0x00038698 File Offset: 0x00036898
		internal IntermediateColumnReference(string columnName, string outputTableName)
		{
			this.m_columnName = columnName;
			this.m_outputTableName = outputTableName;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x000386AE File Offset: 0x000368AE
		internal BatchColumnReference Bind(BatchDataSetPlan plan, OutputTableMapping outputTableMapping)
		{
			return new BatchColumnReference(new BatchDataBinding(plan, this.GetOutputTableIndex(outputTableMapping)), this.m_columnName);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x000386C8 File Offset: 0x000368C8
		protected int GetOutputTableIndex(OutputTableMapping outputTableMapping)
		{
			return outputTableMapping.IndexOf(this.m_outputTableName);
		}

		// Token: 0x040006C5 RID: 1733
		protected readonly string m_columnName;

		// Token: 0x040006C6 RID: 1734
		protected string m_outputTableName;
	}
}

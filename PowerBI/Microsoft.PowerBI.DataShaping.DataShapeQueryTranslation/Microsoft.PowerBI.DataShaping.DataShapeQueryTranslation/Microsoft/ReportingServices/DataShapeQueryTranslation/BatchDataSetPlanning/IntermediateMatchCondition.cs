using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000192 RID: 402
	internal sealed class IntermediateMatchCondition : IntermediateColumnReference
	{
		// Token: 0x06000DCB RID: 3531 RVA: 0x000386D6 File Offset: 0x000368D6
		internal IntermediateMatchCondition(string columnName, string outputTableName, bool matchValue)
			: base(columnName, outputTableName)
		{
			this.m_matchValue = matchValue;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x000386E7 File Offset: 0x000368E7
		public bool MatchValue
		{
			get
			{
				return this.m_matchValue;
			}
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x000386EF File Offset: 0x000368EF
		internal new BatchMatchCondition Bind(BatchDataSetPlan plan, OutputTableMapping outputTableMapping)
		{
			return new BatchMatchCondition(new BatchDataBinding(plan, base.GetOutputTableIndex(outputTableMapping)), this.m_columnName, this.m_matchValue);
		}

		// Token: 0x040006C7 RID: 1735
		private readonly bool m_matchValue;
	}
}

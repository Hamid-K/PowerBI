using System;
using Microsoft.DataShaping;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000175 RID: 373
	internal sealed class IntermediateDiscardCondition : IntermediateColumnReference
	{
		// Token: 0x06000D64 RID: 3428 RVA: 0x000373A2 File Offset: 0x000355A2
		internal IntermediateDiscardCondition(string columnName, bool matchValue, BatchDiscardConditionOperator op)
			: base(columnName, "TemporaryOutputTable")
		{
			this.MatchValue = matchValue;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x000373B7 File Offset: 0x000355B7
		public bool MatchValue { get; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x000373BF File Offset: 0x000355BF
		public BatchDiscardConditionOperator Operator { get; }

		// Token: 0x06000D67 RID: 3431 RVA: 0x000373C7 File Offset: 0x000355C7
		public void SetOutputTableName(string name)
		{
			this.m_outputTableName = name;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x000373D0 File Offset: 0x000355D0
		internal new BatchDiscardCondition Bind(BatchDataSetPlan plan, OutputTableMapping outputTableMapping)
		{
			Contract.RetailAssert(this.m_outputTableName != "TemporaryOutputTable", "Temporary table name should have been changed");
			return new BatchDiscardCondition(new BatchDataBinding(plan, base.GetOutputTableIndex(outputTableMapping)), this.m_columnName, this.MatchValue, this.Operator);
		}

		// Token: 0x0400068E RID: 1678
		private const string TemporaryOutputTableName = "TemporaryOutputTable";
	}
}

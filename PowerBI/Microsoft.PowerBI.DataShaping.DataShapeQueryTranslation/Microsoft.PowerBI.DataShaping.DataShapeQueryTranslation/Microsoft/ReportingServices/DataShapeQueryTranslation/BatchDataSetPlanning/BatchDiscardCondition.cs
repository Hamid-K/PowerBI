using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000172 RID: 370
	internal sealed class BatchDiscardCondition : BatchColumnReference
	{
		// Token: 0x06000D5F RID: 3423 RVA: 0x00037306 File Offset: 0x00035506
		internal BatchDiscardCondition(BatchDataBinding binding, string columnName, bool matchValue, BatchDiscardConditionOperator op)
			: base(binding, columnName)
		{
			this.MatchValue = matchValue;
			this.Operator = op;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0003731F File Offset: 0x0003551F
		public bool MatchValue { get; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x00037327 File Offset: 0x00035527
		public BatchDiscardConditionOperator Operator { get; }

		// Token: 0x06000D62 RID: 3426 RVA: 0x00037330 File Offset: 0x00035530
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("DiscardCondition");
			builder.WriteAttribute<string>("ColumnName", base.ColumnName, false, false);
			builder.WriteAttribute<BatchDiscardConditionOperator>("Operator", this.Operator, false, false);
			builder.WriteAttribute<bool>("MatchValue", this.MatchValue, true, false);
			builder.WriteProperty<BatchDataBinding>("DataBinding", base.DataBinding, false);
			builder.EndObject();
		}
	}
}

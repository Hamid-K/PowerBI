using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000177 RID: 375
	internal sealed class BatchMatchCondition : BatchColumnReference
	{
		// Token: 0x06000D6C RID: 3436 RVA: 0x000374E4 File Offset: 0x000356E4
		internal BatchMatchCondition(BatchDataBinding binding, string columnName, bool matchValue)
			: base(binding, columnName)
		{
			this.m_matchValue = matchValue;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x000374F5 File Offset: 0x000356F5
		public bool MatchValue
		{
			get
			{
				return this.m_matchValue;
			}
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00037500 File Offset: 0x00035700
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("MatchCondition");
			builder.WriteAttribute<string>("ColumnName", base.ColumnName, false, false);
			builder.WriteAttribute<bool>("MatchValue", this.MatchValue, true, false);
			builder.WriteProperty<BatchDataBinding>("DataBinding", base.DataBinding, false);
			builder.EndObject();
		}

		// Token: 0x04000691 RID: 1681
		private readonly bool m_matchValue;
	}
}

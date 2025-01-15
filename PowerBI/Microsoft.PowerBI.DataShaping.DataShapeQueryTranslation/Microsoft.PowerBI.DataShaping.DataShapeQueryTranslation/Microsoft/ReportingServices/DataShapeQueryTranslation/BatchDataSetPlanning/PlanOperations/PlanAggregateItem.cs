using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200020E RID: 526
	internal abstract class PlanAggregateItem : IEquatable<PlanAggregateItem>, IStructuredToString
	{
		// Token: 0x0600124A RID: 4682
		internal abstract void Accept(IPlanAggregateItemVisitor visitor);

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600124B RID: 4683
		internal abstract bool PreferPlanName { get; }

		// Token: 0x0600124C RID: 4684 RVA: 0x00048B5D File Offset: 0x00046D5D
		public override int GetHashCode()
		{
			return this.GetHashCodeInternal();
		}

		// Token: 0x0600124D RID: 4685
		protected abstract int GetHashCodeInternal();

		// Token: 0x0600124E RID: 4686 RVA: 0x00048B65 File Offset: 0x00046D65
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlanAggregateItem);
		}

		// Token: 0x0600124F RID: 4687
		public abstract bool Equals(PlanAggregateItem other);

		// Token: 0x06001250 RID: 4688 RVA: 0x00048B74 File Offset: 0x00046D74
		public string ToString(ExpressionTable expressionTable)
		{
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(ExpressionStringBuilderFactory.Create(expressionTable, false), 0, false);
			this.WriteTo(structuredStringBuilder);
			return structuredStringBuilder.ToString();
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00048B9D File Offset: 0x00046D9D
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x06001252 RID: 4690
		public abstract void WriteTo(StructuredStringBuilder builder);
	}
}

using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000207 RID: 519
	internal abstract class PlanGroupByItem : IEquatable<PlanGroupByItem>, IStructuredToString
	{
		// Token: 0x06001215 RID: 4629
		internal abstract void Accept(IPlanGroupByItemVisitor visitor);

		// Token: 0x06001216 RID: 4630 RVA: 0x00048720 File Offset: 0x00046920
		public override int GetHashCode()
		{
			return this.GetHashCodeInternal();
		}

		// Token: 0x06001217 RID: 4631
		protected abstract int GetHashCodeInternal();

		// Token: 0x06001218 RID: 4632 RVA: 0x00048728 File Offset: 0x00046928
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlanGroupByItem);
		}

		// Token: 0x06001219 RID: 4633
		public abstract bool Equals(PlanGroupByItem other);

		// Token: 0x0600121A RID: 4634 RVA: 0x00048738 File Offset: 0x00046938
		public string ToString(ExpressionTable expressionTable)
		{
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(ExpressionStringBuilderFactory.Create(expressionTable, false), 0, false);
			this.WriteTo(structuredStringBuilder);
			return structuredStringBuilder.ToString();
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00048761 File Offset: 0x00046961
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x0600121C RID: 4636
		public abstract void WriteTo(StructuredStringBuilder builder);
	}
}

using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000217 RID: 535
	internal abstract class PlanProjectItem : IEquatable<PlanProjectItem>, IStructuredToString
	{
		// Token: 0x06001287 RID: 4743
		public abstract void Accept(IPlanProjectItemVisitor visitor);

		// Token: 0x06001288 RID: 4744 RVA: 0x00049138 File Offset: 0x00047338
		public override int GetHashCode()
		{
			return this.GetHashCodeInternal();
		}

		// Token: 0x06001289 RID: 4745
		protected abstract int GetHashCodeInternal();

		// Token: 0x0600128A RID: 4746 RVA: 0x00049140 File Offset: 0x00047340
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlanProjectItem);
		}

		// Token: 0x0600128B RID: 4747
		public abstract bool Equals(PlanProjectItem other);

		// Token: 0x0600128C RID: 4748 RVA: 0x00049150 File Offset: 0x00047350
		public string ToString(ExpressionTable expressionTable)
		{
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(ExpressionStringBuilderFactory.Create(expressionTable, false), 0, false);
			this.WriteTo(structuredStringBuilder);
			return structuredStringBuilder.ToString();
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00049179 File Offset: 0x00047379
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x0600128E RID: 4750
		public abstract void WriteTo(StructuredStringBuilder builder);
	}
}

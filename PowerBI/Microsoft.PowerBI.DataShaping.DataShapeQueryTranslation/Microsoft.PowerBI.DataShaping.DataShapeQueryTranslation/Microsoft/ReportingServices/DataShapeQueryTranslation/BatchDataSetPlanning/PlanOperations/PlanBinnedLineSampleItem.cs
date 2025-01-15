using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001F3 RID: 499
	internal abstract class PlanBinnedLineSampleItem : IEquatable<PlanBinnedLineSampleItem>, IStructuredToString
	{
		// Token: 0x06001145 RID: 4421
		internal abstract void Accept(IPlanBinnedLineSampleItemVisitor visitor);

		// Token: 0x06001146 RID: 4422 RVA: 0x00046E66 File Offset: 0x00045066
		public override int GetHashCode()
		{
			return this.GetHashCodeInternal();
		}

		// Token: 0x06001147 RID: 4423
		protected abstract int GetHashCodeInternal();

		// Token: 0x06001148 RID: 4424 RVA: 0x00046E6E File Offset: 0x0004506E
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlanBinnedLineSampleItem);
		}

		// Token: 0x06001149 RID: 4425
		public abstract bool Equals(PlanBinnedLineSampleItem other);

		// Token: 0x0600114A RID: 4426 RVA: 0x00046E7C File Offset: 0x0004507C
		public string ToString(ExpressionTable expressionTable)
		{
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(ExpressionStringBuilderFactory.Create(expressionTable, false), 0, false);
			this.WriteTo(structuredStringBuilder);
			return structuredStringBuilder.ToString();
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00046EA5 File Offset: 0x000450A5
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x0600114C RID: 4428
		public abstract void WriteTo(StructuredStringBuilder builder);
	}
}

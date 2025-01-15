using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000229 RID: 553
	internal abstract class PlanSortItem : IEquatable<PlanSortItem>, IStructuredToString
	{
		// Token: 0x06001304 RID: 4868
		public abstract void Accept(IPlanSortItemVisitor visitor);

		// Token: 0x06001305 RID: 4869 RVA: 0x00049DB5 File Offset: 0x00047FB5
		public override int GetHashCode()
		{
			return this.GetHashCodeInternal();
		}

		// Token: 0x06001306 RID: 4870
		protected abstract int GetHashCodeInternal();

		// Token: 0x06001307 RID: 4871 RVA: 0x00049DBD File Offset: 0x00047FBD
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlanSortItem);
		}

		// Token: 0x06001308 RID: 4872
		public abstract bool Equals(PlanSortItem other);

		// Token: 0x06001309 RID: 4873 RVA: 0x00049DCC File Offset: 0x00047FCC
		public string ToString(ExpressionTable expressionTable)
		{
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(ExpressionStringBuilderFactory.Create(expressionTable, false), 0, false);
			this.WriteTo(structuredStringBuilder);
			return structuredStringBuilder.ToString();
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00049DF5 File Offset: 0x00047FF5
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x0600130B RID: 4875
		public abstract void WriteTo(StructuredStringBuilder builder);
	}
}

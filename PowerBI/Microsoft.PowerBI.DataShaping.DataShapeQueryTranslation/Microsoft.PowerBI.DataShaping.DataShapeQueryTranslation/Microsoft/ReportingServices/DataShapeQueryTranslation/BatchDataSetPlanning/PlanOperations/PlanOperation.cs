using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001ED RID: 493
	internal abstract class PlanOperation : IEquatable<PlanOperation>, IStructuredToString
	{
		// Token: 0x06001111 RID: 4369
		internal abstract TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor);

		// Token: 0x06001112 RID: 4370 RVA: 0x000466BC File Offset: 0x000448BC
		public sealed override bool Equals(object obj)
		{
			return this.Equals(obj as PlanOperation);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x000466CA File Offset: 0x000448CA
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06001114 RID: 4372
		public abstract bool Equals(PlanOperation other);

		// Token: 0x06001115 RID: 4373 RVA: 0x000466DC File Offset: 0x000448DC
		protected static bool CheckReferenceAndTypeEquality<TOperation>(TOperation @this, PlanOperation other, out bool areEqual, out TOperation otherTyped) where TOperation : PlanOperation
		{
			if (@this == other)
			{
				areEqual = true;
				otherTyped = default(TOperation);
				return true;
			}
			if (@this == null || other == null)
			{
				areEqual = false;
				otherTyped = default(TOperation);
				return true;
			}
			otherTyped = other as TOperation;
			if (otherTyped == null)
			{
				areEqual = false;
				return true;
			}
			areEqual = false;
			return false;
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0004673C File Offset: 0x0004493C
		public string ToString(ExpressionTable expressionTable)
		{
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(ExpressionStringBuilderFactory.Create(expressionTable, false), 0, false);
			this.WriteTo(structuredStringBuilder);
			return structuredStringBuilder.ToString();
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00046765 File Offset: 0x00044965
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x06001118 RID: 4376
		public abstract void WriteTo(StructuredStringBuilder builder);
	}
}

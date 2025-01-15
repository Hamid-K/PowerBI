using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000230 RID: 560
	internal sealed class PlanOperationTopN : PlanOperationLimitByCount
	{
		// Token: 0x06001333 RID: 4915 RVA: 0x0004A213 File Offset: 0x00048413
		internal PlanOperationTopN(PlanOperation input, PlanExpression rowCount, IEnumerable<PlanSortItem> sorts, bool reverseSortOrder = false)
			: base(input, rowCount, sorts)
		{
			this.m_reverseSortOrder = reverseSortOrder;
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x0004A226 File Offset: 0x00048426
		public bool ReverseSortOrder
		{
			get
			{
				return this.m_reverseSortOrder;
			}
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0004A22E File Offset: 0x0004842E
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0004A238 File Offset: 0x00048438
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationTopN planOperationTopN;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationTopN>(this, other, out flag, out planOperationTopN))
			{
				return flag;
			}
			return base.CommonEquals(planOperationTopN) && this.ReverseSortOrder == planOperationTopN.ReverseSortOrder;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0004A270 File Offset: 0x00048470
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("TopN");
			builder.WriteAttribute<bool>("ReverseSortOrder", this.ReverseSortOrder, false, false);
			builder.WriteProperty<PlanExpression>("RowCount", base.RowCount, false);
			builder.WriteProperty<ReadOnlyCollection<PlanSortItem>>("Sorts", base.Sorts, false);
			builder.WriteProperty<PlanOperation>("Input", base.Input, false);
			builder.EndObject();
		}

		// Token: 0x04000877 RID: 2167
		private readonly bool m_reverseSortOrder;
	}
}

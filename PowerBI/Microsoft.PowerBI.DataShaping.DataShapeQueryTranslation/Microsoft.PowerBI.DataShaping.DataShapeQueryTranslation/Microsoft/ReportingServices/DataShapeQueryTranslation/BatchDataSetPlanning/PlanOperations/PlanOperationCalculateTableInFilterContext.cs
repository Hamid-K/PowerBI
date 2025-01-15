using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001F8 RID: 504
	internal sealed class PlanOperationCalculateTableInFilterContext : PlanOperation
	{
		// Token: 0x060011A1 RID: 4513 RVA: 0x000477D4 File Offset: 0x000459D4
		internal PlanOperationCalculateTableInFilterContext(PlanOperation input, IEnumerable<PlanOperation> filters)
		{
			this.m_input = input;
			this.m_filters = filters;
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x000477EA File Offset: 0x000459EA
		internal PlanOperation Input
		{
			get
			{
				return this.m_input;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x000477F2 File Offset: 0x000459F2
		internal IEnumerable<PlanOperation> Filters
		{
			get
			{
				return this.m_filters;
			}
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x000477FA File Offset: 0x000459FA
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00047804 File Offset: 0x00045A04
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationCalculateTableInFilterContext planOperationCalculateTableInFilterContext;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationCalculateTableInFilterContext>(this, other, out flag, out planOperationCalculateTableInFilterContext))
			{
				return flag;
			}
			return this.Input.Equals(planOperationCalculateTableInFilterContext.Input) && this.Filters.SequenceEqual(planOperationCalculateTableInFilterContext.Filters);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00047846 File Offset: 0x00045A46
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("CalculateTableInFilterContext");
			builder.WriteProperty<IEnumerable<PlanOperation>>("Filters", this.Filters, false);
			builder.WriteProperty<PlanOperation>("Input", this.Input, false);
			builder.EndObject();
		}

		// Token: 0x04000805 RID: 2053
		private readonly PlanOperation m_input;

		// Token: 0x04000806 RID: 2054
		private readonly IEnumerable<PlanOperation> m_filters;
	}
}

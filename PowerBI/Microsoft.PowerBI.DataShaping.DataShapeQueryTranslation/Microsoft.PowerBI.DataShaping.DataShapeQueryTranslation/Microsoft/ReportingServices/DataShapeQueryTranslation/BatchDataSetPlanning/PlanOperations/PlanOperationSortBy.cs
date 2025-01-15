using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000228 RID: 552
	internal sealed class PlanOperationSortBy : PlanOperation
	{
		// Token: 0x060012FE RID: 4862 RVA: 0x00049D05 File Offset: 0x00047F05
		internal PlanOperationSortBy(PlanOperation input, IEnumerable<PlanSortItem> sorts)
		{
			this.m_input = input;
			this.m_sorts = sorts.ToReadOnlyCollection<PlanSortItem>();
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x00049D20 File Offset: 0x00047F20
		public PlanOperation Input
		{
			get
			{
				return this.m_input;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x00049D28 File Offset: 0x00047F28
		public ReadOnlyCollection<PlanSortItem> Sorts
		{
			get
			{
				return this.m_sorts;
			}
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00049D30 File Offset: 0x00047F30
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00049D3C File Offset: 0x00047F3C
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationSortBy planOperationSortBy;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationSortBy>(this, other, out flag, out planOperationSortBy))
			{
				return flag;
			}
			return this.Input.Equals(planOperationSortBy.Input) && this.Sorts.SequenceEqual(planOperationSortBy.Sorts);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00049D7E File Offset: 0x00047F7E
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("SortBy");
			builder.WriteProperty<ReadOnlyCollection<PlanSortItem>>("Sorts", this.Sorts, false);
			builder.WriteProperty<PlanOperation>("Input", this.Input, false);
			builder.EndObject();
		}

		// Token: 0x0400086A RID: 2154
		private readonly PlanOperation m_input;

		// Token: 0x0400086B RID: 2155
		private readonly ReadOnlyCollection<PlanSortItem> m_sorts;
	}
}

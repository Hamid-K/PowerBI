using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000232 RID: 562
	internal sealed class PlanOperationTopNSkip : PlanOperationLimitByCount
	{
		// Token: 0x06001342 RID: 4930 RVA: 0x0004A456 File Offset: 0x00048656
		internal PlanOperationTopNSkip(PlanOperation input, PlanExpression rowCount, PlanExpression skipCount, IEnumerable<PlanSortItem> sorts, bool reverseSortOrder = false)
			: base(input, rowCount, sorts)
		{
			Contract.RetailAssert(sorts.Count<PlanSortItem>() <= 1, "sorts.Count() <= 1");
			this.m_skipCount = skipCount;
			this.m_reverseSortOrder = reverseSortOrder;
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x0004A488 File Offset: 0x00048688
		public PlanExpression SkipCount
		{
			get
			{
				return this.m_skipCount;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x0004A490 File Offset: 0x00048690
		public bool ReverseSortOrder
		{
			get
			{
				return this.m_reverseSortOrder;
			}
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0004A498 File Offset: 0x00048698
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0004A4A4 File Offset: 0x000486A4
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationTopNSkip planOperationTopNSkip;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationTopNSkip>(this, other, out flag, out planOperationTopNSkip))
			{
				return flag;
			}
			return base.CommonEquals(planOperationTopNSkip) && this.SkipCount == planOperationTopNSkip.SkipCount && this.ReverseSortOrder == planOperationTopNSkip.ReverseSortOrder;
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0004A4E8 File Offset: 0x000486E8
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("TopNSkip");
			builder.WriteAttribute<bool>("ReverseSortOrder", this.ReverseSortOrder, false, false);
			builder.WriteProperty<PlanExpression>("RowCount", base.RowCount, false);
			builder.WriteProperty<PlanExpression>("SkipCount", this.SkipCount, true);
			builder.WriteProperty<ReadOnlyCollection<PlanSortItem>>("Sorts", base.Sorts, false);
			builder.WriteProperty<PlanOperation>("Input", base.Input, false);
			builder.EndObject();
		}

		// Token: 0x0400087E RID: 2174
		private readonly PlanExpression m_skipCount;

		// Token: 0x0400087F RID: 2175
		private readonly bool m_reverseSortOrder;
	}
}

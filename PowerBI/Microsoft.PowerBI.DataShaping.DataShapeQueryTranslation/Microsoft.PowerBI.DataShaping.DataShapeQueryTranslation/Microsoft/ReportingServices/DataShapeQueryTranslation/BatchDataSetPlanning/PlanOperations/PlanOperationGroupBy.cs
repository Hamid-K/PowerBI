using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000206 RID: 518
	internal sealed class PlanOperationGroupBy : PlanOperation
	{
		// Token: 0x0600120D RID: 4621 RVA: 0x0004855C File Offset: 0x0004675C
		internal PlanOperationGroupBy(PlanOperation input, IEnumerable<PlanGroupByItem> groups, IEnumerable<PlanAggregateItem> aggregates)
		{
			this.m_input = input;
			this.m_groups = groups.ToReadOnlyCollection<PlanGroupByItem>();
			this.m_aggregates = aggregates.ToReadOnlyCollection<PlanAggregateItem>();
			PlanOperationGroupBy.EnsureUniquePlanNames(this.m_groups, this.m_aggregates);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00048594 File Offset: 0x00046794
		private static void EnsureUniquePlanNames(ReadOnlyCollection<PlanGroupByItem> groups, ReadOnlyCollection<PlanAggregateItem> aggregates)
		{
			NamingContext namingContext = new NamingContext(null);
			foreach (PlanGroupByItem planGroupByItem in groups)
			{
				PlanGroupByColumn planGroupByColumn = planGroupByItem as PlanGroupByColumn;
				if (planGroupByColumn != null && !namingContext.RegisterUniqueName(planGroupByColumn.Name))
				{
					Contract.RetailFail("Found duplicate group column");
				}
			}
			foreach (PlanAggregateItem planAggregateItem in aggregates)
			{
				PlanAggregateExpressionItem planAggregateExpressionItem = planAggregateItem as PlanAggregateExpressionItem;
				if (planAggregateExpressionItem != null && !namingContext.RegisterUniqueName(planAggregateExpressionItem.PlanName))
				{
					Contract.RetailFail("Found duplicate aggregate column");
				}
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x00048650 File Offset: 0x00046850
		public PlanOperation Input
		{
			get
			{
				return this.m_input;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x00048658 File Offset: 0x00046858
		public ReadOnlyCollection<PlanGroupByItem> Groups
		{
			get
			{
				return this.m_groups;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00048660 File Offset: 0x00046860
		public ReadOnlyCollection<PlanAggregateItem> Aggregates
		{
			get
			{
				return this.m_aggregates;
			}
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00048668 File Offset: 0x00046868
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00048674 File Offset: 0x00046874
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationGroupBy planOperationGroupBy;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationGroupBy>(this, other, out flag, out planOperationGroupBy))
			{
				return flag;
			}
			return this.Input.Equals(planOperationGroupBy.Input) && this.Groups.SequenceEqual(planOperationGroupBy.Groups) && this.Aggregates.SequenceEqual(planOperationGroupBy.Aggregates);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x000486CC File Offset: 0x000468CC
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("GroupBy");
			builder.WriteProperty<ReadOnlyCollection<PlanGroupByItem>>("Groups", this.Groups, false);
			builder.WriteProperty<ReadOnlyCollection<PlanAggregateItem>>("Aggregates", this.Aggregates, false);
			builder.WriteProperty<PlanOperation>("Input", this.Input, false);
			builder.EndObject();
		}

		// Token: 0x0400082C RID: 2092
		private readonly PlanOperation m_input;

		// Token: 0x0400082D RID: 2093
		private readonly ReadOnlyCollection<PlanGroupByItem> m_groups;

		// Token: 0x0400082E RID: 2094
		private readonly ReadOnlyCollection<PlanAggregateItem> m_aggregates;
	}
}

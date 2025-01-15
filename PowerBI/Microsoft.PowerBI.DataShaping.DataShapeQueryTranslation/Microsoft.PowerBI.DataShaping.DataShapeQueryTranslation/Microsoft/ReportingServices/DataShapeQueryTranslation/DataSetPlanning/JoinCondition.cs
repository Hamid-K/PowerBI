using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000DE RID: 222
	internal sealed class JoinCondition : IStructuredToString
	{
		// Token: 0x0600091A RID: 2330 RVA: 0x0002336A File Offset: 0x0002156A
		internal JoinCondition(DataMember dataMember, bool requiresReversedSortDirections, bool aggregateIndicatorJoinConditionOnly)
		{
			this.m_aggregateIndicatorJoinConditionOnly = aggregateIndicatorJoinConditionOnly;
			this.m_dataMember = dataMember;
			this.m_requiresReversedSortDirections = requiresReversedSortDirections;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00023387 File Offset: 0x00021587
		public bool AggregateIndicatorJoinConditionOnly
		{
			get
			{
				return this.m_aggregateIndicatorJoinConditionOnly;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0002338F File Offset: 0x0002158F
		public bool RequiresReversedSortDirections
		{
			get
			{
				return this.m_requiresReversedSortDirections;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00023397 File Offset: 0x00021597
		public DataMember DataMember
		{
			get
			{
				return this.m_dataMember;
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000233A0 File Offset: 0x000215A0
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("JoinCondition");
			builder.WriteAttribute<DataMember>("DataMember", this.m_dataMember, false, false);
			builder.WriteAttribute<bool>("RequiresReversedSortDirections", this.m_requiresReversedSortDirections, false, false);
			builder.WriteProperty<bool>("AggregateIndicatorJoinConditionOnly", this.m_aggregateIndicatorJoinConditionOnly, false);
			builder.EndObject();
		}

		// Token: 0x04000452 RID: 1106
		private readonly DataMember m_dataMember;

		// Token: 0x04000453 RID: 1107
		private readonly bool m_requiresReversedSortDirections;

		// Token: 0x04000454 RID: 1108
		private readonly bool m_aggregateIndicatorJoinConditionOnly;
	}
}

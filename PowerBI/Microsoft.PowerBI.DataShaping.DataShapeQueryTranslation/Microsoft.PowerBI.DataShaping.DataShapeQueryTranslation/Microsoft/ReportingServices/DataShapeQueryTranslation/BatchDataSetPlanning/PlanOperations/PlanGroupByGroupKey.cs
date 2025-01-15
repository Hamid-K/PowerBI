using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200020C RID: 524
	internal sealed class PlanGroupByGroupKey : PlanGroupByItem
	{
		// Token: 0x0600123E RID: 4670 RVA: 0x00048AA0 File Offset: 0x00046CA0
		internal PlanGroupByGroupKey(GroupKey groupKey, Identifier ownerId)
		{
			this.m_groupKey = groupKey;
			this.m_ownerId = ownerId;
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x00048AB6 File Offset: 0x00046CB6
		public GroupKey GroupKey
		{
			get
			{
				return this.m_groupKey;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00048ABE File Offset: 0x00046CBE
		public Identifier OwnerId
		{
			get
			{
				return this.m_ownerId;
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00048AC6 File Offset: 0x00046CC6
		internal override void Accept(IPlanGroupByItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00048ACF File Offset: 0x00046CCF
		protected override int GetHashCodeInternal()
		{
			return Hashing.CombineHash(this.m_groupKey.GetHashCode(), this.m_ownerId.GetHashCode());
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00048AEC File Offset: 0x00046CEC
		public override bool Equals(PlanGroupByItem other)
		{
			PlanGroupByGroupKey planGroupByGroupKey = other as PlanGroupByGroupKey;
			return planGroupByGroupKey != null && this.GroupKey == planGroupByGroupKey.GroupKey && this.OwnerId == planGroupByGroupKey.OwnerId;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00048B24 File Offset: 0x00046D24
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("GroupByGroupKey");
			builder.WriteAttribute<GroupKey>("GroupKey", this.m_groupKey, false, false);
			builder.WriteAttribute<Identifier>("OwnerId", this.m_ownerId, false, false);
			builder.EndObject();
		}

		// Token: 0x04000837 RID: 2103
		private readonly GroupKey m_groupKey;

		// Token: 0x04000838 RID: 2104
		private readonly Identifier m_ownerId;
	}
}

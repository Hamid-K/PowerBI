using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200022A RID: 554
	internal sealed class PlanMemberSortItem : PlanSortItem
	{
		// Token: 0x0600130D RID: 4877 RVA: 0x00049E06 File Offset: 0x00048006
		internal PlanMemberSortItem(DataMember member)
		{
			this.m_member = member;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00049E15 File Offset: 0x00048015
		public DataMember Member
		{
			get
			{
				return this.m_member;
			}
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00049E1D File Offset: 0x0004801D
		public override void Accept(IPlanSortItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00049E26 File Offset: 0x00048026
		protected override int GetHashCodeInternal()
		{
			return this.m_member.GetHashCode();
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00049E34 File Offset: 0x00048034
		public override bool Equals(PlanSortItem other)
		{
			PlanMemberSortItem planMemberSortItem = other as PlanMemberSortItem;
			return planMemberSortItem != null && this.Member == planMemberSortItem.Member;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00049E5B File Offset: 0x0004805B
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("MemberSortItem");
			builder.WriteAttribute<string>("Member", this.m_member.Id.Value, false, false);
			builder.EndObject();
		}

		// Token: 0x0400086C RID: 2156
		private readonly DataMember m_member;
	}
}

using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001F4 RID: 500
	internal sealed class PlanBinnedLineSampleMember : PlanBinnedLineSampleItem
	{
		// Token: 0x0600114E RID: 4430 RVA: 0x00046EB6 File Offset: 0x000450B6
		internal PlanBinnedLineSampleMember(DataMember member)
		{
			this.m_member = member;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x00046EC5 File Offset: 0x000450C5
		public DataMember Member
		{
			get
			{
				return this.m_member;
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00046ECD File Offset: 0x000450CD
		internal override void Accept(IPlanBinnedLineSampleItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00046ED6 File Offset: 0x000450D6
		protected override int GetHashCodeInternal()
		{
			return this.m_member.GetHashCode();
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00046EE4 File Offset: 0x000450E4
		public override bool Equals(PlanBinnedLineSampleItem other)
		{
			PlanBinnedLineSampleMember planBinnedLineSampleMember = other as PlanBinnedLineSampleMember;
			return planBinnedLineSampleMember != null && this.Member == planBinnedLineSampleMember.Member;
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00046F0B File Offset: 0x0004510B
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("BinnedLineSampleMember");
			builder.WriteAttribute<string>("Member", this.m_member.Id.Value, false, false);
			builder.EndObject();
		}

		// Token: 0x04000803 RID: 2051
		private readonly DataMember m_member;
	}
}

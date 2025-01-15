using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000213 RID: 531
	internal sealed class PlanOperationLeftOuterJoin : PlanOperation
	{
		// Token: 0x0600126C RID: 4716 RVA: 0x00048E0D File Offset: 0x0004700D
		internal PlanOperationLeftOuterJoin(PlanOperation left, PlanOperation right)
		{
			this.m_left = left;
			this.m_right = right;
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00048E23 File Offset: 0x00047023
		public PlanOperation Left
		{
			get
			{
				return this.m_left;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x00048E2B File Offset: 0x0004702B
		public PlanOperation Right
		{
			get
			{
				return this.m_right;
			}
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00048E33 File Offset: 0x00047033
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00048E3C File Offset: 0x0004703C
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationLeftOuterJoin planOperationLeftOuterJoin;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationLeftOuterJoin>(this, other, out flag, out planOperationLeftOuterJoin))
			{
				return flag;
			}
			return this.Left.Equals(planOperationLeftOuterJoin.Left) && this.Right.Equals(planOperationLeftOuterJoin.Right);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00048E7E File Offset: 0x0004707E
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("LeftOuterJoin");
			builder.WriteProperty<PlanOperation>("Left", this.Left, false);
			builder.WriteProperty<PlanOperation>("Right", this.Right, false);
			builder.EndObject();
		}

		// Token: 0x04000840 RID: 2112
		private readonly PlanOperation m_left;

		// Token: 0x04000841 RID: 2113
		private readonly PlanOperation m_right;
	}
}

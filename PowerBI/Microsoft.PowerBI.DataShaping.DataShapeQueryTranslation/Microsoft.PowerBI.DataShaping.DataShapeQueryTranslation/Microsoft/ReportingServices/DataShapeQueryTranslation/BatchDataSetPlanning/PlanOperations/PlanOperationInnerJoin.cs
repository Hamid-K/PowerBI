using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000212 RID: 530
	internal sealed class PlanOperationInnerJoin : PlanOperation
	{
		// Token: 0x06001266 RID: 4710 RVA: 0x00048D63 File Offset: 0x00046F63
		internal PlanOperationInnerJoin(PlanOperation left, PlanOperation right)
		{
			this.m_left = left;
			this.m_right = right;
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x00048D79 File Offset: 0x00046F79
		public PlanOperation Left
		{
			get
			{
				return this.m_left;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x00048D81 File Offset: 0x00046F81
		public PlanOperation Right
		{
			get
			{
				return this.m_right;
			}
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00048D89 File Offset: 0x00046F89
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00048D94 File Offset: 0x00046F94
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationInnerJoin planOperationInnerJoin;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationInnerJoin>(this, other, out flag, out planOperationInnerJoin))
			{
				return flag;
			}
			return this.Left.Equals(planOperationInnerJoin.Left) && this.Right.Equals(planOperationInnerJoin.Right);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00048DD6 File Offset: 0x00046FD6
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("InnerJoin");
			builder.WriteProperty<PlanOperation>("Left", this.Left, false);
			builder.WriteProperty<PlanOperation>("Right", this.Right, false);
			builder.EndObject();
		}

		// Token: 0x0400083E RID: 2110
		private readonly PlanOperation m_left;

		// Token: 0x0400083F RID: 2111
		private readonly PlanOperation m_right;
	}
}

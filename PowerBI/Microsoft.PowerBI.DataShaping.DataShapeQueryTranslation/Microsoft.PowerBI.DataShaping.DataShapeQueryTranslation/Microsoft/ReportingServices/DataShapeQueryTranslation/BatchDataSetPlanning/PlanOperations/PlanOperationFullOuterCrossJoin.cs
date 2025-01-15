using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000204 RID: 516
	internal sealed class PlanOperationFullOuterCrossJoin : PlanOperation
	{
		// Token: 0x060011F4 RID: 4596 RVA: 0x0004815A File Offset: 0x0004635A
		internal PlanOperationFullOuterCrossJoin(IReadOnlyList<PlanOperation> tables)
		{
			this.Tables = tables;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x00048169 File Offset: 0x00046369
		public IReadOnlyList<PlanOperation> Tables { get; }

		// Token: 0x060011F6 RID: 4598 RVA: 0x00048171 File Offset: 0x00046371
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0004817C File Offset: 0x0004637C
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationFullOuterCrossJoin planOperationFullOuterCrossJoin;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationFullOuterCrossJoin>(this, other, out flag, out planOperationFullOuterCrossJoin))
			{
				return flag;
			}
			return this.Tables.SequenceEqual(planOperationFullOuterCrossJoin.Tables);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x000481A9 File Offset: 0x000463A9
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("FullOuterCrossJoin");
			builder.WriteProperty<IReadOnlyList<PlanOperation>>("Tables", this.Tables, false);
			builder.EndObject();
		}
	}
}

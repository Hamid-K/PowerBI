using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001F9 RID: 505
	internal sealed class PlanOperationClearDefaultContext : PlanOperation
	{
		// Token: 0x060011A7 RID: 4519 RVA: 0x0004787D File Offset: 0x00045A7D
		internal PlanOperationClearDefaultContext(IReadOnlyDefaultContextManager manager)
		{
			this.DefaultContextManager = manager;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0004788C File Offset: 0x00045A8C
		internal IReadOnlyDefaultContextManager DefaultContextManager { get; }

		// Token: 0x060011A9 RID: 4521 RVA: 0x00047894 File Offset: 0x00045A94
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x000478A0 File Offset: 0x00045AA0
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationClearDefaultContext planOperationClearDefaultContext;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationClearDefaultContext>(this, other, out flag, out planOperationClearDefaultContext))
			{
				return flag;
			}
			return this.DefaultContextManager.Equals(planOperationClearDefaultContext.DefaultContextManager);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000478CD File Offset: 0x00045ACD
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("ClearDefaultContext");
			builder.WriteAttribute<IReadOnlyDefaultContextManager>("DefaultContextManager", this.DefaultContextManager, false, false);
			builder.EndObject();
		}
	}
}

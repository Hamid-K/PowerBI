using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000201 RID: 513
	internal sealed class PlanOperationEnsureUniqueUnqualifiedNames : PlanOperation
	{
		// Token: 0x060011DF RID: 4575 RVA: 0x00047EEE File Offset: 0x000460EE
		internal PlanOperationEnsureUniqueUnqualifiedNames(PlanOperation input, bool forceRename)
		{
			this.m_input = input;
			this.m_forceRename = forceRename;
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x00047F04 File Offset: 0x00046104
		public PlanOperation Input
		{
			get
			{
				return this.m_input;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00047F0C File Offset: 0x0004610C
		public bool ForceRename
		{
			get
			{
				return this.m_forceRename;
			}
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00047F14 File Offset: 0x00046114
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00047F20 File Offset: 0x00046120
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationEnsureUniqueUnqualifiedNames planOperationEnsureUniqueUnqualifiedNames;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationEnsureUniqueUnqualifiedNames>(this, other, out flag, out planOperationEnsureUniqueUnqualifiedNames))
			{
				return flag;
			}
			return this.Input.Equals(planOperationEnsureUniqueUnqualifiedNames.Input) && this.ForceRename == planOperationEnsureUniqueUnqualifiedNames.ForceRename;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00047F5F File Offset: 0x0004615F
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("EnsureUniqueUnqualifiedNames");
			builder.WriteAttribute<bool>("ForceRename", this.ForceRename, false, false);
			builder.WriteProperty<PlanOperation>("Input", this.Input, false);
			builder.EndObject();
		}

		// Token: 0x04000816 RID: 2070
		private readonly PlanOperation m_input;

		// Token: 0x04000817 RID: 2071
		private readonly bool m_forceRename;
	}
}

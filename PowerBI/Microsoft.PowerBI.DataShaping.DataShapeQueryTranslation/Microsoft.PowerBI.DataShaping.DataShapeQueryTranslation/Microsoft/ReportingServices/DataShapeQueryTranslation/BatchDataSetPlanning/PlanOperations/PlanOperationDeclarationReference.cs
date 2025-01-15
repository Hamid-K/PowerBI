using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001FF RID: 511
	internal sealed class PlanOperationDeclarationReference : PlanOperation
	{
		// Token: 0x060011D4 RID: 4564 RVA: 0x00047DD4 File Offset: 0x00045FD4
		internal PlanOperationDeclarationReference(string declarationName, bool canExpandToMultiTables = false)
		{
			this.DeclarationName = declarationName;
			this.CanExpandToMultiTables = canExpandToMultiTables;
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x00047DEA File Offset: 0x00045FEA
		public string DeclarationName { get; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x00047DF2 File Offset: 0x00045FF2
		public bool CanExpandToMultiTables { get; }

		// Token: 0x060011D7 RID: 4567 RVA: 0x00047DFA File Offset: 0x00045FFA
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00047E04 File Offset: 0x00046004
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationDeclarationReference planOperationDeclarationReference;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationDeclarationReference>(this, other, out flag, out planOperationDeclarationReference))
			{
				return flag;
			}
			return this.DeclarationName == planOperationDeclarationReference.DeclarationName && this.CanExpandToMultiTables == planOperationDeclarationReference.CanExpandToMultiTables;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00047E43 File Offset: 0x00046043
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("DeclarationReference");
			builder.WriteAttribute<string>("Name", this.DeclarationName, false, false);
			builder.WriteAttribute<bool>("CanExpandToMultiTable", this.CanExpandToMultiTables, false, false);
			builder.EndObject();
		}
	}
}

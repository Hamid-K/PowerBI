using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000218 RID: 536
	internal sealed class PlanNamedColumnProjectItem : PlanProjectItem
	{
		// Token: 0x06001290 RID: 4752 RVA: 0x0004918A File Offset: 0x0004738A
		internal PlanNamedColumnProjectItem(string columnName)
		{
			this.ColumnName = columnName;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x00049199 File Offset: 0x00047399
		public string ColumnName { get; }

		// Token: 0x06001292 RID: 4754 RVA: 0x000491A1 File Offset: 0x000473A1
		public override void Accept(IPlanProjectItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000491AA File Offset: 0x000473AA
		protected override int GetHashCodeInternal()
		{
			return this.ColumnName.GetHashCode();
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x000491B8 File Offset: 0x000473B8
		public override bool Equals(PlanProjectItem other)
		{
			PlanNamedColumnProjectItem planNamedColumnProjectItem = other as PlanNamedColumnProjectItem;
			return planNamedColumnProjectItem != null && this.ColumnName == planNamedColumnProjectItem.ColumnName;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000491E2 File Offset: 0x000473E2
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("NamedColumnProjectItem");
			builder.WriteAttribute<string>("Column", this.ColumnName, false, false);
			builder.EndObject();
		}
	}
}

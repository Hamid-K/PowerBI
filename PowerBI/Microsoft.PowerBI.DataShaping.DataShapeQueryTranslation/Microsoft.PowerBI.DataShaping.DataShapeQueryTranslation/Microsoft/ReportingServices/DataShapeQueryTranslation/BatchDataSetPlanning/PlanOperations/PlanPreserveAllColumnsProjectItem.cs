using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200021F RID: 543
	internal sealed class PlanPreserveAllColumnsProjectItem : PlanProjectItem
	{
		// Token: 0x060012BD RID: 4797 RVA: 0x000495E7 File Offset: 0x000477E7
		private PlanPreserveAllColumnsProjectItem()
		{
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x000495EF File Offset: 0x000477EF
		public override void Accept(IPlanProjectItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x000495F8 File Offset: 0x000477F8
		protected override int GetHashCodeInternal()
		{
			return this.GetHashCode();
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00049600 File Offset: 0x00047800
		public override bool Equals(PlanProjectItem other)
		{
			return other is PlanPreserveAllColumnsProjectItem;
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0004960B File Offset: 0x0004780B
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("PreserveAllColumnsProjectItem");
			builder.EndObject();
		}

		// Token: 0x04000855 RID: 2133
		internal static readonly PlanPreserveAllColumnsProjectItem Instance = new PlanPreserveAllColumnsProjectItem();
	}
}

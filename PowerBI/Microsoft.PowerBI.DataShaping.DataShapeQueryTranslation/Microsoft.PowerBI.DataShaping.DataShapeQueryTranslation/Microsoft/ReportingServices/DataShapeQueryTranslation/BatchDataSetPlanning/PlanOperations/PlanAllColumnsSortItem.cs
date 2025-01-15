using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200022C RID: 556
	internal sealed class PlanAllColumnsSortItem : PlanSortItem
	{
		// Token: 0x0600131A RID: 4890 RVA: 0x00049F53 File Offset: 0x00048153
		internal PlanAllColumnsSortItem(SortDirection direction)
		{
			this.Direction = direction;
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x00049F62 File Offset: 0x00048162
		public SortDirection Direction { get; }

		// Token: 0x0600131C RID: 4892 RVA: 0x00049F6A File Offset: 0x0004816A
		public override void Accept(IPlanSortItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00049F74 File Offset: 0x00048174
		protected override int GetHashCodeInternal()
		{
			return this.Direction.GetHashCode();
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00049F98 File Offset: 0x00048198
		public override bool Equals(PlanSortItem other)
		{
			PlanAllColumnsSortItem planAllColumnsSortItem = other as PlanAllColumnsSortItem;
			return planAllColumnsSortItem != null && this.Direction == planAllColumnsSortItem.Direction;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00049FBF File Offset: 0x000481BF
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("PlanAllColumnsSortItem");
			builder.WriteAttribute<SortDirection>("Direction", this.Direction, false, false);
			builder.EndObject();
		}
	}
}

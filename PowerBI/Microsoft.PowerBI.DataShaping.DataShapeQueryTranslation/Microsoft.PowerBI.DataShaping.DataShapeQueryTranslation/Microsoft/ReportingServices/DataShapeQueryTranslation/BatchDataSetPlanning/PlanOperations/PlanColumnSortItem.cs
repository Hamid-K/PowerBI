using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200022B RID: 555
	internal sealed class PlanColumnSortItem : PlanSortItem
	{
		// Token: 0x06001313 RID: 4883 RVA: 0x00049E8B File Offset: 0x0004808B
		internal PlanColumnSortItem(string name, SortDirection direction)
		{
			this.m_name = name;
			this.m_direction = direction;
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x00049EA1 File Offset: 0x000480A1
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x00049EA9 File Offset: 0x000480A9
		public SortDirection Direction
		{
			get
			{
				return this.m_direction;
			}
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00049EB1 File Offset: 0x000480B1
		public override void Accept(IPlanSortItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00049EBA File Offset: 0x000480BA
		protected override int GetHashCodeInternal()
		{
			return Hashing.CombineHash(this.m_name.GetHashCode(), this.m_direction.GetHashCode());
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00049EE0 File Offset: 0x000480E0
		public override bool Equals(PlanSortItem other)
		{
			PlanColumnSortItem planColumnSortItem = other as PlanColumnSortItem;
			return planColumnSortItem != null && this.Name == planColumnSortItem.Name && this.Direction == planColumnSortItem.Direction;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00049F1A File Offset: 0x0004811A
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("ColumnSortItem");
			builder.WriteAttribute<string>("Name", this.m_name, false, false);
			builder.WriteAttribute<SortDirection>("Direction", this.m_direction, false, false);
			builder.EndObject();
		}

		// Token: 0x0400086D RID: 2157
		private readonly string m_name;

		// Token: 0x0400086E RID: 2158
		private readonly SortDirection m_direction;
	}
}

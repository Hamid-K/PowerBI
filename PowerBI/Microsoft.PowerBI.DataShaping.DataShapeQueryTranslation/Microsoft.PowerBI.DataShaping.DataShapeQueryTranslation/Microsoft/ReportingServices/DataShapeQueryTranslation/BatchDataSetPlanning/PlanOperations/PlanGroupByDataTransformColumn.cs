using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200020A RID: 522
	internal sealed class PlanGroupByDataTransformColumn : PlanGroupByItem
	{
		// Token: 0x06001232 RID: 4658 RVA: 0x000489A4 File Offset: 0x00046BA4
		internal PlanGroupByDataTransformColumn(DataTransformTableColumn column)
		{
			this.m_column = column;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x000489B3 File Offset: 0x00046BB3
		public DataTransformTableColumn Column
		{
			get
			{
				return this.m_column;
			}
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x000489BB File Offset: 0x00046BBB
		internal override void Accept(IPlanGroupByItemVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x000489C4 File Offset: 0x00046BC4
		protected override int GetHashCodeInternal()
		{
			return this.m_column.GetHashCode();
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x000489D4 File Offset: 0x00046BD4
		public override bool Equals(PlanGroupByItem other)
		{
			PlanGroupByDataTransformColumn planGroupByDataTransformColumn = other as PlanGroupByDataTransformColumn;
			return planGroupByDataTransformColumn != null && this.Column == planGroupByDataTransformColumn.Column;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000489FB File Offset: 0x00046BFB
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("GroupByDataTransformColumn");
			builder.WriteAttribute<DataTransformTableColumn>("Column", this.m_column, false, false);
			builder.EndObject();
		}

		// Token: 0x04000835 RID: 2101
		private readonly DataTransformTableColumn m_column;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001EE RID: 494
	internal sealed class PlanOperationAddJoinIndex : PlanOperation
	{
		// Token: 0x0600111A RID: 4378 RVA: 0x00046776 File Offset: 0x00044976
		internal PlanOperationAddJoinIndex(PlanOperation table, Calculation indexColumnCalculation, PlanOperation indexTable, IEnumerable<PlanSortItem> indexTableSorts)
		{
			this.Table = table;
			this.IndexColumnCalculation = indexColumnCalculation;
			this.IndexTable = indexTable;
			this.IndexTableSorts = indexTableSorts.ToReadOnlyList<PlanSortItem>();
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x000467A0 File Offset: 0x000449A0
		public PlanOperation Table { get; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x000467A8 File Offset: 0x000449A8
		public Calculation IndexColumnCalculation { get; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x000467B0 File Offset: 0x000449B0
		public PlanOperation IndexTable { get; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x000467B8 File Offset: 0x000449B8
		public IReadOnlyList<PlanSortItem> IndexTableSorts { get; }

		// Token: 0x0600111F RID: 4383 RVA: 0x000467C0 File Offset: 0x000449C0
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x000467CC File Offset: 0x000449CC
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationAddJoinIndex planOperationAddJoinIndex;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationAddJoinIndex>(this, other, out flag, out planOperationAddJoinIndex))
			{
				return flag;
			}
			return this.Table.Equals(planOperationAddJoinIndex.Table) && this.IndexColumnCalculation == planOperationAddJoinIndex.IndexColumnCalculation && this.IndexTable.Equals(planOperationAddJoinIndex.IndexTable) && this.IndexTableSorts.SequenceEqual(planOperationAddJoinIndex.IndexTableSorts);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00046830 File Offset: 0x00044A30
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("PlanOperationAddJoinIndex");
			builder.WriteProperty<Calculation>("IndexColumnCalculation", this.IndexColumnCalculation, false);
			builder.WriteProperty<PlanOperation>("Table", this.Table, false);
			builder.WriteProperty<PlanOperation>("IndexTable", this.IndexTable, false);
			builder.WriteProperty<IReadOnlyList<PlanSortItem>>("IndexTableSorts", this.IndexTableSorts, false);
			builder.EndObject();
		}
	}
}

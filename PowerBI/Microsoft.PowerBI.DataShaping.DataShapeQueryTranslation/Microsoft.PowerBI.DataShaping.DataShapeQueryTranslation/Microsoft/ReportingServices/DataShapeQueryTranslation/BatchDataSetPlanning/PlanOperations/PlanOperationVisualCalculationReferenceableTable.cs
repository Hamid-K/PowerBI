using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000234 RID: 564
	internal sealed class PlanOperationVisualCalculationReferenceableTable : PlanOperation
	{
		// Token: 0x0600134D RID: 4941 RVA: 0x0004A5F2 File Offset: 0x000487F2
		internal PlanOperationVisualCalculationReferenceableTable(PlanOperation table, IReadOnlyList<ColumnWithExplicitName> explicitlyNamedColumns)
		{
			this.Table = table;
			this.ExplicitlyNamedColumns = explicitlyNamedColumns;
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x0004A608 File Offset: 0x00048808
		public PlanOperation Table { get; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x0004A610 File Offset: 0x00048810
		public IReadOnlyList<ColumnWithExplicitName> ExplicitlyNamedColumns { get; }

		// Token: 0x06001350 RID: 4944 RVA: 0x0004A618 File Offset: 0x00048818
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationVisualCalculationReferenceableTable planOperationVisualCalculationReferenceableTable;
			if (!PlanOperation.CheckReferenceAndTypeEquality<PlanOperationVisualCalculationReferenceableTable>(this, other, out flag, out planOperationVisualCalculationReferenceableTable))
			{
				return this.Table.Equals(planOperationVisualCalculationReferenceableTable.Table) && this.ExplicitlyNamedColumns.SequenceEqual(planOperationVisualCalculationReferenceableTable.ExplicitlyNamedColumns);
			}
			return flag;
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0004A65A File Offset: 0x0004885A
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("VisualCalculationReferenceableTable");
			builder.WriteProperty<PlanOperation>("Table", this.Table, false);
			builder.WriteProperty<IReadOnlyList<ColumnWithExplicitName>>("ExplicitlyNamedColumns", this.ExplicitlyNamedColumns, false);
			builder.EndObject();
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0004A691 File Offset: 0x00048891
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200022E RID: 558
	internal sealed class PlanOperationSubstituteWithIndex : PlanOperation
	{
		// Token: 0x06001323 RID: 4899 RVA: 0x00049FE5 File Offset: 0x000481E5
		internal PlanOperationSubstituteWithIndex(PlanOperation table, string indexColumnName, PlanOperation indexTable, IEnumerable<PlanSortItem> indexTableSorts)
		{
			this.Table = table;
			this.IndexColumnName = indexColumnName;
			this.IndexTable = indexTable;
			this.IndexTableSorts = indexTableSorts.ToReadOnlyList<PlanSortItem>();
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0004A00F File Offset: 0x0004820F
		public PlanOperation Table { get; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x0004A017 File Offset: 0x00048217
		public string IndexColumnName { get; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0004A01F File Offset: 0x0004821F
		public PlanOperation IndexTable { get; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x0004A027 File Offset: 0x00048227
		public IReadOnlyList<PlanSortItem> IndexTableSorts { get; }

		// Token: 0x06001328 RID: 4904 RVA: 0x0004A02F File Offset: 0x0004822F
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0004A038 File Offset: 0x00048238
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationSubstituteWithIndex planOperationSubstituteWithIndex;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationSubstituteWithIndex>(this, other, out flag, out planOperationSubstituteWithIndex))
			{
				return flag;
			}
			return this.Table.Equals(planOperationSubstituteWithIndex.Table) && this.IndexColumnName == planOperationSubstituteWithIndex.IndexColumnName && this.IndexTable.Equals(planOperationSubstituteWithIndex.IndexTable) && this.IndexTableSorts.SequenceEqual(planOperationSubstituteWithIndex.IndexTableSorts);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0004A0A0 File Offset: 0x000482A0
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("SubstituteWithIndex");
			builder.WriteAttribute<string>("IndexColumnName", this.IndexColumnName, false, false);
			builder.WriteProperty<PlanOperation>("Table", this.Table, false);
			builder.WriteProperty<PlanOperation>("IndexTable", this.IndexTable, false);
			builder.WriteProperty<IReadOnlyList<PlanSortItem>>("IndexTableSorts", this.IndexTableSorts, false);
			builder.EndObject();
		}
	}
}

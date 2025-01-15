using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000226 RID: 550
	internal sealed class PlanOperationSingleRow : PlanOperation
	{
		// Token: 0x060012EE RID: 4846 RVA: 0x00049AB8 File Offset: 0x00047CB8
		internal PlanOperationSingleRow(IEnumerable<Calculation> calculations, IEnumerable<PlanOperation> contextTables, IEnumerable<ExistsFilterItem> existsFilters, IEnumerable<SingleRowAdditionalColumn> additionalColumns)
		{
			this.m_calculations = calculations.ToReadOnlyCollection<Calculation>();
			this.m_contextTables = contextTables.ToReadOnlyCollection<PlanOperation>();
			this.m_existsFilters = existsFilters.ToReadOnlyCollection<ExistsFilterItem>();
			this.m_additionalColumns = additionalColumns.ToReadOnlyCollection<SingleRowAdditionalColumn>();
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x00049AF1 File Offset: 0x00047CF1
		public ReadOnlyCollection<Calculation> Calculations
		{
			get
			{
				return this.m_calculations;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x00049AF9 File Offset: 0x00047CF9
		internal ReadOnlyCollection<PlanOperation> ContextTables
		{
			get
			{
				return this.m_contextTables;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00049B01 File Offset: 0x00047D01
		internal ReadOnlyCollection<ExistsFilterItem> ExistsFilters
		{
			get
			{
				return this.m_existsFilters;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x00049B09 File Offset: 0x00047D09
		internal ReadOnlyCollection<SingleRowAdditionalColumn> AdditionalColumns
		{
			get
			{
				return this.m_additionalColumns;
			}
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00049B11 File Offset: 0x00047D11
		internal override TResult Accept<TResult>(IPlanOperationVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00049B1C File Offset: 0x00047D1C
		public override bool Equals(PlanOperation other)
		{
			bool flag;
			PlanOperationSingleRow planOperationSingleRow;
			if (PlanOperation.CheckReferenceAndTypeEquality<PlanOperationSingleRow>(this, other, out flag, out planOperationSingleRow))
			{
				return flag;
			}
			return this.Calculations.SequenceEqual(planOperationSingleRow.Calculations) && this.ContextTables.SequenceEqual(planOperationSingleRow.ContextTables) && this.ExistsFilters.SequenceEqual(planOperationSingleRow.ExistsFilters) && this.AdditionalColumns.SequenceEqual(planOperationSingleRow.AdditionalColumns);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00049B84 File Offset: 0x00047D84
		public override void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("SingleRow");
			builder.WriteProperty<ReadOnlyCollection<Calculation>>("Calculations", this.Calculations, false);
			builder.WriteProperty<ReadOnlyCollection<PlanOperation>>("ContextTables", this.ContextTables, false);
			builder.WriteProperty<ReadOnlyCollection<SingleRowAdditionalColumn>>("AdditionalColumns", this.AdditionalColumns, false);
			builder.WriteProperty<ReadOnlyCollection<ExistsFilterItem>>("ExistsFilters", this.ExistsFilters, false);
			builder.EndObject();
		}

		// Token: 0x04000863 RID: 2147
		private readonly ReadOnlyCollection<Calculation> m_calculations;

		// Token: 0x04000864 RID: 2148
		private readonly ReadOnlyCollection<PlanOperation> m_contextTables;

		// Token: 0x04000865 RID: 2149
		private readonly ReadOnlyCollection<ExistsFilterItem> m_existsFilters;

		// Token: 0x04000866 RID: 2150
		private readonly ReadOnlyCollection<SingleRowAdditionalColumn> m_additionalColumns;
	}
}

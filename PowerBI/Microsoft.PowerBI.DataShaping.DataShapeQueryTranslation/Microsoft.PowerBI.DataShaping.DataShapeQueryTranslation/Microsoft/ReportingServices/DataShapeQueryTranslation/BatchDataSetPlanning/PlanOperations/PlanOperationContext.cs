using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001FA RID: 506
	internal sealed class PlanOperationContext : IEquatable<PlanOperationContext>, IStructuredToString
	{
		// Token: 0x060011AC RID: 4524 RVA: 0x000478F3 File Offset: 0x00045AF3
		internal PlanOperationContext(PlanOperation table, IReadOnlyList<IScope> rowScopes, IReadOnlyList<Calculation> calculations, IReadOnlyList<DataMember> showAll, PlanOperationFilteringMetadata filteringMetadata)
			: this(table, new RowScopesMetadata(rowScopes), calculations, showAll, filteringMetadata)
		{
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00047907 File Offset: 0x00045B07
		internal PlanOperationContext(PlanOperation table, RowScopesMetadata rowScopes, IReadOnlyList<Calculation> calculations, IReadOnlyList<DataMember> showAll, PlanOperationFilteringMetadata filteringMetadata)
		{
			this.Table = table;
			this.RowScopes = rowScopes;
			this.Calculations = calculations;
			this.ShowAll = showAll;
			this.FilteringMetadata = filteringMetadata;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00047934 File Offset: 0x00045B34
		internal PlanOperationContext(PlanOperation table, IReadOnlyList<IScope> rowScopes, IReadOnlyList<Calculation> calculations = null)
			: this(table, new RowScopesMetadata(rowScopes), calculations)
		{
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00047944 File Offset: 0x00045B44
		internal PlanOperationContext(PlanOperation table, RowScopesMetadata rowScopes, IReadOnlyList<Calculation> calculations = null)
		{
			this.Table = table;
			this.RowScopes = rowScopes;
			this.Calculations = calculations ?? Util.EmptyReadOnlyList<Calculation>();
			this.ShowAll = Util.EmptyReadOnlyList<DataMember>();
			this.FilteringMetadata = new PlanOperationFilteringMetadata(Util.EmptyReadOnlyList<SubtotalColumnFilteringMetadata>(), false);
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x00047991 File Offset: 0x00045B91
		internal PlanOperation Table { get; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x00047999 File Offset: 0x00045B99
		internal RowScopesMetadata RowScopes { get; }

		// Token: 0x060011B2 RID: 4530 RVA: 0x000479A1 File Offset: 0x00045BA1
		internal IReadOnlyList<DataMember> GetAllGroups()
		{
			return this.RowScopes.GetAllGroups();
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x000479AE File Offset: 0x00045BAE
		internal IReadOnlyList<Calculation> Calculations { get; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x000479B8 File Offset: 0x00045BB8
		internal IReadOnlyList<DataMember> Totals
		{
			get
			{
				if (this.m_totals == null)
				{
					this.m_totals = this.FilteringMetadata.TotalsMetadata.Select((SubtotalColumnFilteringMetadata tm) => tm.Member).ToReadOnlyList<DataMember>();
				}
				return this.m_totals;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x00047A0D File Offset: 0x00045C0D
		internal IReadOnlyList<DataMember> ShowAll { get; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x00047A15 File Offset: 0x00045C15
		internal IReadOnlyList<SubtotalColumnFilteringMetadata> TotalsMetadata
		{
			get
			{
				return this.FilteringMetadata.TotalsMetadata;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00047A22 File Offset: 0x00045C22
		internal bool RespectsInstanceFilters
		{
			get
			{
				return this.FilteringMetadata.RespectsInstanceFilters;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00047A2F File Offset: 0x00045C2F
		internal PlanOperationFilteringMetadata FilteringMetadata { get; }

		// Token: 0x060011B9 RID: 4537 RVA: 0x00047A37 File Offset: 0x00045C37
		internal PlanOperationContext ReplaceTable(PlanOperation newTable, IReadOnlyList<IScope> rowScopes, PlanOperationFilteringMetadata filteringMetadata = null, IReadOnlyList<Calculation> calculations = null)
		{
			return this.ReplaceTable(newTable, new RowScopesMetadata(rowScopes), filteringMetadata, calculations);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00047A49 File Offset: 0x00045C49
		internal PlanOperationContext ReplaceTable(PlanOperation newTable, RowScopesMetadata rowScopes = null, PlanOperationFilteringMetadata filteringMetadata = null, IReadOnlyList<Calculation> calculations = null)
		{
			if (this.Table == newTable)
			{
				return this;
			}
			return new PlanOperationContext(newTable, rowScopes ?? this.RowScopes, calculations ?? this.Calculations, this.ShowAll, filteringMetadata ?? this.FilteringMetadata);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00047A84 File Offset: 0x00045C84
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlanOperationContext);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00047A92 File Offset: 0x00045C92
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00047AA4 File Offset: 0x00045CA4
		public bool Equals(PlanOperationContext other)
		{
			return other != null && this.Table.Equals(other.Table) && this.RowScopes.Equals(other.RowScopes) && this.Calculations.SetEquals(other.Calculations) && this.ShowAll.SetEquals(other.ShowAll) && this.FilteringMetadata.Equals(other.FilteringMetadata);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00047B14 File Offset: 0x00045D14
		public override string ToString()
		{
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(ExpressionStringBuilderFactory.Create(null, false), 0, false);
			this.WriteTo(structuredStringBuilder);
			return structuredStringBuilder.ToString();
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00047B40 File Offset: 0x00045D40
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("PlanOperationContext");
			builder.WriteProperty<IReadOnlyList<IScope>>("RowScopes", this.RowScopes.Scopes, false);
			builder.WriteProperty<IReadOnlyList<Calculation>>("Calculations", this.Calculations, false);
			builder.WriteProperty<IReadOnlyList<DataMember>>("ShowAll", this.ShowAll, false);
			builder.WriteProperty<PlanOperation>("Table", this.Table, false);
			builder.WriteProperty<PlanOperationFilteringMetadata>("FilteringMetadata", this.FilteringMetadata, false);
			builder.EndObject();
		}

		// Token: 0x04000808 RID: 2056
		private IReadOnlyList<DataMember> m_totals;
	}
}

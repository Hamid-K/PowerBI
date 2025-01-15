using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x020001AC RID: 428
	internal sealed class TableReference : IEquatable<TableReference>
	{
		// Token: 0x06000F0B RID: 3851 RVA: 0x0003D550 File Offset: 0x0003B750
		internal TableReference(PlanOperationContext operation, string tableName, PlanDeclarationCollection declarations, RowResultSetType rowResultSetType = RowResultSetType.Unrestricted)
		{
			this.m_operation = operation;
			this.m_tableName = tableName;
			this.m_declarations = declarations;
			this.m_rowResultSetType = rowResultSetType;
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0003D575 File Offset: 0x0003B775
		internal RowScopesMetadata RowScopes
		{
			get
			{
				return this.m_operation.RowScopes;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x0003D582 File Offset: 0x0003B782
		internal bool RespectsInstanceFilters
		{
			get
			{
				return this.m_operation.RespectsInstanceFilters;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0003D58F File Offset: 0x0003B78F
		internal RowResultSetType RowResultSetType
		{
			get
			{
				return this.m_rowResultSetType;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x0003D597 File Offset: 0x0003B797
		internal string TableName
		{
			get
			{
				return this.m_tableName;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x0003D59F File Offset: 0x0003B79F
		internal IReadOnlyList<Calculation> Calculations
		{
			get
			{
				return this.m_operation.Calculations;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0003D5AC File Offset: 0x0003B7AC
		internal IReadOnlyList<DataMember> ShowAll
		{
			get
			{
				return this.m_operation.ShowAll;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0003D5B9 File Offset: 0x0003B7B9
		internal IReadOnlyList<SubtotalColumnFilteringMetadata> TotalsMetadata
		{
			get
			{
				return this.m_operation.TotalsMetadata;
			}
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0003D5C6 File Offset: 0x0003B7C6
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TableReference);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0003D5D4 File Offset: 0x0003B7D4
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0003D5E8 File Offset: 0x0003B7E8
		public bool Equals(TableReference other)
		{
			return other != null && this.RowScopes.Equals(other.RowScopes) && this.Calculations.SetEquals(other.Calculations) && this.ShowAll.SetEquals(other.ShowAll) && this.TotalsMetadata.SetEquals(other.TotalsMetadata) && this.RowResultSetType == other.RowResultSetType;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0003D654 File Offset: 0x0003B854
		internal PlanOperationContext Dereference()
		{
			if (!this.m_isReferenced)
			{
				this.m_operation = this.m_operation.DeclareIfNotDeclared(this.m_tableName, this.m_declarations, false, null, false);
				this.m_isReferenced = true;
			}
			return this.m_operation;
		}

		// Token: 0x0400071C RID: 1820
		private readonly string m_tableName;

		// Token: 0x0400071D RID: 1821
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x0400071E RID: 1822
		private PlanOperationContext m_operation;

		// Token: 0x0400071F RID: 1823
		private bool m_isReferenced;

		// Token: 0x04000720 RID: 1824
		private RowResultSetType m_rowResultSetType;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers
{
	// Token: 0x020001AF RID: 431
	internal sealed class AggregateReferenceTable : IAggregateInputTable
	{
		// Token: 0x06000F31 RID: 3889 RVA: 0x0003DE7E File Offset: 0x0003C07E
		internal AggregateReferenceTable(TableReference table)
		{
			this.m_table = table;
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x0003DE8D File Offset: 0x0003C08D
		public PlanOperationContext OperationContext
		{
			get
			{
				return this.m_table.Dereference();
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x0003DE9A File Offset: 0x0003C09A
		public IScope OutputRowScope
		{
			get
			{
				return this.m_table.RowScopes.InnermostScope;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0003DEAC File Offset: 0x0003C0AC
		public RowScopesMetadata RowScopes
		{
			get
			{
				return this.m_table.RowScopes;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x0003DEB9 File Offset: 0x0003C0B9
		public bool RespectsInstanceFilters
		{
			get
			{
				return this.m_table.RespectsInstanceFilters;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0003DEC6 File Offset: 0x0003C0C6
		public RowResultSetType RowResultSetType
		{
			get
			{
				return this.m_table.RowResultSetType;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0003DED3 File Offset: 0x0003C0D3
		public string TableName
		{
			get
			{
				return this.m_table.TableName;
			}
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0003DEE0 File Offset: 0x0003C0E0
		public bool ContainsCalculation(Calculation calc)
		{
			return this.m_table.Calculations.Contains(calc);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0003DEF3 File Offset: 0x0003C0F3
		public bool HasRequiredShowAll(IReadOnlyList<DataMember> requiredState)
		{
			return (requiredState.Count == 0 && this.m_table.ShowAll.Count == 0) || this.m_table.ShowAll.IsSupersetOf(requiredState);
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0003DF22 File Offset: 0x0003C122
		public IReadOnlyList<SubtotalColumnFilteringMetadata> TotalsMetadata
		{
			get
			{
				return this.m_table.TotalsMetadata;
			}
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0003DF2F File Offset: 0x0003C12F
		public PlanOperation ToPlanOperation(DataShapeAnnotations annotations, ScopeTree scopeTree)
		{
			return this.OperationContext.Table;
		}

		// Token: 0x04000729 RID: 1833
		private readonly TableReference m_table;
	}
}

using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000066 RID: 102
	internal sealed class SqlQueryOnSelfPathItem : SqlQueryOnEntityBase
	{
		// Token: 0x060004AC RID: 1196 RVA: 0x000147D2 File Offset: 0x000129D2
		internal SqlQueryOnSelfPathItem(QueryPlanBuilder qpBuilder, NestedQueryKey key)
			: base(qpBuilder, key)
		{
			if (base.Key == null)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			if (!(base.Key.FilteredPathItem.ExpressionPathItem is SelfPathItem))
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			this.PrepareJoinColumns();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00014810 File Offset: 0x00012A10
		internal override SqlTableSource Join(SqlSelectQuery parentQuery, SqlSelectQuery nestedQuery)
		{
			this.SelectJoinKeys(nestedQuery);
			bool flag = base.Key.PathItemOptionality == Optionality.Optional;
			return parentQuery.Join(this.m_joinColumns, nestedQuery, this.m_joinColumns, flag);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00014847 File Offset: 0x00012A47
		internal override void SelectJoinKeys(SqlSelectQuery selectQuery)
		{
			selectQuery.SelectJoinColumns(this.m_joinColumns);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00014858 File Offset: 0x00012A58
		private void PrepareJoinColumns()
		{
			if (this.Entity.ModelEntity != null && this.Entity.ModelEntity.Binding != null)
			{
				this.m_joinColumns = QueryPlanBuilder.GetPrimaryKeyColumns(this.Entity.ModelEntity.Binding);
				return;
			}
			throw new NotImplementedException("Caluclated entites are not supported in SQL 2005.");
		}

		// Token: 0x040001F8 RID: 504
		private IList<DsvColumn> m_joinColumns;
	}
}

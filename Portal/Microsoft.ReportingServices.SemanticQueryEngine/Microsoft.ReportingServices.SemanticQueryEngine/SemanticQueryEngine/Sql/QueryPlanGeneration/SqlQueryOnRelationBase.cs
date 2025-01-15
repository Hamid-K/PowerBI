using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000064 RID: 100
	internal abstract class SqlQueryOnRelationBase : SqlQueryOnEntityBase
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x000144BC File Offset: 0x000126BC
		internal SqlQueryOnRelationBase(QueryPlanBuilder qpBuilder, NestedQueryKey key)
			: base(qpBuilder, key)
		{
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000144C8 File Offset: 0x000126C8
		internal override SqlTableSource Join(SqlSelectQuery parentQuery, SqlSelectQuery nestedQuery)
		{
			if (this.m_parentJoinColumns == null || this.m_nestedJoinColumns == null)
			{
				throw SQEAssert.AssertFalseAndThrow("Join columns are not prepared. InitializeJoinColumns must be called.", Array.Empty<object>());
			}
			this.SelectJoinKeys(nestedQuery);
			bool flag = base.Key.PathItemOptionality == Optionality.Optional;
			return parentQuery.Join(this.m_parentJoinColumns, nestedQuery, this.m_nestedJoinColumns, flag);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001451F File Offset: 0x0001271F
		internal override void SelectJoinKeys(SqlSelectQuery selectQuery)
		{
			if (this.m_nestedJoinColumns == null)
			{
				throw SQEAssert.AssertFalseAndThrow("Join columns are not prepared. InitializeJoinColumns must be called.", Array.Empty<object>());
			}
			selectQuery.SelectJoinColumns(this.m_nestedJoinColumns);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00014545 File Offset: 0x00012745
		protected void InitializeJoinColumns(DsvRelation dsvRelation, RelationEnd relationEnd)
		{
			if (dsvRelation == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("binding"));
			}
			if (relationEnd == RelationEnd.Source)
			{
				this.InitializeJoinColumns(dsvRelation.TargetColumns, dsvRelation.SourceColumns);
				return;
			}
			this.InitializeJoinColumns(dsvRelation.SourceColumns, dsvRelation.TargetColumns);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00014584 File Offset: 0x00012784
		protected void InitializeJoinColumns(IList<DsvColumn> parentJoinColumns, IList<DsvColumn> nestedJoinColumns)
		{
			if (parentJoinColumns == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("parentJoinColumns"));
			}
			if (nestedJoinColumns == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("nestedJoinColumns"));
			}
			if (parentJoinColumns.Count != nestedJoinColumns.Count)
			{
				throw SQEAssert.AssertFalseAndThrow(new InvalidOperationException("parentJoinColumns.Count != nestedJoinColumns.Count"));
			}
			this.m_parentJoinColumns = parentJoinColumns;
			this.m_nestedJoinColumns = nestedJoinColumns;
		}

		// Token: 0x040001F6 RID: 502
		private IList<DsvColumn> m_parentJoinColumns;

		// Token: 0x040001F7 RID: 503
		private IList<DsvColumn> m_nestedJoinColumns;
	}
}

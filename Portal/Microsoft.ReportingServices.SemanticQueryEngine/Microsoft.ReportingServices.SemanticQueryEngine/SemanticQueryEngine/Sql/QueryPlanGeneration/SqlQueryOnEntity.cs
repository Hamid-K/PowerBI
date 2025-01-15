using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000061 RID: 97
	internal class SqlQueryOnEntity : SqlQueryOnEntityBase
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x00014082 File Offset: 0x00012282
		internal SqlQueryOnEntity(QueryPlanBuilder qpBuilder, NestedQueryKey key, IQueryEntity entity)
			: base(qpBuilder, key)
		{
			this.m_entity = entity;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00014094 File Offset: 0x00012294
		internal override SqlTableSource Join(SqlSelectQuery parentQuery, SqlSelectQuery nestedQuery)
		{
			this.SelectJoinKeys(nestedQuery);
			bool flag = base.Key == null || base.Key.PathItemOptionality == Optionality.Optional;
			return parentQuery.Join(nestedQuery, base.KeyExpressions, flag);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000140D0 File Offset: 0x000122D0
		internal override void SelectJoinKeys(SqlSelectQuery selectQuery)
		{
			IQPExpressionInfoCollection keyExpressions = base.KeyExpressions;
			for (int i = 0; i < keyExpressions.Count; i++)
			{
				if (((ISelectList)selectQuery).GetSelectExpression(keyExpressions[i].Expression) == null)
				{
					throw SQEAssert.AssertFalseAndThrow("Key expression is not in the select list.", Array.Empty<object>());
				}
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0001411B File Offset: 0x0001231B
		internal override IQueryEntity Entity
		{
			[DebuggerStepThrough]
			get
			{
				if (base.Entity != null && base.Entity != this.m_entity)
				{
					throw SQEAssert.AssertFalseAndThrow("Query entity mismatch.", Array.Empty<object>());
				}
				return this.m_entity;
			}
		}

		// Token: 0x040001F3 RID: 499
		private readonly IQueryEntity m_entity;
	}
}

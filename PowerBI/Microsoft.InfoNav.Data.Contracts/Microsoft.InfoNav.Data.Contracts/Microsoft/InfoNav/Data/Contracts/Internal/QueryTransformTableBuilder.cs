using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.SemanticQuery.ExpressionBuilder;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002E3 RID: 739
	public sealed class QueryTransformTableBuilder<TParent> : BaseBuilder<QueryTransformTable, TParent>
	{
		// Token: 0x060018A1 RID: 6305 RVA: 0x0002C17C File Offset: 0x0002A37C
		public QueryTransformTableBuilder(string name, TParent parent)
			: base(parent)
		{
			this._transformTable = new QueryTransformTable();
			this._transformTable.Name = name;
			this._transformTable.Columns = new List<QueryTransformTableColumn>();
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0002C1AC File Offset: 0x0002A3AC
		public QueryTransformTableBuilder(QueryTransformTable queryTransformTable, TParent parent)
			: base(parent)
		{
			this._transformTable = queryTransformTable;
			if (this._transformTable.Columns.IsNullOrEmptyCollection<QueryTransformTableColumn>())
			{
				this._transformTable.Columns = new List<QueryTransformTableColumn>();
			}
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x0002C1DE File Offset: 0x0002A3DE
		public QueryTransformTableBuilder<TParent> WithColumn(string inputTable, string columnName, string containerName, string role = null)
		{
			return this.WithColumn(inputTable.SourceRef().Column(columnName).Container(containerName), role, null);
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x0002C1FC File Offset: 0x0002A3FC
		public QueryTransformTableBuilder<TParent> WithColumn(QueryExpressionContainer expression, string role = null, string name = null)
		{
			if (name != null)
			{
				expression.Name = name;
			}
			QueryTransformTableColumn queryTransformTableColumn = new QueryTransformTableColumn
			{
				Role = role,
				Expression = expression
			};
			this._transformTable.Columns.Add(queryTransformTableColumn);
			return this;
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0002C23C File Offset: 0x0002A43C
		public QueryTransformTableBuilder<TParent> WithAggregateColumn(string inputTable, string columnName, string containerName, string role, QueryAggregateFunction queryAggregateFunction = QueryAggregateFunction.Max)
		{
			QueryTransformTableColumn queryTransformTableColumn = new QueryTransformTableColumn
			{
				Role = role,
				Expression = inputTable.SourceRef().Column(columnName).Aggregate(queryAggregateFunction)
					.Container(containerName)
			};
			this._transformTable.Columns.Add(queryTransformTableColumn);
			return this;
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0002C288 File Offset: 0x0002A488
		public QueryTransformTableBuilder<TParent> WithTransformTableRefColumn(string inputTable, string columnName, string containerName, string role)
		{
			QueryTransformTableColumn queryTransformTableColumn = new QueryTransformTableColumn
			{
				Role = role,
				Expression = inputTable.TransformTableRef().Column(columnName).Container(containerName)
			};
			this._transformTable.Columns.Add(queryTransformTableColumn);
			return this;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0002C2D0 File Offset: 0x0002A4D0
		public QueryTransformTableBuilder<TParent> WithOutputRoleRefColumn(string outputRole, string containerName, string role)
		{
			QueryTransformTableColumn queryTransformTableColumn = new QueryTransformTableColumn
			{
				Role = role,
				Expression = outputRole.TransformOutputRoleRef().Container(containerName)
			};
			this._transformTable.Columns.Add(queryTransformTableColumn);
			return this;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x0002C30E File Offset: 0x0002A50E
		public override QueryTransformTable Build()
		{
			return this._transformTable;
		}

		// Token: 0x040008AF RID: 2223
		private readonly QueryTransformTable _transformTable;
	}
}

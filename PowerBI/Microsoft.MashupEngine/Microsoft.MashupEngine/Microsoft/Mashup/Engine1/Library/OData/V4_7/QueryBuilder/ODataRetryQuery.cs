using System;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007EE RID: 2030
	internal sealed class ODataRetryQuery : RetryQuery
	{
		// Token: 0x06003ACE RID: 15054 RVA: 0x000BEF63 File Offset: 0x000BD163
		private ODataRetryQuery(ODataRetryQuery.FallibleOperationKind retryKind, ODataQuery folded, Query fallback)
			: base(folded, fallback)
		{
			this.retryKind = retryKind;
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x000BEF74 File Offset: 0x000BD174
		public static Query New(ODataRetryQuery.FallibleOperationKind retryKind, ODataQuery folded, Query fallback)
		{
			Uri uri;
			if (!folded.TryGetUri(out uri))
			{
				return fallback;
			}
			ODataRetryQuery odataRetryQuery = fallback as ODataRetryQuery;
			if (odataRetryQuery != null && folded.Equals(odataRetryQuery.OptimizedQuery))
			{
				retryKind = ((odataRetryQuery.RetryKind < retryKind) ? odataRetryQuery.RetryKind : retryKind);
				fallback = odataRetryQuery.Query;
			}
			else if (folded.Equals(fallback))
			{
				return folded;
			}
			return new ODataRetryQuery(retryKind, folded, fallback);
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06003AD0 RID: 15056 RVA: 0x000BEFD6 File Offset: 0x000BD1D6
		public ODataRetryQuery.FallibleOperationKind RetryKind
		{
			get
			{
				return this.retryKind;
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x000BEFDE File Offset: 0x000BD1DE
		public new ODataQuery OptimizedQuery
		{
			get
			{
				return (ODataQuery)base.OptimizedQuery;
			}
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x000BEFEC File Offset: 0x000BD1EC
		public override Query SelectRows(FunctionValue function)
		{
			return this.Apply((Query q) => q.SelectRows(function), (Query q) => SelectRowsQuery.New(function, q));
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x000BF024 File Offset: 0x000BD224
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			return this.Apply((Query q) => q.SelectColumns(columnSelection), (Query q) => SelectColumnsQuery.New(columnSelection, q));
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x000BF05C File Offset: 0x000BD25C
		public override Query RenameReorderColumns(ColumnSelection columnSelection)
		{
			return this.Apply((Query q) => q.RenameReorderColumns(columnSelection), (Query q) => RenameReorderColumnsQuery.New(columnSelection, q));
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x000BF094 File Offset: 0x000BD294
		public override Query AddColumns(ColumnsConstructor columnGenerator)
		{
			return this.Apply((Query q) => q.AddColumns(columnGenerator), (Query q) => new AddColumnsQuery(columnGenerator, q));
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x000BF0CC File Offset: 0x000BD2CC
		public override Query NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
		{
			return this.Apply((Query q) => q.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers), (Query q) => new NestedJoinQuery(q, leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers, null));
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x000BF134 File Offset: 0x000BD334
		public override Query Skip(RowCount count)
		{
			return this.Apply((Query q) => q.Skip(count), (Query q) => SkipTakeQuery.New(RowRange.All.Skip(count), q, false));
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x000BF16C File Offset: 0x000BD36C
		public override Query Take(RowCount count)
		{
			return this.Apply((Query q) => q.Take(count), (Query q) => SkipTakeQuery.New(RowRange.All.Take(count), q, false));
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x000BF1A4 File Offset: 0x000BD3A4
		public override Query Sort(TableSortOrder sortOrder)
		{
			return this.Apply((Query q) => q.Sort(sortOrder), (Query q) => SortQuery.New(sortOrder, RowCount.Infinite, q));
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x000BF1DC File Offset: 0x000BD3DC
		public override bool TryJoinAsLeft(RowCount take, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
		{
			query = null;
			return false;
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x000BF1DC File Offset: 0x000BD3DC
		public override bool TryJoinAsRight(RowCount take, Query leftQuery, int[] leftKeyColumns, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
		{
			query = null;
			return false;
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x000BF1E4 File Offset: 0x000BD3E4
		public override Query Group(Grouping grouping)
		{
			return this.Apply((Query q) => q.Group(grouping), (Query q) => new GroupQuery(grouping, q, false));
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x000BF21C File Offset: 0x000BD41C
		public override Query Distinct(TableDistinct distinctCriteria)
		{
			return this.Apply((Query q) => q.Distinct(distinctCriteria), (Query q) => DistinctQuery.New(distinctCriteria, q, false));
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x000912D6 File Offset: 0x0008F4D6
		public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
		{
			query = null;
			return false;
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x000BF254 File Offset: 0x000BD454
		public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
		{
			query = null;
			return false;
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x000BF25C File Offset: 0x000BD45C
		private Query Apply(Func<Query, Query> optimizeOperation, Func<Query, Query> fallbackOperation)
		{
			try
			{
				Query query = optimizeOperation(this.OptimizedQuery);
				ODataQuery odataQuery = query as ODataQuery;
				if (odataQuery != null)
				{
					return ODataRetryQuery.New(this.RetryKind, odataQuery, optimizeOperation(base.Query));
				}
				ODataRetryQuery odataRetryQuery = query as ODataRetryQuery;
				if (odataRetryQuery != null)
				{
					if (this.RetryKind < odataRetryQuery.RetryKind)
					{
						return ODataRetryQuery.New(odataRetryQuery.RetryKind, odataRetryQuery.OptimizedQuery, fallbackOperation(this));
					}
					if (this.RetryKind == odataRetryQuery.RetryKind)
					{
						return ODataRetryQuery.New(this.RetryKind, odataRetryQuery.OptimizedQuery, fallbackOperation(base.Query));
					}
					return ODataRetryQuery.New(this.RetryKind, odataRetryQuery.OptimizedQuery, optimizeOperation(base.Query));
				}
			}
			catch (NotSupportedException)
			{
			}
			return fallbackOperation(this);
		}

		// Token: 0x04001E84 RID: 7812
		private readonly ODataRetryQuery.FallibleOperationKind retryKind;

		// Token: 0x020007EF RID: 2031
		public enum FallibleOperationKind
		{
			// Token: 0x04001E86 RID: 7814
			Top,
			// Token: 0x04001E87 RID: 7815
			ExpandColumns,
			// Token: 0x04001E88 RID: 7816
			Filter,
			// Token: 0x04001E89 RID: 7817
			SelectColumns,
			// Token: 0x04001E8A RID: 7818
			AnyNode,
			// Token: 0x04001E8B RID: 7819
			AllOperations
		}
	}
}

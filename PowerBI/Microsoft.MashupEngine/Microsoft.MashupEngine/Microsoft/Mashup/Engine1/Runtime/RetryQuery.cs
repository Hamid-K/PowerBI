using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015F6 RID: 5622
	internal class RetryQuery : DataSourceQuery
	{
		// Token: 0x06008D6F RID: 36207 RVA: 0x001D991F File Offset: 0x001D7B1F
		public RetryQuery(Query optimizedQuery, Query query)
		{
			this.optimizedQuery = optimizedQuery;
			this.query = query;
		}

		// Token: 0x1700252A RID: 9514
		// (get) Token: 0x06008D70 RID: 36208 RVA: 0x001D9935 File Offset: 0x001D7B35
		public Query Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x1700252B RID: 9515
		// (get) Token: 0x06008D71 RID: 36209 RVA: 0x001D993D File Offset: 0x001D7B3D
		public Query OptimizedQuery
		{
			get
			{
				return this.optimizedQuery;
			}
		}

		// Token: 0x1700252C RID: 9516
		// (get) Token: 0x06008D72 RID: 36210 RVA: 0x001D9945 File Offset: 0x001D7B45
		public override Keys Columns
		{
			get
			{
				return this.query.Columns;
			}
		}

		// Token: 0x06008D73 RID: 36211 RVA: 0x001D9952 File Offset: 0x001D7B52
		public override TypeValue GetColumnType(int column)
		{
			return this.query.GetColumnType(column);
		}

		// Token: 0x1700252D RID: 9517
		// (get) Token: 0x06008D74 RID: 36212 RVA: 0x001D9960 File Offset: 0x001D7B60
		public override IList<TableKey> TableKeys
		{
			get
			{
				return this.query.TableKeys;
			}
		}

		// Token: 0x1700252E RID: 9518
		// (get) Token: 0x06008D75 RID: 36213 RVA: 0x001D996D File Offset: 0x001D7B6D
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return this.query.ComputedColumns;
			}
		}

		// Token: 0x1700252F RID: 9519
		// (get) Token: 0x06008D76 RID: 36214 RVA: 0x001D997A File Offset: 0x001D7B7A
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.query.SortOrder;
			}
		}

		// Token: 0x17002530 RID: 9520
		// (get) Token: 0x06008D77 RID: 36215 RVA: 0x001D9987 File Offset: 0x001D7B87
		public override IEngineHost EngineHost
		{
			get
			{
				return this.query.GetEngineHost();
			}
		}

		// Token: 0x17002531 RID: 9521
		// (get) Token: 0x06008D78 RID: 36216 RVA: 0x001D9994 File Offset: 0x001D7B94
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.query.QueryDomain;
			}
		}

		// Token: 0x06008D79 RID: 36217 RVA: 0x001D99A4 File Offset: 0x001D7BA4
		public override IEnumerable<IValueReference> GetRows()
		{
			IEnumerable<IValueReference> enumerable;
			if (!RetryQuery.TryExecute<IEnumerable<IValueReference>>(() => this.optimizedQuery.GetRows(), out enumerable))
			{
				return this.query.GetRows();
			}
			return new RetryQuery.RetryEnumerable(enumerable, this.query.GetRows());
		}

		// Token: 0x06008D7A RID: 36218 RVA: 0x001D99E4 File Offset: 0x001D7BE4
		private Query Apply(Func<Query, Query> apply, Func<Query, Query> create)
		{
			Query query;
			try
			{
				query = apply(this.optimizedQuery);
			}
			catch (FoldingFailureException)
			{
				return create(this);
			}
			return new RetryQuery(query, apply(this.query));
		}

		// Token: 0x06008D7B RID: 36219 RVA: 0x001D9A30 File Offset: 0x001D7C30
		private bool TryApply(Func<Query, Query> apply, Func<Query, Query> create, out Query query)
		{
			Query query2 = apply(this.optimizedQuery);
			if (query2 == null)
			{
				query = null;
				return false;
			}
			Query query3 = apply(this.query);
			if (query3 == null)
			{
				query3 = create(this.query);
			}
			query = new RetryQuery(query2, query3);
			return true;
		}

		// Token: 0x06008D7C RID: 36220 RVA: 0x001D9A7C File Offset: 0x001D7C7C
		public override Query SelectRows(FunctionValue function)
		{
			return this.Apply((Query q) => q.SelectRows(function), (Query q) => SelectRowsQuery.New(function, q));
		}

		// Token: 0x06008D7D RID: 36221 RVA: 0x001D9AB4 File Offset: 0x001D7CB4
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			return this.Apply((Query q) => q.SelectColumns(columnSelection), (Query q) => SelectColumnsQuery.New(columnSelection, q));
		}

		// Token: 0x06008D7E RID: 36222 RVA: 0x001D9AEC File Offset: 0x001D7CEC
		public override Query RenameReorderColumns(ColumnSelection columnSelection)
		{
			return this.Apply((Query q) => q.RenameReorderColumns(columnSelection), (Query q) => RenameReorderColumnsQuery.New(columnSelection, q));
		}

		// Token: 0x06008D7F RID: 36223 RVA: 0x001D9B24 File Offset: 0x001D7D24
		public override Query AddColumns(ColumnsConstructor columnGenerator)
		{
			return this.Apply((Query q) => q.AddColumns(columnGenerator), (Query q) => new AddColumnsQuery(columnGenerator, q));
		}

		// Token: 0x06008D80 RID: 36224 RVA: 0x001D9B5C File Offset: 0x001D7D5C
		public override Query NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
		{
			return this.Apply((Query q) => q.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers), (Query q) => new NestedJoinQuery(q, leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers, null));
		}

		// Token: 0x06008D81 RID: 36225 RVA: 0x001D9BC4 File Offset: 0x001D7DC4
		public override Query Skip(RowCount count)
		{
			return this.Apply((Query q) => q.Skip(count), (Query q) => SkipTakeQuery.New(RowRange.All.Skip(count), q, false));
		}

		// Token: 0x06008D82 RID: 36226 RVA: 0x001D9BFC File Offset: 0x001D7DFC
		public override Query Take(RowCount count)
		{
			return this.Apply((Query q) => q.Take(count), (Query q) => SkipTakeQuery.New(RowRange.All.Take(count), q, false));
		}

		// Token: 0x06008D83 RID: 36227 RVA: 0x001D9C34 File Offset: 0x001D7E34
		public override Query Sort(TableSortOrder sortOrder)
		{
			return this.Apply((Query q) => q.Sort(sortOrder), (Query q) => SortQuery.New(sortOrder, RowCount.Infinite, q));
		}

		// Token: 0x06008D84 RID: 36228 RVA: 0x001D9C6C File Offset: 0x001D7E6C
		public override Query Unordered()
		{
			return this.Apply((Query q) => q.Unordered(), (Query q) => q.Unordered());
		}

		// Token: 0x06008D85 RID: 36229 RVA: 0x001D9CC0 File Offset: 0x001D7EC0
		public override bool TryJoinAsLeft(RowCount take, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
		{
			return this.TryApply(delegate(Query q)
			{
				Query query2;
				if (q.TryJoinAsLeft(take, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers, out query2))
				{
					return query2;
				}
				return null;
			}, (Query q) => new JoinQuery(take, q, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers), out query);
		}

		// Token: 0x06008D86 RID: 36230 RVA: 0x001D9D38 File Offset: 0x001D7F38
		public override bool TryJoinAsRight(RowCount take, Query leftQuery, int[] leftKeyColumns, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
		{
			return this.TryApply(delegate(Query q)
			{
				Query query2;
				if (q.TryJoinAsRight(take, leftQuery, leftKeyColumns, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers, out query2))
				{
					return query2;
				}
				return null;
			}, (Query q) => new JoinQuery(take, leftQuery, leftKeyColumns, q, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers), out query);
		}

		// Token: 0x06008D87 RID: 36231 RVA: 0x001D9DB0 File Offset: 0x001D7FB0
		public override Query Group(Grouping grouping)
		{
			return this.Apply((Query q) => q.Group(grouping), (Query q) => new GroupQuery(grouping, q, false));
		}

		// Token: 0x06008D88 RID: 36232 RVA: 0x001D9DE8 File Offset: 0x001D7FE8
		public override Query Distinct(TableDistinct distinctCriteria)
		{
			return this.Apply((Query q) => q.Distinct(distinctCriteria), (Query q) => DistinctQuery.New(distinctCriteria, q, false));
		}

		// Token: 0x06008D89 RID: 36233 RVA: 0x001D9E20 File Offset: 0x001D8020
		public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
		{
			return this.TryApply(delegate(Query q)
			{
				Query query2;
				if (q.TryExpandListColumn(columnIndex, singleOrDefault, out query2))
				{
					return query2;
				}
				return null;
			}, (Query q) => new ExpandListColumnQuery(columnIndex, singleOrDefault, null, q), out query);
		}

		// Token: 0x06008D8A RID: 36234 RVA: 0x001D9E60 File Offset: 0x001D8060
		public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
		{
			return this.TryApply(delegate(Query q)
			{
				Query query2;
				if (q.TryExpandRecordColumn(columnToExpand, fieldsToProject, newColumns, out query2))
				{
					return query2;
				}
				return null;
			}, (Query q) => new ExpandRecordColumnQuery(columnToExpand, fieldsToProject, newColumns, null, q), out query);
		}

		// Token: 0x06008D8B RID: 36235 RVA: 0x001D9EA8 File Offset: 0x001D80A8
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			return this.optimizedQuery.TryInvokeAsArgument(function, arguments, index, out result);
		}

		// Token: 0x06008D8C RID: 36236 RVA: 0x001D9EBA File Offset: 0x001D80BA
		public override TableValue DeltaSince(Value tag)
		{
			return this.optimizedQuery.DeltaSince(tag);
		}

		// Token: 0x06008D8D RID: 36237 RVA: 0x001D9EC8 File Offset: 0x001D80C8
		public override Value NativeQuery(TextValue query, Value parameters, Value options)
		{
			return this.optimizedQuery.NativeQuery(query, parameters, options);
		}

		// Token: 0x06008D8E RID: 36238 RVA: 0x001D9ED8 File Offset: 0x001D80D8
		public override ActionValue InsertRows(Query rowsToInsert)
		{
			return this.optimizedQuery.InsertRows(rowsToInsert);
		}

		// Token: 0x06008D8F RID: 36239 RVA: 0x001D9EE6 File Offset: 0x001D80E6
		public override ActionValue UpdateRows(ColumnUpdates columnUpdates)
		{
			return this.optimizedQuery.UpdateRows(columnUpdates);
		}

		// Token: 0x06008D90 RID: 36240 RVA: 0x001D9EF4 File Offset: 0x001D80F4
		public override ActionValue DeleteRows()
		{
			return this.optimizedQuery.DeleteRows();
		}

		// Token: 0x06008D91 RID: 36241 RVA: 0x001D9F01 File Offset: 0x001D8101
		public override ActionValue NativeStatement(TextValue statement, Value parameters, Value options)
		{
			return this.optimizedQuery.NativeStatement(statement, parameters, options);
		}

		// Token: 0x06008D92 RID: 36242 RVA: 0x001D9F11 File Offset: 0x001D8111
		public override bool TryGetReader(out IPageReader reader)
		{
			return this.optimizedQuery.TryGetReader(out reader);
		}

		// Token: 0x06008D93 RID: 36243 RVA: 0x001D9F20 File Offset: 0x001D8120
		public override bool TryGetExpression(out IExpression expression)
		{
			expression = new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(Library._Value.Alternates), new ConstantExpressionSyntaxNode(ListValue.New(new Value[]
			{
				new QueryTableValue.OptimizedQueryTableValue(this.optimizedQuery, null),
				new QueryTableValue.OptimizedQueryTableValue(this.query, null)
			})));
			return true;
		}

		// Token: 0x06008D94 RID: 36244 RVA: 0x001D9F70 File Offset: 0x001D8170
		private static bool TryExecute<TResult>(Func<TResult> func, out TResult result)
		{
			bool flag;
			try
			{
				result = func();
				flag = true;
			}
			catch (Exception ex)
			{
				if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				result = default(TResult);
				flag = false;
			}
			return flag;
		}

		// Token: 0x04004CFE RID: 19710
		private readonly Query query;

		// Token: 0x04004CFF RID: 19711
		private readonly Query optimizedQuery;

		// Token: 0x020015F7 RID: 5623
		private class RetryEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x06008D96 RID: 36246 RVA: 0x001D9FC1 File Offset: 0x001D81C1
			public RetryEnumerable(IEnumerable<IValueReference> optimizedQuery, IEnumerable<IValueReference> query)
			{
				this.optimizedQuery = optimizedQuery;
				this.query = query;
			}

			// Token: 0x06008D97 RID: 36247 RVA: 0x001D9FD7 File Offset: 0x001D81D7
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06008D98 RID: 36248 RVA: 0x001D9FDF File Offset: 0x001D81DF
			public IEnumerator<IValueReference> GetEnumerator()
			{
				return new RetryQuery.RetryEnumerator(this.optimizedQuery, this.query);
			}

			// Token: 0x04004D00 RID: 19712
			private IEnumerable<IValueReference> optimizedQuery;

			// Token: 0x04004D01 RID: 19713
			private IEnumerable<IValueReference> query;
		}

		// Token: 0x020015F8 RID: 5624
		private class RetryEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x17002532 RID: 9522
			// (get) Token: 0x06008D99 RID: 36249 RVA: 0x001D9FF2 File Offset: 0x001D81F2
			public IValueReference Current
			{
				get
				{
					return this.currentEnumerator.Current;
				}
			}

			// Token: 0x17002533 RID: 9523
			// (get) Token: 0x06008D9A RID: 36250 RVA: 0x001D9FF2 File Offset: 0x001D81F2
			object IEnumerator.Current
			{
				get
				{
					return this.currentEnumerator.Current;
				}
			}

			// Token: 0x06008D9B RID: 36251 RVA: 0x001DA000 File Offset: 0x001D8200
			public RetryEnumerator(IEnumerable<IValueReference> optimizedQuery, IEnumerable<IValueReference> query)
			{
				this.query = query;
				this.retry = true;
				if (!RetryQuery.TryExecute<IEnumerator<IValueReference>>(new Func<IEnumerator<IValueReference>>(optimizedQuery.GetEnumerator), out this.currentEnumerator))
				{
					this.currentEnumerator = this.query.GetEnumerator();
					this.retry = false;
				}
			}

			// Token: 0x06008D9C RID: 36252 RVA: 0x001DA053 File Offset: 0x001D8253
			public void Dispose()
			{
				if (this.currentEnumerator != null)
				{
					this.currentEnumerator.Dispose();
					this.currentEnumerator = null;
				}
			}

			// Token: 0x06008D9D RID: 36253 RVA: 0x001DA070 File Offset: 0x001D8270
			public bool MoveNext()
			{
				if (this.retry)
				{
					this.retry = false;
					bool flag;
					if (RetryQuery.TryExecute<bool>(new Func<bool>(this.currentEnumerator.MoveNext), out flag))
					{
						return flag;
					}
					this.currentEnumerator.Dispose();
					this.currentEnumerator = this.query.GetEnumerator();
				}
				return this.currentEnumerator.MoveNext();
			}

			// Token: 0x06008D9E RID: 36254 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x04004D02 RID: 19714
			private readonly IEnumerable<IValueReference> query;

			// Token: 0x04004D03 RID: 19715
			private IEnumerator<IValueReference> currentEnumerator;

			// Token: 0x04004D04 RID: 19716
			private bool retry;
		}
	}
}

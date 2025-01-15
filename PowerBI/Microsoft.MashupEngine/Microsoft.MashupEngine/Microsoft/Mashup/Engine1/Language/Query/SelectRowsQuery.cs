using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200180D RID: 6157
	internal class SelectRowsQuery : Query
	{
		// Token: 0x06009BDC RID: 39900 RVA: 0x00202D2C File Offset: 0x00200F2C
		public static Query New(FunctionValue function, Query innerQuery)
		{
			return SelectRowsQuery.New(function, innerQuery, (FunctionValue f, QueryExpression qe) => new SelectRowsQuery(f, qe, innerQuery));
		}

		// Token: 0x06009BDD RID: 39901 RVA: 0x00202D60 File Offset: 0x00200F60
		public static Query New(FunctionValue function, Query innerQuery, Func<FunctionValue, QueryExpression, Query> selectRowsCtor)
		{
			QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(innerQuery, function);
			bool flag;
			if (!SelectRowsQuery.TryGetConstantCondition(queryExpression, out flag))
			{
				FunctionValue functionValue = QueryExpressionAssembler.Assemble(innerQuery.Columns, queryExpression);
				if (functionValue != null)
				{
					function = functionValue;
				}
				return selectRowsCtor(function, queryExpression);
			}
			if (flag)
			{
				return innerQuery;
			}
			return innerQuery.Take(RowCount.Zero);
		}

		// Token: 0x06009BDE RID: 39902 RVA: 0x00202DAC File Offset: 0x00200FAC
		public static bool TryGetConstantCondition(QueryExpression queryExpression, out bool condition)
		{
			if (queryExpression.Kind == QueryExpressionKind.Constant)
			{
				Value value = ((ConstantQueryExpression)queryExpression).Value;
				if (value.IsLogical)
				{
					condition = value.AsBoolean;
					return true;
				}
				if (value.IsNull)
				{
					condition = false;
					return true;
				}
			}
			condition = false;
			return false;
		}

		// Token: 0x06009BDF RID: 39903 RVA: 0x00202DF1 File Offset: 0x00200FF1
		protected SelectRowsQuery(FunctionValue function, QueryExpression queryExpression, Query innerQuery)
		{
			this.function = function;
			this.queryExpression = queryExpression;
			this.innerQuery = innerQuery;
		}

		// Token: 0x17002828 RID: 10280
		// (get) Token: 0x06009BE0 RID: 39904 RVA: 0x000023C4 File Offset: 0x000005C4
		public override QueryKind Kind
		{
			get
			{
				return QueryKind.SelectRows;
			}
		}

		// Token: 0x17002829 RID: 10281
		// (get) Token: 0x06009BE1 RID: 39905 RVA: 0x00202E0E File Offset: 0x0020100E
		public override Keys Columns
		{
			get
			{
				return this.innerQuery.Columns;
			}
		}

		// Token: 0x06009BE2 RID: 39906 RVA: 0x00202E1B File Offset: 0x0020101B
		public override TypeValue GetColumnType(int column)
		{
			return this.innerQuery.GetColumnType(column);
		}

		// Token: 0x1700282A RID: 10282
		// (get) Token: 0x06009BE3 RID: 39907 RVA: 0x00202E29 File Offset: 0x00201029
		public override IList<TableKey> TableKeys
		{
			get
			{
				return this.innerQuery.TableKeys;
			}
		}

		// Token: 0x1700282B RID: 10283
		// (get) Token: 0x06009BE4 RID: 39908 RVA: 0x00202E36 File Offset: 0x00201036
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return this.innerQuery.ComputedColumns;
			}
		}

		// Token: 0x1700282C RID: 10284
		// (get) Token: 0x06009BE5 RID: 39909 RVA: 0x00202E43 File Offset: 0x00201043
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.innerQuery.SortOrder;
			}
		}

		// Token: 0x1700282D RID: 10285
		// (get) Token: 0x06009BE6 RID: 39910 RVA: 0x00202E50 File Offset: 0x00201050
		public Query InnerQuery
		{
			get
			{
				return this.innerQuery;
			}
		}

		// Token: 0x1700282E RID: 10286
		// (get) Token: 0x06009BE7 RID: 39911 RVA: 0x00202E58 File Offset: 0x00201058
		public FunctionValue Condition
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x1700282F RID: 10287
		// (get) Token: 0x06009BE8 RID: 39912 RVA: 0x00202E60 File Offset: 0x00201060
		public QueryExpression QueryExpression
		{
			get
			{
				return this.queryExpression;
			}
		}

		// Token: 0x06009BE9 RID: 39913 RVA: 0x00202E68 File Offset: 0x00201068
		public override TableValue GetPartitionTable(int[] columns)
		{
			return this.innerQuery.GetPartitionTable(columns);
		}

		// Token: 0x06009BEA RID: 39914 RVA: 0x00202E78 File Offset: 0x00201078
		public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
		{
			QueryExpression queryExpression = this.QueryExpression;
			if (queryExpression != null && queryExpression.AllAccess(ArgumentAccess.Deny, (int column) => column != columnIndex) && this.innerQuery.TryExpandListColumn(columnIndex, singleOrDefault, out query))
			{
				query = query.SelectRows(this.function);
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009BEB RID: 39915 RVA: 0x00202EE0 File Offset: 0x002010E0
		public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
		{
			if (this.QueryExpression != null)
			{
				List<QueryExpression> conjunctiveNF = SelectRowsQuery.GetConjunctiveNF(this.QueryExpression);
				List<QueryExpression> list = null;
				List<QueryExpression> list2 = null;
				int i = conjunctiveNF.Count - 1;
				Func<int, bool> <>9__0;
				Func<int, int> <>9__1;
				while (i >= 0)
				{
					QueryExpression queryExpression = conjunctiveNF[i];
					if (list != null)
					{
						goto IL_00B3;
					}
					QueryExpression queryExpression2 = queryExpression;
					Func<InvocationQueryExpression, bool> deny = ArgumentAccess.Deny;
					Func<int, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (int column) => column != columnToExpand);
					}
					if (!queryExpression2.AllAccess(deny, func))
					{
						goto IL_00B3;
					}
					if (list2 == null)
					{
						list2 = new List<QueryExpression>();
					}
					List<QueryExpression> list3 = list2;
					QueryExpression queryExpression3 = queryExpression;
					Func<int, int> func2;
					if ((func2 = <>9__1) == null)
					{
						func2 = (<>9__1 = delegate(int column)
						{
							if (column >= columnToExpand)
							{
								return column + fieldsToProject.Length - 1;
							}
							return column;
						});
					}
					list3.Add(queryExpression3.AdjustColumnAccess(func2));
					IL_00C4:
					i--;
					continue;
					IL_00B3:
					if (list == null)
					{
						list = new List<QueryExpression>();
					}
					list.Add(queryExpression);
					goto IL_00C4;
				}
				if (list2 != null)
				{
					Query query2 = this.innerQuery;
					if (list != null)
					{
						list.Reverse();
						FunctionValue functionValue = QueryExpressionAssembler.Assemble(query2.Columns, SelectRowsQuery.CreateConjunctiveNF(list));
						query2 = query2.SelectRows(functionValue);
					}
					if (query2.TryExpandRecordColumn(columnToExpand, fieldsToProject, newColumns, out query2))
					{
						list2.Reverse();
						FunctionValue functionValue2 = QueryExpressionAssembler.Assemble(query2.Columns, SelectRowsQuery.CreateConjunctiveNF(list2));
						query = query2.SelectRows(functionValue2);
						return true;
					}
				}
			}
			query = null;
			return false;
		}

		// Token: 0x06009BEC RID: 39916 RVA: 0x00203037 File Offset: 0x00201237
		public override Query Unordered()
		{
			return this.innerQuery.Unordered().SelectRows(this.function);
		}

		// Token: 0x17002830 RID: 10288
		// (get) Token: 0x06009BED RID: 39917 RVA: 0x0020304F File Offset: 0x0020124F
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.innerQuery.QueryDomain;
			}
		}

		// Token: 0x06009BEE RID: 39918 RVA: 0x0020305C File Offset: 0x0020125C
		public override IEnumerable<IValueReference> GetRows()
		{
			return new SelectRowsQuery.SelectRowsEnumerable(this.innerQuery.GetRows(), this.function);
		}

		// Token: 0x06009BEF RID: 39919 RVA: 0x00203074 File Offset: 0x00201274
		public static List<QueryExpression> GetConjunctiveNF(QueryExpression queryExpression)
		{
			List<QueryExpression> list = new List<QueryExpression>();
			Queue<QueryExpression> queue = new Queue<QueryExpression>();
			queue.Enqueue(queryExpression);
			while (queue.Count > 0)
			{
				QueryExpression queryExpression2 = queue.Dequeue();
				if (queryExpression2.Kind == QueryExpressionKind.Binary)
				{
					BinaryQueryExpression binaryQueryExpression = (BinaryQueryExpression)queryExpression2;
					if (binaryQueryExpression.Operator == BinaryOperator2.And)
					{
						queue.Enqueue(binaryQueryExpression.Left);
						queue.Enqueue(binaryQueryExpression.Right);
					}
					else
					{
						list.Add(queryExpression2);
					}
				}
				else
				{
					list.Add(queryExpression2);
				}
			}
			return list;
		}

		// Token: 0x06009BF0 RID: 39920 RVA: 0x002030EC File Offset: 0x002012EC
		public static List<QueryExpression> GetDisjunctiveNF(QueryExpression queryExpression)
		{
			List<QueryExpression> list = new List<QueryExpression>();
			Queue<QueryExpression> queue = new Queue<QueryExpression>();
			queue.Enqueue(queryExpression);
			while (queue.Count > 0)
			{
				QueryExpression queryExpression2 = queue.Dequeue();
				if (queryExpression2.Kind == QueryExpressionKind.Binary)
				{
					BinaryQueryExpression binaryQueryExpression = (BinaryQueryExpression)queryExpression2;
					if (binaryQueryExpression.Operator == BinaryOperator2.Or)
					{
						queue.Enqueue(binaryQueryExpression.Left);
						queue.Enqueue(binaryQueryExpression.Right);
					}
					else
					{
						list.Add(queryExpression2);
					}
				}
				else
				{
					list.Add(queryExpression2);
				}
			}
			return list;
		}

		// Token: 0x06009BF1 RID: 39921 RVA: 0x00203164 File Offset: 0x00201364
		public static void AddNormalForm(List<QueryExpression> list, QueryExpression expression, BinaryOperator2 binaryOperator)
		{
			int num = list.Count;
			list.Add(expression);
			while (list.Count > num)
			{
				while (list[num].Kind == QueryExpressionKind.Binary && ((BinaryQueryExpression)list[num]).Operator == binaryOperator)
				{
					BinaryQueryExpression binaryQueryExpression = (BinaryQueryExpression)list[num];
					list.Insert(num, binaryQueryExpression.Left);
					list[num + 1] = binaryQueryExpression.Right;
				}
				num++;
			}
		}

		// Token: 0x06009BF2 RID: 39922 RVA: 0x002031DC File Offset: 0x002013DC
		public static QueryExpression CreateConjunctiveNF(IList<QueryExpression> predicates)
		{
			QueryExpression queryExpression = predicates[0];
			for (int i = 1; i < predicates.Count; i++)
			{
				queryExpression = new BinaryQueryExpression(BinaryOperator2.And, queryExpression, predicates[i]);
			}
			return queryExpression;
		}

		// Token: 0x06009BF3 RID: 39923 RVA: 0x00203214 File Offset: 0x00201414
		public static QueryExpression CreateDisjunctiveNF(IList<QueryExpression> predicates)
		{
			QueryExpression queryExpression = predicates[0];
			for (int i = 1; i < predicates.Count; i++)
			{
				queryExpression = new BinaryQueryExpression(BinaryOperator2.Or, queryExpression, predicates[i]);
			}
			return queryExpression;
		}

		// Token: 0x06009BF4 RID: 39924 RVA: 0x0020324B File Offset: 0x0020144B
		public static bool TryGetConditions(RecordTypeValue rowType, Keys innerColumns, FunctionValue condition, Func<QueryExpression, bool> canApplyBefore, out FunctionValue beforeCondition, out FunctionValue afterCondition)
		{
			return SelectRowsQuery.TryGetAndAdjustConditions(rowType, innerColumns, condition, canApplyBefore, null, out beforeCondition, out afterCondition);
		}

		// Token: 0x06009BF5 RID: 39925 RVA: 0x0020325B File Offset: 0x0020145B
		public static bool TryGetAndAdjustConditions(RecordTypeValue rowType, Keys innerColumns, FunctionValue condition, Func<QueryExpression, bool> canApplyBefore, Func<QueryExpression, QueryExpression> beforeAdjustment, out FunctionValue beforeCondition, out FunctionValue afterCondition)
		{
			return SelectRowsQuery.TryGetAndAdjustConditions(rowType.FieldKeys, innerColumns, QueryExpressionBuilder.ToQueryExpression(rowType, condition), canApplyBefore, beforeAdjustment, out beforeCondition, out afterCondition);
		}

		// Token: 0x06009BF6 RID: 39926 RVA: 0x00203278 File Offset: 0x00201478
		public static bool TryGetAndAdjustConditions(Keys columns, Keys innerColumns, QueryExpression condition, Func<QueryExpression, bool> canApplyBefore, Func<QueryExpression, QueryExpression> beforeAdjustment, out FunctionValue beforeCondition, out FunctionValue afterCondition)
		{
			beforeCondition = null;
			afterCondition = null;
			List<QueryExpression> conjunctiveNF = SelectRowsQuery.GetConjunctiveNF(condition);
			List<QueryExpression> list = null;
			List<QueryExpression> list2 = null;
			for (int i = 0; i < conjunctiveNF.Count; i++)
			{
				if (list2 == null && canApplyBefore(conjunctiveNF[i]))
				{
					if (list == null)
					{
						list = new List<QueryExpression>();
					}
					QueryExpression queryExpression = conjunctiveNF[i];
					if (beforeAdjustment != null)
					{
						queryExpression = beforeAdjustment(queryExpression);
					}
					list.Add(queryExpression);
				}
				else
				{
					if (list2 == null)
					{
						list2 = new List<QueryExpression>();
					}
					list2.Add(conjunctiveNF[i]);
				}
			}
			if (list != null)
			{
				beforeCondition = QueryExpressionAssembler.Assemble(innerColumns, SelectRowsQuery.CreateConjunctiveNF(list));
			}
			if (list2 != null)
			{
				afterCondition = QueryExpressionAssembler.Assemble(columns, SelectRowsQuery.CreateConjunctiveNF(list2));
			}
			return true;
		}

		// Token: 0x04005232 RID: 21042
		private Query innerQuery;

		// Token: 0x04005233 RID: 21043
		private FunctionValue function;

		// Token: 0x04005234 RID: 21044
		private QueryExpression queryExpression;

		// Token: 0x0200180E RID: 6158
		private class SelectRowsEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x06009BF7 RID: 39927 RVA: 0x00203321 File Offset: 0x00201521
			public SelectRowsEnumerable(IEnumerable<IValueReference> rows, FunctionValue function)
			{
				this.rows = rows;
				this.function = function;
			}

			// Token: 0x06009BF8 RID: 39928 RVA: 0x00203337 File Offset: 0x00201537
			public IEnumerator<IValueReference> GetEnumerator()
			{
				return new SelectRowsQuery.SelectRowsEnumerable.SelectRowsEnumerator(this.rows, this.function);
			}

			// Token: 0x06009BF9 RID: 39929 RVA: 0x0020334A File Offset: 0x0020154A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04005235 RID: 21045
			private IEnumerable<IValueReference> rows;

			// Token: 0x04005236 RID: 21046
			private FunctionValue function;

			// Token: 0x0200180F RID: 6159
			private class SelectRowsEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06009BFA RID: 39930 RVA: 0x00203352 File Offset: 0x00201552
				public SelectRowsEnumerator(IEnumerable<IValueReference> rows, FunctionValue function)
				{
					this.enumerator = rows.GetEnumerator();
					this.function = function;
				}

				// Token: 0x17002831 RID: 10289
				// (get) Token: 0x06009BFB RID: 39931 RVA: 0x0020336D File Offset: 0x0020156D
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06009BFC RID: 39932 RVA: 0x00203375 File Offset: 0x00201575
				public void Dispose()
				{
					this.enumerator.Dispose();
				}

				// Token: 0x06009BFD RID: 39933 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x17002832 RID: 10290
				// (get) Token: 0x06009BFE RID: 39934 RVA: 0x00203382 File Offset: 0x00201582
				public IValueReference Current
				{
					get
					{
						return this.enumerator.Current;
					}
				}

				// Token: 0x06009BFF RID: 39935 RVA: 0x00203390 File Offset: 0x00201590
				public bool MoveNext()
				{
					while (this.enumerator.MoveNext())
					{
						Value value = this.function.Invoke(this.enumerator.Current.Value.AsRecord);
						if (!value.IsNull && value.AsBoolean)
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x04005237 RID: 21047
				private IEnumerator<IValueReference> enumerator;

				// Token: 0x04005238 RID: 21048
				private FunctionValue function;
			}
		}
	}
}

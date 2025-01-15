using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017D1 RID: 6097
	internal static class FilteredQuery
	{
		// Token: 0x06009A28 RID: 39464 RVA: 0x001FE97C File Offset: 0x001FCB7C
		public static Query New<T>(FunctionValue function, T filteredQuery) where T : Query, IFilteredQuery
		{
			return SelectRowsQuery.New(function, filteredQuery, (FunctionValue f, QueryExpression qe) => new FilteredQuery.WithSelectRowsQuery<T>(f, qe, filteredQuery));
		}

		// Token: 0x06009A29 RID: 39465 RVA: 0x001FE9B4 File Offset: 0x001FCBB4
		private static FunctionValue CombineSelectors(RecordTypeValue rowType, FunctionValue left, FunctionValue right)
		{
			List<QueryExpression> list = new List<QueryExpression>();
			FilteredQuery.CombineSelectors(list, SelectRowsQuery.GetConjunctiveNF(QueryExpressionBuilder.ToQueryExpression(rowType, left)));
			FilteredQuery.CombineSelectors(list, SelectRowsQuery.GetConjunctiveNF(QueryExpressionBuilder.ToQueryExpression(rowType, right)));
			return QueryExpressionAssembler.Assemble(rowType.FieldKeys, SelectRowsQuery.CreateConjunctiveNF(list));
		}

		// Token: 0x06009A2A RID: 39466 RVA: 0x001FE9FC File Offset: 0x001FCBFC
		private static void CombineSelectors(List<QueryExpression> combinedExprs, List<QueryExpression> exprsToAdd)
		{
			foreach (QueryExpression queryExpression in exprsToAdd)
			{
				bool flag;
				if (SelectRowsQuery.TryGetConstantCondition(queryExpression, out flag))
				{
					if (!flag)
					{
						combinedExprs.Clear();
						combinedExprs.Add(ConstantQueryExpression.False);
					}
				}
				else
				{
					combinedExprs.Add(queryExpression);
				}
			}
		}

		// Token: 0x020017D2 RID: 6098
		private class WithSelectRowsQuery<T> : SelectRowsQuery where T : Query, IFilteredQuery
		{
			// Token: 0x06009A2B RID: 39467 RVA: 0x001FEA6C File Offset: 0x001FCC6C
			public WithSelectRowsQuery(FunctionValue function, QueryExpression queryExpression, T filteredQuery)
				: base(function, queryExpression, filteredQuery)
			{
				this.filteredQuery = filteredQuery;
			}

			// Token: 0x06009A2C RID: 39468 RVA: 0x001FEA84 File Offset: 0x001FCC84
			public override IEnumerable<IValueReference> GetRows()
			{
				Query query;
				if (this.filteredQuery.TrySelectRows(base.Condition, out query))
				{
					return query.GetRows();
				}
				return base.GetRows();
			}

			// Token: 0x06009A2D RID: 39469 RVA: 0x001FEAB8 File Offset: 0x001FCCB8
			public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
			{
				Query query;
				if (this.filteredQuery.TrySelectRows(base.Condition, out query))
				{
					return query.TryInvokeAsArgument(function, arguments, index, out result);
				}
				return base.TryInvokeAsArgument(function, arguments, index, out result);
			}

			// Token: 0x06009A2E RID: 39470 RVA: 0x001FEAF6 File Offset: 0x001FCCF6
			public override Query SelectRows(FunctionValue condition)
			{
				return FilteredQuery.New<T>(FilteredQuery.CombineSelectors(QueryTableValue.NewRowType(this), base.Condition, condition), this.filteredQuery);
			}

			// Token: 0x06009A2F RID: 39471 RVA: 0x001FEB15 File Offset: 0x001FCD15
			public override ActionValue InsertRows(Query rowsToInsert)
			{
				return this.filteredQuery.InsertRows(rowsToInsert);
			}

			// Token: 0x06009A30 RID: 39472 RVA: 0x001FEB28 File Offset: 0x001FCD28
			public override ActionValue UpdateRows(ColumnUpdates columnUpdates)
			{
				return this.filteredQuery.UpdateRows(columnUpdates, base.Condition);
			}

			// Token: 0x06009A31 RID: 39473 RVA: 0x001FEB41 File Offset: 0x001FCD41
			public override ActionValue DeleteRows()
			{
				return this.filteredQuery.DeleteRows(base.Condition);
			}

			// Token: 0x04005170 RID: 20848
			private readonly T filteredQuery;
		}
	}
}

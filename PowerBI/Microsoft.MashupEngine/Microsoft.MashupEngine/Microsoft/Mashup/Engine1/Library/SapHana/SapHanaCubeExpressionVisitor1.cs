using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200042F RID: 1071
	internal sealed class SapHanaCubeExpressionVisitor1 : SapHanaCubeExpressionVisitor
	{
		// Token: 0x0600247C RID: 9340 RVA: 0x000676F6 File Offset: 0x000658F6
		public SapHanaCubeExpressionVisitor1(SapHanaCubeBase cube, OdbcQueryDomain queryDomain)
			: base(cube, queryDomain)
		{
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x00067700 File Offset: 0x00065900
		protected override Query CompileQuery(QueryCubeExpression expression, IList<ParameterArguments> variableArguments)
		{
			if (variableArguments.Count > 0)
			{
				expression = this.ApplyVariableArguments(expression, variableArguments);
			}
			expression = expression.PropagateDimensionAttributesDownwards();
			expression = expression.PropagateMeasuresDownwards();
			expression = expression.Flatten();
			return this.VisitQuery(expression);
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x00067734 File Offset: 0x00065934
		private QueryCubeExpression ApplyVariableArguments(CubeExpression cubeExpression, IList<ParameterArguments> arguments)
		{
			if (cubeExpression.Kind == CubeExpressionKind.Identifier)
			{
				CubeExpression cubeExpression2 = null;
				foreach (ParameterArguments parameterArguments in arguments)
				{
					CubeExpression cubeExpression3 = SapHanaCubeExpressionVisitor.VariableFilterCompiler.Compile(this.cube.Parameters[parameterArguments.Parameter.Identifier], parameterArguments.Values);
					if (cubeExpression2 == null)
					{
						cubeExpression2 = cubeExpression3;
					}
					else
					{
						cubeExpression2 = new BinaryCubeExpression(BinaryOperator2.And, cubeExpression2, cubeExpression3);
					}
				}
				return new QueryCubeExpression(cubeExpression, cubeExpression2.GetReferences(), EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, cubeExpression2, EmptyArray<CubeSortOrder>.Instance, RowRange.All);
			}
			QueryCubeExpression queryCubeExpression = (QueryCubeExpression)cubeExpression;
			return new QueryCubeExpression(this.ApplyVariableArguments(queryCubeExpression.From, arguments), queryCubeExpression.DimensionAttributes, queryCubeExpression.Properties, queryCubeExpression.Measures, queryCubeExpression.MeasureProperties, queryCubeExpression.Filter, queryCubeExpression.Sort, queryCubeExpression.RowRange);
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x00067828 File Offset: 0x00065A28
		private Query VisitQuery(QueryCubeExpression queryExpression)
		{
			if (queryExpression.DimensionAttributes.Count + queryExpression.Measures.Count == 0 || queryExpression.RowRange.TakeCount.IsZero || !queryExpression.RowRange.SkipCount.IsZero || this.FiltersNonAdditiveMeasures(queryExpression.Filter))
			{
				throw new NotSupportedException();
			}
			Query query = this.VisitSubquery(queryExpression.From);
			KeysBuilder keysBuilder = new KeysBuilder(queryExpression.DimensionAttributes.Count + queryExpression.Measures.Count);
			KeysBuilder keysBuilder2 = new KeysBuilder(queryExpression.DimensionAttributes.Count);
			int[] array = new int[queryExpression.DimensionAttributes.Count];
			for (int i = 0; i < queryExpression.DimensionAttributes.Count; i++)
			{
				IdentifierCubeExpression identifierCubeExpression = queryExpression.DimensionAttributes[i];
				keysBuilder.Add(identifierCubeExpression.Identifier);
				keysBuilder2.Add(identifierCubeExpression.Identifier);
				array[i] = query.Columns.IndexOfKey(identifierCubeExpression.Identifier);
			}
			ColumnConstructor[] array2 = new ColumnConstructor[queryExpression.Measures.Count];
			for (int j = 0; j < queryExpression.Measures.Count; j++)
			{
				string identifier = queryExpression.Measures[j].Identifier;
				SapHanaMeasure sapHanaMeasure;
				if (!this.cube.Measures.TryGetMeasure(identifier, out sapHanaMeasure))
				{
					throw new NotSupportedException();
				}
				keysBuilder.Add(identifier);
				array2[j] = new ColumnConstructor(identifier, QueryExpressionAssembler.Assemble(query.Columns, this.GetMeasureExpr(query.Columns, sapHanaMeasure)));
			}
			Grouping grouping = new Grouping(false, keysBuilder.ToKeys(), keysBuilder2.ToKeys(), array, array2, true, null, new QueryTableValue(query).Type.AsTableType);
			query = query.Group(grouping);
			SapHanaCubeExpressionVisitor.CubeExpressionTranslator cubeExpressionTranslator = new SapHanaCubeExpressionVisitor.CubeExpressionTranslator(this, query.Columns);
			if (queryExpression.Filter != null)
			{
				QueryExpression queryExpression2 = cubeExpressionTranslator.GetQueryExpression(queryExpression.Filter);
				FunctionValue functionValue = QueryExpressionAssembler.Assemble(query.Columns, queryExpression2);
				query = query.SelectRows(functionValue);
			}
			if (queryExpression.Sort.Count > 0)
			{
				SortOrder[] array3 = new SortOrder[queryExpression.Sort.Count];
				for (int k = 0; k < array3.Length; k++)
				{
					CubeSortOrder cubeSortOrder = queryExpression.Sort[k];
					QueryExpression queryExpression3 = cubeExpressionTranslator.GetQueryExpression(cubeSortOrder.Expression);
					FunctionValue functionValue2 = QueryExpressionAssembler.Assemble(query.Columns, queryExpression3);
					array3[k] = new SortOrder(functionValue2, null, cubeSortOrder.Ascending);
				}
				query = query.Sort(new TableSortOrder(array3));
			}
			return query.Take(queryExpression.RowRange.TakeCount);
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x00067AD8 File Offset: 0x00065CD8
		private Query VisitSubquery(CubeExpression cubeExpression)
		{
			CubeExpressionKind kind = cubeExpression.Kind;
			if (kind == CubeExpressionKind.Identifier)
			{
				return this.VisitSubquery((IdentifierCubeExpression)cubeExpression);
			}
			if (kind == CubeExpressionKind.Query)
			{
				return this.VisitQuery((QueryCubeExpression)cubeExpression);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x00067B14 File Offset: 0x00065D14
		private Query VisitSubquery(IdentifierCubeExpression identifierCubeExpression)
		{
			string identifier = identifierCubeExpression.Identifier;
			FromItem fromItem = base.NewFrom(this.cube.SchemaName, identifier);
			OdbcTableInfo orCreateTableInfo = this.queryDomain.DataSource.GetOrCreateTableInfo(new OdbcIdentifier(null, this.cube.SchemaName, identifier), null);
			return new OptimizableQuery(this.queryDomain.NewQuery(orCreateTableInfo, new FromItem[] { fromItem }));
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x00067B7A File Offset: 0x00065D7A
		protected override QueryExpression GetMeasureExpr(Keys columns, SapHanaMeasure measure)
		{
			if (measure.AggregationType == SapHanaAggregationType.Count)
			{
				return new InvocationQueryExpression(new ConstantQueryExpression(TableModule.Table.RowCount), new QueryExpression[] { ArgumentAccessQueryExpression.Instance });
			}
			return base.GetMeasureExpr(columns, measure);
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x00067BAC File Offset: 0x00065DAC
		private bool FiltersNonAdditiveMeasures(CubeExpression filter)
		{
			if (filter != null)
			{
				foreach (IdentifierCubeExpression identifierCubeExpression in filter.GetReferences())
				{
					SapHanaMeasure sapHanaMeasure;
					if (this.cube.Measures.TryGetMeasure(identifierCubeExpression.Identifier, out sapHanaMeasure) && !sapHanaMeasure.IsAdditive)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}
	}
}

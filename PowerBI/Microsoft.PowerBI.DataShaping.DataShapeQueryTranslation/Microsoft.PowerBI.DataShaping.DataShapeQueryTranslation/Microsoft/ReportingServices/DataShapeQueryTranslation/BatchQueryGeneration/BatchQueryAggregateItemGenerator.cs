using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000130 RID: 304
	internal sealed class BatchQueryAggregateItemGenerator : IPlanAggregateItemVisitor
	{
		// Token: 0x06000B6E RID: 2926 RVA: 0x0002D8B8 File Offset: 0x0002BAB8
		private BatchQueryAggregateItemGenerator(IQueryExpressionGenerator expressionGenerator, IEnumerable<QueryTableColumn> groupColumns, BatchQueryGenerationNamingContext namingContext)
		{
			this.m_expressionGenerator = expressionGenerator;
			this.m_columns = new QueryItemCollection<QueryTableColumn>();
			this.m_columnMap = new WritableGeneratedColumnMap();
			this.m_namingContext = namingContext;
			this.m_namingContext.RegisterNames(groupColumns.Select((QueryTableColumn c) => c.Name));
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002D920 File Offset: 0x0002BB20
		public static BatchQueryAggregateItemGenerationResult Generate(IQueryExpressionGenerator expressionGenerator, IEnumerable<QueryTableColumn> groupColumns, ReadOnlyCollection<PlanAggregateItem> planAggregateItems, BatchQueryGenerationNamingContext namingContext)
		{
			BatchQueryAggregateItemGenerator batchQueryAggregateItemGenerator = new BatchQueryAggregateItemGenerator(expressionGenerator, groupColumns, namingContext);
			foreach (PlanAggregateItem planAggregateItem in planAggregateItems)
			{
				planAggregateItem.Accept(batchQueryAggregateItemGenerator);
			}
			return new BatchQueryAggregateItemGenerationResult(batchQueryAggregateItemGenerator.Columns, batchQueryAggregateItemGenerator.ColumnMap);
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0002D980 File Offset: 0x0002BB80
		private IEnumerable<QueryTableColumn> Columns
		{
			get
			{
				return this.m_columns;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0002D988 File Offset: 0x0002BB88
		private GeneratedColumnMap ColumnMap
		{
			get
			{
				return this.m_columnMap;
			}
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002D990 File Offset: 0x0002BB90
		public void Visit(PlanAggregateCalculationItem item)
		{
			Calculation calculation = item.Calculation;
			foreach (KeyValuePair<ExpressionId, QueryExpressionContext> keyValuePair in this.m_expressionGenerator.TranslateCalculation(calculation))
			{
				QueryTableColumn queryTableColumn = this.AddOrReuseColumn(keyValuePair.Value.QueryExpression, calculation.Id.Value, item.PreferPlanName);
				this.m_columnMap.Add(keyValuePair.Key, queryTableColumn);
			}
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0002DA20 File Offset: 0x0002BC20
		public void Visit(PlanAggregateExpressionItem item)
		{
			QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(item.ExpressionId, item.ExpressionContext);
			QueryTableColumn queryTableColumn = this.AddOrReuseColumn(queryExpressionContext.QueryExpression, item.PlanName, item.PreferPlanName);
			this.m_columnMap.Add(item.PlanName, queryTableColumn);
			this.m_columnMap.Add(item.ExpressionId, queryTableColumn);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002DA84 File Offset: 0x0002BC84
		private QueryTableColumn AddOrReuseColumn(QueryExpression queryExpression, string candidateName, bool preferPlanName)
		{
			foreach (QueryTableColumn queryTableColumn in this.m_columns)
			{
				if (queryTableColumn.Expression.Equals(queryExpression))
				{
					return queryTableColumn;
				}
			}
			string text = (preferPlanName ? this.m_namingContext.CreateAndRegisterUniqueName(candidateName) : this.m_namingContext.CreateAndRegisterName(queryExpression, candidateName));
			QueryTableColumn queryTableColumn2 = queryExpression.ToQueryTableColumn(text);
			this.m_columns.Add(queryTableColumn2.Name, queryTableColumn2);
			return queryTableColumn2;
		}

		// Token: 0x040005C5 RID: 1477
		private readonly IQueryExpressionGenerator m_expressionGenerator;

		// Token: 0x040005C6 RID: 1478
		private readonly QueryItemCollection<QueryTableColumn> m_columns;

		// Token: 0x040005C7 RID: 1479
		private readonly WritableGeneratedColumnMap m_columnMap;

		// Token: 0x040005C8 RID: 1480
		private readonly BatchQueryGenerationNamingContext m_namingContext;
	}
}

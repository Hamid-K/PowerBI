using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000146 RID: 326
	internal sealed class BatchQueryProjectItemGenerator : IPlanProjectItemVisitor
	{
		// Token: 0x06000BFE RID: 3070 RVA: 0x00030724 File Offset: 0x0002E924
		private BatchQueryProjectItemGenerator(IQueryExpressionGenerator expressionGenerator, GeneratedTable input, BatchQueryGenerationNamingContext namingContext, CalculationExpressionMap calculationExpressions, DataShapeAnnotations annotations)
		{
			this.m_input = input;
			this.m_expressionGenerator = expressionGenerator;
			this.m_addedColumns = new QueryItemCollection<QueryTableColumn>();
			this.m_preservedColumns = new QueryItemCollection<QueryTableColumn>();
			this.m_outputColumnMap = new WritableGeneratedColumnMap();
			this.m_namingContext = namingContext;
			this.m_calculationExpressions = calculationExpressions;
			this.m_annotations = annotations;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00030780 File Offset: 0x0002E980
		public static BatchQueryProjectItemGenerator.BatchQueryProjectItemGenerationResult Generate(IQueryExpressionGenerator expressionGenerator, GeneratedTable input, IReadOnlyList<PlanProjectItem> planProjectItems, BatchQueryGenerationNamingContext namingContext, CalculationExpressionMap calculationExpressions, DataShapeAnnotations annotations)
		{
			BatchQueryProjectItemGenerator batchQueryProjectItemGenerator = new BatchQueryProjectItemGenerator(expressionGenerator, input, namingContext, calculationExpressions, annotations);
			foreach (PlanProjectItem planProjectItem in planProjectItems)
			{
				planProjectItem.Accept(batchQueryProjectItemGenerator);
			}
			return new BatchQueryProjectItemGenerator.BatchQueryProjectItemGenerationResult(batchQueryProjectItemGenerator.m_preservedColumns.Concat(batchQueryProjectItemGenerator.m_addedColumns), batchQueryProjectItemGenerator.m_outputColumnMap);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x000307F0 File Offset: 0x0002E9F0
		public void Visit(PlanTransformExistingColumnProjectItem item)
		{
			QueryTableColumn queryTableColumn = this.m_input.ColumnMap[item.NewColumnExpression.ExpressionId.Value];
			this.AddExistingColumnToOutputColumnMap(queryTableColumn, item, item.ExpressionContext.PropertyName);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00030834 File Offset: 0x0002EA34
		public void Visit(PlanTransformExistingColumnWithSameNameProjectItem item)
		{
			QueryTableColumn queryTableColumn = this.m_input.ColumnMap[item.ExistingColumnExpression.ExpressionId.Value];
			this.AddExistingColumnToOutputColumnMap(queryTableColumn, item, queryTableColumn.Name);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00030874 File Offset: 0x0002EA74
		private void AddExistingColumnToOutputColumnMap(QueryTableColumn existingColumn, PlanPreserveColumnsProjectItem item, string suggestedName)
		{
			QueryTableColumn queryTableColumn = this.AddOrReuseColumn(existingColumn.QdmReference(), suggestedName);
			foreach (ExpressionId expressionId in item.PlanIdentities)
			{
				this.m_outputColumnMap.Add(expressionId, queryTableColumn);
			}
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x000308D8 File Offset: 0x0002EAD8
		public void Visit(PlanCalculationProjectItem item)
		{
			Calculation calculation = item.Calculation;
			foreach (KeyValuePair<ExpressionId, QueryExpressionContext> keyValuePair in this.m_expressionGenerator.TranslateCalculation(calculation))
			{
				this.PreserveOrAddReferenceColumn(keyValuePair.Key, keyValuePair.Value.QueryExpression, calculation.Id.Value);
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00030958 File Offset: 0x0002EB58
		public void Visit(PlanDataMemberProjectItem item)
		{
			string value = item.DataMember.Id.Value;
			foreach (GroupKey groupKey in item.DataMember.Group.GroupKeys)
			{
				QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateGroupKeyReference(groupKey);
				this.PreserveOrAddReferenceColumn(groupKey.Value.ExpressionId.Value, queryExpressionContext.QueryExpression, value);
			}
			List<SortKey> sortKeys = item.DataMember.Group.SortKeys;
			SortByMeasureInfoCollection sortByMeasureInfos = this.m_annotations.DataMemberAnnotations.GetSortByMeasureInfos(item.DataMember);
			foreach (SortKey sortKey in sortKeys)
			{
				bool flag = sortByMeasureInfos != null && sortByMeasureInfos.ContainsKey(sortKey);
				if (!item.ExcludeMeasureSortKeys || !flag)
				{
					QueryExpressionContext queryExpressionContext2 = this.m_expressionGenerator.TranslateSortKeyReference(sortKey);
					this.PreserveOrAddReferenceColumn(sortKey.Value.ExpressionId.Value, queryExpressionContext2.QueryExpression, value);
				}
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00030A9C File Offset: 0x0002EC9C
		public void Visit(PlanNewColumnProjectItem item)
		{
			QueryExpressionContext queryExpressionContext;
			if (item.ExpressionNode != null)
			{
				queryExpressionContext = this.m_expressionGenerator.TranslateExpression(item.ExpressionNode, item.ExpressionContext);
			}
			else
			{
				queryExpressionContext = this.m_expressionGenerator.TranslateExpression(item.ExpressionId.GetValueOrDefault(), item.ExpressionContext);
			}
			QueryTableColumn queryTableColumn = null;
			switch (item.ColumnReuse)
			{
			case ColumnReuseKind.None:
				queryTableColumn = this.AddNewColumn(queryExpressionContext.QueryExpression, item.SuggestedName);
				break;
			case ColumnReuseKind.ByExpression:
				queryTableColumn = this.AddOrReuseColumn(queryExpressionContext.QueryExpression, item.SuggestedName);
				break;
			case ColumnReuseKind.ByReference:
				queryTableColumn = this.GetReferencedColumn(queryExpressionContext.QueryExpression, item.SuggestedName);
				break;
			default:
				Contract.RetailFail("Invalid ColumnReuseKind {0}", item.ColumnReuse);
				break;
			}
			if (item.ExpressionNode != null)
			{
				this.m_outputColumnMap.Add(item.SuggestedName, queryTableColumn);
				return;
			}
			this.m_outputColumnMap.Add(item.ExpressionId.GetValueOrDefault(), queryTableColumn);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00030B94 File Offset: 0x0002ED94
		public void Visit(PlanPreserveCalculationProjectItem item)
		{
			ReadOnlyCollection<ExpressionId> expressions = this.m_calculationExpressions.GetExpressions(item.Calculation);
			for (int i = 0; i < expressions.Count; i++)
			{
				ExpressionId expressionId = expressions[i];
				QueryTableColumn queryTableColumn = this.m_input.ColumnMap[expressionId];
				this.PreserveColumn(queryTableColumn);
				this.m_outputColumnMap.Add(expressionId, queryTableColumn);
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00030BF3 File Offset: 0x0002EDF3
		private bool IsOutputColumn(QueryTableColumn column)
		{
			return this.m_preservedColumns.Contains(column) || this.m_addedColumns.Contains(column);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00030C14 File Offset: 0x0002EE14
		public void Visit(PlanNamedColumnProjectItem item)
		{
			string columnName = item.ColumnName;
			QueryTableColumn queryTableColumn = this.m_input.ColumnMap[columnName];
			this.m_outputColumnMap.Add(columnName, queryTableColumn);
			this.PreserveColumn(queryTableColumn);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00030C50 File Offset: 0x0002EE50
		public void Visit(PlanMapColumnIdentityProjectItem item)
		{
			ExpressionId sourceIdentity = item.SourceIdentity;
			QueryTableColumn queryTableColumn = this.m_input.ColumnMap[sourceIdentity];
			foreach (ExpressionId expressionId in item.TargetIdentities)
			{
				this.m_outputColumnMap.Add(expressionId, queryTableColumn);
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00030CBC File Offset: 0x0002EEBC
		public void Visit(PlanPreserveColumnsProjectItem item)
		{
			foreach (ExpressionId expressionId in item.PlanIdentities)
			{
				QueryTableColumn queryTableColumn = this.m_input.ColumnMap[expressionId];
				this.m_outputColumnMap.Add(expressionId, queryTableColumn);
				this.PreserveColumn(queryTableColumn);
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00030D2C File Offset: 0x0002EF2C
		public void Visit(PlanPreserveAllColumnsProjectItem item)
		{
			this.PreserveAllColumns(null);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00030D38 File Offset: 0x0002EF38
		private void PreserveAllColumns(HashSet<QueryTableColumn> excludedColumns = null)
		{
			this.m_outputColumnMap.AddColumnMap(this.m_input.ColumnMap, excludedColumns);
			IEnumerable<QueryTableColumn> columns = this.m_input.QueryTable.Columns;
			HashSet<QueryTableColumn> hashSet = new HashSet<QueryTableColumn>(this.m_input.ColumnMap.GetAllColumns());
			foreach (QueryTableColumn queryTableColumn in columns)
			{
				if (hashSet.Contains(queryTableColumn) && (excludedColumns == null || !excludedColumns.Contains(queryTableColumn)))
				{
					this.PreserveColumn(queryTableColumn);
				}
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00030DD4 File Offset: 0x0002EFD4
		private void PreserveAllColumns(IReadOnlyDictionary<ExpressionId, QueryTableColumn> excludedExpressionIds, IReadOnlyDictionary<string, QueryTableColumn> excludedPlanColumnNames)
		{
			WritableGeneratedColumnMap writableGeneratedColumnMap = this.m_input.ColumnMap.FilterColumns(excludedExpressionIds, excludedPlanColumnNames);
			this.m_outputColumnMap.AddColumnMap(writableGeneratedColumnMap, null);
			HashSet<QueryTableColumn> hashSet = new HashSet<QueryTableColumn>(writableGeneratedColumnMap.GetAllColumns());
			foreach (QueryTableColumn queryTableColumn in this.m_input.QueryTable.Columns)
			{
				if (hashSet.Contains(queryTableColumn))
				{
					this.PreserveColumn(queryTableColumn);
				}
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00030E64 File Offset: 0x0002F064
		public void Visit(PlanPreserveAllColumnsExceptProjectItem item)
		{
			HashSet<QueryTableColumn> hashSet = new HashSet<QueryTableColumn>();
			if (item.ColumnNames != null)
			{
				foreach (string text in item.ColumnNames)
				{
					QueryTableColumn queryTableColumn;
					if (!this.m_input.ColumnMap.TryGetColumn(text, out queryTableColumn))
					{
						throw new InvalidOperationException(StringUtil.FormatInvariant("Cannot remove column '{0}' because it does not exist in the input table", new object[] { text }));
					}
					hashSet.Add(queryTableColumn);
				}
				this.PreserveAllColumns(hashSet);
				return;
			}
			BatchQueryProjectItemGenerator.BatchQueryProjectItemGenerationResult batchQueryProjectItemGenerationResult = BatchQueryProjectItemGenerator.Generate(this.m_expressionGenerator, this.m_input, item.PlanProjectItems, this.m_namingContext, this.m_calculationExpressions, this.m_annotations);
			this.PreserveAllColumns(batchQueryProjectItemGenerationResult.OutputColumnMap.ExpressionMap, batchQueryProjectItemGenerationResult.OutputColumnMap.PlanColumnNameMap);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00030F40 File Offset: 0x0002F140
		private QueryTableColumn PreserveColumn(QueryTableColumn column)
		{
			QueryTableColumn queryTableColumn = column.ToReferenceColumn();
			this.m_preservedColumns.Add(column.Name, queryTableColumn);
			this.m_namingContext.RegisterName(column.Name);
			return queryTableColumn;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00030F7C File Offset: 0x0002F17C
		private QueryTableColumn AddOrReuseColumn(QueryExpression expression, string fallbackName)
		{
			QueryTableColumn queryTableColumn = this.m_addedColumns.Concat(this.m_preservedColumns).FirstOrDefault((QueryTableColumn c) => expression.Equals(c.Expression));
			if (queryTableColumn != null)
			{
				return queryTableColumn;
			}
			return this.AddNewColumn(expression, fallbackName);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00030FCC File Offset: 0x0002F1CC
		private void AddReferenceColumn(QueryExpression queryExpression, ExpressionId expressionId, string fallbackName)
		{
			QueryTableColumn referencedColumn = this.GetReferencedColumn(queryExpression, fallbackName);
			this.m_outputColumnMap.Add(expressionId, referencedColumn);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00030FF0 File Offset: 0x0002F1F0
		private QueryTableColumn GetReferencedColumn(QueryExpression expression, string fallbackName)
		{
			QdmTableColumnReferenceExpression qdmTableColumnReferenceExpression = expression as QdmTableColumnReferenceExpression;
			QueryTableColumn queryTableColumn;
			if (qdmTableColumnReferenceExpression != null)
			{
				if (this.IsOutputColumn(qdmTableColumnReferenceExpression.Target))
				{
					queryTableColumn = qdmTableColumnReferenceExpression.Target;
				}
				else
				{
					queryTableColumn = this.PreserveColumn(qdmTableColumnReferenceExpression.Target);
				}
			}
			else
			{
				queryTableColumn = this.AddOrReuseColumn(expression, fallbackName);
			}
			return queryTableColumn;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00031038 File Offset: 0x0002F238
		private QueryTableColumn AddNewColumn(QueryExpression expression, string fallbackName)
		{
			string text = this.m_namingContext.CreateAndRegisterName(expression, fallbackName);
			QueryTableColumn queryTableColumn = expression.ToQueryTableColumn(text);
			this.m_addedColumns.Add(queryTableColumn.Name, queryTableColumn);
			return queryTableColumn;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00031070 File Offset: 0x0002F270
		private void PreserveOrAddReferenceColumn(ExpressionId expressionId, QueryExpression queryExpression, string fallbackName)
		{
			QueryTableColumn queryTableColumn;
			if (this.m_input.ColumnMap.TryGetColumn(expressionId, out queryTableColumn))
			{
				this.m_outputColumnMap.Add(expressionId, queryTableColumn);
				this.PreserveColumn(queryTableColumn);
				return;
			}
			this.AddReferenceColumn(queryExpression, expressionId, fallbackName);
		}

		// Token: 0x0400060E RID: 1550
		private readonly QueryItemCollection<QueryTableColumn> m_preservedColumns;

		// Token: 0x0400060F RID: 1551
		private readonly QueryItemCollection<QueryTableColumn> m_addedColumns;

		// Token: 0x04000610 RID: 1552
		private readonly WritableGeneratedColumnMap m_outputColumnMap;

		// Token: 0x04000611 RID: 1553
		private readonly GeneratedTable m_input;

		// Token: 0x04000612 RID: 1554
		private readonly IQueryExpressionGenerator m_expressionGenerator;

		// Token: 0x04000613 RID: 1555
		private readonly BatchQueryGenerationNamingContext m_namingContext;

		// Token: 0x04000614 RID: 1556
		private readonly CalculationExpressionMap m_calculationExpressions;

		// Token: 0x04000615 RID: 1557
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x020002E0 RID: 736
		internal sealed class BatchQueryProjectItemGenerationResult
		{
			// Token: 0x06001697 RID: 5783 RVA: 0x00051C11 File Offset: 0x0004FE11
			internal BatchQueryProjectItemGenerationResult(IEnumerable<QueryTableColumn> projectedColumns, GeneratedColumnMap outputColumnMap)
			{
				this.ProjectedColumns = projectedColumns.ToReadOnlyList<QueryTableColumn>();
				this.OutputColumnMap = outputColumnMap;
			}

			// Token: 0x170003F4 RID: 1012
			// (get) Token: 0x06001698 RID: 5784 RVA: 0x00051C2C File Offset: 0x0004FE2C
			public IReadOnlyList<QueryTableColumn> ProjectedColumns { get; }

			// Token: 0x170003F5 RID: 1013
			// (get) Token: 0x06001699 RID: 5785 RVA: 0x00051C34 File Offset: 0x0004FE34
			public GeneratedColumnMap OutputColumnMap { get; }
		}
	}
}

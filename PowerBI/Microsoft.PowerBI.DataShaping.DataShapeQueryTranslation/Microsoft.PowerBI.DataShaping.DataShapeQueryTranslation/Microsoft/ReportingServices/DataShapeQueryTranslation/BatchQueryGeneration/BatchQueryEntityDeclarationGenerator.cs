using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000135 RID: 309
	internal sealed class BatchQueryEntityDeclarationGenerator
	{
		// Token: 0x06000B83 RID: 2947 RVA: 0x0002DDA0 File Offset: 0x0002BFA0
		public BatchQueryEntityDeclarationGenerator(BatchQueryGenerationContext context, IQueryExpressionGenerator expressionGenerator, BatchQueryTableGenerator tableGenerator, BatchQueryExpressionReferenceContext referenceContext)
		{
			this._context = context;
			this._expressionGenerator = expressionGenerator;
			this._tableGenerator = tableGenerator;
			this._referenceContext = referenceContext;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002DDC8 File Offset: 0x0002BFC8
		public global::System.ValueTuple<QueryTableDeclarationExpression, GeneratedColumnMap> Generate(PlanNamedEntity planEntity, string queryName, BatchQueryGenerationNamingContext namingContext)
		{
			NamingContext namingContext2 = new NamingContext(QueryNamingContext.NameComparer);
			GeneratedTable generatedTable = this._tableGenerator.Generate(planEntity.Value, this._expressionGenerator, namingContext);
			this._referenceContext.PushReferenceTable(generatedTable);
			WritableGeneratedColumnMap writableGeneratedColumnMap = new WritableGeneratedColumnMap();
			writableGeneratedColumnMap.AddColumnMap(generatedTable.ColumnMap, null);
			BatchQueryEntityDeclarationGenerator.RegisterColumnNames(namingContext2, generatedTable);
			QueryTableDeclarationBuilder queryTableDeclarationBuilder = new QueryTableDeclarationBuilder(queryName, generatedTable.QueryTable, this._context.Schema.GetDefaultSchema());
			this.GenerateAdditionalColumns(planEntity.AdditionalColumns, queryTableDeclarationBuilder, writableGeneratedColumnMap, namingContext2);
			this.UpdateSubtotalColumnMapping(planEntity.SubtotalsOverAdditionalColumns, writableGeneratedColumnMap);
			if (planEntity.VisualShape != null)
			{
				this.GenerateVisualShape(planEntity.VisualShape, generatedTable.ColumnMap, queryTableDeclarationBuilder.AddVisualShape(), writableGeneratedColumnMap, namingContext2);
			}
			this._referenceContext.PopReferenceTable();
			return new global::System.ValueTuple<QueryTableDeclarationExpression, GeneratedColumnMap>(queryTableDeclarationBuilder.ToDeclaration(), writableGeneratedColumnMap);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002DE94 File Offset: 0x0002C094
		private static void RegisterColumnNames(NamingContext columnNamingContext, GeneratedTable tableValue)
		{
			foreach (QueryTableColumn queryTableColumn in tableValue.QueryTable.Columns)
			{
				columnNamingContext.RegisterUniqueName(queryTableColumn.Name);
			}
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002DEEC File Offset: 0x0002C0EC
		private void GenerateVisualShape(PlanVisualShape planVisualShape, GeneratedColumnMap inputColumnMap, QueryVisualShapeBuilder visualShapeBuilder, WritableGeneratedColumnMap columnMap, NamingContext columnNamingContext)
		{
			if (planVisualShape.VisualAxes != null)
			{
				foreach (VisualAxis visualAxis in planVisualShape.VisualAxes)
				{
					QueryVisualAxisBuilder queryVisualAxisBuilder = visualShapeBuilder.AddAxis(BatchQueryEntityDeclarationGenerator.GenerateAxisName(visualAxis.Name));
					this.GenerateVisualAxis(visualAxis, queryVisualAxisBuilder, inputColumnMap);
				}
			}
			if (planVisualShape.IsDensifiedColumnName != null)
			{
				string text = columnNamingContext.GenerateUniqueName(planVisualShape.IsDensifiedColumnName);
				QueryTableColumn queryTableColumn = visualShapeBuilder.SetIsDensifiedColumnName(text);
				columnMap.Add(planVisualShape.IsDensifiedColumnName, queryTableColumn);
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002DF84 File Offset: 0x0002C184
		private void GenerateVisualAxis(VisualAxis axis, QueryVisualAxisBuilder axisBuilder, GeneratedColumnMap inputColumnMap)
		{
			QueryVisualAxisGroupBuilder queryVisualAxisGroupBuilder = null;
			foreach (VisualAxisGroup visualAxisGroup in axis.Groups)
			{
				IScope resolvedScope = visualAxisGroup.Member.GetResolvedScope(this._context.ExpressionTable);
				DataMember dataMember = resolvedScope as DataMember;
				Microsoft.DataShaping.Contract.RetailAssert(dataMember != null, "The member of an axis group must reference a data member. Reference type: {0}, Expression:{1}", ((resolvedScope != null) ? resolvedScope.GetType().ToString() : null) ?? "<null>", visualAxisGroup.Member.ExpressionId);
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				if (this._context.Annotations.TryGetBatchSubtotalAnnotation(dataMember, out batchSubtotalAnnotation) && batchSubtotalAnnotation.Usage.IsIncludeInStage(SubtotalUsage.VisualCalculations))
				{
					queryVisualAxisGroupBuilder = axisBuilder.AddGroup();
					QueryTableColumn queryTableColumn = inputColumnMap[batchSubtotalAnnotation.SubtotalIndicatorColumnName];
					queryVisualAxisGroupBuilder.SetSubtotalIndicator(queryTableColumn.QdmReference());
				}
				else if (queryVisualAxisGroupBuilder == null)
				{
					queryVisualAxisGroupBuilder = axisBuilder.AddGroup();
				}
				this.AddKeysToVisualAxisGroup(dataMember, queryVisualAxisGroupBuilder, inputColumnMap);
				if (dataMember.Group.SortKeys != null)
				{
					foreach (SortKey sortKey in dataMember.Group.SortKeys)
					{
						ExpressionId value = sortKey.Value.ExpressionId.Value;
						axisBuilder.AddOrderBy(inputColumnMap[value].QdmReference().ToSortClause(BatchQueryGenerationUtils.TranslateSortDirection(sortKey.SortDirection.Value, false)));
					}
				}
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002E138 File Offset: 0x0002C338
		private void AddKeysToVisualAxisGroup(DataMember dataMember, QueryVisualAxisGroupBuilder groupBuilder, GeneratedColumnMap inputColumnMap)
		{
			HashSet<QueryTableColumn> hashSet = new HashSet<QueryTableColumn>();
			foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey groupKey in dataMember.Group.GroupKeys)
			{
				BatchQueryEntityDeclarationGenerator.AddKeyToGroup(groupBuilder, groupKey.Value, inputColumnMap, hashSet);
			}
			foreach (Calculation calculation in dataMember.Calculations)
			{
				ExpressionId expressionId;
				if (this._context.GroupDetailMapping.TryGetGroupKey(calculation.Value.ExpressionId.Value, out expressionId))
				{
					BatchQueryEntityDeclarationGenerator.AddKeyToGroup(groupBuilder, calculation.Value, inputColumnMap, hashSet);
				}
			}
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002E214 File Offset: 0x0002C414
		private static void AddKeyToGroup(QueryVisualAxisGroupBuilder groupBuilder, Expression expression, GeneratedColumnMap inputColumnMap, HashSet<QueryTableColumn> addedKeyColumns)
		{
			QueryTableColumn queryTableColumn = inputColumnMap[expression.ExpressionId.Value];
			if (addedKeyColumns.Add(queryTableColumn))
			{
				groupBuilder.AddKey(queryTableColumn.QdmReference());
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002E24C File Offset: 0x0002C44C
		private static QueryVisualAxisName GenerateAxisName(string name)
		{
			StringComparer nameComparer = QueryNamingContext.NameComparer;
			if (nameComparer.Equals(name, "rows"))
			{
				return QueryVisualAxisName.Rows;
			}
			if (nameComparer.Equals(name, "columns"))
			{
				return QueryVisualAxisName.Columns;
			}
			if (nameComparer.Equals(name, "rowpages"))
			{
				return QueryVisualAxisName.RowPages;
			}
			throw new NotSupportedException("Unknown axis name " + name.MarkAsCustomerContent() + ".");
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002E2AC File Offset: 0x0002C4AC
		private void GenerateAdditionalColumns(IReadOnlyList<Calculation> additionalColumns, QueryTableDeclarationBuilder tableBuilder, WritableGeneratedColumnMap expressionToColumnMap, NamingContext columnNamingContext)
		{
			if (additionalColumns == null)
			{
				return;
			}
			List<global::System.ValueTuple<ExpressionId, QueryExpression, string>> list = null;
			foreach (Calculation calculation in additionalColumns)
			{
				List<KeyValuePair<ExpressionId, QueryExpressionContext>> list2 = this._expressionGenerator.TranslateCalculation(calculation);
				Microsoft.DataShaping.Contract.RetailAssert(list2.Count == 1, "Earlier stages should have ensured that this Calculation has a single expression.");
				ExpressionId key = list2[0].Key;
				QueryExpressionContext value = list2[0].Value;
				if (!string.IsNullOrEmpty(calculation.NativeReferenceName))
				{
					string nativeReferenceName = calculation.NativeReferenceName;
					columnNamingContext.RegisterUniqueName(nativeReferenceName);
					BatchQueryEntityDeclarationGenerator.AddAdditionalColumn(tableBuilder, expressionToColumnMap, key, nativeReferenceName, value.QueryExpression);
				}
				else
				{
					Microsoft.DataShaping.Util.AddToLazyList<global::System.ValueTuple<ExpressionId, QueryExpression, string>>(ref list, new global::System.ValueTuple<ExpressionId, QueryExpression, string>(key, value.QueryExpression, calculation.Id.Value));
				}
			}
			if (list != null)
			{
				foreach (global::System.ValueTuple<ExpressionId, QueryExpression, string> valueTuple in list)
				{
					string text = columnNamingContext.GenerateUniqueName(valueTuple.Item3);
					BatchQueryEntityDeclarationGenerator.AddAdditionalColumn(tableBuilder, expressionToColumnMap, valueTuple.Item1, text, valueTuple.Item2);
				}
			}
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002E3F0 File Offset: 0x0002C5F0
		private static void AddAdditionalColumn(QueryTableDeclarationBuilder tableBuilder, WritableGeneratedColumnMap expressionToColumnMap, ExpressionId expressionId, string name, QueryExpression queryExpression)
		{
			QueryTableColumn queryTableColumn = tableBuilder.AddAdditionalColumn(queryExpression, name);
			expressionToColumnMap.Add(expressionId, queryTableColumn);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002E410 File Offset: 0x0002C610
		private void UpdateSubtotalColumnMapping(IReadOnlyList<Calculation> subtotalsOverAdditionalColumns, WritableGeneratedColumnMap expressionToColumnMap)
		{
			if (subtotalsOverAdditionalColumns == null)
			{
				return;
			}
			foreach (Calculation calculation in subtotalsOverAdditionalColumns)
			{
				Calculation calculation2;
				Microsoft.DataShaping.Contract.RetailAssert(this._context.Annotations.IsSubtotal(calculation, out calculation2), "The calculation {0} is not a subtotal but was included in {1} which should only contain subtotals.", calculation.Id, "subtotalsOverAdditionalColumns");
				expressionToColumnMap.Add(calculation.Value.ExpressionId.Value, expressionToColumnMap[calculation2.Value.ExpressionId.Value]);
			}
		}

		// Token: 0x040005D2 RID: 1490
		private readonly BatchQueryGenerationContext _context;

		// Token: 0x040005D3 RID: 1491
		private readonly IQueryExpressionGenerator _expressionGenerator;

		// Token: 0x040005D4 RID: 1492
		private readonly BatchQueryTableGenerator _tableGenerator;

		// Token: 0x040005D5 RID: 1493
		private readonly BatchQueryExpressionReferenceContext _referenceContext;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200014B RID: 331
	internal sealed class BatchTopNPerLevelQueryTableGenerator
	{
		// Token: 0x06000C4E RID: 3150 RVA: 0x00032B9C File Offset: 0x00030D9C
		private BatchTopNPerLevelQueryTableGenerator(ExpressionTable expressionTable, DataShapeAnnotations annotations, GeneratedTable generatedTableInput, IQueryExpressionGenerator expressionGenerator, PlanOperationTopNPerLevelSample planTopNPerLevelSample, TranslationErrorContext translationErrorContext, FederatedEntityDataModel model, IFeatureSwitchProvider featureSwitchProvider)
		{
			this._expressionTable = expressionTable;
			this._annotations = annotations;
			this._generatedTableInput = generatedTableInput;
			this._expressionGenerator = expressionGenerator;
			this._planTopNPerLevelSample = planTopNPerLevelSample;
			this._translationErrorContext = translationErrorContext;
			this._model = model;
			this._featureSwitchProvider = featureSwitchProvider;
			this._pathExpressionContext = new ExpressionContext(translationErrorContext, ObjectType.TopNPerLevelLimitOperator, null, "PathValues");
			this._windowExpressionContext = new ExpressionContext(translationErrorContext, ObjectType.TopNPerLevelLimitOperator, null, "WindowExpansionInstance");
			this._levelsExpressionContext = new ExpressionContext(translationErrorContext, ObjectType.TopNPerLevelLimitOperator, null, "Levels");
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00032C2B File Offset: 0x00030E2B
		internal bool UseConceptualSchema
		{
			get
			{
				return this._featureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema);
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00032C3A File Offset: 0x00030E3A
		public static GeneratedTable Generate(PlanOperationTopNPerLevelSample planTopNPerLevelSample, IQueryExpressionGenerator expressionGenerator, GeneratedTable generatedTableInput, ExpressionTable expressionTable, DataShapeAnnotations annotations, TranslationErrorContext translationErrorContext, FederatedEntityDataModel model, IFeatureSwitchProvider featureSwitchProvider)
		{
			return new BatchTopNPerLevelQueryTableGenerator(expressionTable, annotations, generatedTableInput, expressionGenerator, planTopNPerLevelSample, translationErrorContext, model, featureSwitchProvider).Generate();
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00032C54 File Offset: 0x00030E54
		private GeneratedTable Generate()
		{
			Contract.RetailAssert(this._planTopNPerLevelSample.SortItems.Count >= 1, "Expect at least one sort item");
			QueryExpressionContext queryExpressionContext = this._expressionGenerator.TranslateExpression(this._planTopNPerLevelSample.RowCount.Expression, this._planTopNPerLevelSample.RowCount.Context);
			this._topNPerLevelTableBuilder = new TopNPerLevelTableBuilder(this._generatedTableInput.QueryTable, queryExpressionContext.QueryExpression, this._planTopNPerLevelSample.RestartIndicatorColumnName);
			DataMember member = this._planTopNPerLevelSample.SortItems[0].Member;
			QueryExpression subtotalColumnRefererence = this.GetSubtotalColumnRefererence(member);
			this._topNPerLevelTableBuilder.AddLevel(0, subtotalColumnRefererence, null, null, null);
			List<IReadOnlyList<QueryExpression>> list = new List<IReadOnlyList<QueryExpression>>();
			List<IReadOnlyList<QueryExpression>> list2 = new List<IReadOnlyList<QueryExpression>>();
			for (int i = 0; i < this._planTopNPerLevelSample.SortItems.Count; i++)
			{
				PlanMemberSortItem planMemberSortItem = this._planTopNPerLevelSample.SortItems[i];
				QueryExpression queryExpression = null;
				if (i < this._planTopNPerLevelSample.SortItems.Count - 1)
				{
					PlanMemberSortItem planMemberSortItem2 = this._planTopNPerLevelSample.SortItems[i + 1];
					queryExpression = this.GetSubtotalColumnRefererence(planMemberSortItem2.Member);
				}
				List<QuerySortClause> list3 = BatchQuerySortItemGenerator.Generate(this._generatedTableInput, planMemberSortItem, this._expressionTable, this._model, this.UseConceptualSchema, false).ToList<QuerySortClause>();
				SortByMeasureInfoCollection sortByMeasureInfos = this._annotations.DataMemberAnnotations.GetSortByMeasureInfos(planMemberSortItem.Member);
				List<QueryExpression> list4 = new List<QueryExpression>();
				if (list3.Count == planMemberSortItem.Member.Group.SortKeys.Count)
				{
					for (int j = 0; j < list3.Count; j++)
					{
						SortKey sortKey = planMemberSortItem.Member.Group.SortKeys[j];
						if (sortByMeasureInfos == null || !sortByMeasureInfos.ContainsKey(sortKey))
						{
							list4.Add(list3[j].Expression);
						}
					}
				}
				IReadOnlyList<QueryExpression> levelValueColumns = this.GetLevelValueColumns(i);
				list.Add(levelValueColumns);
				list2.Add(list4);
				this._topNPerLevelTableBuilder.AddLevel(i + 1, queryExpression, list3, levelValueColumns, list4);
			}
			if (this._planTopNPerLevelSample.WindowExpansionInstance != null)
			{
				TopNPerLevelWindowExpansionBuilder topNPerLevelWindowExpansionBuilder = this._topNPerLevelTableBuilder.WithExpansion();
				this.GenerateWindowExpansionState(this._planTopNPerLevelSample.WindowExpansionInstance, topNPerLevelWindowExpansionBuilder, list.GetEnumerator(), list2.GetEnumerator());
			}
			QueryTable queryTable = this._topNPerLevelTableBuilder.ToQueryTable();
			WritableGeneratedColumnMap writableGeneratedColumnMap = this._generatedTableInput.ColumnMap.FilterColumns(queryTable.Columns);
			writableGeneratedColumnMap.Add(this._planTopNPerLevelSample.RestartIndicatorColumnName, BatchQdmExpressionBuilder.CreateDerivedColumn<long>(this._planTopNPerLevelSample.RestartIndicatorColumnName));
			return new GeneratedTable(queryTable, writableGeneratedColumnMap);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00032F08 File Offset: 0x00031108
		private QueryExpression GetSubtotalColumnRefererence(DataMember dynamic)
		{
			BatchSubtotalAnnotation batchSubtotalAnnotation;
			if (!this._annotations.TryGetBatchSubtotalAnnotation(dynamic, out batchSubtotalAnnotation))
			{
				return null;
			}
			QueryTableColumn queryTableColumn;
			this._generatedTableInput.ColumnMap.TryGetColumn(batchSubtotalAnnotation.SubtotalIndicatorColumnName, out queryTableColumn);
			return queryTableColumn.QdmReference();
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00032F48 File Offset: 0x00031148
		private void GenerateWindowExpansionState(LimitWindowExpansionInstance limitWindowExpansionInstance, TopNPerLevelWindowExpansionBuilder windowExpansionBuilder, IEnumerator<IReadOnlyList<QueryExpression>> levelValueSourceExpressions, IEnumerator<IReadOnlyList<QueryExpression>> windowValueSourceExpressions)
		{
			if (!limitWindowExpansionInstance.Values.IsNullOrEmpty<Expression>() && levelValueSourceExpressions.MoveNext())
			{
				IReadOnlyList<QueryExpression> readOnlyList = limitWindowExpansionInstance.Values.Select((Expression p, int index) => this.ConvertToQueryExpression(p, this._pathExpressionContext, levelValueSourceExpressions.Current[index])).EvaluateReadOnly<QueryExpression>();
				windowExpansionBuilder.WithValues(readOnlyList);
			}
			if (windowValueSourceExpressions.MoveNext() && !limitWindowExpansionInstance.WindowValues.IsNullOrEmpty<LimitWindowExpansionValue>())
			{
				Func<Expression, int, QueryExpression> <>9__1;
				foreach (LimitWindowExpansionValue limitWindowExpansionValue in limitWindowExpansionInstance.WindowValues)
				{
					IEnumerable<Expression> values = limitWindowExpansionValue.Values;
					Func<Expression, int, QueryExpression> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (Expression w, int index) => this.ConvertToQueryExpression(w, this._windowExpressionContext, windowValueSourceExpressions.Current[index]));
					}
					IReadOnlyList<QueryExpression> readOnlyList2 = values.Select(func).EvaluateReadOnly<QueryExpression>();
					windowExpansionBuilder.WithWindow(readOnlyList2, limitWindowExpansionValue.WindowKind);
				}
			}
			if (!limitWindowExpansionInstance.Children.IsNullOrEmpty<LimitWindowExpansionInstance>())
			{
				foreach (LimitWindowExpansionInstance limitWindowExpansionInstance2 in limitWindowExpansionInstance.Children)
				{
					TopNPerLevelWindowExpansionBuilder topNPerLevelWindowExpansionBuilder = windowExpansionBuilder.WithChild();
					this.GenerateWindowExpansionState(limitWindowExpansionInstance2, topNPerLevelWindowExpansionBuilder, levelValueSourceExpressions, windowValueSourceExpressions);
				}
			}
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x000330B4 File Offset: 0x000312B4
		private QueryExpression ConvertToQueryExpression(Expression expression, ExpressionContext expressionContext, QueryExpression correspondingSourceExpresion)
		{
			if (this._expressionGenerator.IsNullLiteralExpression(expression))
			{
				Contract.RetailAssert(correspondingSourceExpresion != null, "correspondingSourceExpresion should not be null.");
				return this._expressionGenerator.TranslateNullExpression(expression, correspondingSourceExpresion.ConceptualResultType).QueryExpression;
			}
			return this._expressionGenerator.TranslateExpression(expression.ExpressionId.Value, expressionContext).QueryExpression;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00033114 File Offset: 0x00031314
		private IReadOnlyList<QueryExpression> GetLevelValueColumns(int index)
		{
			IReadOnlyList<IReadOnlyList<Expression>> levels = this._planTopNPerLevelSample.Levels;
			if (levels.IsNullOrEmpty<IReadOnlyList<Expression>>())
			{
				return null;
			}
			if (index >= levels.Count)
			{
				return null;
			}
			return levels[index].Select((Expression l) => this.ConvertToQueryExpression(l, this._levelsExpressionContext, null)).ToList<QueryExpression>();
		}

		// Token: 0x04000628 RID: 1576
		private readonly ExpressionTable _expressionTable;

		// Token: 0x04000629 RID: 1577
		private readonly DataShapeAnnotations _annotations;

		// Token: 0x0400062A RID: 1578
		private readonly GeneratedTable _generatedTableInput;

		// Token: 0x0400062B RID: 1579
		private readonly IQueryExpressionGenerator _expressionGenerator;

		// Token: 0x0400062C RID: 1580
		private readonly PlanOperationTopNPerLevelSample _planTopNPerLevelSample;

		// Token: 0x0400062D RID: 1581
		private readonly TranslationErrorContext _translationErrorContext;

		// Token: 0x0400062E RID: 1582
		private readonly FederatedEntityDataModel _model;

		// Token: 0x0400062F RID: 1583
		private readonly IFeatureSwitchProvider _featureSwitchProvider;

		// Token: 0x04000630 RID: 1584
		private TopNPerLevelTableBuilder _topNPerLevelTableBuilder;

		// Token: 0x04000631 RID: 1585
		private ExpressionContext _pathExpressionContext;

		// Token: 0x04000632 RID: 1586
		private ExpressionContext _windowExpressionContext;

		// Token: 0x04000633 RID: 1587
		private ExpressionContext _levelsExpressionContext;
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDefinitionGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataTransformBypass;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DefinitionGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDefinitionGeneration
{
	// Token: 0x0200015F RID: 351
	internal class BatchDsdGenerator : DsdGeneratorBase
	{
		// Token: 0x06000CB9 RID: 3257 RVA: 0x00034914 File Offset: 0x00032B14
		private BatchDsdGenerator(BatchDataSetPlanningResult dataSetPlanningResult, ReadOnlyCollection<BatchQueryGenerationResult> queryGenerationResults, DataShapeAnnotations annotations, ScopeTree scopeTree, TranslationErrorContext errorContext, DataSourceContext dataSourceContext, DataTransformRestorationResult transformRestorationResult, CancellationToken cancellationToken, bool applyTransformsInQuery, bool generateComposableQueryColumnNames, bool disableDictionaryEncoding, bool useConceptualSchema)
			: base(annotations, scopeTree, errorContext, dataSourceContext, applyTransformsInQuery, useConceptualSchema)
		{
			this.m_dataSetPlanningResult = dataSetPlanningResult;
			this.m_queryGenerationResult = queryGenerationResults.Single<BatchQueryGenerationResult>();
			this.m_transformRestorationResult = transformRestorationResult;
			this.m_finalExpressionTable = this.m_transformRestorationResult.ExpressionTables.Single<ReadOnlyExpressionTable>();
			this.m_generateComposableQueryColumnNames = generateComposableQueryColumnNames;
			this.m_cancellationToken = cancellationToken;
			this.m_disableDictionaryEncoding = disableDictionaryEncoding;
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0003497C File Offset: 0x00032B7C
		public static DataShapeDefinition Generate(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dsqDataShape, DataSourceContext dataSourceContext, BatchDataSetPlanningResult dataSetPlanningResult, ReadOnlyCollection<BatchQueryGenerationResult> queryGenerationResults, DataShapeAnnotations annotations, ScopeTree scopeTree, TranslationErrorContext errorContext, DataTransformRestorationResult transformRestorationResult, CancellationToken cancellationToken, bool applyTransformsInQuery, bool generateComposableQueryColumnNames, bool disableDictionaryEncoding, bool useConceptualSchema)
		{
			DataShapeDefinition dataShapeDefinition;
			try
			{
				dataShapeDefinition = new BatchDsdGenerator(dataSetPlanningResult, queryGenerationResults, annotations, scopeTree, errorContext, dataSourceContext, transformRestorationResult, cancellationToken, applyTransformsInQuery, generateComposableQueryColumnNames, disableDictionaryEncoding, useConceptualSchema).Generate(dsqDataShape);
			}
			catch (DefinitionGenerationException)
			{
				dataShapeDefinition = null;
			}
			return dataShapeDefinition;
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x000349C4 File Offset: 0x00032BC4
		protected override ResultEncodingHints BuildResultEncodingHints()
		{
			CalculationsWithSharedValues calculationsWithSharedValues = this.m_dataSetPlanningResult.CalculationsWithSharedValues;
			return ResultEncodingHints.Create((calculationsWithSharedValues != null) ? calculationsWithSharedValues.CalculationsList : null, this.m_disableDictionaryEncoding);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x000349E8 File Offset: 0x00032BE8
		protected override Collation BuildCollation(DataSourceContext dataSourceContext)
		{
			bool flag;
			Collation collation = base.BuildCollationBasicProperties(dataSourceContext, out flag);
			collation.PreferOrdinalStringEquality = flag;
			return collation;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00034A08 File Offset: 0x00032C08
		protected override DataBinding BuildDataBinding(IDataBoundItem item)
		{
			BatchDataBinding dataBindingForItem = this.m_dataSetPlanningResult.GetDataBindingForItem(item);
			if (dataBindingForItem == null)
			{
				return null;
			}
			string text = null;
			PlanNamedTableContext planNamedTableContext = dataBindingForItem.DataSetPlan.OutputTables[dataBindingForItem.OutputTableIndex];
			ResultSetReference resultSetReference = new ResultSetReference(dataBindingForItem.DataSetPlan, planNamedTableContext);
			DataTransformTable dataTransformTable;
			if (this.m_transformRestorationResult.TryGetTransformTableForResultSet(resultSetReference, out dataTransformTable))
			{
				this.m_idContext.TryGetId(dataTransformTable, out text);
			}
			else
			{
				this.m_idContext.TryGetId(planNamedTableContext, out text);
			}
			this.m_currentTableBinding = text;
			return new DataBinding
			{
				TableId = text
			};
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00034A94 File Offset: 0x00032C94
		protected override IList<DataSet> BuildDataSets(ExtensionSchema extensionSchema)
		{
			DataSet dataSet = new DataSet
			{
				Id = this.MakeDataSetId(this.m_queryGenerationResult.DataSetPlan),
				DataSourceId = this.m_dataSource.Id
			};
			BatchQueryTranslationResult batchQueryTranslationResult;
			try
			{
				batchQueryTranslationResult = this.m_queryGenerationResult.QueryDefinition.Translate(this.m_cancellationToken, this.m_useConceptualSchema);
			}
			catch (CommandTreeTranslationException ex)
			{
				DefinitionGenerationUtils.HandleCommandTreeTranslationException(ex, this.m_errorContext, dataSet.Id);
				throw;
			}
			dataSet.Query = batchQueryTranslationResult.CommandText;
			dataSet.ResultTables = this.BuildTables(batchQueryTranslationResult, this.m_queryGenerationResult.DataSetPlan);
			dataSet.QuerySourceMap = DefinitionGenerationUtils.BuildQuerySourceMap(batchQueryTranslationResult.QuerySourceMap);
			dataSet.QueryParameters = DefinitionGenerationUtils.BuildQueryParameters(this.m_queryGenerationResult.DataSetPlan.QueryParameters, this.m_queryGenerationResult.QueryParameterMap);
			return new List<DataSet> { dataSet };
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00034B78 File Offset: 0x00032D78
		protected override Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode BuildExpressionForCalculation(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Calculation calculation)
		{
			bool flag = this.m_annotations.CanBeHandledByProcessing(calculation);
			ExpressionTable expressionTable;
			DefinitionGenerationUtils.GetExpressionNode(calculation.Value.ExpressionId, this.m_finalExpressionTable, this.m_dataSetPlanningResult.ExpressionTable, flag, out expressionTable);
			return base.BuildExpression(calculation.Value.ExpressionId, expressionTable);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00034BC9 File Offset: 0x00032DC9
		protected override FieldValueExpressionNode BuildIntersectionCorrelation(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape)
		{
			if (this.m_dataSetPlanningResult.GetCorrelationForItem(dataShape) == null)
			{
				return null;
			}
			return base.BuildField(this.m_queryGenerationResult.GetIntersectionCorrelationExpressionId(dataShape), this.m_finalExpressionTable);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00034BF4 File Offset: 0x00032DF4
		protected override MatchCondition BuildMatchCondition(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember member)
		{
			BatchQueryMemberMatchConditions memberMatchConditions = this.m_queryGenerationResult.MemberMatchConditions;
			if (memberMatchConditions == null)
			{
				return null;
			}
			BatchQueryMemberMatchCondition batchQueryMemberMatchCondition;
			if (!memberMatchConditions.TryGetValue(member, out batchQueryMemberMatchCondition))
			{
				return null;
			}
			return new MatchCondition
			{
				Field = base.BuildField(new ExpressionId?(batchQueryMemberMatchCondition.ExpressionId), this.m_finalExpressionTable),
				Value = batchQueryMemberMatchCondition.MatchValue
			};
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00034C50 File Offset: 0x00032E50
		protected override DiscardCondition BuildDiscardCondition(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember member)
		{
			BatchQueryMemberDiscardConditions memberDiscardConditions = this.m_queryGenerationResult.MemberDiscardConditions;
			if (memberDiscardConditions == null)
			{
				return null;
			}
			BatchQueryMemberDiscardCondition batchQueryMemberDiscardCondition;
			if (!memberDiscardConditions.TryGetValue(member, out batchQueryMemberDiscardCondition))
			{
				return null;
			}
			return new DiscardCondition
			{
				Field = base.BuildField(new ExpressionId?(batchQueryMemberDiscardCondition.ExpressionId), this.m_finalExpressionTable),
				Value = batchQueryMemberDiscardCondition.MatchValue,
				Operator = this.GetDiscardConditionOperator(batchQueryMemberDiscardCondition.Operator)
			};
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00034CBB File Offset: 0x00032EBB
		private DiscardConditionComparisonOperator GetDiscardConditionOperator(BatchDiscardConditionOperator op)
		{
			if (op == BatchDiscardConditionOperator.NotEqual)
			{
				return DiscardConditionComparisonOperator.NotEqual;
			}
			Contract.RetailFail("Unrecognized BatchDiscardConditionOperator {0}", op);
			return DiscardConditionComparisonOperator.NotEqual;
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00034CD4 File Offset: 0x00032ED4
		protected override RestartKindDefinition BuildRestartKindDefinition(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dsqMember)
		{
			BatchQueryRestartIndicator restartIndicator = this.m_queryGenerationResult.RestartIndicator;
			if (restartIndicator == null || !restartIndicator.DataMembersToRestart.Contains(dsqMember))
			{
				return null;
			}
			return new RestartKindDefinition
			{
				RestartIndicator = base.BuildField(new ExpressionId?(restartIndicator.RestartIndicatorId), this.m_finalExpressionTable)
			};
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00034D22 File Offset: 0x00032F22
		protected override ExpressionTable GetExpressionTableForLimits()
		{
			return this.m_dataSetPlanningResult.ExpressionTable;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00034D2F File Offset: 0x00032F2F
		protected override ExpressionTable GetExpressionTableForLimitOverrides()
		{
			return this.m_finalExpressionTable;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00034D37 File Offset: 0x00032F37
		protected override IList<LimitOverride> GetLimitOverrides()
		{
			if (this.m_dataSetPlanningResult.LimitInfo == null)
			{
				return null;
			}
			return this.m_dataSetPlanningResult.LimitInfo.LimitOverrides;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00034D58 File Offset: 0x00032F58
		protected override IList<LimitTelemetryItem> GetLimitTelemetry()
		{
			if (this.m_dataSetPlanningResult.LimitInfo == null)
			{
				return null;
			}
			return this.m_dataSetPlanningResult.LimitInfo.TelemetryItems;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00034D79 File Offset: 0x00032F79
		protected override ExpressionTable GetExpressionTableForMember(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember member)
		{
			return this.m_finalExpressionTable;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00034D81 File Offset: 0x00032F81
		protected override ExpressionTable GetExpressionTableForTransforms()
		{
			return this.m_transformRestorationResult.TransformExpressionTable;
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00034D90 File Offset: 0x00032F90
		protected override Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode GetRestartExpressionForStatic(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember, ExpressionTable expressionTable, ObjectType objectType)
		{
			BatchQueryMemberMatchConditions memberMatchConditions = this.m_queryGenerationResult.MemberMatchConditions;
			Contract.RetailAssert(memberMatchConditions != null, "Missing match conditions");
			BatchQueryMemberMatchCondition batchQueryMemberMatchCondition = null;
			if (!memberMatchConditions.TryGetValue(dataMember, out batchQueryMemberMatchCondition))
			{
				Contract.RetailFail("Expected to find a match condition for the subtotal");
			}
			return base.BuildExpression(new ExpressionId?(batchQueryMemberMatchCondition.ExpressionId), expressionTable);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00034DE0 File Offset: 0x00032FE0
		protected override string GetDataSetTableId(DataSetFieldReferenceExpressionNode node)
		{
			string text;
			if (!this.m_idContext.TryGetId(node.TablePlan, out text))
			{
				Contract.RetailFail("Missing Id for data set table");
			}
			return text;
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00034E10 File Offset: 0x00033010
		protected override bool HasReusableSecondary(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape)
		{
			if (this.m_scopeTree.GetParentScope(dataShape) == null)
			{
				return false;
			}
			return (from s in dataShape.SecondaryHierarchy.GetAllMembersDepthFirst()
				select this.m_dataSetPlanningResult.GetDataBindingForItem(s)).Any((BatchDataBinding b) => b != null && b.DataSetPlan.OutputTables[b.OutputTableIndex].IsReusable);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00034E70 File Offset: 0x00033070
		private IList<ResultTable> BuildTables(BatchQueryTranslationResult queryGenResult, BatchDataSetPlan plan)
		{
			ReadOnlyCollection<BatchQueryTranslationTableResult> tables = queryGenResult.Tables;
			IReadOnlyList<PlanNamedTableContext> outputTables = plan.OutputTables;
			List<ResultTable> list = new List<ResultTable>();
			foreach (Tuple<BatchQueryTranslationTableResult, PlanNamedTableContext> tuple in this.CorrelateByPosition<BatchQueryTranslationTableResult, PlanNamedTableContext>(tables, outputTables))
			{
				list.Add(this.BuildTable(tuple));
			}
			return list;
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00034EDC File Offset: 0x000330DC
		private IEnumerable<Tuple<T1, T2>> CorrelateByPosition<T1, T2>(IEnumerable<T1> sequence1, IEnumerable<T2> sequence2)
		{
			IEnumerator<T1> seq1Enumerator = sequence1.GetEnumerator();
			IEnumerator<T2> seq2Enumerator = sequence2.GetEnumerator();
			while (seq1Enumerator.MoveNext() && seq2Enumerator.MoveNext())
			{
				yield return new Tuple<T1, T2>(seq1Enumerator.Current, seq2Enumerator.Current);
			}
			if (seq1Enumerator.MoveNext())
			{
				throw new InvalidOperationException("Too many items in sequence 1");
			}
			if (seq2Enumerator.MoveNext())
			{
				throw new InvalidOperationException("Too many items in sequence 2");
			}
			yield break;
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00034EF4 File Offset: 0x000330F4
		private ResultTable BuildTable(Tuple<BatchQueryTranslationTableResult, PlanNamedTableContext> tableInput)
		{
			PlanNamedTableContext item = tableInput.Item2;
			ResultTable resultTable = new ResultTable
			{
				Id = this.m_idContext.MakeUniqueId(item.Name, item),
				Fields = new List<Field>(),
				IsReusable = item.IsReusable
			};
			foreach (QueryResultField queryResultField in tableInput.Item1.ResultFields)
			{
				resultTable.Fields.Add(DsdDataSetGenerator.CreateField(queryResultField, this.m_generateComposableQueryColumnNames));
			}
			return resultTable;
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00034F94 File Offset: 0x00033194
		private string MakeDataSetId(BatchDataSetPlan dataSetPlan)
		{
			return this.m_idContext.MakeUniqueId(dataSetPlan.Name + "DataSet", dataSetPlan);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00034FB4 File Offset: 0x000331B4
		protected override IList<string> BuildSegmentationTableIds(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape)
		{
			IEnumerable<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember> segmentationMembers = dataShape.GetSegmentationMembers(this.m_annotations.SubtotalAnnotations);
			if (segmentationMembers == null || !segmentationMembers.Any<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember>())
			{
				return null;
			}
			List<string> list = new List<string>();
			foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember in segmentationMembers)
			{
				string text;
				if (this.m_segmentationItemsToTableMapping.TryGetValue(dataMember, out text) && !list.Contains(text))
				{
					list.Add(text);
				}
				bool flag = text != null;
				string text2 = "Could not find binding for member ";
				Identifier id = dataMember.Id;
				Contract.RetailAssert(flag, text2 + ((id != null) ? id.ToString() : null));
			}
			Contract.RetailAssert(list.Count > 0, "Expected to find table ids for segmentation");
			return list;
		}

		// Token: 0x04000654 RID: 1620
		private readonly BatchDataSetPlanningResult m_dataSetPlanningResult;

		// Token: 0x04000655 RID: 1621
		private readonly BatchQueryGenerationResult m_queryGenerationResult;

		// Token: 0x04000656 RID: 1622
		private readonly DataTransformRestorationResult m_transformRestorationResult;

		// Token: 0x04000657 RID: 1623
		private readonly ExpressionTable m_finalExpressionTable;

		// Token: 0x04000658 RID: 1624
		private readonly bool m_generateComposableQueryColumnNames;

		// Token: 0x04000659 RID: 1625
		private readonly CancellationToken m_cancellationToken;

		// Token: 0x0400065A RID: 1626
		private readonly bool m_disableDictionaryEncoding;
	}
}

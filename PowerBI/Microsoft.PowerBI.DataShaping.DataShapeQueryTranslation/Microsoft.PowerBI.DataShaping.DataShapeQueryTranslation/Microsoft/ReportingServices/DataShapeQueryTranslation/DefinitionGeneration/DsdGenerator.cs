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
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDefinitionGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataTransformBypass;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DefinitionGeneration
{
	// Token: 0x020000BB RID: 187
	internal sealed class DsdGenerator : DsdGeneratorBase
	{
		// Token: 0x060007F7 RID: 2039 RVA: 0x0001EB50 File Offset: 0x0001CD50
		private DsdGenerator(DataSourceContext dataSourceContext, DataSetPlanningResult dataSetPlanningResult, ReadOnlyCollection<QueryGenerationResult> queryGenerationResults, IFeatureSwitchProvider featureSwitchProvider, TranslationErrorContext errorContext, ScopeTree scopeTree, ExpressionTable expressionTable, DataShapeAnnotations annotations, DataTransformRestorationResult transformRestorationResult, CancellationToken cancellationToken, bool applyTransformsInQuery, bool disableDictionaryEncoding, bool useConceptualSchema)
			: base(annotations, scopeTree, errorContext, dataSourceContext, applyTransformsInQuery, useConceptualSchema)
		{
			this.m_expressionTable = expressionTable;
			this.m_dataSetPlanningResult = dataSetPlanningResult;
			this.m_queryGenerationResults = queryGenerationResults;
			this.m_featureSwitchProvider = featureSwitchProvider;
			this.m_transformRestorationResult = transformRestorationResult;
			this.m_cancellationToken = cancellationToken;
			this.m_disableDictionaryEncoding = disableDictionaryEncoding;
			this.m_usedIds = new HashSet<string>(DsdGeneratorBase.DataShapeDefinitionIdComparer);
			this.m_dataSetIds = this.BuildIdsCollection(new Func<string, string>(this.MakeDataSetId));
			this.m_tableIds = this.BuildIdsCollection(new Func<string, string>(this.MakeTableId));
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001EBE4 File Offset: 0x0001CDE4
		public static DataShapeDefinition Generate(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dsqDataShape, DataSourceContext dataSourceContext, DataSetPlanningResult dataSetPlanningResult, IFeatureSwitchProvider featureSwitchProvider, ReadOnlyCollection<QueryGenerationResult> queryGenerationResults, TranslationErrorContext errorContext, ScopeTree scopeTree, ExpressionTable expressionTable, DataShapeAnnotations annotations, DataTransformRestorationResult transformRestorationResult, CancellationToken cancellationToken, bool applyTransformsInQuery, bool disableDictionaryEncoding)
		{
			DataShapeDefinition dataShapeDefinition;
			try
			{
				dataShapeDefinition = new DsdGenerator(dataSourceContext, dataSetPlanningResult, queryGenerationResults, featureSwitchProvider, errorContext, scopeTree, expressionTable, annotations, transformRestorationResult, cancellationToken, applyTransformsInQuery, disableDictionaryEncoding, featureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema)).Generate(dsqDataShape);
			}
			catch (DefinitionGenerationException)
			{
				dataShapeDefinition = null;
			}
			return dataShapeDefinition;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001EC34 File Offset: 0x0001CE34
		protected override Collation BuildCollation(DataSourceContext dataSourceContext)
		{
			bool flag;
			Collation collation = base.BuildCollationBasicProperties(dataSourceContext, out flag);
			collation.UseOrdinalStringKeyGeneration = flag;
			return collation;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001EC54 File Offset: 0x0001CE54
		protected override IList<DataSet> BuildDataSets(ExtensionSchema extensionSchema)
		{
			List<DataSet> list = new List<DataSet>(this.m_queryGenerationResults.Count);
			for (int i = 0; i < this.m_queryGenerationResults.Count; i++)
			{
				QueryGenerationResult queryGenerationResult = this.m_queryGenerationResults[i];
				string dataSetId = this.GetDataSetId(queryGenerationResult.DataSetPlan);
				string tableId = this.GetTableId(queryGenerationResult.DataSetPlan);
				IList<QueryParameter> list2 = DefinitionGenerationUtils.BuildQueryParameters(queryGenerationResult.DataSetPlan.QueryParameters, queryGenerationResult.QueryParameterMap);
				DataSet dataSet = DsdDataSetGenerator.CreateDataSet(base.GetDataSourceId(), dataSetId, tableId, this.m_errorContext, queryGenerationResult.QueryDefinition, queryGenerationResult.QueryTrimmer, this.m_dataSetPlanningResult.IsReusableBinding(i), extensionSchema, this.m_featureSwitchProvider, this.m_cancellationToken, list2);
				list.Add(dataSet);
			}
			return list.AsReadOnly();
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001ED1C File Offset: 0x0001CF1C
		protected override Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataBinding BuildDataBinding(IDataBoundItem owner)
		{
			Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning.DataBinding dataBindingForItem = this.m_dataSetPlanningResult.GetDataBindingForItem(owner);
			if (dataBindingForItem == null)
			{
				return null;
			}
			int dataSetPlanIndex = dataBindingForItem.DataSetPlanIndex;
			ResultSetReference resultSetReference = new ResultSetReference(this.m_dataSetPlanningResult.DataSetPlans[dataSetPlanIndex], null);
			DataTransformTable dataTransformTable;
			string text;
			if (this.m_transformRestorationResult.TryGetTransformTableForResultSet(resultSetReference, out dataTransformTable))
			{
				text = base.GetTransformTableId(dataTransformTable);
			}
			else
			{
				text = this.GetTableId(dataSetPlanIndex);
			}
			this.m_currentTableBinding = text;
			IList<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Relationship> list = null;
			if (dataBindingForItem.Relationship != null)
			{
				List<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.JoinCondition> list2 = this.BuildJoinConditions(dataBindingForItem, dataSetPlanIndex);
				if (list2 != null)
				{
					Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Relationship relationship = new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Relationship
					{
						ParentScope = dataBindingForItem.Relationship.ParentScope.Id.Value,
						JoinConditions = list2
					};
					list = new List<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Relationship>(1);
					list.Add(relationship);
				}
			}
			return new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataBinding
			{
				TableId = text,
				Relationships = list,
				RestoreContext = dataBindingForItem.ShouldRestoreContext
			};
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001EDF8 File Offset: 0x0001CFF8
		protected override FieldValueExpressionNode BuildIntersectionCorrelation(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape)
		{
			return null;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001EDFC File Offset: 0x0001CFFC
		protected override Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode BuildExpressionForCalculation(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Calculation calculation)
		{
			IScope containingScope = this.m_scopeTree.GetContainingScope(calculation);
			Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning.DataBinding dataBinding = this.FindBindingForScope(containingScope);
			Contract.RetailAssert(dataBinding != null, "At least one parent scope must have a data binding");
			this.m_dataSetPlanningResult.GetDataSetPlan(dataBinding.DataSetPlanIndex);
			bool flag = this.m_annotations.CanBeHandledByProcessing(calculation);
			ExpressionTable expressionTable;
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.ExpressionNode expressionNode = DefinitionGenerationUtils.GetExpressionNode(calculation.Value.ExpressionId, this.GetExpressionTableForItem(calculation), this.m_dataSetPlanningResult.ExpressionTable, flag, out expressionTable);
			return base.BuildExpression(expressionNode, expressionTable);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001EE7C File Offset: 0x0001D07C
		protected override MatchCondition BuildMatchCondition(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember)
		{
			Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning.DataBinding dataBindingForItem = this.m_dataSetPlanningResult.GetDataBindingForItem(dataMember);
			string text;
			QueryGenerationResult queryGenerationResult;
			if (dataBindingForItem == null)
			{
				text = this.m_currentTableBinding;
				queryGenerationResult = this.GetQueryGenerationResultForTableId(text);
			}
			else
			{
				queryGenerationResult = this.GetQueryGenerationResultForDataSetPlanIndex(dataBindingForItem.DataSetPlanIndex);
				text = this.GetTableId(dataBindingForItem.DataSetPlanIndex);
			}
			if (queryGenerationResult.AggregateIndicatorFieldNames.Count == 0)
			{
				return null;
			}
			bool flag = false;
			string text2 = null;
			BatchSubtotalAnnotation batchSubtotalAnnotation;
			if (dataMember.IsDynamic)
			{
				if (this.m_annotations.TryGetBatchSubtotalAnnotation(dataMember, out batchSubtotalAnnotation) && batchSubtotalAnnotation.Usage.IsIncludeInOutput())
				{
					queryGenerationResult.AggregateIndicatorFieldNames.TryGetValue(dataMember, out text2);
				}
			}
			else if (this.m_annotations.TryGetBatchSubtotalSourceAnnotation(dataMember, out batchSubtotalAnnotation))
			{
				Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember2 = batchSubtotalAnnotation.StopScope as Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember;
				if (dataMember2 != null)
				{
					queryGenerationResult.AggregateIndicatorFieldNames.TryGetValue(dataMember2, out text2);
					flag = true;
				}
			}
			if (text2 == null)
			{
				return null;
			}
			return new MatchCondition
			{
				Field = new FieldValueExpressionNode
				{
					TableId = text,
					FieldId = text2
				},
				Value = flag
			};
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001EF73 File Offset: 0x0001D173
		protected override DiscardCondition BuildDiscardCondition(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember)
		{
			return null;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001EF76 File Offset: 0x0001D176
		protected override RestartKindDefinition BuildRestartKindDefinition(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dsqMember)
		{
			return null;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001EF79 File Offset: 0x0001D179
		protected override ResultEncodingHints BuildResultEncodingHints()
		{
			if (!this.m_disableDictionaryEncoding)
			{
				return null;
			}
			return new ResultEncodingHints
			{
				DisableDictionaryEncoding = this.m_disableDictionaryEncoding
			};
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001EF96 File Offset: 0x0001D196
		protected override Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode GetRestartExpressionForStatic(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember, ExpressionTable expressionTable, ObjectType objectType)
		{
			if (objectType == ObjectType.StartPosition)
			{
				return null;
			}
			return new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.LiteralExpressionNode
			{
				Value = false
			};
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
		protected override ExpressionTable GetExpressionTableForMember(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember member)
		{
			if (!member.IsDynamic)
			{
				return null;
			}
			return this.GetExpressionTableForItem(member);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001EFC3 File Offset: 0x0001D1C3
		protected override string GetDataSetTableId(DataSetFieldReferenceExpressionNode exprNode)
		{
			return this.GetTableId(exprNode.DataSetPlan);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001EFD1 File Offset: 0x0001D1D1
		protected override ExpressionTable GetExpressionTableForLimits()
		{
			return this.m_expressionTable;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001EFD9 File Offset: 0x0001D1D9
		protected override ExpressionTable GetExpressionTableForLimitOverrides()
		{
			return null;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001EFDC File Offset: 0x0001D1DC
		protected override IList<LimitOverride> GetLimitOverrides()
		{
			return null;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001EFDF File Offset: 0x0001D1DF
		protected override IList<LimitTelemetryItem> GetLimitTelemetry()
		{
			return null;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001EFE2 File Offset: 0x0001D1E2
		protected override bool HasReusableSecondary(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape)
		{
			return false;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001EFE8 File Offset: 0x0001D1E8
		protected override IList<string> BuildSegmentationTableIds(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape)
		{
			if (dataShape.PrimaryHierarchy == null || dataShape.PrimaryHierarchy.DataMembers == null)
			{
				return null;
			}
			IEnumerable<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember> enumerable = from m in dataShape.PrimaryHierarchy.GetAllDynamicMembers()
				where m.ParticipatesInWindowing(this.m_annotations.SubtotalAnnotations)
				select m;
			List<string> list = null;
			foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember in enumerable)
			{
				string text;
				if (this.m_segmentationItemsToTableMapping.TryGetValue(dataMember, out text) && (list == null || !list.Contains(text)))
				{
					Util.AddToLazyList<string>(ref list, text);
				}
				bool flag = text != null;
				string text2 = "Could not find binding for member ";
				Identifier id = dataMember.Id;
				Contract.RetailAssert(flag, text2 + ((id != null) ? id.ToString() : null));
			}
			return list;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001F0A8 File Offset: 0x0001D2A8
		protected override CorrelationMode? BuildCorrelationMode(Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataShape dataShape)
		{
			if (dataShape.SecondaryHierarchy.HasDynamic() && dataShape.HasPrimaryMembers() && this.m_dataSetPlanningResult.HasReusableBinding())
			{
				return new CorrelationMode?(CorrelationMode.ValueBased);
			}
			return null;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001F0E7 File Offset: 0x0001D2E7
		protected override ExpressionTable GetExpressionTableForTransforms()
		{
			return this.m_transformRestorationResult.TransformExpressionTable;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001F0F4 File Offset: 0x0001D2F4
		private List<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.JoinCondition> BuildJoinConditions(Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning.DataBinding dsqDataBinding, int dataSetPlanIndex)
		{
			int targetDataSetPlanIndex = dsqDataBinding.Relationship.TargetDataSetPlanIndex;
			QueryGenerationResult queryGenerationResultForDataSetPlanIndex = this.GetQueryGenerationResultForDataSetPlanIndex(targetDataSetPlanIndex);
			ReadOnlyDictionary<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember, string> aggregateIndicatorFieldNames = queryGenerationResultForDataSetPlanIndex.AggregateIndicatorFieldNames;
			ReadOnlyCollection<Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning.JoinCondition> joinConditions = dsqDataBinding.Relationship.JoinConditions;
			if (joinConditions == null)
			{
				return null;
			}
			List<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.JoinCondition> list = new List<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.JoinCondition>(joinConditions.Count);
			QueryGenerationResult queryGenerationResultForDataSetPlanIndex2 = this.GetQueryGenerationResultForDataSetPlanIndex(dataSetPlanIndex);
			ReadOnlyDictionary<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember, string> aggregateIndicatorFieldNames2 = queryGenerationResultForDataSetPlanIndex2.AggregateIndicatorFieldNames;
			foreach (Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning.JoinCondition joinCondition in joinConditions)
			{
				Microsoft.DataShaping.InternalContracts.DataShapeDefinition.JoinCondition joinCondition2 = this.CreateAggregateIndicatorJoinCondition(joinCondition, aggregateIndicatorFieldNames2, dataSetPlanIndex);
				if (joinCondition2 != null)
				{
					list.Add(joinCondition2);
				}
				if (!joinCondition.AggregateIndicatorJoinConditionOnly)
				{
					Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember = joinCondition.DataMember;
					bool requiresReversedSortDirections = joinCondition.RequiresReversedSortDirections;
					List<SortKey> sortKeys = dataMember.Group.SortKeys;
					Contract.RetailAssert(sortKeys != null, "Missing SortKeys collection on group");
					for (int i = 0; i < sortKeys.Count; i++)
					{
						SortKey sortKey = sortKeys[i];
						Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode expressionNode = base.BuildExpression(sortKey.Value.ExpressionId, queryGenerationResultForDataSetPlanIndex.ExpressionTable);
						Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode expressionNode2 = base.BuildExpression(sortKey.Value.ExpressionId, queryGenerationResultForDataSetPlanIndex2.ExpressionTable);
						Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortDirection sortDirection = sortKey.SortDirection.Value;
						if (requiresReversedSortDirections)
						{
							sortDirection = sortDirection.ReverseSortDirection();
						}
						list.Add(new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.JoinCondition
						{
							PrimaryKey = expressionNode,
							SecondaryKey = expressionNode2,
							SortDirection = base.ConvertSortDirection(sortDirection)
						});
					}
				}
			}
			return list;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001F288 File Offset: 0x0001D488
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.JoinCondition CreateAggregateIndicatorJoinCondition(Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning.JoinCondition dsqJoinCondition, ReadOnlyDictionary<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember, string> aggregateIndicatorFieldMap, int dataSetPlanIndex)
		{
			string text;
			if (aggregateIndicatorFieldMap != null && aggregateIndicatorFieldMap.TryGetValue(dsqJoinCondition.DataMember, out text))
			{
				FieldValueExpressionNode fieldValueExpressionNode = new FieldValueExpressionNode
				{
					FieldId = text,
					TableId = this.GetTableId(dataSetPlanIndex)
				};
				Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.LiteralExpressionNode literalExpressionNode = new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.LiteralExpressionNode
				{
					Value = dsqJoinCondition.AggregateIndicatorJoinConditionOnly
				};
				DataMemberPlanElement dataMemberPlanElement = (DataMemberPlanElement)this.m_dataSetPlanningResult.GetDataSetPlan(dataSetPlanIndex).Scopes.Where((ScopePlanElement s) => s.Scope == dsqJoinCondition.DataMember).FirstOrDefault<ScopePlanElement>();
				return new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.JoinCondition
				{
					PrimaryKey = literalExpressionNode,
					SecondaryKey = fieldValueExpressionNode,
					SortDirection = base.ConvertSortDirection(dataMemberPlanElement.RollupInfo.SortDirection)
				};
			}
			return null;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001F354 File Offset: 0x0001D554
		private Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning.DataBinding FindBindingForScope(IScope containingScope)
		{
			Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning.DataBinding effectiveDataBinding = null;
			this.m_scopeTree.TraverseUp(containingScope, delegate(IScope scope)
			{
				effectiveDataBinding = this.m_dataSetPlanningResult.GetDataBindingForItem(scope);
				return effectiveDataBinding == null;
			});
			return effectiveDataBinding;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001F394 File Offset: 0x0001D594
		private string MakeDataSetId(string dataSetPlanName)
		{
			return this.m_idContext.MakeUniqueId(dataSetPlanName + "DataSet");
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001F3AC File Offset: 0x0001D5AC
		private string MakeTableId(string dataSetPlanName)
		{
			return this.m_idContext.MakeUniqueId(dataSetPlanName + "Table");
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001F3C4 File Offset: 0x0001D5C4
		private ExpressionTable GetExpressionTableForItem(IContextItem item)
		{
			int planIndex = this.GetQueryGenerationResultForItem(item).DataSetPlan.PlanIndex;
			return this.m_transformRestorationResult.ExpressionTables[planIndex];
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001F3F4 File Offset: 0x0001D5F4
		private QueryGenerationResult GetQueryGenerationResultForItem(IContextItem item)
		{
			DataSetPlan outputDataSetPlanForItem = this.m_dataSetPlanningResult.GetOutputDataSetPlanForItem(item);
			return this.GetQueryGenerationResultForDataSetPlan(outputDataSetPlanForItem);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001F415 File Offset: 0x0001D615
		private QueryGenerationResult GetQueryGenerationResultForDataSetPlan(DataSetPlan dataSetPlan)
		{
			return this.GetQueryGenerationResultForDataSetPlanIndex(dataSetPlan.PlanIndex);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001F423 File Offset: 0x0001D623
		private QueryGenerationResult GetQueryGenerationResultForDataSetPlanIndex(int dataSetPlanIndex)
		{
			return this.m_queryGenerationResults[dataSetPlanIndex];
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001F434 File Offset: 0x0001D634
		private QueryGenerationResult GetQueryGenerationResultForTableId(string tableId)
		{
			int num = this.m_tableIds.IndexOf(tableId);
			if (num >= 0)
			{
				return this.GetQueryGenerationResultForDataSetPlanIndex(num);
			}
			foreach (KeyValuePair<ResultSetReference, DataTransformTable> keyValuePair in this.m_transformRestorationResult.ResultSetToTableMapping)
			{
				DataTransformTable value = keyValuePair.Value;
				if (base.GetTransformTableId(value) == tableId)
				{
					num = keyValuePair.Key.DataSetPlan.PlanIndex;
					return this.GetQueryGenerationResultForDataSetPlanIndex(num);
				}
			}
			Contract.RetailFail("Could not find result set for tableId {0}", tableId);
			throw new InvalidOperationException();
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001F4E0 File Offset: 0x0001D6E0
		private ReadOnlyCollection<string> BuildIdsCollection(Func<string, string> makeId)
		{
			List<string> list = new List<string>(this.m_queryGenerationResults.Count);
			for (int i = 0; i < this.m_queryGenerationResults.Count; i++)
			{
				QueryGenerationResult queryGenerationResult = this.m_queryGenerationResults[i];
				string text = makeId(queryGenerationResult.DataSetPlan.Name);
				list.Add(text);
			}
			return list.AsReadOnly();
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001F540 File Offset: 0x0001D740
		private string GetTableId(IDataSetPlan dataSetPlan)
		{
			return this.GetTableId(dataSetPlan.PlanIndex);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001F54E File Offset: 0x0001D74E
		private string GetTableId(int dataSetPlanIndex)
		{
			return this.m_tableIds[dataSetPlanIndex];
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001F55C File Offset: 0x0001D75C
		private string GetDataSetId(IDataSetPlan dataSetPlan)
		{
			return this.GetDataSetId(dataSetPlan.PlanIndex);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001F56A File Offset: 0x0001D76A
		private string GetDataSetId(int dataSetPlanIndex)
		{
			return this.m_dataSetIds[dataSetPlanIndex];
		}

		// Token: 0x040003F8 RID: 1016
		private const string TableSuffix = "Table";

		// Token: 0x040003F9 RID: 1017
		private readonly ReadOnlyCollection<string> m_dataSetIds;

		// Token: 0x040003FA RID: 1018
		private readonly ReadOnlyCollection<string> m_tableIds;

		// Token: 0x040003FB RID: 1019
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x040003FC RID: 1020
		private readonly DataSetPlanningResult m_dataSetPlanningResult;

		// Token: 0x040003FD RID: 1021
		private readonly ReadOnlyCollection<QueryGenerationResult> m_queryGenerationResults;

		// Token: 0x040003FE RID: 1022
		private readonly IFeatureSwitchProvider m_featureSwitchProvider;

		// Token: 0x040003FF RID: 1023
		private readonly DataTransformRestorationResult m_transformRestorationResult;

		// Token: 0x04000400 RID: 1024
		private readonly CancellationToken m_cancellationToken;

		// Token: 0x04000401 RID: 1025
		private readonly bool m_disableDictionaryEncoding;

		// Token: 0x04000402 RID: 1026
		private readonly HashSet<string> m_usedIds;
	}
}

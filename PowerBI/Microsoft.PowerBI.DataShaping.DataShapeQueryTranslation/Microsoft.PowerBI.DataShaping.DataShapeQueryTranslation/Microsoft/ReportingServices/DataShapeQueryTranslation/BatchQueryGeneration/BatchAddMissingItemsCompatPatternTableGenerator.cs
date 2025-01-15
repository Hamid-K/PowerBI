using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200012D RID: 301
	internal sealed class BatchAddMissingItemsCompatPatternTableGenerator
	{
		// Token: 0x06000B49 RID: 2889 RVA: 0x0002C1C1 File Offset: 0x0002A3C1
		private BatchAddMissingItemsCompatPatternTableGenerator(BatchQueryGenerationContext context, GeneratedDeclarationCollection declarations, IQueryExpressionGenerator expressionGenerator)
		{
			this.m_context = context;
			this.m_declarations = declarations;
			this.m_expressionGenerator = expressionGenerator;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0002C1DE File Offset: 0x0002A3DE
		public static GeneratedTable Generate(PlanOperationAddMissingItemsCompatPattern operation, BatchQueryGenerationContext context, GeneratedDeclarationCollection declarations, IQueryExpressionGenerator expressionGenerator)
		{
			return new BatchAddMissingItemsCompatPatternTableGenerator(context, declarations, expressionGenerator).Generate(operation);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0002C1F0 File Offset: 0x0002A3F0
		private GeneratedTable Generate(PlanOperationAddMissingItemsCompatPattern operation)
		{
			WritableGeneratedColumnMap writableGeneratedColumnMap = new WritableGeneratedColumnMap();
			Dictionary<string, QueryTableColumn> dictionary = new Dictionary<string, QueryTableColumn>();
			FieldRelationshipAnnotations defaultFieldRelationshipAnnotations = this.m_context.Schema.GetDefaultFieldRelationshipAnnotations();
			ColumnGroupingAnnotations defaultColumnGroupingAnnotations = this.m_context.Schema.GetDefaultColumnGroupingAnnotations();
			FederatedEntityDataModel model = this.m_context.Model;
			EntityDataModel entityDataModel = ((model != null) ? model.BaseModel : null);
			IConceptualSchema defaultSchema = this.m_context.Schema.GetDefaultSchema();
			DefaultContextManager defaultContextManager = new DefaultContextManager(defaultFieldRelationshipAnnotations, defaultColumnGroupingAnnotations);
			QueryBuilder queryBuilder = new QueryBuilder(entityDataModel, defaultSchema, this.m_context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema), defaultContextManager, true, true, null);
			List<GroupReference> list = new List<GroupReference>();
			bool flag = true;
			foreach (PlanGroupByMember planGroupByMember in operation.Members)
			{
				foreach (GroupCluster groupCluster in GroupClusterBuilder.Build(planGroupByMember.Member.Group, this.m_context.ExpressionTable))
				{
					GroupReference groupReference = this.AddGroupClusterToQuery(queryBuilder, writableGeneratedColumnMap, dictionary, planGroupByMember.Member, groupCluster);
					list.Add(groupReference);
					flag &= groupCluster.ShowItemsWithNoData;
				}
				this.AddSortKeys(queryBuilder, planGroupByMember.Member, list, writableGeneratedColumnMap, dictionary);
			}
			if (operation.MeasureJoinConstraints != null)
			{
				QueryConstraintValidator queryConstraintValidator;
				if (!flag)
				{
					queryConstraintValidator = null;
				}
				else
				{
					FederatedEntityDataModel model2 = this.m_context.Model;
					queryConstraintValidator = new QueryConstraintValidator((model2 != null) ? model2.BaseModel : null, this.m_context.Schema.GetDefaultSchema(), list.Select((GroupReference gr) => gr.Group), Microsoft.DataShaping.Util.EmptyReadOnlyCollection<Measure>(), null, false, true, this.m_context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema));
				}
				QueryConstraintValidator queryConstraintValidator2 = queryConstraintValidator;
				List<IJoinPredicate> list2 = new List<IJoinPredicate>();
				foreach (Calculation calculation in operation.MeasureJoinConstraints)
				{
					this.AddCalculation(queryBuilder, list, list2, calculation, queryConstraintValidator2);
				}
				if (!list2.IsNullOrEmpty<IJoinPredicate>())
				{
					queryBuilder.SetJoinPredicates(list2);
				}
			}
			if (operation.AllowBlankRow)
			{
				queryBuilder.SetAllowBlankRow();
			}
			QueryExpression query = queryBuilder.GetQueryDefinition(false).ToQueryCommandTree(new QdmTranslationSettings(true, false, false), this.m_context.FeatureSwitchProvider, this.m_context.CancellationToken, null).Query;
			IReadOnlyList<ConceptualTypeColumn> columns = ((ConceptualTableType)query.ConceptualResultType).RowType.Columns;
			List<QueryTableColumn> list3 = new List<QueryTableColumn>(columns.Count);
			foreach (ConceptualTypeColumn conceptualTypeColumn in columns)
			{
				QueryTableColumn queryTableColumn;
				if (!dictionary.TryGetValue(conceptualTypeColumn.Name, out queryTableColumn))
				{
					Microsoft.DataShaping.Contract.RetailFail("Missing QueryTableColumn for field");
				}
				list3.Add(queryTableColumn);
			}
			QueryTableDefinition queryTableDefinition = new QueryTableDefinition(list3, query, QdmNames.GenerateAll(null));
			if (operation.ContextTables.IsNullOrEmpty<PlanOperation>())
			{
				return new GeneratedTable(queryTableDefinition, writableGeneratedColumnMap);
			}
			IEnumerable<QueryExpression> enumerable = from pair in BatchQueryGenerationUtils.GenerateContextTables(operation.ContextTables, this.m_declarations, this.m_context, this.m_expressionGenerator, new BatchQueryExpressionReferenceContext())
				select pair.Item1.Expression;
			return new GeneratedTable(queryTableDefinition.Calculate(enumerable), writableGeneratedColumnMap);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002C578 File Offset: 0x0002A778
		private void AddSortKeys(QueryBuilder queryBuilder, DataMember dataMember, List<GroupReference> qdmGroupRefs, WritableGeneratedColumnMap columnMap, Dictionary<string, QueryTableColumn> columnsByName)
		{
			List<SortKey> sortKeys = dataMember.Group.SortKeys;
			Microsoft.DataShaping.Contract.RetailAssert(dataMember.Group.ScopeIdDefinition == null, "ScopeIdDefinition is not supported for GenerateAll plan operation.");
			ExpressionContext expressionContext = new ExpressionContext(this.m_context.ErrorContext, ObjectType.SortKey, dataMember.Id, "Value");
			if (sortKeys != null)
			{
				SortByMeasureInfoCollection sortByMeasureInfos = this.m_context.Annotations.DataMemberAnnotations.GetSortByMeasureInfos(dataMember);
				for (int i = 0; i < sortKeys.Count; i++)
				{
					SortKey sortKey = sortKeys[i];
					if (sortByMeasureInfos == null || !sortByMeasureInfos.ContainsKey(sortKey))
					{
						ExpressionId value = sortKey.Value.ExpressionId.Value;
						QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(value, expressionContext);
						string text = this.AddGroupDetailToQuery(queryBuilder, qdmGroupRefs, queryExpressionContext.QueryExpression, dataMember.Id.Value);
						this.AddColumn(columnMap, columnsByName, new QueryTableColumn(text, queryExpressionContext.QueryExpression), sortKey.Value.ExpressionId.Value);
					}
				}
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002C684 File Offset: 0x0002A884
		private void AddColumn(WritableGeneratedColumnMap columnMap, Dictionary<string, QueryTableColumn> columnsByName, QueryTableColumn column, ExpressionId exprId)
		{
			QueryTableColumn queryTableColumn;
			if (columnsByName.TryGetValue(column.Name, out queryTableColumn))
			{
				columnMap.Add(exprId, queryTableColumn);
				return;
			}
			columnMap.Add(exprId, column);
			columnsByName.Add(column.Name, column);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002C6C4 File Offset: 0x0002A8C4
		private string AddGroupDetailToQuery(QueryBuilder queryBuilder, List<GroupReference> qdmGroupRefs, QueryExpression queryExpression, string fallbackCandidateName)
		{
			GroupReference groupReference;
			if (!queryBuilder.TryFindInnermostCompatibleGroupReference(qdmGroupRefs, queryExpression, out groupReference))
			{
				Microsoft.DataShaping.Contract.RetailFail("Invalid detail expression");
			}
			return queryBuilder.AddGroupDetailToQuery(groupReference, queryExpression, fallbackCandidateName);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002C6F4 File Offset: 0x0002A8F4
		private void AddCalculation(QueryBuilder queryBuilder, List<GroupReference> qdmGroupRefs, List<IJoinPredicate> measureJoinPredicates, Calculation calculation, QueryConstraintValidator constraintValidator)
		{
			List<KeyValuePair<ExpressionId, QueryExpressionContext>> list = this.m_expressionGenerator.TranslateCalculation(calculation);
			for (int i = 0; i < list.Count; i++)
			{
				QueryExpressionContext value = list[i].Value;
				QueryExpression queryExpression = value.QueryExpression;
				if (value.CalculateAsMeasure)
				{
					if (constraintValidator == null || constraintValidator.IsMeasureExpressionUnconstrained(queryExpression))
					{
						IJoinPredicate joinPredicate = JoinPredicates.CreateJoinPredicateForMeasureExpression(queryExpression);
						measureJoinPredicates.Add(joinPredicate);
					}
				}
				else
				{
					this.AddGroupDetailToQuery(queryBuilder, qdmGroupRefs, queryExpression, calculation.Id.Value);
				}
			}
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0002C778 File Offset: 0x0002A978
		private GroupReference AddGroupClusterToQuery(QueryBuilder queryBuilder, WritableGeneratedColumnMap columnMap, Dictionary<string, QueryTableColumn> columnsByName, DataMember dataMember, GroupCluster groupCluster)
		{
			List<QueryExpression> list = new List<QueryExpression>(groupCluster.GroupKeys.Count);
			foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey groupKey in groupCluster.GroupKeys)
			{
				string text;
				QueryExpression queryExpression = QueryBuilderExtensions.AddGroupKeyToQuery(queryBuilder, this.m_expressionGenerator, this.m_context.ErrorContext, groupKey, dataMember.Id, out text);
				list.Add(queryExpression);
				this.AddColumn(columnMap, columnsByName, new QueryTableColumn(text, queryExpression), groupKey.Value.ExpressionId.Value);
			}
			FollowingJoinBehavior followingJoinBehavior = (groupCluster.ShowItemsWithNoData ? FollowingJoinBehavior.OuterJoin : FollowingJoinBehavior.InnerJoin);
			return queryBuilder.AddOrReuseGroup(list, null, null, false, followingJoinBehavior);
		}

		// Token: 0x040005B4 RID: 1460
		private readonly BatchQueryGenerationContext m_context;

		// Token: 0x040005B5 RID: 1461
		private readonly GeneratedDeclarationCollection m_declarations;

		// Token: 0x040005B6 RID: 1462
		private readonly IQueryExpressionGenerator m_expressionGenerator;
	}
}

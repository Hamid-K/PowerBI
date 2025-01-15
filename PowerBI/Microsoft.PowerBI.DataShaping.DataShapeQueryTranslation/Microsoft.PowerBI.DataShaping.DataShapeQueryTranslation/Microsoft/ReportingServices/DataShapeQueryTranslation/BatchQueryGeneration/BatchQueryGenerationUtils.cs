using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000141 RID: 321
	internal static class BatchQueryGenerationUtils
	{
		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002F32C File Offset: 0x0002D52C
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "QueryTable", "ShouldCrossFilterGroupColumns" })]
		public static IEnumerable<global::System.ValueTuple<QueryTable, bool>> GenerateContextTables(IEnumerable<PlanOperation> contextTablePlans, GeneratedDeclarationCollection declarations, BatchQueryGenerationContext context, IQueryExpressionGenerator expressionGenerator, BatchQueryExpressionReferenceContext exprReferenceContext)
		{
			List<GeneratedTable> list = new List<GeneratedTable>();
			foreach (PlanOperation planOperation in contextTablePlans)
			{
				IReadOnlyList<GeneratedTable> readOnlyList = new BatchQueryTableGenerator(context, declarations, exprReferenceContext).GenerateTables(planOperation, expressionGenerator, null);
				list.AddRange(readOnlyList);
			}
			return list.Select((GeneratedTable t) => new global::System.ValueTuple<QueryTable, bool>(t.QueryTable, t.ShouldCrossFilterGroupColumns));
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002F3B4 File Offset: 0x0002D5B4
		public static QueryExistsFilter TranslateExistsFilter(ExistsFilterItem existsFilter, IQueryExpressionGenerator expressionGenerator, EntityDataModel model, IConceptualSchema schema, bool useConceptualSchema)
		{
			return new QueryExistsFilter(useConceptualSchema ? null : existsFilter.Targets.Select((Expression e) => BatchQueryGenerationUtils.UnwrapEntitySetExpression(e, expressionGenerator, model)), useConceptualSchema ? null : BatchQueryGenerationUtils.UnwrapEntitySetExpression(existsFilter.Exists, expressionGenerator, model), useConceptualSchema ? existsFilter.Targets.Select((Expression e) => BatchQueryGenerationUtils.UnwrapEntityExpression(e, expressionGenerator, schema)) : null, useConceptualSchema ? BatchQueryGenerationUtils.UnwrapEntityExpression(existsFilter.Exists, expressionGenerator, schema) : null);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0002F459 File Offset: 0x0002D659
		internal static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection TranslateSortDirection(Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortDirection direction, bool reverseSortOrder)
		{
			if (reverseSortOrder)
			{
				direction = direction.ReverseSortDirection();
			}
			return direction.ToQdmSortDirection();
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002F46C File Offset: 0x0002D66C
		public static IReadOnlyList<GeneratedTable> GenerateFilterContextTables(IReadOnlyDefaultContextManager defaultContextManager, FederatedEntityDataModel model, bool useConceptualSchema)
		{
			if (defaultContextManager.IsEmpty)
			{
				return Microsoft.DataShaping.Util.EmptyArray<GeneratedTable>();
			}
			IEnumerable<IConceptualColumn> columnsRequiringClearDefaultFilterContext = defaultContextManager.GetColumnsRequiringClearDefaultFilterContext();
			if (useConceptualSchema)
			{
				IReadOnlyList<IGrouping<IConceptualEntity, IConceptualColumn>> readOnlyList = (from tuple in columnsRequiringClearDefaultFilterContext.GroupBy((IConceptualColumn f) => f.Entity, ConceptualEntityExtensionAwareEqualityComparer.Instance)
					orderby tuple.Key.EdmName
					select tuple).EvaluateReadOnly<IGrouping<IConceptualEntity, IConceptualColumn>>();
				List<QueryExpression> list = new List<QueryExpression>(readOnlyList.Count);
				for (int i = 0; i < readOnlyList.Count; i++)
				{
					IReadOnlyList<IConceptualColumn> readOnlyList2 = readOnlyList[i].OrderBy((IConceptualColumn f) => f.EdmName).EvaluateReadOnly<IConceptualColumn>();
					QueryAllExpression queryAllExpression = readOnlyList[i].Key.All(readOnlyList2);
					list.Add(queryAllExpression);
				}
				return BatchQueryGenerationUtils.GenerateFilterContextTables(list);
			}
			IReadOnlyList<IGrouping<EntitySet, IEdmFieldInstance>> readOnlyList3 = (from c in columnsRequiringClearDefaultFilterContext
				select model.GetCorrespondingEdmFieldInstance(c) into f
				group f by f.Entity into tuple
				orderby tuple.Key.Name
				select tuple).EvaluateReadOnly<IGrouping<EntitySet, IEdmFieldInstance>>();
			List<QueryExpression> list2 = new List<QueryExpression>(readOnlyList3.Count);
			for (int j = 0; j < readOnlyList3.Count; j++)
			{
				IReadOnlyList<EdmField> readOnlyList4 = (from f in readOnlyList3[j]
					orderby f.Field.Name
					select f.Field).EvaluateReadOnly<EdmField>();
				QueryAllExpression queryAllExpression2 = readOnlyList3[j].Key.All(readOnlyList4, null, null);
				list2.Add(queryAllExpression2);
			}
			return BatchQueryGenerationUtils.GenerateFilterContextTables(list2);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002F67C File Offset: 0x0002D87C
		internal static IReadOnlyList<GeneratedTable> GenerateFilterContextTables(IReadOnlyList<QueryExpression> qdmFilterExpressions)
		{
			List<GeneratedTable> list = new List<GeneratedTable>(qdmFilterExpressions.Count);
			foreach (QueryExpression queryExpression in qdmFilterExpressions)
			{
				QueryTable queryTable = queryExpression.AsTableDefinition();
				WritableGeneratedColumnMap writableGeneratedColumnMap = new WritableGeneratedColumnMap();
				list.Add(new GeneratedTable(queryTable, writableGeneratedColumnMap)
				{
					ShouldCrossFilterGroupColumns = true
				});
			}
			return list;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002F6EC File Offset: 0x0002D8EC
		private static EntitySet UnwrapEntitySetExpression(Expression expression, IQueryExpressionGenerator expressionGenerator, EntityDataModel model)
		{
			ResolvedEntitySetExpressionNode resolvedEntitySetExpressionNode = (ResolvedEntitySetExpressionNode)expressionGenerator.GetExpressionNode(expression.ExpressionId.Value);
			return model.EntitySets.FindByEdmReferenceName(resolvedEntitySetExpressionNode.Entity.Name);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0002F72C File Offset: 0x0002D92C
		private static IConceptualEntity UnwrapEntityExpression(Expression expression, IQueryExpressionGenerator expressionGenerator, IConceptualSchema schema)
		{
			ResolvedEntitySetExpressionNode resolvedEntitySetExpressionNode = (ResolvedEntitySetExpressionNode)expressionGenerator.GetExpressionNode(expression.ExpressionId.Value);
			IConceptualEntity conceptualEntity;
			schema.TryGetEntity(resolvedEntitySetExpressionNode.Entity.Name, out conceptualEntity);
			return conceptualEntity;
		}
	}
}

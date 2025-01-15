using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x0200010E RID: 270
	internal sealed class SubQueryDataSetPlanner
	{
		// Token: 0x06000A66 RID: 2662 RVA: 0x000284A8 File Offset: 0x000266A8
		private SubQueryDataSetPlanner(ExpressionTable inputExpressionTable, ScopeTree scopeTree, IFederatedConceptualSchema schema, DataShapeAnnotations annotations, TranslationErrorContext errorContext, ContextGraph contextGraph, ContextWeights contextWeights, IReadOnlyDictionary<IIdentifiable, List<Filter>> translatedFilterTable)
		{
			this.m_inputExpressionTable = inputExpressionTable;
			this.m_scopeTree = scopeTree;
			this.m_schema = schema;
			this.m_annotations = annotations;
			this.m_errorContext = errorContext;
			this.m_contextGraph = contextGraph;
			this.m_contextWeights = contextWeights;
			this.m_translatedFilterTable = translatedFilterTable;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000284F8 File Offset: 0x000266F8
		public static DataSetPlan BuildSubQueryDataSetPlan(ExpressionTable inputExpressionTable, ScopeTree scopeTree, IFederatedConceptualSchema schema, DataShapeAnnotations annotations, TranslationErrorContext errorContext, ContextGraph contextGraph, ContextWeights contextWeights, ExpressionContext context, IScope outerScope, IScope innerScope, IScope rollupStartScope, bool filterEmptyGroups, IContextItem contextItem, IReadOnlyDictionary<IIdentifiable, List<Filter>> translatedFilterTable, EvaluationContextBuilderOptions options)
		{
			return new SubQueryDataSetPlanner(inputExpressionTable, scopeTree, schema, annotations, errorContext, contextGraph, contextWeights, translatedFilterTable).BuildSubQueryDataSetPlan(context, outerScope, innerScope, rollupStartScope, filterEmptyGroups, contextItem, options);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00028520 File Offset: 0x00026720
		private DataSetPlan BuildSubQueryDataSetPlan(ExpressionContext context, IScope outerScope, IScope innerScope, IScope rollupStartScope, bool filterEmptyGroups, IContextItem contextItem, EvaluationContextBuilderOptions options)
		{
			if (this.SatisfiesSubQueryGenerationConstraints(outerScope, innerScope, rollupStartScope, context))
			{
				EvaluationContext evaluationContext = EvaluationContextBuilder.BuildContext(this.m_contextGraph, this.m_contextWeights, this.m_scopeTree, contextItem, this.m_annotations, options);
				DataSetPlanInfo dataSetPlanInfo = new DataSetPlanInfo(contextItem.Id.Value, evaluationContext.Elements);
				dataSetPlanInfo.AddOutputItem(contextItem);
				DataSetPlan dataSetPlan = DataSetPlanTranslator.Translate(this.m_inputExpressionTable, this.m_scopeTree, this.m_schema, this.m_annotations, this.m_errorContext, dataSetPlanInfo, 0, filterEmptyGroups, () => SubQueryDataSetPlanner.MakePlanName(outerScope, innerScope, rollupStartScope), this.m_translatedFilterTable, null, null, null, null);
				List<ScopePlanElement> list = this.PostProcessPlanForSubQuery(outerScope, innerScope, rollupStartScope, contextItem, filterEmptyGroups, dataSetPlan);
				return new DataSetPlan(SubQueryDataSetPlanner.MakePlanName(outerScope, innerScope, rollupStartScope), 0, list, true, true, null, null, null, null);
			}
			context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, context.ObjectType, context.ObjectId, context.PropertyName, TranslationMessagePhrases.InvalidSubQueryScopes(outerScope.Id, innerScope.Id)));
			return null;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0002866B File Offset: 0x0002686B
		private bool SatisfiesSubQueryGenerationConstraints(IScope outerScope, IScope innerScope, IScope rollupStartScope, ExpressionContext context)
		{
			return SubQueryDataSetPlanner.SatisfiesSubQueryGenerationConstraints(outerScope, innerScope, rollupStartScope, this.m_scopeTree, context, this.m_inputExpressionTable);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00028684 File Offset: 0x00026884
		internal static bool SatisfiesSubQueryGenerationConstraints(IScope outerScope, IScope innerScope, IScope rollupStartScope, ScopeTree scopeTree, ExpressionContext context, ExpressionTable inputExpressionTable)
		{
			if (scopeTree.IsParentScope(outerScope, innerScope))
			{
				return rollupStartScope == null || scopeTree.IsParentScope(innerScope, rollupStartScope);
			}
			if (!scopeTree.AreSameScope(outerScope, innerScope) && !scopeTree.IsParentScope(innerScope, outerScope))
			{
				bool flag = false;
				if (outerScope.ObjectType == ObjectType.DataShape)
				{
					IScope immediateParent = scopeTree.GetParentScope(outerScope);
					flag = !scopeTree.TraverseUp(innerScope, (IScope scope) => !scopeTree.AreSameScope(scope, immediateParent));
					Filter filterForTargetStructure = ((DataShape)outerScope).Filters.GetFilterForTargetStructure(inputExpressionTable, outerScope);
					if (flag && filterForTargetStructure != null)
					{
						context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, context.ObjectType, context.ObjectId, context.PropertyName, TranslationMessagePhrases.InvalidSubQueryScopes(outerScope.Id, innerScope.Id)));
						return false;
					}
				}
				return flag;
			}
			return false;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00028778 File Offset: 0x00026978
		private List<ScopePlanElement> PostProcessPlanForSubQuery(IScope outerScope, IScope innerScope, IScope rollupStartScope, IContextItem contextItem, bool filterEmptyGroups, DataSetPlan plan)
		{
			ReadOnlyCollection<ScopePlanElement> scopes = plan.Scopes;
			int num = -1;
			int num2 = -1;
			for (int i = 0; i < scopes.Count; i++)
			{
				ScopePlanElement scopePlanElement = scopes[i];
				if (scopePlanElement.Scope == outerScope)
				{
					num = i;
				}
				if (scopePlanElement.Scope == innerScope)
				{
					num2 = i;
				}
				IScope scope = scopePlanElement.Scope;
			}
			if (outerScope.ObjectType == ObjectType.DataShape && this.m_scopeTree.GetParentScope(outerScope) == scopes[0].Scope)
			{
				bool flag = ((DataShape)outerScope).Filters.GetFilterForTargetStructure(this.m_inputExpressionTable, outerScope) == null;
			}
			List<ScopePlanElement> list = new List<ScopePlanElement>((num > -1) ? (scopes.Count - num) : scopes.Count);
			for (int j = num + 1; j <= num2; j++)
			{
				list.Add(scopes[j]);
			}
			if (filterEmptyGroups || rollupStartScope != null)
			{
				for (int k = num2 + 1; k < scopes.Count; k++)
				{
					ScopePlanElement scopePlanElement2 = scopes[k];
					if (rollupStartScope == null && this.AreSameIntersectionScope(innerScope, scopePlanElement2.Scope))
					{
						list.Add(scopePlanElement2);
					}
					else
					{
						list.Add(scopePlanElement2.OmitProjection());
					}
				}
			}
			return list;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000288A0 File Offset: 0x00026AA0
		private bool AreSameIntersectionScope(IScope candidate1, IScope candidate2)
		{
			DataIntersection dataIntersection = candidate1 as DataIntersection;
			DataIntersection dataIntersection2 = candidate2 as DataIntersection;
			return dataIntersection != null && dataIntersection2 != null && this.m_scopeTree.AreSameIntersectionScope(dataIntersection, dataIntersection2);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000288D0 File Offset: 0x00026AD0
		public static string MakePlanName(IScope outerScope, IScope innerScope, IScope rollupStartScope = null)
		{
			if (rollupStartScope == null)
			{
				return Microsoft.DataShaping.StringUtil.FormatInvariant("SubQueryFrom{0}To{1}", new object[]
				{
					outerScope.Id.Value,
					innerScope.Id.Value
				});
			}
			return Microsoft.DataShaping.StringUtil.FormatInvariant("SubQueryFrom{0}To{1}Rollup{2}", new object[]
			{
				outerScope.Id.Value,
				innerScope.Id.Value,
				rollupStartScope.Id.Value
			});
		}

		// Token: 0x0400051B RID: 1307
		private const int SubQueryPlanIndex = 0;

		// Token: 0x0400051C RID: 1308
		private readonly ScopeTree m_scopeTree;

		// Token: 0x0400051D RID: 1309
		private readonly IFederatedConceptualSchema m_schema;

		// Token: 0x0400051E RID: 1310
		private readonly ExpressionTable m_inputExpressionTable;

		// Token: 0x0400051F RID: 1311
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x04000520 RID: 1312
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000521 RID: 1313
		private readonly ContextGraph m_contextGraph;

		// Token: 0x04000522 RID: 1314
		private readonly ContextWeights m_contextWeights;

		// Token: 0x04000523 RID: 1315
		private readonly IReadOnlyDictionary<IIdentifiable, List<Filter>> m_translatedFilterTable;
	}
}

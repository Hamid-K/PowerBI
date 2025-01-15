using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning
{
	// Token: 0x02000127 RID: 295
	internal abstract class DataSetPlannerExpressionTreeTranslatorBase : ExpressionNodeTreeTransform
	{
		// Token: 0x06000B1F RID: 2847 RVA: 0x0002B7B3 File Offset: 0x000299B3
		protected DataSetPlannerExpressionTreeTranslatorBase(ScopeTree scopeTree, DataShapeAnnotations annotations, ExpressionTable expressionTable, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery)
			: base(true)
		{
			this.m_scopeTree = scopeTree;
			this.m_annotations = annotations;
			this.m_inputExpressionTable = expressionTable;
			this.m_transformReferenceMap = transformReferenceMap;
			this.m_applyTransformsInQuery = applyTransformsInQuery;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002B7E1 File Offset: 0x000299E1
		protected DataSetPlannerExpressionTreeTranslatorBase(ScopeTree scopeTree, DataShapeAnnotations annotations, ExpressionTable expressionTable, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery, ExpressionContext context, IScope containingScope)
			: this(scopeTree, annotations, expressionTable, transformReferenceMap, applyTransformsInQuery)
		{
			this.m_context = context;
			this.m_containingScope = containingScope;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002B800 File Offset: 0x00029A00
		public override ExpressionNode Visit(FunctionCallExpressionNode node)
		{
			string name = node.Descriptor.Name;
			if (name == "Evaluate")
			{
				return this.TranslateEvaluateExpression(node);
			}
			if (!(name == "Subtotal"))
			{
				return base.Visit(node);
			}
			return this.TranslateSubtotalExpression(node);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002B84C File Offset: 0x00029A4C
		protected virtual ExpressionNode TranslateEvaluateExpression(FunctionCallExpressionNode node)
		{
			return base.Visit(node);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002B855 File Offset: 0x00029A55
		protected virtual ExpressionNode TranslateSubtotalExpression(FunctionCallExpressionNode node)
		{
			return base.Visit(node);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0002B860 File Offset: 0x00029A60
		protected ExpressionNode VisitNodeInScope(ExpressionNode node, IScope targetScope)
		{
			IScope containingScope = this.m_containingScope;
			this.m_containingScope = targetScope;
			ExpressionNode expressionNode = this.Visit(node);
			this.m_containingScope = containingScope;
			return expressionNode;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0002B88C File Offset: 0x00029A8C
		protected ExpressionNode TranslateEvaluateWithScopeFunction(FunctionCallExpressionNode node, FunctionCallExpressionNode scopeNode)
		{
			HashSet<ExpressionNode> hashSet = new HashSet<ExpressionNode>();
			foreach (IScope scope in this.m_scopeTree.GetAllParentScopes(this.m_containingScope))
			{
				DataMember dataMember = scope as DataMember;
				if (dataMember != null)
				{
					this.CollectGroupKeys(dataMember, hashSet);
				}
				DataShape dataShape = scope as DataShape;
				if (dataShape != null)
				{
					this.CollectTransformGroupKeys(dataShape, hashSet);
				}
			}
			foreach (ResolvedPropertyExpressionNode resolvedPropertyExpressionNode in scopeNode.Arguments.Cast<ResolvedPropertyExpressionNode>())
			{
				if (!hashSet.Remove(resolvedPropertyExpressionNode))
				{
					this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.ScopeFunctionArgumentsNotInCurrentScope(resolvedPropertyExpressionNode.Property.GetFullName().ToString())));
				}
			}
			return new RemoveGroupingsExpressionNode(this.Visit(node.Arguments[0]), hashSet);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002B9B4 File Offset: 0x00029BB4
		private void CollectGroupKeys(DataMember dataMember, HashSet<ExpressionNode> groupKeyNodes)
		{
			if (this.m_transformReferenceMap.HasDataTransformColumnReference(dataMember))
			{
				return;
			}
			Group group = dataMember.GetGroup(this.m_annotations);
			foreach (GroupKey groupKey in group.GroupKeys)
			{
				this.CollectGroupKeyNode(groupKey.Value, groupKeyNodes);
			}
			if (group.SortKeys != null)
			{
				foreach (SortKey sortKey in group.SortKeys)
				{
					this.CollectGroupKeyNode(sortKey.Value, groupKeyNodes);
				}
			}
			foreach (Calculation calculation in this.m_scopeTree.GetItems<Calculation>(dataMember.Id))
			{
				this.CollectGroupKeyNode(calculation.Value, groupKeyNodes);
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002BAD0 File Offset: 0x00029CD0
		private void CollectTransformGroupKeys(DataShape dataShape, HashSet<ExpressionNode> groupKeysNodes)
		{
			if (!this.m_applyTransformsInQuery)
			{
				return;
			}
			DataTransformTable dataTransformInputTable = CommonDataSetPlanningUtils.GetDataTransformInputTable(dataShape);
			if (dataTransformInputTable == null)
			{
				return;
			}
			foreach (DataTransformTableColumn dataTransformTableColumn in dataTransformInputTable.Columns)
			{
				this.CollectGroupKeyNode(dataTransformTableColumn.Value, groupKeysNodes);
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0002BB40 File Offset: 0x00029D40
		private void CollectGroupKeyNode(Expression expr, HashSet<ExpressionNode> groupKeyNodes)
		{
			ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = this.m_inputExpressionTable.GetNode(expr) as ResolvedPropertyExpressionNode;
			if (resolvedPropertyExpressionNode != null && resolvedPropertyExpressionNode.Property is IConceptualColumn)
			{
				groupKeyNodes.Add(resolvedPropertyExpressionNode);
			}
		}

		// Token: 0x0400059E RID: 1438
		protected readonly ScopeTree m_scopeTree;

		// Token: 0x0400059F RID: 1439
		protected readonly DataShapeAnnotations m_annotations;

		// Token: 0x040005A0 RID: 1440
		protected readonly ExpressionTable m_inputExpressionTable;

		// Token: 0x040005A1 RID: 1441
		protected readonly DataTransformReferenceMap m_transformReferenceMap;

		// Token: 0x040005A2 RID: 1442
		protected readonly bool m_applyTransformsInQuery;

		// Token: 0x040005A3 RID: 1443
		protected ExpressionContext m_context;

		// Token: 0x040005A4 RID: 1444
		protected IScope m_containingScope;
	}
}

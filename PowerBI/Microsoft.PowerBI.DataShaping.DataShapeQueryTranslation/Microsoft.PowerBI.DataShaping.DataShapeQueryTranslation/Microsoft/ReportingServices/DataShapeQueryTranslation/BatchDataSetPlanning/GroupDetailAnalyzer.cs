using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000184 RID: 388
	internal sealed class GroupDetailAnalyzer : CalculationExpressionAnalyzer
	{
		// Token: 0x06000D9B RID: 3483 RVA: 0x00037D5E File Offset: 0x00035F5E
		private GroupDetailAnalyzer(DataShapeAnnotations annotations, ScopeTree scopeTree, WritableExpressionTable expressionTable, IFederatedConceptualSchema schema)
			: base(expressionTable)
		{
			this.m_annotations = annotations;
			this.m_scopeTree = scopeTree;
			this.m_mapping = new WritableGroupDetailMap();
			this.m_schema = schema;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00037D88 File Offset: 0x00035F88
		public static GroupDetailAnalyzer.Result Analyze(DataShape dataShape, DataShapeAnnotations annotations, ScopeTree scopeTree, WritableExpressionTable expressionTable, IFederatedConceptualSchema schema)
		{
			GroupDetailAnalyzer groupDetailAnalyzer = new GroupDetailAnalyzer(annotations, scopeTree, expressionTable, schema);
			groupDetailAnalyzer.Visit(dataShape);
			return new GroupDetailAnalyzer.Result(groupDetailAnalyzer.m_mapping.AsReadOnly(), groupDetailAnalyzer.m_calculationMap);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00037DBD File Offset: 0x00035FBD
		protected override void Enter(DataMember dataMember)
		{
			this.VisitGroup(dataMember);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00037DC8 File Offset: 0x00035FC8
		private void VisitGroup(DataMember member)
		{
			Group group = member.Group;
			if (group == null)
			{
				return;
			}
			if (group.SortKeys != null)
			{
				foreach (SortKey sortKey in group.SortKeys)
				{
					this.VisitExpression(sortKey.Value, member);
				}
			}
			if (group.ScopeIdDefinition != null)
			{
				foreach (ScopeValueDefinition scopeValueDefinition in group.ScopeIdDefinition.Values)
				{
					this.VisitExpression(scopeValueDefinition.Value, member);
				}
			}
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00037E8C File Offset: 0x0003608C
		protected override void Visit(Calculation calculation)
		{
			if (this.m_annotations.CanBeHandledByProcessing(calculation))
			{
				return;
			}
			base.Visit(calculation);
			ReadOnlyCollection<ExpressionId> readOnlyCollection;
			if (!this.m_calculationMap.TryGetExpressions(calculation, out readOnlyCollection))
			{
				return;
			}
			IScope containingScope = this.m_scopeTree.GetContainingScope(calculation);
			foreach (ExpressionId expressionId in readOnlyCollection)
			{
				this.VisitExpression(expressionId, containingScope);
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00037F0C File Offset: 0x0003610C
		private void VisitExpression(Expression expression, IScope scope)
		{
			this.VisitExpression(expression.ExpressionId.Value, scope);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00037F30 File Offset: 0x00036130
		private void VisitExpression(ExpressionId exprId, IScope scope)
		{
			ExpressionNode node = this.m_expressionTable.GetNode(exprId);
			if (MeasureAnalyzer.IsMeasure(node))
			{
				return;
			}
			ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = node as ResolvedPropertyExpressionNode;
			ExpressionId expressionId;
			if (resolvedPropertyExpressionNode != null && this.TryFindGroupKeyForDetail(resolvedPropertyExpressionNode, scope, out expressionId))
			{
				this.m_mapping.AddDetail(expressionId, exprId);
			}
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00037F78 File Offset: 0x00036178
		private bool TryFindGroupKeyForDetail(ResolvedPropertyExpressionNode node, IScope scope, out ExpressionId keyId)
		{
			ExpressionId resultId = new ExpressionId(0);
			GroupDetailAnalyzer.MatchStatus status = GroupDetailAnalyzer.MatchStatus.NoMatch;
			this.m_scopeTree.TraverseUp(scope, delegate(IScope candidateScope)
			{
				DataMember dataMember = candidateScope as DataMember;
				if (dataMember == null)
				{
					return true;
				}
				foreach (GroupKey groupKey in dataMember.Group.GroupKeys)
				{
					ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = this.m_expressionTable.GetNode(groupKey.Value) as ResolvedPropertyExpressionNode;
					if (resolvedPropertyExpressionNode != null)
					{
						IConceptualProperty property = resolvedPropertyExpressionNode.Property;
						if (property == node.Property)
						{
							resultId = groupKey.Value.ExpressionId.Value;
							status = GroupDetailAnalyzer.MatchStatus.SameExpression;
							return false;
						}
						IConceptualColumn conceptualColumn = node.Property.AsColumn();
						if (status < GroupDetailAnalyzer.MatchStatus.GroupBy && conceptualColumn != null && conceptualColumn.Grouping.QueryGroupColumns.Contains(property.AsColumn()))
						{
							resultId = groupKey.Value.ExpressionId.Value;
							status = GroupDetailAnalyzer.MatchStatus.GroupBy;
						}
						if (status < GroupDetailAnalyzer.MatchStatus.SameEntity && node.Property.IsFromEntity(resolvedPropertyExpressionNode.Property.Entity))
						{
							resultId = groupKey.Value.ExpressionId.Value;
							status = GroupDetailAnalyzer.MatchStatus.SameEntity;
						}
					}
				}
				return true;
			});
			keyId = resultId;
			return status > GroupDetailAnalyzer.MatchStatus.NoMatch;
		}

		// Token: 0x040006A7 RID: 1703
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x040006A8 RID: 1704
		private readonly ScopeTree m_scopeTree;

		// Token: 0x040006A9 RID: 1705
		private readonly WritableGroupDetailMap m_mapping;

		// Token: 0x040006AA RID: 1706
		private readonly IFederatedConceptualSchema m_schema;

		// Token: 0x020002F6 RID: 758
		private enum MatchStatus
		{
			// Token: 0x04000AF7 RID: 2807
			NoMatch,
			// Token: 0x04000AF8 RID: 2808
			SameEntity,
			// Token: 0x04000AF9 RID: 2809
			GroupBy,
			// Token: 0x04000AFA RID: 2810
			SameExpression
		}

		// Token: 0x020002F7 RID: 759
		internal struct Result
		{
			// Token: 0x060016E4 RID: 5860 RVA: 0x00052187 File Offset: 0x00050387
			public Result(GroupDetailMap detailMap, CalculationExpressionMap calculationMap)
			{
				this.m_detailMap = detailMap;
				this.m_calculationMap = calculationMap;
			}

			// Token: 0x17000401 RID: 1025
			// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00052197 File Offset: 0x00050397
			public GroupDetailMap DetailMap
			{
				get
				{
					return this.m_detailMap;
				}
			}

			// Token: 0x17000402 RID: 1026
			// (get) Token: 0x060016E6 RID: 5862 RVA: 0x0005219F File Offset: 0x0005039F
			public CalculationExpressionMap CalculationMap
			{
				get
				{
					return this.m_calculationMap;
				}
			}

			// Token: 0x04000AFB RID: 2811
			private readonly GroupDetailMap m_detailMap;

			// Token: 0x04000AFC RID: 2812
			private readonly CalculationExpressionMap m_calculationMap;
		}
	}
}

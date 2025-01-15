using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000D9 RID: 217
	internal sealed class ContextGraphBuilder : DataShapeVisitor
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x000228FF File Offset: 0x00020AFF
		private ContextGraphBuilder(ScopeTree scopeTree, DataShapeAnnotations annotations, ExpressionTable expressionTable)
		{
			this.m_scopeTree = scopeTree;
			this.m_annotations = annotations;
			this.m_expressionTable = expressionTable;
			this.m_graph = new ContextGraph();
			this.m_innermostDataShapeScopes = new Stack<IScope>();
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00022932 File Offset: 0x00020B32
		public static ContextGraph BuildGraph(ScopeTree scopeTree, DataShapeAnnotations annotations, ExpressionTable expressionTable, DataShape dataShape)
		{
			ContextGraphBuilder contextGraphBuilder = new ContextGraphBuilder(scopeTree, annotations, expressionTable);
			contextGraphBuilder.Visit(dataShape);
			return contextGraphBuilder.m_graph;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00022948 File Offset: 0x00020B48
		protected override void Enter(DataShape dataShape)
		{
			this.m_innermostDataShapeScopes.Push(this.m_scopeTree.GetInnermostScopeInDataShape(dataShape));
			ContextGraph.Node orCreateNode = this.m_graph.GetOrCreateNode(dataShape);
			IScope parentScope = this.m_scopeTree.GetParentScope(dataShape);
			if (parentScope != null)
			{
				ContextGraph.Node orCreateNode2 = this.m_graph.GetOrCreateNode(parentScope);
				this.AddEdge(orCreateNode, orCreateNode2, ContextState.Output, null);
				if (!dataShape.ContextOnly.GetValueOrDefault<bool>() && this.IsNeededToConstrainGroupJoins(dataShape))
				{
					if (orCreateNode2.Item.ObjectType == ObjectType.DataShape)
					{
						this.AddEdge(orCreateNode2, orCreateNode, ContextState.JoinConstraint, new ContextState?(ContextState.JoinConstraint));
						return;
					}
					this.AddEdge(orCreateNode2, orCreateNode, ContextState.JoinConstraint, null);
				}
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x000229EF File Offset: 0x00020BEF
		protected override void Exit(DataShape dataShape)
		{
			this.m_innermostDataShapeScopes.Pop();
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00022A00 File Offset: 0x00020C00
		protected override void Enter(DataMember dataMember)
		{
			if (dataMember.IsDynamic)
			{
				ContextGraph.Node orCreateNode = this.m_graph.GetOrCreateNode(dataMember);
				IScope parentScope = this.m_scopeTree.GetParentScope(dataMember);
				ContextGraph.Node orCreateNode2 = this.m_graph.GetOrCreateNode(parentScope);
				this.AddEdge(orCreateNode, orCreateNode2, ContextState.Output, null);
				if (parentScope.ObjectType == ObjectType.DataShape)
				{
					this.AddEdge(orCreateNode2, orCreateNode, ContextState.JoinConstraint, new ContextState?(ContextState.JoinConstraint));
				}
				if (parentScope.ObjectType == ObjectType.DataMember)
				{
					this.AddEdge(orCreateNode2, orCreateNode, ContextState.Context, null);
				}
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00022A84 File Offset: 0x00020C84
		protected override void Visit(Calculation calculation)
		{
			ContextGraph.Node orCreateNode = this.m_graph.GetOrCreateNode(calculation);
			IScope containingStructuralScope = this.m_scopeTree.GetContainingStructuralScope(calculation);
			ContextGraph.Node orCreateNode2 = this.m_graph.GetOrCreateNode(containingStructuralScope);
			this.AddEdge(orCreateNode, containingStructuralScope, ContextState.Output, null);
			Calculation calculation2;
			if (this.m_annotations.IsSubtotal(calculation, out calculation2))
			{
				ContextGraph.Node orCreateNode3 = this.m_graph.GetOrCreateNode(calculation2);
				this.AddEdge(orCreateNode, orCreateNode3, ContextState.Rollup, null);
			}
			if (this.IsNeededToConstrainGroupJoins(calculation))
			{
				this.AddEdgeToConstrainGroupJoins(orCreateNode2, orCreateNode);
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00022B10 File Offset: 0x00020D10
		protected override void Enter(DataIntersection dataIntersection)
		{
			if (this.m_scopeTree.HasScope(dataIntersection.Id))
			{
				ContextGraph.Node orCreateNode = this.m_graph.GetOrCreateNode(dataIntersection);
				IScope primaryParentScope = this.m_scopeTree.GetPrimaryParentScope(dataIntersection);
				ContextGraph.Node orCreateNode2 = this.m_graph.GetOrCreateNode(primaryParentScope);
				this.AddEdge(orCreateNode, orCreateNode2, ContextState.Output, null);
				IScope secondaryParentScope = this.m_scopeTree.GetSecondaryParentScope(dataIntersection);
				ContextGraph.Node orCreateNode3 = this.m_graph.GetOrCreateNode(secondaryParentScope);
				this.AddEdge(orCreateNode, orCreateNode3, ContextState.Output, null);
				if (this.IsNeededToConstrainGroupJoins(dataIntersection))
				{
					this.AddEdgeToConstrainGroupJoins(orCreateNode2, orCreateNode);
					this.AddEdgeToConstrainGroupJoins(orCreateNode3, orCreateNode);
				}
			}
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00022BB8 File Offset: 0x00020DB8
		private bool IsNeededToConstrainGroupJoins(DataShape dataShape)
		{
			IScope parentScope = this.m_scopeTree.GetParentScope(dataShape);
			return parentScope != null && this.IsInnermostScopeInParentDataShape(parentScope);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00022BDE File Offset: 0x00020DDE
		private bool IsNeededToConstrainGroupJoins(DataIntersection intersection)
		{
			return this.IsInnermostScopeInDataShape(intersection);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00022BE8 File Offset: 0x00020DE8
		private bool IsNeededToConstrainGroupJoins(Calculation calculation)
		{
			if (!this.m_annotations.NeededForQueryCalculationContext(calculation))
			{
				return false;
			}
			IScope containingScope = this.m_scopeTree.GetContainingScope(calculation);
			return this.IsInnermostScopeInDataShape(containingScope);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00022C1C File Offset: 0x00020E1C
		private bool IsInnermostScopeInDataShape(IScope scope)
		{
			IScope scope2 = this.m_innermostDataShapeScopes.Peek();
			return this.m_scopeTree.AreSameScope(scope2, scope);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00022C44 File Offset: 0x00020E44
		private bool IsInnermostScopeInParentDataShape(IScope scope)
		{
			IScope scope2 = this.m_innermostDataShapeScopes.ElementAt(1);
			return this.m_scopeTree.AreSameScope(scope2, scope);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00022C6C File Offset: 0x00020E6C
		private void AddEdgeToConstrainGroupJoins(ContextGraph.Node parentNode, ContextGraph.Node childNode)
		{
			if (parentNode.Item.ObjectType == ObjectType.DataShape)
			{
				this.AddEdge(parentNode, childNode, ContextState.Context, new ContextState?(ContextState.JoinConstraint));
				return;
			}
			this.AddEdge(parentNode, childNode, ContextState.Context, null);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00022CAA File Offset: 0x00020EAA
		private void AddEdge(ContextGraph.Node startNode, ContextGraph.Node endNode, ContextState kind, ContextState? requiredEntryState = null)
		{
			startNode.Edges.Add(new ContextGraph.Edge(endNode, kind, requiredEntryState));
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00022CC0 File Offset: 0x00020EC0
		private void AddEdge(ContextGraph.Node startNode, IContextItem endItem, ContextState kind, ContextState? requiredEntryState = null)
		{
			ContextGraph.Node orCreateNode = this.m_graph.GetOrCreateNode(endItem);
			this.AddEdge(startNode, orCreateNode, kind, requiredEntryState);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00022CE8 File Offset: 0x00020EE8
		private void AddEdge(IContextItem startItem, IContextItem endItem, ContextState kind, ContextState? requiredEntryState = null)
		{
			ContextGraph.Node orCreateNode = this.m_graph.GetOrCreateNode(startItem);
			ContextGraph.Node orCreateNode2 = this.m_graph.GetOrCreateNode(endItem);
			this.AddEdge(orCreateNode, orCreateNode2, kind, requiredEntryState);
		}

		// Token: 0x04000440 RID: 1088
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000441 RID: 1089
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x04000442 RID: 1090
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x04000443 RID: 1091
		private readonly ContextGraph m_graph;

		// Token: 0x04000444 RID: 1092
		private readonly Stack<IScope> m_innermostDataShapeScopes;
	}
}

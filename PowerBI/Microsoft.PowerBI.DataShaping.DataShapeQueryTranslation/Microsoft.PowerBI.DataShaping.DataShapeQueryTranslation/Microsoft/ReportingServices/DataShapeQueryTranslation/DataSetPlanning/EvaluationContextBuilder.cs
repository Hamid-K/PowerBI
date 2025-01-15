using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x02000105 RID: 261
	internal sealed class EvaluationContextBuilder
	{
		// Token: 0x06000A34 RID: 2612 RVA: 0x0002784C File Offset: 0x00025A4C
		private EvaluationContextBuilder(ContextGraph contextGraph, ContextWeights contextWeights, ScopeTree scopeTree, DataShapeAnnotations annotations, EvaluationContextBuilderOptions options)
		{
			this.m_contextGraph = contextGraph;
			this.m_contextWeights = contextWeights;
			this.m_scopeTree = scopeTree;
			this.m_annotations = annotations;
			this.m_options = options;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00027879 File Offset: 0x00025A79
		public static EvaluationContext BuildContext(ContextGraph contextGraph, ContextWeights contextWeights, ScopeTree scopeTree, IContextItem item, DataShapeAnnotations annotations, EvaluationContextBuilderOptions options)
		{
			return new EvaluationContext(new EvaluationContextBuilder(contextGraph, contextWeights, scopeTree, annotations, options).DetermineElements(item));
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00027894 File Offset: 0x00025A94
		private IEnumerable<ContextElement> DetermineElements(IContextItem item)
		{
			ContextGraph.Node node = this.m_contextGraph.GetNode(item);
			ContextState contextState = ContextState.Output;
			Dictionary<IContextItem, ContextElement> dictionary = EvaluationContextBuilder.DiscoverTraversal.Traverse(node, contextState);
			dictionary = EvaluationContextBuilder.RollupTraversal.Traverse(dictionary, node, contextState);
			dictionary = EvaluationContextBuilder.SynchronizationTraversal.Traverse(dictionary, node, contextState);
			this.LinkCalculationToRollupParents(item, dictionary);
			this.AttachLimitsAppliedToContextElements(item, dictionary);
			List<ContextElement> list = dictionary.Values.OrderBy((ContextElement e) => this.m_contextWeights.GetWeight(e.Content)).ToList<ContextElement>();
			this.AdjustContextStatesAfterOrdering(item, list);
			return list;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00027900 File Offset: 0x00025B00
		private void LinkCalculationToRollupParents(IContextItem item, Dictionary<IContextItem, ContextElement> elementsByItem)
		{
			Calculation calculation = item as Calculation;
			if (calculation == null)
			{
				return;
			}
			if (!this.m_annotations.IsSubtotal(calculation))
			{
				return;
			}
			IScope rollupParent = this.m_annotations.GetRollupParent(calculation);
			if (rollupParent != null)
			{
				elementsByItem[rollupParent] = elementsByItem[rollupParent].RecordRollupCalculation(calculation);
			}
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0002794C File Offset: 0x00025B4C
		private void AttachLimitsAppliedToContextElements(IContextItem item, Dictionary<IContextItem, ContextElement> elementsByItem)
		{
			this.m_annotations.GetContainingDataShape(item);
			foreach (IContextItem contextItem in elementsByItem.Keys.Evaluate<IContextItem>())
			{
				Limit limit = null;
				ObjectType objectType = contextItem.ObjectType;
				if (objectType != ObjectType.DataIntersection)
				{
					if (objectType == ObjectType.DataMember)
					{
						limit = this.m_annotations.GetLimitWithInnermostTarget((DataMember)contextItem);
					}
				}
				else
				{
					limit = this.m_annotations.GetLimit((DataIntersection)contextItem);
				}
				if (limit != null)
				{
					elementsByItem[contextItem] = elementsByItem[contextItem].AttachLimit(limit);
				}
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x000279F4 File Offset: 0x00025BF4
		private bool IncludesParentScope(IEnumerable<IScope> allScopes, IScope childScope)
		{
			foreach (IScope scope in allScopes)
			{
				if (this.m_scopeTree.IsParentScope(scope, childScope))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00027A4C File Offset: 0x00025C4C
		private void AdjustContextStatesAfterOrdering(IContextItem item, List<ContextElement> orderedElements)
		{
			HashSet<IScope> hashSet = new HashSet<IScope>();
			IScope itemScope = this.GetItemScope(item);
			for (int i = 0; i < orderedElements.Count; i++)
			{
				ContextElement contextElement = orderedElements[i];
				if (contextElement.Content == item)
				{
					break;
				}
				IScope scope = contextElement.Content as IScope;
				if (scope == null)
				{
					Calculation calculation = contextElement.Content as Calculation;
					IScope containingScope = this.m_scopeTree.GetContainingScope(calculation);
					if (hashSet.Contains(containingScope))
					{
						orderedElements[i] = contextElement.ChangeStateTo(ContextState.ContextOnly);
					}
				}
				else if (!this.m_scopeTree.AreSameScope(itemScope, scope) && contextElement.ElementState == ContextState.Context)
				{
					orderedElements[i] = contextElement.ChangeStateTo(ContextState.ContextOnly);
					hashSet.Add(scope);
				}
			}
			this.HandleContextOnlyMembers(orderedElements, item);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00027B11 File Offset: 0x00025D11
		private IScope GetItemScope(IContextItem item)
		{
			if (item is IScope)
			{
				return (IScope)item;
			}
			if (item is Calculation)
			{
				return this.m_scopeTree.GetContainingScope((Calculation)item);
			}
			return null;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00027B40 File Offset: 0x00025D40
		private void HandleContextOnlyMembers(List<ContextElement> orderedElements, IContextItem targetItem)
		{
			IScope itemScope = this.GetItemScope(targetItem);
			for (int i = 0; i < orderedElements.Count; i++)
			{
				ContextElement contextElement = orderedElements[i];
				DataMember dataMember = contextElement.Content as DataMember;
				if (dataMember != null && dataMember.ContextOnly)
				{
					bool flag = false;
					ContextOnlyElementMarking contextOnlyMarkingKind = this.m_options.ContextOnlyMarkingKind;
					if (contextOnlyMarkingKind != ContextOnlyElementMarking.All)
					{
						if (contextOnlyMarkingKind == ContextOnlyElementMarking.TargetItemChildrenOnly)
						{
							flag = !this.m_scopeTree.IsSameOrParentScope(dataMember, itemScope);
						}
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						orderedElements[i] = contextElement.SetContextOnly();
					}
				}
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00027BC8 File Offset: 0x00025DC8
		private static ContextState ComputeExitState(ContextState enterState, ContextState edgeState)
		{
			if (enterState == ContextState.Output)
			{
				return edgeState;
			}
			if (enterState != ContextState.Context)
			{
				if (enterState != ContextState.JoinConstraint)
				{
					Contract.RetailFail("Only 'Output', 'Context' and 'JoinConstraint' enter states expected.");
					throw new InvalidOperationException("Only 'Output', 'Context' and 'JoinConstraint' enter states expected.");
				}
				return ContextState.JoinConstraint;
			}
			else
			{
				if (edgeState != ContextState.JoinConstraint)
				{
					return ContextState.Context;
				}
				return ContextState.JoinConstraint;
			}
		}

		// Token: 0x040004F5 RID: 1269
		private readonly ContextGraph m_contextGraph;

		// Token: 0x040004F6 RID: 1270
		private readonly ContextWeights m_contextWeights;

		// Token: 0x040004F7 RID: 1271
		private readonly ScopeTree m_scopeTree;

		// Token: 0x040004F8 RID: 1272
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x040004F9 RID: 1273
		private readonly EvaluationContextBuilderOptions m_options;

		// Token: 0x020002C6 RID: 710
		private abstract class ContextGraphTraversal
		{
			// Token: 0x0600163E RID: 5694 RVA: 0x00051231 File Offset: 0x0004F431
			protected ContextGraphTraversal()
			{
				this.m_nodesToVisit = new Stack<EvaluationContextBuilder.ContextGraphTraversal.TraversalStep>();
				this.m_visitedNodes = new HashSet<ContextGraph.Node>();
			}

			// Token: 0x0600163F RID: 5695 RVA: 0x00051250 File Offset: 0x0004F450
			protected void DoTraversal(ContextGraph.Node startNode, ContextState startState)
			{
				this.m_nodesToVisit.Push(new EvaluationContextBuilder.ContextGraphTraversal.TraversalStep(startNode, startState));
				while (this.m_nodesToVisit.Count > 0)
				{
					EvaluationContextBuilder.ContextGraphTraversal.TraversalStep traversalStep = this.m_nodesToVisit.Pop();
					ContextGraph.Node node = traversalStep.Node;
					ContextState state = traversalStep.State;
					if (this.m_visitedNodes.Contains(node))
					{
						this.VisitExistingNode(node, state);
					}
					else
					{
						this.m_visitedNodes.Add(node);
						this.VisitNode(node, state);
					}
				}
			}

			// Token: 0x06001640 RID: 5696 RVA: 0x000512C4 File Offset: 0x0004F4C4
			protected virtual void VisitExistingNode(ContextGraph.Node node, ContextState newState)
			{
			}

			// Token: 0x06001641 RID: 5697
			protected abstract void VisitNode(ContextGraph.Node node, ContextState enterState);

			// Token: 0x06001642 RID: 5698 RVA: 0x000512C6 File Offset: 0x0004F4C6
			protected void TraverseEdge(ContextGraph.Edge edge, ContextState exitState)
			{
				this.m_nodesToVisit.Push(new EvaluationContextBuilder.ContextGraphTraversal.TraversalStep(edge.End, exitState));
			}

			// Token: 0x04000A7F RID: 2687
			private readonly Stack<EvaluationContextBuilder.ContextGraphTraversal.TraversalStep> m_nodesToVisit;

			// Token: 0x04000A80 RID: 2688
			private readonly HashSet<ContextGraph.Node> m_visitedNodes;

			// Token: 0x02000333 RID: 819
			private struct TraversalStep
			{
				// Token: 0x06001799 RID: 6041 RVA: 0x00052F34 File Offset: 0x00051134
				public TraversalStep(ContextGraph.Node node, ContextState state)
				{
					this.Node = node;
					this.State = state;
				}

				// Token: 0x04000B90 RID: 2960
				public ContextGraph.Node Node;

				// Token: 0x04000B91 RID: 2961
				public ContextState State;
			}
		}

		// Token: 0x020002C7 RID: 711
		private sealed class DiscoverTraversal : EvaluationContextBuilder.ContextGraphTraversal
		{
			// Token: 0x06001643 RID: 5699 RVA: 0x000512DF File Offset: 0x0004F4DF
			private DiscoverTraversal()
			{
				this.m_elements = new Dictionary<IContextItem, ContextElement>();
			}

			// Token: 0x06001644 RID: 5700 RVA: 0x000512F2 File Offset: 0x0004F4F2
			public static Dictionary<IContextItem, ContextElement> Traverse(ContextGraph.Node startNode, ContextState startState)
			{
				EvaluationContextBuilder.DiscoverTraversal discoverTraversal = new EvaluationContextBuilder.DiscoverTraversal();
				discoverTraversal.DoTraversal(startNode, startState);
				return discoverTraversal.m_elements;
			}

			// Token: 0x06001645 RID: 5701 RVA: 0x00051308 File Offset: 0x0004F508
			protected override void VisitExistingNode(ContextGraph.Node node, ContextState newState)
			{
				ContextElement contextElement = this.m_elements[node.Item];
				if (contextElement.ElementState == newState || !contextElement.ElementState.CanChangeTo(newState))
				{
					return;
				}
				this.VisitNode(node, newState);
			}

			// Token: 0x06001646 RID: 5702 RVA: 0x00051348 File Offset: 0x0004F548
			protected override void VisitNode(ContextGraph.Node node, ContextState enterState)
			{
				ContextState contextState = ((enterState == ContextState.JoinConstraint) ? ContextState.Context : enterState);
				this.m_elements[node.Item] = new ContextElement(node.Item, contextState);
				foreach (ContextGraph.Edge edge in node.Edges)
				{
					if (edge.ShouldTraverse(enterState) && edge.State != ContextState.Rollup && edge.State != ContextState.SynchronizationTarget)
					{
						ContextState contextState2 = EvaluationContextBuilder.ComputeExitState(enterState, edge.State);
						base.TraverseEdge(edge, contextState2);
					}
				}
			}

			// Token: 0x06001647 RID: 5703 RVA: 0x000513EC File Offset: 0x0004F5EC
			[Conditional("DEBUG")]
			private void AssertExistingElementHasCompatibleState(ContextGraph.Node node, ContextState enterState)
			{
				ContextElement contextElement;
				if (this.m_elements.TryGetValue(node.Item, out contextElement))
				{
					Contract.RetailAssert(contextElement.ElementState.CanChangeTo(enterState), "Incompatible state change");
				}
			}

			// Token: 0x04000A81 RID: 2689
			private readonly Dictionary<IContextItem, ContextElement> m_elements;
		}

		// Token: 0x020002C8 RID: 712
		private sealed class RollupTraversal : EvaluationContextBuilder.ContextGraphTraversal
		{
			// Token: 0x06001648 RID: 5704 RVA: 0x00051424 File Offset: 0x0004F624
			private RollupTraversal(Dictionary<IContextItem, ContextElement> elements)
			{
				this.m_elements = elements;
			}

			// Token: 0x06001649 RID: 5705 RVA: 0x00051433 File Offset: 0x0004F633
			public static Dictionary<IContextItem, ContextElement> Traverse(Dictionary<IContextItem, ContextElement> elements, ContextGraph.Node startNode, ContextState startState)
			{
				EvaluationContextBuilder.RollupTraversal rollupTraversal = new EvaluationContextBuilder.RollupTraversal(elements);
				rollupTraversal.DoTraversal(startNode, startState);
				return rollupTraversal.m_elements;
			}

			// Token: 0x0600164A RID: 5706 RVA: 0x00051448 File Offset: 0x0004F648
			protected override void VisitNode(ContextGraph.Node node, ContextState enterState)
			{
				if (enterState == ContextState.Rollup)
				{
					ContextElement contextElement;
					if (this.m_elements.TryGetValue(node.Item, out contextElement) && contextElement.ElementState == ContextState.Output)
					{
						return;
					}
					this.m_elements[node.Item] = new ContextElement(node.Item, ContextState.Rollup);
				}
				foreach (ContextGraph.Edge edge in node.Edges)
				{
					if (enterState == ContextState.Rollup || edge.State == ContextState.Rollup)
					{
						base.TraverseEdge(edge, ContextState.Rollup);
					}
				}
			}

			// Token: 0x04000A82 RID: 2690
			private readonly Dictionary<IContextItem, ContextElement> m_elements;
		}

		// Token: 0x020002C9 RID: 713
		private sealed class SynchronizationTraversal : EvaluationContextBuilder.ContextGraphTraversal
		{
			// Token: 0x0600164B RID: 5707 RVA: 0x000514E8 File Offset: 0x0004F6E8
			private SynchronizationTraversal(Dictionary<IContextItem, ContextElement> elements)
			{
				this.m_elements = elements;
			}

			// Token: 0x0600164C RID: 5708 RVA: 0x000514F7 File Offset: 0x0004F6F7
			public static Dictionary<IContextItem, ContextElement> Traverse(Dictionary<IContextItem, ContextElement> elements, ContextGraph.Node startNode, ContextState startState)
			{
				EvaluationContextBuilder.SynchronizationTraversal synchronizationTraversal = new EvaluationContextBuilder.SynchronizationTraversal(elements);
				synchronizationTraversal.DoTraversal(startNode, startState);
				return synchronizationTraversal.m_elements;
			}

			// Token: 0x0600164D RID: 5709 RVA: 0x0005150C File Offset: 0x0004F70C
			protected override void VisitNode(ContextGraph.Node node, ContextState enterState)
			{
				if (enterState == ContextState.SynchronizationTarget)
				{
					ContextElement contextElement;
					this.m_elements.TryGetValue(node.Item, out contextElement);
					DataMember dataMember = node.Item as DataMember;
					if (dataMember != null && dataMember.IsDynamic && dataMember.Group.StartPosition == null)
					{
						this.m_elements[node.Item] = new ContextElement(node.Item, ContextState.Output);
					}
					else
					{
						this.m_elements[node.Item] = new ContextElement(node.Item, ContextState.SynchronizationTarget);
					}
				}
				if (enterState == ContextState.SynchronizationContextOnly)
				{
					ContextElement contextElement2;
					if (this.m_elements.TryGetValue(node.Item, out contextElement2) && contextElement2.ElementState == ContextState.Output)
					{
						return;
					}
					this.m_elements[node.Item] = new ContextElement(node.Item, ContextState.ContextOnly);
				}
				foreach (ContextGraph.Edge edge in node.Edges)
				{
					if (EvaluationContextBuilder.SynchronizationTraversal.ShouldTraverse(edge, enterState))
					{
						if (enterState == ContextState.Output && edge.State == ContextState.SynchronizationTarget)
						{
							base.TraverseEdge(edge, ContextState.SynchronizationTarget);
						}
						else if (enterState == ContextState.SynchronizationTarget || enterState == ContextState.SynchronizationContextOnly)
						{
							base.TraverseEdge(edge, ContextState.SynchronizationContextOnly);
						}
						else
						{
							base.TraverseEdge(edge, edge.State);
						}
					}
				}
			}

			// Token: 0x0600164E RID: 5710 RVA: 0x00051654 File Offset: 0x0004F854
			private static bool ShouldTraverse(ContextGraph.Edge edge, ContextState enterState)
			{
				if (enterState == ContextState.SynchronizationContextOnly)
				{
					enterState = ContextState.JoinConstraint;
				}
				return edge.ShouldTraverse(enterState);
			}

			// Token: 0x04000A83 RID: 2691
			private readonly Dictionary<IContextItem, ContextElement> m_elements;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000FE RID: 254
	internal sealed class DsdDataBindingGenerator
	{
		// Token: 0x06000A03 RID: 2563 RVA: 0x000269A2 File Offset: 0x00024BA2
		private DsdDataBindingGenerator(ScopeTree scopeTree, DataShapeAnnotations annotations, ExpressionTable expressionTable, List<DataSetPlanInfo> plans, OutputPlanMapping outputPlanMapping)
		{
			this.m_scopeTree = scopeTree;
			this.m_annotations = annotations;
			this.m_expressionTable = expressionTable;
			this.m_plans = plans;
			this.m_outputPlanMapping = outputPlanMapping;
			this.m_bindingMapping = new DataBindingMapping();
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000269DA File Offset: 0x00024BDA
		public static DataBindingMapping GenerateDataBindings(DataShape dataShape, ScopeTree scopeTree, DataShapeAnnotations annotations, ExpressionTable expressionTable, List<DataSetPlanInfo> plans, OutputPlanMapping outputPlanMapping)
		{
			DsdDataBindingGenerator dsdDataBindingGenerator = new DsdDataBindingGenerator(scopeTree, annotations, expressionTable, plans, outputPlanMapping);
			dsdDataBindingGenerator.Generate(dataShape);
			return dsdDataBindingGenerator.m_bindingMapping;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000269F4 File Offset: 0x00024BF4
		private void Generate(DataShape dataShape)
		{
			this.m_dataShapeContext = DataShapeContext.Create(dataShape, this.m_annotations, this.m_scopeTree);
			this.m_bindingMetadata = DsdDataBindingMetadataCollector.Collect(this.m_dataShapeContext, this.m_annotations, this.m_outputPlanMapping);
			this.DetermineBindings(dataShape);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00026A34 File Offset: 0x00024C34
		private void DetermineBindings(DataShape dataShape)
		{
			DataShape currentDataShape = this.m_currentDataShape;
			this.m_currentDataShape = dataShape;
			int bindingIndex = this.m_bindingMetadata.GetBindingIndex(dataShape);
			this.AddBinding(dataShape, bindingIndex, false, null);
			this.ReplaceRange(dataShape, bindingIndex, false);
			this.DetermineBindings(dataShape.SecondaryHierarchy, true);
			this.DetermineBindings(dataShape.PrimaryHierarchy, false);
			this.m_currentDataShape = currentDataShape;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00026A90 File Offset: 0x00024C90
		private void DetermineBindings(DataHierarchy hierarchy, bool isSecondary)
		{
			if (hierarchy == null)
			{
				return;
			}
			this.DetermineBindings(hierarchy.DataMembers, isSecondary);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00026AA4 File Offset: 0x00024CA4
		private void DetermineBindings(IList<DataMember> dataMembers, bool isSecondary)
		{
			if (dataMembers == null)
			{
				return;
			}
			foreach (DataMember dataMember in dataMembers)
			{
				this.DetermineBindings(dataMember, isSecondary);
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00026AF4 File Offset: 0x00024CF4
		private void DetermineBindings(DataMember dataMember, bool isSecondary)
		{
			DsdDataBindingGenerator.BindingRange currentRange = this.m_currentRange;
			int num;
			if (!this.m_bindingMetadata.TryGetBindingIndex(dataMember, out num))
			{
				return;
			}
			if (isSecondary && dataMember.IsDynamic && this.m_currentDataShape.HasPrimaryMembers())
			{
				this.m_bindingMapping.AddReusableBinding(num);
			}
			bool flag = false;
			Relationship relationship = null;
			if (num != currentRange.BindingIndex)
			{
				if (currentRange.IsDynamic)
				{
					flag = true;
					List<JoinCondition> list = this.BuildJoinConditions(this.m_plans[num], this.m_plans[currentRange.BindingIndex], dataMember, null);
					relationship = new Relationship(currentRange.BindingIndex, currentRange.CurrentContainedItem, list);
				}
				this.AddBinding(dataMember, num, flag, relationship);
				this.ReplaceRange(dataMember, num, dataMember.IsDynamic);
			}
			else
			{
				if (this.m_bindingMetadata.HasEnforcedBinding(dataMember))
				{
					this.AddBinding(dataMember, num, flag, relationship);
				}
				this.m_currentRange.AddContainedItem(dataMember, dataMember.IsDynamic);
			}
			this.DetermineBindings(dataMember.DataMembers, isSecondary);
			if (!dataMember.ContextOnly && !isSecondary && this.m_annotations.IsLeaf(dataMember) && this.m_currentDataShape.DataRows != null)
			{
				Contract.RetailAssert(this.m_currentDataShape.SecondaryHierarchy != null && this.m_currentDataShape.SecondaryHierarchy.DataMembers != null, "Expected to find secondary data members for intersection");
				DataRow dataRow = this.m_currentDataShape.DataRows[this.m_annotations.GetLeafIndex(dataMember)];
				for (int i = 0; i < dataRow.Intersections.Count; i++)
				{
					this.DetermineBindings(dataRow.Intersections[i], i);
				}
			}
			this.m_currentRange.EndContainedItem();
			if (flag)
			{
				this.m_currentRange = currentRange;
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00026CA8 File Offset: 0x00024EA8
		private void DetermineBindings(DataIntersection dataIntersection, int index)
		{
			int num;
			if (!this.m_bindingMetadata.TryGetBindingIndex(dataIntersection, out num))
			{
				return;
			}
			DataMember secondaryLeaf = this.m_bindingMetadata.GetSecondaryLeaf(index);
			int bindingIndex = this.m_bindingMetadata.GetBindingIndex(secondaryLeaf);
			List<JoinCondition> list = this.BuildJoinConditions(this.m_plans[num], this.m_plans[bindingIndex], dataIntersection, secondaryLeaf);
			DataBinding dataBinding = new DataBinding(num, new Relationship(bindingIndex, secondaryLeaf, list), false);
			this.m_bindingMapping.Add(dataIntersection, dataBinding);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00026D20 File Offset: 0x00024F20
		private void ReplaceRange(IScope startItem, int bindingIndex, bool isDynamic)
		{
			DsdDataBindingGenerator.BindingRange bindingRange = new DsdDataBindingGenerator.BindingRange(startItem, bindingIndex, isDynamic);
			this.m_currentRange = bindingRange;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00026D40 File Offset: 0x00024F40
		private void AddBinding(IScope item, int bindingIndex, bool shouldRestore, Relationship relationship)
		{
			DataBinding dataBinding = new DataBinding(bindingIndex, relationship, shouldRestore);
			this.m_bindingMapping.Add(item, dataBinding);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00026D64 File Offset: 0x00024F64
		private List<JoinCondition> BuildJoinConditions(DataSetPlanInfo plan1, DataSetPlanInfo plan2, IScope joinPoint, DataMember secondaryParent = null)
		{
			List<JoinCondition> list = null;
			bool flag = this.NeedAggregateIndicatorFieldJoinCondition(joinPoint);
			IEnumerable<ContextElement> memberElementsForJoin = DsdDataBindingGenerator.GetMemberElementsForJoin(plan1, joinPoint);
			IEnumerable<ContextElement> memberElementsForJoin2 = DsdDataBindingGenerator.GetMemberElementsForJoin(plan2, joinPoint);
			using (IEnumerator<ContextElement> enumerator = memberElementsForJoin.GetEnumerator())
			{
				using (IEnumerator<ContextElement> enumerator2 = memberElementsForJoin2.GetEnumerator())
				{
					while (enumerator.MoveNext() && enumerator2.MoveNext())
					{
						ContextElement contextElement = enumerator.Current;
						ContextElement contextElement2 = enumerator2.Current;
						DataMember dataMember = (DataMember)contextElement.Content;
						if (contextElement.Content.Id == contextElement2.Content.Id && contextElement.ElementState.ShouldIncludeInQueryOutput() && contextElement2.ElementState.ShouldIncludeInQueryOutput() && !BindingGenerationUtils.IsDuplicateMember(list, dataMember, this.m_expressionTable))
						{
							bool requiresReversedSortDirection = contextElement.RequiresReversedSortDirection;
							JoinCondition joinCondition = null;
							if (joinPoint is DataMember)
							{
								joinCondition = this.BuildJoinConditionForMember(dataMember, joinPoint, requiresReversedSortDirection);
							}
							else if (joinPoint is DataIntersection)
							{
								joinCondition = this.BuildJoinConditionForIntersection(dataMember, joinPoint, requiresReversedSortDirection, secondaryParent, flag);
							}
							else
							{
								Contract.RetailFail("Expected JoinPoint to be DataMember or DataIntersection");
							}
							if (joinCondition != null)
							{
								Util.AddToLazyList<JoinCondition>(ref list, joinCondition);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00026EA8 File Offset: 0x000250A8
		private JoinCondition BuildJoinConditionForMember(DataMember member, IScope joinPoint, bool requiresReversedSortDirections)
		{
			Contract.RetailAssert(this.m_scopeTree.HasScope(joinPoint.Id), "JoinPoint did not have scope");
			if (this.m_scopeTree.IsSameOrParentScope(member, joinPoint))
			{
				return new JoinCondition(member, requiresReversedSortDirections, false);
			}
			return null;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00026EE0 File Offset: 0x000250E0
		private JoinCondition BuildJoinConditionForIntersection(DataMember member, IScope joinPoint, bool requiresReversedSortDirections, DataMember secondaryParent, bool needAggregateIndicatorFieldJoinCondition)
		{
			if (this.m_scopeTree.HasScope(joinPoint.Id) && this.m_scopeTree.IsSameOrParentScope(member, joinPoint))
			{
				return new JoinCondition(member, requiresReversedSortDirections, false);
			}
			if (secondaryParent.IsSubtotal(this.m_annotations.SubtotalAnnotations))
			{
				DataMember peerDynamicForSubtotal = this.GetPeerDynamicForSubtotal(secondaryParent);
				if (this.m_scopeTree.AreSameScope(member, peerDynamicForSubtotal) && needAggregateIndicatorFieldJoinCondition)
				{
					return new JoinCondition(member, requiresReversedSortDirections, true);
				}
			}
			else
			{
				DataMember dataMember = secondaryParent;
				if (!dataMember.IsDynamic)
				{
					dataMember = this.m_bindingMetadata.GetSecondaryDynamicParentForStatic(dataMember);
				}
				if (dataMember != null && this.m_scopeTree.IsSameOrParentScope(member, dataMember))
				{
					return new JoinCondition(member, requiresReversedSortDirections, false);
				}
			}
			return null;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00026F88 File Offset: 0x00025188
		private bool NeedAggregateIndicatorFieldJoinCondition(IScope joinPoint)
		{
			if (joinPoint.ObjectType != ObjectType.DataIntersection)
			{
				return false;
			}
			List<Calculation> calculations = ((DataIntersection)joinPoint).Calculations;
			if (calculations == null)
			{
				return false;
			}
			for (int i = 0; i < calculations.Count; i++)
			{
				if (this.m_annotations.IsSubtotal(calculations[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00026FDC File Offset: 0x000251DC
		private static IEnumerable<ContextElement> GetMemberElementsForJoin(DataSetPlanInfo plan, IContextItem joinPoint)
		{
			return from e in plan.Elements.TakeWhile((ContextElement e) => e.Content.Id != joinPoint.Id)
				where e.Content.ObjectType == ObjectType.DataMember
				select e;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00027034 File Offset: 0x00025234
		private DataMember GetPeerDynamicForSubtotal(DataMember subtotal)
		{
			BatchSubtotalAnnotation batchSubtotalAnnotation;
			if (this.m_annotations.SubtotalAnnotations.TryGetSubtotalSourceAnnotation(subtotal, out batchSubtotalAnnotation))
			{
				return batchSubtotalAnnotation.StopScope as DataMember;
			}
			return null;
		}

		// Token: 0x040004CF RID: 1231
		private readonly ScopeTree m_scopeTree;

		// Token: 0x040004D0 RID: 1232
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x040004D1 RID: 1233
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x040004D2 RID: 1234
		private readonly List<DataSetPlanInfo> m_plans;

		// Token: 0x040004D3 RID: 1235
		private readonly OutputPlanMapping m_outputPlanMapping;

		// Token: 0x040004D4 RID: 1236
		private readonly DataBindingMapping m_bindingMapping;

		// Token: 0x040004D5 RID: 1237
		private DsdDataBindingMetadata m_bindingMetadata;

		// Token: 0x040004D6 RID: 1238
		private DataShapeContext m_dataShapeContext;

		// Token: 0x040004D7 RID: 1239
		private DataShape m_currentDataShape;

		// Token: 0x040004D8 RID: 1240
		private DsdDataBindingGenerator.BindingRange m_currentRange;

		// Token: 0x020002C2 RID: 706
		private struct BindingRange
		{
			// Token: 0x06001632 RID: 5682 RVA: 0x0005113B File Offset: 0x0004F33B
			internal BindingRange(IScope startScope, int bindingIndex, bool isDynamic)
			{
				this._bindingIndex = bindingIndex;
				this._containedItems = new Stack<DsdDataBindingGenerator.BindingItem>();
				this.AddContainedItem(startScope, isDynamic);
			}

			// Token: 0x06001633 RID: 5683 RVA: 0x00051158 File Offset: 0x0004F358
			internal void AddContainedItem(IScope scope, bool isDynamic)
			{
				this._containedItems.Push(new DsdDataBindingGenerator.BindingItem
				{
					Scope = scope,
					IsDynamic = isDynamic
				});
			}

			// Token: 0x06001634 RID: 5684 RVA: 0x00051189 File Offset: 0x0004F389
			internal void EndContainedItem()
			{
				this._containedItems.Pop();
			}

			// Token: 0x170003EE RID: 1006
			// (get) Token: 0x06001635 RID: 5685 RVA: 0x00051197 File Offset: 0x0004F397
			public int BindingIndex
			{
				get
				{
					return this._bindingIndex;
				}
			}

			// Token: 0x170003EF RID: 1007
			// (get) Token: 0x06001636 RID: 5686 RVA: 0x0005119F File Offset: 0x0004F39F
			internal bool IsDynamic
			{
				get
				{
					return this.HasContainedItems && this._containedItems.Peek().IsDynamic;
				}
			}

			// Token: 0x170003F0 RID: 1008
			// (get) Token: 0x06001637 RID: 5687 RVA: 0x000511BB File Offset: 0x0004F3BB
			internal IScope CurrentContainedItem
			{
				get
				{
					if (!this.HasContainedItems)
					{
						return null;
					}
					return this._containedItems.Peek().Scope;
				}
			}

			// Token: 0x170003F1 RID: 1009
			// (get) Token: 0x06001638 RID: 5688 RVA: 0x000511D7 File Offset: 0x0004F3D7
			internal bool HasContainedItems
			{
				get
				{
					return this._containedItems.Count > 0;
				}
			}

			// Token: 0x04000A78 RID: 2680
			private readonly int _bindingIndex;

			// Token: 0x04000A79 RID: 2681
			private readonly Stack<DsdDataBindingGenerator.BindingItem> _containedItems;
		}

		// Token: 0x020002C3 RID: 707
		private struct BindingItem
		{
			// Token: 0x04000A7A RID: 2682
			internal IScope Scope;

			// Token: 0x04000A7B RID: 2683
			internal bool IsDynamic;
		}
	}
}

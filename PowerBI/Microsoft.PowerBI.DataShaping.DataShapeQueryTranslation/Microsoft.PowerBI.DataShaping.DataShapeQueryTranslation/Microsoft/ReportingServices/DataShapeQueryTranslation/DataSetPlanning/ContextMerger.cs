using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000DA RID: 218
	internal sealed class ContextMerger
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x00022D1A File Offset: 0x00020F1A
		internal ContextMerger(ContextWeights contextWeights, DataShapeAnnotations annotations)
		{
			this.m_contextWeights = contextWeights;
			this.m_annotations = annotations;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00022D30 File Offset: 0x00020F30
		public bool MergeContexts(IEnumerable<ContextElement> plan, IEnumerable<ContextElement> context, out List<ContextElement> newContext)
		{
			newContext = new List<ContextElement>();
			StatefulEnumerator<ContextElement> statefulEnumerator = StatefulEnumerator<ContextElement>.CreateAtFirstItem(plan);
			StatefulEnumerator<ContextElement> statefulEnumerator2 = StatefulEnumerator<ContextElement>.CreateAtFirstItem(context);
			ContextMerger.MergeState mergeState = new ContextMerger.MergeState(this.m_annotations);
			while (statefulEnumerator.HasItem && statefulEnumerator2.HasItem)
			{
				ContextElement contextElement = statefulEnumerator.Current;
				ContextElement contextElement2 = statefulEnumerator2.Current;
				if (contextElement.Content.Id == contextElement2.Content.Id)
				{
					if (contextElement.Limit != contextElement2.Limit)
					{
						return false;
					}
					if (!this.AddCommonElement(newContext, mergeState, contextElement, contextElement2))
					{
						return false;
					}
					statefulEnumerator.MoveNext();
					statefulEnumerator2.MoveNext();
				}
				else if (this.m_contextWeights.GetWeight(contextElement.Content) < this.m_contextWeights.GetWeight(contextElement2.Content))
				{
					if (!mergeState.UpdateForAddPlanElement(contextElement))
					{
						return false;
					}
					newContext.Add(contextElement);
					statefulEnumerator.MoveNext();
				}
				else
				{
					if (!mergeState.UpdateForAddContextElement(contextElement2))
					{
						return false;
					}
					newContext.Add(contextElement2);
					statefulEnumerator2.MoveNext();
				}
			}
			return this.AddRemainingPlanElements(statefulEnumerator, mergeState, newContext) && this.AddRemainingContextElements(statefulEnumerator2, mergeState, newContext);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00022E50 File Offset: 0x00021050
		private bool AddCommonElement(List<ContextElement> newElements, ContextMerger.MergeState mergeState, ContextElement planElement, ContextElement contextElement)
		{
			ContextElement contextElement2;
			if (planElement.ElementState == contextElement.ElementState)
			{
				contextElement2 = ContextElement.MergeElements(planElement, contextElement, contextElement.ElementState);
			}
			else if (planElement.ElementState.CanChangeTo(contextElement.ElementState))
			{
				contextElement2 = ContextElement.MergeElements(planElement, contextElement, contextElement.ElementState);
			}
			else if (contextElement.ElementState.CanChangeTo(planElement.ElementState))
			{
				contextElement2 = ContextElement.MergeElements(planElement, contextElement, planElement.ElementState);
			}
			else
			{
				if (!planElement.ElementState.CanChangeTo(ContextState.OutputRollup) || !contextElement.ElementState.CanChangeTo(ContextState.OutputRollup) || this.IsPrimaryHierarchyDynamic(planElement.Content))
				{
					return false;
				}
				contextElement2 = ContextElement.MergeElements(planElement, contextElement, ContextState.OutputRollup);
			}
			if (!mergeState.UpdateForAddCommonElement(contextElement2))
			{
				return false;
			}
			newElements.Add(contextElement2);
			return true;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00022F18 File Offset: 0x00021118
		private bool IsPrimaryHierarchyDynamic(IContextItem item)
		{
			DataMember dataMember = item as DataMember;
			return dataMember != null && dataMember.IsDynamic && this.m_annotations.IsPrimaryMember(dataMember);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00022F48 File Offset: 0x00021148
		private bool AddRemainingPlanElements(StatefulEnumerator<ContextElement> elements, ContextMerger.MergeState mergeState, List<ContextElement> newElements)
		{
			while (elements.HasItem)
			{
				ContextElement contextElement = elements.Current;
				elements.MoveNext();
				if (!mergeState.UpdateForAddPlanElement(contextElement))
				{
					return false;
				}
				newElements.Add(contextElement);
			}
			return true;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00022F80 File Offset: 0x00021180
		private bool AddRemainingContextElements(StatefulEnumerator<ContextElement> elements, ContextMerger.MergeState mergeState, List<ContextElement> newElements)
		{
			while (elements.HasItem)
			{
				ContextElement contextElement = elements.Current;
				elements.MoveNext();
				if (!mergeState.UpdateForAddContextElement(contextElement))
				{
					return false;
				}
				newElements.Add(contextElement);
			}
			return true;
		}

		// Token: 0x04000445 RID: 1093
		private readonly ContextWeights m_contextWeights;

		// Token: 0x04000446 RID: 1094
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x020002B1 RID: 689
		private sealed class MergeState
		{
			// Token: 0x060015F4 RID: 5620 RVA: 0x00050BBF File Offset: 0x0004EDBF
			internal MergeState(DataShapeAnnotations annotations)
			{
				this.m_annotations = annotations;
				this.m_commonState = new ContextMerger.MergeSourceState();
				this.m_planState = new ContextMerger.MergeSourceState();
				this.m_contextState = new ContextMerger.MergeSourceState();
			}

			// Token: 0x060015F5 RID: 5621 RVA: 0x00050BEF File Offset: 0x0004EDEF
			public bool UpdateForAddCommonElement(ContextElement element)
			{
				this.m_commonState.AddedGroup |= this.IsGroup(element);
				this.m_commonState.AddedMeasure |= this.IsMeasure(element);
				return true;
			}

			// Token: 0x060015F6 RID: 5622 RVA: 0x00050C24 File Offset: 0x0004EE24
			public bool UpdateForAddPlanElement(ContextElement element)
			{
				return this.UpdateForAdd(element, this.m_planState, this.m_contextState);
			}

			// Token: 0x060015F7 RID: 5623 RVA: 0x00050C39 File Offset: 0x0004EE39
			public bool UpdateForAddContextElement(ContextElement element)
			{
				return this.UpdateForAdd(element, this.m_contextState, this.m_planState);
			}

			// Token: 0x060015F8 RID: 5624 RVA: 0x00050C50 File Offset: 0x0004EE50
			private bool UpdateForAdd(ContextElement element, ContextMerger.MergeSourceState source, ContextMerger.MergeSourceState other)
			{
				bool flag = this.IsMeasure(element);
				source.AddedMeasure = source.AddedMeasure || flag;
				if (flag && other.AddedGroup)
				{
					return false;
				}
				if (this.IsGroup(element))
				{
					source.AddedGroup |= true;
					if (this.m_commonState.AddedMeasure || other.AddedMeasure || other.AddedGroup)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060015F9 RID: 5625 RVA: 0x00050CB7 File Offset: 0x0004EEB7
			private bool IsGroup(ContextElement element)
			{
				return element.Content.ObjectType == ObjectType.DataMember;
			}

			// Token: 0x060015FA RID: 5626 RVA: 0x00050CC8 File Offset: 0x0004EEC8
			private bool IsMeasure(ContextElement element)
			{
				Calculation calculation = element.Content as Calculation;
				return calculation != null && this.m_annotations.IsMeasure(calculation);
			}

			// Token: 0x04000A4D RID: 2637
			private readonly ContextMerger.MergeSourceState m_commonState;

			// Token: 0x04000A4E RID: 2638
			private readonly ContextMerger.MergeSourceState m_planState;

			// Token: 0x04000A4F RID: 2639
			private readonly ContextMerger.MergeSourceState m_contextState;

			// Token: 0x04000A50 RID: 2640
			private readonly DataShapeAnnotations m_annotations;
		}

		// Token: 0x020002B2 RID: 690
		private sealed class MergeSourceState
		{
			// Token: 0x170003EB RID: 1003
			// (get) Token: 0x060015FB RID: 5627 RVA: 0x00050CF2 File Offset: 0x0004EEF2
			// (set) Token: 0x060015FC RID: 5628 RVA: 0x00050CFA File Offset: 0x0004EEFA
			internal bool AddedMeasure { get; set; }

			// Token: 0x170003EC RID: 1004
			// (get) Token: 0x060015FD RID: 5629 RVA: 0x00050D03 File Offset: 0x0004EF03
			// (set) Token: 0x060015FE RID: 5630 RVA: 0x00050D0B File Offset: 0x0004EF0B
			internal bool AddedGroup { get; set; }
		}
	}
}

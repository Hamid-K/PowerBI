using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x02000102 RID: 258
	[DebuggerDisplay("[Content] Id={Content.Id}, [State={ElementState}]")]
	internal sealed class ContextElement
	{
		// Token: 0x06000A25 RID: 2597 RVA: 0x00027547 File Offset: 0x00025747
		internal ContextElement(IContextItem content, ContextState state)
			: this(content, state, null, null, false)
		{
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00027554 File Offset: 0x00025754
		internal ContextElement(IContextItem content, ContextState state, ReadOnlyCollection<Calculation> rollupCalculations, Limit limit, bool requiresReversedSortDirection)
		{
			this.m_content = content;
			this.m_elementState = state;
			this.m_rollupCalculations = rollupCalculations;
			this.m_limit = limit;
			this.m_requiresReversedSortDirection = requiresReversedSortDirection;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00027584 File Offset: 0x00025784
		public static ContextElement MergeElements(ContextElement first, ContextElement second, ContextState targetState)
		{
			if (first.m_rollupCalculations == null && second.m_rollupCalculations == null)
			{
				return first.ChangeStateTo(targetState);
			}
			if (first.m_rollupCalculations == null)
			{
				return second.ChangeStateTo(targetState);
			}
			if (second.m_rollupCalculations == null)
			{
				return first.ChangeStateTo(targetState);
			}
			int num = first.m_rollupCalculations.Count<Calculation>();
			int num2 = second.m_rollupCalculations.Count<Calculation>();
			Calculation[] array = new Calculation[num + num2];
			for (int i = 0; i < num; i++)
			{
				array[i] = first.m_rollupCalculations[i];
			}
			for (int j = 0; j < num2; j++)
			{
				array[num + j] = second.m_rollupCalculations[j];
			}
			return new ContextElement(first.Content, targetState, array.AsReadOnlyCollection<Calculation>(), first.m_limit, first.m_requiresReversedSortDirection);
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x00027646 File Offset: 0x00025846
		public IContextItem Content
		{
			get
			{
				return this.m_content;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0002764E File Offset: 0x0002584E
		public Limit Limit
		{
			get
			{
				return this.m_limit;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00027656 File Offset: 0x00025856
		public bool RequiresReversedSortDirection
		{
			get
			{
				return this.m_requiresReversedSortDirection;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0002765E File Offset: 0x0002585E
		public ContextState ElementState
		{
			get
			{
				return this.m_elementState;
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00027666 File Offset: 0x00025866
		public ContextElement ChangeStateTo(ContextState state)
		{
			return new ContextElement(this.m_content, state, this.m_rollupCalculations, this.m_limit, this.m_requiresReversedSortDirection);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00027688 File Offset: 0x00025888
		public ContextElement RecordRollupCalculation(Calculation calculation)
		{
			ReadOnlyCollection<Calculation> readOnlyCollection = new ReadOnlyCollection<Calculation>(new Calculation[] { calculation });
			return new ContextElement(this.m_content, this.m_elementState, readOnlyCollection, this.m_limit, this.m_requiresReversedSortDirection);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x000276C3 File Offset: 0x000258C3
		public ContextElement AttachLimit(Limit limit)
		{
			return new ContextElement(this.m_content, this.m_elementState, this.m_rollupCalculations, limit, this.m_requiresReversedSortDirection);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x000276E3 File Offset: 0x000258E3
		public ContextElement ReverseSortDirection()
		{
			return new ContextElement(this.m_content, this.m_elementState, this.m_rollupCalculations, this.m_limit, !this.m_requiresReversedSortDirection);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002770B File Offset: 0x0002590B
		public ContextElement SetContextOnly()
		{
			return new ContextElement(this.m_content, ContextState.ContextOnly, this.m_rollupCalculations, this.m_limit, this.m_requiresReversedSortDirection);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002772C File Offset: 0x0002592C
		public SortDirection GetRollupSortDirection(TranslationErrorContext errorContext, DataShapeAnnotations annotations)
		{
			SortDirection sortDirection = SortDirection.Ascending;
			if (this.m_elementState == ContextState.OutputRollup && this.m_rollupCalculations != null)
			{
				SortDirection? sortDirection2 = null;
				foreach (Calculation calculation in this.m_rollupCalculations)
				{
					SortDirection subtotalSortDirection = annotations.GetSubtotalSortDirection(calculation);
					if (sortDirection2 == null)
					{
						sortDirection2 = new SortDirection?(subtotalSortDirection);
					}
					else if (sortDirection2.Value != subtotalSortDirection)
					{
						errorContext.Register(TranslationMessages.InconsistentSortDirectionForSubtotal(EngineMessageSeverity.Error, this.Content.ObjectType, this.Content.Id, null));
						break;
					}
				}
				if (sortDirection2 != null)
				{
					sortDirection = sortDirection2.Value;
				}
			}
			if (this.m_requiresReversedSortDirection)
			{
				sortDirection = sortDirection.ReverseSortDirection();
			}
			return sortDirection;
		}

		// Token: 0x040004E7 RID: 1255
		private readonly IContextItem m_content;

		// Token: 0x040004E8 RID: 1256
		private readonly ContextState m_elementState;

		// Token: 0x040004E9 RID: 1257
		private readonly ReadOnlyCollection<Calculation> m_rollupCalculations;

		// Token: 0x040004EA RID: 1258
		private readonly Limit m_limit;

		// Token: 0x040004EB RID: 1259
		private readonly bool m_requiresReversedSortDirection;
	}
}

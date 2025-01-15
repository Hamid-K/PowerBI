using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000E2 RID: 226
	internal abstract class ScopePlanElement : DataSetPlanElement
	{
		// Token: 0x06000932 RID: 2354 RVA: 0x00023720 File Offset: 0x00021920
		protected ScopePlanElement(IList<NestedPlanElement> nestedElements, bool isProjected, FilterCondition filterCondition = null, Limit limit = null)
		{
			this.m_nestedPlanElements = nestedElements.AsReadOnlyCollection<NestedPlanElement>();
			this.m_isProjected = isProjected;
			this.m_filterCondition = filterCondition;
			this.m_limit = limit;
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000933 RID: 2355
		public abstract IScope Scope { get; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0002374A File Offset: 0x0002194A
		public ReadOnlyCollection<NestedPlanElement> NestedElements
		{
			get
			{
				return this.m_nestedPlanElements;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00023752 File Offset: 0x00021952
		public bool IsProjected
		{
			get
			{
				return this.m_isProjected;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0002375A File Offset: 0x0002195A
		public FilterCondition FilterCondition
		{
			get
			{
				return this.m_filterCondition;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00023762 File Offset: 0x00021962
		public Limit Limit
		{
			get
			{
				return this.m_limit;
			}
		}

		// Token: 0x06000938 RID: 2360
		public abstract ScopePlanElement OmitProjection();

		// Token: 0x06000939 RID: 2361
		public abstract ScopePlanElement OmitNestedElements();

		// Token: 0x0600093A RID: 2362 RVA: 0x0002376C File Offset: 0x0002196C
		protected List<NestedPlanElement> GetNestedElementsForOmitProjection()
		{
			return (from item in this.NestedElements
				let omitProjectItem = item.OmitProjection()
				where omitProjectItem != null
				select omitProjectItem).ToList<NestedPlanElement>();
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x000237F0 File Offset: 0x000219F0
		protected List<NestedPlanElement> AddToNestedElementCollection(NestedPlanElement element)
		{
			List<NestedPlanElement> list = new List<NestedPlanElement>(this.m_nestedPlanElements.Count + 1);
			list.AddRange(this.m_nestedPlanElements);
			list.Add(element);
			return list;
		}

		// Token: 0x0600093C RID: 2364
		public abstract ScopePlanElement AddNestedPlanElement(NestedPlanElement expression);

		// Token: 0x0600093D RID: 2365 RVA: 0x00023818 File Offset: 0x00021A18
		protected void WriteToBase(StructuredStringBuilder builder)
		{
			builder.WriteAttribute<bool>("IsProjected", this.m_isProjected, true, false);
			builder.WriteProperty<ReadOnlyCollection<NestedPlanElement>>("NestedPlanElements", this.m_nestedPlanElements, false);
			builder.WriteProperty<FilterCondition>("FilterCondition", this.m_filterCondition, false);
			builder.WriteProperty<Limit>("Limit", this.m_limit, false);
		}

		// Token: 0x0400045E RID: 1118
		private readonly ReadOnlyCollection<NestedPlanElement> m_nestedPlanElements;

		// Token: 0x0400045F RID: 1119
		private readonly bool m_isProjected;

		// Token: 0x04000460 RID: 1120
		private readonly FilterCondition m_filterCondition;

		// Token: 0x04000461 RID: 1121
		private readonly Limit m_limit;
	}
}

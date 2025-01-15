using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000EC RID: 236
	internal sealed class DataSetPlanInfo
	{
		// Token: 0x06000991 RID: 2449 RVA: 0x00024667 File Offset: 0x00022867
		internal DataSetPlanInfo(string planName, IEnumerable<ContextElement> elements)
		{
			this.m_planName = planName;
			this.m_elements = new List<ContextElement>(elements);
			this.m_outputItems = new List<IContextItem>();
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002468D File Offset: 0x0002288D
		private DataSetPlanInfo(string planName, int planIndex, List<ContextElement> elements, List<IContextItem> outputItems)
		{
			this.m_planName = planName;
			this.m_planIndex = planIndex;
			this.m_elements = elements;
			this.m_outputItems = outputItems;
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x000246B2 File Offset: 0x000228B2
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x000246BA File Offset: 0x000228BA
		public List<ContextElement> Elements
		{
			get
			{
				return this.m_elements;
			}
			set
			{
				this.m_elements = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x000246C3 File Offset: 0x000228C3
		public string PlanName
		{
			get
			{
				return this.m_planName;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x000246CB File Offset: 0x000228CB
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x000246D3 File Offset: 0x000228D3
		public int PlanIndex
		{
			get
			{
				return this.m_planIndex;
			}
			set
			{
				this.m_planIndex = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x000246DC File Offset: 0x000228DC
		public IEnumerable<IContextItem> OutputItems
		{
			get
			{
				return this.m_outputItems;
			}
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000246E4 File Offset: 0x000228E4
		public void AddOutputItem(IContextItem item)
		{
			this.m_outputItems.Add(item);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x000246F4 File Offset: 0x000228F4
		public void AddOutputItems(IEnumerable<IContextItem> items)
		{
			foreach (IContextItem contextItem in items)
			{
				this.AddOutputItem(contextItem);
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0002473C File Offset: 0x0002293C
		public DataSetPlanInfo ReplaceElements(List<ContextElement> newElements)
		{
			return new DataSetPlanInfo(this.m_planName, this.m_planIndex, newElements, this.m_outputItems);
		}

		// Token: 0x04000480 RID: 1152
		private readonly string m_planName;

		// Token: 0x04000481 RID: 1153
		private readonly List<IContextItem> m_outputItems;

		// Token: 0x04000482 RID: 1154
		private List<ContextElement> m_elements;

		// Token: 0x04000483 RID: 1155
		private int m_planIndex;
	}
}

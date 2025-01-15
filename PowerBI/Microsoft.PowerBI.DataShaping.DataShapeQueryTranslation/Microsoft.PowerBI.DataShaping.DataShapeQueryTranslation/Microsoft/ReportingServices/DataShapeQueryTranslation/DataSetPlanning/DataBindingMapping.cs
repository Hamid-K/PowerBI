using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000FC RID: 252
	internal sealed class DataBindingMapping
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x000261F3 File Offset: 0x000243F3
		internal IEnumerable<KeyValuePair<IDataBoundItem, DataBinding>> ItemBindings
		{
			get
			{
				return this.m_itemBindings;
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x000261FB File Offset: 0x000243FB
		public void Add(IDataBoundItem item, DataBinding binding)
		{
			if (this.m_itemBindings == null)
			{
				this.m_itemBindings = new Dictionary<IDataBoundItem, DataBinding>();
			}
			this.m_itemBindings.Add(item, binding);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0002621D File Offset: 0x0002441D
		public void Add(int planIndex, DataBinding binding)
		{
			if (this.m_planBindings == null)
			{
				this.m_planBindings = new List<DataBinding>();
			}
			this.m_planBindings.SetAtExtendedIndex(planIndex, binding);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00026240 File Offset: 0x00024440
		public bool TryGetValue(IDataBoundItem item, out DataBinding binding)
		{
			binding = null;
			return this.m_itemBindings != null && this.m_itemBindings.TryGetValue(item, out binding);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0002625C File Offset: 0x0002445C
		public bool TryGetValue(int planIndex, out DataBinding binding)
		{
			binding = null;
			if (this.m_planBindings == null || this.m_planBindings.Count <= planIndex)
			{
				return false;
			}
			binding = this.m_planBindings[planIndex];
			return binding != null;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002628C File Offset: 0x0002448C
		public void AddReusableBinding(int index)
		{
			if (this.m_reusableBindingIndices == null)
			{
				this.m_reusableBindingIndices = new HashSet<int>();
			}
			this.m_reusableBindingIndices.Add(index);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x000262AE File Offset: 0x000244AE
		public bool IsReusableBinding(int index)
		{
			return this.m_reusableBindingIndices != null && this.m_reusableBindingIndices.Contains(index);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x000262C6 File Offset: 0x000244C6
		public bool HasReusableBinding()
		{
			return this.m_reusableBindingIndices != null;
		}

		// Token: 0x040004C4 RID: 1220
		private Dictionary<IDataBoundItem, DataBinding> m_itemBindings;

		// Token: 0x040004C5 RID: 1221
		private List<DataBinding> m_planBindings;

		// Token: 0x040004C6 RID: 1222
		private HashSet<int> m_reusableBindingIndices;
	}
}

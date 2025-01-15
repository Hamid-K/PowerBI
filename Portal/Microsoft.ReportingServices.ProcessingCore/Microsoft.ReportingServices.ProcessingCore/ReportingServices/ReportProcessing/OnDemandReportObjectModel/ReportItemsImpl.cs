using System;
using System.Collections;
using System.Threading;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007B6 RID: 1974
	internal sealed class ReportItemsImpl : ReportItems
	{
		// Token: 0x0600701E RID: 28702 RVA: 0x001D3190 File Offset: 0x001D1390
		internal ReportItemsImpl(bool lockAdd)
		{
			this.m_lockAdd = lockAdd;
			this.m_collection = new Hashtable();
			this.m_specialMode = false;
			this.m_specialModeIndex = null;
		}

		// Token: 0x17002638 RID: 9784
		public override ReportItem this[string key]
		{
			get
			{
				if (key == null || this.m_collection == null)
				{
					throw new ReportProcessingException_NonExistingReportItemReference(key);
				}
				if (this.m_specialMode)
				{
					this.m_specialModeIndex = key;
				}
				ReportItem reportItem = this.m_collection[key] as ReportItem;
				if (reportItem == null)
				{
					throw new ReportProcessingException_NonExistingReportItemReference(key);
				}
				return reportItem;
			}
		}

		// Token: 0x17002639 RID: 9785
		// (set) Token: 0x06007020 RID: 28704 RVA: 0x001D3203 File Offset: 0x001D1403
		internal bool SpecialMode
		{
			set
			{
				this.m_specialMode = value;
			}
		}

		// Token: 0x06007021 RID: 28705 RVA: 0x001D320C File Offset: 0x001D140C
		internal void ResetAll()
		{
			foreach (object obj in this.m_collection.Values)
			{
				((ReportItemImpl)obj).Reset();
			}
		}

		// Token: 0x06007022 RID: 28706 RVA: 0x001D3268 File Offset: 0x001D1468
		internal void ResetAll(VariantResult aResult)
		{
			foreach (object obj in this.m_collection.Values)
			{
				((ReportItemImpl)obj).Reset(aResult);
			}
		}

		// Token: 0x06007023 RID: 28707 RVA: 0x001D32C4 File Offset: 0x001D14C4
		internal void Add(ReportItemImpl reportItem)
		{
			try
			{
				if (this.m_lockAdd)
				{
					Monitor.Enter(this.m_collection);
				}
				this.m_collection.Add(reportItem.Name, reportItem);
			}
			finally
			{
				if (this.m_lockAdd)
				{
					Monitor.Exit(this.m_collection);
				}
			}
		}

		// Token: 0x06007024 RID: 28708 RVA: 0x001D331C File Offset: 0x001D151C
		internal void AddAll(ReportItemsImpl reportItems)
		{
			foreach (object obj in reportItems.m_collection.Values)
			{
				ReportItemImpl reportItemImpl = (ReportItemImpl)obj;
				this.Add(reportItemImpl);
			}
		}

		// Token: 0x06007025 RID: 28709 RVA: 0x001D337C File Offset: 0x001D157C
		internal ReportItem GetReportItem(string aName)
		{
			return this.m_collection[aName] as ReportItem;
		}

		// Token: 0x06007026 RID: 28710 RVA: 0x001D338F File Offset: 0x001D158F
		internal string GetSpecialModeIndex()
		{
			string specialModeIndex = this.m_specialModeIndex;
			this.m_specialModeIndex = null;
			return specialModeIndex;
		}

		// Token: 0x040039ED RID: 14829
		private bool m_lockAdd;

		// Token: 0x040039EE RID: 14830
		private Hashtable m_collection;

		// Token: 0x040039EF RID: 14831
		private bool m_specialMode;

		// Token: 0x040039F0 RID: 14832
		private string m_specialModeIndex;
	}
}

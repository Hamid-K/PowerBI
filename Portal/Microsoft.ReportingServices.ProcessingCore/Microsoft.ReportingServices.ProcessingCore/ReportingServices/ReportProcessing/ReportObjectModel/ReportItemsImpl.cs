using System;
using System.Collections;
using System.Threading;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x02000794 RID: 1940
	internal sealed class ReportItemsImpl : ReportItems
	{
		// Token: 0x06006C3D RID: 27709 RVA: 0x001B7053 File Offset: 0x001B5253
		internal ReportItemsImpl()
			: this(false)
		{
		}

		// Token: 0x06006C3E RID: 27710 RVA: 0x001B705C File Offset: 0x001B525C
		internal ReportItemsImpl(bool lockAdd)
		{
			this.m_lockAdd = lockAdd;
			this.m_collection = new Hashtable();
			this.m_specialMode = false;
			this.m_specialModeIndex = null;
		}

		// Token: 0x170025AE RID: 9646
		public override ReportItem this[string key]
		{
			get
			{
				if (key == null || this.m_collection == null)
				{
					throw new ReportProcessingException_NonExistingReportItemReference(key);
				}
				ReportItem reportItem2;
				try
				{
					if (this.m_specialMode)
					{
						this.m_specialModeIndex = key;
					}
					ReportItem reportItem = this.m_collection[key] as ReportItem;
					if (reportItem == null)
					{
						throw new ReportProcessingException_NonExistingReportItemReference(key);
					}
					reportItem2 = reportItem;
				}
				catch
				{
					throw new ReportProcessingException_NonExistingReportItemReference(key);
				}
				return reportItem2;
			}
		}

		// Token: 0x170025AF RID: 9647
		// (set) Token: 0x06006C40 RID: 27712 RVA: 0x001B70EC File Offset: 0x001B52EC
		internal bool SpecialMode
		{
			set
			{
				this.m_specialMode = value;
			}
		}

		// Token: 0x06006C41 RID: 27713 RVA: 0x001B70F8 File Offset: 0x001B52F8
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

		// Token: 0x06006C42 RID: 27714 RVA: 0x001B7150 File Offset: 0x001B5350
		internal string GetSpecialModeIndex()
		{
			string specialModeIndex = this.m_specialModeIndex;
			this.m_specialModeIndex = null;
			return specialModeIndex;
		}

		// Token: 0x0400365E RID: 13918
		private bool m_lockAdd;

		// Token: 0x0400365F RID: 13919
		private Hashtable m_collection;

		// Token: 0x04003660 RID: 13920
		private bool m_specialMode;

		// Token: 0x04003661 RID: 13921
		private string m_specialModeIndex;

		// Token: 0x04003662 RID: 13922
		internal const string Name = "ReportItems";

		// Token: 0x04003663 RID: 13923
		internal const string FullName = "Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.ReportItems";
	}
}

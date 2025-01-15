using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200001F RID: 31
	public sealed class PageSection : ReportItem
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x00009C5C File Offset: 0x00007E5C
		internal PageSection(string uniqueName, PageSection pageSectionDef, PageSectionInstance pageSectionInstance, Report report, RenderingContext renderingContext, bool pageDef)
			: base(uniqueName, 0, pageSectionDef, pageSectionInstance, renderingContext)
		{
			this.m_pageSectionDef = pageSectionDef;
			this.m_pageSectionInstance = pageSectionInstance;
			this.m_pageDef = pageDef;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00009C80 File Offset: 0x00007E80
		public ReportItem Find(string uniqueName)
		{
			if (uniqueName == null || uniqueName.Length <= 0)
			{
				return null;
			}
			if (uniqueName.Equals(base.UniqueName))
			{
				return this;
			}
			char[] array = new char[] { 'a' };
			string[] array2 = uniqueName.Split(array);
			if (array2 == null || array2.Length < 2)
			{
				return null;
			}
			object obj = ((this.m_pageSectionInstance != null) ? this.m_pageSectionInstance : this.m_pageSectionDef);
			NonComputedUniqueNames nonComputedUniqueNames = null;
			for (int i = 1; i < array2.Length; i++)
			{
				IIndexInto indexInto = obj as IIndexInto;
				if (indexInto == null)
				{
					obj = null;
					break;
				}
				int num = ReportItem.StringToInt(array2[i]);
				NonComputedUniqueNames nonComputedUniqueNames2 = null;
				obj = indexInto.GetChildAt(num, out nonComputedUniqueNames2);
				if (nonComputedUniqueNames == null)
				{
					nonComputedUniqueNames = nonComputedUniqueNames2;
				}
				else
				{
					if (nonComputedUniqueNames.ChildrenUniqueNames == null || num < 0 || num >= nonComputedUniqueNames.ChildrenUniqueNames.Length)
					{
						return null;
					}
					nonComputedUniqueNames = nonComputedUniqueNames.ChildrenUniqueNames[num];
				}
			}
			if (obj == null)
			{
				return null;
			}
			ReportItem reportItem2;
			if (obj is ReportItem)
			{
				ReportItem reportItem = (ReportItem)obj;
				reportItem2 = ReportItem.CreateItem(uniqueName, reportItem, null, base.RenderingContext, nonComputedUniqueNames);
			}
			else
			{
				ReportItemInstance reportItemInstance = (ReportItemInstance)obj;
				reportItem2 = ReportItem.CreateItem(uniqueName, reportItemInstance.ReportItemDef, reportItemInstance, base.RenderingContext, nonComputedUniqueNames);
			}
			return reportItem2;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00009D9C File Offset: 0x00007F9C
		internal override bool Search(SearchContext searchContext)
		{
			ReportItemCollection reportItemCollection = this.ReportItemCollection;
			return reportItemCollection != null && reportItemCollection.Search(searchContext);
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00009DBC File Offset: 0x00007FBC
		public bool PrintOnFirstPage
		{
			get
			{
				return ((PageSection)base.ReportItemDef).PrintOnFirstPage;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00009DCE File Offset: 0x00007FCE
		public bool PrintOnLastPage
		{
			get
			{
				return ((PageSection)base.ReportItemDef).PrintOnLastPage;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00009DE0 File Offset: 0x00007FE0
		public ReportItemCollection ReportItemCollection
		{
			get
			{
				ReportItemCollection reportItemCollection = this.m_reportItems;
				if (this.m_reportItems == null)
				{
					reportItemCollection = new ReportItemCollection(this.m_pageSectionDef.ReportItems, (this.m_pageSectionInstance == null) ? null : this.m_pageSectionInstance.ReportItemColInstance, base.RenderingContext, null);
					if (base.RenderingContext.CacheState)
					{
						this.m_reportItems = reportItemCollection;
					}
				}
				return reportItemCollection;
			}
		}

		// Token: 0x0400007B RID: 123
		private ReportItemCollection m_reportItems;

		// Token: 0x0400007C RID: 124
		private PageSection m_pageSectionDef;

		// Token: 0x0400007D RID: 125
		private bool m_pageDef;

		// Token: 0x0400007E RID: 126
		private PageSectionInstance m_pageSectionInstance;

		// Token: 0x0400007F RID: 127
		internal const string PageHeaderUniqueNamePrefix = "ph";

		// Token: 0x04000080 RID: 128
		internal const string PageFooterUniqueNamePrefix = "pf";
	}
}

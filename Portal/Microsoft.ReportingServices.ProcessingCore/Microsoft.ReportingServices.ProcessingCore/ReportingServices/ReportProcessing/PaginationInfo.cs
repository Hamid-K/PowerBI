using System;
using System.Collections;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000756 RID: 1878
	[Serializable]
	internal sealed class PaginationInfo
	{
		// Token: 0x06006828 RID: 26664 RVA: 0x00195A9A File Offset: 0x00193C9A
		internal PaginationInfo()
		{
			this.m_pages = new ArrayList();
		}

		// Token: 0x170024D1 RID: 9425
		// (get) Token: 0x06006829 RID: 26665 RVA: 0x00195AAD File Offset: 0x00193CAD
		// (set) Token: 0x0600682A RID: 26666 RVA: 0x00195AB5 File Offset: 0x00193CB5
		internal int TotalPageNumber
		{
			get
			{
				return this.m_totalPageNumber;
			}
			set
			{
				this.m_totalPageNumber = value;
			}
		}

		// Token: 0x170024D2 RID: 9426
		internal Page this[int pageNumber]
		{
			get
			{
				return (Page)this.m_pages[pageNumber];
			}
			set
			{
				this.m_pages[pageNumber] = value;
			}
		}

		// Token: 0x170024D3 RID: 9427
		// (get) Token: 0x0600682D RID: 26669 RVA: 0x00195AE0 File Offset: 0x00193CE0
		internal int CurrentPageCount
		{
			get
			{
				return this.m_pages.Count;
			}
		}

		// Token: 0x0600682E RID: 26670 RVA: 0x00195AED File Offset: 0x00193CED
		internal void AddPage(Page page)
		{
			this.m_pages.Add(page);
		}

		// Token: 0x0600682F RID: 26671 RVA: 0x00195AFC File Offset: 0x00193CFC
		internal void Clear()
		{
			this.m_pages.Clear();
		}

		// Token: 0x06006830 RID: 26672 RVA: 0x00195B09 File Offset: 0x00193D09
		internal void InsertPage(int pageNumber, Page page)
		{
			this.m_pages.Insert(pageNumber, page);
		}

		// Token: 0x06006831 RID: 26673 RVA: 0x00195B18 File Offset: 0x00193D18
		internal void RemovePage(int pageNumber)
		{
			this.m_pages.RemoveAt(pageNumber);
		}

		// Token: 0x04003386 RID: 13190
		private ArrayList m_pages;

		// Token: 0x04003387 RID: 13191
		private int m_totalPageNumber;
	}
}

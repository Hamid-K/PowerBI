using System;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000018 RID: 24
	internal sealed class SearchContext
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x0000946A File Offset: 0x0000766A
		internal SearchContext(int searchPage, string findValue, int itemStartPage, int itemEndPage)
		{
			this.m_searchPage = searchPage;
			this.m_findValue = findValue;
			this.m_itemStartPage = itemStartPage;
			this.m_itemEndPage = itemEndPage;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x000094A4 File Offset: 0x000076A4
		internal SearchContext(SearchContext copy)
		{
			this.m_searchPage = copy.SearchPage;
			this.m_findValue = copy.FindValue;
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x000094D9 File Offset: 0x000076D9
		internal int SearchPage
		{
			get
			{
				return this.m_searchPage;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x000094E1 File Offset: 0x000076E1
		internal string FindValue
		{
			get
			{
				return this.m_findValue;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x000094E9 File Offset: 0x000076E9
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x000094F1 File Offset: 0x000076F1
		internal int ItemStartPage
		{
			get
			{
				return this.m_itemStartPage;
			}
			set
			{
				this.m_itemStartPage = value;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x000094FA File Offset: 0x000076FA
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x00009502 File Offset: 0x00007702
		internal int ItemEndPage
		{
			get
			{
				return this.m_itemEndPage;
			}
			set
			{
				this.m_itemEndPage = value;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000950B File Offset: 0x0000770B
		internal bool IsItemOnSearchPage
		{
			get
			{
				return this.m_itemStartPage <= this.m_searchPage && this.m_searchPage <= this.m_itemEndPage;
			}
		}

		// Token: 0x04000061 RID: 97
		private int m_searchPage = -1;

		// Token: 0x04000062 RID: 98
		private int m_itemStartPage = -1;

		// Token: 0x04000063 RID: 99
		private int m_itemEndPage = -1;

		// Token: 0x04000064 RID: 100
		private string m_findValue;
	}
}

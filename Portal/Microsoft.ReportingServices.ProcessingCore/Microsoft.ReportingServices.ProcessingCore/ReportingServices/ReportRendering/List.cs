using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000031 RID: 49
	internal sealed class List : DataRegion
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x0000E4A6 File Offset: 0x0000C6A6
		internal List(int intUniqueName, List reportItemDef, ListInstance reportItemInstance, RenderingContext renderingContext)
			: base(intUniqueName, reportItemDef, reportItemInstance, renderingContext)
		{
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		public override bool PageBreakAtEnd
		{
			get
			{
				if (((List)base.ReportItemDef).Grouping == null)
				{
					return ((List)base.ReportItemDef).PageBreakAtEnd;
				}
				return ((List)base.ReportItemDef).PageBreakAtEnd || ((List)base.ReportItemDef).Grouping.PageBreakAtEnd;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0000E510 File Offset: 0x0000C710
		public override bool PageBreakAtStart
		{
			get
			{
				if (((List)base.ReportItemDef).Grouping == null)
				{
					return ((List)base.ReportItemDef).PageBreakAtStart;
				}
				return ((List)base.ReportItemDef).PageBreakAtStart || ((List)base.ReportItemDef).Grouping.PageBreakAtStart;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000E569 File Offset: 0x0000C769
		public bool GroupBreakAtStart
		{
			get
			{
				return this.Contents[0].PageBreakAtStart;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000E57C File Offset: 0x0000C77C
		public bool GroupBreakAtEnd
		{
			get
			{
				return this.Contents[0].PageBreakAtEnd;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000E590 File Offset: 0x0000C790
		public ListContentCollection Contents
		{
			get
			{
				ListContentCollection listContentCollection = this.m_contents;
				if (this.m_contents == null)
				{
					listContentCollection = new ListContentCollection(this);
					if (base.RenderingContext.CacheState)
					{
						this.m_contents = listContentCollection;
					}
				}
				return listContentCollection;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000E5C8 File Offset: 0x0000C7C8
		public override bool NoRows
		{
			get
			{
				return base.ReportItemInstance == null || ((ListInstance)base.ReportItemInstance).ListContents.Count == 0;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000E5EC File Offset: 0x0000C7EC
		internal override string InstanceInfoNoRowMessage
		{
			get
			{
				if (base.InstanceInfo != null)
				{
					return ((ListInstanceInfo)base.InstanceInfo).NoRows;
				}
				return null;
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000E608 File Offset: 0x0000C808
		public bool IsListContentOnThisPage(int contentIndex, int pageNumber, int listStartPage, out int startPage, out int endPage)
		{
			startPage = -1;
			endPage = -1;
			RenderingPagesRangesList childrenStartAndEndPages = ((ListInstance)base.ReportItemInstance).ChildrenStartAndEndPages;
			if (childrenStartAndEndPages == null)
			{
				return true;
			}
			if (((List)base.ReportItemInstance.ReportItemDef).Grouping == null)
			{
				pageNumber -= listStartPage;
				Global.Tracer.Assert(pageNumber >= 0 && pageNumber < childrenStartAndEndPages.Count);
				RenderingPagesRanges renderingPagesRanges = childrenStartAndEndPages[pageNumber];
				startPage = pageNumber;
				endPage = pageNumber;
				return contentIndex >= renderingPagesRanges.StartRow && contentIndex < renderingPagesRanges.StartRow + renderingPagesRanges.NumberOfDetails;
			}
			Global.Tracer.Assert(contentIndex >= 0 && contentIndex < childrenStartAndEndPages.Count);
			if (contentIndex >= childrenStartAndEndPages.Count)
			{
				return false;
			}
			RenderingPagesRanges renderingPagesRanges2 = childrenStartAndEndPages[contentIndex];
			startPage = renderingPagesRanges2.StartPage;
			endPage = renderingPagesRanges2.EndPage;
			return pageNumber >= startPage && pageNumber <= endPage;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000E6EC File Offset: 0x0000C8EC
		public void GetListContentOnPage(int page, int listStartPage, out int startChild, out int endChild)
		{
			startChild = -1;
			endChild = -1;
			if (base.ReportItemInstance == null)
			{
				return;
			}
			RenderingPagesRangesList childrenStartAndEndPages = ((ListInstance)base.ReportItemInstance).ChildrenStartAndEndPages;
			if (childrenStartAndEndPages == null)
			{
				return;
			}
			if (((List)base.ReportItemInstance.ReportItemDef).Grouping != null)
			{
				RenderingContext.FindRange(childrenStartAndEndPages, 0, childrenStartAndEndPages.Count - 1, page, ref startChild, ref endChild);
				return;
			}
			if (childrenStartAndEndPages == null)
			{
				return;
			}
			page -= listStartPage;
			Global.Tracer.Assert(page >= 0 && page < childrenStartAndEndPages.Count);
			RenderingPagesRanges renderingPagesRanges = childrenStartAndEndPages[page];
			startChild = renderingPagesRanges.StartRow;
			endChild = startChild + renderingPagesRanges.NumberOfDetails - 1;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000E78C File Offset: 0x0000C98C
		internal override bool Search(SearchContext searchContext)
		{
			if (base.SkipSearch || this.NoRows)
			{
				return false;
			}
			ListContentCollection contents = this.Contents;
			bool flag;
			if (searchContext.ItemStartPage != searchContext.ItemEndPage)
			{
				int num = 0;
				int num2 = 0;
				SearchContext searchContext2 = new SearchContext(searchContext);
				this.GetListContentOnPage(searchContext.SearchPage, searchContext.ItemStartPage, out num, out num2);
				int num3;
				int num4;
				this.IsListContentOnThisPage(num, searchContext.SearchPage, searchContext.ItemStartPage, out num3, out num4);
				searchContext2.ItemStartPage = num3;
				searchContext2.ItemEndPage = num4;
				flag = List.SearchPartialList(contents, searchContext2, num, num);
				num++;
				if (!flag && num < num2)
				{
					searchContext2.ItemStartPage = searchContext.SearchPage;
					searchContext2.ItemEndPage = searchContext.SearchPage;
					flag = List.SearchPartialList(contents, searchContext2, num, num2 - 1);
					num = num2;
				}
				if (!flag && num == num2)
				{
					this.IsListContentOnThisPage(num2, searchContext.SearchPage, searchContext.ItemStartPage, out num3, out num4);
					searchContext2.ItemStartPage = num3;
					searchContext2.ItemEndPage = num4;
					flag = List.SearchPartialList(contents, searchContext2, num2, num2);
				}
			}
			else
			{
				flag = List.SearchFullList(contents, searchContext);
			}
			return flag;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000E898 File Offset: 0x0000CA98
		internal static bool SearchPartialList(ListContentCollection contents, SearchContext searchContext, int startChild, int endChild)
		{
			if (contents == null)
			{
				return false;
			}
			bool flag = false;
			while (startChild <= endChild && !flag)
			{
				flag = contents[startChild].ReportItemCollection.Search(searchContext);
				startChild++;
			}
			return flag;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000E8D0 File Offset: 0x0000CAD0
		internal static bool SearchFullList(ListContentCollection contents, SearchContext searchContext)
		{
			if (contents == null)
			{
				return false;
			}
			bool flag = false;
			int num = 0;
			while (num < contents.Count && !flag)
			{
				ListContent listContent = contents[num];
				if (!listContent.Hidden)
				{
					flag = listContent.ReportItemCollection.Search(searchContext);
				}
				num++;
			}
			return flag;
		}

		// Token: 0x040000EE RID: 238
		private ListContentCollection m_contents;
	}
}

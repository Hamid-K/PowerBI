using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000025 RID: 37
	public sealed class ReportItemCollection
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x0000C110 File Offset: 0x0000A310
		internal ReportItemCollection(ReportItemCollection reportItemColDef, ReportItemColInstance reportItemColInstance, RenderingContext renderingContext, NonComputedUniqueNames[] childrenNonComputedUniqueNames)
		{
			if (reportItemColInstance != null)
			{
				ReportItemColInstanceInfo instanceInfo = reportItemColInstance.GetInstanceInfo(renderingContext.ChunkManager, renderingContext.InPageSection);
				Global.Tracer.Assert(childrenNonComputedUniqueNames == null || instanceInfo.ChildrenNonComputedUniqueNames == null);
				if (childrenNonComputedUniqueNames == null)
				{
					childrenNonComputedUniqueNames = instanceInfo.ChildrenNonComputedUniqueNames;
				}
			}
			this.m_childrenNonComputedUniqueNames = childrenNonComputedUniqueNames;
			this.m_reportItemColInstance = reportItemColInstance;
			this.m_reportItemColDef = reportItemColDef;
			this.m_renderingContext = renderingContext;
		}

		// Token: 0x17000368 RID: 872
		public ReportItem this[int index]
		{
			get
			{
				if (0 > index || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				ReportItem reportItem2;
				if (this.m_reportItems == null || this.m_reportItems[index] == null)
				{
					int num = 0;
					bool flag = false;
					ReportItem reportItem = null;
					this.m_reportItemColDef.GetReportItem(index, out flag, out num, out reportItem);
					NonComputedUniqueNames nonComputedUniqueNames = null;
					ReportItemInstance reportItemInstance = null;
					if (!flag)
					{
						if (this.m_childrenNonComputedUniqueNames != null)
						{
							nonComputedUniqueNames = this.m_childrenNonComputedUniqueNames[num];
						}
					}
					else if (this.m_reportItemColInstance != null)
					{
						reportItemInstance = this.m_reportItemColInstance[num];
					}
					reportItem2 = ReportItem.CreateItem(index, reportItem, reportItemInstance, this.m_renderingContext, nonComputedUniqueNames);
					if (this.m_renderingContext.CacheState)
					{
						if (this.m_reportItems == null)
						{
							this.m_reportItems = new ReportItem[this.Count];
						}
						this.m_reportItems[index] = reportItem2;
					}
				}
				else
				{
					reportItem2 = this.m_reportItems[index];
				}
				return reportItem2;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000C276 File Offset: 0x0000A476
		public int Count
		{
			get
			{
				return this.m_reportItemColDef.Count;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000C283 File Offset: 0x0000A483
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000C2AA File Offset: 0x0000A4AA
		public object SharedRenderingInfo
		{
			get
			{
				return this.m_renderingContext.RenderingInfoManager.SharedRenderingInfo[this.m_reportItemColDef.ID];
			}
			set
			{
				this.m_renderingContext.RenderingInfoManager.SharedRenderingInfo[this.m_reportItemColDef.ID] = value;
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000C2D4 File Offset: 0x0000A4D4
		public void GetReportItemStartAndEndPages(int currentPage, int index, out int startPage, out int endPage)
		{
			if (0 > index || index >= this.Count)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
			}
			startPage = currentPage;
			endPage = currentPage;
			if (this.m_reportItemColInstance != null)
			{
				this.m_reportItemColInstance.GetReportItemStartAndEndPages(index, ref startPage, ref endPage);
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000C340 File Offset: 0x0000A540
		internal bool Search(SearchContext searchContext)
		{
			if (this.m_reportItemColDef.Count == 0)
			{
				return false;
			}
			bool flag = false;
			int num = 0;
			int num2 = 0;
			SearchContext searchContext2 = new SearchContext(searchContext);
			int num3 = 0;
			while (!flag && num3 < this.m_reportItemColDef.Count)
			{
				ReportItem reportItem = this[num3];
				if (searchContext.ItemStartPage != searchContext.ItemEndPage)
				{
					this.GetReportItemStartAndEndPages(searchContext.SearchPage, num3, out num, out num2);
					searchContext2.ItemStartPage = num;
					searchContext2.ItemEndPage = num2;
					if (searchContext2.IsItemOnSearchPage)
					{
						flag = this.SearchRepeatedSiblings(reportItem as DataRegion, searchContext2);
						if (!flag)
						{
							flag = reportItem.Search(searchContext2);
						}
					}
				}
				else
				{
					flag = reportItem.Search(searchContext2);
				}
				num3++;
			}
			return flag;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
		private bool SearchRepeatedSiblings(DataRegion dataRegion, SearchContext searchContext)
		{
			if (dataRegion == null)
			{
				return false;
			}
			bool flag = false;
			int[] repeatSiblings = dataRegion.GetRepeatSiblings();
			if (repeatSiblings != null)
			{
				SearchContext searchContext2 = new SearchContext(searchContext);
				int num = 0;
				while (!flag && num < repeatSiblings.Length)
				{
					int num2 = repeatSiblings[num];
					flag = this[num2].Search(searchContext2);
					num++;
				}
			}
			return flag;
		}

		// Token: 0x040000BF RID: 191
		private ReportItem[] m_reportItems;

		// Token: 0x040000C0 RID: 192
		private ReportItemCollection m_reportItemColDef;

		// Token: 0x040000C1 RID: 193
		private ReportItemColInstance m_reportItemColInstance;

		// Token: 0x040000C2 RID: 194
		private NonComputedUniqueNames[] m_childrenNonComputedUniqueNames;

		// Token: 0x040000C3 RID: 195
		private RenderingContext m_renderingContext;
	}
}

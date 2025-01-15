using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000721 RID: 1825
	[Serializable]
	internal sealed class ReportItemColInstance : InstanceInfoOwner, ISearchByUniqueName, IIndexInto
	{
		// Token: 0x060065D6 RID: 26070 RVA: 0x00190444 File Offset: 0x0018E644
		internal ReportItemColInstance(ReportProcessing.ProcessingContext pc, ReportItemCollection reportItemsDef)
		{
			this.m_reportItemColDef = reportItemsDef;
			if (reportItemsDef.ComputedReportItems != null)
			{
				this.m_reportItemInstances = new ReportItemInstanceList(reportItemsDef.ComputedReportItems.Count);
			}
			if (pc != null)
			{
				this.m_childrenNonComputedUniqueNames = NonComputedUniqueNames.CreateNonComputedUniqueNames(pc, reportItemsDef);
			}
			this.m_instanceInfo = new ReportItemColInstanceInfo(pc, reportItemsDef, this);
		}

		// Token: 0x060065D7 RID: 26071 RVA: 0x0019049A File Offset: 0x0018E69A
		internal ReportItemColInstance()
		{
		}

		// Token: 0x1700240D RID: 9229
		// (get) Token: 0x060065D8 RID: 26072 RVA: 0x001904A2 File Offset: 0x0018E6A2
		// (set) Token: 0x060065D9 RID: 26073 RVA: 0x001904AA File Offset: 0x0018E6AA
		internal ReportItemInstanceList ReportItemInstances
		{
			get
			{
				return this.m_reportItemInstances;
			}
			set
			{
				this.m_reportItemInstances = value;
			}
		}

		// Token: 0x1700240E RID: 9230
		// (get) Token: 0x060065DA RID: 26074 RVA: 0x001904B3 File Offset: 0x0018E6B3
		// (set) Token: 0x060065DB RID: 26075 RVA: 0x001904BB File Offset: 0x0018E6BB
		internal ReportItemCollection ReportItemColDef
		{
			get
			{
				return this.m_reportItemColDef;
			}
			set
			{
				this.m_reportItemColDef = value;
			}
		}

		// Token: 0x1700240F RID: 9231
		internal ReportItemInstance this[int index]
		{
			get
			{
				return this.m_reportItemInstances[index];
			}
		}

		// Token: 0x17002410 RID: 9232
		// (get) Token: 0x060065DD RID: 26077 RVA: 0x001904D2 File Offset: 0x0018E6D2
		// (set) Token: 0x060065DE RID: 26078 RVA: 0x001904DA File Offset: 0x0018E6DA
		internal NonComputedUniqueNames[] ChildrenNonComputedUniqueNames
		{
			get
			{
				return this.m_childrenNonComputedUniqueNames;
			}
			set
			{
				this.m_childrenNonComputedUniqueNames = value;
			}
		}

		// Token: 0x17002411 RID: 9233
		// (get) Token: 0x060065DF RID: 26079 RVA: 0x001904E3 File Offset: 0x0018E6E3
		// (set) Token: 0x060065E0 RID: 26080 RVA: 0x001904EB File Offset: 0x0018E6EB
		internal RenderingPagesRangesList ChildrenStartAndEndPages
		{
			get
			{
				return this.m_childrenStartAndEndPages;
			}
			set
			{
				this.m_childrenStartAndEndPages = value;
			}
		}

		// Token: 0x060065E1 RID: 26081 RVA: 0x001904F4 File Offset: 0x0018E6F4
		internal void Add(ReportItemInstance riInstance)
		{
			Global.Tracer.Assert(this.m_reportItemInstances != null);
			this.m_reportItemInstances.Add(riInstance);
		}

		// Token: 0x060065E2 RID: 26082 RVA: 0x00190518 File Offset: 0x0018E718
		internal int GetReportItemUniqueName(int index)
		{
			ReportItem reportItem = null;
			Global.Tracer.Assert(index >= 0 && index < this.m_reportItemColDef.Count);
			bool flag;
			int num;
			this.m_reportItemColDef.GetReportItem(index, out flag, out num, out reportItem);
			int num2;
			if (!flag)
			{
				num2 = this.m_childrenNonComputedUniqueNames[num].UniqueName;
			}
			else
			{
				num2 = this.m_reportItemInstances[num].UniqueName;
			}
			return num2;
		}

		// Token: 0x060065E3 RID: 26083 RVA: 0x00190580 File Offset: 0x0018E780
		internal void GetReportItemStartAndEndPages(int index, ref int startPage, ref int endPage)
		{
			Global.Tracer.Assert(index >= 0 && index < this.m_reportItemColDef.Count);
			if (this.m_childrenStartAndEndPages == null)
			{
				return;
			}
			RenderingPagesRanges renderingPagesRanges = this.m_childrenStartAndEndPages[index];
			startPage = renderingPagesRanges.StartPage;
			endPage = renderingPagesRanges.EndPage;
		}

		// Token: 0x060065E4 RID: 26084 RVA: 0x001905D4 File Offset: 0x0018E7D4
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfoOwner, new MemberInfoList
			{
				new MemberInfo(MemberName.ReportItemInstances, ObjectType.ReportItemInstanceList),
				new MemberInfo(MemberName.ChildrenStartAndEndPages, ObjectType.RenderingPagesRangesList)
			});
		}

		// Token: 0x060065E5 RID: 26085 RVA: 0x00190618 File Offset: 0x0018E818
		object ISearchByUniqueName.Find(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			int count = this.m_reportItemColDef.Count;
			for (int i = 0; i < count; i++)
			{
				bool flag;
				int num;
				ReportItem reportItem;
				this.m_reportItemColDef.GetReportItem(i, out flag, out num, out reportItem);
				if (flag)
				{
					object obj = ((ISearchByUniqueName)this.m_reportItemInstances[num]).Find(targetUniqueName, ref nonCompNames, chunkManager);
					if (obj != null)
					{
						return obj;
					}
				}
				else
				{
					NonComputedUniqueNames nonComputedUniqueNames = this.GetInstanceInfo(chunkManager, false).ChildrenNonComputedUniqueNames[num];
					object obj = ((ISearchByUniqueName)reportItem).Find(targetUniqueName, ref nonComputedUniqueNames, chunkManager);
					if (obj != null)
					{
						nonCompNames = nonComputedUniqueNames;
						return obj;
					}
				}
			}
			return null;
		}

		// Token: 0x060065E6 RID: 26086 RVA: 0x00190698 File Offset: 0x0018E898
		object IIndexInto.GetChildAt(int index, out NonComputedUniqueNames nonCompNames)
		{
			bool flag;
			int num;
			ReportItem reportItem;
			this.m_reportItemColDef.GetReportItem(index, out flag, out num, out reportItem);
			if (flag)
			{
				nonCompNames = null;
				return this.m_reportItemInstances[num];
			}
			nonCompNames = ((ReportItemColInstanceInfo)this.m_instanceInfo).ChildrenNonComputedUniqueNames[num];
			return reportItem;
		}

		// Token: 0x060065E7 RID: 26087 RVA: 0x001906E0 File Offset: 0x0018E8E0
		internal ReportItemColInstanceInfo GetInstanceInfo(ChunkManager.RenderingChunkManager chunkManager, bool inPageSection)
		{
			if (this.m_instanceInfo is OffsetInfo)
			{
				Global.Tracer.Assert(chunkManager != null);
				IntermediateFormatReader intermediateFormatReader;
				if (inPageSection)
				{
					intermediateFormatReader = chunkManager.GetPageSectionInstanceReader(((OffsetInfo)this.m_instanceInfo).Offset);
				}
				else
				{
					intermediateFormatReader = chunkManager.GetReader(((OffsetInfo)this.m_instanceInfo).Offset);
				}
				return intermediateFormatReader.ReadReportItemColInstanceInfo();
			}
			return (ReportItemColInstanceInfo)this.m_instanceInfo;
		}

		// Token: 0x060065E8 RID: 26088 RVA: 0x00190750 File Offset: 0x0018E950
		internal void SetPaginationForNonComputedChild(ReportProcessing.Pagination pagination, ReportItem reportItem, ReportItem parentDef)
		{
			ReportItemCollection reportItemColDef = this.m_reportItemColDef;
			int num = parentDef.StartPage;
			if (parentDef is Table)
			{
				num = ((Table)parentDef).CurrentPage;
			}
			else if (parentDef is Matrix)
			{
				num = ((Matrix)parentDef).CurrentPage;
			}
			reportItem.TopInStartPage = parentDef.TopInStartPage;
			if (reportItem.SiblingAboveMe != null)
			{
				for (int i = 0; i < reportItem.SiblingAboveMe.Count; i++)
				{
					ReportItem reportItem2 = reportItemColDef[reportItem.SiblingAboveMe[i]];
					int num2 = reportItem2.EndPage;
					if (reportItem2.TopValue + reportItem2.HeightValue > reportItem.TopValue)
					{
						num2 = reportItem2.StartPage;
						reportItem.TopInStartPage = reportItem2.TopInStartPage;
					}
					else
					{
						bool flag = reportItem2.ShareMyLastPage;
						if (!(reportItem2 is Table) && !(reportItem2 is Matrix))
						{
							flag = false;
						}
						if (!pagination.IgnorePageBreak && pagination.PageBreakAtEnd(reportItem2) && !flag)
						{
							num2++;
							if (i == reportItem.SiblingAboveMe.Count - 1)
							{
								pagination.SetCurrentPageHeight(reportItem, reportItem.HeightValue);
								reportItem.TopInStartPage = 0.0;
							}
						}
						else
						{
							reportItem.TopInStartPage = pagination.CurrentPageHeight;
						}
					}
					num = Math.Max(num, num2);
				}
			}
			if (pagination.CanMoveToNextPage(pagination.PageBreakAtStart(reportItem)))
			{
				num++;
				reportItem.TopInStartPage = 0.0;
				pagination.SetCurrentPageHeight(reportItem, reportItem.HeightValue);
			}
			reportItem.StartPage = num;
			reportItem.EndPage = num;
		}

		// Token: 0x040032D9 RID: 13017
		private ReportItemInstanceList m_reportItemInstances;

		// Token: 0x040032DA RID: 13018
		private RenderingPagesRangesList m_childrenStartAndEndPages;

		// Token: 0x040032DB RID: 13019
		[Reference]
		private ReportItemCollection m_reportItemColDef;

		// Token: 0x040032DC RID: 13020
		[NonSerialized]
		private NonComputedUniqueNames[] m_childrenNonComputedUniqueNames;
	}
}

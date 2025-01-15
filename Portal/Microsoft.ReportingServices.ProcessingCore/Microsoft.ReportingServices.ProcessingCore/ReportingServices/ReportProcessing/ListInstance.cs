using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000734 RID: 1844
	[Serializable]
	internal sealed class ListInstance : ReportItemInstance, IPageItem
	{
		// Token: 0x06006670 RID: 26224 RVA: 0x00191608 File Offset: 0x0018F808
		internal ListInstance(ReportProcessing.ProcessingContext pc, List reportItemDef)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.m_instanceInfo = new ListInstanceInfo(pc, reportItemDef, this);
			this.m_listContentInstances = new ListContentInstanceList();
			this.m_renderingPages = new RenderingPagesRangesList();
		}

		// Token: 0x06006671 RID: 26225 RVA: 0x00191654 File Offset: 0x0018F854
		internal ListInstance(ReportProcessing.ProcessingContext pc, List reportItemDef, ListContentInstanceList listContentInstances, RenderingPagesRangesList renderingPages)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.m_instanceInfo = new ListInstanceInfo(pc, reportItemDef, this);
			this.m_listContentInstances = listContentInstances;
			this.m_renderingPages = renderingPages;
		}

		// Token: 0x06006672 RID: 26226 RVA: 0x0019168E File Offset: 0x0018F88E
		internal ListInstance()
		{
		}

		// Token: 0x17002436 RID: 9270
		// (get) Token: 0x06006673 RID: 26227 RVA: 0x001916A4 File Offset: 0x0018F8A4
		// (set) Token: 0x06006674 RID: 26228 RVA: 0x001916AC File Offset: 0x0018F8AC
		internal ListContentInstanceList ListContents
		{
			get
			{
				return this.m_listContentInstances;
			}
			set
			{
				this.m_listContentInstances = value;
			}
		}

		// Token: 0x17002437 RID: 9271
		// (get) Token: 0x06006675 RID: 26229 RVA: 0x001916B5 File Offset: 0x0018F8B5
		// (set) Token: 0x06006676 RID: 26230 RVA: 0x001916BD File Offset: 0x0018F8BD
		internal RenderingPagesRangesList ChildrenStartAndEndPages
		{
			get
			{
				return this.m_renderingPages;
			}
			set
			{
				this.m_renderingPages = value;
			}
		}

		// Token: 0x17002438 RID: 9272
		// (get) Token: 0x06006677 RID: 26231 RVA: 0x001916C6 File Offset: 0x0018F8C6
		// (set) Token: 0x06006678 RID: 26232 RVA: 0x001916CE File Offset: 0x0018F8CE
		internal int NumberOfContentsOnThisPage
		{
			get
			{
				return this.m_numberOfContentsOnThisPage;
			}
			set
			{
				this.m_numberOfContentsOnThisPage = value;
			}
		}

		// Token: 0x17002439 RID: 9273
		// (get) Token: 0x06006679 RID: 26233 RVA: 0x001916D7 File Offset: 0x0018F8D7
		// (set) Token: 0x0600667A RID: 26234 RVA: 0x001916DF File Offset: 0x0018F8DF
		int IPageItem.StartPage
		{
			get
			{
				return this.m_startPage;
			}
			set
			{
				this.m_startPage = value;
			}
		}

		// Token: 0x1700243A RID: 9274
		// (get) Token: 0x0600667B RID: 26235 RVA: 0x001916E8 File Offset: 0x0018F8E8
		// (set) Token: 0x0600667C RID: 26236 RVA: 0x001916F0 File Offset: 0x0018F8F0
		int IPageItem.EndPage
		{
			get
			{
				return this.m_endPage;
			}
			set
			{
				this.m_endPage = value;
			}
		}

		// Token: 0x0600667D RID: 26237 RVA: 0x001916FC File Offset: 0x0018F8FC
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstance, new MemberInfoList
			{
				new MemberInfo(MemberName.ListContentInstances, ObjectType.ListContentInstanceList),
				new MemberInfo(MemberName.ChildrenStartAndEndPages, ObjectType.RenderingPagesRangesList)
			});
		}

		// Token: 0x0600667E RID: 26238 RVA: 0x00191740 File Offset: 0x0018F940
		protected override object SearchChildren(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			int count = this.m_listContentInstances.Count;
			for (int i = 0; i < count; i++)
			{
				object obj = ((ISearchByUniqueName)this.m_listContentInstances[i]).Find(targetUniqueName, ref nonCompNames, chunkManager);
				if (obj != null)
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x0600667F RID: 26239 RVA: 0x00191780 File Offset: 0x0018F980
		internal override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
			return reader.ReadListInstanceInfo((List)this.m_reportItemDef);
		}

		// Token: 0x040032FC RID: 13052
		private ListContentInstanceList m_listContentInstances;

		// Token: 0x040032FD RID: 13053
		private RenderingPagesRangesList m_renderingPages;

		// Token: 0x040032FE RID: 13054
		[NonSerialized]
		private int m_numberOfContentsOnThisPage;

		// Token: 0x040032FF RID: 13055
		[NonSerialized]
		private int m_startPage = -1;

		// Token: 0x04003300 RID: 13056
		[NonSerialized]
		private int m_endPage = -1;
	}
}

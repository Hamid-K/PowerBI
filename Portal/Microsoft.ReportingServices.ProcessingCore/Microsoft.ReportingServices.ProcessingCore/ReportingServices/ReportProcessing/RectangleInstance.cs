using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000728 RID: 1832
	[Serializable]
	internal sealed class RectangleInstance : ReportItemInstance, IShowHideContainer, IIndexInto, IPageItem
	{
		// Token: 0x06006612 RID: 26130 RVA: 0x00190DCC File Offset: 0x0018EFCC
		internal RectangleInstance(ReportProcessing.ProcessingContext pc, Rectangle reportItemDef, int index)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.m_instanceInfo = new RectangleInstanceInfo(pc, reportItemDef, this, index);
			pc.Pagination.EnterIgnoreHeight(reportItemDef.StartHidden);
			this.m_reportItemColInstance = new ReportItemColInstance(pc, reportItemDef.ReportItems);
		}

		// Token: 0x06006613 RID: 26131 RVA: 0x00190E26 File Offset: 0x0018F026
		internal RectangleInstance()
		{
		}

		// Token: 0x1700241B RID: 9243
		// (get) Token: 0x06006614 RID: 26132 RVA: 0x00190E3C File Offset: 0x0018F03C
		// (set) Token: 0x06006615 RID: 26133 RVA: 0x00190E44 File Offset: 0x0018F044
		internal ReportItemColInstance ReportItemColInstance
		{
			get
			{
				return this.m_reportItemColInstance;
			}
			set
			{
				this.m_reportItemColInstance = value;
			}
		}

		// Token: 0x1700241C RID: 9244
		// (get) Token: 0x06006616 RID: 26134 RVA: 0x00190E4D File Offset: 0x0018F04D
		// (set) Token: 0x06006617 RID: 26135 RVA: 0x00190E55 File Offset: 0x0018F055
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

		// Token: 0x1700241D RID: 9245
		// (get) Token: 0x06006618 RID: 26136 RVA: 0x00190E5E File Offset: 0x0018F05E
		// (set) Token: 0x06006619 RID: 26137 RVA: 0x00190E66 File Offset: 0x0018F066
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

		// Token: 0x0600661A RID: 26138 RVA: 0x00190E70 File Offset: 0x0018F070
		internal override int GetDocumentMapUniqueName()
		{
			int linkToChild = ((Rectangle)this.m_reportItemDef).LinkToChild;
			if (linkToChild >= 0)
			{
				return this.m_reportItemColInstance.GetReportItemUniqueName(linkToChild);
			}
			return this.m_uniqueName;
		}

		// Token: 0x0600661B RID: 26139 RVA: 0x00190EA5 File Offset: 0x0018F0A5
		object IIndexInto.GetChildAt(int index, out NonComputedUniqueNames nonCompNames)
		{
			return ((IIndexInto)this.m_reportItemColInstance).GetChildAt(index, out nonCompNames);
		}

		// Token: 0x0600661C RID: 26140 RVA: 0x00190EB4 File Offset: 0x0018F0B4
		protected override object SearchChildren(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			return ((ISearchByUniqueName)this.m_reportItemColInstance).Find(targetUniqueName, ref nonCompNames, chunkManager);
		}

		// Token: 0x0600661D RID: 26141 RVA: 0x00190EC4 File Offset: 0x0018F0C4
		void IShowHideContainer.BeginProcessContainer(ReportProcessing.ProcessingContext context)
		{
			context.BeginProcessContainer(this.m_uniqueName, this.m_reportItemDef.Visibility);
		}

		// Token: 0x0600661E RID: 26142 RVA: 0x00190EDD File Offset: 0x0018F0DD
		void IShowHideContainer.EndProcessContainer(ReportProcessing.ProcessingContext context)
		{
			context.EndProcessContainer(this.m_uniqueName, this.m_reportItemDef.Visibility);
		}

		// Token: 0x0600661F RID: 26143 RVA: 0x00190EF8 File Offset: 0x0018F0F8
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstance, new MemberInfoList
			{
				new MemberInfo(MemberName.ReportItemColInstance, ObjectType.ReportItemColInstance)
			});
		}

		// Token: 0x06006620 RID: 26144 RVA: 0x00190F26 File Offset: 0x0018F126
		internal override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
			return reader.ReadRectangleInstanceInfo((Rectangle)this.m_reportItemDef);
		}

		// Token: 0x040032E6 RID: 13030
		private ReportItemColInstance m_reportItemColInstance;

		// Token: 0x040032E7 RID: 13031
		[NonSerialized]
		private int m_startPage = -1;

		// Token: 0x040032E8 RID: 13032
		[NonSerialized]
		private int m_endPage = -1;
	}
}

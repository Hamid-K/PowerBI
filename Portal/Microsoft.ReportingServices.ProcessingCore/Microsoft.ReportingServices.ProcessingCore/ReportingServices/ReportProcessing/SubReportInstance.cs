using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000730 RID: 1840
	[Serializable]
	internal sealed class SubReportInstance : ReportItemInstance, IPageItem
	{
		// Token: 0x06006656 RID: 26198 RVA: 0x0019138F File Offset: 0x0018F58F
		internal SubReportInstance(ReportProcessing.ProcessingContext pc, SubReport reportItemDef, int index)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.m_instanceInfo = new SubReportInstanceInfo(pc, reportItemDef, this, index);
			pc.Pagination.EnterIgnoreHeight(reportItemDef.StartHidden);
		}

		// Token: 0x06006657 RID: 26199 RVA: 0x001913CC File Offset: 0x0018F5CC
		internal SubReportInstance()
		{
		}

		// Token: 0x17002430 RID: 9264
		// (get) Token: 0x06006658 RID: 26200 RVA: 0x001913E2 File Offset: 0x0018F5E2
		// (set) Token: 0x06006659 RID: 26201 RVA: 0x001913EA File Offset: 0x0018F5EA
		internal ReportInstance ReportInstance
		{
			get
			{
				return this.m_reportInstance;
			}
			set
			{
				this.m_reportInstance = value;
			}
		}

		// Token: 0x17002431 RID: 9265
		// (get) Token: 0x0600665A RID: 26202 RVA: 0x001913F3 File Offset: 0x0018F5F3
		// (set) Token: 0x0600665B RID: 26203 RVA: 0x001913FB File Offset: 0x0018F5FB
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

		// Token: 0x17002432 RID: 9266
		// (get) Token: 0x0600665C RID: 26204 RVA: 0x00191404 File Offset: 0x0018F604
		// (set) Token: 0x0600665D RID: 26205 RVA: 0x0019140C File Offset: 0x0018F60C
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

		// Token: 0x0600665E RID: 26206 RVA: 0x00191418 File Offset: 0x0018F618
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstance, new MemberInfoList
			{
				new MemberInfo(MemberName.ReportInstance, ObjectType.ReportInstance)
			});
		}

		// Token: 0x0600665F RID: 26207 RVA: 0x00191443 File Offset: 0x0018F643
		protected override object SearchChildren(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_reportInstance == null)
			{
				return null;
			}
			return ((ISearchByUniqueName)this.m_reportInstance).Find(targetUniqueName, ref nonCompNames, chunkManager);
		}

		// Token: 0x06006660 RID: 26208 RVA: 0x0019145D File Offset: 0x0018F65D
		internal override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
			return reader.ReadSubReportInstanceInfo((SubReport)this.m_reportItemDef);
		}

		// Token: 0x040032F7 RID: 13047
		private ReportInstance m_reportInstance;

		// Token: 0x040032F8 RID: 13048
		[NonSerialized]
		private int m_startPage = -1;

		// Token: 0x040032F9 RID: 13049
		[NonSerialized]
		private int m_endPage = -1;
	}
}

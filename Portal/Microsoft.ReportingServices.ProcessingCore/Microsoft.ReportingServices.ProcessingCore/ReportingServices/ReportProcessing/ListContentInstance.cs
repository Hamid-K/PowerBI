using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000736 RID: 1846
	[Serializable]
	internal sealed class ListContentInstance : InstanceInfoOwner, ISearchByUniqueName, IShowHideContainer
	{
		// Token: 0x06006685 RID: 26245 RVA: 0x00191824 File Offset: 0x0018FA24
		internal ListContentInstance(ReportProcessing.ProcessingContext pc, List listDef)
		{
			this.m_uniqueName = pc.CreateUniqueName();
			this.m_listDef = listDef;
			this.m_reportItemColInstance = new ReportItemColInstance(pc, listDef.ReportItems);
			this.m_instanceInfo = new ListContentInstanceInfo(pc, this, listDef);
			pc.Pagination.EnterIgnoreHeight(listDef.StartHidden);
		}

		// Token: 0x06006686 RID: 26246 RVA: 0x0019187B File Offset: 0x0018FA7B
		internal ListContentInstance()
		{
		}

		// Token: 0x1700243C RID: 9276
		// (get) Token: 0x06006687 RID: 26247 RVA: 0x00191883 File Offset: 0x0018FA83
		// (set) Token: 0x06006688 RID: 26248 RVA: 0x0019188B File Offset: 0x0018FA8B
		internal int UniqueName
		{
			get
			{
				return this.m_uniqueName;
			}
			set
			{
				this.m_uniqueName = value;
			}
		}

		// Token: 0x1700243D RID: 9277
		// (get) Token: 0x06006689 RID: 26249 RVA: 0x00191894 File Offset: 0x0018FA94
		// (set) Token: 0x0600668A RID: 26250 RVA: 0x0019189C File Offset: 0x0018FA9C
		internal List ListDef
		{
			get
			{
				return this.m_listDef;
			}
			set
			{
				this.m_listDef = value;
			}
		}

		// Token: 0x1700243E RID: 9278
		// (get) Token: 0x0600668B RID: 26251 RVA: 0x001918A5 File Offset: 0x0018FAA5
		// (set) Token: 0x0600668C RID: 26252 RVA: 0x001918AD File Offset: 0x0018FAAD
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

		// Token: 0x0600668D RID: 26253 RVA: 0x001918B6 File Offset: 0x0018FAB6
		object ISearchByUniqueName.Find(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			return ((ISearchByUniqueName)this.m_reportItemColInstance).Find(targetUniqueName, ref nonCompNames, chunkManager);
		}

		// Token: 0x0600668E RID: 26254 RVA: 0x001918C6 File Offset: 0x0018FAC6
		void IShowHideContainer.BeginProcessContainer(ReportProcessing.ProcessingContext context)
		{
			context.BeginProcessContainer(this.m_uniqueName, this.m_listDef.Visibility);
		}

		// Token: 0x0600668F RID: 26255 RVA: 0x001918DF File Offset: 0x0018FADF
		void IShowHideContainer.EndProcessContainer(ReportProcessing.ProcessingContext context)
		{
			context.EndProcessContainer(this.m_uniqueName, this.m_listDef.Visibility);
		}

		// Token: 0x06006690 RID: 26256 RVA: 0x001918F8 File Offset: 0x0018FAF8
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfoOwner, new MemberInfoList
			{
				new MemberInfo(MemberName.UniqueName, Token.Int32),
				new MemberInfo(MemberName.ReportItemColInstance, ObjectType.ReportItemColInstance)
			});
		}

		// Token: 0x06006691 RID: 26257 RVA: 0x0019193C File Offset: 0x0018FB3C
		internal ListContentInstanceInfo GetInstanceInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_instanceInfo is OffsetInfo)
			{
				Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
				return chunkManager.GetReader(((OffsetInfo)this.m_instanceInfo).Offset).ReadListContentInstanceInfo();
			}
			return (ListContentInstanceInfo)this.m_instanceInfo;
		}

		// Token: 0x04003302 RID: 13058
		private int m_uniqueName;

		// Token: 0x04003303 RID: 13059
		private ReportItemColInstance m_reportItemColInstance;

		// Token: 0x04003304 RID: 13060
		[Reference]
		[NonSerialized]
		private List m_listDef;
	}
}

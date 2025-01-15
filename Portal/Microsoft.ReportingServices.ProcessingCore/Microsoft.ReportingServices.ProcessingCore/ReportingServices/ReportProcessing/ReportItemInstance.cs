using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200071C RID: 1820
	[Serializable]
	internal abstract class ReportItemInstance : InstanceInfoOwner, ISearchByUniqueName
	{
		// Token: 0x0600658E RID: 25998 RVA: 0x0018FC43 File Offset: 0x0018DE43
		internal ReportItemInstance(int uniqueName, ReportItem reportItemDef)
		{
			this.m_uniqueName = uniqueName;
			this.m_reportItemDef = reportItemDef;
		}

		// Token: 0x0600658F RID: 25999 RVA: 0x0018FC59 File Offset: 0x0018DE59
		internal ReportItemInstance()
		{
		}

		// Token: 0x170023F8 RID: 9208
		// (get) Token: 0x06006590 RID: 26000 RVA: 0x0018FC61 File Offset: 0x0018DE61
		// (set) Token: 0x06006591 RID: 26001 RVA: 0x0018FC69 File Offset: 0x0018DE69
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

		// Token: 0x170023F9 RID: 9209
		// (get) Token: 0x06006592 RID: 26002 RVA: 0x0018FC72 File Offset: 0x0018DE72
		// (set) Token: 0x06006593 RID: 26003 RVA: 0x0018FC7A File Offset: 0x0018DE7A
		internal ReportItem ReportItemDef
		{
			get
			{
				return this.m_reportItemDef;
			}
			set
			{
				this.m_reportItemDef = value;
			}
		}

		// Token: 0x06006594 RID: 26004 RVA: 0x0018FC83 File Offset: 0x0018DE83
		object ISearchByUniqueName.Find(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_uniqueName == targetUniqueName)
			{
				nonCompNames = null;
				return this;
			}
			return this.SearchChildren(targetUniqueName, ref nonCompNames, chunkManager);
		}

		// Token: 0x06006595 RID: 26005 RVA: 0x0018FC9C File Offset: 0x0018DE9C
		protected virtual object SearchChildren(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			return null;
		}

		// Token: 0x06006596 RID: 26006 RVA: 0x0018FC9F File Offset: 0x0018DE9F
		internal virtual int GetDocumentMapUniqueName()
		{
			return this.m_uniqueName;
		}

		// Token: 0x06006597 RID: 26007 RVA: 0x0018FCA8 File Offset: 0x0018DEA8
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfoOwner, new MemberInfoList
			{
				new MemberInfo(MemberName.UniqueName, Token.Int32)
			});
		}

		// Token: 0x06006598 RID: 26008 RVA: 0x0018FCD9 File Offset: 0x0018DED9
		internal ReportItemInstanceInfo GetInstanceInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			return this.GetInstanceInfo(chunkManager, false);
		}

		// Token: 0x06006599 RID: 26009 RVA: 0x0018FCE4 File Offset: 0x0018DEE4
		internal ReportItemInstanceInfo GetInstanceInfo(ChunkManager.RenderingChunkManager chunkManager, bool inPageSection)
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
				return this.ReadInstanceInfo(intermediateFormatReader);
			}
			return (ReportItemInstanceInfo)this.m_instanceInfo;
		}

		// Token: 0x0600659A RID: 26010
		internal abstract ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader);

		// Token: 0x040032C4 RID: 12996
		protected int m_uniqueName;

		// Token: 0x040032C5 RID: 12997
		[Reference]
		protected ReportItem m_reportItemDef;
	}
}

using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000747 RID: 1863
	[Serializable]
	internal sealed class TableRowInstance : InstanceInfoOwner, IShowHideContainer, ISearchByUniqueName
	{
		// Token: 0x06006768 RID: 26472 RVA: 0x00193C3E File Offset: 0x00191E3E
		internal TableRowInstance(ReportProcessing.ProcessingContext pc, TableRow rowDef, Table tableDef, IndexedExprHost visibilityHiddenExprHost)
		{
			this.m_uniqueName = pc.CreateUniqueName();
			this.m_instanceInfo = new TableRowInstanceInfo(pc, rowDef, this, tableDef, visibilityHiddenExprHost);
			this.m_tableRowDef = rowDef;
			this.m_tableRowReportItemColInstance = new ReportItemColInstance(pc, rowDef.ReportItems);
		}

		// Token: 0x06006769 RID: 26473 RVA: 0x00193C7C File Offset: 0x00191E7C
		internal TableRowInstance()
		{
		}

		// Token: 0x17002489 RID: 9353
		// (get) Token: 0x0600676A RID: 26474 RVA: 0x00193C84 File Offset: 0x00191E84
		// (set) Token: 0x0600676B RID: 26475 RVA: 0x00193C8C File Offset: 0x00191E8C
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

		// Token: 0x1700248A RID: 9354
		// (get) Token: 0x0600676C RID: 26476 RVA: 0x00193C95 File Offset: 0x00191E95
		// (set) Token: 0x0600676D RID: 26477 RVA: 0x00193C9D File Offset: 0x00191E9D
		internal ReportItemColInstance TableRowReportItemColInstance
		{
			get
			{
				return this.m_tableRowReportItemColInstance;
			}
			set
			{
				this.m_tableRowReportItemColInstance = value;
			}
		}

		// Token: 0x1700248B RID: 9355
		// (get) Token: 0x0600676E RID: 26478 RVA: 0x00193CA6 File Offset: 0x00191EA6
		// (set) Token: 0x0600676F RID: 26479 RVA: 0x00193CAE File Offset: 0x00191EAE
		internal TableRow TableRowDef
		{
			get
			{
				return this.m_tableRowDef;
			}
			set
			{
				this.m_tableRowDef = value;
			}
		}

		// Token: 0x06006770 RID: 26480 RVA: 0x00193CB7 File Offset: 0x00191EB7
		object ISearchByUniqueName.Find(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			return ((ISearchByUniqueName)this.m_tableRowReportItemColInstance).Find(targetUniqueName, ref nonCompNames, chunkManager);
		}

		// Token: 0x06006771 RID: 26481 RVA: 0x00193CC7 File Offset: 0x00191EC7
		void IShowHideContainer.BeginProcessContainer(ReportProcessing.ProcessingContext context)
		{
			context.BeginProcessContainer(this.m_uniqueName, this.m_tableRowDef.Visibility);
		}

		// Token: 0x06006772 RID: 26482 RVA: 0x00193CE0 File Offset: 0x00191EE0
		void IShowHideContainer.EndProcessContainer(ReportProcessing.ProcessingContext context)
		{
			context.EndProcessContainer(this.m_uniqueName, this.m_tableRowDef.Visibility);
		}

		// Token: 0x06006773 RID: 26483 RVA: 0x00193CFC File Offset: 0x00191EFC
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfoOwner, new MemberInfoList
			{
				new MemberInfo(MemberName.UniqueName, Token.Int32),
				new MemberInfo(MemberName.TableRowReportItemColInstance, ObjectType.ReportItemColInstance)
			});
		}

		// Token: 0x06006774 RID: 26484 RVA: 0x00193D40 File Offset: 0x00191F40
		internal TableRowInstanceInfo GetInstanceInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_instanceInfo is OffsetInfo)
			{
				Global.Tracer.Assert(chunkManager != null);
				return chunkManager.GetReader(((OffsetInfo)this.m_instanceInfo).Offset).ReadTableRowInstanceInfo();
			}
			return (TableRowInstanceInfo)this.m_instanceInfo;
		}

		// Token: 0x0400334A RID: 13130
		private int m_uniqueName;

		// Token: 0x0400334B RID: 13131
		private ReportItemColInstance m_tableRowReportItemColInstance;

		// Token: 0x0400334C RID: 13132
		[Reference]
		[NonSerialized]
		private TableRow m_tableRowDef;
	}
}

using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000744 RID: 1860
	[Serializable]
	internal sealed class TableDetailInstance : InstanceInfoOwner, IShowHideContainer, ISearchByUniqueName
	{
		// Token: 0x0600674F RID: 26447 RVA: 0x001938DC File Offset: 0x00191ADC
		internal TableDetailInstance(ReportProcessing.ProcessingContext pc, TableDetail tableDetailDef, Table tableDef)
		{
			this.m_uniqueName = pc.CreateUniqueName();
			this.m_instanceInfo = new TableDetailInstanceInfo(pc, tableDetailDef, this, tableDef);
			pc.Pagination.EnterIgnoreHeight(tableDetailDef.StartHidden);
			this.m_tableDetailDef = tableDetailDef;
			if (tableDetailDef.DetailRows != null)
			{
				IndexedExprHost indexedExprHost = ((tableDetailDef.ExprHost != null) ? tableDetailDef.ExprHost.TableRowVisibilityHiddenExpressions : null);
				this.m_detailRowInstances = new TableRowInstance[tableDetailDef.DetailRows.Count];
				for (int i = 0; i < this.m_detailRowInstances.Length; i++)
				{
					this.m_detailRowInstances[i] = new TableRowInstance(pc, tableDetailDef.DetailRows[i], tableDef, indexedExprHost);
				}
			}
		}

		// Token: 0x06006750 RID: 26448 RVA: 0x00193986 File Offset: 0x00191B86
		internal TableDetailInstance()
		{
		}

		// Token: 0x17002483 RID: 9347
		// (get) Token: 0x06006751 RID: 26449 RVA: 0x0019398E File Offset: 0x00191B8E
		// (set) Token: 0x06006752 RID: 26450 RVA: 0x00193996 File Offset: 0x00191B96
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

		// Token: 0x17002484 RID: 9348
		// (get) Token: 0x06006753 RID: 26451 RVA: 0x0019399F File Offset: 0x00191B9F
		// (set) Token: 0x06006754 RID: 26452 RVA: 0x001939A7 File Offset: 0x00191BA7
		internal TableDetail TableDetailDef
		{
			get
			{
				return this.m_tableDetailDef;
			}
			set
			{
				this.m_tableDetailDef = value;
			}
		}

		// Token: 0x17002485 RID: 9349
		// (get) Token: 0x06006755 RID: 26453 RVA: 0x001939B0 File Offset: 0x00191BB0
		// (set) Token: 0x06006756 RID: 26454 RVA: 0x001939B8 File Offset: 0x00191BB8
		internal TableRowInstance[] DetailRowInstances
		{
			get
			{
				return this.m_detailRowInstances;
			}
			set
			{
				this.m_detailRowInstances = value;
			}
		}

		// Token: 0x06006757 RID: 26455 RVA: 0x001939C4 File Offset: 0x00191BC4
		object ISearchByUniqueName.Find(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_detailRowInstances != null)
			{
				int num = this.m_detailRowInstances.Length;
				for (int i = 0; i < num; i++)
				{
					object obj = ((ISearchByUniqueName)this.m_detailRowInstances[i]).Find(targetUniqueName, ref nonCompNames, chunkManager);
					if (obj != null)
					{
						return obj;
					}
				}
			}
			return null;
		}

		// Token: 0x06006758 RID: 26456 RVA: 0x00193A07 File Offset: 0x00191C07
		void IShowHideContainer.BeginProcessContainer(ReportProcessing.ProcessingContext context)
		{
			context.BeginProcessContainer(this.m_uniqueName, this.m_tableDetailDef.Visibility);
		}

		// Token: 0x06006759 RID: 26457 RVA: 0x00193A20 File Offset: 0x00191C20
		void IShowHideContainer.EndProcessContainer(ReportProcessing.ProcessingContext context)
		{
			context.EndProcessContainer(this.m_uniqueName, this.m_tableDetailDef.Visibility);
		}

		// Token: 0x0600675A RID: 26458 RVA: 0x00193A3C File Offset: 0x00191C3C
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfoOwner, new MemberInfoList
			{
				new MemberInfo(MemberName.UniqueName, Token.Int32),
				new MemberInfo(MemberName.DetailRowInstances, Token.Array, ObjectType.TableRowInstance)
			});
		}

		// Token: 0x0600675B RID: 26459 RVA: 0x00193A81 File Offset: 0x00191C81
		internal TableDetailInstanceInfo GetInstanceInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_instanceInfo is OffsetInfo)
			{
				return chunkManager.GetReader(((OffsetInfo)this.m_instanceInfo).Offset).ReadTableDetailInstanceInfo();
			}
			return (TableDetailInstanceInfo)this.m_instanceInfo;
		}

		// Token: 0x04003344 RID: 13124
		private int m_uniqueName;

		// Token: 0x04003345 RID: 13125
		private TableRowInstance[] m_detailRowInstances;

		// Token: 0x04003346 RID: 13126
		[Reference]
		[NonSerialized]
		private TableDetail m_tableDetailDef;
	}
}

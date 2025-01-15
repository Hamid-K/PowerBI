using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000742 RID: 1858
	[Serializable]
	internal sealed class TableGroupInstance : InstanceInfoOwner, IShowHideContainer, ISearchByUniqueName
	{
		// Token: 0x0600672F RID: 26415 RVA: 0x00193364 File Offset: 0x00191564
		internal TableGroupInstance(ReportProcessing.ProcessingContext pc, TableGroup tableGroupDef)
		{
			Table table = (Table)tableGroupDef.DataRegionDef;
			this.m_uniqueName = pc.CreateUniqueName();
			this.m_instanceInfo = new TableGroupInstanceInfo(pc, tableGroupDef, this);
			pc.Pagination.EnterIgnoreHeight(tableGroupDef.StartHidden);
			this.m_tableGroupDef = tableGroupDef;
			IndexedExprHost indexedExprHost = ((tableGroupDef.ExprHost != null) ? tableGroupDef.ExprHost.TableRowVisibilityHiddenExpressions : null);
			this.m_renderingPages = new RenderingPagesRangesList();
			if (tableGroupDef.HeaderRows != null)
			{
				this.m_headerRowInstances = new TableRowInstance[tableGroupDef.HeaderRows.Count];
				for (int i = 0; i < this.m_headerRowInstances.Length; i++)
				{
					this.m_headerRowInstances[i] = new TableRowInstance(pc, tableGroupDef.HeaderRows[i], table, indexedExprHost);
				}
			}
			if (tableGroupDef.FooterRows != null)
			{
				this.m_footerRowInstances = new TableRowInstance[tableGroupDef.FooterRows.Count];
				for (int j = 0; j < this.m_footerRowInstances.Length; j++)
				{
					this.m_footerRowInstances[j] = new TableRowInstance(pc, tableGroupDef.FooterRows[j], table, indexedExprHost);
				}
			}
			if (tableGroupDef.SubGroup != null)
			{
				this.m_subGroupInstances = new TableGroupInstanceList();
				return;
			}
			if (table.TableDetail != null)
			{
				this.m_tableDetailInstances = new TableDetailInstanceList();
			}
		}

		// Token: 0x06006730 RID: 26416 RVA: 0x00193498 File Offset: 0x00191698
		internal TableGroupInstance()
		{
		}

		// Token: 0x17002478 RID: 9336
		// (get) Token: 0x06006731 RID: 26417 RVA: 0x001934A0 File Offset: 0x001916A0
		// (set) Token: 0x06006732 RID: 26418 RVA: 0x001934A8 File Offset: 0x001916A8
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

		// Token: 0x17002479 RID: 9337
		// (get) Token: 0x06006733 RID: 26419 RVA: 0x001934B1 File Offset: 0x001916B1
		// (set) Token: 0x06006734 RID: 26420 RVA: 0x001934B9 File Offset: 0x001916B9
		internal TableGroup TableGroupDef
		{
			get
			{
				return this.m_tableGroupDef;
			}
			set
			{
				this.m_tableGroupDef = value;
			}
		}

		// Token: 0x1700247A RID: 9338
		// (get) Token: 0x06006735 RID: 26421 RVA: 0x001934C2 File Offset: 0x001916C2
		// (set) Token: 0x06006736 RID: 26422 RVA: 0x001934CA File Offset: 0x001916CA
		internal TableRowInstance[] HeaderRowInstances
		{
			get
			{
				return this.m_headerRowInstances;
			}
			set
			{
				this.m_headerRowInstances = value;
			}
		}

		// Token: 0x1700247B RID: 9339
		// (get) Token: 0x06006737 RID: 26423 RVA: 0x001934D3 File Offset: 0x001916D3
		// (set) Token: 0x06006738 RID: 26424 RVA: 0x001934DB File Offset: 0x001916DB
		internal TableRowInstance[] FooterRowInstances
		{
			get
			{
				return this.m_footerRowInstances;
			}
			set
			{
				this.m_footerRowInstances = value;
			}
		}

		// Token: 0x1700247C RID: 9340
		// (get) Token: 0x06006739 RID: 26425 RVA: 0x001934E4 File Offset: 0x001916E4
		// (set) Token: 0x0600673A RID: 26426 RVA: 0x001934EC File Offset: 0x001916EC
		internal TableGroupInstanceList SubGroupInstances
		{
			get
			{
				return this.m_subGroupInstances;
			}
			set
			{
				this.m_subGroupInstances = value;
			}
		}

		// Token: 0x1700247D RID: 9341
		// (get) Token: 0x0600673B RID: 26427 RVA: 0x001934F5 File Offset: 0x001916F5
		// (set) Token: 0x0600673C RID: 26428 RVA: 0x001934FD File Offset: 0x001916FD
		internal TableDetailInstanceList TableDetailInstances
		{
			get
			{
				return this.m_tableDetailInstances;
			}
			set
			{
				this.m_tableDetailInstances = value;
			}
		}

		// Token: 0x1700247E RID: 9342
		// (get) Token: 0x0600673D RID: 26429 RVA: 0x00193506 File Offset: 0x00191706
		// (set) Token: 0x0600673E RID: 26430 RVA: 0x0019350E File Offset: 0x0019170E
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

		// Token: 0x1700247F RID: 9343
		// (get) Token: 0x0600673F RID: 26431 RVA: 0x00193517 File Offset: 0x00191717
		// (set) Token: 0x06006740 RID: 26432 RVA: 0x0019351F File Offset: 0x0019171F
		internal int NumberOfChildrenOnThisPage
		{
			get
			{
				return this.m_numberOfChildrenOnThisPage;
			}
			set
			{
				this.m_numberOfChildrenOnThisPage = value;
			}
		}

		// Token: 0x06006741 RID: 26433 RVA: 0x00193528 File Offset: 0x00191728
		object ISearchByUniqueName.Find(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_headerRowInstances != null)
			{
				int num = this.m_headerRowInstances.Length;
				for (int i = 0; i < num; i++)
				{
					object obj = ((ISearchByUniqueName)this.m_headerRowInstances[i]).Find(targetUniqueName, ref nonCompNames, chunkManager);
					if (obj != null)
					{
						return obj;
					}
				}
			}
			if (this.m_subGroupInstances != null)
			{
				int count = this.m_subGroupInstances.Count;
				for (int j = 0; j < count; j++)
				{
					object obj = ((ISearchByUniqueName)this.m_subGroupInstances[j]).Find(targetUniqueName, ref nonCompNames, chunkManager);
					if (obj != null)
					{
						return obj;
					}
				}
			}
			else if (this.m_tableDetailInstances != null)
			{
				int count2 = this.m_tableDetailInstances.Count;
				for (int k = 0; k < count2; k++)
				{
					object obj = ((ISearchByUniqueName)this.m_tableDetailInstances[k]).Find(targetUniqueName, ref nonCompNames, chunkManager);
					if (obj != null)
					{
						return obj;
					}
				}
			}
			if (this.m_footerRowInstances != null)
			{
				int num2 = this.m_footerRowInstances.Length;
				for (int l = 0; l < num2; l++)
				{
					object obj = ((ISearchByUniqueName)this.m_footerRowInstances[l]).Find(targetUniqueName, ref nonCompNames, chunkManager);
					if (obj != null)
					{
						return obj;
					}
				}
			}
			return null;
		}

		// Token: 0x06006742 RID: 26434 RVA: 0x00193627 File Offset: 0x00191827
		void IShowHideContainer.BeginProcessContainer(ReportProcessing.ProcessingContext context)
		{
			context.BeginProcessContainer(this.m_uniqueName, this.m_tableGroupDef.Visibility);
		}

		// Token: 0x06006743 RID: 26435 RVA: 0x00193640 File Offset: 0x00191840
		void IShowHideContainer.EndProcessContainer(ReportProcessing.ProcessingContext context)
		{
			context.EndProcessContainer(this.m_uniqueName, this.m_tableGroupDef.Visibility);
		}

		// Token: 0x06006744 RID: 26436 RVA: 0x0019365C File Offset: 0x0019185C
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfoOwner, new MemberInfoList
			{
				new MemberInfo(MemberName.UniqueName, Token.Int32),
				new MemberInfo(MemberName.HeaderRowInstances, Token.Array, ObjectType.TableRowInstance),
				new MemberInfo(MemberName.FooterRowInstances, Token.Array, ObjectType.TableRowInstance),
				new MemberInfo(MemberName.SubGroupInstances, ObjectType.TableGroupInstanceList),
				new MemberInfo(MemberName.SimpleDetailStartUniqueName, Token.Int32),
				new MemberInfo(MemberName.TableDetailInstances, ObjectType.TableDetailInstanceList),
				new MemberInfo(MemberName.ChildrenStartAndEndPages, ObjectType.RenderingPagesRangesList)
			});
		}

		// Token: 0x06006745 RID: 26437 RVA: 0x0019370A File Offset: 0x0019190A
		internal TableGroupInstanceInfo GetInstanceInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_instanceInfo is OffsetInfo)
			{
				return chunkManager.GetReader(((OffsetInfo)this.m_instanceInfo).Offset).ReadTableGroupInstanceInfo();
			}
			return (TableGroupInstanceInfo)this.m_instanceInfo;
		}

		// Token: 0x04003339 RID: 13113
		private int m_uniqueName;

		// Token: 0x0400333A RID: 13114
		private TableRowInstance[] m_headerRowInstances;

		// Token: 0x0400333B RID: 13115
		private TableRowInstance[] m_footerRowInstances;

		// Token: 0x0400333C RID: 13116
		private TableGroupInstanceList m_subGroupInstances;

		// Token: 0x0400333D RID: 13117
		private TableDetailInstanceList m_tableDetailInstances;

		// Token: 0x0400333E RID: 13118
		private RenderingPagesRangesList m_renderingPages;

		// Token: 0x0400333F RID: 13119
		[Reference]
		[NonSerialized]
		private TableGroup m_tableGroupDef;

		// Token: 0x04003340 RID: 13120
		[NonSerialized]
		private int m_numberOfChildrenOnThisPage;
	}
}

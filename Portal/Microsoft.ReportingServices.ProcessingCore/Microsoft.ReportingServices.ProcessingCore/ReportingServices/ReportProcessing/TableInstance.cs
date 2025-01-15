using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000740 RID: 1856
	[Serializable]
	internal sealed class TableInstance : ReportItemInstance, IShowHideContainer, IPageItem
	{
		// Token: 0x0600670D RID: 26381 RVA: 0x00192DB0 File Offset: 0x00190FB0
		internal TableInstance(ReportProcessing.ProcessingContext pc, Table reportItemDef)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.ConstructorHelper(pc, reportItemDef);
			if (reportItemDef.TableGroups == null && reportItemDef.TableDetail != null)
			{
				this.m_tableDetailInstances = new TableDetailInstanceList();
			}
			this.m_renderingPages = new RenderingPagesRangesList();
			this.m_currentPage = reportItemDef.StartPage;
			reportItemDef.CurrentPage = reportItemDef.StartPage;
		}

		// Token: 0x0600670E RID: 26382 RVA: 0x00192E28 File Offset: 0x00191028
		internal TableInstance(ReportProcessing.ProcessingContext pc, Table reportItemDef, TableDetailInstanceList tableDetailInstances, RenderingPagesRangesList renderingPages)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.ConstructorHelper(pc, reportItemDef);
			if (reportItemDef.TableGroups == null && reportItemDef.TableDetail != null)
			{
				this.m_tableDetailInstances = tableDetailInstances;
				this.m_renderingPages = renderingPages;
			}
			this.m_currentPage = reportItemDef.StartPage;
			reportItemDef.CurrentPage = reportItemDef.StartPage;
			reportItemDef.BottomInEndPage = pc.Pagination.CurrentPageHeight;
		}

		// Token: 0x0600670F RID: 26383 RVA: 0x00192EA7 File Offset: 0x001910A7
		internal TableInstance()
		{
		}

		// Token: 0x1700246D RID: 9325
		// (get) Token: 0x06006710 RID: 26384 RVA: 0x00192EC4 File Offset: 0x001910C4
		// (set) Token: 0x06006711 RID: 26385 RVA: 0x00192ECC File Offset: 0x001910CC
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

		// Token: 0x1700246E RID: 9326
		// (get) Token: 0x06006712 RID: 26386 RVA: 0x00192ED5 File Offset: 0x001910D5
		// (set) Token: 0x06006713 RID: 26387 RVA: 0x00192EDD File Offset: 0x001910DD
		internal TableGroupInstanceList TableGroupInstances
		{
			get
			{
				return this.m_tableGroupInstances;
			}
			set
			{
				this.m_tableGroupInstances = value;
			}
		}

		// Token: 0x1700246F RID: 9327
		// (get) Token: 0x06006714 RID: 26388 RVA: 0x00192EE6 File Offset: 0x001910E6
		// (set) Token: 0x06006715 RID: 26389 RVA: 0x00192EEE File Offset: 0x001910EE
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

		// Token: 0x17002470 RID: 9328
		// (get) Token: 0x06006716 RID: 26390 RVA: 0x00192EF7 File Offset: 0x001910F7
		// (set) Token: 0x06006717 RID: 26391 RVA: 0x00192EFF File Offset: 0x001910FF
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

		// Token: 0x17002471 RID: 9329
		// (get) Token: 0x06006718 RID: 26392 RVA: 0x00192F08 File Offset: 0x00191108
		// (set) Token: 0x06006719 RID: 26393 RVA: 0x00192F10 File Offset: 0x00191110
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

		// Token: 0x17002472 RID: 9330
		// (get) Token: 0x0600671A RID: 26394 RVA: 0x00192F19 File Offset: 0x00191119
		// (set) Token: 0x0600671B RID: 26395 RVA: 0x00192F21 File Offset: 0x00191121
		internal int CurrentPage
		{
			get
			{
				return this.m_currentPage;
			}
			set
			{
				this.m_currentPage = value;
			}
		}

		// Token: 0x17002473 RID: 9331
		// (get) Token: 0x0600671C RID: 26396 RVA: 0x00192F2A File Offset: 0x0019112A
		// (set) Token: 0x0600671D RID: 26397 RVA: 0x00192F32 File Offset: 0x00191132
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

		// Token: 0x17002474 RID: 9332
		// (get) Token: 0x0600671E RID: 26398 RVA: 0x00192F3B File Offset: 0x0019113B
		// (set) Token: 0x0600671F RID: 26399 RVA: 0x00192F43 File Offset: 0x00191143
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

		// Token: 0x17002475 RID: 9333
		// (get) Token: 0x06006720 RID: 26400 RVA: 0x00192F4C File Offset: 0x0019114C
		// (set) Token: 0x06006721 RID: 26401 RVA: 0x00192F54 File Offset: 0x00191154
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

		// Token: 0x06006722 RID: 26402 RVA: 0x00192F60 File Offset: 0x00191160
		private void ConstructorHelper(ReportProcessing.ProcessingContext pc, Table reportItemDef)
		{
			this.m_instanceInfo = new TableInstanceInfo(pc, reportItemDef, this);
			pc.Pagination.EnterIgnoreHeight(reportItemDef.StartHidden);
			IndexedExprHost indexedExprHost = ((reportItemDef.TableExprHost != null) ? reportItemDef.TableExprHost.TableRowVisibilityHiddenExpressions : null);
			if (reportItemDef.HeaderRows != null)
			{
				this.m_headerRowInstances = new TableRowInstance[reportItemDef.HeaderRows.Count];
				for (int i = 0; i < this.m_headerRowInstances.Length; i++)
				{
					this.m_headerRowInstances[i] = new TableRowInstance(pc, reportItemDef.HeaderRows[i], reportItemDef, indexedExprHost);
				}
			}
			if (reportItemDef.FooterRows != null)
			{
				this.m_footerRowInstances = new TableRowInstance[reportItemDef.FooterRows.Count];
				for (int j = 0; j < this.m_footerRowInstances.Length; j++)
				{
					this.m_footerRowInstances[j] = new TableRowInstance(pc, reportItemDef.FooterRows[j], reportItemDef, indexedExprHost);
				}
			}
			if (reportItemDef.TableGroups != null)
			{
				this.m_tableGroupInstances = new TableGroupInstanceList();
			}
		}

		// Token: 0x06006723 RID: 26403 RVA: 0x00193050 File Offset: 0x00191250
		protected override object SearchChildren(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
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
			if (this.m_tableGroupInstances != null)
			{
				int count = this.m_tableGroupInstances.Count;
				for (int j = 0; j < count; j++)
				{
					object obj = ((ISearchByUniqueName)this.m_tableGroupInstances[j]).Find(targetUniqueName, ref nonCompNames, chunkManager);
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

		// Token: 0x06006724 RID: 26404 RVA: 0x0019314F File Offset: 0x0019134F
		void IShowHideContainer.BeginProcessContainer(ReportProcessing.ProcessingContext context)
		{
			context.BeginProcessContainer(this.m_uniqueName, this.m_reportItemDef.Visibility);
		}

		// Token: 0x06006725 RID: 26405 RVA: 0x00193168 File Offset: 0x00191368
		void IShowHideContainer.EndProcessContainer(ReportProcessing.ProcessingContext context)
		{
			context.EndProcessContainer(this.m_uniqueName, this.m_reportItemDef.Visibility);
		}

		// Token: 0x06006726 RID: 26406 RVA: 0x00193184 File Offset: 0x00191384
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstance, new MemberInfoList
			{
				new MemberInfo(MemberName.HeaderRowInstances, Token.Array, ObjectType.TableRowInstance),
				new MemberInfo(MemberName.TableGroupInstances, ObjectType.TableGroupInstanceList),
				new MemberInfo(MemberName.SimpleDetailStartUniqueName, Token.Int32),
				new MemberInfo(MemberName.TableDetailInstances, ObjectType.TableDetailInstanceList),
				new MemberInfo(MemberName.FooterRowInstances, Token.Array, ObjectType.TableRowInstance),
				new MemberInfo(MemberName.ChildrenStartAndEndPages, ObjectType.RenderingPagesRangesList)
			});
		}

		// Token: 0x06006727 RID: 26407 RVA: 0x0019321C File Offset: 0x0019141C
		internal override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
			return reader.ReadTableInstanceInfo((Table)this.m_reportItemDef);
		}

		// Token: 0x0400332E RID: 13102
		private TableRowInstance[] m_headerRowInstances;

		// Token: 0x0400332F RID: 13103
		private TableGroupInstanceList m_tableGroupInstances;

		// Token: 0x04003330 RID: 13104
		private TableDetailInstanceList m_tableDetailInstances;

		// Token: 0x04003331 RID: 13105
		private TableRowInstance[] m_footerRowInstances;

		// Token: 0x04003332 RID: 13106
		private RenderingPagesRangesList m_renderingPages;

		// Token: 0x04003333 RID: 13107
		[NonSerialized]
		private int m_currentPage = -1;

		// Token: 0x04003334 RID: 13108
		[NonSerialized]
		private int m_numberOfChildrenOnThisPage;

		// Token: 0x04003335 RID: 13109
		[NonSerialized]
		private int m_startPage = -1;

		// Token: 0x04003336 RID: 13110
		[NonSerialized]
		private int m_endPage = -1;
	}
}

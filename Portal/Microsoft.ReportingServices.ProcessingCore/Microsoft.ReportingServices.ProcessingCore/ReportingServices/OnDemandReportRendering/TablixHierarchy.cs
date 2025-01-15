using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200036A RID: 874
	public sealed class TablixHierarchy : MemberHierarchy<TablixMember>
	{
		// Token: 0x0600215C RID: 8540 RVA: 0x00080E24 File Offset: 0x0007F024
		internal TablixHierarchy(Tablix owner, bool isColumn)
			: base(owner, isColumn)
		{
		}

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x0600215D RID: 8541 RVA: 0x00080E2E File Offset: 0x0007F02E
		private Tablix OwnerTablix
		{
			get
			{
				return this.m_owner as Tablix;
			}
		}

		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x00080E3C File Offset: 0x0007F03C
		public TablixMemberCollection MemberCollection
		{
			get
			{
				if (this.m_members == null)
				{
					if (this.OwnerTablix.IsOldSnapshot)
					{
						switch (this.OwnerTablix.SnapshotTablixType)
						{
						case Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.List:
							if (this.m_isColumn)
							{
								this.m_members = new ShimListMemberCollection(this, this.OwnerTablix);
							}
							else
							{
								this.m_members = new ShimListMemberCollection(this, this.OwnerTablix, this.OwnerTablix.RenderList.Contents);
							}
							break;
						case Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Table:
							this.OwnerTablix.ResetMemberCellDefinitionIndex(0);
							this.m_members = new ShimTableMemberCollection(this, this.OwnerTablix, this.m_isColumn);
							break;
						case Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix:
						{
							this.OwnerTablix.ResetMemberCellDefinitionIndex(0);
							MatrixMemberCollection matrixMemberCollection = (this.m_isColumn ? this.OwnerTablix.RenderMatrix.ColumnMemberCollection : this.OwnerTablix.RenderMatrix.RowMemberCollection);
							this.m_members = new ShimMatrixMemberCollection(this, this.OwnerTablix, this.m_isColumn, null, matrixMemberCollection, this.CreateMatrixMemberCache());
							break;
						}
						}
						if (!this.m_isColumn)
						{
							this.CalculatePropagatedPageBreak();
						}
					}
					else
					{
						Tablix tablixDef = this.OwnerTablix.TablixDef;
						if (tablixDef.TablixColumns != null)
						{
							this.m_members = new InternalTablixMemberCollection(this, this.OwnerTablix, null, this.m_isColumn ? tablixDef.TablixColumnMembers : tablixDef.TablixRowMembers);
						}
					}
				}
				return (TablixMemberCollection)this.m_members;
			}
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x00080FA4 File Offset: 0x0007F1A4
		private void CalculatePropagatedPageBreak()
		{
			Microsoft.ReportingServices.ReportRendering.DataRegion dataRegion = (Microsoft.ReportingServices.ReportRendering.DataRegion)this.m_owner.RenderReportItem;
			bool flag = dataRegion.SharedHidden == SharedHiddenState.Sometimes;
			PageBreakLocation pageBreakLocation = PageBreakHelper.GetPageBreakLocation(dataRegion.PageBreakAtStart, dataRegion.PageBreakAtEnd);
			if (this.m_members != null && this.m_members.Count > 0)
			{
				pageBreakLocation = PageBreakHelper.MergePageBreakLocations(this.CalculatePropagatedPageBreak(this.m_members, flag, this.OwnerTablix.SnapshotTablixType == Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Table), pageBreakLocation);
			}
			this.OwnerTablix.SetPageBreakLocation(pageBreakLocation);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x00081024 File Offset: 0x0007F224
		private PageBreakLocation CalculatePropagatedPageBreak(DataRegionMemberCollection<TablixMember> members, bool thisOrAnscestorHasToggle, bool isTable)
		{
			PageBreakLocation pageBreakLocation = PageBreakLocation.None;
			bool flag = false;
			ShimTablixMember shimTablixMember = null;
			for (int i = 0; i < members.Count; i++)
			{
				ShimTablixMember shimTablixMember2 = (ShimTablixMember)members[i];
				if (!shimTablixMember2.IsStatic)
				{
					shimTablixMember = shimTablixMember2;
					break;
				}
				if (isTable)
				{
					if (shimTablixMember2.RepeatOnNewPage)
					{
						flag = true;
					}
				}
				else if (shimTablixMember2.Children != null && shimTablixMember2.Children.Count > 0)
				{
					pageBreakLocation = this.CalculatePropagatedPageBreak(shimTablixMember2.Children, thisOrAnscestorHasToggle, false);
				}
			}
			if (shimTablixMember != null)
			{
				thisOrAnscestorHasToggle |= shimTablixMember.Visibility != null && shimTablixMember.Visibility.HiddenState == SharedHiddenState.Sometimes;
				PageBreakLocation pageBreakLocation2 = PageBreakLocation.None;
				Group currentShimRenderGroup = shimTablixMember.Group.CurrentShimRenderGroup;
				if (currentShimRenderGroup != null)
				{
					pageBreakLocation2 = PageBreakHelper.GetPageBreakLocation(currentShimRenderGroup.PageBreakAtStart, currentShimRenderGroup.PageBreakAtEnd);
				}
				if (shimTablixMember.Children != null)
				{
					pageBreakLocation2 = PageBreakHelper.MergePageBreakLocations(this.CalculatePropagatedPageBreak(shimTablixMember.Children, thisOrAnscestorHasToggle, isTable), pageBreakLocation2);
				}
				shimTablixMember.SetPropagatedPageBreak(pageBreakLocation2);
				if ((!isTable || flag) && pageBreakLocation2 != PageBreakLocation.None)
				{
					if (!thisOrAnscestorHasToggle)
					{
						pageBreakLocation = pageBreakLocation2;
					}
					shimTablixMember.SetPropagatedPageBreak(PageBreakLocation.Between);
				}
			}
			return pageBreakLocation;
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x0008112B File Offset: 0x0007F32B
		internal override void ResetContext()
		{
			this.ResetContext(true);
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x00081134 File Offset: 0x0007F334
		internal void ResetContext(bool clearCache)
		{
			if (clearCache)
			{
				this.OwnerTablix.ResetMemberCellDefinitionIndex(0);
			}
			if (this.m_members != null && this.OwnerTablix.IsOldSnapshot)
			{
				switch (this.OwnerTablix.SnapshotTablixType)
				{
				case Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.List:
					if (!this.m_isColumn)
					{
						((ShimListMemberCollection)this.m_members).UpdateContext(this.OwnerTablix.RenderList.Contents);
						return;
					}
					break;
				case Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Table:
					if (!this.m_isColumn)
					{
						((ShimTableMemberCollection)this.m_members).UpdateContext();
						return;
					}
					break;
				case Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix:
				{
					MatrixMemberInfoCache matrixMemberInfoCache = null;
					if (clearCache && this.m_isColumn)
					{
						matrixMemberInfoCache = this.CreateMatrixMemberCache();
					}
					((ShimMatrixMemberCollection)this.m_members).UpdateContext(matrixMemberInfoCache);
					break;
				}
				default:
					return;
				}
			}
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x000811F4 File Offset: 0x0007F3F4
		private MatrixMemberInfoCache CreateMatrixMemberCache()
		{
			if (this.m_isColumn)
			{
				MatrixMemberCollection columnMemberCollection = this.OwnerTablix.RenderMatrix.ColumnMemberCollection;
				if (columnMemberCollection.MatrixHeadingDef.SubHeading != null)
				{
					this.OwnerTablix.MatrixMemberColIndexes = new MatrixMemberInfoCache(-1, columnMemberCollection.Count);
				}
				else
				{
					this.OwnerTablix.MatrixMemberColIndexes = new MatrixMemberInfoCache(0, -1);
				}
				return this.OwnerTablix.MatrixMemberColIndexes;
			}
			return null;
		}
	}
}

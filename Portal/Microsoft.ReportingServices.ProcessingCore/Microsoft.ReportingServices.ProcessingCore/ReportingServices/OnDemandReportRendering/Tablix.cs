using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000363 RID: 867
	public sealed class Tablix : Microsoft.ReportingServices.OnDemandReportRendering.DataRegion, IPageBreakItem
	{
		// Token: 0x060020FA RID: 8442 RVA: 0x0007FEED File Offset: 0x0007E0ED
		internal Tablix(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, Microsoft.ReportingServices.ReportIntermediateFormat.Tablix reportItemDef, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(parentDefinitionPath, indexIntoParentCollectionDef, reportItemDef, renderingContext)
		{
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x0007FEFA File Offset: 0x0007E0FA
		internal Tablix(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, bool inSubtotal, Microsoft.ReportingServices.ReportRendering.List renderList, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, renderList, renderingContext)
		{
			this.m_snapshotDataRegionType = Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.List;
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x0007FF10 File Offset: 0x0007E110
		internal Tablix(IDefinitionPath parentDefinitionPath, int indexIntoParentCollection, bool inSubtotal, Microsoft.ReportingServices.ReportRendering.Table renderTable, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(parentDefinitionPath, indexIntoParentCollection, inSubtotal, renderTable, renderingContext)
		{
			this.m_snapshotDataRegionType = Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Table;
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x0007FF26 File Offset: 0x0007E126
		internal Tablix(IDefinitionPath parentDefinitionPath, int indexIntoParentCollection, bool inSubtotal, Microsoft.ReportingServices.ReportRendering.Matrix renderMatrix, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(parentDefinitionPath, indexIntoParentCollection, inSubtotal, renderMatrix, renderingContext)
		{
			this.m_snapshotDataRegionType = Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix;
		}

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x0007FF3C File Offset: 0x0007E13C
		public override DataElementOutputTypes DataElementOutput
		{
			get
			{
				if (this.m_isOldSnapshot && Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.List == this.m_snapshotDataRegionType && base.DataElementOutput == DataElementOutputTypes.Output)
				{
					return DataElementOutputTypes.ContentsOnly;
				}
				return base.DataElementOutput;
			}
		}

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x060020FF RID: 8447 RVA: 0x0007FF5F File Offset: 0x0007E15F
		public bool CanScroll
		{
			get
			{
				return !this.m_isOldSnapshot && this.TablixDef.CanScroll;
			}
		}

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x0007FF76 File Offset: 0x0007E176
		public bool KeepTogether
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return ((Microsoft.ReportingServices.ReportRendering.DataRegion)this.m_renderReportItem).KeepTogether;
				}
				return this.TablixDef.KeepTogether;
			}
		}

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06002101 RID: 8449 RVA: 0x0007FF9C File Offset: 0x0007E19C
		public TablixLayoutDirection LayoutDirection
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					if (Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix == this.m_snapshotDataRegionType)
					{
						return (TablixLayoutDirection)this.RenderMatrix.LayoutDirection;
					}
					return TablixLayoutDirection.LTR;
				}
				else
				{
					if (this.TablixDef.LayoutDirection)
					{
						return TablixLayoutDirection.RTL;
					}
					return TablixLayoutDirection.LTR;
				}
			}
		}

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x0007FFCD File Offset: 0x0007E1CD
		public TablixCorner Corner
		{
			get
			{
				if (this.m_corner == null)
				{
					this.m_corner = new TablixCorner(this);
				}
				return this.m_corner;
			}
		}

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x0007FFE9 File Offset: 0x0007E1E9
		public TablixHierarchy ColumnHierarchy
		{
			get
			{
				if (this.m_columns == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_columns = new TablixHierarchy(this, true);
					}
					else
					{
						this.m_columns = new TablixHierarchy(this, true);
					}
				}
				return this.m_columns;
			}
		}

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x0008001D File Offset: 0x0007E21D
		public TablixHierarchy RowHierarchy
		{
			get
			{
				if (this.m_rows == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_rows = new TablixHierarchy(this, false);
					}
					else
					{
						this.m_rows = new TablixHierarchy(this, false);
					}
				}
				return this.m_rows;
			}
		}

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x00080051 File Offset: 0x0007E251
		public TablixBody Body
		{
			get
			{
				if (this.m_body == null)
				{
					this.m_body = new TablixBody(this);
				}
				return this.m_body;
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x00080070 File Offset: 0x0007E270
		public int Columns
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					switch (this.m_snapshotDataRegionType)
					{
					case Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.List:
						return 0;
					case Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Table:
						return 0;
					case Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix:
						return this.RenderMatrix.Columns;
					}
				}
				return this.TablixDef.ColumnHeaderRowCount;
			}
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06002107 RID: 8455 RVA: 0x000800C0 File Offset: 0x0007E2C0
		public int Rows
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					switch (this.m_snapshotDataRegionType)
					{
					case Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.List:
						return 0;
					case Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Table:
						return 0;
					case Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix:
						return this.RenderMatrix.Rows;
					}
				}
				return this.TablixDef.RowHeaderColumnCount;
			}
		}

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x06002108 RID: 8456 RVA: 0x0008010D File Offset: 0x0007E30D
		public int GroupsBeforeRowHeaders
		{
			get
			{
				if (!this.m_isOldSnapshot)
				{
					return this.TablixDef.GroupsBeforeRowHeaders;
				}
				if (Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix == this.m_snapshotDataRegionType)
				{
					return this.RenderMatrix.GroupsBeforeRowHeaders;
				}
				return 0;
			}
		}

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x00080139 File Offset: 0x0007E339
		public bool RepeatRowHeaders
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix == this.m_snapshotDataRegionType;
				}
				return this.TablixDef.RepeatRowHeaders;
			}
		}

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x0600210A RID: 8458 RVA: 0x00080158 File Offset: 0x0007E358
		public bool RepeatColumnHeaders
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix == this.m_snapshotDataRegionType;
				}
				return this.TablixDef.RepeatColumnHeaders;
			}
		}

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x00080178 File Offset: 0x0007E378
		public bool FixedRowHeaders
		{
			get
			{
				if (!this.m_isOldSnapshot)
				{
					return this.TablixDef.FixedRowHeaders;
				}
				if (Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix == this.m_snapshotDataRegionType)
				{
					return this.RenderMatrix.RowGroupingFixedHeader;
				}
				return Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Table == this.m_snapshotDataRegionType && this.RenderTable.FixedHeader && !this.RenderTable.HasFixedColumnHeaders;
			}
		}

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x000801D8 File Offset: 0x0007E3D8
		public bool FixedColumnHeaders
		{
			get
			{
				if (!this.m_isOldSnapshot)
				{
					return this.TablixDef.FixedColumnHeaders;
				}
				if (Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix == this.m_snapshotDataRegionType)
				{
					return this.RenderMatrix.ColumnGroupingFixedHeader;
				}
				return Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Table == this.m_snapshotDataRegionType && this.RenderTable.FixedHeader && this.RenderTable.HasFixedColumnHeaders;
			}
		}

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x00080233 File Offset: 0x0007E433
		public bool OmitBorderOnPageBreak
		{
			get
			{
				return !this.m_isOldSnapshot && this.TablixDef.OmitBorderOnPageBreak;
			}
		}

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x0600210E RID: 8462 RVA: 0x0008024A File Offset: 0x0007E44A
		public BandLayoutOptions BandLayout
		{
			get
			{
				if (this.m_isOldSnapshot || this.TablixDef.BandLayout == null)
				{
					return null;
				}
				if (this.m_bandLayout == null)
				{
					this.m_bandLayout = new BandLayoutOptions(this.TablixDef.BandLayout);
				}
				return this.m_bandLayout;
			}
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x00080287 File Offset: 0x0007E487
		private ReportSizeProperty GetOrCreateMarginProperty(ref ReportSizeProperty property, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			if (this.m_isOldSnapshot || expression == null)
			{
				return null;
			}
			if (property == null)
			{
				property = new ReportSizeProperty(expression);
			}
			return property;
		}

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x000802A4 File Offset: 0x0007E4A4
		public ReportSizeProperty TopMargin
		{
			get
			{
				return this.GetOrCreateMarginProperty(ref this.m_topMargin, this.TablixDef.TopMargin);
			}
		}

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x06002111 RID: 8465 RVA: 0x000802BD File Offset: 0x0007E4BD
		public ReportSizeProperty BottomMargin
		{
			get
			{
				return this.GetOrCreateMarginProperty(ref this.m_bottomMargin, this.TablixDef.BottomMargin);
			}
		}

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x000802D6 File Offset: 0x0007E4D6
		public ReportSizeProperty LeftMargin
		{
			get
			{
				return this.GetOrCreateMarginProperty(ref this.m_leftMargin, this.TablixDef.LeftMargin);
			}
		}

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06002113 RID: 8467 RVA: 0x000802EF File Offset: 0x0007E4EF
		public ReportSizeProperty RightMargin
		{
			get
			{
				return this.GetOrCreateMarginProperty(ref this.m_rightMargin, this.TablixDef.RightMargin);
			}
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x00080308 File Offset: 0x0007E508
		internal void SetPageBreakLocation(PageBreakLocation pageBreakLocation)
		{
			if (this.m_isOldSnapshot)
			{
				this.m_propagatedPageBreak = new PageBreakLocation?(pageBreakLocation);
			}
		}

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x00080320 File Offset: 0x0007E520
		[Obsolete("Use PageBreak.BreakLocation instead.")]
		PageBreakLocation IPageBreakItem.PageBreakLocation
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					if (this.m_propagatedPageBreak == null)
					{
						this.m_propagatedPageBreak = new PageBreakLocation?(PageBreakLocation.None);
						TablixMemberCollection memberCollection = this.RowHierarchy.MemberCollection;
					}
					return this.m_propagatedPageBreak.Value;
				}
				PageBreak pageBreak = base.PageBreak;
				if (pageBreak.HasEnabledInstance)
				{
					return pageBreak.BreakLocation;
				}
				return PageBreakLocation.None;
			}
		}

		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x0008037D File Offset: 0x0007E57D
		public bool HideStaticsIfNoRows
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Table != this.m_snapshotDataRegionType || (this.RenderTable.TableHeader == null && this.RenderTable.TableFooter == null);
				}
				return this.TablixDef.HideStaticsIfNoRows;
			}
		}

		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x06002117 RID: 8471 RVA: 0x000803BC File Offset: 0x0007E5BC
		public override ReportSize Width
		{
			get
			{
				if (this.m_isOldSnapshot && Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix == this.m_snapshotDataRegionType && this.m_cachedWidth == null)
				{
					TablixHierarchy columnHierarchy = this.ColumnHierarchy;
					if (columnHierarchy != null && columnHierarchy.MemberCollection != null)
					{
						base.SetCachedWidth(columnHierarchy.MemberCollection.SizeDelta);
					}
				}
				return base.Width;
			}
		}

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x0008040C File Offset: 0x0007E60C
		public override ReportSize Height
		{
			get
			{
				if (this.m_isOldSnapshot && Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix == this.m_snapshotDataRegionType && this.m_cachedHeight == null)
				{
					TablixHierarchy rowHierarchy = this.RowHierarchy;
					if (rowHierarchy != null && rowHierarchy.MemberCollection != null)
					{
						base.SetCachedHeight(rowHierarchy.MemberCollection.SizeDelta);
					}
				}
				return base.Height;
			}
		}

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x0008045B File Offset: 0x0007E65B
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Tablix TablixDef
		{
			get
			{
				return (Microsoft.ReportingServices.ReportIntermediateFormat.Tablix)this.m_reportItemDef;
			}
		}

		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x00080468 File Offset: 0x0007E668
		internal override bool HasDataCells
		{
			get
			{
				return this.m_body != null && this.m_body.HasRowCollection;
			}
		}

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x0008047F File Offset: 0x0007E67F
		internal override IDataRegionRowCollection RowCollection
		{
			get
			{
				if (this.m_body != null)
				{
					return this.m_body.RowCollection;
				}
				return null;
			}
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x00080496 File Offset: 0x0007E696
		internal override ReportItemInstance GetOrCreateInstance()
		{
			if (this.m_instance == null)
			{
				this.m_instance = new TablixInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x0600211D RID: 8477 RVA: 0x000804B2 File Offset: 0x0007E6B2
		internal Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type SnapshotTablixType
		{
			get
			{
				return this.m_snapshotDataRegionType;
			}
		}

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x000804BA File Offset: 0x0007E6BA
		internal Microsoft.ReportingServices.ReportRendering.List RenderList
		{
			get
			{
				if (!this.m_isOldSnapshot || Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.List != this.m_snapshotDataRegionType)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return (Microsoft.ReportingServices.ReportRendering.List)this.m_renderReportItem;
			}
		}

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x000804E3 File Offset: 0x0007E6E3
		internal Microsoft.ReportingServices.ReportRendering.Table RenderTable
		{
			get
			{
				if (!this.m_isOldSnapshot || Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Table != this.m_snapshotDataRegionType)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return (Microsoft.ReportingServices.ReportRendering.Table)this.m_renderReportItem;
			}
		}

		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x0008050C File Offset: 0x0007E70C
		internal Microsoft.ReportingServices.ReportRendering.Matrix RenderMatrix
		{
			get
			{
				if (!this.m_isOldSnapshot || Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix != this.m_snapshotDataRegionType)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return (Microsoft.ReportingServices.ReportRendering.Matrix)this.m_renderReportItem;
			}
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x00080538 File Offset: 0x0007E738
		internal override void UpdateRenderReportItem(Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem)
		{
			base.UpdateRenderReportItem(renderReportItem);
			if (renderReportItem != null)
			{
				this.m_matrixRowDefinitionMapping = null;
				this.m_matrixColDefinitionMapping = null;
				if (Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix == this.m_snapshotDataRegionType && this.m_corner != null)
				{
					this.m_corner.ResetContext();
				}
				if (this.m_rows != null)
				{
					this.m_rows.ResetContext(true);
				}
				if (this.m_columns != null)
				{
					this.m_columns.ResetContext(true);
					return;
				}
			}
			else
			{
				if (Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix == this.m_snapshotDataRegionType && this.m_corner != null)
				{
					this.m_corner.ResetContext();
				}
				if (this.m_rows != null)
				{
					this.m_rows.ResetContext(false);
				}
				if (this.m_columns != null)
				{
					this.m_columns.ResetContext(false);
				}
			}
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000805E6 File Offset: 0x0007E7E6
		internal int GetCurrentMemberCellDefinitionIndex()
		{
			return this.m_memberCellDefinitionIndex;
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000805F0 File Offset: 0x0007E7F0
		internal int GetAndIncrementMemberCellDefinitionIndex()
		{
			int memberCellDefinitionIndex = this.m_memberCellDefinitionIndex;
			this.m_memberCellDefinitionIndex = memberCellDefinitionIndex + 1;
			return memberCellDefinitionIndex;
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0008060E File Offset: 0x0007E80E
		internal void ResetMemberCellDefinitionIndex(int startIndex)
		{
			this.m_memberCellDefinitionIndex = startIndex;
		}

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x00080618 File Offset: 0x0007E818
		internal int[] MatrixRowDefinitionMapping
		{
			get
			{
				if (this.m_isOldSnapshot && Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix == this.m_snapshotDataRegionType && this.m_matrixRowDefinitionMapping == null)
				{
					this.m_matrixRowDefinitionMapping = this.CalculateMatrixDefinitionMapping(((Microsoft.ReportingServices.ReportProcessing.Matrix)this.RenderMatrix.ReportItemDef).Rows);
				}
				return this.m_matrixRowDefinitionMapping;
			}
		}

		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x00080668 File Offset: 0x0007E868
		internal int[] MatrixColDefinitionMapping
		{
			get
			{
				if (this.m_isOldSnapshot && Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.Matrix == this.m_snapshotDataRegionType && this.m_matrixColDefinitionMapping == null)
				{
					this.m_matrixColDefinitionMapping = this.CalculateMatrixDefinitionMapping(((Microsoft.ReportingServices.ReportProcessing.Matrix)this.RenderMatrix.ReportItemDef).Columns);
				}
				return this.m_matrixColDefinitionMapping;
			}
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x000806B8 File Offset: 0x0007E8B8
		private int[] CalculateMatrixDefinitionMapping(MatrixHeading heading)
		{
			List<int> list = new List<int>();
			int num = 0;
			this.AddInnerHierarchy(heading, list, ref num);
			return list.ToArray();
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000806E0 File Offset: 0x0007E8E0
		private void AddInnerHierarchy(MatrixHeading heading, List<int> mapping, ref int definitionIndex)
		{
			if (heading == null)
			{
				int num = definitionIndex;
				definitionIndex = num + 1;
				mapping.Add(num);
				return;
			}
			if (heading.Grouping == null)
			{
				this.AddInnerStatics(heading, mapping, ref definitionIndex);
				return;
			}
			if (heading.Subtotal == null)
			{
				this.AddInnerHierarchy(heading.SubHeading, mapping, ref definitionIndex);
				return;
			}
			int num2 = definitionIndex;
			if (Subtotal.PositionType.Before == heading.Subtotal.Position)
			{
				this.AddInnerStatics(heading, mapping, ref definitionIndex);
				definitionIndex = num2;
				this.AddInnerHierarchy(heading.SubHeading, mapping, ref definitionIndex);
				return;
			}
			this.AddInnerHierarchy(heading.SubHeading, mapping, ref definitionIndex);
			definitionIndex = num2;
			this.AddInnerStatics(heading, mapping, ref definitionIndex);
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00080770 File Offset: 0x0007E970
		private void AddInnerStatics(MatrixHeading heading, List<int> mapping, ref int definitionIndex)
		{
			if (heading == null)
			{
				int num = definitionIndex;
				definitionIndex = num + 1;
				mapping.Add(num);
				return;
			}
			if (heading.Grouping == null)
			{
				int count = heading.ReportItems.Count;
				for (int i = 0; i < count; i++)
				{
					this.AddInnerHierarchy(heading.SubHeading, mapping, ref definitionIndex);
				}
				return;
			}
			this.AddInnerHierarchy(heading.SubHeading, mapping, ref definitionIndex);
		}

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x000807CD File Offset: 0x0007E9CD
		// (set) Token: 0x0600212B RID: 8491 RVA: 0x000807D5 File Offset: 0x0007E9D5
		internal MatrixMemberInfoCache MatrixMemberColIndexes
		{
			get
			{
				return this.m_matrixMemberColIndexes;
			}
			set
			{
				this.m_matrixMemberColIndexes = value;
			}
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000807E0 File Offset: 0x0007E9E0
		internal override void SetNewContextChildren()
		{
			if (this.m_corner != null)
			{
				this.m_corner.SetNewContext();
			}
			if (this.m_rows != null)
			{
				this.m_rows.SetNewContext();
			}
			if (this.m_columns != null)
			{
				this.m_columns.SetNewContext();
			}
			if (this.m_reportItemDef != null)
			{
				((Microsoft.ReportingServices.ReportIntermediateFormat.Tablix)this.m_reportItemDef).ResetTextBoxImpls(this.m_renderingContext.OdpContext);
			}
		}

		// Token: 0x04001098 RID: 4248
		private TablixCorner m_corner;

		// Token: 0x04001099 RID: 4249
		private TablixHierarchy m_columns;

		// Token: 0x0400109A RID: 4250
		private TablixHierarchy m_rows;

		// Token: 0x0400109B RID: 4251
		private TablixBody m_body;

		// Token: 0x0400109C RID: 4252
		private int[] m_matrixRowDefinitionMapping;

		// Token: 0x0400109D RID: 4253
		private int[] m_matrixColDefinitionMapping;

		// Token: 0x0400109E RID: 4254
		private int m_memberCellDefinitionIndex;

		// Token: 0x0400109F RID: 4255
		private MatrixMemberInfoCache m_matrixMemberColIndexes;

		// Token: 0x040010A0 RID: 4256
		private PageBreakLocation? m_propagatedPageBreak;

		// Token: 0x040010A1 RID: 4257
		private BandLayoutOptions m_bandLayout;

		// Token: 0x040010A2 RID: 4258
		private ReportSizeProperty m_topMargin;

		// Token: 0x040010A3 RID: 4259
		private ReportSizeProperty m_bottomMargin;

		// Token: 0x040010A4 RID: 4260
		private ReportSizeProperty m_leftMargin;

		// Token: 0x040010A5 RID: 4261
		private ReportSizeProperty m_rightMargin;
	}
}

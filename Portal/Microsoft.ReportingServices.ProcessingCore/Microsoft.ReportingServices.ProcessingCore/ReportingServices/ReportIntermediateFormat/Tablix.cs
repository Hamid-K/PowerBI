using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200051B RID: 1307
	internal sealed class Tablix : Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion, ICreateSubtotals, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060045B9 RID: 17849 RVA: 0x00123E24 File Offset: 0x00122024
		internal Tablix(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x00123E3B File Offset: 0x0012203B
		internal Tablix(int id, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(id, parent)
		{
		}

		// Token: 0x17001D2D RID: 7469
		// (get) Token: 0x060045BB RID: 17851 RVA: 0x00123E53 File Offset: 0x00122053
		// (set) Token: 0x060045BC RID: 17852 RVA: 0x00123E5B File Offset: 0x0012205B
		internal bool CanScroll
		{
			get
			{
				return this.m_canScroll;
			}
			set
			{
				this.m_canScroll = value;
			}
		}

		// Token: 0x17001D2E RID: 7470
		// (get) Token: 0x060045BD RID: 17853 RVA: 0x00123E64 File Offset: 0x00122064
		// (set) Token: 0x060045BE RID: 17854 RVA: 0x00123E6C File Offset: 0x0012206C
		internal bool ComputeHeight
		{
			get
			{
				return this.m_computeHeight;
			}
			set
			{
				this.m_computeHeight = value;
			}
		}

		// Token: 0x17001D2F RID: 7471
		// (get) Token: 0x060045BF RID: 17855 RVA: 0x00123E75 File Offset: 0x00122075
		// (set) Token: 0x060045C0 RID: 17856 RVA: 0x00123E7D File Offset: 0x0012207D
		internal bool ComputeWidth
		{
			get
			{
				return this.m_computeWidth;
			}
			set
			{
				this.m_computeWidth = value;
			}
		}

		// Token: 0x17001D30 RID: 7472
		// (get) Token: 0x060045C1 RID: 17857 RVA: 0x00123E86 File Offset: 0x00122086
		// (set) Token: 0x060045C2 RID: 17858 RVA: 0x00123E8E File Offset: 0x0012208E
		internal bool KeepTogether
		{
			get
			{
				return this.m_keepTogether;
			}
			set
			{
				this.m_keepTogether = value;
			}
		}

		// Token: 0x17001D31 RID: 7473
		// (get) Token: 0x060045C3 RID: 17859 RVA: 0x00123E97 File Offset: 0x00122097
		internal override Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Tablix;
			}
		}

		// Token: 0x17001D32 RID: 7474
		// (get) Token: 0x060045C4 RID: 17860 RVA: 0x00123E9B File Offset: 0x0012209B
		internal override HierarchyNodeList ColumnMembers
		{
			get
			{
				return this.m_tablixColumnMembers;
			}
		}

		// Token: 0x17001D33 RID: 7475
		// (get) Token: 0x060045C5 RID: 17861 RVA: 0x00123EA3 File Offset: 0x001220A3
		internal override HierarchyNodeList RowMembers
		{
			get
			{
				return this.m_tablixRowMembers;
			}
		}

		// Token: 0x17001D34 RID: 7476
		// (get) Token: 0x060045C6 RID: 17862 RVA: 0x00123EAB File Offset: 0x001220AB
		internal override RowList Rows
		{
			get
			{
				return this.m_tablixRows;
			}
		}

		// Token: 0x17001D35 RID: 7477
		// (get) Token: 0x060045C7 RID: 17863 RVA: 0x00123EB3 File Offset: 0x001220B3
		// (set) Token: 0x060045C8 RID: 17864 RVA: 0x00123EBB File Offset: 0x001220BB
		internal TablixMemberList TablixColumnMembers
		{
			get
			{
				return this.m_tablixColumnMembers;
			}
			set
			{
				this.m_tablixColumnMembers = value;
			}
		}

		// Token: 0x17001D36 RID: 7478
		// (get) Token: 0x060045C9 RID: 17865 RVA: 0x00123EC4 File Offset: 0x001220C4
		// (set) Token: 0x060045CA RID: 17866 RVA: 0x00123ECC File Offset: 0x001220CC
		internal TablixMemberList TablixRowMembers
		{
			get
			{
				return this.m_tablixRowMembers;
			}
			set
			{
				this.m_tablixRowMembers = value;
			}
		}

		// Token: 0x17001D37 RID: 7479
		// (get) Token: 0x060045CB RID: 17867 RVA: 0x00123ED5 File Offset: 0x001220D5
		// (set) Token: 0x060045CC RID: 17868 RVA: 0x00123EDD File Offset: 0x001220DD
		internal TablixRowList TablixRows
		{
			get
			{
				return this.m_tablixRows;
			}
			set
			{
				this.m_tablixRows = value;
			}
		}

		// Token: 0x17001D38 RID: 7480
		// (get) Token: 0x060045CD RID: 17869 RVA: 0x00123EE6 File Offset: 0x001220E6
		// (set) Token: 0x060045CE RID: 17870 RVA: 0x00123EEE File Offset: 0x001220EE
		internal List<TablixColumn> TablixColumns
		{
			get
			{
				return this.m_tablixColumns;
			}
			set
			{
				this.m_tablixColumns = value;
			}
		}

		// Token: 0x17001D39 RID: 7481
		// (get) Token: 0x060045CF RID: 17871 RVA: 0x00123EF7 File Offset: 0x001220F7
		// (set) Token: 0x060045D0 RID: 17872 RVA: 0x00123EFF File Offset: 0x001220FF
		internal List<List<TablixCornerCell>> Corner
		{
			get
			{
				return this.m_corner;
			}
			set
			{
				this.m_corner = value;
			}
		}

		// Token: 0x060045D1 RID: 17873 RVA: 0x00123F08 File Offset: 0x00122108
		protected override List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> ComputeDataRegionScopedItemsForDataProcessing()
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> list = base.ComputeDataRegionScopedItemsForDataProcessing();
			if (this.Corner != null)
			{
				for (int i = 0; i < this.Corner.Count; i++)
				{
					List<TablixCornerCell> list2 = this.Corner[i];
					if (list2 != null && list2.Count != 0)
					{
						for (int j = 0; j < list2.Count; j++)
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.MergeDataProcessingItems(list2[j], ref list);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x17001D3A RID: 7482
		// (get) Token: 0x060045D2 RID: 17874 RVA: 0x00123F72 File Offset: 0x00122172
		// (set) Token: 0x060045D3 RID: 17875 RVA: 0x00123F7A File Offset: 0x0012217A
		internal PageBreakLocation PropagatedPageBreakLocation
		{
			get
			{
				return this.m_propagatedPageBreakLocation;
			}
			set
			{
				this.m_propagatedPageBreakLocation = value;
			}
		}

		// Token: 0x17001D3B RID: 7483
		// (get) Token: 0x060045D4 RID: 17876 RVA: 0x00123F83 File Offset: 0x00122183
		// (set) Token: 0x060045D5 RID: 17877 RVA: 0x00123F8B File Offset: 0x0012218B
		internal int InnerRowLevelWithPageBreak
		{
			get
			{
				return this.m_innerRowLevelWithPageBreak;
			}
			set
			{
				this.m_innerRowLevelWithPageBreak = value;
			}
		}

		// Token: 0x17001D3C RID: 7484
		// (get) Token: 0x060045D6 RID: 17878 RVA: 0x00123F94 File Offset: 0x00122194
		// (set) Token: 0x060045D7 RID: 17879 RVA: 0x00123F9C File Offset: 0x0012219C
		internal int GroupsBeforeRowHeaders
		{
			get
			{
				return this.m_groupsBeforeRowHeaders;
			}
			set
			{
				this.m_groupsBeforeRowHeaders = value;
			}
		}

		// Token: 0x17001D3D RID: 7485
		// (get) Token: 0x060045D8 RID: 17880 RVA: 0x00123FA5 File Offset: 0x001221A5
		// (set) Token: 0x060045D9 RID: 17881 RVA: 0x00123FAD File Offset: 0x001221AD
		internal bool LayoutDirection
		{
			get
			{
				return this.m_layoutDirection;
			}
			set
			{
				this.m_layoutDirection = value;
			}
		}

		// Token: 0x17001D3E RID: 7486
		// (get) Token: 0x060045DA RID: 17882 RVA: 0x00123FB6 File Offset: 0x001221B6
		// (set) Token: 0x060045DB RID: 17883 RVA: 0x00123FBE File Offset: 0x001221BE
		public bool RepeatColumnHeaders
		{
			get
			{
				return this.m_repeatColumnHeaders;
			}
			set
			{
				this.m_repeatColumnHeaders = value;
			}
		}

		// Token: 0x17001D3F RID: 7487
		// (get) Token: 0x060045DC RID: 17884 RVA: 0x00123FC7 File Offset: 0x001221C7
		// (set) Token: 0x060045DD RID: 17885 RVA: 0x00123FCF File Offset: 0x001221CF
		public bool RepeatRowHeaders
		{
			get
			{
				return this.m_repeatRowHeaders;
			}
			set
			{
				this.m_repeatRowHeaders = value;
			}
		}

		// Token: 0x17001D40 RID: 7488
		// (get) Token: 0x060045DE RID: 17886 RVA: 0x00123FD8 File Offset: 0x001221D8
		// (set) Token: 0x060045DF RID: 17887 RVA: 0x00123FE0 File Offset: 0x001221E0
		internal bool FixedColumnHeaders
		{
			get
			{
				return this.m_fixedColumnHeaders;
			}
			set
			{
				this.m_fixedColumnHeaders = value;
			}
		}

		// Token: 0x17001D41 RID: 7489
		// (get) Token: 0x060045E0 RID: 17888 RVA: 0x00123FE9 File Offset: 0x001221E9
		// (set) Token: 0x060045E1 RID: 17889 RVA: 0x00123FF1 File Offset: 0x001221F1
		internal bool FixedRowHeaders
		{
			get
			{
				return this.m_fixedRowHeaders;
			}
			set
			{
				this.m_fixedRowHeaders = value;
			}
		}

		// Token: 0x17001D42 RID: 7490
		// (get) Token: 0x060045E2 RID: 17890 RVA: 0x00123FFA File Offset: 0x001221FA
		// (set) Token: 0x060045E3 RID: 17891 RVA: 0x00124002 File Offset: 0x00122202
		internal int ColumnHeaderRowCount
		{
			get
			{
				return this.m_columnHeaderRowCount;
			}
			set
			{
				this.m_columnHeaderRowCount = value;
			}
		}

		// Token: 0x17001D43 RID: 7491
		// (get) Token: 0x060045E4 RID: 17892 RVA: 0x0012400B File Offset: 0x0012220B
		// (set) Token: 0x060045E5 RID: 17893 RVA: 0x00124013 File Offset: 0x00122213
		internal int RowHeaderColumnCount
		{
			get
			{
				return this.m_rowHeaderColumnCount;
			}
			set
			{
				this.m_rowHeaderColumnCount = value;
			}
		}

		// Token: 0x17001D44 RID: 7492
		// (get) Token: 0x060045E6 RID: 17894 RVA: 0x0012401C File Offset: 0x0012221C
		// (set) Token: 0x060045E7 RID: 17895 RVA: 0x00124024 File Offset: 0x00122224
		internal bool OmitBorderOnPageBreak
		{
			get
			{
				return this.m_omitBorderOnPageBreak;
			}
			set
			{
				this.m_omitBorderOnPageBreak = value;
			}
		}

		// Token: 0x17001D45 RID: 7493
		// (get) Token: 0x060045E8 RID: 17896 RVA: 0x0012402D File Offset: 0x0012222D
		// (set) Token: 0x060045E9 RID: 17897 RVA: 0x00124035 File Offset: 0x00122235
		internal bool HideStaticsIfNoRows
		{
			get
			{
				return this.m_hideStaticsIfNoRows;
			}
			set
			{
				this.m_hideStaticsIfNoRows = value;
			}
		}

		// Token: 0x17001D46 RID: 7494
		// (get) Token: 0x060045EA RID: 17898 RVA: 0x0012403E File Offset: 0x0012223E
		internal TablixExprHost TablixExprHost
		{
			get
			{
				return this.m_tablixExprHost;
			}
		}

		// Token: 0x17001D47 RID: 7495
		// (get) Token: 0x060045EB RID: 17899 RVA: 0x00124046 File Offset: 0x00122246
		// (set) Token: 0x060045EC RID: 17900 RVA: 0x0012404E File Offset: 0x0012224E
		internal BandLayoutOptions BandLayout
		{
			get
			{
				return this.m_bandLayout;
			}
			set
			{
				this.m_bandLayout = value;
			}
		}

		// Token: 0x17001D48 RID: 7496
		// (get) Token: 0x060045ED RID: 17901 RVA: 0x00124057 File Offset: 0x00122257
		// (set) Token: 0x060045EE RID: 17902 RVA: 0x0012405F File Offset: 0x0012225F
		internal ExpressionInfo TopMargin
		{
			get
			{
				return this.m_topMargin;
			}
			set
			{
				this.m_topMargin = value;
			}
		}

		// Token: 0x17001D49 RID: 7497
		// (get) Token: 0x060045EF RID: 17903 RVA: 0x00124068 File Offset: 0x00122268
		// (set) Token: 0x060045F0 RID: 17904 RVA: 0x00124070 File Offset: 0x00122270
		internal ExpressionInfo BottomMargin
		{
			get
			{
				return this.m_bottomMargin;
			}
			set
			{
				this.m_bottomMargin = value;
			}
		}

		// Token: 0x17001D4A RID: 7498
		// (get) Token: 0x060045F1 RID: 17905 RVA: 0x00124079 File Offset: 0x00122279
		// (set) Token: 0x060045F2 RID: 17906 RVA: 0x00124081 File Offset: 0x00122281
		internal ExpressionInfo LeftMargin
		{
			get
			{
				return this.m_leftMargin;
			}
			set
			{
				this.m_leftMargin = value;
			}
		}

		// Token: 0x17001D4B RID: 7499
		// (get) Token: 0x060045F3 RID: 17907 RVA: 0x0012408A File Offset: 0x0012228A
		// (set) Token: 0x060045F4 RID: 17908 RVA: 0x00124092 File Offset: 0x00122292
		internal ExpressionInfo RightMargin
		{
			get
			{
				return this.m_rightMargin;
			}
			set
			{
				this.m_rightMargin = value;
			}
		}

		// Token: 0x17001D4C RID: 7500
		// (get) Token: 0x060045F5 RID: 17909 RVA: 0x0012409B File Offset: 0x0012229B
		// (set) Token: 0x060045F6 RID: 17910 RVA: 0x001240A3 File Offset: 0x001222A3
		internal bool EnableRowDrilldown
		{
			get
			{
				return this.m_enableRowDrilldown;
			}
			set
			{
				this.m_enableRowDrilldown = value;
			}
		}

		// Token: 0x17001D4D RID: 7501
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x001240AC File Offset: 0x001222AC
		// (set) Token: 0x060045F8 RID: 17912 RVA: 0x001240B4 File Offset: 0x001222B4
		internal bool EnableColumnDrilldown
		{
			get
			{
				return this.m_enableColumnDrilldown;
			}
			set
			{
				this.m_enableColumnDrilldown = value;
			}
		}

		// Token: 0x17001D4E RID: 7502
		// (get) Token: 0x060045F9 RID: 17913 RVA: 0x001240BD File Offset: 0x001222BD
		protected override IndexedExprHost UserSortExpressionsHost
		{
			get
			{
				if (this.m_tablixExprHost == null)
				{
					return null;
				}
				return this.m_tablixExprHost.UserSortExpressionsHost;
			}
		}

		// Token: 0x17001D4F RID: 7503
		// (get) Token: 0x060045FA RID: 17914 RVA: 0x001240D4 File Offset: 0x001222D4
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.InitData InitializationData
		{
			get
			{
				if (this.m_initData == null)
				{
					this.m_initData = new Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.InitData();
				}
				return this.m_initData;
			}
		}

		// Token: 0x17001D50 RID: 7504
		// (get) Token: 0x060045FB RID: 17915 RVA: 0x001240EF File Offset: 0x001222EF
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox> InScopeTextBoxes
		{
			get
			{
				return this.m_inScopeTextBoxes;
			}
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x001240F8 File Offset: 0x001222F8
		protected override void TraverseDataRegionLevelScopes(IRIFScopeVisitor visitor)
		{
			if (this.m_corner != null)
			{
				for (int i = 0; i < this.m_corner.Count; i++)
				{
					List<TablixCornerCell> list = this.m_corner[i];
					if (list != null)
					{
						for (int j = 0; j < list.Count; j++)
						{
							base.TraverseScopes(visitor, list[j], i, j);
						}
					}
				}
			}
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x00124154 File Offset: 0x00122354
		internal override bool Initialize(InitializationContext context)
		{
			context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InTablix;
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			context.ColumnHeaderLevelSizeList = this.m_initData.ColumnHeaderLevelSizeList;
			context.RowHeaderLevelSizeList = this.m_initData.RowHeaderLevelSizeList;
			if (!context.RegisterDataRegion(this))
			{
				return false;
			}
			context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataSet | Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion;
			bool flag = context.RegisterVisibility(this.m_visibility, this);
			context.ExprHostBuilder.DataRegionStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.Tablix, this.m_name);
			base.Initialize(context);
			if (!context.ErrorContext.HasError)
			{
				new Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.SizeCalculator(this).CalculateSizes(context);
			}
			this.InitializeBand(context);
			base.ExprHostID = context.ExprHostBuilder.DataRegionEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.Tablix);
			if (flag)
			{
				context.UnRegisterVisibility(this.m_visibility, this);
			}
			context.UnRegisterDataRegion(this);
			return false;
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x00124244 File Offset: 0x00122444
		private void InitializeBand(InitializationContext context)
		{
			this.ValidateMarginAndCreateExpression(this.m_topMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.MarginPosition.TopMargin, context);
			this.ValidateMarginAndCreateExpression(this.m_bottomMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.MarginPosition.BottomMargin, context);
			this.ValidateMarginAndCreateExpression(this.m_leftMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.MarginPosition.LeftMargin, context);
			this.ValidateMarginAndCreateExpression(this.m_rightMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.MarginPosition.RightMargin, context);
			if (this.m_bandLayout != null)
			{
				this.m_bandLayout.Initialize(this, context);
			}
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x001242A0 File Offset: 0x001224A0
		private void ValidateMarginAndCreateExpression(ExpressionInfo marginExpression, Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.MarginPosition position, InitializationContext context)
		{
			if (marginExpression != null)
			{
				string text = position.ToString();
				if (!marginExpression.IsExpression)
				{
					context.ValidateSize(marginExpression.OriginalText, text);
				}
				marginExpression.Initialize(text, context);
				context.ExprHostBuilder.MarginExpression(marginExpression, text);
			}
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x001242EC File Offset: 0x001224EC
		protected override void InitializeRVDirectionDependentItemsInCorner(InitializationContext context)
		{
			if (this.m_corner != null)
			{
				foreach (List<TablixCornerCell> list in this.m_corner)
				{
					foreach (TablixCornerCell tablixCornerCell in list)
					{
						if (tablixCornerCell.ColSpan > 0 && tablixCornerCell.RowSpan > 0)
						{
							if (tablixCornerCell.CellContents != null)
							{
								tablixCornerCell.CellContents.InitializeRVDirectionDependentItems(context);
							}
							if (tablixCornerCell.AltCellContents != null)
							{
								tablixCornerCell.AltCellContents.InitializeRVDirectionDependentItems(context);
							}
						}
					}
				}
			}
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x001243B4 File Offset: 0x001225B4
		protected override void InitializeRVDirectionDependentItems(int outerIndex, int innerIndex, InitializationContext context)
		{
			int num;
			int num2;
			if (this.m_processingInnerGrouping == Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.ProcessingInnerGroupings.Row)
			{
				num = outerIndex;
				num2 = innerIndex;
			}
			else
			{
				num = innerIndex;
				num2 = outerIndex;
			}
			if (this.m_tablixRows != null)
			{
				TablixCell tablixCell = this.m_tablixRows[num2].TablixCells[num];
				if (tablixCell != null)
				{
					tablixCell.InitializeRVDirectionDependentItems(context);
					if (context.HasUserSorts && !context.IsDataRegionScopedCell)
					{
						base.CopyCellAggregates(tablixCell);
					}
				}
			}
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x00124418 File Offset: 0x00122618
		protected override void DetermineGroupingExprValueCountInCorner(InitializationContext context, int groupingExprCount)
		{
			if (this.m_corner != null)
			{
				foreach (List<TablixCornerCell> list in this.m_corner)
				{
					foreach (TablixCornerCell tablixCornerCell in list)
					{
						if (tablixCornerCell.ColSpan > 0 && tablixCornerCell.RowSpan > 0)
						{
							if (tablixCornerCell.CellContents != null)
							{
								tablixCornerCell.CellContents.DetermineGroupingExprValueCount(context, groupingExprCount);
							}
							if (tablixCornerCell.AltCellContents != null)
							{
								tablixCornerCell.AltCellContents.DetermineGroupingExprValueCount(context, groupingExprCount);
							}
						}
					}
				}
			}
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x001244E0 File Offset: 0x001226E0
		protected override void DetermineGroupingExprValueCount(int outerIndex, int innerIndex, InitializationContext context, int groupingExprCount)
		{
			int num;
			int num2;
			if (this.m_processingInnerGrouping == Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.ProcessingInnerGroupings.Row)
			{
				num = outerIndex;
				num2 = innerIndex;
			}
			else
			{
				num = innerIndex;
				num2 = outerIndex;
			}
			if (this.m_tablixRows != null)
			{
				TablixCell tablixCell = this.m_tablixRows[num2].TablixCells[num];
				if (tablixCell != null)
				{
					tablixCell.DetermineGroupingExprValueCount(context, groupingExprCount);
				}
			}
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x0012452C File Offset: 0x0012272C
		internal static void ValidateKeepWithGroup(TablixMemberList members, InitializationContext context)
		{
			if (members != null && Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.HasDynamic(members))
			{
				int num = -1;
				int num2 = -1;
				bool flag = false;
				bool? flag2 = null;
				for (int i = 0; i < members.Count; i++)
				{
					if (members[i].Grouping != null)
					{
						num = i;
						flag = false;
						num2 = -1;
						flag2 = null;
					}
					else
					{
						if (flag2 != null)
						{
							bool? flag3 = flag2;
							bool repeatOnNewPage = members[i].RepeatOnNewPage;
							if (!((flag3.GetValueOrDefault() == repeatOnNewPage) & (flag3 != null)))
							{
								context.ErrorContext.Register(ProcessingErrorCode.rsInvalidRepeatOnNewPage, Severity.Error, context.ObjectType, context.ObjectName, "TablixMember", new string[]
								{
									"RepeatOnNewPage",
									flag2.Value ? "True" : "False",
									members[i].RepeatOnNewPage ? "True" : "False"
								});
							}
						}
						else
						{
							flag2 = new bool?(members[i].RepeatOnNewPage);
						}
						if (flag && members[i].KeepWithGroup != KeepWithGroup.After)
						{
							context.ErrorContext.Register(ProcessingErrorCode.rsInvalidKeepWithGroup, Severity.Error, context.ObjectType, context.ObjectName, "TablixMember", new string[]
							{
								"KeepWithGroup",
								"After",
								(members[i].KeepWithGroup == KeepWithGroup.None) ? "None" : "Before"
							});
						}
						else if (members[i].KeepWithGroup == KeepWithGroup.Before)
						{
							if (num == -1)
							{
								if (members[i].ParentMember != null)
								{
									members[i].KeepWithGroup = members[i].ParentMember.KeepWithGroup;
								}
								else
								{
									members[i].KeepWithGroup = KeepWithGroup.None;
								}
							}
							else if (num != i - 1 && members[i - 1].KeepWithGroup != KeepWithGroup.Before)
							{
								context.ErrorContext.Register(ProcessingErrorCode.rsInvalidKeepWithGroup, Severity.Error, context.ObjectType, context.ObjectName, "TablixMember", new string[]
								{
									"KeepWithGroup",
									"Before",
									(members[i - 1].KeepWithGroup == KeepWithGroup.None) ? "None" : "After"
								});
							}
						}
						else if (members[i].KeepWithGroup == KeepWithGroup.After)
						{
							flag = true;
							num2 = i;
						}
					}
				}
				if (flag)
				{
					for (int j = num2; j < members.Count; j++)
					{
						if (members[j].ParentMember != null)
						{
							members[j].KeepWithGroup = members[j].ParentMember.KeepWithGroup;
						}
						else
						{
							members[j].KeepWithGroup = KeepWithGroup.None;
						}
					}
				}
			}
		}

		// Token: 0x06004605 RID: 17925 RVA: 0x00124800 File Offset: 0x00122A00
		private static bool HasDynamic(TablixMemberList members)
		{
			using (IEnumerator enumerator = members.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!((TablixMember)enumerator.Current).IsStatic)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004606 RID: 17926 RVA: 0x0012485C File Offset: 0x00122A5C
		protected override void InitializeCorner(InitializationContext context)
		{
			if ((this.m_columnHeaderRowCount == 0 || this.m_rowHeaderColumnCount == 0) && this.m_corner == null)
			{
				return;
			}
			if (this.m_corner == null || this.m_corner.Count != this.m_columnHeaderRowCount)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfTablixCornerRows, Severity.Error, context.ObjectType, context.ObjectName, "TablixCornerRows", Array.Empty<string>());
			}
			if (this.m_corner != null)
			{
				int[] array = new int[this.m_rowHeaderColumnCount];
				int[] array2 = new int[this.m_columnHeaderRowCount];
				for (int i = 0; i < this.m_corner.Count; i++)
				{
					List<TablixCornerCell> list = this.m_corner[i];
					for (int j = 0; j < list.Count; j++)
					{
						if (list.Count != this.m_rowHeaderColumnCount)
						{
							context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfTablixCornerCells, Severity.Error, context.ObjectType, context.ObjectName, "TablixCornerCell", new string[] { i.ToString(CultureInfo.InvariantCulture.NumberFormat) });
							return;
						}
						TablixCornerCell tablixCornerCell = list[j];
						Global.Tracer.Assert((tablixCornerCell.ColSpan == 0) ? (tablixCornerCell.RowSpan == 0) : (tablixCornerCell.RowSpan > 0), "(((cell.ColSpan == 0) ? (cell.RowSpan == 0) : cell.RowSpan > 0))");
						int num = 0;
						while (num < tablixCornerCell.ColSpan && j + num < array.Length)
						{
							array[j + num] += tablixCornerCell.RowSpan;
							num++;
						}
						int num2 = 0;
						while (num2 < tablixCornerCell.RowSpan && i + num2 < array2.Length)
						{
							array2[i + num2] += tablixCornerCell.ColSpan;
							num2++;
						}
						tablixCornerCell.Initialize(base.ID, -1, i, j, context);
					}
				}
				for (int k = 0; k < array.Length; k++)
				{
					if (array[k] != this.m_columnHeaderRowCount)
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsInvalidTablixCornerRowSpans, Severity.Error, context.ObjectType, context.ObjectName, "TablixCornerRows", new string[] { k.ToString(CultureInfo.InvariantCulture.NumberFormat) });
					}
				}
				for (int l = 0; l < array2.Length; l++)
				{
					if (array2[l] != this.m_rowHeaderColumnCount)
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsInvalidTablixCornerColumnSpans, Severity.Error, context.ObjectType, context.ObjectName, "TablixCornerCell", new string[] { l.ToString(CultureInfo.InvariantCulture.NumberFormat) });
					}
				}
			}
		}

		// Token: 0x06004607 RID: 17927 RVA: 0x00124AF0 File Offset: 0x00122CF0
		protected override bool InitializeRows(InitializationContext context)
		{
			double num = 0.0;
			double num2 = 0.0;
			bool flag = true;
			if ((this.m_tablixColumnMembers != null && this.m_tablixColumns == null) || (this.m_tablixColumnMembers == null && this.m_tablixColumns != null) || (this.m_tablixColumns != null && this.m_tablixColumns.Count != this.m_columnCount))
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfTablixColumns, Severity.Error, context.ObjectType, context.ObjectName, "TablixColumns", Array.Empty<string>());
				flag = false;
			}
			else if (this.m_tablixColumns != null)
			{
				foreach (TablixColumn tablixColumn in this.m_tablixColumns)
				{
					tablixColumn.Initialize(context);
					if (!tablixColumn.ForAutoSubtotal)
					{
						num += tablixColumn.WidthValue;
					}
				}
			}
			if ((this.m_tablixRowMembers != null && this.m_tablixRows == null) || (this.m_tablixRowMembers == null && this.m_tablixRows != null) || (this.m_tablixRows != null && this.m_tablixRows.Count != this.m_rowCount))
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfTablixRows, Severity.Error, context.ObjectType, context.ObjectName, "TablixRows", Array.Empty<string>());
				flag = false;
			}
			if (this.m_tablixRows != null)
			{
				for (int i = 0; i < this.m_tablixRows.Count; i++)
				{
					TablixRow tablixRow = this.TablixRows[i];
					if (tablixRow == null || tablixRow.Cells == null || tablixRow.Cells.Count != this.m_columnCount)
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfTablixCells, Severity.Error, context.ObjectType, context.ObjectName, "TablixCells", Array.Empty<string>());
						flag = false;
					}
					tablixRow.Initialize(context);
					if (!tablixRow.ForAutoSubtotal)
					{
						num2 += tablixRow.HeightValue;
					}
					int num3 = 0;
					for (int j = 0; j < tablixRow.Cells.Count; j++)
					{
						TablixCell tablixCell = tablixRow.TablixCells[j];
						if (tablixCell.ColSpan > 1 && !this.ValidateColSpan(this, j, tablixCell.ColSpan))
						{
							context.ErrorContext.Register(ProcessingErrorCode.rsInvalidTablixCellColSpan, Severity.Error, context.ObjectType, context.ObjectName, "TablixCell", Array.Empty<string>());
							flag = false;
						}
						num3 += tablixCell.ColSpan;
					}
					if (num3 != this.m_columnCount)
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsInvalidTablixCellColSpans, Severity.Error, context.ObjectType, context.ObjectName, "TablixCells", Array.Empty<string>());
						flag = false;
					}
				}
			}
			Math.Round(num2, 10);
			Math.Round(num, 10);
			if (!this.CanScroll)
			{
				this.m_heightValue = num2;
				this.m_widthValue = num;
			}
			else
			{
				if (this.ComputeHeight)
				{
					this.m_heightValue = num2;
				}
				else
				{
					this.m_heightValue = new ReportSize(this.m_height).ToMillimeters();
				}
				if (this.ComputeWidth)
				{
					this.m_widthValue = num;
				}
				else
				{
					this.m_widthValue = new ReportSize(this.m_width).ToMillimeters();
				}
			}
			return flag;
		}

		// Token: 0x06004608 RID: 17928 RVA: 0x00124E24 File Offset: 0x00123024
		private bool ValidateColSpan(Microsoft.ReportingServices.ReportIntermediateFormat.Tablix tablix, int index, int colSpan)
		{
			int num = -1;
			foreach (object obj in tablix.ColumnMembers)
			{
				TablixMember tablixMember = (TablixMember)obj;
				if (!this.ValidateColSpan(tablixMember, index, colSpan, ref num))
				{
					return false;
				}
				if (num >= index + colSpan - 1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x00124E9C File Offset: 0x0012309C
		private bool ValidateColSpan(TablixMember aMember, int index, int colSpan, ref int current)
		{
			if (current >= index && !aMember.IsStatic)
			{
				return false;
			}
			if (aMember.SubMembers != null && aMember.SubMembers.Count > 0)
			{
				using (IEnumerator enumerator = aMember.SubMembers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						TablixMember tablixMember = (TablixMember)obj;
						if (!this.ValidateColSpan(tablixMember, index, colSpan, ref current))
						{
							return false;
						}
						if (current >= index + colSpan - 1)
						{
							return true;
						}
					}
					goto IL_0082;
				}
			}
			current++;
			IL_0082:
			return current < index || aMember.IsStatic;
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x00124F4C File Offset: 0x0012314C
		protected override bool ValidateInnerStructure(InitializationContext context)
		{
			if (this.m_rowCount > 0 && this.m_columnCount > 0)
			{
				if (this.m_initData.IsTopLevelDataRegion)
				{
					if ((this.m_fixedRowHeaders && this.m_initData.HasFixedColData) || (this.m_fixedColumnHeaders && this.m_initData.HasFixedRowData))
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsInvalidFixedHeaderOnOppositeHierarchy, Severity.Error, context.ObjectType, context.ObjectName, "FixedHeader", new string[] { this.m_fixedColumnHeaders ? "FixedColumnHeaders" : "FixedRowHeaders" });
					}
					if (this.m_initData.HasFixedRowData && !this.m_tablixRowMembers[0].FixedData)
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsInvalidFixedDataRowPosition, Severity.Error, context.ObjectType, context.ObjectName, "FixedData", Array.Empty<string>());
					}
					if (this.m_initData.HasFixedColData && this.m_groupsBeforeRowHeaders > 0 && this.m_tablixColumnMembers[0].FixedData)
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsInvalidFixedDataColumnPosition, Severity.Error, context.ObjectType, context.ObjectName, "FixedData", Array.Empty<string>());
					}
					if (this.m_initData.HasFixedColData)
					{
						for (int i = 0; i < this.m_tablixRows.Count; i++)
						{
							TablixRow tablixRow = this.m_tablixRows[i];
							int num = tablixRow.TablixCells[this.m_initData.FixedColStartIndex].ColSpan;
							if (num > 0)
							{
								for (int j = this.m_initData.FixedColStartIndex + 1; j < this.m_initData.FixedColStartIndex + this.m_initData.FixedColLength; j++)
								{
									num += tablixRow.TablixCells[j].ColSpan;
								}
							}
							if (num != this.m_initData.FixedColLength)
							{
								context.ErrorContext.Register(ProcessingErrorCode.rsInvalidFixedDataBodyCellSpans, Severity.Error, context.ObjectType, context.ObjectName, i.ToString(CultureInfo.InvariantCulture), Array.Empty<string>());
							}
						}
					}
				}
				else if (this.m_initData.HasFixedColData || this.m_initData.HasFixedRowData || this.m_fixedRowHeaders || this.m_fixedColumnHeaders)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsFixedHeadersInInnerDataRegion, Severity.Error, context.ObjectType, context.ObjectName, "FixedHeader", Array.Empty<string>());
				}
				if (this.m_groupsBeforeRowHeaders > 0 && this.m_tablixColumnMembers[0].Grouping == null)
				{
					this.m_groupsBeforeRowHeaders = 0;
				}
			}
			return true;
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x001251E4 File Offset: 0x001233E4
		protected override bool InitializeColumnMembers(InitializationContext context)
		{
			if (this.m_tablixColumnMembers == null)
			{
				this.m_heightValue = 0.0;
				this.m_height = "0mm";
				return false;
			}
			bool flag = base.InitializeColumnMembers(context);
			if (flag && (!this.CanScroll || this.ComputeHeight))
			{
				this.m_heightValue += context.GetTotalHeaderSize(true, this.m_columnHeaderRowCount);
				this.m_heightValue = Math.Round(this.m_heightValue, 10);
				this.m_height = Microsoft.ReportingServices.ReportPublishing.Converter.ConvertSize(this.m_heightValue);
			}
			return flag;
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x00125270 File Offset: 0x00123470
		protected override bool InitializeRowMembers(InitializationContext context)
		{
			if (this.m_tablixColumnMembers == null)
			{
				this.m_widthValue = 0.0;
				this.m_width = "0mm";
				return false;
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.ValidateKeepWithGroup(this.m_tablixRowMembers, context);
			bool flag = base.InitializeRowMembers(context);
			if (flag && (!this.CanScroll || this.ComputeWidth))
			{
				this.m_widthValue += context.GetTotalHeaderSize(false, this.m_rowHeaderColumnCount);
				this.m_widthValue = Math.Round(this.m_widthValue, 10);
				this.m_width = Microsoft.ReportingServices.ReportPublishing.Converter.ConvertSize(this.m_widthValue);
			}
			return flag;
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x00125306 File Offset: 0x00123506
		protected override void InitializeData(InitializationContext context)
		{
			context.RegisterReportItems(this.m_tablixRows);
			base.InitializeData(context);
			context.UnRegisterReportItems(this.m_tablixRows);
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x00125329 File Offset: 0x00123529
		internal bool ValidateBandReportItemReference(string reportItemName)
		{
			return reportItemName == null || this.ContainsReportItemInCurrentScope(reportItemName, false, true);
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x0012533C File Offset: 0x0012353C
		private bool IsOrContainsReportItemInCurrentScope(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem currentItem, string reportItemName)
		{
			if (currentItem == null)
			{
				return false;
			}
			if (string.CompareOrdinal(currentItem.Name, reportItemName) == 0)
			{
				return true;
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType = currentItem.GetObjectType();
			if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Rectangle)
			{
				if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Tablix)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.Tablix tablix = (Microsoft.ReportingServices.ReportIntermediateFormat.Tablix)currentItem;
					if (!tablix.HasFilters)
					{
						return tablix.ContainsReportItemInCurrentScope(reportItemName, true, false);
					}
				}
				return false;
			}
			return this.ContainsReportItemInCurrentScope(((Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)currentItem).ReportItems, reportItemName);
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x001253A4 File Offset: 0x001235A4
		private bool ContainsReportItemInCurrentScope(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection items, string reportItemName)
		{
			if (items == null)
			{
				return false;
			}
			foreach (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem in items)
			{
				if (this.IsOrContainsReportItemInCurrentScope(reportItem, reportItemName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x001253FC File Offset: 0x001235FC
		private bool ContainsReportItemInCurrentScope(string reportItemName, bool includeCorner, bool includeDynamics)
		{
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			return (includeCorner && this.CornerContainsReportItemInCurrentScope(reportItemName)) || this.ContainsReportItemInCurrentScope(this.m_tablixRowMembers, reportItemName, includeDynamics, ref list) || this.ContainsReportItemInCurrentScope(this.m_tablixColumnMembers, reportItemName, includeDynamics, ref list2) || this.BodyContainsReportItemInCurrentScope(list, list2, reportItemName);
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x00125450 File Offset: 0x00123650
		private bool CornerContainsReportItemInCurrentScope(string reportItemName)
		{
			if (this.m_corner == null)
			{
				return false;
			}
			foreach (List<TablixCornerCell> list in this.m_corner)
			{
				if (list != null)
				{
					foreach (TablixCornerCell tablixCornerCell in list)
					{
						if (this.ContainsReportItemInCurrentScope(tablixCornerCell, reportItemName))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x001254F4 File Offset: 0x001236F4
		private bool ContainsReportItemInCurrentScope(TablixCellBase cell, string reportItemName)
		{
			return cell != null && (this.IsOrContainsReportItemInCurrentScope(cell.CellContents, reportItemName) || this.IsOrContainsReportItemInCurrentScope(cell.AltCellContents, reportItemName));
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x00125519 File Offset: 0x00123719
		private bool ContainsReportItemInCurrentScope(TablixHeader header, string reportItemName)
		{
			return header != null && (this.IsOrContainsReportItemInCurrentScope(header.CellContents, reportItemName) || this.IsOrContainsReportItemInCurrentScope(header.AltCellContents, reportItemName));
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x00125540 File Offset: 0x00123740
		private bool ContainsReportItemInCurrentScope(TablixMemberList members, string reportItemName, bool includeDynamics, ref List<int> memberCellIndices)
		{
			if (members == null)
			{
				return false;
			}
			foreach (object obj in members)
			{
				TablixMember tablixMember = (TablixMember)obj;
				if (tablixMember.IsStatic || includeDynamics)
				{
					if (this.ContainsReportItemInCurrentScope(tablixMember.TablixHeader, reportItemName) || this.ContainsReportItemInCurrentScope(tablixMember.SubMembers, reportItemName, includeDynamics, ref memberCellIndices))
					{
						return true;
					}
					if (tablixMember.IsLeaf)
					{
						memberCellIndices.Add(tablixMember.MemberCellIndex);
					}
				}
			}
			return false;
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x001255DC File Offset: 0x001237DC
		private bool BodyContainsReportItemInCurrentScope(List<int> rowCellIndices, List<int> colCellIndices, string reportItemName)
		{
			foreach (int num in rowCellIndices)
			{
				TablixRow tablixRow = this.TablixRows[num];
				if (tablixRow.Cells != null)
				{
					foreach (int num2 in colCellIndices)
					{
						TablixCell tablixCell = tablixRow.TablixCells[num2];
						if (this.ContainsReportItemInCurrentScope(tablixCell, reportItemName))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06004617 RID: 17943 RVA: 0x00125694 File Offset: 0x00123894
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Tablix tablix = (Microsoft.ReportingServices.ReportIntermediateFormat.Tablix)base.PublishClone(context);
			context.CurrentDataRegionClone = tablix;
			if (this.m_tablixColumnMembers != null)
			{
				tablix.m_tablixColumnMembers = new TablixMemberList(this.m_tablixColumnMembers.Count);
				foreach (object obj in this.m_tablixColumnMembers)
				{
					TablixMember tablixMember = (TablixMember)obj;
					tablix.m_tablixColumnMembers.Add(tablixMember.PublishClone(context, tablix));
				}
			}
			if (this.m_tablixRowMembers != null)
			{
				tablix.m_tablixRowMembers = new TablixMemberList(this.m_tablixRowMembers.Count);
				foreach (object obj2 in this.m_tablixRowMembers)
				{
					TablixMember tablixMember2 = (TablixMember)obj2;
					tablix.m_tablixRowMembers.Add(tablixMember2.PublishClone(context, tablix));
				}
			}
			if (this.m_corner != null)
			{
				tablix.m_corner = new List<List<TablixCornerCell>>(this.m_corner.Count);
				foreach (List<TablixCornerCell> list in this.m_corner)
				{
					List<TablixCornerCell> list2 = new List<TablixCornerCell>(list.Count);
					foreach (TablixCornerCell tablixCornerCell in list)
					{
						list2.Add((TablixCornerCell)tablixCornerCell.PublishClone(context));
					}
					tablix.m_corner.Add(list2);
				}
			}
			if (this.m_tablixRows != null)
			{
				tablix.m_tablixRows = new TablixRowList(this.m_tablixRows.Count);
				foreach (object obj3 in this.m_tablixRows)
				{
					TablixRow tablixRow = (TablixRow)obj3;
					tablix.m_tablixRows.Add((TablixRow)tablixRow.PublishClone(context));
				}
			}
			if (this.m_tablixColumns != null)
			{
				tablix.m_tablixColumns = new List<TablixColumn>(this.m_tablixColumns.Count);
				foreach (TablixColumn tablixColumn in this.m_tablixColumns)
				{
					tablix.m_tablixColumns.Add((TablixColumn)tablixColumn.PublishClone(context));
				}
			}
			context.CreateSubtotalsDefinitions.Add(tablix);
			return tablix;
		}

		// Token: 0x06004618 RID: 17944 RVA: 0x00125960 File Offset: 0x00123B60
		protected override ReportHierarchyNode CreateHierarchyNode(int id)
		{
			return new TablixMember(id, this);
		}

		// Token: 0x06004619 RID: 17945 RVA: 0x00125969 File Offset: 0x00123B69
		protected override Row CreateRow(int id, int columnCount)
		{
			return new TablixRow(id)
			{
				Height = "0mm",
				TablixCells = new TablixCellList(columnCount)
			};
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x00125988 File Offset: 0x00123B88
		protected override Cell CreateCell(int id, int rowIndex, int colIndex)
		{
			TablixCell tablixCell = new TablixCell(id, this);
			if (rowIndex != -1)
			{
				tablixCell.ColSpan = 1;
				tablixCell.RowSpan = ((TablixCell)this.Rows[rowIndex].Cells[0]).RowSpan;
			}
			else if (colIndex != -1)
			{
				tablixCell.ColSpan = ((TablixCell)this.Rows[0].Cells[colIndex]).ColSpan;
				tablixCell.RowSpan = 1;
			}
			return tablixCell;
		}

		// Token: 0x0600461B RID: 17947 RVA: 0x00125A04 File Offset: 0x00123C04
		protected override void CreateDomainScopeRowsAndCells(AutomaticSubtotalContext context, ReportHierarchyNode member)
		{
			base.CreateDomainScopeRowsAndCells(context, member);
			if (member.IsColumn)
			{
				TablixColumn tablixColumn = new TablixColumn(context.GenerateID());
				tablixColumn.Width = "0mm";
				this.m_tablixColumns.Insert(this.ColumnMembers.GetMemberIndex(member), tablixColumn);
			}
		}

		// Token: 0x0600461C RID: 17948 RVA: 0x00125A54 File Offset: 0x00123C54
		public void CreateAutomaticSubtotals(AutomaticSubtotalContext context)
		{
			if (this.m_createdSubtotals)
			{
				return;
			}
			if (this.m_tablixRows != null && this.m_rowCount == this.m_tablixRows.Count && this.m_tablixColumns != null && this.m_tablixColumns.Count == this.m_columnCount)
			{
				for (int i = 0; i < this.m_tablixRows.Count; i++)
				{
					if (this.m_tablixRows[i].Cells == null || this.m_tablixRows[i].Cells.Count != this.m_columnCount)
					{
						return;
					}
				}
				context.Location = Microsoft.ReportingServices.ReportPublishing.LocationFlags.None;
				context.ObjectType = this.ObjectType;
				context.ObjectName = "Tablix";
				context.CurrentDataRegion = this;
				context.OriginalRowCount = this.m_rowCount;
				context.OriginalColumnCount = this.m_columnCount;
				context.CellLists = new List<CellList>(this.m_tablixRows.Count);
				for (int j = 0; j < this.m_tablixRows.Count; j++)
				{
					context.CellLists.Add(new CellList());
				}
				context.TablixColumns = new List<TablixColumn>(this.m_tablixColumns.Count);
				context.Rows = new RowList(this.m_tablixRows.Count);
				context.CurrentScope = this.m_name;
				context.CurrentDataScope = this;
				context.StartIndex = 0;
				this.CreateAutomaticSubtotals(context, this.m_tablixColumnMembers, true);
				context.StartIndex = 0;
				this.CreateAutomaticSubtotals(context, this.m_tablixRowMembers, false);
				context.CurrentScope = null;
				context.CurrentDataScope = null;
				this.m_createdSubtotals = true;
				return;
			}
		}

		// Token: 0x0600461D RID: 17949 RVA: 0x00125BF4 File Offset: 0x00123DF4
		private int CreateAutomaticSubtotals(AutomaticSubtotalContext context, TablixMemberList members, bool isColumn)
		{
			int num = 0;
			bool flag = this.AllSiblingsHaveConditionalOrToggleableVisibility(members);
			for (int i = 0; i < members.Count; i++)
			{
				TablixMember tablixMember = members[i];
				if (tablixMember.Grouping != null && tablixMember.HasToggleableVisibility && flag)
				{
					context.CurrentIndex = context.StartIndex;
					if (isColumn)
					{
						foreach (CellList cellList in context.CellLists)
						{
							cellList.Clear();
						}
						context.TablixColumns.Clear();
					}
					else
					{
						context.Rows.Clear();
					}
					int num2 = 0;
					bool flag2 = true;
					context.HeaderLevel = tablixMember.HeaderLevel;
					Global.Tracer.Assert(tablixMember.HeaderLevelHasStaticArray != null, "(member.HeaderLevelHasStaticArray != null)");
					context.HeaderLevelHasStaticArray = tablixMember.HeaderLevelHasStaticArray;
					base.BuildAndSetupAxisScopeTreeForAutoSubtotals(ref context, tablixMember);
					TablixMember tablixMember2 = tablixMember.CreateAutomaticSubtotalClone(context, tablixMember.ParentMember, true, out num2, ref flag2);
					context.AdjustReferences();
					tablixMember2.IsAutoSubtotal = true;
					if (i + 1 < members.Count)
					{
						TablixMember tablixMember3 = members[i + 1];
						if (tablixMember3.IsStatic && tablixMember3.KeepWithGroup == KeepWithGroup.Before)
						{
							tablixMember2.KeepWithGroup = KeepWithGroup.Before;
							tablixMember2.RepeatOnNewPage = tablixMember3.RepeatOnNewPage;
						}
					}
					members.Insert(i + 1, tablixMember2);
					num = context.CurrentIndex - context.StartIndex;
					if (isColumn)
					{
						for (int j = 0; j < this.m_tablixRows.Count; j++)
						{
							this.m_tablixRows[j].Cells.InsertRange(context.CurrentIndex, context.CellLists[j]);
						}
						this.m_tablixColumns.InsertRange(context.CurrentIndex, context.TablixColumns);
						this.m_columnCount += num;
					}
					else
					{
						this.m_tablixRows.InsertRange(context.CurrentIndex, context.Rows);
						this.m_rowCount += num;
					}
					if (tablixMember.SubMembers != null)
					{
						context.CurrentScope = tablixMember.Grouping.Name;
						context.CurrentDataScope = tablixMember;
						int num3 = this.CreateAutomaticSubtotals(context, tablixMember.SubMembers, isColumn);
						if (isColumn)
						{
							tablixMember.ColSpan += num3;
						}
						else
						{
							tablixMember.RowSpan += num3;
						}
						num += num3;
					}
					else
					{
						int num4 = context.StartIndex;
						context.StartIndex = num4 + 1;
					}
				}
				else if (tablixMember.SubMembers != null)
				{
					if (tablixMember.Grouping != null)
					{
						context.CurrentScope = tablixMember.Grouping.Name;
						context.CurrentDataScope = tablixMember;
					}
					int num5 = this.CreateAutomaticSubtotals(context, tablixMember.SubMembers, isColumn);
					if (isColumn)
					{
						tablixMember.ColSpan += num5;
					}
					else
					{
						tablixMember.RowSpan += num5;
					}
					num += num5;
				}
				else
				{
					int num4 = context.StartIndex;
					context.StartIndex = num4 + 1;
				}
			}
			return num;
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x00125EF8 File Offset: 0x001240F8
		private bool AllSiblingsHaveConditionalOrToggleableVisibility(TablixMemberList members)
		{
			if (members.Count > 1)
			{
				for (int i = 0; i < members.Count; i++)
				{
					if (!members[i].HasConditionalOrToggleableVisibility)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x00125F30 File Offset: 0x00124130
		internal void ValidateBandStructure(PublishingContextStruct context)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			this.SetIgnoredPropertiesForBandingToDefault(context);
			if (this.LayoutDirection)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsInvalidBandInvalidLayoutDirection, Severity.Error, context.ObjectType, context.ObjectName, "Tablix", Array.Empty<string>());
			}
			foreach (object obj in this.TablixColumnMembers)
			{
				((TablixMember)obj).ValidateTablixMemberForBanding(context, out flag);
				if (flag)
				{
					num++;
				}
			}
			foreach (object obj2 in this.TablixRowMembers)
			{
				((TablixMember)obj2).ValidateTablixMemberForBanding(context, out flag);
				if (flag)
				{
					num2++;
				}
			}
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x00126028 File Offset: 0x00124228
		private void SetIgnoredPropertiesForBandingToDefault(PublishingContextStruct context)
		{
			if (this.GroupsBeforeRowHeaders != 0)
			{
				this.GroupsBeforeRowHeaders = 0;
				context.ErrorContext.Register(ProcessingErrorCode.rsBandIgnoredProperties, Severity.Warning, context.ObjectType, context.ObjectName, "GroupsBeforeRowHeaders", Array.Empty<string>());
			}
			if (this.RepeatColumnHeaders)
			{
				this.RepeatColumnHeaders = false;
				context.ErrorContext.Register(ProcessingErrorCode.rsBandIgnoredProperties, Severity.Warning, context.ObjectType, context.ObjectName, "RepeatColumnHeaders", Array.Empty<string>());
			}
			if (this.RepeatRowHeaders)
			{
				this.RepeatRowHeaders = false;
				context.ErrorContext.Register(ProcessingErrorCode.rsBandIgnoredProperties, Severity.Warning, context.ObjectType, context.ObjectName, "FixedColumnHeaders", Array.Empty<string>());
			}
			if (this.FixedColumnHeaders)
			{
				this.FixedColumnHeaders = false;
				context.ErrorContext.Register(ProcessingErrorCode.rsBandIgnoredProperties, Severity.Warning, context.ObjectType, context.ObjectName, "FixedColumnHeaders", Array.Empty<string>());
			}
			if (this.FixedRowHeaders)
			{
				this.FixedRowHeaders = false;
				context.ErrorContext.Register(ProcessingErrorCode.rsBandIgnoredProperties, Severity.Warning, context.ObjectType, context.ObjectName, "FixedRowHeaders", Array.Empty<string>());
			}
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x00126158 File Offset: 0x00124358
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Tablix, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegion, new List<MemberInfo>
			{
				new MemberInfo(MemberName.TablixColumnMembers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixMember),
				new MemberInfo(MemberName.TablixRowMembers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixMember),
				new MemberInfo(MemberName.TablixRows, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixRow),
				new MemberInfo(MemberName.TablixColumns, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixColumn),
				new MemberInfo(MemberName.TablixCornerCells, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixCornerCell),
				new MemberInfo(MemberName.PropagatedPageBreakLocation, Token.Enum),
				new MemberInfo(MemberName.InnerRowLevelWithPageBreak, Token.Int32),
				new MemberInfo(MemberName.GroupsBeforeRowHeaders, Token.Int32),
				new MemberInfo(MemberName.LayoutDirection, Token.Boolean),
				new MemberInfo(MemberName.RepeatColumnHeaders, Token.Boolean),
				new MemberInfo(MemberName.RepeatRowHeaders, Token.Boolean),
				new MemberInfo(MemberName.FixedColumnHeaders, Token.Boolean),
				new MemberInfo(MemberName.FixedRowHeaders, Token.Boolean),
				new MemberInfo(MemberName.OmitBorderOnPageBreak, Token.Boolean),
				new MemberInfo(MemberName.HideStaticsIfNoRows, Token.Boolean),
				new MemberInfo(MemberName.InScopeTextBoxes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TextBox),
				new MemberInfo(MemberName.ColumnHeaderRowCount, Token.Int32),
				new MemberInfo(MemberName.RowHeaderColumnCount, Token.Int32),
				new MemberInfo(MemberName.KeepTogether, Token.Boolean),
				new MemberInfo(MemberName.CanScroll, Token.Boolean),
				new MemberInfo(MemberName.BandLayout, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObject),
				new MemberInfo(MemberName.TopMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.BottomMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LeftMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.RightMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.EnableRowDrilldown, Token.Boolean, Lifetime.AddedIn(200)),
				new MemberInfo(MemberName.EnableColumnDrilldown, Token.Boolean, Lifetime.AddedIn(200))
			});
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x001263CC File Offset: 0x001245CC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.FixedRowHeaders)
				{
					if (memberName <= MemberName.InnerRowLevelWithPageBreak)
					{
						switch (memberName)
						{
						case MemberName.LeftMargin:
							writer.Write(this.m_leftMargin);
							continue;
						case MemberName.LeftMarginValue:
						case MemberName.RightMarginValue:
						case MemberName.TopMarginValue:
							break;
						case MemberName.RightMargin:
							writer.Write(this.m_rightMargin);
							continue;
						case MemberName.TopMargin:
							writer.Write(this.m_topMargin);
							continue;
						case MemberName.BottomMargin:
							writer.Write(this.m_bottomMargin);
							continue;
						default:
							if (memberName == MemberName.KeepTogether)
							{
								writer.Write(this.m_keepTogether);
								continue;
							}
							if (memberName == MemberName.InnerRowLevelWithPageBreak)
							{
								writer.Write(this.m_innerRowLevelWithPageBreak);
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.GroupsBeforeRowHeaders)
						{
							writer.Write(this.m_groupsBeforeRowHeaders);
							continue;
						}
						if (memberName == MemberName.LayoutDirection)
						{
							writer.Write(this.m_layoutDirection);
							continue;
						}
						switch (memberName)
						{
						case MemberName.PropagatedPageBreakLocation:
							writer.WriteEnum((int)this.m_propagatedPageBreakLocation);
							continue;
						case MemberName.TablixRows:
							writer.Write(this.m_tablixRows);
							continue;
						case MemberName.TablixColumns:
							writer.Write<TablixColumn>(this.m_tablixColumns);
							continue;
						case MemberName.TablixColumnMembers:
							writer.Write(this.m_tablixColumnMembers);
							continue;
						case MemberName.TablixRowMembers:
							writer.Write(this.m_tablixRowMembers);
							continue;
						case MemberName.TablixCornerCells:
							writer.Write<TablixCornerCell>(this.m_corner);
							continue;
						case MemberName.RepeatColumnHeaders:
							writer.Write(this.m_repeatColumnHeaders);
							continue;
						case MemberName.RepeatRowHeaders:
							writer.Write(this.m_repeatRowHeaders);
							continue;
						case MemberName.FixedColumnHeaders:
							writer.Write(this.m_fixedColumnHeaders);
							continue;
						case MemberName.FixedRowHeaders:
							writer.Write(this.m_fixedRowHeaders);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.CanScroll)
				{
					if (memberName == MemberName.OmitBorderOnPageBreak)
					{
						writer.Write(this.m_omitBorderOnPageBreak);
						continue;
					}
					switch (memberName)
					{
					case MemberName.HideStaticsIfNoRows:
						writer.Write(this.m_hideStaticsIfNoRows);
						continue;
					case MemberName.InScopeTextBoxes:
						writer.WriteListOfReferences(this.m_inScopeTextBoxes);
						continue;
					case MemberName.ColumnHeaderRowCount:
						writer.Write(this.m_columnHeaderRowCount);
						continue;
					case MemberName.RowHeaderColumnCount:
						writer.Write(this.m_rowHeaderColumnCount);
						continue;
					default:
						if (memberName == MemberName.CanScroll)
						{
							writer.Write(this.m_canScroll);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.BandLayout)
					{
						writer.Write(this.m_bandLayout);
						continue;
					}
					if (memberName == MemberName.EnableRowDrilldown)
					{
						writer.Write(this.m_enableRowDrilldown);
						continue;
					}
					if (memberName == MemberName.EnableColumnDrilldown)
					{
						writer.Write(this.m_enableColumnDrilldown);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x00126710 File Offset: 0x00124910
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.FixedRowHeaders)
				{
					if (memberName <= MemberName.InnerRowLevelWithPageBreak)
					{
						switch (memberName)
						{
						case MemberName.LeftMargin:
							this.m_leftMargin = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.LeftMarginValue:
						case MemberName.RightMarginValue:
						case MemberName.TopMarginValue:
							break;
						case MemberName.RightMargin:
							this.m_rightMargin = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.TopMargin:
							this.m_topMargin = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.BottomMargin:
							this.m_bottomMargin = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						default:
							if (memberName == MemberName.KeepTogether)
							{
								this.m_keepTogether = reader.ReadBoolean();
								continue;
							}
							if (memberName == MemberName.InnerRowLevelWithPageBreak)
							{
								this.m_innerRowLevelWithPageBreak = reader.ReadInt32();
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.GroupsBeforeRowHeaders)
						{
							this.m_groupsBeforeRowHeaders = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.LayoutDirection)
						{
							this.m_layoutDirection = reader.ReadBoolean();
							continue;
						}
						switch (memberName)
						{
						case MemberName.PropagatedPageBreakLocation:
							this.m_propagatedPageBreakLocation = (PageBreakLocation)reader.ReadEnum();
							continue;
						case MemberName.TablixRows:
							this.m_tablixRows = reader.ReadListOfRIFObjects<TablixRowList>();
							continue;
						case MemberName.TablixColumns:
							this.m_tablixColumns = reader.ReadGenericListOfRIFObjects<TablixColumn>();
							continue;
						case MemberName.TablixColumnMembers:
							this.m_tablixColumnMembers = reader.ReadListOfRIFObjects<TablixMemberList>();
							continue;
						case MemberName.TablixRowMembers:
							this.m_tablixRowMembers = reader.ReadListOfRIFObjects<TablixMemberList>();
							continue;
						case MemberName.TablixCornerCells:
							this.m_corner = reader.ReadListOfListsOfRIFObjects<TablixCornerCell>();
							continue;
						case MemberName.RepeatColumnHeaders:
							this.m_repeatColumnHeaders = reader.ReadBoolean();
							continue;
						case MemberName.RepeatRowHeaders:
							this.m_repeatRowHeaders = reader.ReadBoolean();
							continue;
						case MemberName.FixedColumnHeaders:
							this.m_fixedColumnHeaders = reader.ReadBoolean();
							continue;
						case MemberName.FixedRowHeaders:
							this.m_fixedRowHeaders = reader.ReadBoolean();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.CanScroll)
				{
					if (memberName == MemberName.OmitBorderOnPageBreak)
					{
						this.m_omitBorderOnPageBreak = reader.ReadBoolean();
						continue;
					}
					switch (memberName)
					{
					case MemberName.HideStaticsIfNoRows:
						this.m_hideStaticsIfNoRows = reader.ReadBoolean();
						continue;
					case MemberName.InScopeTextBoxes:
						this.m_inScopeTextBoxes = reader.ReadGenericListOfReferences<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox>(this);
						continue;
					case MemberName.ColumnHeaderRowCount:
						this.m_columnHeaderRowCount = reader.ReadInt32();
						continue;
					case MemberName.RowHeaderColumnCount:
						this.m_rowHeaderColumnCount = reader.ReadInt32();
						continue;
					default:
						if (memberName == MemberName.CanScroll)
						{
							this.m_canScroll = reader.ReadBoolean();
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.BandLayout)
					{
						this.m_bandLayout = reader.ReadRIFObject<BandLayoutOptions>();
						continue;
					}
					if (memberName == MemberName.EnableRowDrilldown)
					{
						this.m_enableRowDrilldown = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.EnableColumnDrilldown)
					{
						this.m_enableColumnDrilldown = reader.ReadBoolean();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
			if (reader.IntermediateFormatVersion.CompareTo(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatVersion.RTM2008) < 0)
			{
				this.FixIndexInCollections();
			}
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x00126A84 File Offset: 0x00124C84
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.InScopeTextBoxes)
					{
						if (this.m_inScopeTextBoxes == null)
						{
							this.m_inScopeTextBoxes = new List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox>();
						}
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						Global.Tracer.Assert(referenceableItems[memberReference.RefID] is Microsoft.ReportingServices.ReportIntermediateFormat.TextBox);
						this.m_inScopeTextBoxes.Add((Microsoft.ReportingServices.ReportIntermediateFormat.TextBox)referenceableItems[memberReference.RefID]);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x00126B70 File Offset: 0x00124D70
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Tablix;
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x00126B78 File Offset: 0x00124D78
		private void FixIndexInCollections()
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.IndexInCollectionUpgrader indexInCollectionUpgrader = new Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.IndexInCollectionUpgrader();
			if (this.m_tablixRowMembers != null && this.m_tablixColumnMembers != null)
			{
				int num = 0;
				int num2 = 0;
				foreach (object obj in this.m_tablixRowMembers)
				{
					TablixMember tablixMember = (TablixMember)obj;
					this.FixIndexInCollection(tablixMember, indexInCollectionUpgrader, false, ref num, ref num2);
				}
			}
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x00126BF8 File Offset: 0x00124DF8
		private void FixIndexInCollection(TablixMember member, Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.IndexInCollectionUpgrader indexUpgrader, bool isColumn, ref int rowIndex, ref int colIndex)
		{
			if (!member.IsStatic)
			{
				indexUpgrader.RegisterGroup(member.Grouping.Name);
			}
			if (member.SubMembers != null && member.SubMembers.Count > 0)
			{
				using (IEnumerator enumerator = member.SubMembers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						TablixMember tablixMember = (TablixMember)obj;
						this.FixIndexInCollection(tablixMember, indexUpgrader, isColumn, ref rowIndex, ref colIndex);
					}
					goto IL_00F8;
				}
			}
			if (!isColumn)
			{
				colIndex = 0;
				foreach (object obj2 in this.m_tablixColumnMembers)
				{
					TablixMember tablixMember2 = (TablixMember)obj2;
					this.FixIndexInCollection(tablixMember2, indexUpgrader, true, ref rowIndex, ref colIndex);
				}
				rowIndex++;
			}
			else
			{
				TablixCell tablixCell = this.m_tablixRows[rowIndex].TablixCells[colIndex];
				if (tablixCell != null)
				{
					indexUpgrader.SetIndexInCollection(tablixCell);
				}
				colIndex++;
			}
			IL_00F8:
			if (!member.IsStatic)
			{
				indexUpgrader.UnregisterGroup(member.Grouping.Name);
			}
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x00126D34 File Offset: 0x00124F34
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
				this.m_tablixExprHost = reportExprHost.TablixHostsRemotable[base.ExprHostID];
				base.DataRegionSetExprHost(this.m_tablixExprHost, this.m_tablixExprHost.SortHost, this.m_tablixExprHost.FilterHostsRemotable, this.m_tablixExprHost.UserSortExpressionsHost, this.m_tablixExprHost.PageBreakExprHost, this.m_tablixExprHost.JoinConditionExprHostsRemotable, reportObjectModel);
			}
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x00126DC0 File Offset: 0x00124FC0
		internal override void DataRegionContentsSetExprHost(ObjectModelImpl reportObjectModel, bool traverseDataRegions)
		{
			if (this.m_corner != null)
			{
				for (int i = 0; i < this.m_corner.Count; i++)
				{
					List<TablixCornerCell> list = this.m_corner[i];
					for (int j = 0; j < list.Count; j++)
					{
						TablixCornerCell tablixCornerCell = list[j];
						if (tablixCornerCell != null && tablixCornerCell.CellContents != null)
						{
							reportObjectModel.OdpContext.RuntimeInitializeReportItemObjs(tablixCornerCell.CellContents, traverseDataRegions);
							if (tablixCornerCell.AltCellContents != null)
							{
								reportObjectModel.OdpContext.RuntimeInitializeReportItemObjs(tablixCornerCell.AltCellContents, traverseDataRegions);
							}
						}
					}
				}
			}
			if (this.m_tablixRows != null)
			{
				IList<TablixCellExprHost> list2 = ((this.m_tablixExprHost != null) ? this.m_tablixExprHost.CellHostsRemotable : null);
				for (int k = 0; k < this.m_tablixRows.Count; k++)
				{
					TablixRow tablixRow = this.m_tablixRows[k];
					Global.Tracer.Assert(tablixRow != null && tablixRow.Cells != null, "(null != row && null != row.Cells)");
					for (int l = 0; l < tablixRow.TablixCells.Count; l++)
					{
						TablixCell tablixCell = tablixRow.TablixCells[l];
						Global.Tracer.Assert(tablixCell != null, "(null != cell)");
						if (list2 != null && tablixCell.ExpressionHostID >= 0)
						{
							tablixCell.SetExprHost(list2[tablixCell.ExpressionHostID], reportObjectModel);
						}
						if (tablixCell.CellContents != null)
						{
							reportObjectModel.OdpContext.RuntimeInitializeReportItemObjs(tablixCell.CellContents, traverseDataRegions);
							if (tablixCell.AltCellContents != null)
							{
								reportObjectModel.OdpContext.RuntimeInitializeReportItemObjs(tablixCell.AltCellContents, traverseDataRegions);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x00126F5B File Offset: 0x0012515B
		internal override object EvaluateNoRowsMessageExpression()
		{
			return this.m_tablixExprHost.NoRowsExpr;
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x00126F68 File Offset: 0x00125168
		internal string EvaluateTablixMargin(IReportScopeInstance reportScopeInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.MarginPosition marginPosition, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return context.ReportRuntime.EvaluateTablixMarginExpression(this, marginPosition);
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x00126F7F File Offset: 0x0012517F
		protected override void AddInScopeTextBox(Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textbox)
		{
			if (this.m_inScopeTextBoxes == null)
			{
				this.m_inScopeTextBoxes = new List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox>();
			}
			this.m_inScopeTextBoxes.Add(textbox);
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x00126FA0 File Offset: 0x001251A0
		internal override void ResetTextBoxImpls(OnDemandProcessingContext context)
		{
			if (this.m_inScopeTextBoxes != null)
			{
				for (int i = 0; i < this.m_inScopeTextBoxes.Count; i++)
				{
					this.m_inScopeTextBoxes[i].ResetTextBoxImpl(context);
				}
			}
		}

		// Token: 0x04001F66 RID: 8038
		private bool m_canScroll;

		// Token: 0x04001F67 RID: 8039
		private bool m_keepTogether;

		// Token: 0x04001F68 RID: 8040
		private TablixMemberList m_tablixColumnMembers;

		// Token: 0x04001F69 RID: 8041
		private TablixMemberList m_tablixRowMembers;

		// Token: 0x04001F6A RID: 8042
		private TablixRowList m_tablixRows;

		// Token: 0x04001F6B RID: 8043
		private List<TablixColumn> m_tablixColumns;

		// Token: 0x04001F6C RID: 8044
		private List<List<TablixCornerCell>> m_corner;

		// Token: 0x04001F6D RID: 8045
		private PageBreakLocation m_propagatedPageBreakLocation;

		// Token: 0x04001F6E RID: 8046
		private int m_innerRowLevelWithPageBreak = -1;

		// Token: 0x04001F6F RID: 8047
		private int m_groupsBeforeRowHeaders;

		// Token: 0x04001F70 RID: 8048
		private bool m_layoutDirection;

		// Token: 0x04001F71 RID: 8049
		private bool m_repeatColumnHeaders;

		// Token: 0x04001F72 RID: 8050
		private bool m_repeatRowHeaders;

		// Token: 0x04001F73 RID: 8051
		private bool m_fixedColumnHeaders;

		// Token: 0x04001F74 RID: 8052
		private bool m_fixedRowHeaders;

		// Token: 0x04001F75 RID: 8053
		private bool m_omitBorderOnPageBreak;

		// Token: 0x04001F76 RID: 8054
		private bool m_hideStaticsIfNoRows = true;

		// Token: 0x04001F77 RID: 8055
		[Reference]
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox> m_inScopeTextBoxes;

		// Token: 0x04001F78 RID: 8056
		private int m_columnHeaderRowCount;

		// Token: 0x04001F79 RID: 8057
		private int m_rowHeaderColumnCount;

		// Token: 0x04001F7A RID: 8058
		private BandLayoutOptions m_bandLayout;

		// Token: 0x04001F7B RID: 8059
		private ExpressionInfo m_topMargin;

		// Token: 0x04001F7C RID: 8060
		private ExpressionInfo m_bottomMargin;

		// Token: 0x04001F7D RID: 8061
		private ExpressionInfo m_leftMargin;

		// Token: 0x04001F7E RID: 8062
		private ExpressionInfo m_rightMargin;

		// Token: 0x04001F7F RID: 8063
		private bool m_enableRowDrilldown;

		// Token: 0x04001F80 RID: 8064
		private bool m_enableColumnDrilldown;

		// Token: 0x04001F81 RID: 8065
		[NonSerialized]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.InitData m_initData;

		// Token: 0x04001F82 RID: 8066
		[NonSerialized]
		private bool m_createdSubtotals;

		// Token: 0x04001F83 RID: 8067
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.GetDeclaration();

		// Token: 0x04001F84 RID: 8068
		[NonSerialized]
		private bool m_computeHeight;

		// Token: 0x04001F85 RID: 8069
		[NonSerialized]
		private bool m_computeWidth;

		// Token: 0x04001F86 RID: 8070
		[NonSerialized]
		private TablixExprHost m_tablixExprHost;

		// Token: 0x02000984 RID: 2436
		internal enum MarginPosition
		{
			// Token: 0x0400414F RID: 16719
			TopMargin,
			// Token: 0x04004150 RID: 16720
			BottomMargin,
			// Token: 0x04004151 RID: 16721
			LeftMargin,
			// Token: 0x04004152 RID: 16722
			RightMargin
		}

		// Token: 0x02000985 RID: 2437
		internal class InitData
		{
			// Token: 0x04004153 RID: 16723
			internal bool HasFixedColData;

			// Token: 0x04004154 RID: 16724
			internal int FixedColStartIndex;

			// Token: 0x04004155 RID: 16725
			internal int FixedColLength;

			// Token: 0x04004156 RID: 16726
			internal bool HasFixedRowData;

			// Token: 0x04004157 RID: 16727
			internal bool IsTopLevelDataRegion;

			// Token: 0x04004158 RID: 16728
			internal IList<Pair<double, int>> RowHeaderLevelSizeList;

			// Token: 0x04004159 RID: 16729
			internal IList<Pair<double, int>> ColumnHeaderLevelSizeList;
		}

		// Token: 0x02000986 RID: 2438
		private class SizeCalculator
		{
			// Token: 0x06008084 RID: 32900 RVA: 0x00211350 File Offset: 0x0020F550
			internal SizeCalculator(Microsoft.ReportingServices.ReportIntermediateFormat.Tablix tablix)
			{
				this.m_tablix = tablix;
			}

			// Token: 0x06008085 RID: 32901 RVA: 0x00211360 File Offset: 0x0020F560
			internal void CalculateSizes(InitializationContext context)
			{
				if (this.m_tablix.Corner != null)
				{
					this.CalculateCornerSizes(context);
				}
				if (this.m_tablix.TablixRowMembers != null)
				{
					this.CalculateMemberSizes(context, this.m_tablix.TablixRowMembers, false, 0);
				}
				if (this.m_tablix.TablixColumnMembers != null)
				{
					this.CalculateMemberSizes(context, this.m_tablix.TablixColumnMembers, true, 0);
				}
				if (this.m_tablix.TablixRows != null)
				{
					this.CalculateCellSizes(context);
				}
			}

			// Token: 0x06008086 RID: 32902 RVA: 0x002113D8 File Offset: 0x0020F5D8
			private void CalculateCellSizes(InitializationContext context)
			{
				for (int i = 0; i < this.m_tablix.TablixRows.Count; i++)
				{
					TablixCellList tablixCells = this.m_tablix.TablixRows[i].TablixCells;
					for (int j = 0; j < tablixCells.Count; j++)
					{
						TablixCell tablixCell = tablixCells[j];
						if (tablixCell.CellContents != null)
						{
							double num = 0.0;
							double heightValue = this.m_tablix.TablixRows[i].HeightValue;
							for (int k = j; k < tablixCell.ColSpan + j; k++)
							{
								num += this.m_tablix.TablixColumns[k].WidthValue;
							}
							tablixCell.CellContents.CalculateSizes(num, heightValue, context, true);
							if (tablixCell.AltCellContents != null)
							{
								tablixCell.AltCellContents.CalculateSizes(num, heightValue, context, true);
							}
						}
					}
				}
			}

			// Token: 0x06008087 RID: 32903 RVA: 0x002114C8 File Offset: 0x0020F6C8
			private void CalculateMemberSizes(InitializationContext context, TablixMemberList members, bool isColumn, int index)
			{
				int num = index;
				for (int i = 0; i < members.Count; i++)
				{
					double num2 = 0.0;
					double num3 = 0.0;
					double num4 = 0.0;
					double num5 = 0.0;
					TablixMember tablixMember = members[i];
					if (tablixMember.TablixHeader != null && tablixMember.TablixHeader.CellContents != null)
					{
						if (isColumn)
						{
							num2 = tablixMember.TablixHeader.SizeValue;
							num5 = num2;
							for (int j = num; j < tablixMember.ColSpan + num; j++)
							{
								TablixColumn tablixColumn = this.m_tablix.TablixColumns[j];
								if (!tablixColumn.ForAutoSubtotal)
								{
									num4 += tablixColumn.WidthValue;
								}
								num3 += tablixColumn.WidthValue;
							}
						}
						else
						{
							num3 = tablixMember.TablixHeader.SizeValue;
							num4 += num3;
							for (int k = num; k < tablixMember.RowSpan + num; k++)
							{
								TablixRow tablixRow = this.m_tablix.TablixRows[k];
								if (!tablixRow.ForAutoSubtotal)
								{
									num5 += tablixRow.HeightValue;
								}
								num2 += tablixRow.HeightValue;
							}
						}
						Microsoft.ReportingServices.ReportProcessing.ObjectType objectType = tablixMember.TablixHeader.CellContents.ObjectType;
						if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart || objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel || objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Map)
						{
							tablixMember.TablixHeader.CellContents.CalculateSizes(num4, num5, context, true);
						}
						else
						{
							tablixMember.TablixHeader.CellContents.CalculateSizes(num3, num2, context, true);
						}
						if (tablixMember.TablixHeader.AltCellContents != null)
						{
							objectType = tablixMember.TablixHeader.AltCellContents.ObjectType;
							if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart || objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel || objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Map)
							{
								tablixMember.TablixHeader.AltCellContents.CalculateSizes(num4, num5, context, true);
							}
							else
							{
								tablixMember.TablixHeader.AltCellContents.CalculateSizes(num3, num2, context, true);
							}
						}
					}
					if (tablixMember.SubMembers != null)
					{
						this.CalculateMemberSizes(context, tablixMember.SubMembers, isColumn, num);
					}
					if (isColumn)
					{
						num += tablixMember.ColSpan;
					}
					else
					{
						num += tablixMember.RowSpan;
					}
				}
			}

			// Token: 0x06008088 RID: 32904 RVA: 0x002116E4 File Offset: 0x0020F8E4
			private void CalculateCornerSizes(InitializationContext context)
			{
				for (int i = 0; i < this.m_tablix.Corner.Count; i++)
				{
					List<TablixCornerCell> list = this.m_tablix.Corner[i];
					for (int j = 0; j < list.Count; j++)
					{
						TablixCornerCell tablixCornerCell = list[j];
						if (tablixCornerCell.CellContents != null)
						{
							double headerSize = context.GetHeaderSize(this.m_tablix.InitializationData.ColumnHeaderLevelSizeList, i, tablixCornerCell.RowSpan);
							double headerSize2 = context.GetHeaderSize(this.m_tablix.InitializationData.RowHeaderLevelSizeList, j, tablixCornerCell.ColSpan);
							tablixCornerCell.CellContents.CalculateSizes(headerSize2, headerSize, context, true);
							if (tablixCornerCell.AltCellContents != null)
							{
								tablixCornerCell.AltCellContents.CalculateSizes(headerSize2, headerSize, context, true);
							}
						}
					}
				}
			}

			// Token: 0x0400415A RID: 16730
			private Microsoft.ReportingServices.ReportIntermediateFormat.Tablix m_tablix;
		}

		// Token: 0x02000987 RID: 2439
		private sealed class IndexInCollectionUpgrader
		{
			// Token: 0x06008089 RID: 32905 RVA: 0x002117D0 File Offset: 0x0020F9D0
			internal void RegisterGroup(string groupName)
			{
				this.m_groupingScopes.Add(groupName, null);
			}

			// Token: 0x0600808A RID: 32906 RVA: 0x002117DF File Offset: 0x0020F9DF
			internal void UnregisterGroup(string groupName)
			{
				this.m_groupingScopes.Remove(groupName);
			}

			// Token: 0x0600808B RID: 32907 RVA: 0x002117F0 File Offset: 0x0020F9F0
			internal void SetIndexInCollection(TablixCell indexedInCollection)
			{
				Hashtable hashtable = (Hashtable)this.m_groupingScopes.Clone();
				int num;
				if (this.m_indexInCollectionTable.TryGetValue(hashtable, out num))
				{
					num++;
					this.m_indexInCollectionTable[hashtable] = num;
				}
				else
				{
					num = 0;
					this.m_indexInCollectionTable.Add(hashtable, num);
				}
				indexedInCollection.IndexInCollection = num;
			}

			// Token: 0x0400415B RID: 16731
			private Dictionary<Hashtable, int> m_indexInCollectionTable = new Dictionary<Hashtable, int>(InitializationContext.HashtableKeyComparer.Instance);

			// Token: 0x0400415C RID: 16732
			private Hashtable m_groupingScopes = new Hashtable();
		}
	}
}

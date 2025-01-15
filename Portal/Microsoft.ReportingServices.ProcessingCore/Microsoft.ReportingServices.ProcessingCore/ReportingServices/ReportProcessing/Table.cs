using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006F6 RID: 1782
	[Serializable]
	internal sealed class Table : DataRegion, IPageBreakItem, IRunningValueHolder
	{
		// Token: 0x060062B7 RID: 25271 RVA: 0x001893C6 File Offset: 0x001875C6
		internal Table(Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x060062B8 RID: 25272 RVA: 0x001893D6 File Offset: 0x001875D6
		internal Table(int id, Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(id, parent)
		{
			this.m_runningValues = new RunningValueInfoList();
		}

		// Token: 0x170022E3 RID: 8931
		// (get) Token: 0x060062B9 RID: 25273 RVA: 0x001893F2 File Offset: 0x001875F2
		internal override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Table;
			}
		}

		// Token: 0x170022E4 RID: 8932
		// (get) Token: 0x060062BA RID: 25274 RVA: 0x001893F6 File Offset: 0x001875F6
		// (set) Token: 0x060062BB RID: 25275 RVA: 0x001893FE File Offset: 0x001875FE
		internal TableColumnList TableColumns
		{
			get
			{
				return this.m_tableColumns;
			}
			set
			{
				this.m_tableColumns = value;
			}
		}

		// Token: 0x170022E5 RID: 8933
		// (get) Token: 0x060062BC RID: 25276 RVA: 0x00189407 File Offset: 0x00187607
		// (set) Token: 0x060062BD RID: 25277 RVA: 0x0018940F File Offset: 0x0018760F
		internal TableRowList HeaderRows
		{
			get
			{
				return this.m_headerRows;
			}
			set
			{
				this.m_headerRows = value;
			}
		}

		// Token: 0x170022E6 RID: 8934
		// (get) Token: 0x060062BE RID: 25278 RVA: 0x00189418 File Offset: 0x00187618
		// (set) Token: 0x060062BF RID: 25279 RVA: 0x00189420 File Offset: 0x00187620
		internal bool HeaderRepeatOnNewPage
		{
			get
			{
				return this.m_headerRepeatOnNewPage;
			}
			set
			{
				this.m_headerRepeatOnNewPage = value;
			}
		}

		// Token: 0x170022E7 RID: 8935
		// (get) Token: 0x060062C0 RID: 25280 RVA: 0x00189429 File Offset: 0x00187629
		// (set) Token: 0x060062C1 RID: 25281 RVA: 0x00189431 File Offset: 0x00187631
		internal TableGroup TableGroups
		{
			get
			{
				return this.m_tableGroups;
			}
			set
			{
				this.m_tableGroups = value;
			}
		}

		// Token: 0x170022E8 RID: 8936
		// (get) Token: 0x060062C2 RID: 25282 RVA: 0x0018943A File Offset: 0x0018763A
		// (set) Token: 0x060062C3 RID: 25283 RVA: 0x00189442 File Offset: 0x00187642
		internal TableDetail TableDetail
		{
			get
			{
				return this.m_tableDetail;
			}
			set
			{
				this.m_tableDetail = value;
			}
		}

		// Token: 0x170022E9 RID: 8937
		// (get) Token: 0x060062C4 RID: 25284 RVA: 0x0018944B File Offset: 0x0018764B
		// (set) Token: 0x060062C5 RID: 25285 RVA: 0x00189453 File Offset: 0x00187653
		internal TableGroup DetailGroup
		{
			get
			{
				return this.m_detailGroup;
			}
			set
			{
				this.m_detailGroup = value;
			}
		}

		// Token: 0x170022EA RID: 8938
		// (get) Token: 0x060062C6 RID: 25286 RVA: 0x0018945C File Offset: 0x0018765C
		// (set) Token: 0x060062C7 RID: 25287 RVA: 0x00189464 File Offset: 0x00187664
		internal TableRowList FooterRows
		{
			get
			{
				return this.m_footerRows;
			}
			set
			{
				this.m_footerRows = value;
			}
		}

		// Token: 0x170022EB RID: 8939
		// (get) Token: 0x060062C8 RID: 25288 RVA: 0x0018946D File Offset: 0x0018766D
		// (set) Token: 0x060062C9 RID: 25289 RVA: 0x00189475 File Offset: 0x00187675
		internal bool FooterRepeatOnNewPage
		{
			get
			{
				return this.m_footerRepeatOnNewPage;
			}
			set
			{
				this.m_footerRepeatOnNewPage = value;
			}
		}

		// Token: 0x170022EC RID: 8940
		// (get) Token: 0x060062CA RID: 25290 RVA: 0x0018947E File Offset: 0x0018767E
		// (set) Token: 0x060062CB RID: 25291 RVA: 0x00189486 File Offset: 0x00187686
		internal bool PropagatedPageBreakAtStart
		{
			get
			{
				return this.m_propagatedPageBreakAtStart;
			}
			set
			{
				this.m_propagatedPageBreakAtStart = value;
			}
		}

		// Token: 0x170022ED RID: 8941
		// (get) Token: 0x060062CC RID: 25292 RVA: 0x0018948F File Offset: 0x0018768F
		// (set) Token: 0x060062CD RID: 25293 RVA: 0x00189497 File Offset: 0x00187697
		internal bool GroupBreakAtStart
		{
			get
			{
				return this.m_groupPageBreakAtStart;
			}
			set
			{
				this.m_groupPageBreakAtStart = value;
			}
		}

		// Token: 0x170022EE RID: 8942
		// (get) Token: 0x060062CE RID: 25294 RVA: 0x001894A0 File Offset: 0x001876A0
		// (set) Token: 0x060062CF RID: 25295 RVA: 0x001894A8 File Offset: 0x001876A8
		internal bool PropagatedPageBreakAtEnd
		{
			get
			{
				return this.m_propagatedPageBreakAtEnd;
			}
			set
			{
				this.m_propagatedPageBreakAtEnd = value;
			}
		}

		// Token: 0x170022EF RID: 8943
		// (get) Token: 0x060062D0 RID: 25296 RVA: 0x001894B1 File Offset: 0x001876B1
		// (set) Token: 0x060062D1 RID: 25297 RVA: 0x001894B9 File Offset: 0x001876B9
		internal bool GroupBreakAtEnd
		{
			get
			{
				return this.m_groupPageBreakAtEnd;
			}
			set
			{
				this.m_groupPageBreakAtEnd = value;
			}
		}

		// Token: 0x170022F0 RID: 8944
		// (get) Token: 0x060062D2 RID: 25298 RVA: 0x001894C2 File Offset: 0x001876C2
		// (set) Token: 0x060062D3 RID: 25299 RVA: 0x001894CA File Offset: 0x001876CA
		internal bool FillPage
		{
			get
			{
				return this.m_fillPage;
			}
			set
			{
				this.m_fillPage = value;
			}
		}

		// Token: 0x170022F1 RID: 8945
		// (get) Token: 0x060062D4 RID: 25300 RVA: 0x001894D3 File Offset: 0x001876D3
		// (set) Token: 0x060062D5 RID: 25301 RVA: 0x001894DB File Offset: 0x001876DB
		internal bool UseOWC
		{
			get
			{
				return this.m_useOWC;
			}
			set
			{
				this.m_useOWC = value;
			}
		}

		// Token: 0x170022F2 RID: 8946
		// (get) Token: 0x060062D6 RID: 25302 RVA: 0x001894E4 File Offset: 0x001876E4
		// (set) Token: 0x060062D7 RID: 25303 RVA: 0x001894EC File Offset: 0x001876EC
		internal bool OWCNonSharedStyles
		{
			get
			{
				return this.m_owcNonSharedStyles;
			}
			set
			{
				this.m_owcNonSharedStyles = value;
			}
		}

		// Token: 0x170022F3 RID: 8947
		// (get) Token: 0x060062D8 RID: 25304 RVA: 0x001894F5 File Offset: 0x001876F5
		// (set) Token: 0x060062D9 RID: 25305 RVA: 0x001894FD File Offset: 0x001876FD
		internal RunningValueInfoList RunningValues
		{
			get
			{
				return this.m_runningValues;
			}
			set
			{
				this.m_runningValues = value;
			}
		}

		// Token: 0x170022F4 RID: 8948
		// (get) Token: 0x060062DA RID: 25306 RVA: 0x00189506 File Offset: 0x00187706
		// (set) Token: 0x060062DB RID: 25307 RVA: 0x0018950E File Offset: 0x0018770E
		internal string DetailDataElementName
		{
			get
			{
				return this.m_detailDataElementName;
			}
			set
			{
				this.m_detailDataElementName = value;
			}
		}

		// Token: 0x170022F5 RID: 8949
		// (get) Token: 0x060062DC RID: 25308 RVA: 0x00189517 File Offset: 0x00187717
		// (set) Token: 0x060062DD RID: 25309 RVA: 0x0018951F File Offset: 0x0018771F
		internal string DetailDataCollectionName
		{
			get
			{
				return this.m_detailDataCollectionName;
			}
			set
			{
				this.m_detailDataCollectionName = value;
			}
		}

		// Token: 0x170022F6 RID: 8950
		// (get) Token: 0x060062DE RID: 25310 RVA: 0x00189528 File Offset: 0x00187728
		// (set) Token: 0x060062DF RID: 25311 RVA: 0x00189530 File Offset: 0x00187730
		internal DataElementOutputTypes DetailDataElementOutput
		{
			get
			{
				return this.m_detailDataElementOutput;
			}
			set
			{
				this.m_detailDataElementOutput = value;
			}
		}

		// Token: 0x170022F7 RID: 8951
		// (get) Token: 0x060062E0 RID: 25312 RVA: 0x00189539 File Offset: 0x00187739
		internal TableExprHost TableExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170022F8 RID: 8952
		// (get) Token: 0x060062E1 RID: 25313 RVA: 0x00189541 File Offset: 0x00187741
		// (set) Token: 0x060062E2 RID: 25314 RVA: 0x00189549 File Offset: 0x00187749
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

		// Token: 0x170022F9 RID: 8953
		// (get) Token: 0x060062E3 RID: 25315 RVA: 0x00189552 File Offset: 0x00187752
		protected override DataRegionExprHost DataRegionExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170022FA RID: 8954
		// (get) Token: 0x060062E4 RID: 25316 RVA: 0x0018955A File Offset: 0x0018775A
		// (set) Token: 0x060062E5 RID: 25317 RVA: 0x00189562 File Offset: 0x00187762
		internal bool FixedHeader
		{
			get
			{
				return this.m_fixedHeader;
			}
			set
			{
				this.m_fixedHeader = value;
			}
		}

		// Token: 0x170022FB RID: 8955
		// (get) Token: 0x060062E6 RID: 25318 RVA: 0x0018956B File Offset: 0x0018776B
		// (set) Token: 0x060062E7 RID: 25319 RVA: 0x00189573 File Offset: 0x00187773
		internal bool HasFixedColumnHeaders
		{
			get
			{
				return this.m_hasFixedColumnHeaders;
			}
			set
			{
				this.m_hasFixedColumnHeaders = value;
			}
		}

		// Token: 0x170022FC RID: 8956
		// (get) Token: 0x060062E8 RID: 25320 RVA: 0x0018957C File Offset: 0x0018777C
		internal double HeaderHeightValue
		{
			get
			{
				if (this.m_headerRows != null)
				{
					return this.m_headerRows.GetHeightValue();
				}
				return 0.0;
			}
		}

		// Token: 0x170022FD RID: 8957
		// (get) Token: 0x060062E9 RID: 25321 RVA: 0x0018959B File Offset: 0x0018779B
		internal double DetailHeightValue
		{
			get
			{
				if (this.m_tableDetail != null && this.m_tableDetail.DetailRows != null)
				{
					return this.m_tableDetail.DetailRows.GetHeightValue();
				}
				return 0.0;
			}
		}

		// Token: 0x170022FE RID: 8958
		// (get) Token: 0x060062EA RID: 25322 RVA: 0x001895CC File Offset: 0x001877CC
		// (set) Token: 0x060062EB RID: 25323 RVA: 0x001895D4 File Offset: 0x001877D4
		internal bool[] ColumnsStartHidden
		{
			get
			{
				return this.m_columnsStartHidden;
			}
			set
			{
				this.m_columnsStartHidden = value;
			}
		}

		// Token: 0x060062EC RID: 25324 RVA: 0x001895E0 File Offset: 0x001877E0
		internal override bool Initialize(InitializationContext context)
		{
			context.Location |= LocationFlags.InMatrixOrTable;
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			context.RegisterDataRegion(this);
			this.InternalInitialize(context);
			context.UnRegisterDataRegion(this);
			return false;
		}

		// Token: 0x060062ED RID: 25325 RVA: 0x00189634 File Offset: 0x00187834
		private void InternalInitialize(InitializationContext context)
		{
			context.Location = context.Location | LocationFlags.InDataSet | LocationFlags.InDataRegion;
			context.ExprHostBuilder.TableStart(this.m_name);
			base.Initialize(context);
			context.RegisterRunningValues(this.m_runningValues);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, true, false);
			}
			bool[] array;
			this.InitializeTableColumns(context, ref this.m_widthValue, out array);
			this.m_width = Converter.ConvertSize(this.m_widthValue);
			this.InitializeHeaderAndFooter(this.m_tableColumns.Count, context, ref this.m_heightValue, array);
			context.Location &= ~LocationFlags.InMatrixCellTopLevelItem;
			this.InitializeTableGroupsOrDetail(this.m_tableColumns.Count, context, ref this.m_heightValue, array);
			this.m_height = Converter.ConvertSize(this.m_heightValue);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
			context.UnRegisterRunningValues(this.m_runningValues);
			Table.OWCFlagsCalculator.Calculate(this, out this.m_useOWC, out this.m_owcNonSharedStyles);
			if (!context.ErrorContext.HasError)
			{
				Table.TopLevelItemsSizes.Calculate(this, context);
			}
			base.ExprHostID = context.ExprHostBuilder.TableEnd();
		}

		// Token: 0x060062EE RID: 25326 RVA: 0x00189760 File Offset: 0x00187960
		protected override void DataRendererInitialize(InitializationContext context)
		{
			base.DataRendererInitialize(context);
			CLSNameValidator.ValidateDataElementName(ref this.m_detailDataElementName, "Detail", context.ObjectType, context.ObjectName, "DetailDataElementName", context.ErrorContext);
			CLSNameValidator.ValidateDataElementName(ref this.m_detailDataCollectionName, this.m_detailDataElementName + "_Collection", context.ObjectType, context.ObjectName, "DetailDataCollectionName", context.ErrorContext);
		}

		// Token: 0x060062EF RID: 25327 RVA: 0x001897D8 File Offset: 0x001879D8
		internal override void RegisterReceiver(InitializationContext context)
		{
			this.RegisterHeaderAndFooter(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.RegisterReceiver(context, true);
			}
			this.RegisterTableColumnsReceiver(context);
			this.RegisterHeaderAndFooterReceiver(context);
			this.RegisterTableGroupsOrDetailReceiver(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
			this.UnRegisterHeaderAndFooter(context);
		}

		// Token: 0x060062F0 RID: 25328 RVA: 0x00189832 File Offset: 0x00187A32
		internal void RegisterHeaderAndFooter(InitializationContext context)
		{
			if (this.m_headerRows != null)
			{
				this.m_headerRows.Register(context);
			}
			if (this.m_footerRows != null)
			{
				this.m_footerRows.Register(context);
			}
		}

		// Token: 0x060062F1 RID: 25329 RVA: 0x0018985C File Offset: 0x00187A5C
		internal void UnRegisterHeaderAndFooter(InitializationContext context)
		{
			if (this.m_footerRows != null)
			{
				this.m_footerRows.UnRegister(context);
			}
			if (this.m_headerRows != null)
			{
				this.m_headerRows.UnRegister(context);
			}
		}

		// Token: 0x060062F2 RID: 25330 RVA: 0x00189888 File Offset: 0x00187A88
		private void InitializeTableColumns(InitializationContext context, ref double tableWidth, out bool[] tableColumnVisibility)
		{
			context.ExprHostBuilder.TableColumnVisibilityHiddenExpressionsStart();
			tableColumnVisibility = new bool[this.m_tableColumns.Count];
			for (int i = 0; i < this.m_tableColumns.Count; i++)
			{
				this.m_tableColumns[i].Initialize(context);
				tableWidth += this.m_tableColumns[i].WidthValue;
				tableColumnVisibility[i] = this.m_tableColumns[i].Visibility == null || this.m_tableColumns[i].Visibility.Hidden == null || this.m_tableColumns[i].Visibility.Toggle != null || (ExpressionInfo.Types.Constant == this.m_tableColumns[i].Visibility.Hidden.Type && !this.m_tableColumns[i].Visibility.Hidden.BoolValue);
			}
			context.ExprHostBuilder.TableColumnVisibilityHiddenExpressionsEnd();
		}

		// Token: 0x060062F3 RID: 25331 RVA: 0x0018998C File Offset: 0x00187B8C
		private void RegisterTableColumnsReceiver(InitializationContext context)
		{
			for (int i = 0; i < this.m_tableColumns.Count; i++)
			{
				this.m_tableColumns[i].RegisterReceiver(context);
			}
		}

		// Token: 0x060062F4 RID: 25332 RVA: 0x001899C4 File Offset: 0x00187BC4
		private void InitializeHeaderAndFooter(int numberOfColumns, InitializationContext context, ref double tableHeight, bool[] tableColumnVisibility)
		{
			context.ExprHostBuilder.TableRowVisibilityHiddenExpressionsStart();
			if (this.m_headerRows != null)
			{
				for (int i = 0; i < this.m_headerRows.Count; i++)
				{
					Global.Tracer.Assert(this.m_headerRows[i] != null);
					this.m_headerRows[i].Initialize(false, numberOfColumns, context, ref tableHeight, tableColumnVisibility);
				}
			}
			if (this.m_footerRows != null)
			{
				for (int j = 0; j < this.m_footerRows.Count; j++)
				{
					Global.Tracer.Assert(this.m_footerRows[j] != null);
					this.m_footerRows[j].Initialize(false, numberOfColumns, context, ref tableHeight, tableColumnVisibility);
				}
			}
			context.ExprHostBuilder.TableRowVisibilityHiddenExpressionsEnd();
		}

		// Token: 0x060062F5 RID: 25333 RVA: 0x00189A88 File Offset: 0x00187C88
		private void RegisterHeaderAndFooterReceiver(InitializationContext context)
		{
			if (this.m_headerRows != null)
			{
				for (int i = 0; i < this.m_headerRows.Count; i++)
				{
					Global.Tracer.Assert(this.m_headerRows[i] != null);
					this.m_headerRows[i].RegisterReceiver(context);
				}
			}
			if (this.m_footerRows != null)
			{
				for (int j = 0; j < this.m_footerRows.Count; j++)
				{
					Global.Tracer.Assert(this.m_footerRows[j] != null);
					this.m_footerRows[j].RegisterReceiver(context);
				}
			}
		}

		// Token: 0x060062F6 RID: 25334 RVA: 0x00189B28 File Offset: 0x00187D28
		private void InitializeTableGroupsOrDetail(int numberOfColumns, InitializationContext context, ref double tableHeight, bool[] tableColumnVisibility)
		{
			TableGroup tableGroup = this.m_detailGroup;
			if (tableGroup != null && this.m_tableGroups == null)
			{
				this.m_tableGroups = this.m_detailGroup;
				tableGroup = null;
			}
			if (this.m_tableGroups != null)
			{
				this.m_tableGroups.Initialize(numberOfColumns, this.m_tableDetail, tableGroup, context, ref tableHeight, tableColumnVisibility);
				return;
			}
			if (this.m_tableDetail != null)
			{
				this.m_tableDetail.Initialize(numberOfColumns, context, ref tableHeight, tableColumnVisibility);
			}
		}

		// Token: 0x060062F7 RID: 25335 RVA: 0x00189B8D File Offset: 0x00187D8D
		private void RegisterTableGroupsOrDetailReceiver(InitializationContext context)
		{
			if (this.m_tableGroups != null)
			{
				this.m_tableGroups.RegisterReceiver(context, this.m_tableDetail);
				return;
			}
			if (this.m_tableDetail != null)
			{
				this.m_tableDetail.RegisterReceiver(context);
			}
		}

		// Token: 0x060062F8 RID: 25336 RVA: 0x00189BBE File Offset: 0x00187DBE
		RunningValueInfoList IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		// Token: 0x060062F9 RID: 25337 RVA: 0x00189BC6 File Offset: 0x00187DC6
		void IRunningValueHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_runningValues != null);
			if (this.m_runningValues.Count == 0)
			{
				this.m_runningValues = null;
			}
		}

		// Token: 0x060062FA RID: 25338 RVA: 0x00189BF0 File Offset: 0x00187DF0
		internal void CalculatePropagatedFlags()
		{
			bool flag = true;
			if (this.m_tableGroups == null)
			{
				return;
			}
			this.m_tableGroups.CalculatePropagatedFlags(out this.m_groupPageBreakAtStart, out this.m_groupPageBreakAtEnd);
			if (this.m_tableGroups.HeaderRows != null)
			{
				flag = this.m_tableGroups.HeaderRepeatOnNewPage;
			}
			this.m_propagatedPageBreakAtStart = this.m_tableGroups.Grouping.PageBreakAtStart || (this.m_tableGroups.PropagatedPageBreakAtStart && flag);
			flag = true;
			if (this.m_tableGroups.FooterRows != null)
			{
				flag = this.m_tableGroups.FooterRepeatOnNewPage;
			}
			this.m_propagatedPageBreakAtEnd = this.m_tableGroups.Grouping.PageBreakAtEnd || (this.m_tableGroups.PropagatedPageBreakAtEnd && flag);
		}

		// Token: 0x060062FB RID: 25339 RVA: 0x00189CA4 File Offset: 0x00187EA4
		bool IPageBreakItem.IgnorePageBreaks()
		{
			if (this.m_pagebreakState == PageBreakStates.Unknown)
			{
				this.m_pagebreakState = PageBreakStates.CanIgnore;
				if (SharedHiddenState.Never == Visibility.GetSharedHidden(this.m_visibility))
				{
					if (this.m_tableColumns != null)
					{
						int num = 0;
						while (num < this.m_tableColumns.Count && SharedHiddenState.Never != Visibility.GetSharedHidden(this.m_tableColumns[num].Visibility))
						{
							num++;
						}
						if (num < this.m_tableColumns.Count)
						{
							this.m_pagebreakState = PageBreakStates.CannotIgnore;
						}
					}
					if (PageBreakStates.CannotIgnore == this.m_pagebreakState)
					{
						if (this.m_tableGroups == null)
						{
							if (this.m_tableDetail != null)
							{
								if (SharedHiddenState.Never != Visibility.GetSharedHidden(this.m_tableDetail.Visibility))
								{
									this.m_pagebreakState = PageBreakStates.CanIgnore;
								}
								for (int i = 0; i < this.m_tableDetail.DetailRows.Count; i++)
								{
									if (SharedHiddenState.Never != Visibility.GetSharedHidden(this.m_tableDetail.DetailRows[i].Visibility))
									{
										this.m_pagebreakState = PageBreakStates.CanIgnore;
									}
								}
							}
						}
						else if (SharedHiddenState.Never != Visibility.GetSharedHidden(this.m_tableGroups.Visibility))
						{
							this.m_pagebreakState = PageBreakStates.CanIgnore;
						}
					}
				}
			}
			return PageBreakStates.CanIgnore == this.m_pagebreakState;
		}

		// Token: 0x060062FC RID: 25340 RVA: 0x00189DC0 File Offset: 0x00187FC0
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.TableHostsRemotable[base.ExprHostID];
				base.DataRegionSetExprHost(this.m_exprHost, reportObjectModel);
				if (this.m_exprHost.TableColumnVisibilityHiddenExpressions != null)
				{
					this.m_exprHost.TableColumnVisibilityHiddenExpressions.SetReportObjectModel(reportObjectModel);
				}
				if (this.m_exprHost.TableRowVisibilityHiddenExpressions != null)
				{
					this.m_exprHost.TableRowVisibilityHiddenExpressions.SetReportObjectModel(reportObjectModel);
				}
			}
		}

		// Token: 0x060062FD RID: 25341 RVA: 0x00189E4C File Offset: 0x0018804C
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataRegion, new MemberInfoList
			{
				new MemberInfo(MemberName.TableColumns, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.TableColumnList),
				new MemberInfo(MemberName.HeaderRows, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.TableRowList),
				new MemberInfo(MemberName.HeaderRepeatOnNewPage, Token.Boolean),
				new MemberInfo(MemberName.TableGroups, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.TableGroup),
				new MemberInfo(MemberName.TableDetail, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.TableDetail),
				new MemberInfo(MemberName.DetailGroup, Token.Reference, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.TableGroup),
				new MemberInfo(MemberName.FooterRows, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.TableRowList),
				new MemberInfo(MemberName.FooterRepeatOnNewPage, Token.Boolean),
				new MemberInfo(MemberName.PropagatedPageBreakAtStart, Token.Boolean),
				new MemberInfo(MemberName.GroupPageBreakAtStart, Token.Boolean),
				new MemberInfo(MemberName.PropagatedPageBreakAtEnd, Token.Boolean),
				new MemberInfo(MemberName.GroupPageBreakAtEnd, Token.Boolean),
				new MemberInfo(MemberName.FillPage, Token.Boolean),
				new MemberInfo(MemberName.UseOwc, Token.Boolean),
				new MemberInfo(MemberName.OwcNonSharedStyles, Token.Boolean),
				new MemberInfo(MemberName.RunningValues, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.RunningValueInfoList),
				new MemberInfo(MemberName.DetailDataElementName, Token.String),
				new MemberInfo(MemberName.DetailDataCollectionName, Token.String),
				new MemberInfo(MemberName.DetailDataElementOutput, Token.Enum),
				new MemberInfo(MemberName.FixedHeader, Token.Boolean)
			});
		}

		// Token: 0x040031C6 RID: 12742
		private TableColumnList m_tableColumns;

		// Token: 0x040031C7 RID: 12743
		private TableRowList m_headerRows;

		// Token: 0x040031C8 RID: 12744
		private bool m_headerRepeatOnNewPage;

		// Token: 0x040031C9 RID: 12745
		private TableGroup m_tableGroups;

		// Token: 0x040031CA RID: 12746
		private TableDetail m_tableDetail;

		// Token: 0x040031CB RID: 12747
		private TableGroup m_detailGroup;

		// Token: 0x040031CC RID: 12748
		private TableRowList m_footerRows;

		// Token: 0x040031CD RID: 12749
		private bool m_footerRepeatOnNewPage;

		// Token: 0x040031CE RID: 12750
		private bool m_propagatedPageBreakAtStart;

		// Token: 0x040031CF RID: 12751
		private bool m_groupPageBreakAtStart;

		// Token: 0x040031D0 RID: 12752
		private bool m_propagatedPageBreakAtEnd;

		// Token: 0x040031D1 RID: 12753
		private bool m_groupPageBreakAtEnd;

		// Token: 0x040031D2 RID: 12754
		private bool m_fillPage;

		// Token: 0x040031D3 RID: 12755
		private bool m_useOWC;

		// Token: 0x040031D4 RID: 12756
		private bool m_owcNonSharedStyles;

		// Token: 0x040031D5 RID: 12757
		private RunningValueInfoList m_runningValues;

		// Token: 0x040031D6 RID: 12758
		private string m_detailDataElementName;

		// Token: 0x040031D7 RID: 12759
		private string m_detailDataCollectionName;

		// Token: 0x040031D8 RID: 12760
		private DataElementOutputTypes m_detailDataElementOutput;

		// Token: 0x040031D9 RID: 12761
		private bool m_fixedHeader;

		// Token: 0x040031DA RID: 12762
		[NonSerialized]
		private TableExprHost m_exprHost;

		// Token: 0x040031DB RID: 12763
		[NonSerialized]
		private int m_currentPage = -1;

		// Token: 0x040031DC RID: 12764
		[NonSerialized]
		private bool m_hasFixedColumnHeaders;

		// Token: 0x040031DD RID: 12765
		[NonSerialized]
		private bool[] m_columnsStartHidden;

		// Token: 0x02000CC9 RID: 3273
		private sealed class OWCFlagsCalculator
		{
			// Token: 0x06008D0B RID: 36107 RVA: 0x0023DCF6 File Offset: 0x0023BEF6
			private OWCFlagsCalculator()
			{
			}

			// Token: 0x06008D0C RID: 36108 RVA: 0x0023DD08 File Offset: 0x0023BF08
			internal static void Calculate(Table table, out bool useOWC, out bool owcNonSharedStyles)
			{
				Table.OWCFlagsCalculator owcflagsCalculator = new Table.OWCFlagsCalculator();
				owcflagsCalculator.CalculateOWCFlags(table);
				useOWC = owcflagsCalculator.m_useOWC;
				owcNonSharedStyles = owcflagsCalculator.m_owcNonSharedStyles;
			}

			// Token: 0x06008D0D RID: 36109 RVA: 0x0023DD34 File Offset: 0x0023BF34
			private void CalculateOWCFlags(Table table)
			{
				this.CalculateOWCFlags(table.HeaderRows);
				if (this.IsFinish())
				{
					return;
				}
				this.CalculateOWCFlags(table.TableGroups);
				if (this.IsFinish())
				{
					return;
				}
				this.CalculateOWCFlags(table.TableDetail);
				if (this.IsFinish())
				{
					return;
				}
				this.CalculateOWCFlags(table.FooterRows);
			}

			// Token: 0x06008D0E RID: 36110 RVA: 0x0023DD8C File Offset: 0x0023BF8C
			private void CalculateOWCFlags(TableGroup tableGroup)
			{
				if (tableGroup == null)
				{
					return;
				}
				this.CalculateOWCFlags(tableGroup.HeaderRows);
				if (this.IsFinish())
				{
					return;
				}
				this.CalculateOWCFlags(tableGroup.SubGroup);
				if (this.IsFinish())
				{
					return;
				}
				this.CalculateOWCFlags(tableGroup.FooterRows);
			}

			// Token: 0x06008D0F RID: 36111 RVA: 0x0023DDC8 File Offset: 0x0023BFC8
			private void CalculateOWCFlags(TableDetail tableDetail)
			{
				if (tableDetail == null)
				{
					return;
				}
				this.CalculateOWCFlags(tableDetail.DetailRows);
				this.IsFinish();
			}

			// Token: 0x06008D10 RID: 36112 RVA: 0x0023DDE4 File Offset: 0x0023BFE4
			private void CalculateOWCFlags(TableRowList tableRows)
			{
				if (tableRows == null)
				{
					return;
				}
				for (int i = 0; i < tableRows.Count; i++)
				{
					this.CalculateOWCFlags(tableRows[i]);
					if (this.IsFinish())
					{
						return;
					}
				}
			}

			// Token: 0x06008D11 RID: 36113 RVA: 0x0023DE1C File Offset: 0x0023C01C
			private void CalculateOWCFlags(TableRow tableRow)
			{
				if (tableRow == null)
				{
					return;
				}
				if (tableRow.ReportItems == null)
				{
					return;
				}
				for (int i = 0; i < tableRow.ReportItems.Count; i++)
				{
					this.m_numberOfTextBoxAndCheckBox = 0U;
					this.CalculateOWCFlags(tableRow.ReportItems[i]);
					if (this.IsFinish())
					{
						return;
					}
				}
			}

			// Token: 0x06008D12 RID: 36114 RVA: 0x0023DE70 File Offset: 0x0023C070
			private void CalculateOWCFlags(Microsoft.ReportingServices.ReportProcessing.ReportItem item)
			{
				if (item == null)
				{
					return;
				}
				if ((item is TextBox || item is CheckBox || item is Rectangle) && item.Visibility != null)
				{
					this.m_useOWC = false;
					return;
				}
				if (item is TextBox || item is CheckBox)
				{
					this.m_numberOfTextBoxAndCheckBox += 1U;
					if (this.m_numberOfTextBoxAndCheckBox > 1U)
					{
						this.m_useOWC = false;
						return;
					}
					if (item.StyleClass != null && item.StyleClass.ExpressionList != null && 0 < item.StyleClass.ExpressionList.Count)
					{
						this.m_owcNonSharedStyles = true;
					}
				}
				if (item is TextBox && ((TextBox)item).IsToggle)
				{
					this.m_useOWC = false;
					return;
				}
				if (item is Rectangle)
				{
					Rectangle rectangle = (Rectangle)item;
					if (rectangle.ReportItems != null)
					{
						for (int i = 0; i < rectangle.ReportItems.Count; i++)
						{
							this.CalculateOWCFlags(rectangle.ReportItems[i]);
							if (this.IsFinish())
							{
								return;
							}
						}
					}
				}
				if (item is Image || item is SubReport || item is ActiveXControl || item is DataRegion)
				{
					this.m_useOWC = false;
					return;
				}
			}

			// Token: 0x06008D13 RID: 36115 RVA: 0x0023DF94 File Offset: 0x0023C194
			private bool IsFinish()
			{
				return !this.m_useOWC;
			}

			// Token: 0x04004E9F RID: 20127
			private const uint MaxNumberOfTextBoxAndCheckBox = 1U;

			// Token: 0x04004EA0 RID: 20128
			private uint m_numberOfTextBoxAndCheckBox;

			// Token: 0x04004EA1 RID: 20129
			private bool m_useOWC = true;

			// Token: 0x04004EA2 RID: 20130
			private bool m_owcNonSharedStyles;
		}

		// Token: 0x02000CCA RID: 3274
		private sealed class TopLevelItemsSizes
		{
			// Token: 0x06008D14 RID: 36116 RVA: 0x0023DF9F File Offset: 0x0023C19F
			private TopLevelItemsSizes(TableColumnList tableColumns, InitializationContext context)
			{
				this.m_tableColumns = tableColumns;
				this.m_context = context;
			}

			// Token: 0x06008D15 RID: 36117 RVA: 0x0023DFB5 File Offset: 0x0023C1B5
			internal static void Calculate(Table table, InitializationContext context)
			{
				new Table.TopLevelItemsSizes(table.TableColumns, context).CalculateSizes(table);
			}

			// Token: 0x06008D16 RID: 36118 RVA: 0x0023DFC9 File Offset: 0x0023C1C9
			private void CalculateSizes(Table table)
			{
				this.CalculateSizes(table.HeaderRows);
				this.CalculateSizes(table.TableGroups);
				this.CalculateSizes(table.TableDetail);
				this.CalculateSizes(table.FooterRows);
			}

			// Token: 0x06008D17 RID: 36119 RVA: 0x0023DFFB File Offset: 0x0023C1FB
			private void CalculateSizes(TableGroup tableGroup)
			{
				if (tableGroup == null)
				{
					return;
				}
				this.CalculateSizes(tableGroup.HeaderRows);
				this.CalculateSizes(tableGroup.SubGroup);
				this.CalculateSizes(tableGroup.FooterRows);
			}

			// Token: 0x06008D18 RID: 36120 RVA: 0x0023E025 File Offset: 0x0023C225
			private void CalculateSizes(TableDetail tableDetail)
			{
				if (tableDetail == null)
				{
					return;
				}
				this.CalculateSizes(tableDetail.DetailRows);
			}

			// Token: 0x06008D19 RID: 36121 RVA: 0x0023E038 File Offset: 0x0023C238
			private void CalculateSizes(TableRowList tableRows)
			{
				if (tableRows == null)
				{
					return;
				}
				for (int i = 0; i < tableRows.Count; i++)
				{
					this.CalculateSizes(tableRows[i]);
				}
			}

			// Token: 0x06008D1A RID: 36122 RVA: 0x0023E068 File Offset: 0x0023C268
			private void CalculateSizes(TableRow tableRow)
			{
				if (tableRow == null)
				{
					return;
				}
				if (tableRow.ReportItems == null)
				{
					return;
				}
				int num = 0;
				for (int i = 0; i < tableRow.ReportItems.Count; i++)
				{
					double num2 = 0.0;
					for (int j = tableRow.ColSpans[i]; j > 0; j--)
					{
						num2 += this.m_tableColumns[num].WidthValue;
						num++;
					}
					this.CalculateSizes(tableRow.ReportItems[i], num2, tableRow.HeightValue);
				}
			}

			// Token: 0x06008D1B RID: 36123 RVA: 0x0023E0F8 File Offset: 0x0023C2F8
			private void CalculateSizes(Microsoft.ReportingServices.ReportProcessing.ReportItem item, double width, double height)
			{
				if (item == null)
				{
					return;
				}
				item.CalculateSizes(width, height, this.m_context, true);
			}

			// Token: 0x04004EA3 RID: 20131
			private TableColumnList m_tableColumns;

			// Token: 0x04004EA4 RID: 20132
			private InitializationContext m_context;
		}
	}
}

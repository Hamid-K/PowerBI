using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006F1 RID: 1777
	[Serializable]
	internal sealed class Matrix : Pivot, IPageBreakItem
	{
		// Token: 0x06006211 RID: 25105 RVA: 0x001878F2 File Offset: 0x00185AF2
		internal Matrix(Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x06006212 RID: 25106 RVA: 0x00187918 File Offset: 0x00185B18
		internal Matrix(int id, int idForCornerReportItems, int idForCellReportItems, Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(id, parent)
		{
			this.m_cornerReportItems = new ReportItemCollection(idForCornerReportItems, false);
			this.m_cellReportItems = new ReportItemCollection(idForCellReportItems, false);
		}

		// Token: 0x1700229E RID: 8862
		// (get) Token: 0x06006213 RID: 25107 RVA: 0x00187964 File Offset: 0x00185B64
		internal override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Matrix;
			}
		}

		// Token: 0x1700229F RID: 8863
		// (get) Token: 0x06006214 RID: 25108 RVA: 0x00187968 File Offset: 0x00185B68
		// (set) Token: 0x06006215 RID: 25109 RVA: 0x00187970 File Offset: 0x00185B70
		internal ReportItemCollection CornerReportItems
		{
			get
			{
				return this.m_cornerReportItems;
			}
			set
			{
				this.m_cornerReportItems = value;
			}
		}

		// Token: 0x170022A0 RID: 8864
		// (get) Token: 0x06006216 RID: 25110 RVA: 0x00187979 File Offset: 0x00185B79
		internal Microsoft.ReportingServices.ReportProcessing.ReportItem CornerReportItem
		{
			get
			{
				if (this.m_cornerReportItems != null && 0 < this.m_cornerReportItems.Count)
				{
					return this.m_cornerReportItems[0];
				}
				return null;
			}
		}

		// Token: 0x170022A1 RID: 8865
		// (get) Token: 0x06006217 RID: 25111 RVA: 0x0018799F File Offset: 0x00185B9F
		internal override PivotHeading PivotColumns
		{
			get
			{
				return this.m_columns;
			}
		}

		// Token: 0x170022A2 RID: 8866
		// (get) Token: 0x06006218 RID: 25112 RVA: 0x001879A7 File Offset: 0x00185BA7
		internal override PivotHeading PivotRows
		{
			get
			{
				return this.m_rows;
			}
		}

		// Token: 0x170022A3 RID: 8867
		// (get) Token: 0x06006219 RID: 25113 RVA: 0x001879AF File Offset: 0x00185BAF
		// (set) Token: 0x0600621A RID: 25114 RVA: 0x001879B7 File Offset: 0x00185BB7
		internal MatrixHeading Columns
		{
			get
			{
				return this.m_columns;
			}
			set
			{
				this.m_columns = value;
			}
		}

		// Token: 0x170022A4 RID: 8868
		// (get) Token: 0x0600621B RID: 25115 RVA: 0x001879C0 File Offset: 0x00185BC0
		// (set) Token: 0x0600621C RID: 25116 RVA: 0x001879C8 File Offset: 0x00185BC8
		internal MatrixHeading Rows
		{
			get
			{
				return this.m_rows;
			}
			set
			{
				this.m_rows = value;
			}
		}

		// Token: 0x170022A5 RID: 8869
		// (get) Token: 0x0600621D RID: 25117 RVA: 0x001879D1 File Offset: 0x00185BD1
		// (set) Token: 0x0600621E RID: 25118 RVA: 0x001879D9 File Offset: 0x00185BD9
		internal ReportItemCollection CellReportItems
		{
			get
			{
				return this.m_cellReportItems;
			}
			set
			{
				this.m_cellReportItems = value;
			}
		}

		// Token: 0x170022A6 RID: 8870
		// (get) Token: 0x0600621F RID: 25119 RVA: 0x001879E2 File Offset: 0x00185BE2
		internal override RunningValueInfoList PivotCellRunningValues
		{
			get
			{
				return this.m_cellReportItems.RunningValues;
			}
		}

		// Token: 0x170022A7 RID: 8871
		// (get) Token: 0x06006220 RID: 25120 RVA: 0x001879EF File Offset: 0x00185BEF
		// (set) Token: 0x06006221 RID: 25121 RVA: 0x001879F7 File Offset: 0x00185BF7
		internal IntList CellIDs
		{
			get
			{
				return this.m_cellIDs;
			}
			set
			{
				this.m_cellIDs = value;
			}
		}

		// Token: 0x170022A8 RID: 8872
		// (get) Token: 0x06006222 RID: 25122 RVA: 0x00187A00 File Offset: 0x00185C00
		// (set) Token: 0x06006223 RID: 25123 RVA: 0x00187A08 File Offset: 0x00185C08
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

		// Token: 0x170022A9 RID: 8873
		// (get) Token: 0x06006224 RID: 25124 RVA: 0x00187A11 File Offset: 0x00185C11
		// (set) Token: 0x06006225 RID: 25125 RVA: 0x00187A19 File Offset: 0x00185C19
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

		// Token: 0x170022AA RID: 8874
		// (get) Token: 0x06006226 RID: 25126 RVA: 0x00187A22 File Offset: 0x00185C22
		// (set) Token: 0x06006227 RID: 25127 RVA: 0x00187A2A File Offset: 0x00185C2A
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

		// Token: 0x170022AB RID: 8875
		// (get) Token: 0x06006228 RID: 25128 RVA: 0x00187A33 File Offset: 0x00185C33
		// (set) Token: 0x06006229 RID: 25129 RVA: 0x00187A3B File Offset: 0x00185C3B
		internal MatrixRowList MatrixRows
		{
			get
			{
				return this.m_matrixRows;
			}
			set
			{
				this.m_matrixRows = value;
			}
		}

		// Token: 0x170022AC RID: 8876
		// (get) Token: 0x0600622A RID: 25130 RVA: 0x00187A44 File Offset: 0x00185C44
		// (set) Token: 0x0600622B RID: 25131 RVA: 0x00187A4C File Offset: 0x00185C4C
		internal MatrixColumnList MatrixColumns
		{
			get
			{
				return this.m_matrixColumns;
			}
			set
			{
				this.m_matrixColumns = value;
			}
		}

		// Token: 0x170022AD RID: 8877
		// (get) Token: 0x0600622C RID: 25132 RVA: 0x00187A55 File Offset: 0x00185C55
		// (set) Token: 0x0600622D RID: 25133 RVA: 0x00187A5D File Offset: 0x00185C5D
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

		// Token: 0x170022AE RID: 8878
		// (get) Token: 0x0600622E RID: 25134 RVA: 0x00187A66 File Offset: 0x00185C66
		// (set) Token: 0x0600622F RID: 25135 RVA: 0x00187A6E File Offset: 0x00185C6E
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

		// Token: 0x170022AF RID: 8879
		// (get) Token: 0x06006230 RID: 25136 RVA: 0x00187A77 File Offset: 0x00185C77
		internal override PivotHeading PivotStaticColumns
		{
			get
			{
				return this.m_staticColumns;
			}
		}

		// Token: 0x170022B0 RID: 8880
		// (get) Token: 0x06006231 RID: 25137 RVA: 0x00187A7F File Offset: 0x00185C7F
		internal override PivotHeading PivotStaticRows
		{
			get
			{
				return this.m_staticRows;
			}
		}

		// Token: 0x170022B1 RID: 8881
		// (get) Token: 0x06006232 RID: 25138 RVA: 0x00187A87 File Offset: 0x00185C87
		// (set) Token: 0x06006233 RID: 25139 RVA: 0x00187A8F File Offset: 0x00185C8F
		internal MatrixHeading StaticColumns
		{
			get
			{
				return this.m_staticColumns;
			}
			set
			{
				this.m_staticColumns = value;
			}
		}

		// Token: 0x170022B2 RID: 8882
		// (get) Token: 0x06006234 RID: 25140 RVA: 0x00187A98 File Offset: 0x00185C98
		// (set) Token: 0x06006235 RID: 25141 RVA: 0x00187AA0 File Offset: 0x00185CA0
		internal MatrixHeading StaticRows
		{
			get
			{
				return this.m_staticRows;
			}
			set
			{
				this.m_staticRows = value;
			}
		}

		// Token: 0x170022B3 RID: 8883
		// (get) Token: 0x06006236 RID: 25142 RVA: 0x00187AA9 File Offset: 0x00185CA9
		// (set) Token: 0x06006237 RID: 25143 RVA: 0x00187AB1 File Offset: 0x00185CB1
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

		// Token: 0x170022B4 RID: 8884
		// (get) Token: 0x06006238 RID: 25144 RVA: 0x00187ABA File Offset: 0x00185CBA
		// (set) Token: 0x06006239 RID: 25145 RVA: 0x00187AC2 File Offset: 0x00185CC2
		internal StringList OwcCellNames
		{
			get
			{
				return this.m_owcCellNames;
			}
			set
			{
				this.m_owcCellNames = value;
			}
		}

		// Token: 0x170022B5 RID: 8885
		// (get) Token: 0x0600623A RID: 25146 RVA: 0x00187ACB File Offset: 0x00185CCB
		// (set) Token: 0x0600623B RID: 25147 RVA: 0x00187AD3 File Offset: 0x00185CD3
		internal string CellDataElementName
		{
			get
			{
				return this.m_cellDataElementName;
			}
			set
			{
				this.m_cellDataElementName = value;
			}
		}

		// Token: 0x170022B6 RID: 8886
		// (get) Token: 0x0600623C RID: 25148 RVA: 0x00187ADC File Offset: 0x00185CDC
		// (set) Token: 0x0600623D RID: 25149 RVA: 0x00187AE4 File Offset: 0x00185CE4
		internal bool FirstInstance
		{
			get
			{
				return this.m_firstInstance;
			}
			set
			{
				this.m_firstInstance = value;
			}
		}

		// Token: 0x170022B7 RID: 8887
		// (get) Token: 0x0600623E RID: 25150 RVA: 0x00187AED File Offset: 0x00185CED
		// (set) Token: 0x0600623F RID: 25151 RVA: 0x00187AF5 File Offset: 0x00185CF5
		internal BoolList FirstCellInstances
		{
			get
			{
				return this.m_firstCellInstances;
			}
			set
			{
				this.m_firstCellInstances = value;
			}
		}

		// Token: 0x170022B8 RID: 8888
		// (get) Token: 0x06006240 RID: 25152 RVA: 0x00187AFE File Offset: 0x00185CFE
		internal MatrixExprHost MatrixExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170022B9 RID: 8889
		// (get) Token: 0x06006241 RID: 25153 RVA: 0x00187B06 File Offset: 0x00185D06
		protected override DataRegionExprHost DataRegionExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170022BA RID: 8890
		// (get) Token: 0x06006242 RID: 25154 RVA: 0x00187B0E File Offset: 0x00185D0E
		// (set) Token: 0x06006243 RID: 25155 RVA: 0x00187B16 File Offset: 0x00185D16
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

		// Token: 0x170022BB RID: 8891
		// (get) Token: 0x06006244 RID: 25156 RVA: 0x00187B1F File Offset: 0x00185D1F
		// (set) Token: 0x06006245 RID: 25157 RVA: 0x00187B27 File Offset: 0x00185D27
		internal NonComputedUniqueNames CornerNonComputedUniqueNames
		{
			get
			{
				return this.m_cornerNonComputedUniqueNames;
			}
			set
			{
				this.m_cornerNonComputedUniqueNames = value;
			}
		}

		// Token: 0x170022BC RID: 8892
		// (get) Token: 0x06006246 RID: 25158 RVA: 0x00187B30 File Offset: 0x00185D30
		// (set) Token: 0x06006247 RID: 25159 RVA: 0x00187B38 File Offset: 0x00185D38
		internal bool InOutermostSubtotalCell
		{
			get
			{
				return this.m_inOutermostSubtotalCell;
			}
			set
			{
				this.m_inOutermostSubtotalCell = value;
			}
		}

		// Token: 0x170022BD RID: 8893
		// (get) Token: 0x06006248 RID: 25160 RVA: 0x00187B44 File Offset: 0x00185D44
		internal ReportSizeCollection CellHeightsForRendering
		{
			get
			{
				if (this.m_cellHeightsForRendering == null)
				{
					this.m_cellHeightsForRendering = new ReportSizeCollection(this.m_matrixRows.Count);
					for (int i = 0; i < this.m_matrixRows.Count; i++)
					{
						MatrixRow matrixRow = this.m_matrixRows[i];
						this.m_cellHeightsForRendering[i] = new ReportSize(matrixRow.Height, matrixRow.HeightValue);
					}
				}
				return this.m_cellHeightsForRendering;
			}
		}

		// Token: 0x170022BE RID: 8894
		// (get) Token: 0x06006249 RID: 25161 RVA: 0x00187BB8 File Offset: 0x00185DB8
		internal ReportSizeCollection CellWidthsForRendering
		{
			get
			{
				if (this.m_cellWidthsForRendering == null)
				{
					this.m_cellWidthsForRendering = new ReportSizeCollection(this.m_matrixColumns.Count);
					for (int i = 0; i < this.m_matrixColumns.Count; i++)
					{
						MatrixColumn matrixColumn = this.m_matrixColumns[i];
						this.m_cellWidthsForRendering[i] = new ReportSize(matrixColumn.Width, matrixColumn.WidthValue);
					}
				}
				return this.m_cellWidthsForRendering;
			}
		}

		// Token: 0x170022BF RID: 8895
		// (get) Token: 0x0600624A RID: 25162 RVA: 0x00187C29 File Offset: 0x00185E29
		// (set) Token: 0x0600624B RID: 25163 RVA: 0x00187C31 File Offset: 0x00185E31
		internal string[] CellIDsForRendering
		{
			get
			{
				return this.m_cellIDsForRendering;
			}
			set
			{
				this.m_cellIDsForRendering = value;
			}
		}

		// Token: 0x170022C0 RID: 8896
		// (get) Token: 0x0600624C RID: 25164 RVA: 0x00187C3A File Offset: 0x00185E3A
		// (set) Token: 0x0600624D RID: 25165 RVA: 0x00187C42 File Offset: 0x00185E42
		internal bool ColumnGroupingFixedHeader
		{
			get
			{
				return this.m_columnGroupingFixedHeader;
			}
			set
			{
				this.m_columnGroupingFixedHeader = value;
			}
		}

		// Token: 0x170022C1 RID: 8897
		// (get) Token: 0x0600624E RID: 25166 RVA: 0x00187C4B File Offset: 0x00185E4B
		// (set) Token: 0x0600624F RID: 25167 RVA: 0x00187C53 File Offset: 0x00185E53
		internal bool RowGroupingFixedHeader
		{
			get
			{
				return this.m_rowGroupingFixedHeader;
			}
			set
			{
				this.m_rowGroupingFixedHeader = value;
			}
		}

		// Token: 0x170022C2 RID: 8898
		// (get) Token: 0x06006250 RID: 25168 RVA: 0x00187C5C File Offset: 0x00185E5C
		internal ReportProcessing.PageTextboxes CellPageTextboxes
		{
			get
			{
				return this.m_cellPageTextboxes;
			}
		}

		// Token: 0x170022C3 RID: 8899
		// (get) Token: 0x06006251 RID: 25169 RVA: 0x00187C64 File Offset: 0x00185E64
		internal ReportProcessing.PageTextboxes ColumnHeaderPageTextboxes
		{
			get
			{
				return this.m_columnHeaderPageTextboxes;
			}
		}

		// Token: 0x170022C4 RID: 8900
		// (get) Token: 0x06006252 RID: 25170 RVA: 0x00187C6C File Offset: 0x00185E6C
		internal ReportProcessing.PageTextboxes RowHeaderPageTextboxes
		{
			get
			{
				return this.m_rowHeaderPageTextboxes;
			}
		}

		// Token: 0x170022C5 RID: 8901
		// (get) Token: 0x06006253 RID: 25171 RVA: 0x00187C74 File Offset: 0x00185E74
		// (set) Token: 0x06006254 RID: 25172 RVA: 0x00187C91 File Offset: 0x00185E91
		internal int CellPage
		{
			get
			{
				if (0 > this.m_cellPage)
				{
					this.m_cellPage = this.m_currentPage;
				}
				return this.m_cellPage;
			}
			set
			{
				this.m_cellPage = value;
			}
		}

		// Token: 0x06006255 RID: 25173 RVA: 0x00187C9C File Offset: 0x00185E9C
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

		// Token: 0x06006256 RID: 25174 RVA: 0x00187CF0 File Offset: 0x00185EF0
		private void InternalInitialize(InitializationContext context)
		{
			context.Location = context.Location | LocationFlags.InDataSet | LocationFlags.InDataRegion;
			context.ExprHostBuilder.MatrixStart(this.m_name);
			base.Initialize(context);
			context.RegisterRunningValues(this.m_runningValues);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, true, false);
			}
			this.CornerInitialize(context);
			context.Location &= ~LocationFlags.InMatrixCellTopLevelItem;
			bool flag = false;
			context.Location |= LocationFlags.InMatrixGroupHeader;
			int num;
			double num2;
			this.ColumnsInitialize(context, out num, out num2, out flag);
			bool flag2 = flag;
			int num3;
			double num4;
			this.RowsInitialize(context, out num3, out num4, out flag);
			context.Location &= ~LocationFlags.InMatrixGroupHeader;
			if (flag)
			{
				flag2 = true;
			}
			double num5;
			double num6;
			this.MatrixCellInitialize(context, num, num3, flag2, out num5, out num6);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
			context.UnRegisterRunningValues(this.m_runningValues);
			base.CopyHeadingAggregates(this.m_rows);
			this.m_rows.TransferHeadingAggregates();
			base.CopyHeadingAggregates(this.m_columns);
			this.m_columns.TransferHeadingAggregates();
			this.m_heightValue = num2 + num5;
			this.m_height = Converter.ConvertSize(this.m_heightValue);
			this.m_widthValue = num4 + num6;
			this.m_width = Converter.ConvertSize(this.m_widthValue);
			if (!context.ErrorContext.HasError)
			{
				Matrix.TopLevelItemsSizes.Calculate(this, num4, num2, num6, num5, context);
			}
			base.ExprHostID = context.ExprHostBuilder.MatrixEnd();
		}

		// Token: 0x06006257 RID: 25175 RVA: 0x00187E78 File Offset: 0x00186078
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.MatrixHostsRemotable[base.ExprHostID];
				base.DataRegionSetExprHost(this.m_exprHost, reportObjectModel);
			}
		}

		// Token: 0x06006258 RID: 25176 RVA: 0x00187EC6 File Offset: 0x001860C6
		protected override void DataRendererInitialize(InitializationContext context)
		{
			base.DataRendererInitialize(context);
			CLSNameValidator.ValidateDataElementName(ref this.m_cellDataElementName, "Cell", context.ObjectType, context.ObjectName, "CellDataElementName", context.ErrorContext);
		}

		// Token: 0x06006259 RID: 25177 RVA: 0x00187EFC File Offset: 0x001860FC
		internal override void RegisterReceiver(InitializationContext context)
		{
			context.RegisterReportItems(this.m_cornerReportItems);
			if (this.m_visibility != null)
			{
				this.m_visibility.RegisterReceiver(context, true);
			}
			this.m_cornerReportItems.RegisterReceiver(context);
			this.ColumnsRegisterReceiver(context);
			this.RowsRegisterReceiver(context);
			this.MatrixCellRegisterReceiver(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
			context.UnRegisterReportItems(this.m_cornerReportItems);
		}

		// Token: 0x0600625A RID: 25178 RVA: 0x00187F70 File Offset: 0x00186170
		internal void CalculatePropagatedFlags()
		{
			MatrixHeading matrixHeading = this.m_rows;
			int num = 0;
			do
			{
				if (matrixHeading.Grouping != null)
				{
					if (matrixHeading.Grouping.PageBreakAtStart)
					{
						this.m_propagatedPageBreakAtStart = true;
						this.m_innerRowLevelWithPageBreak = num;
					}
					if (matrixHeading.Grouping.PageBreakAtEnd)
					{
						this.m_propagatedPageBreakAtEnd = true;
						this.m_innerRowLevelWithPageBreak = num;
					}
				}
				matrixHeading = matrixHeading.SubHeading;
				num++;
			}
			while (matrixHeading != null);
		}

		// Token: 0x0600625B RID: 25179 RVA: 0x00187FD2 File Offset: 0x001861D2
		private void CornerInitialize(InitializationContext context)
		{
			this.m_cornerReportItems.Initialize(context, false);
		}

		// Token: 0x0600625C RID: 25180 RVA: 0x00187FE4 File Offset: 0x001861E4
		private void ColumnsInitialize(InitializationContext context, out int expectedNumberOfMatrixColumns, out double size, out bool computedSubtotal)
		{
			computedSubtotal = false;
			size = 0.0;
			this.m_columns.DynamicInitialize(true, 0, context, ref size);
			this.m_columns.StaticInitialize(context);
			expectedNumberOfMatrixColumns = ((this.m_staticColumns != null) ? this.m_staticColumns.NumberOfStatics : 1);
			if (this.m_columns.Grouping == null)
			{
				Global.Tracer.Assert(this.m_columns.ReportItems != null);
				context.SpecialTransferRunningValues(this.m_columns.ReportItems.RunningValues);
				return;
			}
			if (this.m_columns.Subtotal != null)
			{
				Global.Tracer.Assert(this.m_columns.Subtotal.ReportItems != null);
				context.SpecialTransferRunningValues(this.m_columns.Subtotal.ReportItems.RunningValues);
				computedSubtotal = this.m_columns.Subtotal.Computed;
			}
		}

		// Token: 0x0600625D RID: 25181 RVA: 0x001880CB File Offset: 0x001862CB
		private void ColumnsRegisterReceiver(InitializationContext context)
		{
			this.m_columns.DynamicRegisterReceiver(context);
			this.m_columns.StaticRegisterReceiver(context);
		}

		// Token: 0x0600625E RID: 25182 RVA: 0x001880E8 File Offset: 0x001862E8
		private void RowsInitialize(InitializationContext context, out int expectedNumberOfMatrixRows, out double size, out bool computedSubtotal)
		{
			computedSubtotal = false;
			size = 0.0;
			this.m_rows.DynamicInitialize(false, 0, context, ref size);
			this.m_rows.StaticInitialize(context);
			expectedNumberOfMatrixRows = ((this.m_staticRows != null) ? this.m_staticRows.NumberOfStatics : 1);
			if (this.m_rows.Grouping == null)
			{
				Global.Tracer.Assert(this.m_rows.ReportItems != null);
				context.SpecialTransferRunningValues(this.m_rows.ReportItems.RunningValues);
				return;
			}
			if (this.m_rows.Subtotal != null)
			{
				Global.Tracer.Assert(this.m_rows.Subtotal.ReportItems != null);
				context.SpecialTransferRunningValues(this.m_rows.Subtotal.ReportItems.RunningValues);
				computedSubtotal = this.m_rows.Subtotal.Computed;
			}
		}

		// Token: 0x0600625F RID: 25183 RVA: 0x001881CF File Offset: 0x001863CF
		private void RowsRegisterReceiver(InitializationContext context)
		{
			this.m_rows.DynamicRegisterReceiver(context);
			this.m_rows.StaticRegisterReceiver(context);
		}

		// Token: 0x06006260 RID: 25184 RVA: 0x001881EC File Offset: 0x001863EC
		private void MatrixCellInitialize(InitializationContext context, int expectedNumberOfMatrixColumns, int expectedNumberOfMatrixRows, bool computedSubtotal, out double totalCellHeight, out double totalCellWidth)
		{
			if (expectedNumberOfMatrixColumns != this.m_matrixColumns.Count)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfMatrixColumns, Severity.Error, context.ObjectType, context.ObjectName, "MatrixColumns", Array.Empty<string>());
			}
			if (expectedNumberOfMatrixRows != this.m_matrixRows.Count)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfMatrixRows, Severity.Error, context.ObjectType, context.ObjectName, "MatrixRows", Array.Empty<string>());
			}
			for (int i = 0; i < this.m_matrixRows.Count; i++)
			{
				if (expectedNumberOfMatrixColumns != this.m_matrixRows[i].NumberOfMatrixCells)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfMatrixCells, Severity.Error, context.ObjectType, context.ObjectName, "MatrixCells", Array.Empty<string>());
				}
			}
			totalCellHeight = 0.0;
			totalCellWidth = 0.0;
			for (int j = 0; j < this.m_matrixColumns.Count; j++)
			{
				this.m_matrixColumns[j].Initialize(context);
				totalCellWidth = Math.Round(totalCellWidth + this.m_matrixColumns[j].WidthValue, Validator.DecimalPrecision);
			}
			for (int k = 0; k < this.m_matrixRows.Count; k++)
			{
				this.m_matrixRows[k].Initialize(context);
				totalCellHeight = Math.Round(totalCellHeight + this.m_matrixRows[k].HeightValue, Validator.DecimalPrecision);
			}
			context.Location = context.Location | LocationFlags.InMatrixCell | LocationFlags.InMatrixCellTopLevelItem;
			context.MatrixName = this.m_name;
			context.RegisterTablixCellScope(this.m_columns.SubHeading == null && this.m_columns.Grouping == null, this.m_cellAggregates, this.m_cellPostSortAggregates);
			for (MatrixHeading matrixHeading = this.m_rows; matrixHeading != null; matrixHeading = matrixHeading.SubHeading)
			{
				context.RegisterReportItems(matrixHeading.ReportItems);
				if (matrixHeading.Grouping != null)
				{
					context.Location |= LocationFlags.InGrouping;
					context.RegisterGroupingScopeForTablixCell(matrixHeading.Grouping.Name, false, matrixHeading.Grouping.SimpleGroupExpressions, matrixHeading.Aggregates, matrixHeading.PostSortAggregates, matrixHeading.RecursiveAggregates, matrixHeading.Grouping);
				}
			}
			if (this.m_rows.Grouping != null && this.m_rows.Subtotal != null && this.m_staticRows != null)
			{
				context.CopyRunningValues(this.StaticRows.ReportItems.RunningValues, this.m_aggregates);
			}
			for (MatrixHeading matrixHeading = this.m_columns; matrixHeading != null; matrixHeading = matrixHeading.SubHeading)
			{
				context.RegisterReportItems(matrixHeading.ReportItems);
				if (matrixHeading.Grouping != null)
				{
					context.Location |= LocationFlags.InGrouping;
					context.RegisterGroupingScopeForTablixCell(matrixHeading.Grouping.Name, true, matrixHeading.Grouping.SimpleGroupExpressions, matrixHeading.Aggregates, matrixHeading.PostSortAggregates, matrixHeading.RecursiveAggregates, matrixHeading.Grouping);
				}
			}
			if (this.m_columns.Grouping != null && this.m_columns.Subtotal != null && this.m_staticColumns != null)
			{
				context.CopyRunningValues(this.StaticColumns.ReportItems.RunningValues, this.m_aggregates);
			}
			if (computedSubtotal)
			{
				this.m_cellReportItems.MarkChildrenComputed();
			}
			context.RegisterReportItems(this.m_cellReportItems);
			Matrix.OWCFlagsCalculator.Calculate(this);
			bool registerHiddenReceiver = context.RegisterHiddenReceiver;
			context.RegisterHiddenReceiver = false;
			context.RegisterScopeInMatrixCell(base.Name, "0_CellScope" + base.Name, true);
			this.m_cellReportItems.Initialize(context, true);
			if (context.IsRunningValueDirectionColumn())
			{
				this.m_processingInnerGrouping = Pivot.ProcessingInnerGroupings.Row;
			}
			context.UpdateScopesInMatrixCells(base.Name, this.GenerateUserSortGroupingList(Pivot.ProcessingInnerGroupings.Row == this.m_processingInnerGrouping));
			context.TextboxesWithDetailSortExpressionInitialize();
			context.RegisterHiddenReceiver = registerHiddenReceiver;
			for (MatrixHeading matrixHeading = this.m_rows; matrixHeading != null; matrixHeading = matrixHeading.SubHeading)
			{
				context.UnRegisterReportItems(matrixHeading.ReportItems);
				if (matrixHeading.Grouping != null)
				{
					context.UnRegisterGroupingScopeForTablixCell(matrixHeading.Grouping.Name, false);
					context.ProcessUserSortInnerScope(matrixHeading.Grouping.Name, true, false);
				}
			}
			for (MatrixHeading matrixHeading = this.m_rows; matrixHeading != null; matrixHeading = matrixHeading.SubHeading)
			{
				if (matrixHeading.Grouping != null)
				{
					context.ValidateUserSortInnerScope(matrixHeading.Grouping.Name);
				}
			}
			for (MatrixHeading matrixHeading = this.m_columns; matrixHeading != null; matrixHeading = matrixHeading.SubHeading)
			{
				context.UnRegisterReportItems(matrixHeading.ReportItems);
				if (matrixHeading.Grouping != null)
				{
					context.UnRegisterGroupingScopeForTablixCell(matrixHeading.Grouping.Name, true);
					context.ProcessUserSortInnerScope(matrixHeading.Grouping.Name, true, true);
				}
			}
			for (MatrixHeading matrixHeading = this.m_columns; matrixHeading != null; matrixHeading = matrixHeading.SubHeading)
			{
				if (matrixHeading.Grouping != null)
				{
					context.ValidateUserSortInnerScope(matrixHeading.Grouping.Name);
				}
			}
			this.m_cellReportItems.RegisterReceiver(context);
			context.UnRegisterReportItems(this.m_cellReportItems);
			context.UnRegisterTablixCellScope();
		}

		// Token: 0x06006261 RID: 25185 RVA: 0x001886D4 File Offset: 0x001868D4
		private GroupingList GenerateUserSortGroupingList(bool rowIsInnerGrouping)
		{
			GroupingList groupingList = new GroupingList();
			for (MatrixHeading matrixHeading = (rowIsInnerGrouping ? this.m_rows : this.m_columns); matrixHeading != null; matrixHeading = matrixHeading.SubHeading)
			{
				if (matrixHeading.Grouping != null)
				{
					groupingList.Add(matrixHeading.Grouping);
				}
			}
			for (MatrixHeading matrixHeading = (rowIsInnerGrouping ? this.m_columns : this.m_rows); matrixHeading != null; matrixHeading = matrixHeading.SubHeading)
			{
				if (matrixHeading.Grouping != null)
				{
					groupingList.Add(matrixHeading.Grouping);
				}
			}
			return groupingList;
		}

		// Token: 0x06006262 RID: 25186 RVA: 0x0018874E File Offset: 0x0018694E
		private void MatrixCellRegisterReceiver(InitializationContext context)
		{
			context.RegisterReportItems(this.m_cellReportItems);
			this.m_cellReportItems.RegisterReceiver(context);
			context.UnRegisterReportItems(this.m_cellReportItems);
		}

		// Token: 0x06006263 RID: 25187 RVA: 0x00188778 File Offset: 0x00186978
		bool IPageBreakItem.IgnorePageBreaks()
		{
			if (this.m_pagebreakState == PageBreakStates.Unknown)
			{
				if (SharedHiddenState.Never != Visibility.GetSharedHidden(this.m_visibility))
				{
					this.m_pagebreakState = PageBreakStates.CanIgnore;
				}
				else if (SharedHiddenState.Never != Visibility.GetSharedHidden(this.m_rows.Visibility))
				{
					this.m_pagebreakState = PageBreakStates.CanIgnore;
				}
				else
				{
					this.m_pagebreakState = PageBreakStates.CannotIgnore;
				}
			}
			return PageBreakStates.CanIgnore == this.m_pagebreakState;
		}

		// Token: 0x06006264 RID: 25188 RVA: 0x001887D4 File Offset: 0x001869D4
		internal Microsoft.ReportingServices.ReportProcessing.ReportItem GetCellReportItem(int rowIndex, int columnIndex)
		{
			int num = rowIndex * this.m_matrixColumns.Count + columnIndex;
			return this.m_cellReportItems[num];
		}

		// Token: 0x06006265 RID: 25189 RVA: 0x001887FD File Offset: 0x001869FD
		internal void InitializePageSectionProcessing()
		{
			this.m_cellPageTextboxes = new ReportProcessing.PageTextboxes();
			this.m_columnHeaderPageTextboxes = new ReportProcessing.PageTextboxes();
			this.m_rowHeaderPageTextboxes = new ReportProcessing.PageTextboxes();
		}

		// Token: 0x06006266 RID: 25190 RVA: 0x00188820 File Offset: 0x00186A20
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.Pivot, new MemberInfoList
			{
				new MemberInfo(MemberName.Columns, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.MatrixHeading),
				new MemberInfo(MemberName.Rows, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.MatrixHeading),
				new MemberInfo(MemberName.CornerReportItems, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemCollection),
				new MemberInfo(MemberName.CellReportItems, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemCollection),
				new MemberInfo(MemberName.CellIDs, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.IntList),
				new MemberInfo(MemberName.PropagatedPageBreakAtStart, Token.Boolean),
				new MemberInfo(MemberName.PropagatedPageBreakAtEnd, Token.Boolean),
				new MemberInfo(MemberName.InnerRowLevelWithPageBreak, Token.Int32),
				new MemberInfo(MemberName.MatrixRows, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.MatrixRowList),
				new MemberInfo(MemberName.MatrixColumns, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.MatrixColumnList),
				new MemberInfo(MemberName.GroupsBeforeRowHeaders, Token.Int32),
				new MemberInfo(MemberName.LayoutDirection, Token.Boolean),
				new MemberInfo(MemberName.StaticColumns, Token.Reference, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.MatrixHeading),
				new MemberInfo(MemberName.StaticRows, Token.Reference, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.MatrixHeading),
				new MemberInfo(MemberName.UseOwc, Token.Boolean),
				new MemberInfo(MemberName.OwcCellNames, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.StringList),
				new MemberInfo(MemberName.CellDataElementName, Token.String),
				new MemberInfo(MemberName.ColumnGroupingFixedHeader, Token.Boolean),
				new MemberInfo(MemberName.RowGroupingFixedHeader, Token.Boolean)
			});
		}

		// Token: 0x0400318C RID: 12684
		private MatrixHeading m_columns;

		// Token: 0x0400318D RID: 12685
		private MatrixHeading m_rows;

		// Token: 0x0400318E RID: 12686
		private ReportItemCollection m_cornerReportItems;

		// Token: 0x0400318F RID: 12687
		private ReportItemCollection m_cellReportItems;

		// Token: 0x04003190 RID: 12688
		private IntList m_cellIDs;

		// Token: 0x04003191 RID: 12689
		private bool m_propagatedPageBreakAtStart;

		// Token: 0x04003192 RID: 12690
		private bool m_propagatedPageBreakAtEnd;

		// Token: 0x04003193 RID: 12691
		private int m_innerRowLevelWithPageBreak = -1;

		// Token: 0x04003194 RID: 12692
		private MatrixRowList m_matrixRows;

		// Token: 0x04003195 RID: 12693
		private MatrixColumnList m_matrixColumns;

		// Token: 0x04003196 RID: 12694
		private int m_groupsBeforeRowHeaders;

		// Token: 0x04003197 RID: 12695
		private bool m_layoutDirection;

		// Token: 0x04003198 RID: 12696
		[Reference]
		private MatrixHeading m_staticColumns;

		// Token: 0x04003199 RID: 12697
		[Reference]
		private MatrixHeading m_staticRows;

		// Token: 0x0400319A RID: 12698
		private bool m_useOWC;

		// Token: 0x0400319B RID: 12699
		private StringList m_owcCellNames;

		// Token: 0x0400319C RID: 12700
		private string m_cellDataElementName;

		// Token: 0x0400319D RID: 12701
		private bool m_columnGroupingFixedHeader;

		// Token: 0x0400319E RID: 12702
		private bool m_rowGroupingFixedHeader;

		// Token: 0x0400319F RID: 12703
		[NonSerialized]
		private bool m_firstInstance = true;

		// Token: 0x040031A0 RID: 12704
		[NonSerialized]
		private BoolList m_firstCellInstances;

		// Token: 0x040031A1 RID: 12705
		[NonSerialized]
		private MatrixExprHost m_exprHost;

		// Token: 0x040031A2 RID: 12706
		[NonSerialized]
		private int m_currentPage = -1;

		// Token: 0x040031A3 RID: 12707
		[NonSerialized]
		private int m_cellPage = -1;

		// Token: 0x040031A4 RID: 12708
		[NonSerialized]
		private ReportProcessing.PageTextboxes m_cellPageTextboxes;

		// Token: 0x040031A5 RID: 12709
		[NonSerialized]
		private ReportProcessing.PageTextboxes m_columnHeaderPageTextboxes;

		// Token: 0x040031A6 RID: 12710
		[NonSerialized]
		private ReportProcessing.PageTextboxes m_rowHeaderPageTextboxes;

		// Token: 0x040031A7 RID: 12711
		[NonSerialized]
		private NonComputedUniqueNames m_cornerNonComputedUniqueNames;

		// Token: 0x040031A8 RID: 12712
		[NonSerialized]
		private bool m_inOutermostSubtotalCell;

		// Token: 0x040031A9 RID: 12713
		[NonSerialized]
		private ReportSizeCollection m_cellHeightsForRendering;

		// Token: 0x040031AA RID: 12714
		[NonSerialized]
		private ReportSizeCollection m_cellWidthsForRendering;

		// Token: 0x040031AB RID: 12715
		[NonSerialized]
		private string[] m_cellIDsForRendering;

		// Token: 0x02000CC6 RID: 3270
		private sealed class OWCFlagsCalculator
		{
			// Token: 0x06008CFA RID: 36090 RVA: 0x0023D57F File Offset: 0x0023B77F
			private OWCFlagsCalculator()
			{
			}

			// Token: 0x06008CFB RID: 36091 RVA: 0x0023D5A4 File Offset: 0x0023B7A4
			internal static void Calculate(Matrix matrix)
			{
				Global.Tracer.Assert(matrix != null);
				Matrix.OWCFlagsCalculator owcflagsCalculator = new Matrix.OWCFlagsCalculator();
				owcflagsCalculator.CalculateOWCFlags(matrix);
				if (owcflagsCalculator.m_useOWC)
				{
					matrix.UseOWC = owcflagsCalculator.m_useOWC;
					matrix.OwcCellNames = owcflagsCalculator.m_owcCellNames;
					int num = 0;
					for (MatrixHeading matrixHeading = matrix.Rows; matrixHeading != null; matrixHeading = matrixHeading.SubHeading)
					{
						if (matrixHeading.Grouping != null)
						{
							matrixHeading.OwcGroupExpression = owcflagsCalculator.m_owcGroupExpression[num];
							num++;
						}
					}
					for (MatrixHeading matrixHeading2 = matrix.Columns; matrixHeading2 != null; matrixHeading2 = matrixHeading2.SubHeading)
					{
						if (matrixHeading2.Grouping != null)
						{
							matrixHeading2.OwcGroupExpression = owcflagsCalculator.m_owcGroupExpression[num];
							num++;
						}
					}
				}
			}

			// Token: 0x06008CFC RID: 36092 RVA: 0x0023D650 File Offset: 0x0023B850
			private void CalculateOWCFlags(Matrix matrix)
			{
				this.CalculateOWCFlags(matrix.Rows);
				if (this.IsFinish())
				{
					return;
				}
				this.CalculateOWCFlags(matrix.Columns);
				if (this.IsFinish())
				{
					return;
				}
				if (matrix.CellReportItems != null)
				{
					for (int i = 0; i < matrix.CellReportItems.Count; i++)
					{
						this.DetectIllegalReportItems(matrix.CellReportItems[i]);
						if (this.IsFinish())
						{
							return;
						}
						TextBox textBox = this.FindNotAlwaysHiddenTextBox(matrix.CellReportItems[i]);
						if (this.IsFinish())
						{
							return;
						}
						if (textBox == null)
						{
							this.m_useOWC = false;
							return;
						}
						Global.Tracer.Assert(textBox.Value != null);
						DataAggregateInfo sumAggregateWithoutScope = textBox.Value.GetSumAggregateWithoutScope();
						if (sumAggregateWithoutScope == null)
						{
							this.m_useOWC = false;
							return;
						}
						Global.Tracer.Assert(sumAggregateWithoutScope.Expressions != null);
						Global.Tracer.Assert(1 == sumAggregateWithoutScope.Expressions.Length);
						if (ExpressionInfo.Types.Field == sumAggregateWithoutScope.Expressions[0].Type)
						{
							this.m_owcCellNames.Add(sumAggregateWithoutScope.Expressions[0].Value);
						}
						else
						{
							this.m_owcCellNames.Add(textBox.Name);
						}
					}
				}
			}

			// Token: 0x06008CFD RID: 36093 RVA: 0x0023D780 File Offset: 0x0023B980
			private void CalculateOWCFlags(MatrixHeading heading)
			{
				if (heading == null)
				{
					return;
				}
				if (heading.Grouping == null)
				{
					this.m_staticHeadingCount++;
					if (this.m_staticHeadingCount > 1)
					{
						this.m_useOWC = false;
						return;
					}
					if (heading.SubHeading != null)
					{
						this.m_useOWC = false;
						return;
					}
					if (heading.ReportItems != null)
					{
						for (int i = 0; i < heading.ReportItems.Count; i++)
						{
							this.DetectIllegalReportItems(heading.ReportItems[i]);
							if (this.IsFinish())
							{
								return;
							}
							TextBox textBox = this.FindNotAlwaysHiddenTextBox(heading.ReportItems[i]);
							if (this.IsFinish())
							{
								return;
							}
							if (textBox == null)
							{
								this.m_useOWC = false;
								return;
							}
						}
					}
				}
				else
				{
					ExpressionInfo expressionInfo = null;
					if (heading.Grouping.GroupExpressions != null)
					{
						if (heading.Grouping.GroupExpressions.Count != 1)
						{
							this.m_useOWC = false;
							return;
						}
						expressionInfo = heading.Grouping.GroupExpressions[0];
					}
					this.DetectIllegalReportItems(heading.ReportItem);
					if (this.IsFinish())
					{
						return;
					}
					TextBox textBox2 = this.FindNotAlwaysHiddenTextBox(heading.ReportItem);
					if (this.IsFinish())
					{
						return;
					}
					if (textBox2 == null)
					{
						this.m_useOWC = false;
						return;
					}
					Global.Tracer.Assert(expressionInfo != null);
					Global.Tracer.Assert(textBox2.Value != null);
					if (expressionInfo.OriginalText != textBox2.Value.OriginalText)
					{
						this.m_owcGroupExpression.Add(true);
					}
					else
					{
						this.m_owcGroupExpression.Add(false);
					}
				}
				this.CalculateOWCFlags(heading.SubHeading);
			}

			// Token: 0x06008CFE RID: 36094 RVA: 0x0023D910 File Offset: 0x0023BB10
			private void DetectIllegalReportItems(Microsoft.ReportingServices.ReportProcessing.ReportItem reportItem)
			{
				if (reportItem is DataRegion || reportItem is Image || reportItem is SubReport || reportItem is ActiveXControl || reportItem is CheckBox)
				{
					this.m_useOWC = false;
					return;
				}
				if (reportItem is Rectangle)
				{
					this.DetectIllegalReportItems(((Rectangle)reportItem).ReportItems);
				}
			}

			// Token: 0x06008CFF RID: 36095 RVA: 0x0023D968 File Offset: 0x0023BB68
			private void DetectIllegalReportItems(ReportItemCollection reportItems)
			{
				if (reportItems == null)
				{
					return;
				}
				for (int i = 0; i < reportItems.Count; i++)
				{
					this.DetectIllegalReportItems(reportItems[i]);
					if (this.IsFinish())
					{
						return;
					}
				}
			}

			// Token: 0x06008D00 RID: 36096 RVA: 0x0023D9A0 File Offset: 0x0023BBA0
			private TextBox FindNotAlwaysHiddenTextBox(Microsoft.ReportingServices.ReportProcessing.ReportItem reportItem)
			{
				if (reportItem is TextBox)
				{
					if (Visibility.GetSharedHidden(reportItem.Visibility) != SharedHiddenState.Always)
					{
						return (TextBox)reportItem;
					}
				}
				else if (reportItem is Rectangle)
				{
					return this.FindNotAlwaysHiddenTextBox(((Rectangle)reportItem).ReportItems);
				}
				return null;
			}

			// Token: 0x06008D01 RID: 36097 RVA: 0x0023D9DC File Offset: 0x0023BBDC
			private TextBox FindNotAlwaysHiddenTextBox(ReportItemCollection reportItems)
			{
				if (reportItems == null)
				{
					return null;
				}
				TextBox textBox = null;
				for (int i = 0; i < reportItems.Count; i++)
				{
					Microsoft.ReportingServices.ReportProcessing.ReportItem reportItem = reportItems[i];
					TextBox textBox2 = this.FindNotAlwaysHiddenTextBox(reportItem);
					if (this.IsFinish())
					{
						return null;
					}
					if (textBox2 != null)
					{
						if (textBox != null)
						{
							this.m_useOWC = false;
							return null;
						}
						textBox = textBox2;
					}
				}
				return textBox;
			}

			// Token: 0x06008D02 RID: 36098 RVA: 0x0023DA2D File Offset: 0x0023BC2D
			private bool IsFinish()
			{
				return !this.m_useOWC;
			}

			// Token: 0x04004E95 RID: 20117
			private bool m_useOWC = true;

			// Token: 0x04004E96 RID: 20118
			private StringList m_owcCellNames = new StringList();

			// Token: 0x04004E97 RID: 20119
			private BoolList m_owcGroupExpression = new BoolList();

			// Token: 0x04004E98 RID: 20120
			private int m_staticHeadingCount;
		}

		// Token: 0x02000CC7 RID: 3271
		private sealed class TopLevelItemsSizes
		{
			// Token: 0x06008D03 RID: 36099 RVA: 0x0023DA38 File Offset: 0x0023BC38
			private TopLevelItemsSizes(MatrixColumnList columns, MatrixRowList rows, InitializationContext context)
			{
				this.m_columns = columns;
				this.m_rows = rows;
				this.m_context = context;
			}

			// Token: 0x06008D04 RID: 36100 RVA: 0x0023DA55 File Offset: 0x0023BC55
			internal static void Calculate(Matrix matrix, double cornerWidth, double cornerHeight, double colsWidth, double rowsHeight, InitializationContext context)
			{
				new Matrix.TopLevelItemsSizes(matrix.MatrixColumns, matrix.MatrixRows, context).CalculateSizes(matrix, cornerWidth, cornerHeight, colsWidth, rowsHeight);
			}

			// Token: 0x06008D05 RID: 36101 RVA: 0x0023DA75 File Offset: 0x0023BC75
			private void CalculateSizes(Matrix matrix, double cornerWidth, double cornerHeight, double colsWidth, double rowsHeight)
			{
				this.CalculateCorner(matrix, cornerWidth, cornerHeight);
				this.CalculateColumns(matrix.Columns, colsWidth);
				this.CalculateRows(matrix.Rows, rowsHeight);
				this.CalculateCells(matrix);
			}

			// Token: 0x06008D06 RID: 36102 RVA: 0x0023DAA3 File Offset: 0x0023BCA3
			private void CalculateCorner(Matrix matrix, double width, double height)
			{
				if (matrix.CornerReportItems == null || 0 >= matrix.CornerReportItems.Count)
				{
					return;
				}
				this.CalculateSize(matrix.CornerReportItems[0], width, height);
			}

			// Token: 0x06008D07 RID: 36103 RVA: 0x0023DAD0 File Offset: 0x0023BCD0
			private void CalculateCells(Matrix matrix)
			{
				int num = 0;
				for (int i = 0; i < this.m_rows.Count; i++)
				{
					for (int j = 0; j < this.m_columns.Count; j++)
					{
						this.CalculateSize(matrix.CellReportItems[num], this.m_columns[j].WidthValue, this.m_rows[i].HeightValue);
						num++;
					}
				}
			}

			// Token: 0x06008D08 RID: 36104 RVA: 0x0023DB44 File Offset: 0x0023BD44
			private void CalculateColumns(MatrixHeading column, double width)
			{
				if (column == null)
				{
					return;
				}
				double num = width;
				if (column.Grouping == null)
				{
					if (column.ReportItems != null)
					{
						for (int i = 0; i < column.ReportItems.Count; i++)
						{
							this.CalculateSize(column.ReportItems[i], this.m_columns[i].WidthValue, column.SizeValue);
							if (this.m_columns[i].WidthValue < num)
							{
								num = this.m_columns[i].WidthValue;
							}
						}
					}
				}
				else
				{
					if (column.Subtotal != null)
					{
						this.CalculateSize(column.Subtotal.ReportItem, width, column.SizeValue);
					}
					this.CalculateSize(column.ReportItem, width, column.SizeValue);
				}
				this.CalculateColumns(column.SubHeading, num);
			}

			// Token: 0x06008D09 RID: 36105 RVA: 0x0023DC14 File Offset: 0x0023BE14
			private void CalculateRows(MatrixHeading row, double height)
			{
				if (row == null)
				{
					return;
				}
				double num = height;
				if (row.Grouping == null)
				{
					if (row.ReportItems != null)
					{
						for (int i = 0; i < row.ReportItems.Count; i++)
						{
							this.CalculateSize(row.ReportItems[i], row.SizeValue, this.m_rows[i].HeightValue);
							if (this.m_rows[i].HeightValue < num)
							{
								num = this.m_rows[i].HeightValue;
							}
						}
					}
				}
				else
				{
					if (row.Subtotal != null)
					{
						this.CalculateSize(row.Subtotal.ReportItem, row.SizeValue, height);
					}
					this.CalculateSize(row.ReportItem, row.SizeValue, height);
				}
				this.CalculateRows(row.SubHeading, num);
			}

			// Token: 0x06008D0A RID: 36106 RVA: 0x0023DCE1 File Offset: 0x0023BEE1
			private void CalculateSize(Microsoft.ReportingServices.ReportProcessing.ReportItem item, double width, double height)
			{
				if (item == null)
				{
					return;
				}
				item.CalculateSizes(width, height, this.m_context, true);
			}

			// Token: 0x04004E99 RID: 20121
			private MatrixColumnList m_columns;

			// Token: 0x04004E9A RID: 20122
			private MatrixRowList m_rows;

			// Token: 0x04004E9B RID: 20123
			private InitializationContext m_context;
		}
	}
}

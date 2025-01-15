using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000762 RID: 1890
	[Serializable]
	internal sealed class CustomReportItem : Tablix, IRunningValueHolder, IErrorContext
	{
		// Token: 0x06006892 RID: 26770 RVA: 0x00196562 File Offset: 0x00194762
		internal CustomReportItem(Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x06006893 RID: 26771 RVA: 0x00196579 File Offset: 0x00194779
		internal CustomReportItem(int id, int idAltReportitem, Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(id, parent)
		{
			this.m_dataRowCells = new DataCellsList();
			this.m_cellRunningValues = new RunningValueInfoList();
			this.m_altReportItem = new ReportItemCollection(idAltReportitem, false);
		}

		// Token: 0x170024EE RID: 9454
		// (get) Token: 0x06006894 RID: 26772 RVA: 0x001965B4 File Offset: 0x001947B4
		internal override ObjectType ObjectType
		{
			get
			{
				return ObjectType.CustomReportItem;
			}
		}

		// Token: 0x170024EF RID: 9455
		// (get) Token: 0x06006895 RID: 26773 RVA: 0x001965B8 File Offset: 0x001947B8
		internal override TablixHeadingList TablixColumns
		{
			get
			{
				return this.m_columns;
			}
		}

		// Token: 0x170024F0 RID: 9456
		// (get) Token: 0x06006896 RID: 26774 RVA: 0x001965C0 File Offset: 0x001947C0
		internal override TablixHeadingList TablixRows
		{
			get
			{
				return this.m_rows;
			}
		}

		// Token: 0x170024F1 RID: 9457
		// (get) Token: 0x06006897 RID: 26775 RVA: 0x001965C8 File Offset: 0x001947C8
		internal override RunningValueInfoList TablixCellRunningValues
		{
			get
			{
				return this.m_cellRunningValues;
			}
		}

		// Token: 0x170024F2 RID: 9458
		// (get) Token: 0x06006898 RID: 26776 RVA: 0x001965D0 File Offset: 0x001947D0
		// (set) Token: 0x06006899 RID: 26777 RVA: 0x001965D8 File Offset: 0x001947D8
		internal string Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x170024F3 RID: 9459
		// (get) Token: 0x0600689A RID: 26778 RVA: 0x001965E1 File Offset: 0x001947E1
		// (set) Token: 0x0600689B RID: 26779 RVA: 0x001965E9 File Offset: 0x001947E9
		internal ReportItemCollection AltReportItem
		{
			get
			{
				return this.m_altReportItem;
			}
			set
			{
				this.m_altReportItem = value;
			}
		}

		// Token: 0x170024F4 RID: 9460
		// (get) Token: 0x0600689C RID: 26780 RVA: 0x001965F2 File Offset: 0x001947F2
		// (set) Token: 0x0600689D RID: 26781 RVA: 0x001965FA File Offset: 0x001947FA
		internal CustomReportItemHeadingList Columns
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

		// Token: 0x170024F5 RID: 9461
		// (get) Token: 0x0600689E RID: 26782 RVA: 0x00196603 File Offset: 0x00194803
		// (set) Token: 0x0600689F RID: 26783 RVA: 0x0019660B File Offset: 0x0019480B
		internal CustomReportItemHeadingList Rows
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

		// Token: 0x170024F6 RID: 9462
		// (get) Token: 0x060068A0 RID: 26784 RVA: 0x00196614 File Offset: 0x00194814
		// (set) Token: 0x060068A1 RID: 26785 RVA: 0x0019661C File Offset: 0x0019481C
		internal DataCellsList DataRowCells
		{
			get
			{
				return this.m_dataRowCells;
			}
			set
			{
				this.m_dataRowCells = value;
			}
		}

		// Token: 0x170024F7 RID: 9463
		// (get) Token: 0x060068A2 RID: 26786 RVA: 0x00196625 File Offset: 0x00194825
		// (set) Token: 0x060068A3 RID: 26787 RVA: 0x0019662D File Offset: 0x0019482D
		internal RunningValueInfoList CellRunningValues
		{
			get
			{
				return this.m_cellRunningValues;
			}
			set
			{
				this.m_cellRunningValues = value;
			}
		}

		// Token: 0x170024F8 RID: 9464
		// (get) Token: 0x060068A4 RID: 26788 RVA: 0x00196636 File Offset: 0x00194836
		// (set) Token: 0x060068A5 RID: 26789 RVA: 0x0019663E File Offset: 0x0019483E
		internal IntList CellExprHostIDs
		{
			get
			{
				return this.m_cellExprHostIDs;
			}
			set
			{
				this.m_cellExprHostIDs = value;
			}
		}

		// Token: 0x170024F9 RID: 9465
		// (get) Token: 0x060068A6 RID: 26790 RVA: 0x00196647 File Offset: 0x00194847
		// (set) Token: 0x060068A7 RID: 26791 RVA: 0x0019664F File Offset: 0x0019484F
		internal int ExpectedColumns
		{
			get
			{
				return this.m_expectedColumns;
			}
			set
			{
				this.m_expectedColumns = value;
			}
		}

		// Token: 0x170024FA RID: 9466
		// (get) Token: 0x060068A8 RID: 26792 RVA: 0x00196658 File Offset: 0x00194858
		// (set) Token: 0x060068A9 RID: 26793 RVA: 0x00196660 File Offset: 0x00194860
		internal int ExpectedRows
		{
			get
			{
				return this.m_expectedRows;
			}
			set
			{
				this.m_expectedRows = value;
			}
		}

		// Token: 0x170024FB RID: 9467
		// (get) Token: 0x060068AA RID: 26794 RVA: 0x00196669 File Offset: 0x00194869
		internal CustomReportItemHeadingList StaticColumns
		{
			get
			{
				if (!this.m_staticColumnsInitialized)
				{
					this.InitializeStaticGroups(false);
					this.m_staticColumnsInitialized = true;
				}
				return this.m_staticColumns;
			}
		}

		// Token: 0x170024FC RID: 9468
		// (get) Token: 0x060068AB RID: 26795 RVA: 0x00196687 File Offset: 0x00194887
		internal CustomReportItemHeadingList StaticRows
		{
			get
			{
				if (!this.m_staticRowsInitialized)
				{
					this.InitializeStaticGroups(true);
					this.m_staticRowsInitialized = true;
				}
				return this.m_staticRows;
			}
		}

		// Token: 0x170024FD RID: 9469
		// (get) Token: 0x060068AC RID: 26796 RVA: 0x001966A5 File Offset: 0x001948A5
		// (set) Token: 0x060068AD RID: 26797 RVA: 0x001966AD File Offset: 0x001948AD
		internal ReportItemCollection RenderReportItem
		{
			get
			{
				return this.m_renderReportItem;
			}
			set
			{
				this.m_renderReportItem = value;
			}
		}

		// Token: 0x170024FE RID: 9470
		// (get) Token: 0x060068AE RID: 26798 RVA: 0x001966B6 File Offset: 0x001948B6
		// (set) Token: 0x060068AF RID: 26799 RVA: 0x001966BE File Offset: 0x001948BE
		internal bool FirstInstanceOfRenderReportItem
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

		// Token: 0x170024FF RID: 9471
		// (get) Token: 0x060068B0 RID: 26800 RVA: 0x001966C7 File Offset: 0x001948C7
		// (set) Token: 0x060068B1 RID: 26801 RVA: 0x001966CF File Offset: 0x001948CF
		internal ReportProcessing.ProcessingContext ProcessingContext
		{
			get
			{
				return this.m_processingContext;
			}
			set
			{
				this.m_processingContext = value;
			}
		}

		// Token: 0x17002500 RID: 9472
		// (get) Token: 0x060068B2 RID: 26802 RVA: 0x001966D8 File Offset: 0x001948D8
		// (set) Token: 0x060068B3 RID: 26803 RVA: 0x001966E0 File Offset: 0x001948E0
		internal ObjectType CustomObjectType
		{
			get
			{
				return this.m_customObjectType;
			}
			set
			{
				this.m_customObjectType = value;
			}
		}

		// Token: 0x17002501 RID: 9473
		// (get) Token: 0x060068B4 RID: 26804 RVA: 0x001966E9 File Offset: 0x001948E9
		// (set) Token: 0x060068B5 RID: 26805 RVA: 0x001966F1 File Offset: 0x001948F1
		internal string CustomObjectName
		{
			get
			{
				return this.m_customObjectName;
			}
			set
			{
				this.m_customObjectName = value;
			}
		}

		// Token: 0x17002502 RID: 9474
		// (get) Token: 0x060068B6 RID: 26806 RVA: 0x001966FA File Offset: 0x001948FA
		protected override DataRegionExprHost DataRegionExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x060068B7 RID: 26807 RVA: 0x00196702 File Offset: 0x00194902
		RunningValueInfoList IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		// Token: 0x060068B8 RID: 26808 RVA: 0x0019670C File Offset: 0x0019490C
		void IRunningValueHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_cellRunningValues != null);
			if (this.m_cellRunningValues.Count == 0)
			{
				this.m_cellRunningValues = null;
			}
			Global.Tracer.Assert(this.m_runningValues != null);
			if (this.m_runningValues.Count == 0)
			{
				this.m_runningValues = null;
			}
		}

		// Token: 0x060068B9 RID: 26809 RVA: 0x00196768 File Offset: 0x00194968
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			context.ExprHostBuilder.CustomReportItemStart(this.m_name);
			if (this.m_dataSetName != null)
			{
				context.RegisterDataRegion(this);
			}
			base.Initialize(context);
			if (this.m_altReportItem != null)
			{
				if (this.m_altReportItem.Count == 0)
				{
					this.m_altReportItem = null;
				}
				else
				{
					context.RegisterReportItems(this.m_altReportItem);
					this.m_altReportItem.Initialize(context, false);
					context.UnRegisterReportItems(this.m_altReportItem);
				}
			}
			context.RegisterRunningValues(this.m_runningValues);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, true, false);
			}
			if (this.m_dataSetName != null)
			{
				this.CustomInitialize(context);
				context.UnRegisterDataRegion(this);
			}
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
			context.UnRegisterRunningValues(this.m_runningValues);
			base.ExprHostID = context.ExprHostBuilder.CustomReportItemEnd();
			return false;
		}

		// Token: 0x060068BA RID: 26810 RVA: 0x00196870 File Offset: 0x00194A70
		private void CustomInitialize(InitializationContext context)
		{
			context.Location = context.Location | LocationFlags.InDataSet | LocationFlags.InDataRegion;
			context.Location &= ~LocationFlags.InMatrixCellTopLevelItem;
			Global.Tracer.Assert(this.m_columns == null || this.m_expectedColumns > 0);
			Global.Tracer.Assert(this.m_rows == null || this.m_expectedRows > 0);
			if (this.ValidateRDLStructure(context) && this.ValidateProcessingRestrictions(context))
			{
				context.AggregateEscalateScopes = new StringList();
				context.AggregateEscalateScopes.Add(this.m_name);
				if (this.m_columns != null)
				{
					int num = 0;
					int num2 = 0;
					this.m_expectedColumns += this.m_columns.Initialize(0, this.m_dataRowCells, ref num, ref num2, context);
					base.ColumnCount = num2 + 1;
					if (1 == base.ColumnCount && this.m_columns[0].Static)
					{
						for (int i = 0; i < this.m_columns.Count; i++)
						{
							context.SpecialTransferRunningValues(this.m_columns[i].RunningValues);
						}
					}
				}
				if (this.m_rows != null)
				{
					int num3 = 0;
					int num4 = 0;
					this.m_expectedRows += this.m_rows.Initialize(0, this.m_dataRowCells, ref num3, ref num4, context);
					base.RowCount = num4 + 1;
					if (1 == base.RowCount && this.m_rows[0].Static)
					{
						for (int j = 0; j < this.m_rows.Count; j++)
						{
							context.SpecialTransferRunningValues(this.m_rows[j].RunningValues);
						}
					}
				}
				context.AggregateEscalateScopes = null;
				context.AggregateRewriteScopes = null;
				this.DataCellInitialize(context);
				this.CopyHeadingAggregates(this.m_rows);
				this.m_rows.TransferHeadingAggregates();
				this.CopyHeadingAggregates(this.m_columns);
				this.m_columns.TransferHeadingAggregates();
			}
		}

		// Token: 0x060068BB RID: 26811 RVA: 0x00196A6C File Offset: 0x00194C6C
		private void InitializeStaticGroups(bool isRows)
		{
			Global.Tracer.Assert(this.m_rows != null && this.m_columns != null);
			CustomReportItemHeadingList customReportItemHeadingList = (isRows ? this.m_rows : this.m_columns);
			int num = 0;
			while (customReportItemHeadingList != null)
			{
				num++;
				if (customReportItemHeadingList[0].Static)
				{
					if (isRows)
					{
						this.m_staticRows = customReportItemHeadingList;
					}
					else
					{
						this.m_staticColumns = customReportItemHeadingList;
					}
					Global.Tracer.Assert(customReportItemHeadingList.InnerHeadings() == null);
					return;
				}
				customReportItemHeadingList = (CustomReportItemHeadingList)customReportItemHeadingList.InnerHeadings();
			}
		}

		// Token: 0x060068BC RID: 26812 RVA: 0x00196AF8 File Offset: 0x00194CF8
		private bool ValidateProcessingRestrictions(InitializationContext context)
		{
			if (this.m_expectedColumns * this.m_expectedRows == 0)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsMissingDataGroupings, Severity.Error, context.ObjectType, context.ObjectName, (this.m_expectedColumns == 0) ? "DataColumnGroupings" : "DataRowGroupings", Array.Empty<string>());
				return false;
			}
			if (this.m_dataRowCells == null || this.m_dataRowCells.Count == 0)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsMissingDataCells, Severity.Error, context.ObjectType, context.ObjectName, "DataRows", Array.Empty<string>());
				return false;
			}
			Global.Tracer.Assert(this.m_rows != null && this.m_columns != null);
			return CustomReportItemHeading.ValidateProcessingRestrictions(this.m_rows, false, false, context) && CustomReportItemHeading.ValidateProcessingRestrictions(this.m_columns, true, false, context);
		}

		// Token: 0x060068BD RID: 26813 RVA: 0x00196BD0 File Offset: 0x00194DD0
		private bool ValidateRDLStructure(InitializationContext context)
		{
			if (this.m_dataRowCells == null || this.m_dataRowCells.Count == 0)
			{
				if (this.m_expectedColumns * this.m_expectedRows != 0)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfDataRows, Severity.Error, context.ObjectType, context.ObjectName, this.m_expectedRows.ToString(CultureInfo.InvariantCulture.NumberFormat), Array.Empty<string>());
					return false;
				}
			}
			else
			{
				if (this.m_expectedRows == 0)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsMissingDataGrouping, Severity.Error, context.ObjectType, context.ObjectName, "DataRowGroupings", Array.Empty<string>());
				}
				if (this.m_expectedColumns == 0)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsMissingDataGrouping, Severity.Error, context.ObjectType, context.ObjectName, "DataColumnGroupings", Array.Empty<string>());
				}
				if (this.m_expectedRows * this.m_expectedColumns == 0)
				{
					return false;
				}
				int count = this.m_dataRowCells.Count;
				if (this.m_expectedRows != count)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfDataRows, Severity.Error, context.ObjectType, context.ObjectName, this.m_expectedRows.ToString(CultureInfo.InvariantCulture.NumberFormat), Array.Empty<string>());
					return false;
				}
				bool flag = false;
				int num = 0;
				while (num < count && !flag)
				{
					Global.Tracer.Assert(this.m_dataRowCells[num] != null);
					if (this.m_dataRowCells[num].Count != this.m_expectedColumns)
					{
						flag = true;
					}
					num++;
				}
				if (flag)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfDataCellsInDataRow, Severity.Error, context.ObjectType, context.ObjectName, this.m_expectedColumns.ToString(CultureInfo.InvariantCulture.NumberFormat), Array.Empty<string>());
					return false;
				}
			}
			return true;
		}

		// Token: 0x060068BE RID: 26814 RVA: 0x00196D90 File Offset: 0x00194F90
		private void DataCellInitialize(InitializationContext context)
		{
			context.Location |= LocationFlags.InMatrixCell;
			context.MatrixName = this.m_name;
			context.RegisterTablixCellScope(1 == this.m_expectedColumns && this.m_columns[0].Grouping == null, this.m_cellAggregates, this.m_cellPostSortAggregates);
			Global.Tracer.Assert(this.m_expectedColumns * this.m_expectedRows != 0);
			this.m_cellExprHostIDs = new IntList(this.m_expectedColumns * this.m_expectedRows);
			context.RegisterRunningValues(this.m_cellRunningValues);
			this.SetupRowScopesAndInitialize(this.m_rows, 0, context);
			context.UnRegisterRunningValues(this.m_cellRunningValues);
			if (context.IsRunningValueDirectionColumn())
			{
				this.m_processingInnerGrouping = Pivot.ProcessingInnerGroupings.Row;
			}
			context.UnRegisterTablixCellScope();
		}

		// Token: 0x060068BF RID: 26815 RVA: 0x00196E60 File Offset: 0x00195060
		private void SetupRowScopesAndInitialize(CustomReportItemHeadingList rowHeadings, int cellRowIndex, InitializationContext context)
		{
			Global.Tracer.Assert(rowHeadings != null);
			int count = rowHeadings.Count;
			for (int i = 0; i < count; i++)
			{
				CustomReportItemHeading customReportItemHeading = rowHeadings[i];
				if (customReportItemHeading.Grouping != null)
				{
					context.Location |= LocationFlags.InGrouping;
					context.RegisterGroupingScopeForTablixCell(customReportItemHeading.Grouping.Name, false, customReportItemHeading.Grouping.SimpleGroupExpressions, customReportItemHeading.Aggregates, customReportItemHeading.PostSortAggregates, customReportItemHeading.RecursiveAggregates, customReportItemHeading.Grouping);
				}
				if (customReportItemHeading.InnerHeadings != null)
				{
					this.SetupRowScopesAndInitialize(customReportItemHeading.InnerHeadings, cellRowIndex, context);
				}
				else
				{
					Global.Tracer.Assert(this.m_dataRowCells != null && cellRowIndex < this.m_dataRowCells.Count);
					int num = 0;
					this.SetupColumnScopesAndInitialize(this.m_columns, this.m_dataRowCells[cellRowIndex], ref num, context);
					Global.Tracer.Assert(num == this.m_dataRowCells[cellRowIndex].Count);
					cellRowIndex++;
				}
				if (customReportItemHeading.Grouping != null)
				{
					context.UnRegisterGroupingScopeForTablixCell(customReportItemHeading.Grouping.Name, false);
				}
			}
		}

		// Token: 0x060068C0 RID: 26816 RVA: 0x00196F80 File Offset: 0x00195180
		private void SetupColumnScopesAndInitialize(CustomReportItemHeadingList columnHeadings, DataCellList cellList, ref int cellIndex, InitializationContext context)
		{
			Global.Tracer.Assert(columnHeadings != null);
			int count = columnHeadings.Count;
			for (int i = 0; i < count; i++)
			{
				CustomReportItemHeading customReportItemHeading = columnHeadings[i];
				if (customReportItemHeading.Grouping != null)
				{
					context.Location |= LocationFlags.InGrouping;
					context.RegisterGroupingScopeForTablixCell(customReportItemHeading.Grouping.Name, true, customReportItemHeading.Grouping.SimpleGroupExpressions, customReportItemHeading.Aggregates, customReportItemHeading.PostSortAggregates, customReportItemHeading.RecursiveAggregates, customReportItemHeading.Grouping);
				}
				if (customReportItemHeading.InnerHeadings != null)
				{
					this.SetupColumnScopesAndInitialize(customReportItemHeading.InnerHeadings, cellList, ref cellIndex, context);
				}
				else
				{
					context.ExprHostBuilder.DataCellStart();
					cellList[cellIndex].Initialize(null, context);
					this.m_cellExprHostIDs.Add(context.ExprHostBuilder.DataCellEnd());
					cellIndex++;
				}
				if (customReportItemHeading.Grouping != null)
				{
					context.UnRegisterGroupingScopeForTablixCell(customReportItemHeading.Grouping.Name, true);
				}
			}
		}

		// Token: 0x060068C1 RID: 26817 RVA: 0x00197080 File Offset: 0x00195280
		internal void CopyHeadingAggregates(CustomReportItemHeadingList headings)
		{
			if (headings != null)
			{
				int count = headings.Count;
				for (int i = 0; i < count; i++)
				{
					CustomReportItemHeading customReportItemHeading = headings[i];
					customReportItemHeading.CopySubHeadingAggregates();
					Tablix.CopyAggregates(customReportItemHeading.Aggregates, this.m_aggregates);
					Tablix.CopyAggregates(customReportItemHeading.PostSortAggregates, this.m_postSortAggregates);
					Tablix.CopyAggregates(customReportItemHeading.RecursiveAggregates, this.m_aggregates);
				}
			}
		}

		// Token: 0x060068C2 RID: 26818 RVA: 0x001970E4 File Offset: 0x001952E4
		internal override int GetDynamicHeadingCount(bool outerGroupings)
		{
			int num;
			if ((outerGroupings && this.m_processingInnerGrouping == Pivot.ProcessingInnerGroupings.Column) || (!outerGroupings && this.m_processingInnerGrouping == Pivot.ProcessingInnerGroupings.Row))
			{
				num = base.RowCount;
				if (this.StaticRows != null)
				{
					num--;
				}
			}
			else
			{
				num = base.ColumnCount;
				if (this.StaticColumns != null)
				{
					num--;
				}
			}
			return num;
		}

		// Token: 0x060068C3 RID: 26819 RVA: 0x00197131 File Offset: 0x00195331
		internal override TablixHeadingList SkipStatics(TablixHeadingList headings)
		{
			Global.Tracer.Assert(headings != null && 1 <= headings.Count && headings is CustomReportItemHeadingList);
			if (((CustomReportItemHeadingList)headings)[0].Static)
			{
				return null;
			}
			return headings;
		}

		// Token: 0x060068C4 RID: 26820 RVA: 0x0019716C File Offset: 0x0019536C
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.CustomReportItemHostsRemotable[base.ExprHostID];
				base.DataRegionSetExprHost(this.m_exprHost, reportObjectModel);
				if (this.m_exprHost.DataCellHostsRemotable != null)
				{
					IList<DataCellExprHost> dataCellHostsRemotable = this.m_exprHost.DataCellHostsRemotable;
					int num = 0;
					Global.Tracer.Assert(this.m_dataRowCells.Count <= this.m_cellExprHostIDs.Count);
					for (int i = 0; i < this.m_dataRowCells.Count; i++)
					{
						DataCellList dataCellList = this.m_dataRowCells[i];
						Global.Tracer.Assert(dataCellList != null);
						for (int j = 0; j < dataCellList.Count; j++)
						{
							DataValueList dataValueList = dataCellList[j];
							Global.Tracer.Assert(dataValueList != null);
							int num2 = this.m_cellExprHostIDs[num++];
							if (num2 >= 0)
							{
								dataValueList.SetExprHost(dataCellHostsRemotable[num2].DataValueHostsRemotable, reportObjectModel);
							}
						}
					}
				}
			}
		}

		// Token: 0x060068C5 RID: 26821 RVA: 0x00197290 File Offset: 0x00195490
		internal override Hashtable GetOuterScopeNames(int dynamicLevel)
		{
			Hashtable hashtable = new Hashtable();
			CustomReportItemHeadingList customReportItemHeadingList = (CustomReportItemHeadingList)base.GetOuterHeading();
			int num = 0;
			while (num <= dynamicLevel && customReportItemHeadingList != null)
			{
				if (customReportItemHeadingList[0].Grouping != null)
				{
					hashtable.Add(customReportItemHeadingList[0].Grouping.Name, customReportItemHeadingList[0].Grouping);
					num++;
				}
				customReportItemHeadingList = (CustomReportItemHeadingList)customReportItemHeadingList.InnerHeadings();
			}
			return hashtable;
		}

		// Token: 0x060068C6 RID: 26822 RVA: 0x001972FC File Offset: 0x001954FC
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.Tablix, new MemberInfoList
			{
				new MemberInfo(MemberName.Type, Token.String),
				new MemberInfo(MemberName.ReportItemColDef, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemCollection),
				new MemberInfo(MemberName.Columns, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.CustomReportItemHeadingList),
				new MemberInfo(MemberName.Rows, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.CustomReportItemHeadingList),
				new MemberInfo(MemberName.DataRowCells, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataCellsList),
				new MemberInfo(MemberName.CellRunningValues, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.RunningValueInfoList),
				new MemberInfo(MemberName.CellExprHostIDs, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.IntList),
				new MemberInfo(MemberName.RenderReportItemColDef, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemCollection)
			});
		}

		// Token: 0x060068C7 RID: 26823 RVA: 0x001973B8 File Offset: 0x001955B8
		internal void CustomProcessingInitialize(CustomReportItemInstance instance, CustomReportItemInstanceInfo instanceInfo, ReportProcessing.ProcessingContext context, int repeatedSiblingIndex)
		{
			this.m_criInstance = instance;
			this.m_criInstanceInfo = instanceInfo;
			this.m_processingContext = context;
			this.m_repeatedSiblingIndex = repeatedSiblingIndex;
			this.m_customObjectType = ObjectType.CustomReportItem;
			this.m_customObjectName = this.m_name;
			this.m_customPropertyName = null;
		}

		// Token: 0x060068C8 RID: 26824 RVA: 0x001973F2 File Offset: 0x001955F2
		internal void CustomProcessingReset()
		{
			this.m_criInstance = null;
			this.m_criInstanceInfo = null;
			this.m_processingContext = null;
			this.m_repeatedSiblingIndex = -1;
			this.m_customObjectType = ObjectType.CustomReportItem;
			this.m_customObjectName = this.m_name;
			this.m_customPropertyName = null;
		}

		// Token: 0x060068C9 RID: 26825 RVA: 0x0019742C File Offset: 0x0019562C
		internal void DeconstructRenderItem(Microsoft.ReportingServices.ReportRendering.ReportItem renderItem, CustomReportItemInstance criInstance)
		{
			if (this.FirstInstanceOfRenderReportItem)
			{
				this.m_renderReportItem = null;
				this.FirstInstanceOfRenderReportItem = false;
			}
			if (renderItem != null)
			{
				bool flag = this.m_renderReportItem == null;
				if (flag)
				{
					this.m_renderReportItem = new ReportItemCollection();
					this.m_renderReportItem.ID = this.m_processingContext.CreateIDForSubreport();
				}
				else
				{
					Global.Tracer.Assert(1 == this.m_renderReportItem.Count);
					if (renderItem.Name != this.m_customTopLevelRenderItemName)
					{
						this.m_processingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIRenderItemDefinitionName, Severity.Error, ObjectType.CustomReportItem, renderItem.Name, this.m_type, new string[] { this.m_name, this.m_customTopLevelRenderItemName });
						throw new ReportProcessingException(this.m_processingContext.ErrorContext.Messages);
					}
				}
				if (renderItem is Image)
				{
					Image image = renderItem as Image;
					this.m_customObjectType = ObjectType.Image;
					this.m_customObjectName = image.Name;
					Image image2;
					if (this.m_renderReportItem.Count == 0)
					{
						this.m_customTopLevelRenderItemName = image.Name;
						image2 = this.DeconstructImageDefinition(image, true);
						this.m_renderReportItem.AddCustomRenderItem(image2);
					}
					else
					{
						image2 = this.m_renderReportItem[0] as Image;
						if (image2 == null)
						{
							this.m_processingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIRenderItemInstanceType, Severity.Error, this.m_renderReportItem[0].ObjectType, this.m_renderReportItem[0].Name, this.m_type, new string[]
							{
								this.m_name,
								ErrorContext.GetLocalizedObjectTypeString(this.m_customObjectType)
							});
							throw new ReportProcessingException(this.m_processingContext.ErrorContext.Messages);
						}
					}
					ImageInstance imageInstance = new ImageInstance(this.m_processingContext, image2, this.m_repeatedSiblingIndex, true);
					ImageInstanceInfo instanceInfo = imageInstance.InstanceInfo;
					this.DeconstructImageInstance(image2, imageInstance, instanceInfo, image, flag, true, this);
					criInstance.AltReportItemColInstance = new ReportItemColInstance(this.m_processingContext, this.m_renderReportItem);
					criInstance.AltReportItemColInstance.Add(imageInstance);
				}
				return;
			}
			if (this.FirstInstanceOfRenderReportItem)
			{
				this.m_renderReportItem = null;
				this.m_processingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIRenderItemNull, Severity.Error, this.m_customObjectType, this.m_customObjectName, this.m_type, Array.Empty<string>());
				throw new ReportProcessingException(this.m_processingContext.ErrorContext.Messages);
			}
			this.m_renderReportItem = null;
			this.m_processingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIRenderInstanceNull, Severity.Error, this.m_customObjectType, this.m_customObjectName, this.m_type, Array.Empty<string>());
			throw new ReportProcessingException(this.m_processingContext.ErrorContext.Messages);
		}

		// Token: 0x060068CA RID: 26826 RVA: 0x001976D0 File Offset: 0x001958D0
		private void DeconstructImageInstance(Image image, ImageInstance imageInstance, ImageInstanceInfo imageInstanceInfo, Image renderImage, bool isfirstInstance, bool isRootItem, IErrorContext errorContext)
		{
			this.DeconstructReportItemInstance(image, imageInstance, imageInstanceInfo, renderImage, isRootItem);
			byte[] imageData = renderImage.Processing.m_imageData;
			string mimeType = renderImage.Processing.m_mimeType;
			if (!Validator.ValidateMimeType(mimeType))
			{
				this.m_customPropertyName = "MIMEType";
				if (mimeType == null)
				{
					errorContext.Register(ProcessingErrorCode.rsMissingMIMEType, Severity.Warning, Array.Empty<string>());
				}
				else
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidMIMEType, Severity.Warning, new string[] { mimeType });
				}
				imageInstanceInfo.BrokenImage = true;
			}
			if (imageData == null)
			{
				imageInstanceInfo.BrokenImage = true;
			}
			else if (this.m_processingContext.InPageSection && !this.m_processingContext.CreatePageSectionImageChunks)
			{
				imageInstanceInfo.Data = new ImageData(imageData, mimeType);
			}
			else if (this.m_processingContext.CreateReportChunkCallback != null && imageData.Length != 0)
			{
				string text = Guid.NewGuid().ToString();
				using (Stream stream = this.m_processingContext.CreateReportChunkCallback(text, ReportProcessing.ReportChunkTypes.Image, mimeType))
				{
					stream.Write(imageData, 0, imageData.Length);
				}
				this.m_processingContext.ImageStreamNames[text] = new ImageInfo(text, mimeType);
				imageInstanceInfo.ImageValue = text;
			}
			else
			{
				imageInstanceInfo.BrokenImage = true;
			}
			Global.Tracer.Assert(image.Action == null || renderImage.ActionInfo != null);
			if (renderImage.ActionInfo != null)
			{
				Microsoft.ReportingServices.ReportProcessing.Action action = image.Action;
				ActionInstance actionInstance;
				renderImage.ActionInfo.Deconstruct(imageInstance.UniqueName, ref action, out actionInstance, this);
				image.Action = action;
				imageInstanceInfo.Action = actionInstance;
			}
			Style styleClass = image.StyleClass;
			object[] array;
			CustomReportItem.DeconstructRenderStyle(isfirstInstance, renderImage.Processing.SharedStyles, renderImage.Processing.NonSharedStyles, ref styleClass, out array, this);
			image.StyleClass = styleClass;
			imageInstanceInfo.StyleAttributeValues = array;
			if (renderImage.ImageMap != null)
			{
				imageInstanceInfo.ImageMapAreas = renderImage.ImageMap.Deconstruct(this.m_processingContext, this);
			}
		}

		// Token: 0x060068CB RID: 26827 RVA: 0x001978CC File Offset: 0x00195ACC
		private Image DeconstructImageDefinition(Image renderItem, bool isRootItem)
		{
			Image image = new Image(this.m_processingContext.CreateIDForSubreport(), this.m_parent);
			this.DeconstructReportItemDefinition(image, renderItem, isRootItem);
			image.Source = Image.SourceType.External;
			image.Value = new ExpressionInfo(ExpressionInfo.Types.Expression);
			image.MIMEType = new ExpressionInfo(ExpressionInfo.Types.Expression);
			image.Sizing = (Image.Sizings)renderItem.Sizing;
			return image;
		}

		// Token: 0x060068CC RID: 26828 RVA: 0x00197928 File Offset: 0x00195B28
		private void SetLabel(string label, Microsoft.ReportingServices.ReportProcessing.ReportItem definition, ReportItemInstance instance, ReportItemInstanceInfo instanceInfo)
		{
			if (label == null)
			{
				return;
			}
			Global.Tracer.Assert(instance != null && instanceInfo != null);
			instanceInfo.Label = label;
			this.m_processingContext.NavigationInfo.AddToDocumentMap(instance.GetDocumentMapUniqueName(), false, definition.StartPage, label);
		}

		// Token: 0x060068CD RID: 26829 RVA: 0x00197974 File Offset: 0x00195B74
		private void SetBookmark(string bookmark, Microsoft.ReportingServices.ReportProcessing.ReportItem definition, ReportItemInstance instance, ReportItemInstanceInfo instanceInfo)
		{
			if (bookmark == null)
			{
				return;
			}
			Global.Tracer.Assert(instance != null && instanceInfo != null);
			this.m_processingContext.NavigationInfo.ProcessBookmark(definition, instance, instanceInfo, bookmark);
		}

		// Token: 0x060068CE RID: 26830 RVA: 0x001979A4 File Offset: 0x00195BA4
		private void DeconstructReportItemInstance(Microsoft.ReportingServices.ReportProcessing.ReportItem definition, ReportItemInstance instance, ReportItemInstanceInfo instanceInfo, Microsoft.ReportingServices.ReportRendering.ReportItem renderItem, bool isRootItem)
		{
			Global.Tracer.Assert(definition != null && instanceInfo != null && renderItem != null && renderItem.Processing != null);
			instanceInfo.ToolTip = renderItem.Processing.Tooltip;
			definition.StartPage = this.m_criInstance.ReportItemDef.StartPage;
			if (!isRootItem)
			{
				this.SetLabel(renderItem.Processing.Label, definition, instance, instanceInfo);
			}
			else
			{
				string text = null;
				if (base.Label != null)
				{
					if (base.Label.Type == ExpressionInfo.Types.Expression)
					{
						text = this.m_criInstanceInfo.Label;
					}
					else
					{
						text = base.Label.Value;
					}
				}
				if (text == null)
				{
					text = renderItem.Processing.Label;
				}
				this.SetLabel(text, definition, instance, instanceInfo);
			}
			if (!isRootItem)
			{
				this.SetBookmark(renderItem.Processing.Bookmark, definition, instance, instanceInfo);
			}
			else
			{
				string text2 = null;
				if (base.Bookmark != null)
				{
					if (base.Bookmark.Type == ExpressionInfo.Types.Expression)
					{
						text2 = this.m_criInstanceInfo.Bookmark;
					}
					else
					{
						text2 = base.Bookmark.Value;
					}
				}
				if (text2 == null)
				{
					text2 = renderItem.Processing.Bookmark;
				}
				this.SetBookmark(text2, definition, instance, instanceInfo);
			}
			if (definition.CustomProperties == null && renderItem.CustomProperties != null)
			{
				Global.Tracer.Assert(0 < renderItem.CustomProperties.Count);
				this.m_processingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIRenderItemProperties, Severity.Error, this.m_customObjectType, this.m_customObjectName, this.m_type, new string[]
				{
					base.Name,
					"CustomProperties",
					"0",
					renderItem.CustomProperties.Count.ToString(CultureInfo.InvariantCulture)
				});
				throw new ReportProcessingException(this.m_processingContext.ErrorContext.Messages);
			}
			if (definition.CustomProperties != null && (renderItem.CustomProperties == null || definition.CustomProperties.Count != renderItem.CustomProperties.Count))
			{
				int num = ((renderItem.CustomProperties == null) ? 0 : renderItem.CustomProperties.Count);
				this.m_processingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIRenderItemProperties, Severity.Error, this.m_customObjectType, this.m_customObjectName, this.m_type, new string[]
				{
					base.Name,
					"CustomProperties",
					definition.CustomProperties.Count.ToString(CultureInfo.InvariantCulture),
					num.ToString(CultureInfo.InvariantCulture)
				});
				throw new ReportProcessingException(this.m_processingContext.ErrorContext.Messages);
			}
			if (renderItem.CustomProperties != null)
			{
				int count = renderItem.CustomProperties.Count;
				Global.Tracer.Assert(definition.CustomProperties != null && count == renderItem.CustomProperties.Count);
				instanceInfo.CustomPropertyInstances = renderItem.CustomProperties.Deconstruct();
			}
			if (isRootItem)
			{
				instanceInfo.StartHidden = base.StartHidden;
			}
		}

		// Token: 0x060068CF RID: 26831 RVA: 0x00197C98 File Offset: 0x00195E98
		private void DeconstructReportItemDefinition(Microsoft.ReportingServices.ReportProcessing.ReportItem definition, Microsoft.ReportingServices.ReportRendering.ReportItem renderItem, bool isRootItem)
		{
			Global.Tracer.Assert(definition != null && renderItem != null);
			definition.Name = base.Name + "." + renderItem.Name;
			definition.DataElementName = definition.Name;
			definition.ZIndex = renderItem.ZIndex;
			definition.ToolTip = new ExpressionInfo(ExpressionInfo.Types.Expression);
			definition.Label = new ExpressionInfo(ExpressionInfo.Types.Expression);
			definition.Bookmark = new ExpressionInfo(ExpressionInfo.Types.Expression);
			definition.IsFullSize = false;
			definition.RepeatedSibling = false;
			definition.Computed = true;
			if (renderItem.CustomProperties != null)
			{
				int count = renderItem.CustomProperties.Count;
				definition.CustomProperties = new DataValueList(count);
				for (int i = 0; i < count; i++)
				{
					DataValue dataValue = new DataValue();
					dataValue.Name = new ExpressionInfo(ExpressionInfo.Types.Expression);
					dataValue.Value = new ExpressionInfo(ExpressionInfo.Types.Expression);
					definition.CustomProperties.Add(dataValue);
				}
			}
			if (!isRootItem)
			{
				definition.Top = renderItem.Top.ToString();
				definition.TopValue = renderItem.Top.ToMillimeters();
				definition.Left = renderItem.Left.ToString();
				definition.LeftValue = renderItem.Left.ToMillimeters();
				definition.Height = renderItem.Height.ToString();
				definition.HeightValue = renderItem.Height.ToMillimeters();
				definition.Width = renderItem.Width.ToString();
				definition.WidthValue = renderItem.Width.ToMillimeters();
				return;
			}
			this.OverrideDefinitionSettings(definition);
		}

		// Token: 0x060068D0 RID: 26832 RVA: 0x00197E14 File Offset: 0x00196014
		internal static void DeconstructRenderStyle(bool firstStyleInstance, DataValueInstanceList sharedStyles, DataValueInstanceList nonSharedStyles, ref Style style, out object[] styleAttributeValues, CustomReportItem context)
		{
			styleAttributeValues = null;
			int num = ((sharedStyles == null) ? 0 : sharedStyles.Count);
			int num2 = ((nonSharedStyles == null) ? 0 : nonSharedStyles.Count);
			int num3 = num + num2;
			if (style != null && num3 == 0)
			{
				Global.Tracer.Assert(style.StyleAttributes != null && 0 <= style.StyleAttributes.Count - style.CustomSharedStyleCount);
				styleAttributeValues = new object[style.StyleAttributes.Count - style.CustomSharedStyleCount];
				return;
			}
			if (style == null && !firstStyleInstance && 0 < num3)
			{
				context.ProcessingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIRenderItemProperties, Severity.Error, context.CustomObjectType, context.CustomObjectName, context.Type, new string[]
				{
					context.Name,
					"Styles",
					"0",
					num3.ToString(CultureInfo.InvariantCulture)
				});
				throw new ReportProcessingException(context.ProcessingContext.ErrorContext.Messages);
			}
			if (firstStyleInstance)
			{
				if (0 < num3)
				{
					style = new Style(ConstructionPhase.Deserializing);
					style.CustomSharedStyleCount = num;
					style.StyleAttributes = new StyleAttributeHashtable(num3);
					if (num2 > 0)
					{
						style.ExpressionList = new ExpressionInfoList(num2);
						styleAttributeValues = new object[num2];
					}
					for (int i = 0; i < num; i++)
					{
						string name = sharedStyles[i].Name;
						Global.Tracer.Assert(!style.StyleAttributes.ContainsKey(name));
						context.m_customPropertyName = name;
						object obj = ProcessingValidator.ValidateCustomStyle(name, sharedStyles[i].Value, context);
						AttributeInfo attributeInfo = new AttributeInfo();
						attributeInfo.IsExpression = false;
						if (obj != null)
						{
							if ("NumeralVariant" == name)
							{
								attributeInfo.IntValue = (int)obj;
							}
							else
							{
								attributeInfo.Value = (string)obj;
							}
						}
						style.StyleAttributes.Add(name, attributeInfo);
					}
					for (int j = 0; j < num2; j++)
					{
						string name2 = nonSharedStyles[j].Name;
						if (style.StyleAttributes.ContainsKey(name2))
						{
							context.ProcessingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIRenderItemDuplicateStyle, Severity.Error, context.CustomObjectType, context.CustomObjectName, context.Type, new string[] { context.Name, name2 });
							throw new ReportProcessingException(context.ProcessingContext.ErrorContext.Messages);
						}
						context.m_customPropertyName = name2;
						object obj2 = ProcessingValidator.ValidateCustomStyle(name2, nonSharedStyles[j].Value, context);
						int count = style.ExpressionList.Count;
						ExpressionInfo expressionInfo = new ExpressionInfo(ExpressionInfo.Types.Expression);
						style.ExpressionList.Add(expressionInfo);
						AttributeInfo attributeInfo2 = new AttributeInfo();
						attributeInfo2.IsExpression = true;
						attributeInfo2.IntValue = count;
						style.StyleAttributes.Add(name2, attributeInfo2);
						styleAttributeValues[j] = obj2;
					}
					return;
				}
			}
			else
			{
				if (sharedStyles != null && sharedStyles.Count != style.CustomSharedStyleCount)
				{
					context.ProcessingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIRenderItemProperties, Severity.Error, context.CustomObjectType, context.CustomObjectName, context.Type, new string[]
					{
						context.Name,
						"SharedStyles",
						style.CustomSharedStyleCount.ToString(CultureInfo.InvariantCulture),
						sharedStyles.Count.ToString(CultureInfo.InvariantCulture)
					});
					throw new ReportProcessingException(context.ProcessingContext.ErrorContext.Messages);
				}
				for (int k = 0; k < num2; k++)
				{
					if (k == 0)
					{
						styleAttributeValues = new object[style.StyleAttributes.Count - style.CustomSharedStyleCount];
					}
					string name3 = nonSharedStyles[k].Name;
					context.m_customPropertyName = name3;
					object obj3 = ProcessingValidator.ValidateCustomStyle(name3, nonSharedStyles[k].Value, context);
					AttributeInfo attributeInfo3 = style.StyleAttributes[name3];
					if (attributeInfo3 == null)
					{
						context.ProcessingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIRenderItemInvalidStyle, Severity.Error, context.CustomObjectType, context.CustomObjectName, context.Type, new string[] { context.Name, name3 });
						throw new ReportProcessingException(context.ProcessingContext.ErrorContext.Messages);
					}
					if (!attributeInfo3.IsExpression)
					{
						context.ProcessingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIRenderItemInvalidStyleType, Severity.Error, context.CustomObjectType, context.CustomObjectName, context.Type, new string[] { context.Name, name3 });
						throw new ReportProcessingException(context.ProcessingContext.ErrorContext.Messages);
					}
					int intValue = attributeInfo3.IntValue;
					Global.Tracer.Assert(0 <= intValue && intValue < styleAttributeValues.Length);
					styleAttributeValues[intValue] = obj3;
				}
			}
		}

		// Token: 0x060068D1 RID: 26833 RVA: 0x0019830C File Offset: 0x0019650C
		private void OverrideDefinitionSettings(Microsoft.ReportingServices.ReportProcessing.ReportItem target)
		{
			Global.Tracer.Assert(target != null && target.Name != null);
			target.Top = this.m_top;
			target.TopValue = this.m_topValue;
			target.Left = this.m_left;
			target.LeftValue = this.m_leftValue;
			target.Height = this.m_height;
			target.HeightValue = this.m_heightValue;
			target.Width = this.m_width;
			target.WidthValue = this.m_widthValue;
			target.Visibility = this.m_visibility;
			target.IsFullSize = this.m_isFullSize;
			target.DataElementOutput = this.m_dataElementOutput;
			target.DistanceBeforeTop = this.m_distanceBeforeTop;
			target.DistanceFromReportTop = this.m_distanceFromReportTop;
			target.SiblingAboveMe = this.m_siblingAboveMe;
		}

		// Token: 0x060068D2 RID: 26834 RVA: 0x001983DC File Offset: 0x001965DC
		internal static bool CloneObject(object o, out object clone)
		{
			clone = null;
			if (o == null || DBNull.Value == o)
			{
				return true;
			}
			if (o is string)
			{
				clone = string.Copy(o as string);
			}
			else if (o is char)
			{
				clone = '\0';
				clone = (char)o;
			}
			else if (o is bool)
			{
				clone = ((bool)o).CompareTo(true);
			}
			else if (o is short)
			{
				clone = 0;
				clone = (short)o;
			}
			else if (o is int)
			{
				clone = 0;
				clone = (int)o;
			}
			else if (o is long)
			{
				clone = 0L;
				clone = (long)o;
			}
			else if (o is ushort)
			{
				clone = 0;
				clone = (ushort)o;
			}
			else if (o is uint)
			{
				clone = 0U;
				clone = (uint)o;
			}
			else if (o is ulong)
			{
				clone = 0UL;
				clone = (ulong)o;
			}
			else if (o is byte)
			{
				clone = 0;
				clone = (byte)o;
			}
			else if (o is sbyte)
			{
				clone = 0;
				clone = (sbyte)o;
			}
			else if (o is TimeSpan)
			{
				clone = new TimeSpan(((TimeSpan)o).Ticks);
			}
			else if (o is DateTime)
			{
				clone = new DateTime(((DateTime)o).Ticks);
			}
			else if (o is float)
			{
				clone = 0f;
				clone = (float)o;
			}
			else if (o is double)
			{
				clone = 0.0;
				clone = (double)o;
			}
			else if (o is decimal)
			{
				clone = 0m;
				clone = (decimal)o;
			}
			return clone != null;
		}

		// Token: 0x060068D3 RID: 26835 RVA: 0x00198625 File Offset: 0x00196825
		void IErrorContext.Register(ProcessingErrorCode code, Severity severity, params string[] arguments)
		{
			Global.Tracer.Assert(this.m_processingContext != null, "An unexpected error happened during deconstructing a custom report item");
			this.m_processingContext.ErrorContext.Register(code, severity, this.m_customObjectType, this.m_customObjectName, this.m_customPropertyName, arguments);
		}

		// Token: 0x060068D4 RID: 26836 RVA: 0x00198665 File Offset: 0x00196865
		void IErrorContext.Register(ProcessingErrorCode code, Severity severity, ObjectType objectType, string objectName, string propertyName, params string[] arguments)
		{
			Global.Tracer.Assert(this.m_processingContext != null, "An unexpected error happened during deconstructing a custom report item");
			this.m_processingContext.ErrorContext.Register(code, severity, objectType, objectName, propertyName, arguments);
		}

		// Token: 0x04003399 RID: 13209
		private string m_type;

		// Token: 0x0400339A RID: 13210
		private ReportItemCollection m_altReportItem;

		// Token: 0x0400339B RID: 13211
		private CustomReportItemHeadingList m_columns;

		// Token: 0x0400339C RID: 13212
		private CustomReportItemHeadingList m_rows;

		// Token: 0x0400339D RID: 13213
		private DataCellsList m_dataRowCells;

		// Token: 0x0400339E RID: 13214
		private RunningValueInfoList m_cellRunningValues;

		// Token: 0x0400339F RID: 13215
		private IntList m_cellExprHostIDs;

		// Token: 0x040033A0 RID: 13216
		private ReportItemCollection m_renderReportItem;

		// Token: 0x040033A1 RID: 13217
		[NonSerialized]
		private int m_expectedColumns;

		// Token: 0x040033A2 RID: 13218
		[NonSerialized]
		private int m_expectedRows;

		// Token: 0x040033A3 RID: 13219
		[NonSerialized]
		private CustomReportItemExprHost m_exprHost;

		// Token: 0x040033A4 RID: 13220
		[NonSerialized]
		private CustomReportItemHeadingList m_staticColumns;

		// Token: 0x040033A5 RID: 13221
		[NonSerialized]
		private bool m_staticColumnsInitialized;

		// Token: 0x040033A6 RID: 13222
		[NonSerialized]
		private CustomReportItemHeadingList m_staticRows;

		// Token: 0x040033A7 RID: 13223
		[NonSerialized]
		private bool m_staticRowsInitialized;

		// Token: 0x040033A8 RID: 13224
		[NonSerialized]
		private CustomReportItemInstance m_criInstance;

		// Token: 0x040033A9 RID: 13225
		[NonSerialized]
		private CustomReportItemInstanceInfo m_criInstanceInfo;

		// Token: 0x040033AA RID: 13226
		[NonSerialized]
		private ReportProcessing.ProcessingContext m_processingContext;

		// Token: 0x040033AB RID: 13227
		[NonSerialized]
		private int m_repeatedSiblingIndex = -1;

		// Token: 0x040033AC RID: 13228
		[NonSerialized]
		private ObjectType m_customObjectType;

		// Token: 0x040033AD RID: 13229
		[NonSerialized]
		private string m_customObjectName;

		// Token: 0x040033AE RID: 13230
		[NonSerialized]
		private string m_customPropertyName;

		// Token: 0x040033AF RID: 13231
		[NonSerialized]
		private string m_customTopLevelRenderItemName;

		// Token: 0x040033B0 RID: 13232
		[NonSerialized]
		private bool m_firstInstance = true;
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006FD RID: 1789
	[Serializable]
	internal sealed class Chart : Pivot, IRunningValueHolder
	{
		// Token: 0x06006386 RID: 25478 RVA: 0x0018B218 File Offset: 0x00189418
		internal Chart(ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x06006387 RID: 25479 RVA: 0x0018B221 File Offset: 0x00189421
		internal Chart(int id, ReportItem parent)
			: base(id, parent)
		{
			this.m_cellDataPoints = new ChartDataPointList();
			this.m_cellRunningValues = new RunningValueInfoList();
		}

		// Token: 0x17002330 RID: 9008
		// (get) Token: 0x06006388 RID: 25480 RVA: 0x0018B241 File Offset: 0x00189441
		internal override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Chart;
			}
		}

		// Token: 0x17002331 RID: 9009
		// (get) Token: 0x06006389 RID: 25481 RVA: 0x0018B245 File Offset: 0x00189445
		internal override PivotHeading PivotColumns
		{
			get
			{
				return this.m_columns;
			}
		}

		// Token: 0x17002332 RID: 9010
		// (get) Token: 0x0600638A RID: 25482 RVA: 0x0018B24D File Offset: 0x0018944D
		internal override PivotHeading PivotRows
		{
			get
			{
				return this.m_rows;
			}
		}

		// Token: 0x17002333 RID: 9011
		// (get) Token: 0x0600638B RID: 25483 RVA: 0x0018B255 File Offset: 0x00189455
		// (set) Token: 0x0600638C RID: 25484 RVA: 0x0018B25D File Offset: 0x0018945D
		internal ChartHeading Columns
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

		// Token: 0x17002334 RID: 9012
		// (get) Token: 0x0600638D RID: 25485 RVA: 0x0018B266 File Offset: 0x00189466
		// (set) Token: 0x0600638E RID: 25486 RVA: 0x0018B26E File Offset: 0x0018946E
		internal ChartHeading Rows
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

		// Token: 0x17002335 RID: 9013
		// (get) Token: 0x0600638F RID: 25487 RVA: 0x0018B277 File Offset: 0x00189477
		// (set) Token: 0x06006390 RID: 25488 RVA: 0x0018B27F File Offset: 0x0018947F
		internal MultiChart MultiChart
		{
			get
			{
				return this.m_multiChart;
			}
			set
			{
				this.m_multiChart = value;
			}
		}

		// Token: 0x17002336 RID: 9014
		// (get) Token: 0x06006391 RID: 25489 RVA: 0x0018B288 File Offset: 0x00189488
		// (set) Token: 0x06006392 RID: 25490 RVA: 0x0018B290 File Offset: 0x00189490
		internal ChartDataPointList ChartDataPoints
		{
			get
			{
				return this.m_cellDataPoints;
			}
			set
			{
				this.m_cellDataPoints = value;
			}
		}

		// Token: 0x17002337 RID: 9015
		// (get) Token: 0x06006393 RID: 25491 RVA: 0x0018B299 File Offset: 0x00189499
		internal override RunningValueInfoList PivotCellRunningValues
		{
			get
			{
				return this.m_cellRunningValues;
			}
		}

		// Token: 0x17002338 RID: 9016
		// (get) Token: 0x06006394 RID: 25492 RVA: 0x0018B2A1 File Offset: 0x001894A1
		// (set) Token: 0x06006395 RID: 25493 RVA: 0x0018B2A9 File Offset: 0x001894A9
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

		// Token: 0x17002339 RID: 9017
		// (get) Token: 0x06006396 RID: 25494 RVA: 0x0018B2B2 File Offset: 0x001894B2
		// (set) Token: 0x06006397 RID: 25495 RVA: 0x0018B2BA File Offset: 0x001894BA
		internal Legend Legend
		{
			get
			{
				return this.m_legend;
			}
			set
			{
				this.m_legend = value;
			}
		}

		// Token: 0x1700233A RID: 9018
		// (get) Token: 0x06006398 RID: 25496 RVA: 0x0018B2C3 File Offset: 0x001894C3
		// (set) Token: 0x06006399 RID: 25497 RVA: 0x0018B2CB File Offset: 0x001894CB
		internal Axis CategoryAxis
		{
			get
			{
				return this.m_categoryAxis;
			}
			set
			{
				this.m_categoryAxis = value;
			}
		}

		// Token: 0x1700233B RID: 9019
		// (get) Token: 0x0600639A RID: 25498 RVA: 0x0018B2D4 File Offset: 0x001894D4
		// (set) Token: 0x0600639B RID: 25499 RVA: 0x0018B2DC File Offset: 0x001894DC
		internal Axis ValueAxis
		{
			get
			{
				return this.m_valueAxis;
			}
			set
			{
				this.m_valueAxis = value;
			}
		}

		// Token: 0x1700233C RID: 9020
		// (get) Token: 0x0600639C RID: 25500 RVA: 0x0018B2E5 File Offset: 0x001894E5
		internal override PivotHeading PivotStaticColumns
		{
			get
			{
				return this.m_staticColumns;
			}
		}

		// Token: 0x1700233D RID: 9021
		// (get) Token: 0x0600639D RID: 25501 RVA: 0x0018B2ED File Offset: 0x001894ED
		internal override PivotHeading PivotStaticRows
		{
			get
			{
				return this.m_staticRows;
			}
		}

		// Token: 0x1700233E RID: 9022
		// (get) Token: 0x0600639E RID: 25502 RVA: 0x0018B2F5 File Offset: 0x001894F5
		// (set) Token: 0x0600639F RID: 25503 RVA: 0x0018B2FD File Offset: 0x001894FD
		internal ChartHeading StaticColumns
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

		// Token: 0x1700233F RID: 9023
		// (get) Token: 0x060063A0 RID: 25504 RVA: 0x0018B306 File Offset: 0x00189506
		// (set) Token: 0x060063A1 RID: 25505 RVA: 0x0018B30E File Offset: 0x0018950E
		internal ChartHeading StaticRows
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

		// Token: 0x17002340 RID: 9024
		// (get) Token: 0x060063A2 RID: 25506 RVA: 0x0018B317 File Offset: 0x00189517
		// (set) Token: 0x060063A3 RID: 25507 RVA: 0x0018B31F File Offset: 0x0018951F
		internal Chart.ChartTypes Type
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

		// Token: 0x17002341 RID: 9025
		// (get) Token: 0x060063A4 RID: 25508 RVA: 0x0018B328 File Offset: 0x00189528
		// (set) Token: 0x060063A5 RID: 25509 RVA: 0x0018B330 File Offset: 0x00189530
		internal Chart.ChartSubTypes SubType
		{
			get
			{
				return this.m_subType;
			}
			set
			{
				this.m_subType = value;
			}
		}

		// Token: 0x17002342 RID: 9026
		// (get) Token: 0x060063A6 RID: 25510 RVA: 0x0018B339 File Offset: 0x00189539
		// (set) Token: 0x060063A7 RID: 25511 RVA: 0x0018B341 File Offset: 0x00189541
		internal ChartTitle Title
		{
			get
			{
				return this.m_title;
			}
			set
			{
				this.m_title = value;
			}
		}

		// Token: 0x17002343 RID: 9027
		// (get) Token: 0x060063A8 RID: 25512 RVA: 0x0018B34A File Offset: 0x0018954A
		// (set) Token: 0x060063A9 RID: 25513 RVA: 0x0018B352 File Offset: 0x00189552
		internal int PointWidth
		{
			get
			{
				return this.m_pointWidth;
			}
			set
			{
				this.m_pointWidth = value;
			}
		}

		// Token: 0x17002344 RID: 9028
		// (get) Token: 0x060063AA RID: 25514 RVA: 0x0018B35B File Offset: 0x0018955B
		// (set) Token: 0x060063AB RID: 25515 RVA: 0x0018B363 File Offset: 0x00189563
		internal ThreeDProperties ThreeDProperties
		{
			get
			{
				return this.m_3dProperties;
			}
			set
			{
				this.m_3dProperties = value;
			}
		}

		// Token: 0x17002345 RID: 9029
		// (get) Token: 0x060063AC RID: 25516 RVA: 0x0018B36C File Offset: 0x0018956C
		// (set) Token: 0x060063AD RID: 25517 RVA: 0x0018B374 File Offset: 0x00189574
		internal Chart.ChartPalette Palette
		{
			get
			{
				return this.m_palette;
			}
			set
			{
				this.m_palette = value;
			}
		}

		// Token: 0x17002346 RID: 9030
		// (get) Token: 0x060063AE RID: 25518 RVA: 0x0018B37D File Offset: 0x0018957D
		// (set) Token: 0x060063AF RID: 25519 RVA: 0x0018B385 File Offset: 0x00189585
		internal PlotArea PlotArea
		{
			get
			{
				return this.m_plotArea;
			}
			set
			{
				this.m_plotArea = value;
			}
		}

		// Token: 0x17002347 RID: 9031
		// (get) Token: 0x060063B0 RID: 25520 RVA: 0x0018B38E File Offset: 0x0018958E
		internal ChartExprHost ChartExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17002348 RID: 9032
		// (get) Token: 0x060063B1 RID: 25521 RVA: 0x0018B396 File Offset: 0x00189596
		protected override DataRegionExprHost DataRegionExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17002349 RID: 9033
		// (get) Token: 0x060063B2 RID: 25522 RVA: 0x0018B39E File Offset: 0x0018959E
		// (set) Token: 0x060063B3 RID: 25523 RVA: 0x0018B3A6 File Offset: 0x001895A6
		internal IntList NumberOfSeriesDataPoints
		{
			get
			{
				return this.m_numberOfSeriesDataPoints;
			}
			set
			{
				this.m_numberOfSeriesDataPoints = value;
			}
		}

		// Token: 0x1700234A RID: 9034
		// (get) Token: 0x060063B4 RID: 25524 RVA: 0x0018B3AF File Offset: 0x001895AF
		// (set) Token: 0x060063B5 RID: 25525 RVA: 0x0018B3B7 File Offset: 0x001895B7
		internal BoolList SeriesPlotType
		{
			get
			{
				return this.m_seriesPlotType;
			}
			set
			{
				this.m_seriesPlotType = value;
			}
		}

		// Token: 0x1700234B RID: 9035
		// (get) Token: 0x060063B6 RID: 25526 RVA: 0x0018B3C0 File Offset: 0x001895C0
		// (set) Token: 0x060063B7 RID: 25527 RVA: 0x0018B3C8 File Offset: 0x001895C8
		internal bool HasSeriesPlotTypeLine
		{
			get
			{
				return this.m_hasSeriesPlotTypeLine;
			}
			set
			{
				this.m_hasSeriesPlotTypeLine = value;
			}
		}

		// Token: 0x1700234C RID: 9036
		// (get) Token: 0x060063B8 RID: 25528 RVA: 0x0018B3D1 File Offset: 0x001895D1
		// (set) Token: 0x060063B9 RID: 25529 RVA: 0x0018B3D9 File Offset: 0x001895D9
		internal bool HasDataValueAggregates
		{
			get
			{
				return this.m_hasDataValueAggregates;
			}
			set
			{
				this.m_hasDataValueAggregates = value;
			}
		}

		// Token: 0x1700234D RID: 9037
		// (get) Token: 0x060063BA RID: 25530 RVA: 0x0018B3E4 File Offset: 0x001895E4
		internal int StaticSeriesCount
		{
			get
			{
				ExpressionInfoList expressionInfoList = ((this.PivotStaticRows != null) ? ((ChartHeading)this.PivotStaticRows).Labels : null);
				if (expressionInfoList == null)
				{
					return 1;
				}
				return expressionInfoList.Count;
			}
		}

		// Token: 0x1700234E RID: 9038
		// (get) Token: 0x060063BB RID: 25531 RVA: 0x0018B418 File Offset: 0x00189618
		internal int StaticCategoryCount
		{
			get
			{
				ExpressionInfoList expressionInfoList = ((this.PivotStaticColumns != null) ? ((ChartHeading)this.PivotStaticColumns).Labels : null);
				if (expressionInfoList == null)
				{
					return 1;
				}
				return expressionInfoList.Count;
			}
		}

		// Token: 0x060063BC RID: 25532 RVA: 0x0018B44C File Offset: 0x0018964C
		RunningValueInfoList IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		// Token: 0x060063BD RID: 25533 RVA: 0x0018B454 File Offset: 0x00189654
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

		// Token: 0x060063BE RID: 25534 RVA: 0x0018B4B0 File Offset: 0x001896B0
		internal static object[] CreateStyle(ReportProcessing.ProcessingContext pc, Style styleDef, string objectName, int uniqueName)
		{
			object[] array = null;
			if (styleDef != null && styleDef.ExpressionList != null && 0 < styleDef.ExpressionList.Count)
			{
				array = new object[styleDef.ExpressionList.Count];
				ReportProcessing.RuntimeRICollection.EvaluateStyleAttributes(ObjectType.Chart, objectName, styleDef, uniqueName, array, pc);
			}
			return array;
		}

		// Token: 0x060063BF RID: 25535 RVA: 0x0018B4F8 File Offset: 0x001896F8
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			if ((context.Location & LocationFlags.InDetail) != (LocationFlags)0 && (context.Location & LocationFlags.InGrouping) == (LocationFlags)0)
			{
				context.ErrorContext.Register((this.m_parent is Table) ? ProcessingErrorCode.rsDataRegionInTableDetailRow : ProcessingErrorCode.rsDataRegionInDetailList, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
			}
			else
			{
				context.RegisterDataRegion(this);
				this.InternalInitialize(context);
				context.UnRegisterDataRegion(this);
			}
			return false;
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x0018B588 File Offset: 0x00189788
		private void InternalInitialize(InitializationContext context)
		{
			context.Location = context.Location | LocationFlags.InDataSet | LocationFlags.InDataRegion;
			context.ExprHostBuilder.ChartStart(this.m_name);
			base.Initialize(context);
			context.RegisterRunningValues(this.m_runningValues);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, true, false);
			}
			this.CornerInitialize(context);
			context.Location &= ~LocationFlags.InMatrixCellTopLevelItem;
			bool flag = false;
			int num;
			this.ColumnsInitialize(context, out num, out flag);
			bool flag2 = flag;
			int num2;
			this.RowsInitialize(context, out num2, out flag);
			if (flag)
			{
				flag2 = true;
			}
			this.ChartDataPointInitialize(context, num, num2, flag2);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
			context.UnRegisterRunningValues(this.m_runningValues);
			base.CopyHeadingAggregates(this.m_rows);
			this.m_rows.TransferHeadingAggregates();
			base.CopyHeadingAggregates(this.m_columns);
			this.m_columns.TransferHeadingAggregates();
			base.ExprHostID = context.ExprHostBuilder.ChartEnd();
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x0018B68C File Offset: 0x0018988C
		private void CornerInitialize(InitializationContext context)
		{
			if (this.m_categoryAxis != null)
			{
				context.ExprHostBuilder.ChartCategoryAxisStart();
				this.m_categoryAxis.Initialize(context, Axis.Mode.CategoryAxis);
				context.ExprHostBuilder.ChartCategoryAxisEnd();
			}
			if (this.m_valueAxis != null)
			{
				context.ExprHostBuilder.ChartValueAxisStart();
				this.m_valueAxis.Initialize(context, Axis.Mode.ValueAxis);
				context.ExprHostBuilder.ChartValueAxisEnd();
			}
			if (this.m_multiChart != null)
			{
				this.m_multiChart.Initialize(context);
			}
			if (this.m_legend != null)
			{
				this.m_legend.Initialize(context);
			}
			if (this.m_title != null)
			{
				this.m_title.Initialize(context);
			}
			if (this.m_3dProperties != null)
			{
				this.m_3dProperties.Initialize(context);
			}
			if (this.m_plotArea != null)
			{
				this.m_plotArea.Initialize(context);
			}
			if (this.m_categoryAxis != null && this.m_categoryAxis.Scalar)
			{
				Global.Tracer.Assert(this.m_columns != null);
				if (this.m_columns.SubHeading != null)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsMultipleGroupingsOnChartScalarAxis, Severity.Error, context.ObjectType, context.ObjectName, "CategoryAxis", Array.Empty<string>());
					return;
				}
				if (this.StaticColumns != null && this.StaticColumns.Labels != null)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsStaticGroupingOnChartScalarAxis, Severity.Error, context.ObjectType, context.ObjectName, "CategoryAxis", Array.Empty<string>());
					return;
				}
				if (this.m_columns.Grouping != null && this.m_columns.Grouping.GroupExpressions != null && 1 < this.m_columns.Grouping.GroupExpressions.Count)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsMultipleGroupExpressionsOnChartScalarAxis, Severity.Error, context.ObjectType, context.ObjectName, "CategoryAxis", Array.Empty<string>());
					return;
				}
				Global.Tracer.Assert(this.m_columns.SubHeading == null);
				Global.Tracer.Assert(this.StaticColumns == null || this.StaticColumns.Labels == null);
				this.m_columns.ChartGroupExpression = true;
				if (this.m_columns.Labels != null && this.m_columns.Grouping != null && this.m_columns.Grouping.GroupExpressions != null && ReportProcessing.CompareWithInvariantCulture(this.m_columns.Labels[0].Value, this.m_columns.Grouping.GroupExpressions[0].Value, true) != 0)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsLabelExpressionOnChartScalarAxisIsIgnored, Severity.Warning, context.ObjectType, context.ObjectName, "CategoryAxis", Array.Empty<string>());
					this.m_columns.Labels = null;
				}
				if (this.m_columns.Grouping != null && Chart.ChartTypes.Area == this.m_type)
				{
					Global.Tracer.Assert(this.m_columns.Grouping.GroupExpressions != null);
					if (this.m_columns.Sorting == null || this.m_columns.Sorting.SortExpressions == null || this.m_columns.Sorting.SortExpressions[0] == null)
					{
						this.m_columns.Grouping.GroupAndSort = true;
						this.m_columns.Grouping.SortDirections = new BoolList(1);
						this.m_columns.Grouping.SortDirections.Add(true);
						return;
					}
					if (ReportProcessing.CompareWithInvariantCulture(this.m_columns.Grouping.GroupExpressions[0].Value, this.m_columns.Sorting.SortExpressions[0].Value, true) != 0)
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsUnsortedCategoryInAreaChart, Severity.Error, context.ObjectType, context.ObjectName, "CategoryGrouping", new string[] { this.m_columns.Grouping.Name });
						return;
					}
				}
				else if (this.m_columns.Grouping != null && (Chart.ChartTypes.Line == this.m_type || (this.m_type == Chart.ChartTypes.Column && this.m_hasSeriesPlotTypeLine)))
				{
					Global.Tracer.Assert(this.m_columns.Grouping.GroupExpressions != null);
					if (!this.m_columns.Grouping.GroupAndSort)
					{
						bool flag = false;
						if (this.m_columns.Sorting == null || this.m_columns.Sorting.SortExpressions == null || this.m_columns.Sorting.SortExpressions[0] == null)
						{
							flag = true;
						}
						else if (ReportProcessing.CompareWithInvariantCulture(this.m_columns.Grouping.GroupExpressions[0].Value, this.m_columns.Sorting.SortExpressions[0].Value, true) != 0)
						{
							flag = true;
						}
						if (flag)
						{
							context.ErrorContext.Register(ProcessingErrorCode.rsLineChartMightScatter, Severity.Warning, context.ObjectType, context.ObjectName, "CategoryGrouping", Array.Empty<string>());
						}
					}
				}
			}
		}

		// Token: 0x060063C2 RID: 25538 RVA: 0x0018BB8C File Offset: 0x00189D8C
		private void ColumnsInitialize(InitializationContext context, out int expectedNumberOfCategories, out bool computedSubtotal)
		{
			Global.Tracer.Assert(this.m_columns != null);
			computedSubtotal = false;
			this.m_columns.DynamicInitialize(true, 0, context);
			this.m_columns.StaticInitialize(context);
			expectedNumberOfCategories = ((this.m_staticColumns != null) ? this.m_staticColumns.NumberOfStatics : 1);
			if (this.m_columns.Grouping == null)
			{
				Global.Tracer.Assert(this.m_columns != null);
				context.SpecialTransferRunningValues(this.m_columns.RunningValues);
			}
		}

		// Token: 0x060063C3 RID: 25539 RVA: 0x0018BC18 File Offset: 0x00189E18
		private void RowsInitialize(InitializationContext context, out int expectedNumberOfSeries, out bool computedSubtotal)
		{
			Global.Tracer.Assert(this.m_rows != null);
			computedSubtotal = false;
			this.m_rows.DynamicInitialize(false, 0, context);
			this.m_rows.StaticInitialize(context);
			expectedNumberOfSeries = ((this.m_staticRows != null) ? this.m_staticRows.NumberOfStatics : 1);
			if (this.m_rows != null && this.m_rows.Grouping == null)
			{
				context.SpecialTransferRunningValues(this.m_rows.RunningValues);
			}
			if (this.m_hasSeriesPlotTypeLine && this.m_seriesPlotType != null)
			{
				if (this.m_type == Chart.ChartTypes.Column)
				{
					if (this.m_staticRows == null)
					{
						this.m_type = Chart.ChartTypes.Line;
						return;
					}
					this.m_staticRows.PlotTypesLine = this.m_seriesPlotType;
					return;
				}
				else
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsChartSeriesPlotTypeIgnored, Severity.Warning, context.ObjectType, context.ObjectName, "PlotType", Array.Empty<string>());
				}
			}
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x0018BCF8 File Offset: 0x00189EF8
		private void ChartDataPointInitialize(InitializationContext context, int expectedNumberOfCategories, int expectedNumberOfSeries, bool computedCells)
		{
			if (this.m_cellDataPoints == null || this.m_cellDataPoints.Count == 0)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsMissingChartDataPoints, Severity.Error, context.ObjectType, context.ObjectName, "ChartData", Array.Empty<string>());
				return;
			}
			if (expectedNumberOfSeries != this.m_numberOfSeriesDataPoints.Count)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfChartSeries, Severity.Error, context.ObjectType, context.ObjectName, "ChartSeries", Array.Empty<string>());
			}
			bool flag = false;
			int num = 0;
			while (num < this.m_numberOfSeriesDataPoints.Count && !flag)
			{
				if (this.m_numberOfSeriesDataPoints[num] != expectedNumberOfCategories)
				{
					flag = true;
				}
				num++;
			}
			if (flag)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfChartDataPointsInSeries, Severity.Error, context.ObjectType, context.ObjectName, "ChartSeries", Array.Empty<string>());
			}
			int num2 = expectedNumberOfCategories * expectedNumberOfSeries;
			if (num2 != this.m_cellDataPoints.Count)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfChartDataPoints, Severity.Error, context.ObjectType, context.ObjectName, "DataPoints", new string[]
				{
					this.m_cellDataPoints.Count.ToString(CultureInfo.InvariantCulture),
					num2.ToString(CultureInfo.InvariantCulture)
				});
			}
			context.Location |= LocationFlags.InMatrixCell;
			context.MatrixName = this.m_name;
			context.RegisterTablixCellScope(this.m_columns.SubHeading == null && this.m_columns.Grouping == null, this.m_cellAggregates, this.m_cellPostSortAggregates);
			for (ChartHeading chartHeading = this.m_rows; chartHeading != null; chartHeading = chartHeading.SubHeading)
			{
				if (chartHeading.Grouping != null)
				{
					context.Location |= LocationFlags.InGrouping;
					context.RegisterGroupingScopeForTablixCell(chartHeading.Grouping.Name, false, chartHeading.Grouping.SimpleGroupExpressions, chartHeading.Aggregates, chartHeading.PostSortAggregates, chartHeading.RecursiveAggregates, chartHeading.Grouping);
				}
			}
			if (this.m_rows.Grouping != null && this.m_rows.Subtotal != null && this.m_staticRows != null)
			{
				context.CopyRunningValues(this.StaticRows.RunningValues, this.m_aggregates);
			}
			for (ChartHeading chartHeading = this.m_columns; chartHeading != null; chartHeading = chartHeading.SubHeading)
			{
				if (chartHeading.Grouping != null)
				{
					context.Location |= LocationFlags.InGrouping;
					context.RegisterGroupingScopeForTablixCell(chartHeading.Grouping.Name, true, chartHeading.Grouping.SimpleGroupExpressions, chartHeading.Aggregates, chartHeading.PostSortAggregates, chartHeading.RecursiveAggregates, chartHeading.Grouping);
				}
			}
			if (this.m_columns.Grouping != null && this.m_columns.Subtotal != null && this.m_staticColumns != null)
			{
				context.CopyRunningValues(this.StaticColumns.RunningValues, this.m_aggregates);
			}
			Global.Tracer.Assert(this.m_cellDataPoints != null);
			int count = this.m_cellDataPoints.Count;
			int num3 = 1;
			switch (this.m_type)
			{
			case Chart.ChartTypes.Scatter:
				num3 = 2;
				break;
			case Chart.ChartTypes.Bubble:
				num3 = 3;
				break;
			case Chart.ChartTypes.Stock:
				if (Chart.ChartSubTypes.HighLowClose == this.m_subType)
				{
					num3 = 3;
				}
				else
				{
					num3 = 4;
				}
				break;
			}
			context.RegisterRunningValues(this.m_cellRunningValues);
			for (int i = 0; i < count; i++)
			{
				Global.Tracer.Assert(this.m_cellDataPoints[i].DataValues != null);
				if (num3 > this.m_cellDataPoints[i].DataValues.Count)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfDataValues, Severity.Error, context.ObjectType, context.ObjectName, "DataValue", new string[]
					{
						this.m_cellDataPoints[i].DataValues.Count.ToString(CultureInfo.InvariantCulture),
						num3.ToString(CultureInfo.InvariantCulture)
					});
				}
				this.m_cellDataPoints[i].Initialize(context);
			}
			context.UnRegisterRunningValues(this.m_cellRunningValues);
			if (context.IsRunningValueDirectionColumn())
			{
				this.m_processingInnerGrouping = Pivot.ProcessingInnerGroupings.Row;
			}
			for (ChartHeading chartHeading = this.m_rows; chartHeading != null; chartHeading = chartHeading.SubHeading)
			{
				if (chartHeading.Grouping != null)
				{
					context.UnRegisterGroupingScopeForTablixCell(chartHeading.Grouping.Name, false);
				}
			}
			for (ChartHeading chartHeading = this.m_columns; chartHeading != null; chartHeading = chartHeading.SubHeading)
			{
				if (chartHeading.Grouping != null)
				{
					context.UnRegisterGroupingScopeForTablixCell(chartHeading.Grouping.Name, true);
				}
			}
			context.UnRegisterTablixCellScope();
		}

		// Token: 0x060063C5 RID: 25541 RVA: 0x0018C184 File Offset: 0x0018A384
		internal bool IsValidChartSubType()
		{
			if (this.m_subType == Chart.ChartSubTypes.Default)
			{
				if (this.m_type == Chart.ChartTypes.Stock)
				{
					this.m_subType = Chart.ChartSubTypes.HighLowClose;
				}
				else
				{
					this.m_subType = Chart.ChartSubTypes.Plain;
				}
				return true;
			}
			bool flag = true;
			switch (this.m_type)
			{
			case Chart.ChartTypes.Column:
			{
				Chart.ChartSubTypes chartSubTypes = this.m_subType;
				if (chartSubTypes - Chart.ChartSubTypes.Stacked > 2)
				{
					flag = false;
				}
				break;
			}
			case Chart.ChartTypes.Bar:
			{
				Chart.ChartSubTypes chartSubTypes = this.m_subType;
				if (chartSubTypes - Chart.ChartSubTypes.Stacked > 2)
				{
					flag = false;
				}
				break;
			}
			case Chart.ChartTypes.Line:
			{
				Chart.ChartSubTypes chartSubTypes = this.m_subType;
				if (chartSubTypes - Chart.ChartSubTypes.Stacked > 3)
				{
					flag = false;
				}
				break;
			}
			case Chart.ChartTypes.Pie:
			{
				Chart.ChartSubTypes chartSubTypes = this.m_subType;
				if (chartSubTypes != Chart.ChartSubTypes.Plain && chartSubTypes != Chart.ChartSubTypes.Exploded)
				{
					flag = false;
				}
				break;
			}
			case Chart.ChartTypes.Scatter:
			{
				Chart.ChartSubTypes chartSubTypes = this.m_subType;
				if (chartSubTypes != Chart.ChartSubTypes.Plain && chartSubTypes - Chart.ChartSubTypes.Line > 1)
				{
					flag = false;
				}
				break;
			}
			case Chart.ChartTypes.Bubble:
				if (this.m_subType != Chart.ChartSubTypes.Plain)
				{
					flag = false;
				}
				break;
			case Chart.ChartTypes.Area:
			{
				Chart.ChartSubTypes chartSubTypes = this.m_subType;
				if (chartSubTypes - Chart.ChartSubTypes.Stacked > 2)
				{
					flag = false;
				}
				break;
			}
			case Chart.ChartTypes.Doughnut:
			{
				Chart.ChartSubTypes chartSubTypes = this.m_subType;
				if (chartSubTypes != Chart.ChartSubTypes.Plain && chartSubTypes != Chart.ChartSubTypes.Exploded)
				{
					flag = false;
				}
				break;
			}
			case Chart.ChartTypes.Stock:
			{
				Chart.ChartSubTypes chartSubTypes = this.m_subType;
				if (chartSubTypes - Chart.ChartSubTypes.HighLowClose > 2)
				{
					flag = false;
				}
				break;
			}
			default:
				Global.Tracer.Assert(false, string.Empty);
				flag = false;
				break;
			}
			return flag;
		}

		// Token: 0x060063C6 RID: 25542 RVA: 0x0018C2AC File Offset: 0x0018A4AC
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.ChartHostsRemotable[base.ExprHostID];
				base.DataRegionSetExprHost(this.m_exprHost, reportObjectModel);
				if (this.m_multiChart != null && this.m_exprHost.MultiChartHost != null)
				{
					this.m_multiChart.SetExprHost(this.m_exprHost.MultiChartHost, reportObjectModel);
				}
				IList<ChartDataPointExprHost> chartDataPointHostsRemotable = this.m_exprHost.ChartDataPointHostsRemotable;
				for (int i = 0; i < this.m_cellDataPoints.Count; i++)
				{
					ChartDataPoint chartDataPoint = this.m_cellDataPoints[i];
					if (chartDataPoint != null && chartDataPoint.ExprHostID != -1)
					{
						chartDataPoint.SetExprHost(chartDataPointHostsRemotable[chartDataPoint.ExprHostID], reportObjectModel);
					}
				}
				if (this.m_categoryAxis != null && this.m_exprHost.CategoryAxisHost != null)
				{
					this.m_categoryAxis.SetExprHost(this.m_exprHost.CategoryAxisHost, reportObjectModel);
				}
				if (this.m_valueAxis != null && this.m_exprHost.ValueAxisHost != null)
				{
					this.m_valueAxis.SetExprHost(this.m_exprHost.ValueAxisHost, reportObjectModel);
				}
				if (this.m_title != null && this.m_exprHost.TitleHost != null)
				{
					this.m_title.SetExprHost(this.m_exprHost.TitleHost, reportObjectModel);
				}
				if (this.m_exprHost.StaticColumnLabelsHost != null)
				{
					this.m_exprHost.StaticColumnLabelsHost.SetReportObjectModel(reportObjectModel);
				}
				if (this.m_exprHost.StaticRowLabelsHost != null)
				{
					this.m_exprHost.StaticRowLabelsHost.SetReportObjectModel(reportObjectModel);
				}
				if (this.m_legend != null && this.m_exprHost.LegendHost != null)
				{
					this.m_legend.SetExprHost(this.m_exprHost.LegendHost, reportObjectModel);
				}
				if (this.m_plotArea != null && this.m_exprHost.PlotAreaHost != null)
				{
					this.m_plotArea.SetExprHost(this.m_exprHost.PlotAreaHost, reportObjectModel);
				}
			}
		}

		// Token: 0x060063C7 RID: 25543 RVA: 0x0018C490 File Offset: 0x0018A690
		internal ChartDataPoint GetDataPoint(int seriesIndex, int categoryIndex)
		{
			int num = seriesIndex * this.StaticCategoryCount + categoryIndex;
			return this.m_cellDataPoints[num];
		}

		// Token: 0x060063C8 RID: 25544 RVA: 0x0018C4B4 File Offset: 0x0018A6B4
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.Pivot, new MemberInfoList
			{
				new MemberInfo(MemberName.Columns, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ChartHeading),
				new MemberInfo(MemberName.Rows, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ChartHeading),
				new MemberInfo(MemberName.CellDataPoints, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ChartDataPointList),
				new MemberInfo(MemberName.CellRunningValues, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.RunningValueInfoList),
				new MemberInfo(MemberName.MultiChart, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.MultiChart),
				new MemberInfo(MemberName.Legend, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.Legend),
				new MemberInfo(MemberName.CategoryAxis, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.Axis),
				new MemberInfo(MemberName.ValueAxis, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.Axis),
				new MemberInfo(MemberName.StaticColumns, Token.Reference, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ChartHeading),
				new MemberInfo(MemberName.StaticRows, Token.Reference, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ChartHeading),
				new MemberInfo(MemberName.Type, Token.Enum),
				new MemberInfo(MemberName.SubType, Token.Enum),
				new MemberInfo(MemberName.Palette, Token.Enum),
				new MemberInfo(MemberName.Title, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ChartTitle),
				new MemberInfo(MemberName.PointWidth, Token.Int32),
				new MemberInfo(MemberName.ThreeDProperties, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ThreeDProperties),
				new MemberInfo(MemberName.PlotArea, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.PlotArea)
			});
		}

		// Token: 0x0400320A RID: 12810
		private ChartHeading m_columns;

		// Token: 0x0400320B RID: 12811
		private ChartHeading m_rows;

		// Token: 0x0400320C RID: 12812
		private ChartDataPointList m_cellDataPoints;

		// Token: 0x0400320D RID: 12813
		private RunningValueInfoList m_cellRunningValues;

		// Token: 0x0400320E RID: 12814
		private MultiChart m_multiChart;

		// Token: 0x0400320F RID: 12815
		private Legend m_legend;

		// Token: 0x04003210 RID: 12816
		private Axis m_categoryAxis;

		// Token: 0x04003211 RID: 12817
		private Axis m_valueAxis;

		// Token: 0x04003212 RID: 12818
		[Reference]
		private ChartHeading m_staticColumns;

		// Token: 0x04003213 RID: 12819
		[Reference]
		private ChartHeading m_staticRows;

		// Token: 0x04003214 RID: 12820
		private Chart.ChartTypes m_type;

		// Token: 0x04003215 RID: 12821
		private Chart.ChartSubTypes m_subType;

		// Token: 0x04003216 RID: 12822
		private Chart.ChartPalette m_palette;

		// Token: 0x04003217 RID: 12823
		private ChartTitle m_title;

		// Token: 0x04003218 RID: 12824
		private int m_pointWidth;

		// Token: 0x04003219 RID: 12825
		private ThreeDProperties m_3dProperties;

		// Token: 0x0400321A RID: 12826
		private PlotArea m_plotArea;

		// Token: 0x0400321B RID: 12827
		[NonSerialized]
		private ChartExprHost m_exprHost;

		// Token: 0x0400321C RID: 12828
		[NonSerialized]
		private IntList m_numberOfSeriesDataPoints;

		// Token: 0x0400321D RID: 12829
		[NonSerialized]
		private BoolList m_seriesPlotType;

		// Token: 0x0400321E RID: 12830
		[NonSerialized]
		private bool m_hasSeriesPlotTypeLine;

		// Token: 0x0400321F RID: 12831
		[NonSerialized]
		private bool m_hasDataValueAggregates;

		// Token: 0x02000CCB RID: 3275
		internal enum ChartTypes
		{
			// Token: 0x04004EA6 RID: 20134
			Column,
			// Token: 0x04004EA7 RID: 20135
			Bar,
			// Token: 0x04004EA8 RID: 20136
			Line,
			// Token: 0x04004EA9 RID: 20137
			Pie,
			// Token: 0x04004EAA RID: 20138
			Scatter,
			// Token: 0x04004EAB RID: 20139
			Bubble,
			// Token: 0x04004EAC RID: 20140
			Area,
			// Token: 0x04004EAD RID: 20141
			Doughnut,
			// Token: 0x04004EAE RID: 20142
			Stock
		}

		// Token: 0x02000CCC RID: 3276
		internal enum ChartSubTypes
		{
			// Token: 0x04004EB0 RID: 20144
			Default,
			// Token: 0x04004EB1 RID: 20145
			Stacked,
			// Token: 0x04004EB2 RID: 20146
			PercentStacked,
			// Token: 0x04004EB3 RID: 20147
			Plain,
			// Token: 0x04004EB4 RID: 20148
			Smooth,
			// Token: 0x04004EB5 RID: 20149
			Exploded,
			// Token: 0x04004EB6 RID: 20150
			Line,
			// Token: 0x04004EB7 RID: 20151
			SmoothLine,
			// Token: 0x04004EB8 RID: 20152
			HighLowClose,
			// Token: 0x04004EB9 RID: 20153
			OpenHighLowClose,
			// Token: 0x04004EBA RID: 20154
			Candlestick
		}

		// Token: 0x02000CCD RID: 3277
		internal enum ChartPalette
		{
			// Token: 0x04004EBC RID: 20156
			Default,
			// Token: 0x04004EBD RID: 20157
			EarthTones,
			// Token: 0x04004EBE RID: 20158
			Excel,
			// Token: 0x04004EBF RID: 20159
			GrayScale,
			// Token: 0x04004EC0 RID: 20160
			Light,
			// Token: 0x04004EC1 RID: 20161
			Pastel,
			// Token: 0x04004EC2 RID: 20162
			SemiTransparent
		}
	}
}

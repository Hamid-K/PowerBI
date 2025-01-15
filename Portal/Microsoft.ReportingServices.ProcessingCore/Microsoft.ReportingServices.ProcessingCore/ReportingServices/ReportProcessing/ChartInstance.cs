using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200074B RID: 1867
	[Serializable]
	internal sealed class ChartInstance : ReportItemInstance, IPageItem
	{
		// Token: 0x0600678F RID: 26511 RVA: 0x0019458C File Offset: 0x0019278C
		internal ChartInstance(ReportProcessing.ProcessingContext pc, Chart reportItemDef)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.m_instanceInfo = new ChartInstanceInfo(pc, reportItemDef, this);
			pc.Pagination.EnterIgnoreHeight(reportItemDef.StartHidden);
			this.m_multiCharts = new MultiChartInstanceList();
			pc.QuickFind.Add(base.UniqueName, this);
		}

		// Token: 0x06006790 RID: 26512 RVA: 0x001945F0 File Offset: 0x001927F0
		internal ChartInstance()
		{
		}

		// Token: 0x17002494 RID: 9364
		// (get) Token: 0x06006791 RID: 26513 RVA: 0x00194606 File Offset: 0x00192806
		// (set) Token: 0x06006792 RID: 26514 RVA: 0x0019460E File Offset: 0x0019280E
		internal MultiChartInstanceList MultiCharts
		{
			get
			{
				return this.m_multiCharts;
			}
			set
			{
				this.m_multiCharts = value;
			}
		}

		// Token: 0x17002495 RID: 9365
		// (get) Token: 0x06006793 RID: 26515 RVA: 0x00194617 File Offset: 0x00192817
		private MultiChartInstance CurrentMultiChart
		{
			get
			{
				if (0 >= this.m_multiCharts.Count)
				{
					this.m_multiCharts.Add(new MultiChartInstance((Chart)this.m_reportItemDef));
				}
				return this.m_multiCharts[0];
			}
		}

		// Token: 0x17002496 RID: 9366
		// (get) Token: 0x06006794 RID: 26516 RVA: 0x0019464F File Offset: 0x0019284F
		internal ChartHeadingInstanceList ColumnInstances
		{
			get
			{
				return this.CurrentMultiChart.ColumnInstances;
			}
		}

		// Token: 0x17002497 RID: 9367
		// (get) Token: 0x06006795 RID: 26517 RVA: 0x0019465C File Offset: 0x0019285C
		internal ChartHeadingInstanceList RowInstances
		{
			get
			{
				return this.CurrentMultiChart.RowInstances;
			}
		}

		// Token: 0x17002498 RID: 9368
		// (get) Token: 0x06006796 RID: 26518 RVA: 0x00194669 File Offset: 0x00192869
		internal ChartDataPointInstancesList DataPoints
		{
			get
			{
				return this.CurrentMultiChart.DataPoints;
			}
		}

		// Token: 0x17002499 RID: 9369
		// (get) Token: 0x06006797 RID: 26519 RVA: 0x00194676 File Offset: 0x00192876
		internal int DataPointSeriesCount
		{
			get
			{
				if (this.CurrentMultiChart.DataPoints != null)
				{
					return this.CurrentMultiChart.DataPoints.Count;
				}
				return 0;
			}
		}

		// Token: 0x1700249A RID: 9370
		// (get) Token: 0x06006798 RID: 26520 RVA: 0x00194698 File Offset: 0x00192898
		internal int DataPointCategoryCount
		{
			get
			{
				ChartDataPointInstancesList dataPoints = this.CurrentMultiChart.DataPoints;
				if (dataPoints != null)
				{
					Global.Tracer.Assert(dataPoints[0] != null);
					return dataPoints[0].Count;
				}
				return 0;
			}
		}

		// Token: 0x1700249B RID: 9371
		// (get) Token: 0x06006799 RID: 26521 RVA: 0x001946D6 File Offset: 0x001928D6
		internal int CurrentCellOuterIndex
		{
			get
			{
				return this.m_currentCellOuterIndex;
			}
		}

		// Token: 0x1700249C RID: 9372
		// (get) Token: 0x0600679A RID: 26522 RVA: 0x001946DE File Offset: 0x001928DE
		internal int CurrentCellInnerIndex
		{
			get
			{
				return this.m_currentCellInnerIndex;
			}
		}

		// Token: 0x1700249D RID: 9373
		// (set) Token: 0x0600679B RID: 26523 RVA: 0x001946E6 File Offset: 0x001928E6
		internal int CurrentOuterStaticIndex
		{
			set
			{
				this.m_currentOuterStaticIndex = value;
			}
		}

		// Token: 0x1700249E RID: 9374
		// (set) Token: 0x0600679C RID: 26524 RVA: 0x001946EF File Offset: 0x001928EF
		internal int CurrentInnerStaticIndex
		{
			set
			{
				this.m_currentInnerStaticIndex = value;
			}
		}

		// Token: 0x1700249F RID: 9375
		// (get) Token: 0x0600679D RID: 26525 RVA: 0x001946F8 File Offset: 0x001928F8
		// (set) Token: 0x0600679E RID: 26526 RVA: 0x00194705 File Offset: 0x00192905
		internal ChartHeadingInstanceList InnerHeadingInstanceList
		{
			get
			{
				return this.CurrentMultiChart.InnerHeadingInstanceList;
			}
			set
			{
				this.CurrentMultiChart.InnerHeadingInstanceList = value;
			}
		}

		// Token: 0x170024A0 RID: 9376
		// (get) Token: 0x0600679F RID: 26527 RVA: 0x00194713 File Offset: 0x00192913
		// (set) Token: 0x060067A0 RID: 26528 RVA: 0x0019471B File Offset: 0x0019291B
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

		// Token: 0x170024A1 RID: 9377
		// (get) Token: 0x060067A1 RID: 26529 RVA: 0x00194724 File Offset: 0x00192924
		// (set) Token: 0x060067A2 RID: 26530 RVA: 0x0019472C File Offset: 0x0019292C
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

		// Token: 0x060067A3 RID: 26531 RVA: 0x00194735 File Offset: 0x00192935
		internal ChartDataPoint GetCellDataPoint(int cellDPIndex)
		{
			if (-1 == cellDPIndex)
			{
				cellDPIndex = this.GetCurrentCellDPIndex();
			}
			return ((Chart)this.m_reportItemDef).ChartDataPoints[cellDPIndex];
		}

		// Token: 0x060067A4 RID: 26532 RVA: 0x0019475C File Offset: 0x0019295C
		internal ChartDataPointInstance AddCell(ReportProcessing.ProcessingContext pc, int currCellDPIndex)
		{
			ChartDataPointInstancesList dataPoints = this.CurrentMultiChart.DataPoints;
			Chart chart = (Chart)this.m_reportItemDef;
			int num = ((currCellDPIndex < 0) ? this.GetCurrentCellDPIndex() : currCellDPIndex);
			ChartDataPointInstance chartDataPointInstance = new ChartDataPointInstance(pc, chart, this.GetCellDataPoint(num), num);
			if (chart.ProcessingInnerGrouping == Pivot.ProcessingInnerGroupings.Column)
			{
				dataPoints[this.m_currentCellOuterIndex].Add(chartDataPointInstance);
			}
			else
			{
				if (this.m_currentCellOuterIndex == 0)
				{
					Global.Tracer.Assert(dataPoints.Count == this.m_currentCellInnerIndex);
					ChartDataPointInstanceList chartDataPointInstanceList = new ChartDataPointInstanceList();
					dataPoints.Add(chartDataPointInstanceList);
				}
				dataPoints[this.m_currentCellInnerIndex].Add(chartDataPointInstance);
			}
			this.m_currentCellInnerIndex++;
			return chartDataPointInstance;
		}

		// Token: 0x060067A5 RID: 26533 RVA: 0x00194810 File Offset: 0x00192A10
		internal void NewOuterCells()
		{
			ChartDataPointInstancesList dataPoints = this.CurrentMultiChart.DataPoints;
			if (0 < this.m_currentCellInnerIndex || dataPoints.Count == 0)
			{
				if (((Chart)this.m_reportItemDef).ProcessingInnerGrouping == Pivot.ProcessingInnerGroupings.Column)
				{
					ChartDataPointInstanceList chartDataPointInstanceList = new ChartDataPointInstanceList();
					dataPoints.Add(chartDataPointInstanceList);
				}
				if (0 < this.m_currentCellInnerIndex)
				{
					this.m_currentCellOuterIndex++;
					this.m_currentCellInnerIndex = 0;
				}
			}
		}

		// Token: 0x060067A6 RID: 26534 RVA: 0x00194878 File Offset: 0x00192A78
		internal int GetCurrentCellDPIndex()
		{
			Chart chart = (Chart)this.m_reportItemDef;
			int num = ((chart.StaticColumns == null || chart.StaticColumns.Labels == null) ? 1 : chart.StaticColumns.Labels.Count);
			int num2;
			if (chart.ProcessingInnerGrouping == Pivot.ProcessingInnerGroupings.Column)
			{
				num2 = this.m_currentOuterStaticIndex * num + this.m_currentInnerStaticIndex;
			}
			else
			{
				num2 = this.m_currentInnerStaticIndex * num + this.m_currentOuterStaticIndex;
			}
			return num2;
		}

		// Token: 0x060067A7 RID: 26535 RVA: 0x001948E8 File Offset: 0x00192AE8
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstance, new MemberInfoList
			{
				new MemberInfo(MemberName.MultiCharts, ObjectType.MultiChartInstanceList)
			});
		}

		// Token: 0x060067A8 RID: 26536 RVA: 0x00194919 File Offset: 0x00192B19
		internal override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
			return reader.ReadChartInstanceInfo((Chart)this.m_reportItemDef);
		}

		// Token: 0x04003352 RID: 13138
		private MultiChartInstanceList m_multiCharts;

		// Token: 0x04003353 RID: 13139
		[NonSerialized]
		private int m_currentCellOuterIndex;

		// Token: 0x04003354 RID: 13140
		[NonSerialized]
		private int m_currentCellInnerIndex;

		// Token: 0x04003355 RID: 13141
		[NonSerialized]
		private int m_currentOuterStaticIndex;

		// Token: 0x04003356 RID: 13142
		[NonSerialized]
		private int m_currentInnerStaticIndex;

		// Token: 0x04003357 RID: 13143
		[NonSerialized]
		private int m_startPage = -1;

		// Token: 0x04003358 RID: 13144
		[NonSerialized]
		private int m_endPage = -1;
	}
}

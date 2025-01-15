using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
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
	// Token: 0x0200047E RID: 1150
	[Serializable]
	internal sealed class Chart : Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003517 RID: 13591 RVA: 0x000E8C1E File Offset: 0x000E6E1E
		internal Chart(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x000E8C27 File Offset: 0x000E6E27
		internal Chart(int id, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(id, parent)
		{
		}

		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x000E8C31 File Offset: 0x000E6E31
		internal override Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart;
			}
		}

		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x0600351A RID: 13594 RVA: 0x000E8C35 File Offset: 0x000E6E35
		internal override HierarchyNodeList ColumnMembers
		{
			get
			{
				return this.m_categoryMembers;
			}
		}

		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x000E8C3D File Offset: 0x000E6E3D
		internal override HierarchyNodeList RowMembers
		{
			get
			{
				return this.m_seriesMembers;
			}
		}

		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x0600351C RID: 13596 RVA: 0x000E8C45 File Offset: 0x000E6E45
		public override bool IsColumnGroupingSwitched
		{
			get
			{
				return this.m_columnGroupingIsSwitched;
			}
		}

		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x000E8C4D File Offset: 0x000E6E4D
		internal override RowList Rows
		{
			get
			{
				return this.m_chartSeriesCollection;
			}
		}

		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x0600351E RID: 13598 RVA: 0x000E8C55 File Offset: 0x000E6E55
		// (set) Token: 0x0600351F RID: 13599 RVA: 0x000E8C5D File Offset: 0x000E6E5D
		internal ChartMemberList CategoryMembers
		{
			get
			{
				return this.m_categoryMembers;
			}
			set
			{
				this.m_categoryMembers = value;
			}
		}

		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x06003520 RID: 13600 RVA: 0x000E8C66 File Offset: 0x000E6E66
		// (set) Token: 0x06003521 RID: 13601 RVA: 0x000E8C6E File Offset: 0x000E6E6E
		internal ChartMemberList SeriesMembers
		{
			get
			{
				return this.m_seriesMembers;
			}
			set
			{
				this.m_seriesMembers = value;
			}
		}

		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x06003522 RID: 13602 RVA: 0x000E8C77 File Offset: 0x000E6E77
		// (set) Token: 0x06003523 RID: 13603 RVA: 0x000E8C7F File Offset: 0x000E6E7F
		internal ChartSeriesList ChartSeriesCollection
		{
			get
			{
				return this.m_chartSeriesCollection;
			}
			set
			{
				this.m_chartSeriesCollection = value;
			}
		}

		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x06003524 RID: 13604 RVA: 0x000E8C88 File Offset: 0x000E6E88
		// (set) Token: 0x06003525 RID: 13605 RVA: 0x000E8C90 File Offset: 0x000E6E90
		internal List<ChartDerivedSeries> DerivedSeriesCollection
		{
			get
			{
				return this.m_chartDerivedSeriesCollection;
			}
			set
			{
				this.m_chartDerivedSeriesCollection = value;
			}
		}

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x000E8C9C File Offset: 0x000E6E9C
		internal bool HasStaticColumns
		{
			get
			{
				if (this.m_hasStaticColumns != null)
				{
					return this.m_hasStaticColumns.Value;
				}
				if (this.m_categoryMembers == null || this.m_categoryMembers.Count == 0)
				{
					return false;
				}
				if (this.m_categoryMembers.Count > 1)
				{
					this.m_hasStaticColumns = new bool?(true);
				}
				ChartMember chartMember = this.m_categoryMembers[0];
				this.m_hasStaticColumns = new bool?(this.ContainsStatic(chartMember));
				return this.m_hasStaticColumns.Value;
			}
		}

		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x06003527 RID: 13607 RVA: 0x000E8D20 File Offset: 0x000E6F20
		internal bool HasStaticRows
		{
			get
			{
				if (this.m_hasStaticRows != null)
				{
					return this.m_hasStaticRows.Value;
				}
				if (this.m_seriesMembers == null || this.m_seriesMembers.Count == 0)
				{
					return false;
				}
				if (this.m_seriesMembers.Count > 1)
				{
					this.m_hasStaticRows = new bool?(true);
				}
				ChartMember chartMember = this.m_seriesMembers[0];
				this.m_hasStaticRows = new bool?(this.ContainsStatic(chartMember));
				return this.m_hasStaticRows.Value;
			}
		}

		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x06003528 RID: 13608 RVA: 0x000E8DA1 File Offset: 0x000E6FA1
		// (set) Token: 0x06003529 RID: 13609 RVA: 0x000E8DA9 File Offset: 0x000E6FA9
		internal ExpressionInfo DynamicWidth
		{
			get
			{
				return this.m_dynamicWidth;
			}
			set
			{
				this.m_dynamicWidth = value;
			}
		}

		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x0600352A RID: 13610 RVA: 0x000E8DB2 File Offset: 0x000E6FB2
		// (set) Token: 0x0600352B RID: 13611 RVA: 0x000E8DBA File Offset: 0x000E6FBA
		internal ExpressionInfo DynamicHeight
		{
			get
			{
				return this.m_dynamicHeight;
			}
			set
			{
				this.m_dynamicHeight = value;
			}
		}

		// Token: 0x170017A9 RID: 6057
		// (get) Token: 0x0600352C RID: 13612 RVA: 0x000E8DC3 File Offset: 0x000E6FC3
		// (set) Token: 0x0600352D RID: 13613 RVA: 0x000E8DCB File Offset: 0x000E6FCB
		internal List<ChartArea> ChartAreas
		{
			get
			{
				return this.m_chartAreas;
			}
			set
			{
				this.m_chartAreas = value;
			}
		}

		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x0600352E RID: 13614 RVA: 0x000E8DD4 File Offset: 0x000E6FD4
		// (set) Token: 0x0600352F RID: 13615 RVA: 0x000E8DDC File Offset: 0x000E6FDC
		internal List<ChartLegend> Legends
		{
			get
			{
				return this.m_legends;
			}
			set
			{
				this.m_legends = value;
			}
		}

		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x06003530 RID: 13616 RVA: 0x000E8DE5 File Offset: 0x000E6FE5
		// (set) Token: 0x06003531 RID: 13617 RVA: 0x000E8DED File Offset: 0x000E6FED
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle> Titles
		{
			get
			{
				return this.m_titles;
			}
			set
			{
				this.m_titles = value;
			}
		}

		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x06003532 RID: 13618 RVA: 0x000E8DF6 File Offset: 0x000E6FF6
		// (set) Token: 0x06003533 RID: 13619 RVA: 0x000E8DFE File Offset: 0x000E6FFE
		internal ExpressionInfo Palette
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

		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x06003534 RID: 13620 RVA: 0x000E8E07 File Offset: 0x000E7007
		// (set) Token: 0x06003535 RID: 13621 RVA: 0x000E8E0F File Offset: 0x000E700F
		internal ExpressionInfo PaletteHatchBehavior
		{
			get
			{
				return this.m_paletteHatchBehavior;
			}
			set
			{
				this.m_paletteHatchBehavior = value;
			}
		}

		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x06003536 RID: 13622 RVA: 0x000E8E18 File Offset: 0x000E7018
		// (set) Token: 0x06003537 RID: 13623 RVA: 0x000E8E20 File Offset: 0x000E7020
		internal DataValueList CodeParameters
		{
			get
			{
				return this.m_codeParameters;
			}
			set
			{
				this.m_codeParameters = value;
			}
		}

		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x06003538 RID: 13624 RVA: 0x000E8E29 File Offset: 0x000E7029
		// (set) Token: 0x06003539 RID: 13625 RVA: 0x000E8E31 File Offset: 0x000E7031
		internal List<ChartCustomPaletteColor> CustomPaletteColors
		{
			get
			{
				return this.m_customPaletteColors;
			}
			set
			{
				this.m_customPaletteColors = value;
			}
		}

		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x0600353A RID: 13626 RVA: 0x000E8E3A File Offset: 0x000E703A
		// (set) Token: 0x0600353B RID: 13627 RVA: 0x000E8E42 File Offset: 0x000E7042
		internal ChartBorderSkin BorderSkin
		{
			get
			{
				return this.m_borderSkin;
			}
			set
			{
				this.m_borderSkin = value;
			}
		}

		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x0600353C RID: 13628 RVA: 0x000E8E4B File Offset: 0x000E704B
		// (set) Token: 0x0600353D RID: 13629 RVA: 0x000E8E53 File Offset: 0x000E7053
		internal ChartNoDataMessage NoDataMessage
		{
			get
			{
				return this.m_noDataMessage;
			}
			set
			{
				this.m_noDataMessage = value;
			}
		}

		// Token: 0x170017B2 RID: 6066
		// (get) Token: 0x0600353E RID: 13630 RVA: 0x000E8E5C File Offset: 0x000E705C
		internal ChartExprHost ChartExprHost
		{
			get
			{
				return this.m_chartExprHost;
			}
		}

		// Token: 0x170017B3 RID: 6067
		// (get) Token: 0x0600353F RID: 13631 RVA: 0x000E8E64 File Offset: 0x000E7064
		protected override IndexedExprHost UserSortExpressionsHost
		{
			get
			{
				if (this.m_chartExprHost == null)
				{
					return null;
				}
				return this.m_chartExprHost.UserSortExpressionsHost;
			}
		}

		// Token: 0x170017B4 RID: 6068
		// (get) Token: 0x06003540 RID: 13632 RVA: 0x000E8E7B File Offset: 0x000E707B
		// (set) Token: 0x06003541 RID: 13633 RVA: 0x000E8E83 File Offset: 0x000E7083
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

		// Token: 0x170017B5 RID: 6069
		// (get) Token: 0x06003542 RID: 13634 RVA: 0x000E8E8C File Offset: 0x000E708C
		// (set) Token: 0x06003543 RID: 13635 RVA: 0x000E8E94 File Offset: 0x000E7094
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

		// Token: 0x170017B6 RID: 6070
		// (get) Token: 0x06003544 RID: 13636 RVA: 0x000E8E9D File Offset: 0x000E709D
		// (set) Token: 0x06003545 RID: 13637 RVA: 0x000E8EA5 File Offset: 0x000E70A5
		internal int SeriesCount
		{
			get
			{
				return base.RowCount;
			}
			set
			{
				base.RowCount = value;
			}
		}

		// Token: 0x170017B7 RID: 6071
		// (get) Token: 0x06003546 RID: 13638 RVA: 0x000E8EAE File Offset: 0x000E70AE
		// (set) Token: 0x06003547 RID: 13639 RVA: 0x000E8EB6 File Offset: 0x000E70B6
		internal int CategoryCount
		{
			get
			{
				return base.ColumnCount;
			}
			set
			{
				base.ColumnCount = value;
			}
		}

		// Token: 0x170017B8 RID: 6072
		// (get) Token: 0x06003548 RID: 13640 RVA: 0x000E8EBF File Offset: 0x000E70BF
		internal bool DataValueSequenceRendering
		{
			get
			{
				return this.m_dataValueSequenceRendering;
			}
		}

		// Token: 0x170017B9 RID: 6073
		// (get) Token: 0x06003549 RID: 13641 RVA: 0x000E8EC7 File Offset: 0x000E70C7
		// (set) Token: 0x0600354A RID: 13642 RVA: 0x000E8ECF File Offset: 0x000E70CF
		internal bool EnableCategoryDrilldown
		{
			get
			{
				return this.m_enableCategoryDrilldown;
			}
			set
			{
				this.m_enableCategoryDrilldown = value;
			}
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x000E8ED8 File Offset: 0x000E70D8
		internal void SetColumnGroupingDirection(bool isOuter)
		{
			this.m_columnGroupingIsSwitched = isOuter;
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000E8EE4 File Offset: 0x000E70E4
		private bool ContainsStatic(ChartMember member)
		{
			while (member != null)
			{
				if (member.Grouping == null)
				{
					return true;
				}
				if (member.ChartMembers != null && member.ChartMembers.Count > 0)
				{
					if (member.ChartMembers.Count > 1)
					{
						return true;
					}
					member = member.ChartMembers[0];
				}
				else
				{
					member = null;
				}
			}
			return false;
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x000E8F3C File Offset: 0x000E713C
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			if ((context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDetail) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 && (context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InGrouping) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsDataRegionInDetailList, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
			}
			else
			{
				if (!context.RegisterDataRegion(this))
				{
					return false;
				}
				context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataSet | Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion;
				context.ExprHostBuilder.DataRegionStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.Chart, this.m_name);
				base.Initialize(context);
				base.ExprHostID = context.ExprHostBuilder.DataRegionEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.Chart);
				context.UnRegisterDataRegion(this);
			}
			return false;
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x000E8FF2 File Offset: 0x000E71F2
		protected override bool InitializeMembers(InitializationContext context)
		{
			bool flag = base.InitializeMembers(context);
			if (flag)
			{
				bool hasSeriesPlotTypeLine = this.m_hasSeriesPlotTypeLine;
			}
			return flag;
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000E9008 File Offset: 0x000E7208
		protected override void InitializeCorner(InitializationContext context)
		{
			if (this.m_chartAreas != null)
			{
				for (int i = 0; i < this.m_chartAreas.Count; i++)
				{
					this.m_chartAreas[i].Initialize(context);
				}
			}
			if (this.m_legends != null)
			{
				for (int j = 0; j < this.m_legends.Count; j++)
				{
					this.m_legends[j].Initialize(context);
				}
			}
			if (this.m_titles != null)
			{
				for (int k = 0; k < this.m_titles.Count; k++)
				{
					this.m_titles[k].Initialize(context);
				}
			}
			if (this.m_codeParameters != null)
			{
				this.m_codeParameters.Initialize("CodeParameters", context);
			}
			if (this.m_customPaletteColors != null)
			{
				for (int l = 0; l < this.m_customPaletteColors.Count; l++)
				{
					this.m_customPaletteColors[l].Initialize(context, l);
				}
			}
			if (this.m_borderSkin != null)
			{
				this.m_borderSkin.Initialize(context);
			}
			if (this.m_noDataMessage != null)
			{
				this.m_noDataMessage.Initialize(context);
			}
			if (this.m_palette != null)
			{
				this.m_palette.Initialize("Palette", context);
				context.ExprHostBuilder.ChartPalette(this.m_palette);
			}
			if (this.m_dynamicHeight != null)
			{
				this.m_dynamicHeight.Initialize("DynamicHeight", context);
				context.ExprHostBuilder.DynamicHeight(this.m_dynamicHeight);
			}
			if (this.m_dynamicWidth != null)
			{
				this.m_dynamicWidth.Initialize("DynamicWidth", context);
				context.ExprHostBuilder.DynamicWidth(this.m_dynamicWidth);
			}
			if (this.m_paletteHatchBehavior != null)
			{
				this.m_paletteHatchBehavior.Initialize("PaletteHatchBehavior", context);
				context.ExprHostBuilder.ChartPaletteHatchBehavior(this.m_paletteHatchBehavior);
			}
			this.m_dataValueSequenceRendering = this.CalculateDataValueSequenceRendering();
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000E91D0 File Offset: 0x000E73D0
		protected override bool ValidateInnerStructure(InitializationContext context)
		{
			if (this.m_chartSeriesCollection == null || this.m_chartSeriesCollection.Count == 0)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsMissingChartDataPoints, Severity.Error, context.ObjectType, context.ObjectName, "ChartData", Array.Empty<string>());
				return false;
			}
			return true;
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000E9220 File Offset: 0x000E7420
		private bool CalculateDataValueSequenceRendering()
		{
			if (this.m_customProperties != null && this.m_chartSeriesCollection != null)
			{
				for (int i = 0; i < this.m_customProperties.Count; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataValue dataValue = this.m_customProperties[i];
					if (dataValue != null)
					{
						ExpressionInfo name = dataValue.Name;
						ExpressionInfo value = dataValue.Value;
						if (name != null && value != null && !name.IsExpression && !value.IsExpression && name.StringValue == "__Upgraded2005__" && value.StringValue == "__Upgraded2005__")
						{
							for (int j = 0; j < this.m_chartSeriesCollection.Count; j++)
							{
								ChartSeries chartSeries = this.m_chartSeriesCollection[j];
								if (chartSeries.Type != null)
								{
									if (chartSeries.Type.IsExpression || (chartSeries.Subtype != null && chartSeries.Subtype.IsExpression))
									{
										return false;
									}
									if (!this.IsYukonDataRendererType(chartSeries.Type.StringValue, (chartSeries.Subtype != null) ? chartSeries.Subtype.StringValue : null))
									{
										return false;
									}
								}
							}
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000E935C File Offset: 0x000E755C
		private bool IsYukonDataRendererType(string type, string subType)
		{
			return Microsoft.ReportingServices.ReportPublishing.Validator.CompareWithInvariantCulture(type, "Column") || Microsoft.ReportingServices.ReportPublishing.Validator.CompareWithInvariantCulture(type, "Bar") || Microsoft.ReportingServices.ReportPublishing.Validator.CompareWithInvariantCulture(type, "Line") || Microsoft.ReportingServices.ReportPublishing.Validator.CompareWithInvariantCulture(type, "Shape") || Microsoft.ReportingServices.ReportPublishing.Validator.CompareWithInvariantCulture(type, "Scatter") || Microsoft.ReportingServices.ReportPublishing.Validator.CompareWithInvariantCulture(type, "Area") || (Microsoft.ReportingServices.ReportPublishing.Validator.CompareWithInvariantCulture(type, "Range") && (subType == null || Microsoft.ReportingServices.ReportPublishing.Validator.CompareWithInvariantCulture(subType, "Stock") || Microsoft.ReportingServices.ReportPublishing.Validator.CompareWithInvariantCulture(subType, "CandleStick")));
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000E93E6 File Offset: 0x000E75E6
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint GetDataPoint(int seriesIndex, int categoryIndex)
		{
			return this.m_chartSeriesCollection[seriesIndex].DataPoints[categoryIndex];
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000E9400 File Offset: 0x000E7600
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint GetDataPoint(int cellIndex)
		{
			int num = cellIndex / this.CategoryCount;
			int num2 = cellIndex % this.CategoryCount;
			return this.m_chartSeriesCollection[num].DataPoints[num2];
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000E9438 File Offset: 0x000E7638
		internal ChartMember GetChartMember(ChartSeries chartSeries)
		{
			ChartMember chartMember;
			try
			{
				int num = this.m_chartSeriesCollection.IndexOf(chartSeries);
				chartMember = this.GetChartMember(this.m_seriesMembers, num);
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				chartMember = null;
			}
			return chartMember;
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000E9480 File Offset: 0x000E7680
		internal ChartMember GetChartMember(ChartMemberList chartMemberList, int memberCellIndex)
		{
			foreach (object obj in chartMemberList)
			{
				ChartMember chartMember = (ChartMember)obj;
				if (chartMember.ChartMembers == null)
				{
					if (chartMember.MemberCellIndex == memberCellIndex)
					{
						return chartMember;
					}
				}
				else
				{
					ChartMember chartMember2 = this.GetChartMember(chartMember.ChartMembers, memberCellIndex);
					if (chartMember2 != null)
					{
						return chartMember2;
					}
				}
			}
			return null;
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x000E9500 File Offset: 0x000E7700
		internal List<ChartDerivedSeries> GetChildrenDerivedSeries(string chartSeriesName)
		{
			if (this.m_chartDerivedSeriesCollection == null)
			{
				return null;
			}
			List<ChartDerivedSeries> list = null;
			foreach (ChartDerivedSeries chartDerivedSeries in this.m_chartDerivedSeriesCollection)
			{
				if (ReportProcessing.CompareWithInvariantCulture(chartDerivedSeries.SourceChartSeriesName, chartSeriesName, false) == 0)
				{
					if (list == null)
					{
						list = new List<ChartDerivedSeries>();
					}
					list.Add(chartDerivedSeries);
				}
			}
			return list;
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x000E9578 File Offset: 0x000E7778
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)base.PublishClone(context);
			context.CurrentDataRegionClone = chart;
			if (this.m_categoryMembers != null)
			{
				chart.m_categoryMembers = new ChartMemberList(this.m_categoryMembers.Count);
				foreach (object obj in this.m_categoryMembers)
				{
					ChartMember chartMember = (ChartMember)obj;
					chart.m_categoryMembers.Add(chartMember.PublishClone(context, chart));
				}
			}
			if (this.m_seriesMembers != null)
			{
				chart.m_seriesMembers = new ChartMemberList(this.m_seriesMembers.Count);
				foreach (object obj2 in this.m_seriesMembers)
				{
					ChartMember chartMember2 = (ChartMember)obj2;
					chart.m_seriesMembers.Add(chartMember2.PublishClone(context, chart));
				}
			}
			if (this.m_chartSeriesCollection != null)
			{
				chart.m_chartSeriesCollection = new ChartSeriesList(this.m_chartSeriesCollection.Count);
				foreach (object obj3 in this.m_chartSeriesCollection)
				{
					ChartSeries chartSeries = (ChartSeries)obj3;
					chart.m_chartSeriesCollection.Add((ChartSeries)chartSeries.PublishClone(context));
				}
			}
			if (this.m_chartDerivedSeriesCollection != null)
			{
				chart.m_chartDerivedSeriesCollection = new List<ChartDerivedSeries>(this.m_chartDerivedSeriesCollection.Count);
				foreach (ChartDerivedSeries chartDerivedSeries in this.m_chartDerivedSeriesCollection)
				{
					chart.m_chartDerivedSeriesCollection.Add((ChartDerivedSeries)chartDerivedSeries.PublishClone(context));
				}
			}
			if (this.m_chartAreas != null)
			{
				chart.m_chartAreas = new List<ChartArea>(this.m_chartAreas.Count);
				foreach (ChartArea chartArea in this.m_chartAreas)
				{
					chart.m_chartAreas.Add((ChartArea)chartArea.PublishClone(context));
				}
			}
			if (this.m_legends != null)
			{
				chart.m_legends = new List<ChartLegend>(this.m_legends.Count);
				foreach (ChartLegend chartLegend in this.m_legends)
				{
					chart.m_legends.Add((ChartLegend)chartLegend.PublishClone(context));
				}
			}
			if (this.m_titles != null)
			{
				chart.m_titles = new List<Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle>(this.m_titles.Count);
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle chartTitle in this.m_titles)
				{
					chart.m_titles.Add((Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle)chartTitle.PublishClone(context));
				}
			}
			if (this.m_codeParameters != null)
			{
				chart.m_codeParameters = new DataValueList(this.m_codeParameters.Count);
				foreach (object obj4 in this.m_codeParameters)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataValue dataValue = (Microsoft.ReportingServices.ReportIntermediateFormat.DataValue)obj4;
					chart.m_codeParameters.Add((Microsoft.ReportingServices.ReportIntermediateFormat.DataValue)dataValue.PublishClone(context));
				}
			}
			if (this.m_customPaletteColors != null)
			{
				chart.m_customPaletteColors = new List<ChartCustomPaletteColor>(this.m_customPaletteColors.Count);
				foreach (ChartCustomPaletteColor chartCustomPaletteColor in this.m_customPaletteColors)
				{
					chart.m_customPaletteColors.Add((ChartCustomPaletteColor)chartCustomPaletteColor.PublishClone(context));
				}
			}
			if (this.m_noDataMessage != null)
			{
				chart.m_noDataMessage = (ChartNoDataMessage)this.m_noDataMessage.PublishClone(context);
			}
			if (this.m_borderSkin != null)
			{
				chart.m_borderSkin = (ChartBorderSkin)this.m_borderSkin.PublishClone(context);
			}
			if (this.m_dynamicHeight != null)
			{
				chart.m_dynamicHeight = (ExpressionInfo)this.m_dynamicHeight.PublishClone(context);
			}
			if (this.m_dynamicWidth != null)
			{
				chart.m_dynamicWidth = (ExpressionInfo)this.m_dynamicWidth.PublishClone(context);
			}
			if (this.m_palette != null)
			{
				chart.m_palette = (ExpressionInfo)this.m_palette.PublishClone(context);
			}
			if (this.m_paletteHatchBehavior != null)
			{
				chart.m_paletteHatchBehavior = (ExpressionInfo)this.m_paletteHatchBehavior.PublishClone(context);
			}
			return chart;
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x000E9A74 File Offset: 0x000E7C74
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Chart, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegion, new List<MemberInfo>
			{
				new MemberInfo(MemberName.CategoryMembers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartMember),
				new MemberInfo(MemberName.SeriesMembers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartMember),
				new MemberInfo(MemberName.ChartSeriesCollection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartSeries),
				new MemberInfo(MemberName.ChartDerivedSeriesCollection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDerivedSeries),
				new MemberInfo(MemberName.Palette, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ChartAreas, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartArea),
				new MemberInfo(MemberName.ChartLegends, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegend),
				new MemberInfo(MemberName.Titles, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartTitle),
				new MemberInfo(MemberName.CodeParameters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataValue),
				new MemberInfo(MemberName.CustomPaletteColors, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartCustomPaletteColor),
				new MemberInfo(MemberName.BorderSkin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartBorderSkin),
				new MemberInfo(MemberName.NoDataMessage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartNoDataMessage),
				new MemberInfo(MemberName.DynamicHeight, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DynamicWidth, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DataValueSequenceRendering, Token.Boolean),
				new MemberInfo(MemberName.PaletteHatchBehavior, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ColumnGroupingIsSwitched, Token.Boolean),
				new MemberInfo(MemberName.EnableCategoryDrilldown, Token.Boolean, Lifetime.AddedIn(200))
			});
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000E9C24 File Offset: 0x000E7E24
		internal int GenerateActionOwnerID()
		{
			int num = this.m_actionOwnerCounter + 1;
			this.m_actionOwnerCounter = num;
			return num;
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000E9C44 File Offset: 0x000E7E44
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Chart.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.CategoryMembers)
				{
					if (memberName <= MemberName.ChartDerivedSeriesCollection)
					{
						switch (memberName)
						{
						case MemberName.ChartSeriesCollection:
							writer.Write(this.m_chartSeriesCollection);
							continue;
						case MemberName.ChartAreas:
							writer.Write<ChartArea>(this.m_chartAreas);
							continue;
						case MemberName.Titles:
							writer.Write<Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle>(this.m_titles);
							continue;
						case MemberName.AxisTitle:
						case MemberName.LegendTitle:
							break;
						case MemberName.BorderSkin:
							writer.Write(this.m_borderSkin);
							continue;
						default:
							switch (memberName)
							{
							case MemberName.CustomPaletteColors:
								writer.Write<ChartCustomPaletteColor>(this.m_customPaletteColors);
								continue;
							case MemberName.CustomPaletteColor:
							case MemberName.Color:
								break;
							case MemberName.CodeParameters:
								writer.Write(this.m_codeParameters);
								continue;
							case MemberName.NoDataMessage:
								writer.Write(this.m_noDataMessage);
								continue;
							default:
								if (memberName == MemberName.ChartDerivedSeriesCollection)
								{
									writer.Write<ChartDerivedSeries>(this.m_chartDerivedSeriesCollection);
									continue;
								}
								break;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.ChartLegends)
						{
							writer.Write<ChartLegend>(this.m_legends);
							continue;
						}
						if (memberName == MemberName.Palette)
						{
							writer.Write(this.m_palette);
							continue;
						}
						if (memberName == MemberName.CategoryMembers)
						{
							writer.Write(this.m_categoryMembers);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.DynamicWidth)
				{
					if (memberName == MemberName.SeriesMembers)
					{
						writer.Write(this.m_seriesMembers);
						continue;
					}
					if (memberName == MemberName.DynamicHeight)
					{
						writer.Write(this.m_dynamicHeight);
						continue;
					}
					if (memberName == MemberName.DynamicWidth)
					{
						writer.Write(this.m_dynamicWidth);
						continue;
					}
				}
				else if (memberName <= MemberName.PaletteHatchBehavior)
				{
					if (memberName == MemberName.DataValueSequenceRendering)
					{
						writer.Write(this.m_dataValueSequenceRendering);
						continue;
					}
					if (memberName == MemberName.PaletteHatchBehavior)
					{
						writer.Write(this.m_paletteHatchBehavior);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.ColumnGroupingIsSwitched)
					{
						writer.Write(this.m_columnGroupingIsSwitched);
						continue;
					}
					if (memberName == MemberName.EnableCategoryDrilldown)
					{
						writer.Write(this.m_enableCategoryDrilldown);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000E9EAC File Offset: 0x000E80AC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Chart.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.CategoryMembers)
				{
					if (memberName <= MemberName.ChartDerivedSeriesCollection)
					{
						switch (memberName)
						{
						case MemberName.ChartSeriesCollection:
							this.m_chartSeriesCollection = reader.ReadListOfRIFObjects<ChartSeriesList>();
							continue;
						case MemberName.ChartAreas:
							this.m_chartAreas = reader.ReadGenericListOfRIFObjects<ChartArea>();
							continue;
						case MemberName.Titles:
							this.m_titles = reader.ReadGenericListOfRIFObjects<Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle>();
							continue;
						case MemberName.AxisTitle:
						case MemberName.LegendTitle:
							break;
						case MemberName.BorderSkin:
							this.m_borderSkin = (ChartBorderSkin)reader.ReadRIFObject();
							continue;
						default:
							switch (memberName)
							{
							case MemberName.CustomPaletteColors:
								this.m_customPaletteColors = reader.ReadGenericListOfRIFObjects<ChartCustomPaletteColor>();
								continue;
							case MemberName.CustomPaletteColor:
							case MemberName.Color:
								break;
							case MemberName.CodeParameters:
								this.m_codeParameters = reader.ReadListOfRIFObjects<DataValueList>();
								continue;
							case MemberName.NoDataMessage:
								this.m_noDataMessage = (ChartNoDataMessage)reader.ReadRIFObject();
								continue;
							default:
								if (memberName == MemberName.ChartDerivedSeriesCollection)
								{
									this.m_chartDerivedSeriesCollection = reader.ReadGenericListOfRIFObjects<ChartDerivedSeries>();
									continue;
								}
								break;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.ChartLegends)
						{
							this.m_legends = reader.ReadGenericListOfRIFObjects<ChartLegend>();
							continue;
						}
						if (memberName == MemberName.Palette)
						{
							this.m_palette = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.CategoryMembers)
						{
							this.m_categoryMembers = reader.ReadListOfRIFObjects<ChartMemberList>();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.DynamicWidth)
				{
					if (memberName == MemberName.SeriesMembers)
					{
						this.m_seriesMembers = reader.ReadListOfRIFObjects<ChartMemberList>();
						continue;
					}
					if (memberName == MemberName.DynamicHeight)
					{
						this.m_dynamicHeight = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.DynamicWidth)
					{
						this.m_dynamicWidth = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else if (memberName <= MemberName.PaletteHatchBehavior)
				{
					if (memberName == MemberName.DataValueSequenceRendering)
					{
						this.m_dataValueSequenceRendering = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.PaletteHatchBehavior)
					{
						this.m_paletteHatchBehavior = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.ColumnGroupingIsSwitched)
					{
						this.m_columnGroupingIsSwitched = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.EnableCategoryDrilldown)
					{
						this.m_enableCategoryDrilldown = reader.ReadBoolean();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000EA132 File Offset: 0x000E8332
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000EA13C File Offset: 0x000E833C
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Chart;
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x000EA144 File Offset: 0x000E8344
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
				this.m_chartExprHost = reportExprHost.ChartHostsRemotable[base.ExprHostID];
				base.DataRegionSetExprHost(this.m_chartExprHost, this.m_chartExprHost.SortHost, this.m_chartExprHost.FilterHostsRemotable, this.m_chartExprHost.UserSortExpressionsHost, this.m_chartExprHost.PageBreakExprHost, this.m_chartExprHost.JoinConditionExprHostsRemotable, reportObjectModel);
			}
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x000EA1D0 File Offset: 0x000E83D0
		internal override void DataRegionContentsSetExprHost(ObjectModelImpl reportObjectModel, bool traverseDataRegions)
		{
			if (this.m_chartExprHost != null)
			{
				IList<ChartAreaExprHost> chartAreasHostsRemotable = this.m_chartExprHost.ChartAreasHostsRemotable;
				if (this.m_chartAreas != null && chartAreasHostsRemotable != null)
				{
					for (int i = 0; i < this.m_chartAreas.Count; i++)
					{
						ChartArea chartArea = this.m_chartAreas[i];
						if (chartArea != null && chartArea.ExpressionHostID > -1)
						{
							chartArea.SetExprHost(chartAreasHostsRemotable[chartArea.ExpressionHostID], reportObjectModel);
						}
					}
				}
				IList<ChartTitleExprHost> titlesHostsRemotable = this.m_chartExprHost.TitlesHostsRemotable;
				if (this.m_titles != null && titlesHostsRemotable != null)
				{
					for (int j = 0; j < this.m_titles.Count; j++)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle chartTitle = this.m_titles[j];
						if (chartTitle != null && chartTitle.ExpressionHostID > -1)
						{
							chartTitle.SetExprHost(titlesHostsRemotable[chartTitle.ExpressionHostID], reportObjectModel);
						}
					}
				}
				IList<ChartLegendExprHost> legendsHostsRemotable = this.m_chartExprHost.LegendsHostsRemotable;
				if (this.m_legends != null && legendsHostsRemotable != null)
				{
					for (int k = 0; k < this.m_legends.Count; k++)
					{
						ChartLegend chartLegend = this.m_legends[k];
						if (chartLegend != null && chartLegend.ExpressionHostID > -1)
						{
							chartLegend.SetExprHost(legendsHostsRemotable[chartLegend.ExpressionHostID], reportObjectModel);
						}
					}
				}
				IList<ChartCustomPaletteColorExprHost> customPaletteColorHostsRemotable = this.m_chartExprHost.CustomPaletteColorHostsRemotable;
				if (this.m_customPaletteColors != null && customPaletteColorHostsRemotable != null)
				{
					for (int l = 0; l < this.m_customPaletteColors.Count; l++)
					{
						ChartCustomPaletteColor chartCustomPaletteColor = this.m_customPaletteColors[l];
						if (chartCustomPaletteColor != null && chartCustomPaletteColor.ExpressionHostID > -1)
						{
							chartCustomPaletteColor.SetExprHost(customPaletteColorHostsRemotable[chartCustomPaletteColor.ExpressionHostID], reportObjectModel);
						}
					}
				}
				if (this.m_codeParameters != null && this.m_chartExprHost.CodeParametersHostsRemotable != null)
				{
					this.m_codeParameters.SetExprHost(this.m_chartExprHost.CodeParametersHostsRemotable, reportObjectModel);
				}
				if (this.m_borderSkin != null && this.m_chartExprHost.BorderSkinHost != null)
				{
					this.m_borderSkin.SetExprHost(this.m_chartExprHost.BorderSkinHost, reportObjectModel);
				}
				if (this.m_noDataMessage != null && this.m_chartExprHost.NoDataMessageHost != null)
				{
					this.m_noDataMessage.SetExprHost(this.m_chartExprHost.NoDataMessageHost, reportObjectModel);
				}
				IList<ChartSeriesExprHost> seriesCollectionHostsRemotable = this.m_chartExprHost.SeriesCollectionHostsRemotable;
				IList<ChartDataPointExprHost> cellHostsRemotable = this.m_chartExprHost.CellHostsRemotable;
				Global.Tracer.Assert(this.m_chartSeriesCollection != null, "(m_chartSeriesCollection != null)");
				for (int m = 0; m < this.m_chartSeriesCollection.Count; m++)
				{
					ChartSeries chartSeries = this.m_chartSeriesCollection[m];
					Global.Tracer.Assert(chartSeries != null, "(null != series)");
					if (seriesCollectionHostsRemotable != null && chartSeries.ExpressionHostID > -1)
					{
						chartSeries.SetExprHost(seriesCollectionHostsRemotable[chartSeries.ExpressionHostID], reportObjectModel);
					}
					if (cellHostsRemotable != null)
					{
						Global.Tracer.Assert(chartSeries.DataPoints != null, "(null != series.DataPoints)");
						for (int n = 0; n < chartSeries.DataPoints.Count; n++)
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint chartDataPoint = chartSeries.DataPoints[n];
							Global.Tracer.Assert(chartDataPoint != null, "(null != dataPoint)");
							if (chartDataPoint.ExpressionHostID > -1)
							{
								chartDataPoint.SetExprHost(cellHostsRemotable[chartDataPoint.ExpressionHostID], reportObjectModel);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x000EA50D File Offset: 0x000E870D
		internal override object EvaluateNoRowsMessageExpression()
		{
			return this.m_chartExprHost.NoRowsExpr;
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x000EA51A File Offset: 0x000E871A
		internal string EvaluateDynamicWidth(Microsoft.ReportingServices.OnDemandReportRendering.ChartInstance chartInstance, OnDemandProcessingContext context)
		{
			if (this.m_dynamicWidth == null)
			{
				return null;
			}
			context.SetupContext(this, chartInstance);
			return context.ReportRuntime.EvaluateChartDynamicSizeExpression(this, this.m_dynamicWidth, "DynamicWidth", true);
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x000EA546 File Offset: 0x000E8746
		internal string EvaluateDynamicHeight(Microsoft.ReportingServices.OnDemandReportRendering.ChartInstance chartInstance, OnDemandProcessingContext context)
		{
			if (this.m_dynamicHeight == null)
			{
				return null;
			}
			context.SetupContext(this, chartInstance);
			return context.ReportRuntime.EvaluateChartDynamicSizeExpression(this, this.m_dynamicHeight, "DynamicHeight", false);
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x000EA572 File Offset: 0x000E8772
		internal ChartPalette EvaluatePalette(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return EnumTranslator.TranslateChartPalette(context.ReportRuntime.EvaluateChartPaletteExpression(this, base.Name), context.ReportRuntime);
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x000EA599 File Offset: 0x000E8799
		internal PaletteHatchBehavior EvaluatePaletteHatchBehavior(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return EnumTranslator.TranslatePaletteHatchBehavior(context.ReportRuntime.EvaluateChartPaletteHatchBehaviorExpression(this, base.Name), context.ReportRuntime);
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x000EA5C0 File Offset: 0x000E87C0
		protected override ReportHierarchyNode CreateHierarchyNode(int id)
		{
			return new ChartMember(id, this);
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000EA5C9 File Offset: 0x000E87C9
		protected override Row CreateRow(int id, int columnCount)
		{
			return new ChartSeries(this, id)
			{
				DataPoints = new ChartDataPointList(columnCount)
			};
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x000EA5DE File Offset: 0x000E87DE
		protected override Cell CreateCell(int id, int rowIndex, int colIndex)
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint(id, this);
		}

		// Token: 0x04001A31 RID: 6705
		private ChartMemberList m_categoryMembers;

		// Token: 0x04001A32 RID: 6706
		private ChartMemberList m_seriesMembers;

		// Token: 0x04001A33 RID: 6707
		private ChartSeriesList m_chartSeriesCollection;

		// Token: 0x04001A34 RID: 6708
		private List<ChartDerivedSeries> m_chartDerivedSeriesCollection;

		// Token: 0x04001A35 RID: 6709
		private ExpressionInfo m_palette;

		// Token: 0x04001A36 RID: 6710
		private ExpressionInfo m_paletteHatchBehavior;

		// Token: 0x04001A37 RID: 6711
		private List<ChartArea> m_chartAreas;

		// Token: 0x04001A38 RID: 6712
		private List<ChartLegend> m_legends;

		// Token: 0x04001A39 RID: 6713
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle> m_titles;

		// Token: 0x04001A3A RID: 6714
		private List<ChartCustomPaletteColor> m_customPaletteColors;

		// Token: 0x04001A3B RID: 6715
		private DataValueList m_codeParameters;

		// Token: 0x04001A3C RID: 6716
		private ChartBorderSkin m_borderSkin;

		// Token: 0x04001A3D RID: 6717
		private ChartNoDataMessage m_noDataMessage;

		// Token: 0x04001A3E RID: 6718
		private ExpressionInfo m_dynamicHeight;

		// Token: 0x04001A3F RID: 6719
		private ExpressionInfo m_dynamicWidth;

		// Token: 0x04001A40 RID: 6720
		private bool m_dataValueSequenceRendering;

		// Token: 0x04001A41 RID: 6721
		private bool m_columnGroupingIsSwitched;

		// Token: 0x04001A42 RID: 6722
		private bool m_enableCategoryDrilldown;

		// Token: 0x04001A43 RID: 6723
		[NonSerialized]
		private bool m_hasDataValueAggregates;

		// Token: 0x04001A44 RID: 6724
		[NonSerialized]
		private bool m_hasSeriesPlotTypeLine;

		// Token: 0x04001A45 RID: 6725
		[NonSerialized]
		private bool? m_hasStaticColumns;

		// Token: 0x04001A46 RID: 6726
		[NonSerialized]
		private bool? m_hasStaticRows;

		// Token: 0x04001A47 RID: 6727
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.Chart.GetDeclaration();

		// Token: 0x04001A48 RID: 6728
		[NonSerialized]
		private int m_actionOwnerCounter;

		// Token: 0x04001A49 RID: 6729
		[NonSerialized]
		private ChartExprHost m_chartExprHost;
	}
}

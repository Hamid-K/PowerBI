using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200048D RID: 1165
	[Serializable]
	internal sealed class ChartLegend : ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060036D4 RID: 14036 RVA: 0x000EF2D3 File Offset: 0x000ED4D3
		internal ChartLegend()
		{
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x000EF2DB File Offset: 0x000ED4DB
		internal ChartLegend(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
			: base(chart)
		{
		}

		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x060036D6 RID: 14038 RVA: 0x000EF2E4 File Offset: 0x000ED4E4
		// (set) Token: 0x060036D7 RID: 14039 RVA: 0x000EF2EC File Offset: 0x000ED4EC
		internal string LegendName
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x060036D8 RID: 14040 RVA: 0x000EF2F5 File Offset: 0x000ED4F5
		// (set) Token: 0x060036D9 RID: 14041 RVA: 0x000EF2FD File Offset: 0x000ED4FD
		internal ExpressionInfo Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x060036DA RID: 14042 RVA: 0x000EF306 File Offset: 0x000ED506
		// (set) Token: 0x060036DB RID: 14043 RVA: 0x000EF30E File Offset: 0x000ED50E
		internal ExpressionInfo Position
		{
			get
			{
				return this.m_position;
			}
			set
			{
				this.m_position = value;
			}
		}

		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x060036DC RID: 14044 RVA: 0x000EF317 File Offset: 0x000ED517
		// (set) Token: 0x060036DD RID: 14045 RVA: 0x000EF31F File Offset: 0x000ED51F
		internal ExpressionInfo Layout
		{
			get
			{
				return this.m_layout;
			}
			set
			{
				this.m_layout = value;
			}
		}

		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x060036DE RID: 14046 RVA: 0x000EF328 File Offset: 0x000ED528
		// (set) Token: 0x060036DF RID: 14047 RVA: 0x000EF330 File Offset: 0x000ED530
		internal string DockToChartArea
		{
			get
			{
				return this.m_dockToChartArea;
			}
			set
			{
				this.m_dockToChartArea = value;
			}
		}

		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x060036E0 RID: 14048 RVA: 0x000EF339 File Offset: 0x000ED539
		// (set) Token: 0x060036E1 RID: 14049 RVA: 0x000EF341 File Offset: 0x000ED541
		internal ExpressionInfo DockOutsideChartArea
		{
			get
			{
				return this.m_dockOutsideChartArea;
			}
			set
			{
				this.m_dockOutsideChartArea = value;
			}
		}

		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x060036E2 RID: 14050 RVA: 0x000EF34A File Offset: 0x000ED54A
		// (set) Token: 0x060036E3 RID: 14051 RVA: 0x000EF352 File Offset: 0x000ED552
		internal ChartLegendTitle LegendTitle
		{
			get
			{
				return this.m_chartLegendTitle;
			}
			set
			{
				this.m_chartLegendTitle = value;
			}
		}

		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x060036E4 RID: 14052 RVA: 0x000EF35B File Offset: 0x000ED55B
		// (set) Token: 0x060036E5 RID: 14053 RVA: 0x000EF363 File Offset: 0x000ED563
		internal ExpressionInfo AutoFitTextDisabled
		{
			get
			{
				return this.m_autoFitTextDisabled;
			}
			set
			{
				this.m_autoFitTextDisabled = value;
			}
		}

		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x060036E6 RID: 14054 RVA: 0x000EF36C File Offset: 0x000ED56C
		// (set) Token: 0x060036E7 RID: 14055 RVA: 0x000EF374 File Offset: 0x000ED574
		internal ExpressionInfo MinFontSize
		{
			get
			{
				return this.m_minFontSize;
			}
			set
			{
				this.m_minFontSize = value;
			}
		}

		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x060036E8 RID: 14056 RVA: 0x000EF37D File Offset: 0x000ED57D
		// (set) Token: 0x060036E9 RID: 14057 RVA: 0x000EF385 File Offset: 0x000ED585
		internal ExpressionInfo HeaderSeparator
		{
			get
			{
				return this.m_headerSeparator;
			}
			set
			{
				this.m_headerSeparator = value;
			}
		}

		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x060036EA RID: 14058 RVA: 0x000EF38E File Offset: 0x000ED58E
		// (set) Token: 0x060036EB RID: 14059 RVA: 0x000EF396 File Offset: 0x000ED596
		internal ExpressionInfo HeaderSeparatorColor
		{
			get
			{
				return this.m_headerSeparatorColor;
			}
			set
			{
				this.m_headerSeparatorColor = value;
			}
		}

		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x060036EC RID: 14060 RVA: 0x000EF39F File Offset: 0x000ED59F
		// (set) Token: 0x060036ED RID: 14061 RVA: 0x000EF3A7 File Offset: 0x000ED5A7
		internal ExpressionInfo ColumnSeparator
		{
			get
			{
				return this.m_columnSeparator;
			}
			set
			{
				this.m_columnSeparator = value;
			}
		}

		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x060036EE RID: 14062 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		// (set) Token: 0x060036EF RID: 14063 RVA: 0x000EF3B8 File Offset: 0x000ED5B8
		internal ExpressionInfo ColumnSeparatorColor
		{
			get
			{
				return this.m_columnSeparatorColor;
			}
			set
			{
				this.m_columnSeparatorColor = value;
			}
		}

		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x060036F0 RID: 14064 RVA: 0x000EF3C1 File Offset: 0x000ED5C1
		// (set) Token: 0x060036F1 RID: 14065 RVA: 0x000EF3C9 File Offset: 0x000ED5C9
		internal ExpressionInfo ColumnSpacing
		{
			get
			{
				return this.m_columnSpacing;
			}
			set
			{
				this.m_columnSpacing = value;
			}
		}

		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x060036F2 RID: 14066 RVA: 0x000EF3D2 File Offset: 0x000ED5D2
		// (set) Token: 0x060036F3 RID: 14067 RVA: 0x000EF3DA File Offset: 0x000ED5DA
		internal ExpressionInfo InterlacedRows
		{
			get
			{
				return this.m_interlacedRows;
			}
			set
			{
				this.m_interlacedRows = value;
			}
		}

		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x060036F4 RID: 14068 RVA: 0x000EF3E3 File Offset: 0x000ED5E3
		// (set) Token: 0x060036F5 RID: 14069 RVA: 0x000EF3EB File Offset: 0x000ED5EB
		internal ExpressionInfo InterlacedRowsColor
		{
			get
			{
				return this.m_interlacedRowsColor;
			}
			set
			{
				this.m_interlacedRowsColor = value;
			}
		}

		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x060036F6 RID: 14070 RVA: 0x000EF3F4 File Offset: 0x000ED5F4
		// (set) Token: 0x060036F7 RID: 14071 RVA: 0x000EF3FC File Offset: 0x000ED5FC
		internal ExpressionInfo EquallySpacedItems
		{
			get
			{
				return this.m_equallySpacedItems;
			}
			set
			{
				this.m_equallySpacedItems = value;
			}
		}

		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x000EF405 File Offset: 0x000ED605
		// (set) Token: 0x060036F9 RID: 14073 RVA: 0x000EF40D File Offset: 0x000ED60D
		internal ExpressionInfo Reversed
		{
			get
			{
				return this.m_reversed;
			}
			set
			{
				this.m_reversed = value;
			}
		}

		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x060036FA RID: 14074 RVA: 0x000EF416 File Offset: 0x000ED616
		// (set) Token: 0x060036FB RID: 14075 RVA: 0x000EF41E File Offset: 0x000ED61E
		internal ExpressionInfo MaxAutoSize
		{
			get
			{
				return this.m_maxAutoSize;
			}
			set
			{
				this.m_maxAutoSize = value;
			}
		}

		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x000EF427 File Offset: 0x000ED627
		// (set) Token: 0x060036FD RID: 14077 RVA: 0x000EF42F File Offset: 0x000ED62F
		internal ExpressionInfo TextWrapThreshold
		{
			get
			{
				return this.m_textWrapThreshold;
			}
			set
			{
				this.m_textWrapThreshold = value;
			}
		}

		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x060036FE RID: 14078 RVA: 0x000EF438 File Offset: 0x000ED638
		// (set) Token: 0x060036FF RID: 14079 RVA: 0x000EF440 File Offset: 0x000ED640
		internal List<ChartLegendCustomItem> LegendCustomItems
		{
			get
			{
				return this.m_chartLegendCustomItems;
			}
			set
			{
				this.m_chartLegendCustomItems = value;
			}
		}

		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x06003700 RID: 14080 RVA: 0x000EF449 File Offset: 0x000ED649
		// (set) Token: 0x06003701 RID: 14081 RVA: 0x000EF451 File Offset: 0x000ED651
		internal List<ChartLegendColumn> LegendColumns
		{
			get
			{
				return this.m_chartLegendColumns;
			}
			set
			{
				this.m_chartLegendColumns = value;
			}
		}

		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x06003702 RID: 14082 RVA: 0x000EF45A File Offset: 0x000ED65A
		// (set) Token: 0x06003703 RID: 14083 RVA: 0x000EF462 File Offset: 0x000ED662
		internal ChartElementPosition ChartElementPosition
		{
			get
			{
				return this.m_chartElementPosition;
			}
			set
			{
				this.m_chartElementPosition = value;
			}
		}

		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x06003704 RID: 14084 RVA: 0x000EF46B File Offset: 0x000ED66B
		internal ChartLegendExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001835 RID: 6197
		// (get) Token: 0x06003705 RID: 14085 RVA: 0x000EF473 File Offset: 0x000ED673
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x000EF47C File Offset: 0x000ED67C
		internal void SetExprHost(ChartLegendExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_chartLegendTitle != null && exprHost.TitleExprHost != null)
			{
				this.m_chartLegendTitle.SetExprHost(exprHost.TitleExprHost, reportObjectModel);
			}
			IList<ChartLegendCustomItemExprHost> chartLegendCustomItemsHostsRemotable = this.m_exprHost.ChartLegendCustomItemsHostsRemotable;
			if (this.m_chartLegendCustomItems != null && chartLegendCustomItemsHostsRemotable != null)
			{
				for (int i = 0; i < this.m_chartLegendCustomItems.Count; i++)
				{
					ChartLegendCustomItem chartLegendCustomItem = this.m_chartLegendCustomItems[i];
					if (chartLegendCustomItem != null && chartLegendCustomItem.ExpressionHostID > -1)
					{
						chartLegendCustomItem.SetExprHost(chartLegendCustomItemsHostsRemotable[chartLegendCustomItem.ExpressionHostID], reportObjectModel);
					}
				}
			}
			IList<ChartLegendColumnExprHost> chartLegendColumnsHostsRemotable = this.m_exprHost.ChartLegendColumnsHostsRemotable;
			if (this.m_chartLegendColumns != null && chartLegendColumnsHostsRemotable != null)
			{
				for (int j = 0; j < this.m_chartLegendColumns.Count; j++)
				{
					ChartLegendColumn chartLegendColumn = this.m_chartLegendColumns[j];
					if (chartLegendColumn != null && chartLegendColumn.ExpressionHostID > -1)
					{
						chartLegendColumn.SetExprHost(chartLegendColumnsHostsRemotable[chartLegendColumn.ExpressionHostID], reportObjectModel);
					}
				}
			}
			if (this.m_chartElementPosition != null && this.m_exprHost.ChartElementPositionHost != null)
			{
				this.m_chartElementPosition.SetExprHost(this.m_exprHost.ChartElementPositionHost, reportObjectModel);
			}
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x000EF5BC File Offset: 0x000ED7BC
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartLegendStart(this.m_name);
			base.Initialize(context);
			string dockToChartArea = this.m_dockToChartArea;
			if (this.m_position != null)
			{
				this.m_position.Initialize("Position", context);
				context.ExprHostBuilder.ChartLegendPosition(this.m_position);
			}
			if (this.m_layout != null)
			{
				this.m_layout.Initialize("Layout", context);
				context.ExprHostBuilder.ChartLegendLayout(this.m_layout);
			}
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.ChartLegendHidden(this.m_hidden);
			}
			if (this.m_dockOutsideChartArea != null)
			{
				this.m_dockOutsideChartArea.Initialize("DockOutsideChartArea", context);
				context.ExprHostBuilder.ChartLegendDockOutsideChartArea(this.m_dockOutsideChartArea);
			}
			if (this.m_chartLegendTitle != null)
			{
				this.m_chartLegendTitle.Initialize(context);
			}
			if (this.m_autoFitTextDisabled != null)
			{
				this.m_autoFitTextDisabled.Initialize("AutoFitTextDisabled", context);
				context.ExprHostBuilder.ChartLegendAutoFitTextDisabled(this.m_autoFitTextDisabled);
			}
			if (this.m_minFontSize != null)
			{
				this.m_minFontSize.Initialize("MinFontSize", context);
				context.ExprHostBuilder.ChartLegendMinFontSize(this.m_minFontSize);
			}
			if (this.m_headerSeparator != null)
			{
				this.m_headerSeparator.Initialize("HeaderSeparator", context);
				context.ExprHostBuilder.ChartLegendHeaderSeparator(this.m_headerSeparator);
			}
			if (this.m_headerSeparatorColor != null)
			{
				this.m_headerSeparatorColor.Initialize("HeaderSeparatorColor", context);
				context.ExprHostBuilder.ChartLegendHeaderSeparatorColor(this.m_headerSeparatorColor);
			}
			if (this.m_columnSeparator != null)
			{
				this.m_columnSeparator.Initialize("ColumnSeparator", context);
				context.ExprHostBuilder.ChartLegendColumnSeparator(this.m_columnSeparator);
			}
			if (this.m_columnSeparatorColor != null)
			{
				this.m_columnSeparatorColor.Initialize("ColumnSeparatorColor", context);
				context.ExprHostBuilder.ChartLegendColumnSeparatorColor(this.m_columnSeparatorColor);
			}
			if (this.m_columnSpacing != null)
			{
				this.m_columnSpacing.Initialize("ColumnSpacing", context);
				context.ExprHostBuilder.ChartLegendColumnSpacing(this.m_columnSpacing);
			}
			if (this.m_interlacedRows != null)
			{
				this.m_interlacedRows.Initialize("InterlacedRows", context);
				context.ExprHostBuilder.ChartLegendInterlacedRows(this.m_interlacedRows);
			}
			if (this.m_interlacedRowsColor != null)
			{
				this.m_interlacedRowsColor.Initialize("InterlacedRowsColor", context);
				context.ExprHostBuilder.ChartLegendInterlacedRowsColor(this.m_interlacedRowsColor);
			}
			if (this.m_equallySpacedItems != null)
			{
				this.m_equallySpacedItems.Initialize("EquallySpacedItems", context);
				context.ExprHostBuilder.ChartLegendEquallySpacedItems(this.m_equallySpacedItems);
			}
			if (this.m_reversed != null)
			{
				this.m_reversed.Initialize("Reversed", context);
				context.ExprHostBuilder.ChartLegendReversed(this.m_reversed);
			}
			if (this.m_maxAutoSize != null)
			{
				this.m_maxAutoSize.Initialize("MaxAutoSize", context);
				context.ExprHostBuilder.ChartLegendMaxAutoSize(this.m_maxAutoSize);
			}
			if (this.m_textWrapThreshold != null)
			{
				this.m_textWrapThreshold.Initialize("TextWrapThreshold", context);
				context.ExprHostBuilder.ChartLegendTextWrapThreshold(this.m_textWrapThreshold);
			}
			if (this.m_chartLegendCustomItems != null)
			{
				for (int i = 0; i < this.m_chartLegendCustomItems.Count; i++)
				{
					this.m_chartLegendCustomItems[i].Initialize(context);
				}
			}
			if (this.m_chartLegendColumns != null)
			{
				for (int j = 0; j < this.m_chartLegendColumns.Count; j++)
				{
					this.m_chartLegendColumns[j].Initialize(context);
				}
			}
			if (this.m_chartElementPosition != null)
			{
				this.m_chartElementPosition.Initialize(context);
			}
			this.m_exprHostID = context.ExprHostBuilder.ChartLegendEnd();
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x000EF960 File Offset: 0x000EDB60
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartLegend chartLegend = (ChartLegend)base.PublishClone(context);
			if (this.m_position != null)
			{
				chartLegend.m_position = (ExpressionInfo)this.m_position.PublishClone(context);
			}
			if (this.m_layout != null)
			{
				chartLegend.m_layout = (ExpressionInfo)this.m_layout.PublishClone(context);
			}
			if (this.m_hidden != null)
			{
				chartLegend.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
			}
			if (this.m_dockOutsideChartArea != null)
			{
				chartLegend.m_dockOutsideChartArea = (ExpressionInfo)this.m_dockOutsideChartArea.PublishClone(context);
			}
			if (this.m_chartLegendTitle != null)
			{
				chartLegend.m_chartLegendTitle = (ChartLegendTitle)this.m_chartLegendTitle.PublishClone(context);
			}
			if (this.m_autoFitTextDisabled != null)
			{
				chartLegend.m_autoFitTextDisabled = (ExpressionInfo)this.m_autoFitTextDisabled.PublishClone(context);
			}
			if (this.m_minFontSize != null)
			{
				chartLegend.m_minFontSize = (ExpressionInfo)this.m_minFontSize.PublishClone(context);
			}
			if (this.m_headerSeparator != null)
			{
				chartLegend.m_headerSeparator = (ExpressionInfo)this.m_headerSeparator.PublishClone(context);
			}
			if (this.m_headerSeparatorColor != null)
			{
				chartLegend.m_headerSeparatorColor = (ExpressionInfo)this.m_headerSeparatorColor.PublishClone(context);
			}
			if (this.m_columnSeparator != null)
			{
				chartLegend.m_columnSeparator = (ExpressionInfo)this.m_columnSeparator.PublishClone(context);
			}
			if (this.m_columnSeparatorColor != null)
			{
				chartLegend.m_columnSeparatorColor = (ExpressionInfo)this.m_columnSeparatorColor.PublishClone(context);
			}
			if (this.m_columnSpacing != null)
			{
				chartLegend.m_columnSpacing = (ExpressionInfo)this.m_columnSpacing.PublishClone(context);
			}
			if (this.m_interlacedRows != null)
			{
				chartLegend.m_interlacedRows = (ExpressionInfo)this.m_interlacedRows.PublishClone(context);
			}
			if (this.m_interlacedRowsColor != null)
			{
				chartLegend.m_interlacedRowsColor = (ExpressionInfo)this.m_interlacedRowsColor.PublishClone(context);
			}
			if (this.m_equallySpacedItems != null)
			{
				chartLegend.m_equallySpacedItems = (ExpressionInfo)this.m_equallySpacedItems.PublishClone(context);
			}
			if (this.m_reversed != null)
			{
				chartLegend.m_reversed = (ExpressionInfo)this.m_reversed.PublishClone(context);
			}
			if (this.m_maxAutoSize != null)
			{
				chartLegend.m_maxAutoSize = (ExpressionInfo)this.m_maxAutoSize.PublishClone(context);
			}
			if (this.m_textWrapThreshold != null)
			{
				chartLegend.m_textWrapThreshold = (ExpressionInfo)this.m_textWrapThreshold.PublishClone(context);
			}
			if (this.m_chartLegendCustomItems != null)
			{
				chartLegend.m_chartLegendCustomItems = new List<ChartLegendCustomItem>(this.m_chartLegendCustomItems.Count);
				foreach (ChartLegendCustomItem chartLegendCustomItem in this.m_chartLegendCustomItems)
				{
					chartLegend.m_chartLegendCustomItems.Add((ChartLegendCustomItem)chartLegendCustomItem.PublishClone(context));
				}
			}
			if (this.m_chartLegendColumns != null)
			{
				chartLegend.m_chartLegendColumns = new List<ChartLegendColumn>(this.m_chartLegendColumns.Count);
				foreach (ChartLegendColumn chartLegendColumn in this.m_chartLegendColumns)
				{
					chartLegend.m_chartLegendColumns.Add((ChartLegendColumn)chartLegendColumn.PublishClone(context));
				}
			}
			if (this.m_chartElementPosition != null)
			{
				chartLegend.m_chartElementPosition = (ChartElementPosition)this.m_chartElementPosition.PublishClone(context);
			}
			return chartLegend;
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x000EFCB0 File Offset: 0x000EDEB0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegend, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Position, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Layout, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DockToChartArea, Token.String),
				new MemberInfo(MemberName.DockOutsideChartArea, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ChartLegendTitle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegendTitle),
				new MemberInfo(MemberName.AutoFitTextDisabled, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MinFontSize, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.HeaderSeparator, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.HeaderSeparatorColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ColumnSeparator, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ColumnSeparatorColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ColumnSpacing, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.InterlacedRows, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.InterlacedRowsColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.EquallySpacedItems, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Reversed, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MaxAutoSize, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TextWrapThreshold, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ChartLegendCustomItems, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegendCustomItem),
				new MemberInfo(MemberName.ChartLegendColumns, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegendColumn),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.ChartElementPosition, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartElementPosition)
			});
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x000EFECC File Offset: 0x000EE0CC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartLegend.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ChartLegendColumns)
				{
					if (memberName <= MemberName.ColumnSpacing)
					{
						if (memberName == MemberName.Name)
						{
							writer.Write(this.m_name);
							continue;
						}
						if (memberName == MemberName.ColumnSpacing)
						{
							writer.Write(this.m_columnSpacing);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.Position)
						{
							writer.Write(this.m_position);
							continue;
						}
						if (memberName == MemberName.MinFontSize)
						{
							writer.Write(this.m_minFontSize);
							continue;
						}
						if (memberName == MemberName.ChartLegendColumns)
						{
							writer.Write<ChartLegendColumn>(this.m_chartLegendColumns);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.ExprHostID)
				{
					if (memberName == MemberName.ChartLegendCustomItems)
					{
						writer.Write<ChartLegendCustomItem>(this.m_chartLegendCustomItems);
						continue;
					}
					if (memberName == MemberName.Hidden)
					{
						writer.Write(this.m_hidden);
						continue;
					}
					if (memberName == MemberName.ExprHostID)
					{
						writer.Write(this.m_exprHostID);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Layout)
					{
						writer.Write(this.m_layout);
						continue;
					}
					switch (memberName)
					{
					case MemberName.DockToChartArea:
						writer.Write(this.m_dockToChartArea);
						continue;
					case MemberName.DockOutsideChartArea:
						writer.Write(this.m_dockOutsideChartArea);
						continue;
					case MemberName.DockOffset:
					case MemberName.TitleSeparator:
					case MemberName.AlignOrientation:
					case MemberName.ChartAlignType:
					case MemberName.AlignWithChartArea:
					case MemberName.EquallySizedAxesFont:
					case MemberName.Cursor:
					case MemberName.AxesView:
					case MemberName.InnerPlotPosition:
						break;
					case MemberName.ChartLegendTitle:
						writer.Write(this.m_chartLegendTitle);
						continue;
					case MemberName.AutoFitTextDisabled:
						writer.Write(this.m_autoFitTextDisabled);
						continue;
					case MemberName.HeaderSeparator:
						writer.Write(this.m_headerSeparator);
						continue;
					case MemberName.HeaderSeparatorColor:
						writer.Write(this.m_headerSeparatorColor);
						continue;
					case MemberName.ColumnSeparator:
						writer.Write(this.m_columnSeparator);
						continue;
					case MemberName.ColumnSeparatorColor:
						writer.Write(this.m_columnSeparatorColor);
						continue;
					case MemberName.InterlacedRows:
						writer.Write(this.m_interlacedRows);
						continue;
					case MemberName.InterlacedRowsColor:
						writer.Write(this.m_interlacedRowsColor);
						continue;
					case MemberName.EquallySpacedItems:
						writer.Write(this.m_equallySpacedItems);
						continue;
					case MemberName.Reversed:
						writer.Write(this.m_reversed);
						continue;
					case MemberName.MaxAutoSize:
						writer.Write(this.m_maxAutoSize);
						continue;
					case MemberName.TextWrapThreshold:
						writer.Write(this.m_textWrapThreshold);
						continue;
					default:
						if (memberName == MemberName.ChartElementPosition)
						{
							writer.Write(this.m_chartElementPosition);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x000F01AC File Offset: 0x000EE3AC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartLegend.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ChartLegendColumns)
				{
					if (memberName <= MemberName.ColumnSpacing)
					{
						if (memberName == MemberName.Name)
						{
							this.m_name = reader.ReadString();
							continue;
						}
						if (memberName == MemberName.ColumnSpacing)
						{
							this.m_columnSpacing = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.Position)
						{
							this.m_position = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.MinFontSize)
						{
							this.m_minFontSize = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.ChartLegendColumns)
						{
							this.m_chartLegendColumns = reader.ReadGenericListOfRIFObjects<ChartLegendColumn>();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.ExprHostID)
				{
					if (memberName == MemberName.ChartLegendCustomItems)
					{
						this.m_chartLegendCustomItems = reader.ReadGenericListOfRIFObjects<ChartLegendCustomItem>();
						continue;
					}
					if (memberName == MemberName.Hidden)
					{
						this.m_hidden = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.ExprHostID)
					{
						this.m_exprHostID = reader.ReadInt32();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Layout)
					{
						this.m_layout = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					switch (memberName)
					{
					case MemberName.DockToChartArea:
						this.m_dockToChartArea = reader.ReadString();
						continue;
					case MemberName.DockOutsideChartArea:
						this.m_dockOutsideChartArea = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.DockOffset:
					case MemberName.TitleSeparator:
					case MemberName.AlignOrientation:
					case MemberName.ChartAlignType:
					case MemberName.AlignWithChartArea:
					case MemberName.EquallySizedAxesFont:
					case MemberName.Cursor:
					case MemberName.AxesView:
					case MemberName.InnerPlotPosition:
						break;
					case MemberName.ChartLegendTitle:
						this.m_chartLegendTitle = (ChartLegendTitle)reader.ReadRIFObject();
						continue;
					case MemberName.AutoFitTextDisabled:
						this.m_autoFitTextDisabled = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.HeaderSeparator:
						this.m_headerSeparator = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.HeaderSeparatorColor:
						this.m_headerSeparatorColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.ColumnSeparator:
						this.m_columnSeparator = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.ColumnSeparatorColor:
						this.m_columnSeparatorColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.InterlacedRows:
						this.m_interlacedRows = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.InterlacedRowsColor:
						this.m_interlacedRowsColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.EquallySpacedItems:
						this.m_equallySpacedItems = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.Reversed:
						this.m_reversed = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.MaxAutoSize:
						this.m_maxAutoSize = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.TextWrapThreshold:
						this.m_textWrapThreshold = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.ChartElementPosition)
						{
							this.m_chartElementPosition = (ChartElementPosition)reader.ReadRIFObject();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x000F04EE File Offset: 0x000EE6EE
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x000F04F8 File Offset: 0x000EE6F8
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartLegend;
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x000F04FF File Offset: 0x000EE6FF
		internal bool EvaluateHidden(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendHiddenExpression(this, this.m_chart.Name, "Hidden");
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x000F052A File Offset: 0x000EE72A
		internal ChartLegendPositions EvaluatePosition(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartLegendPositions(context.ReportRuntime.EvaluateChartLegendPositionExpression(this, this.m_chart.Name, "Position"), context.ReportRuntime);
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x000F0560 File Offset: 0x000EE760
		internal ChartLegendLayouts EvaluateLayout(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartLegendLayout(context.ReportRuntime.EvaluateChartLegendLayoutExpression(this, this.m_chart.Name, "Layout"), context.ReportRuntime);
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000F0596 File Offset: 0x000EE796
		internal bool EvaluateDockOutsideChartArea(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendDockOutsideChartAreaExpression(this, this.m_chart.Name, "DockOutsideChartArea");
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x000F05C1 File Offset: 0x000EE7C1
		internal bool EvaluateAutoFitTextDisabled(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendAutoFitTextDisabledExpression(this, this.m_chart.Name, "AutoFitTextDisabled");
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x000F05EC File Offset: 0x000EE7EC
		internal string EvaluateMinFontSize(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendMinFontSizeExpression(this, this.m_chart.Name, "MinFontSize");
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x000F0617 File Offset: 0x000EE817
		internal ChartSeparators EvaluateHeaderSeparator(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartSeparator(context.ReportRuntime.EvaluateChartLegendHeaderSeparatorExpression(this, this.m_chart.Name, "HeaderSeparator"), context.ReportRuntime);
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000F064D File Offset: 0x000EE84D
		internal string EvaluateHeaderSeparatorColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendHeaderSeparatorColorExpression(this, this.m_chart.Name, "HeaderSeparatorColor");
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x000F0678 File Offset: 0x000EE878
		internal ChartSeparators EvaluateColumnSeparator(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartSeparator(context.ReportRuntime.EvaluateChartLegendColumnSeparatorExpression(this, this.m_chart.Name, "ColumnSeparator"), context.ReportRuntime);
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x000F06AE File Offset: 0x000EE8AE
		internal string EvaluateColumnSeparatorColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendColumnSeparatorColorExpression(this, this.m_chart.Name, "ColumnSeparatorColor");
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x000F06D9 File Offset: 0x000EE8D9
		internal int EvaluateColumnSpacing(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendColumnSpacingExpression(this, this.m_chart.Name, "ColumnSpacing");
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x000F0704 File Offset: 0x000EE904
		internal bool EvaluateInterlacedRows(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendInterlacedRowsExpression(this, this.m_chart.Name, "InterlacedRows");
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000F072F File Offset: 0x000EE92F
		internal string EvaluateInterlacedRowsColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendInterlacedRowsColorExpression(this, this.m_chart.Name, "InterlacedRowsColor");
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x000F075A File Offset: 0x000EE95A
		internal bool EvaluateEquallySpacedItems(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendEquallySpacedItemsExpression(this, this.m_chart.Name, "EquallySpacedItems");
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x000F0785 File Offset: 0x000EE985
		internal ChartAutoBool EvaluateReversed(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartAutoBool(context.ReportRuntime.EvaluateChartLegendReversedExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x000F07B6 File Offset: 0x000EE9B6
		internal int EvaluateMaxAutoSize(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendMaxAutoSizeExpression(this, this.m_chart.Name, "MaxAutoSize");
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x000F07E1 File Offset: 0x000EE9E1
		internal int EvaluateTextWrapThreshold(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartLegendTextWrapThresholdExpression(this, this.m_chart.Name, "TextWrapThreshold");
		}

		// Token: 0x04001AB4 RID: 6836
		private string m_name;

		// Token: 0x04001AB5 RID: 6837
		private ExpressionInfo m_hidden;

		// Token: 0x04001AB6 RID: 6838
		private ExpressionInfo m_position;

		// Token: 0x04001AB7 RID: 6839
		private ExpressionInfo m_layout;

		// Token: 0x04001AB8 RID: 6840
		private List<ChartLegendCustomItem> m_chartLegendCustomItems;

		// Token: 0x04001AB9 RID: 6841
		private string m_dockToChartArea;

		// Token: 0x04001ABA RID: 6842
		private ExpressionInfo m_dockOutsideChartArea;

		// Token: 0x04001ABB RID: 6843
		private ChartLegendTitle m_chartLegendTitle;

		// Token: 0x04001ABC RID: 6844
		private ExpressionInfo m_autoFitTextDisabled;

		// Token: 0x04001ABD RID: 6845
		private ExpressionInfo m_minFontSize;

		// Token: 0x04001ABE RID: 6846
		private ExpressionInfo m_headerSeparator;

		// Token: 0x04001ABF RID: 6847
		private ExpressionInfo m_headerSeparatorColor;

		// Token: 0x04001AC0 RID: 6848
		private ExpressionInfo m_columnSeparator;

		// Token: 0x04001AC1 RID: 6849
		private ExpressionInfo m_columnSeparatorColor;

		// Token: 0x04001AC2 RID: 6850
		private ExpressionInfo m_columnSpacing;

		// Token: 0x04001AC3 RID: 6851
		private ExpressionInfo m_interlacedRows;

		// Token: 0x04001AC4 RID: 6852
		private ExpressionInfo m_interlacedRowsColor;

		// Token: 0x04001AC5 RID: 6853
		private ExpressionInfo m_equallySpacedItems;

		// Token: 0x04001AC6 RID: 6854
		private ExpressionInfo m_reversed;

		// Token: 0x04001AC7 RID: 6855
		private ExpressionInfo m_maxAutoSize;

		// Token: 0x04001AC8 RID: 6856
		private ExpressionInfo m_textWrapThreshold;

		// Token: 0x04001AC9 RID: 6857
		private List<ChartLegendColumn> m_chartLegendColumns;

		// Token: 0x04001ACA RID: 6858
		private int m_exprHostID;

		// Token: 0x04001ACB RID: 6859
		private ChartElementPosition m_chartElementPosition;

		// Token: 0x04001ACC RID: 6860
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartLegend.GetDeclaration();

		// Token: 0x04001ACD RID: 6861
		[NonSerialized]
		private ChartLegendExprHost m_exprHost;
	}
}

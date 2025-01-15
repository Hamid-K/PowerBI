using System;
using System.Collections.Generic;
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
	// Token: 0x02000493 RID: 1171
	[Serializable]
	internal class ChartSeries : Row, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IActionOwner, IStyleContainer, ICustomPropertiesHolder
	{
		// Token: 0x060037C6 RID: 14278 RVA: 0x000F3040 File Offset: 0x000F1240
		internal ChartSeries()
		{
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000F3048 File Offset: 0x000F1248
		internal ChartSeries(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, int id)
			: base(id)
		{
			this.m_chart = chart;
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000F3058 File Offset: 0x000F1258
		internal ChartSeries(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, ChartDerivedSeries parentDerivedSeries, int id)
			: this(chart, id)
		{
			this.m_parentDerivedSeries = parentDerivedSeries;
		}

		// Token: 0x17001865 RID: 6245
		// (get) Token: 0x060037C9 RID: 14281 RVA: 0x000F3069 File Offset: 0x000F1269
		internal override CellList Cells
		{
			get
			{
				return this.m_dataPoints;
			}
		}

		// Token: 0x17001866 RID: 6246
		// (get) Token: 0x060037CA RID: 14282 RVA: 0x000F3071 File Offset: 0x000F1271
		// (set) Token: 0x060037CB RID: 14283 RVA: 0x000F3079 File Offset: 0x000F1279
		internal ChartDataPointList DataPoints
		{
			get
			{
				return this.m_dataPoints;
			}
			set
			{
				this.m_dataPoints = value;
			}
		}

		// Token: 0x17001867 RID: 6247
		// (get) Token: 0x060037CC RID: 14284 RVA: 0x000F3082 File Offset: 0x000F1282
		internal ChartSeriesExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001868 RID: 6248
		// (get) Token: 0x060037CD RID: 14285 RVA: 0x000F308A File Offset: 0x000F128A
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x17001869 RID: 6249
		// (get) Token: 0x060037CE RID: 14286 RVA: 0x000F3092 File Offset: 0x000F1292
		// (set) Token: 0x060037CF RID: 14287 RVA: 0x000F309A File Offset: 0x000F129A
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Action Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x1700186A RID: 6250
		// (get) Token: 0x060037D0 RID: 14288 RVA: 0x000F30A3 File Offset: 0x000F12A3
		// (set) Token: 0x060037D1 RID: 14289 RVA: 0x000F30AB File Offset: 0x000F12AB
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

		// Token: 0x1700186B RID: 6251
		// (get) Token: 0x060037D2 RID: 14290 RVA: 0x000F30B4 File Offset: 0x000F12B4
		// (set) Token: 0x060037D3 RID: 14291 RVA: 0x000F30BC File Offset: 0x000F12BC
		internal ExpressionInfo HideInLegend
		{
			get
			{
				return this.m_hideInLegend;
			}
			set
			{
				this.m_hideInLegend = value;
			}
		}

		// Token: 0x1700186C RID: 6252
		// (get) Token: 0x060037D4 RID: 14292 RVA: 0x000F30C5 File Offset: 0x000F12C5
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x1700186D RID: 6253
		// (get) Token: 0x060037D5 RID: 14293 RVA: 0x000F30CD File Offset: 0x000F12CD
		// (set) Token: 0x060037D6 RID: 14294 RVA: 0x000F30D5 File Offset: 0x000F12D5
		List<string> IActionOwner.FieldsUsedInValueExpression
		{
			get
			{
				return this.m_fieldsUsedInValueExpression;
			}
			set
			{
				this.m_fieldsUsedInValueExpression = value;
			}
		}

		// Token: 0x1700186E RID: 6254
		// (get) Token: 0x060037D7 RID: 14295 RVA: 0x000F30DE File Offset: 0x000F12DE
		// (set) Token: 0x060037D8 RID: 14296 RVA: 0x000F30E6 File Offset: 0x000F12E6
		internal ExpressionInfo Type
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

		// Token: 0x1700186F RID: 6255
		// (get) Token: 0x060037D9 RID: 14297 RVA: 0x000F30EF File Offset: 0x000F12EF
		// (set) Token: 0x060037DA RID: 14298 RVA: 0x000F30F7 File Offset: 0x000F12F7
		internal ExpressionInfo Subtype
		{
			get
			{
				return this.m_subtype;
			}
			set
			{
				this.m_subtype = value;
			}
		}

		// Token: 0x17001870 RID: 6256
		// (get) Token: 0x060037DB RID: 14299 RVA: 0x000F3100 File Offset: 0x000F1300
		// (set) Token: 0x060037DC RID: 14300 RVA: 0x000F3108 File Offset: 0x000F1308
		internal ChartEmptyPoints EmptyPoints
		{
			get
			{
				return this.m_emptyPoints;
			}
			set
			{
				this.m_emptyPoints = value;
			}
		}

		// Token: 0x17001871 RID: 6257
		// (get) Token: 0x060037DD RID: 14301 RVA: 0x000F3111 File Offset: 0x000F1311
		// (set) Token: 0x060037DE RID: 14302 RVA: 0x000F3119 File Offset: 0x000F1319
		internal ExpressionInfo LegendName
		{
			get
			{
				return this.m_legendName;
			}
			set
			{
				this.m_legendName = value;
			}
		}

		// Token: 0x17001872 RID: 6258
		// (get) Token: 0x060037DF RID: 14303 RVA: 0x000F3122 File Offset: 0x000F1322
		// (set) Token: 0x060037E0 RID: 14304 RVA: 0x000F312A File Offset: 0x000F132A
		internal ExpressionInfo LegendText
		{
			get
			{
				return this.m_legendText;
			}
			set
			{
				this.m_legendText = value;
			}
		}

		// Token: 0x17001873 RID: 6259
		// (get) Token: 0x060037E1 RID: 14305 RVA: 0x000F3133 File Offset: 0x000F1333
		// (set) Token: 0x060037E2 RID: 14306 RVA: 0x000F313B File Offset: 0x000F133B
		internal ExpressionInfo ChartAreaName
		{
			get
			{
				return this.m_chartAreaName;
			}
			set
			{
				this.m_chartAreaName = value;
			}
		}

		// Token: 0x17001874 RID: 6260
		// (get) Token: 0x060037E3 RID: 14307 RVA: 0x000F3144 File Offset: 0x000F1344
		// (set) Token: 0x060037E4 RID: 14308 RVA: 0x000F314C File Offset: 0x000F134C
		internal ExpressionInfo ValueAxisName
		{
			get
			{
				return this.m_valueAxisName;
			}
			set
			{
				this.m_valueAxisName = value;
			}
		}

		// Token: 0x17001875 RID: 6261
		// (get) Token: 0x060037E5 RID: 14309 RVA: 0x000F3155 File Offset: 0x000F1355
		// (set) Token: 0x060037E6 RID: 14310 RVA: 0x000F315D File Offset: 0x000F135D
		internal ExpressionInfo CategoryAxisName
		{
			get
			{
				return this.m_categoryAxisName;
			}
			set
			{
				this.m_categoryAxisName = value;
			}
		}

		// Token: 0x17001876 RID: 6262
		// (get) Token: 0x060037E7 RID: 14311 RVA: 0x000F3166 File Offset: 0x000F1366
		// (set) Token: 0x060037E8 RID: 14312 RVA: 0x000F316E File Offset: 0x000F136E
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel DataLabel
		{
			get
			{
				return this.m_dataLabel;
			}
			set
			{
				this.m_dataLabel = value;
			}
		}

		// Token: 0x17001877 RID: 6263
		// (get) Token: 0x060037E9 RID: 14313 RVA: 0x000F3177 File Offset: 0x000F1377
		// (set) Token: 0x060037EA RID: 14314 RVA: 0x000F317F File Offset: 0x000F137F
		internal ChartMarker Marker
		{
			get
			{
				return this.m_marker;
			}
			set
			{
				this.m_marker = value;
			}
		}

		// Token: 0x17001878 RID: 6264
		// (get) Token: 0x060037EB RID: 14315 RVA: 0x000F3188 File Offset: 0x000F1388
		// (set) Token: 0x060037EC RID: 14316 RVA: 0x000F3190 File Offset: 0x000F1390
		public Microsoft.ReportingServices.ReportIntermediateFormat.Style StyleClass
		{
			get
			{
				return this.m_styleClass;
			}
			set
			{
				this.m_styleClass = value;
			}
		}

		// Token: 0x17001879 RID: 6265
		// (get) Token: 0x060037ED RID: 14317 RVA: 0x000F3199 File Offset: 0x000F1399
		// (set) Token: 0x060037EE RID: 14318 RVA: 0x000F31A1 File Offset: 0x000F13A1
		internal string Name
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

		// Token: 0x1700187A RID: 6266
		// (get) Token: 0x060037EF RID: 14319 RVA: 0x000F31AA File Offset: 0x000F13AA
		// (set) Token: 0x060037F0 RID: 14320 RVA: 0x000F31B2 File Offset: 0x000F13B2
		internal ExpressionInfo ToolTip
		{
			get
			{
				return this.m_toolTip;
			}
			set
			{
				this.m_toolTip = value;
			}
		}

		// Token: 0x1700187B RID: 6267
		// (get) Token: 0x060037F1 RID: 14321 RVA: 0x000F31BB File Offset: 0x000F13BB
		private ChartSeries SourceSeries
		{
			get
			{
				if (this.m_sourceSeries == null && this.m_parentDerivedSeries != null)
				{
					this.m_sourceSeries = this.m_parentDerivedSeries.SourceSeries;
				}
				return this.m_sourceSeries;
			}
		}

		// Token: 0x1700187C RID: 6268
		// (get) Token: 0x060037F2 RID: 14322 RVA: 0x000F31E4 File Offset: 0x000F13E4
		// (set) Token: 0x060037F3 RID: 14323 RVA: 0x000F31EC File Offset: 0x000F13EC
		internal ChartItemInLegend ChartItemInLegend
		{
			get
			{
				return this.m_chartItemInLegend;
			}
			set
			{
				this.m_chartItemInLegend = value;
			}
		}

		// Token: 0x1700187D RID: 6269
		// (get) Token: 0x060037F4 RID: 14324 RVA: 0x000F31F5 File Offset: 0x000F13F5
		private ChartMember ParentChartMember
		{
			get
			{
				if (this.m_parentChartMember == null)
				{
					if (this.SourceSeries == null)
					{
						this.m_parentChartMember = this.m_chart.GetChartMember(this);
					}
					else
					{
						this.m_parentChartMember = this.SourceSeries.ParentChartMember;
					}
				}
				return this.m_parentChartMember;
			}
		}

		// Token: 0x1700187E RID: 6270
		// (get) Token: 0x060037F5 RID: 14325 RVA: 0x000F3232 File Offset: 0x000F1432
		public override List<InstancePathItem> InstancePath
		{
			get
			{
				if (this.m_cachedInstancePath == null)
				{
					this.m_cachedInstancePath = new List<InstancePathItem>();
					if (this.ParentChartMember != null)
					{
						this.m_cachedInstancePath.AddRange(this.ParentChartMember.InstancePath);
					}
				}
				return this.m_cachedInstancePath;
			}
		}

		// Token: 0x1700187F RID: 6271
		// (get) Token: 0x060037F6 RID: 14326 RVA: 0x000F326B File Offset: 0x000F146B
		IInstancePath IStyleContainer.InstancePath
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001880 RID: 6272
		// (get) Token: 0x060037F7 RID: 14327 RVA: 0x000F326E File Offset: 0x000F146E
		IInstancePath ICustomPropertiesHolder.InstancePath
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001881 RID: 6273
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x000F3271 File Offset: 0x000F1471
		Microsoft.ReportingServices.ReportProcessing.ObjectType IStyleContainer.ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart;
			}
		}

		// Token: 0x17001882 RID: 6274
		// (get) Token: 0x060037F9 RID: 14329 RVA: 0x000F3275 File Offset: 0x000F1475
		string IStyleContainer.Name
		{
			get
			{
				return this.m_chart.Name;
			}
		}

		// Token: 0x17001883 RID: 6275
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x000F3282 File Offset: 0x000F1482
		// (set) Token: 0x060037FB RID: 14331 RVA: 0x000F328A File Offset: 0x000F148A
		internal DataValueList CustomProperties
		{
			get
			{
				return this.m_customProperties;
			}
			set
			{
				this.m_customProperties = value;
			}
		}

		// Token: 0x17001884 RID: 6276
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x000F3293 File Offset: 0x000F1493
		// (set) Token: 0x060037FD RID: 14333 RVA: 0x000F329B File Offset: 0x000F149B
		internal ChartSmartLabel ChartSmartLabel
		{
			get
			{
				return this.m_chartSmartLabel;
			}
			set
			{
				this.m_chartSmartLabel = value;
			}
		}

		// Token: 0x17001885 RID: 6277
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x000F32A4 File Offset: 0x000F14A4
		DataValueList ICustomPropertiesHolder.CustomProperties
		{
			get
			{
				return this.m_customProperties;
			}
		}

		// Token: 0x17001886 RID: 6278
		// (get) Token: 0x060037FF RID: 14335 RVA: 0x000F32AC File Offset: 0x000F14AC
		internal List<ChartDerivedSeries> ChildrenDerivedSeries
		{
			get
			{
				if (this.m_childrenDerivedSeries == null)
				{
					this.m_childrenDerivedSeries = this.m_chart.GetChildrenDerivedSeries(this.m_name);
				}
				return this.m_childrenDerivedSeries;
			}
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x000F32D4 File Offset: 0x000F14D4
		internal void SetExprHost(ChartSeriesExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_customProperties != null && this.m_exprHost.CustomPropertyHostsRemotable != null)
			{
				this.m_customProperties.SetExprHost(this.m_exprHost.CustomPropertyHostsRemotable, reportObjectModel);
			}
			if (this.m_action != null && this.m_exprHost.ActionInfoHost != null)
			{
				this.m_action.SetExprHost(this.m_exprHost.ActionInfoHost, reportObjectModel);
			}
			if (this.m_styleClass != null)
			{
				this.m_styleClass.SetStyleExprHost(this.m_exprHost);
			}
			if (this.m_chartSmartLabel != null && this.m_exprHost.SmartLabelHost != null)
			{
				this.m_chartSmartLabel.SetExprHost(this.m_exprHost.SmartLabelHost, reportObjectModel);
			}
			if (this.m_emptyPoints != null && this.m_exprHost.EmptyPointsHost != null)
			{
				this.m_emptyPoints.SetExprHost(this.m_exprHost.EmptyPointsHost, reportObjectModel);
			}
			if (this.m_action != null && this.m_exprHost.ActionInfoHost != null)
			{
				this.m_action.SetExprHost(this.m_exprHost.ActionInfoHost, reportObjectModel);
			}
			if (this.m_dataLabel != null && this.m_exprHost.DataLabelHost != null)
			{
				this.m_dataLabel.SetExprHost(this.m_exprHost.DataLabelHost, reportObjectModel);
			}
			if (this.m_marker != null && this.m_exprHost.ChartMarkerHost != null)
			{
				this.m_marker.SetExprHost(this.m_exprHost.ChartMarkerHost, reportObjectModel);
			}
			List<ChartDerivedSeries> childrenDerivedSeries = this.ChildrenDerivedSeries;
			IList<ChartDerivedSeriesExprHost> chartDerivedSeriesCollectionHostsRemotable = this.m_exprHost.ChartDerivedSeriesCollectionHostsRemotable;
			if (childrenDerivedSeries != null && chartDerivedSeriesCollectionHostsRemotable != null)
			{
				for (int i = 0; i < childrenDerivedSeries.Count; i++)
				{
					ChartDerivedSeries chartDerivedSeries = childrenDerivedSeries[i];
					if (chartDerivedSeries != null && chartDerivedSeries.ExpressionHostID > -1)
					{
						chartDerivedSeries.SetExprHost(chartDerivedSeriesCollectionHostsRemotable[chartDerivedSeries.ExpressionHostID], reportObjectModel);
					}
				}
			}
			if (this.m_chartItemInLegend != null && this.m_exprHost.DataPointInLegendHost != null)
			{
				this.m_chartItemInLegend.SetExprHost(this.m_exprHost.DataPointInLegendHost, reportObjectModel);
			}
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x000F34D8 File Offset: 0x000F16D8
		internal void Initialize(InitializationContext context, string name)
		{
			context.ExprHostBuilder.ChartSeriesStart();
			if (this.m_customProperties != null)
			{
				this.m_customProperties.Initialize("ChartSeries" + name, context);
			}
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_type == null)
			{
				this.m_type = ExpressionInfo.CreateConstExpression(ChartSeriesType.Column.ToString());
			}
			this.m_type.Initialize("Type", context);
			context.ExprHostBuilder.ChartSeriesType(this.m_type);
			if (this.m_subtype == null)
			{
				this.m_subtype = ExpressionInfo.CreateConstExpression(ChartSeriesSubtype.Plain.ToString());
			}
			this.m_subtype.Initialize("Subtype", context);
			context.ExprHostBuilder.ChartSeriesSubtype(this.m_subtype);
			if (this.m_chartSmartLabel != null)
			{
				this.m_chartSmartLabel.Initialize(context);
			}
			if (this.m_emptyPoints != null)
			{
				this.m_emptyPoints.Initialize(context);
			}
			if (this.m_legendName != null)
			{
				this.m_legendName.Initialize("LegendName", context);
				context.ExprHostBuilder.ChartSeriesLegendName(this.m_legendName);
			}
			if (this.m_legendText != null)
			{
				this.m_legendText.Initialize("LegendText", context);
				context.ExprHostBuilder.ChartSeriesLegendText(this.m_legendText);
			}
			if (this.m_chartAreaName != null)
			{
				this.m_chartAreaName.Initialize("ChartAreaName", context);
				context.ExprHostBuilder.ChartSeriesChartAreaName(this.m_chartAreaName);
			}
			if (this.m_valueAxisName != null)
			{
				this.m_valueAxisName.Initialize("ValueAxisName", context);
				context.ExprHostBuilder.ChartSeriesValueAxisName(this.m_valueAxisName);
			}
			if (this.m_categoryAxisName != null)
			{
				this.m_categoryAxisName.Initialize("CategoryAxisName", context);
				context.ExprHostBuilder.ChartSeriesCategoryAxisName(this.m_categoryAxisName);
			}
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.ChartSeriesHidden(this.m_hidden);
			}
			if (this.m_hideInLegend != null)
			{
				this.m_hideInLegend.Initialize("HideInLegend", context);
				context.ExprHostBuilder.ChartSeriesHideInLegend(this.m_hideInLegend);
			}
			if (this.m_dataLabel != null)
			{
				this.m_dataLabel.Initialize(context);
			}
			if (this.m_marker != null)
			{
				this.m_marker.Initialize(context);
			}
			List<ChartDerivedSeries> childrenDerivedSeries = this.ChildrenDerivedSeries;
			if (childrenDerivedSeries != null)
			{
				for (int i = 0; i < childrenDerivedSeries.Count; i++)
				{
					childrenDerivedSeries[i].Initialize(context, i);
				}
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.ChartSeriesToolTip(this.m_toolTip);
			}
			if (this.m_chartItemInLegend != null)
			{
				this.m_chartItemInLegend.Initialize(context);
			}
			context.ExprHostBuilder.ChartSeriesEnd();
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x000F37B4 File Offset: 0x000F19B4
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartSeries chartSeries = (ChartSeries)base.PublishClone(context);
			chartSeries.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)context.CurrentDataRegionClone;
			if (this.m_dataPoints != null)
			{
				chartSeries.m_dataPoints = new ChartDataPointList(this.m_dataPoints.Count);
				foreach (object obj in this.m_dataPoints)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint chartDataPoint = (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint)obj;
					chartSeries.m_dataPoints.Add((Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint)chartDataPoint.PublishClone(context));
				}
			}
			if (this.m_customProperties != null)
			{
				chartSeries.m_customProperties = new DataValueList(this.m_customProperties.Count);
				foreach (object obj2 in this.m_customProperties)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataValue dataValue = (Microsoft.ReportingServices.ReportIntermediateFormat.DataValue)obj2;
					chartSeries.m_customProperties.Add((Microsoft.ReportingServices.ReportIntermediateFormat.DataValue)dataValue.PublishClone(context));
				}
			}
			if (this.m_styleClass != null)
			{
				chartSeries.m_styleClass = (Microsoft.ReportingServices.ReportIntermediateFormat.Style)this.m_styleClass.PublishClone(context);
			}
			if (this.m_action != null)
			{
				chartSeries.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_type != null)
			{
				chartSeries.m_type = (ExpressionInfo)this.m_type.PublishClone(context);
			}
			if (this.m_subtype != null)
			{
				chartSeries.m_subtype = (ExpressionInfo)this.m_subtype.PublishClone(context);
			}
			if (this.m_emptyPoints != null)
			{
				chartSeries.m_emptyPoints = (ChartEmptyPoints)this.m_emptyPoints.PublishClone(context);
			}
			if (this.m_legendName != null)
			{
				chartSeries.m_legendName = (ExpressionInfo)this.m_legendName.PublishClone(context);
			}
			if (this.m_legendText != null)
			{
				chartSeries.m_legendText = (ExpressionInfo)this.m_legendText.PublishClone(context);
			}
			if (this.m_chartAreaName != null)
			{
				chartSeries.m_chartAreaName = (ExpressionInfo)this.m_chartAreaName.PublishClone(context);
			}
			if (this.m_valueAxisName != null)
			{
				chartSeries.m_valueAxisName = (ExpressionInfo)this.m_valueAxisName.PublishClone(context);
			}
			if (this.m_categoryAxisName != null)
			{
				chartSeries.m_categoryAxisName = (ExpressionInfo)this.m_categoryAxisName.PublishClone(context);
			}
			if (this.m_hidden != null)
			{
				chartSeries.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
			}
			if (this.m_hideInLegend != null)
			{
				chartSeries.m_hideInLegend = (ExpressionInfo)this.m_hideInLegend.PublishClone(context);
			}
			if (this.m_chartSmartLabel != null)
			{
				chartSeries.m_chartSmartLabel = (ChartSmartLabel)this.m_chartSmartLabel.PublishClone(context);
			}
			if (this.m_dataLabel != null)
			{
				chartSeries.m_dataLabel = (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel)this.m_dataLabel.PublishClone(context);
			}
			if (this.m_marker != null)
			{
				chartSeries.m_marker = (ChartMarker)this.m_marker.PublishClone(context);
			}
			if (this.m_toolTip != null)
			{
				chartSeries.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			if (this.m_chartItemInLegend != null)
			{
				chartSeries.m_chartItemInLegend = (ChartItemInLegend)this.m_chartItemInLegend.PublishClone(context);
			}
			return chartSeries;
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x000F3AE4 File Offset: 0x000F1CE4
		internal ChartSeriesType EvaluateType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return EnumTranslator.TranslateChartSeriesType(context.ReportRuntime.EvaluateChartSeriesTypeExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x000F3B10 File Offset: 0x000F1D10
		internal ChartSeriesSubtype EvaluateSubtype(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return EnumTranslator.TranslateChartSeriesSubtype(context.ReportRuntime.EvaluateChartSeriesSubtypeExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x000F3B3C File Offset: 0x000F1D3C
		internal string EvaluateLegendName(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartSeriesLegendNameExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x000F3B60 File Offset: 0x000F1D60
		internal string EvaluateLegendText(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = context.ReportRuntime.EvaluateChartSeriesLegendTextExpression(this, this.m_chart.Name);
			string text = null;
			if (variantResult.ErrorOccurred)
			{
				text = RPRes.rsExpressionErrorValue;
			}
			else if (variantResult.Value != null)
			{
				text = Formatter.Format(variantResult.Value, ref this.m_formatter, this.m_chart.StyleClass, this.m_styleClass, context, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, this.m_chart.Name);
			}
			return text;
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x000F3BD9 File Offset: 0x000F1DD9
		internal string EvaluateChartAreaName(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartSeriesChartAreaNameExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x000F3BFA File Offset: 0x000F1DFA
		internal string EvaluateValueAxisName(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartSeriesValueAxisNameExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x000F3C1B File Offset: 0x000F1E1B
		internal string EvaluateCategoryAxisName(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartSeriesCategoryAxisNameExpression(this, this.m_chart.Name);
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x000F3C3C File Offset: 0x000F1E3C
		internal bool EvaluateHidden(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartSeriesHiddenExpression(this, this.m_chart.Name);
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x000F3C5D File Offset: 0x000F1E5D
		internal bool EvaluateHideInLegend(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartSeriesHideInLegendExpression(this, this.m_chart.Name);
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x000F3C80 File Offset: 0x000F1E80
		internal string EvaluateToolTip(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, reportScopeInstance);
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = context.ReportRuntime.EvaluateChartSeriesToolTipExpression(this, this.m_chart.Name);
			string text = null;
			if (variantResult.ErrorOccurred)
			{
				text = RPRes.rsExpressionErrorValue;
			}
			else if (variantResult.Value != null)
			{
				text = Formatter.Format(variantResult.Value, ref this.m_formatter, this.m_chart.StyleClass, this.m_styleClass, context, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, this.m_chart.Name);
			}
			return text;
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x000F3CFC File Offset: 0x000F1EFC
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartSeries, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Row, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ChartDataPoints, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDataPoint),
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.Type, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Subtype, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.EmptyPoints, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartEmptyPoints),
				new MemberInfo(MemberName.LegendName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LegendText, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ChartAreaName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ValueAxisName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.CategoryAxisName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.StyleClass, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Style),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.Chart, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Chart, Token.Reference),
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.HideInLegend, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ChartSmartLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartSmartLabel),
				new MemberInfo(MemberName.CustomProperties, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataValue),
				new MemberInfo(MemberName.DataLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDataLabel),
				new MemberInfo(MemberName.Marker, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartMarker),
				new MemberInfo(MemberName.ChartMember, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartMember, Token.Reference),
				new MemberInfo(MemberName.SourceSeries, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartSeries, Token.Reference),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ChartItemInLegend, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartItemInLegend)
			});
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x000F3F1C File Offset: 0x000F211C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartSeries.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Type)
				{
					if (memberName <= MemberName.Marker)
					{
						if (memberName <= MemberName.StyleClass)
						{
							if (memberName == MemberName.Name)
							{
								writer.Write(this.m_name);
								continue;
							}
							if (memberName == MemberName.StyleClass)
							{
								writer.Write(this.m_styleClass);
								continue;
							}
						}
						else
						{
							if (memberName == MemberName.ToolTip)
							{
								writer.Write(this.m_toolTip);
								continue;
							}
							if (memberName == MemberName.Marker)
							{
								writer.Write(this.m_marker);
								continue;
							}
						}
					}
					else if (memberName <= MemberName.EmptyPoints)
					{
						switch (memberName)
						{
						case MemberName.DataLabel:
							writer.Write(this.m_dataLabel);
							continue;
						case MemberName.AxisLabel:
							break;
						case MemberName.ChartItemInLegend:
							writer.Write(this.m_chartItemInLegend);
							continue;
						case MemberName.LegendText:
							writer.Write(this.m_legendText);
							continue;
						default:
							if (memberName == MemberName.EmptyPoints)
							{
								writer.Write(this.m_emptyPoints);
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.Hidden)
						{
							writer.Write(this.m_hidden);
							continue;
						}
						if (memberName == MemberName.Type)
						{
							writer.Write(this.m_type);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.CategoryAxisName)
				{
					if (memberName <= MemberName.Action)
					{
						if (memberName == MemberName.ExprHostID)
						{
							writer.Write(this.m_exprHostID);
							continue;
						}
						if (memberName == MemberName.Action)
						{
							writer.Write(this.m_action);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.CustomProperties)
						{
							writer.Write(this.m_customProperties);
							continue;
						}
						switch (memberName)
						{
						case MemberName.ChartDataPoints:
							writer.Write(this.m_dataPoints);
							continue;
						case MemberName.Subtype:
							writer.Write(this.m_subtype);
							continue;
						case MemberName.LegendName:
							writer.Write(this.m_legendName);
							continue;
						case MemberName.ChartAreaName:
							writer.Write(this.m_chartAreaName);
							continue;
						case MemberName.ValueAxisName:
							writer.Write(this.m_valueAxisName);
							continue;
						case MemberName.CategoryAxisName:
							writer.Write(this.m_categoryAxisName);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.ChartSmartLabel)
				{
					if (memberName == MemberName.Chart)
					{
						writer.WriteReference(this.m_chart);
						continue;
					}
					if (memberName == MemberName.ChartSmartLabel)
					{
						writer.Write(this.m_chartSmartLabel);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.HideInLegend)
					{
						writer.Write(this.m_hideInLegend);
						continue;
					}
					if (memberName == MemberName.ChartMember)
					{
						writer.WriteReference(this.ParentChartMember);
						continue;
					}
					if (memberName == MemberName.SourceSeries)
					{
						writer.WriteReference(this.SourceSeries);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x000F423C File Offset: 0x000F243C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartSeries.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Type)
				{
					if (memberName <= MemberName.Marker)
					{
						if (memberName <= MemberName.StyleClass)
						{
							if (memberName == MemberName.Name)
							{
								this.m_name = reader.ReadString();
								continue;
							}
							if (memberName == MemberName.StyleClass)
							{
								this.m_styleClass = (Microsoft.ReportingServices.ReportIntermediateFormat.Style)reader.ReadRIFObject();
								continue;
							}
						}
						else
						{
							if (memberName == MemberName.ToolTip)
							{
								this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
								continue;
							}
							if (memberName == MemberName.Marker)
							{
								this.m_marker = (ChartMarker)reader.ReadRIFObject();
								continue;
							}
						}
					}
					else if (memberName <= MemberName.EmptyPoints)
					{
						switch (memberName)
						{
						case MemberName.DataLabel:
							this.m_dataLabel = (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel)reader.ReadRIFObject();
							continue;
						case MemberName.AxisLabel:
							break;
						case MemberName.ChartItemInLegend:
							this.m_chartItemInLegend = (ChartItemInLegend)reader.ReadRIFObject();
							continue;
						case MemberName.LegendText:
							this.m_legendText = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						default:
							if (memberName == MemberName.EmptyPoints)
							{
								this.m_emptyPoints = (ChartEmptyPoints)reader.ReadRIFObject();
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.Hidden)
						{
							this.m_hidden = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.Type)
						{
							this.m_type = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.CategoryAxisName)
				{
					if (memberName <= MemberName.Action)
					{
						if (memberName == MemberName.ExprHostID)
						{
							this.m_exprHostID = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.Action)
						{
							this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.CustomProperties)
						{
							this.m_customProperties = reader.ReadListOfRIFObjects<DataValueList>();
							continue;
						}
						switch (memberName)
						{
						case MemberName.ChartDataPoints:
							this.m_dataPoints = reader.ReadListOfRIFObjects<ChartDataPointList>();
							continue;
						case MemberName.Subtype:
							this.m_subtype = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.LegendName:
							this.m_legendName = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.ChartAreaName:
							this.m_chartAreaName = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.ValueAxisName:
							this.m_valueAxisName = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.CategoryAxisName:
							this.m_categoryAxisName = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.ChartSmartLabel)
				{
					if (memberName == MemberName.Chart)
					{
						this.m_chart = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.Chart>(this);
						continue;
					}
					if (memberName == MemberName.ChartSmartLabel)
					{
						this.m_chartSmartLabel = (ChartSmartLabel)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.HideInLegend)
					{
						this.m_hideInLegend = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.ChartMember)
					{
						this.m_parentChartMember = reader.ReadReference<ChartMember>(this);
						continue;
					}
					if (memberName == MemberName.SourceSeries)
					{
						this.m_sourceSeries = reader.ReadReference<ChartSeries>(this);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x000F45B4 File Offset: 0x000F27B4
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ChartSeries.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName != MemberName.ChartSeries)
					{
						if (memberName != MemberName.Chart)
						{
							if (memberName != MemberName.ChartMember)
							{
								Global.Tracer.Assert(false);
							}
							else
							{
								Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
								this.m_parentChartMember = (ChartMember)referenceableItems[memberReference.RefID];
							}
						}
						else
						{
							Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
							this.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)referenceableItems[memberReference.RefID];
						}
					}
					else
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_sourceSeries = (ChartSeries)referenceableItems[memberReference.RefID];
					}
				}
			}
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x000F46D4 File Offset: 0x000F28D4
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartSeries;
		}

		// Token: 0x04001B02 RID: 6914
		private ChartDataPointList m_dataPoints;

		// Token: 0x04001B03 RID: 6915
		private int m_exprHostID;

		// Token: 0x04001B04 RID: 6916
		private string m_name;

		// Token: 0x04001B05 RID: 6917
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001B06 RID: 6918
		private ExpressionInfo m_type;

		// Token: 0x04001B07 RID: 6919
		private ExpressionInfo m_subtype;

		// Token: 0x04001B08 RID: 6920
		private ChartEmptyPoints m_emptyPoints;

		// Token: 0x04001B09 RID: 6921
		private ExpressionInfo m_legendName;

		// Token: 0x04001B0A RID: 6922
		private ExpressionInfo m_legendText;

		// Token: 0x04001B0B RID: 6923
		private ExpressionInfo m_chartAreaName;

		// Token: 0x04001B0C RID: 6924
		private ExpressionInfo m_valueAxisName;

		// Token: 0x04001B0D RID: 6925
		private ExpressionInfo m_categoryAxisName;

		// Token: 0x04001B0E RID: 6926
		private Microsoft.ReportingServices.ReportIntermediateFormat.Style m_styleClass;

		// Token: 0x04001B0F RID: 6927
		private ExpressionInfo m_hidden;

		// Token: 0x04001B10 RID: 6928
		private ExpressionInfo m_hideInLegend;

		// Token: 0x04001B11 RID: 6929
		private ChartSmartLabel m_chartSmartLabel;

		// Token: 0x04001B12 RID: 6930
		private DataValueList m_customProperties;

		// Token: 0x04001B13 RID: 6931
		private Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel m_dataLabel;

		// Token: 0x04001B14 RID: 6932
		private ChartMarker m_marker;

		// Token: 0x04001B15 RID: 6933
		private ExpressionInfo m_toolTip;

		// Token: 0x04001B16 RID: 6934
		private ChartItemInLegend m_chartItemInLegend;

		// Token: 0x04001B17 RID: 6935
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Chart m_chart;

		// Token: 0x04001B18 RID: 6936
		[Reference]
		private ChartMember m_parentChartMember;

		// Token: 0x04001B19 RID: 6937
		[Reference]
		private ChartSeries m_sourceSeries;

		// Token: 0x04001B1A RID: 6938
		[NonSerialized]
		private ChartDerivedSeries m_parentDerivedSeries;

		// Token: 0x04001B1B RID: 6939
		[NonSerialized]
		private List<ChartDerivedSeries> m_childrenDerivedSeries;

		// Token: 0x04001B1C RID: 6940
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x04001B1D RID: 6941
		[NonSerialized]
		private ChartSeriesExprHost m_exprHost;

		// Token: 0x04001B1E RID: 6942
		[NonSerialized]
		private Formatter m_formatter;

		// Token: 0x04001B1F RID: 6943
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartSeries.GetDeclaration();
	}
}

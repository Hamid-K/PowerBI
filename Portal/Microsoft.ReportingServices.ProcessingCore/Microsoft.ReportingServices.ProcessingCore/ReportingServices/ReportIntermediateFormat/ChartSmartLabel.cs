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
	// Token: 0x02000490 RID: 1168
	[Serializable]
	internal sealed class ChartSmartLabel : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600375E RID: 14174 RVA: 0x000F1727 File Offset: 0x000EF927
		internal ChartSmartLabel()
		{
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x000F172F File Offset: 0x000EF92F
		internal ChartSmartLabel(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, ChartSeries chartSeries)
		{
			this.m_chart = chart;
			this.m_chartSeries = chartSeries;
		}

		// Token: 0x17001845 RID: 6213
		// (get) Token: 0x06003760 RID: 14176 RVA: 0x000F1745 File Offset: 0x000EF945
		internal ChartSmartLabelExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001846 RID: 6214
		// (get) Token: 0x06003761 RID: 14177 RVA: 0x000F174D File Offset: 0x000EF94D
		// (set) Token: 0x06003762 RID: 14178 RVA: 0x000F1755 File Offset: 0x000EF955
		internal ExpressionInfo AllowOutSidePlotArea
		{
			get
			{
				return this.m_allowOutSidePlotArea;
			}
			set
			{
				this.m_allowOutSidePlotArea = value;
			}
		}

		// Token: 0x17001847 RID: 6215
		// (get) Token: 0x06003763 RID: 14179 RVA: 0x000F175E File Offset: 0x000EF95E
		// (set) Token: 0x06003764 RID: 14180 RVA: 0x000F1766 File Offset: 0x000EF966
		internal ExpressionInfo CalloutBackColor
		{
			get
			{
				return this.m_calloutBackColor;
			}
			set
			{
				this.m_calloutBackColor = value;
			}
		}

		// Token: 0x17001848 RID: 6216
		// (get) Token: 0x06003765 RID: 14181 RVA: 0x000F176F File Offset: 0x000EF96F
		// (set) Token: 0x06003766 RID: 14182 RVA: 0x000F1777 File Offset: 0x000EF977
		internal ExpressionInfo CalloutLineAnchor
		{
			get
			{
				return this.m_calloutLineAnchor;
			}
			set
			{
				this.m_calloutLineAnchor = value;
			}
		}

		// Token: 0x17001849 RID: 6217
		// (get) Token: 0x06003767 RID: 14183 RVA: 0x000F1780 File Offset: 0x000EF980
		// (set) Token: 0x06003768 RID: 14184 RVA: 0x000F1788 File Offset: 0x000EF988
		internal ExpressionInfo CalloutLineColor
		{
			get
			{
				return this.m_calloutLineColor;
			}
			set
			{
				this.m_calloutLineColor = value;
			}
		}

		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x06003769 RID: 14185 RVA: 0x000F1791 File Offset: 0x000EF991
		// (set) Token: 0x0600376A RID: 14186 RVA: 0x000F1799 File Offset: 0x000EF999
		internal ExpressionInfo CalloutLineStyle
		{
			get
			{
				return this.m_calloutLineStyle;
			}
			set
			{
				this.m_calloutLineStyle = value;
			}
		}

		// Token: 0x1700184B RID: 6219
		// (get) Token: 0x0600376B RID: 14187 RVA: 0x000F17A2 File Offset: 0x000EF9A2
		// (set) Token: 0x0600376C RID: 14188 RVA: 0x000F17AA File Offset: 0x000EF9AA
		internal ExpressionInfo CalloutLineWidth
		{
			get
			{
				return this.m_calloutLineWidth;
			}
			set
			{
				this.m_calloutLineWidth = value;
			}
		}

		// Token: 0x1700184C RID: 6220
		// (get) Token: 0x0600376D RID: 14189 RVA: 0x000F17B3 File Offset: 0x000EF9B3
		// (set) Token: 0x0600376E RID: 14190 RVA: 0x000F17BB File Offset: 0x000EF9BB
		internal ExpressionInfo CalloutStyle
		{
			get
			{
				return this.m_calloutStyle;
			}
			set
			{
				this.m_calloutStyle = value;
			}
		}

		// Token: 0x1700184D RID: 6221
		// (get) Token: 0x0600376F RID: 14191 RVA: 0x000F17C4 File Offset: 0x000EF9C4
		// (set) Token: 0x06003770 RID: 14192 RVA: 0x000F17CC File Offset: 0x000EF9CC
		internal ExpressionInfo ShowOverlapped
		{
			get
			{
				return this.m_showOverlapped;
			}
			set
			{
				this.m_showOverlapped = value;
			}
		}

		// Token: 0x1700184E RID: 6222
		// (get) Token: 0x06003771 RID: 14193 RVA: 0x000F17D5 File Offset: 0x000EF9D5
		// (set) Token: 0x06003772 RID: 14194 RVA: 0x000F17DD File Offset: 0x000EF9DD
		internal ExpressionInfo MarkerOverlapping
		{
			get
			{
				return this.m_markerOverlapping;
			}
			set
			{
				this.m_markerOverlapping = value;
			}
		}

		// Token: 0x1700184F RID: 6223
		// (get) Token: 0x06003773 RID: 14195 RVA: 0x000F17E6 File Offset: 0x000EF9E6
		// (set) Token: 0x06003774 RID: 14196 RVA: 0x000F17EE File Offset: 0x000EF9EE
		internal ExpressionInfo MaxMovingDistance
		{
			get
			{
				return this.m_maxMovingDistance;
			}
			set
			{
				this.m_maxMovingDistance = value;
			}
		}

		// Token: 0x17001850 RID: 6224
		// (get) Token: 0x06003775 RID: 14197 RVA: 0x000F17F7 File Offset: 0x000EF9F7
		// (set) Token: 0x06003776 RID: 14198 RVA: 0x000F17FF File Offset: 0x000EF9FF
		internal ExpressionInfo MinMovingDistance
		{
			get
			{
				return this.m_minMovingDistance;
			}
			set
			{
				this.m_minMovingDistance = value;
			}
		}

		// Token: 0x17001851 RID: 6225
		// (get) Token: 0x06003777 RID: 14199 RVA: 0x000F1808 File Offset: 0x000EFA08
		// (set) Token: 0x06003778 RID: 14200 RVA: 0x000F1810 File Offset: 0x000EFA10
		internal ChartNoMoveDirections NoMoveDirections
		{
			get
			{
				return this.m_noMoveDirections;
			}
			set
			{
				this.m_noMoveDirections = value;
			}
		}

		// Token: 0x17001852 RID: 6226
		// (get) Token: 0x06003779 RID: 14201 RVA: 0x000F1819 File Offset: 0x000EFA19
		// (set) Token: 0x0600377A RID: 14202 RVA: 0x000F1821 File Offset: 0x000EFA21
		internal ExpressionInfo Disabled
		{
			get
			{
				return this.m_disabled;
			}
			set
			{
				this.m_disabled = value;
			}
		}

		// Token: 0x17001853 RID: 6227
		// (get) Token: 0x0600377B RID: 14203 RVA: 0x000F182A File Offset: 0x000EFA2A
		private IInstancePath InstancePath
		{
			get
			{
				if (this.m_chartSeries != null)
				{
					return this.m_chartSeries;
				}
				return this.m_chart;
			}
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000F1844 File Offset: 0x000EFA44
		internal void SetExprHost(ChartSmartLabelExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_noMoveDirections != null && this.m_exprHost.NoMoveDirectionsHost != null)
			{
				this.m_noMoveDirections.SetExprHost(this.m_exprHost.NoMoveDirectionsHost, reportObjectModel);
			}
		}

		// Token: 0x0600377D RID: 14205 RVA: 0x000F18AC File Offset: 0x000EFAAC
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartSmartLabelStart();
			if (this.m_allowOutSidePlotArea != null)
			{
				this.m_allowOutSidePlotArea.Initialize("AllowOutSidePlotArea", context);
				context.ExprHostBuilder.ChartSmartLabelAllowOutSidePlotArea(this.m_allowOutSidePlotArea);
			}
			if (this.m_calloutBackColor != null)
			{
				this.m_calloutBackColor.Initialize("CalloutBackColor", context);
				context.ExprHostBuilder.ChartSmartLabelCalloutBackColor(this.m_calloutBackColor);
			}
			if (this.m_calloutLineAnchor != null)
			{
				this.m_calloutLineAnchor.Initialize("CalloutLineAnchor", context);
				context.ExprHostBuilder.ChartSmartLabelCalloutLineAnchor(this.m_calloutLineAnchor);
			}
			if (this.m_calloutLineColor != null)
			{
				this.m_calloutLineColor.Initialize("CalloutLineColor", context);
				context.ExprHostBuilder.ChartSmartLabelCalloutLineColor(this.m_calloutLineColor);
			}
			if (this.m_calloutLineStyle != null)
			{
				this.m_calloutLineStyle.Initialize("CalloutLineStyle", context);
				context.ExprHostBuilder.ChartSmartLabelCalloutLineStyle(this.m_calloutLineStyle);
			}
			if (this.m_calloutLineWidth != null)
			{
				this.m_calloutLineWidth.Initialize("CalloutLineWidth", context);
				context.ExprHostBuilder.ChartSmartLabelCalloutLineWidth(this.m_calloutLineWidth);
			}
			if (this.m_calloutStyle != null)
			{
				this.m_calloutStyle.Initialize("CalloutStyle", context);
				context.ExprHostBuilder.ChartSmartLabelCalloutStyle(this.m_calloutStyle);
			}
			if (this.m_showOverlapped != null)
			{
				this.m_showOverlapped.Initialize("ShowOverlapped", context);
				context.ExprHostBuilder.ChartSmartLabelShowOverlapped(this.m_showOverlapped);
			}
			if (this.m_markerOverlapping != null)
			{
				this.m_markerOverlapping.Initialize("MarkerOverlapping", context);
				context.ExprHostBuilder.ChartSmartLabelMarkerOverlapping(this.m_markerOverlapping);
			}
			if (this.m_maxMovingDistance != null)
			{
				this.m_maxMovingDistance.Initialize("MaxMovingDistance", context);
				context.ExprHostBuilder.ChartSmartLabelMaxMovingDistance(this.m_maxMovingDistance);
			}
			if (this.m_minMovingDistance != null)
			{
				this.m_minMovingDistance.Initialize("MinMovingDistance", context);
				context.ExprHostBuilder.ChartSmartLabelMinMovingDistance(this.m_minMovingDistance);
			}
			if (this.m_noMoveDirections != null)
			{
				this.m_noMoveDirections.Initialize(context);
			}
			if (this.m_disabled != null)
			{
				this.m_disabled.Initialize("Disabled", context);
				context.ExprHostBuilder.ChartSmartLabelDisabled(this.m_disabled);
			}
			context.ExprHostBuilder.ChartSmartLabelEnd();
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x000F1AEC File Offset: 0x000EFCEC
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			ChartSmartLabel chartSmartLabel = (ChartSmartLabel)base.MemberwiseClone();
			chartSmartLabel.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)context.CurrentDataRegionClone;
			if (this.m_allowOutSidePlotArea != null)
			{
				chartSmartLabel.m_allowOutSidePlotArea = (ExpressionInfo)this.m_allowOutSidePlotArea.PublishClone(context);
			}
			if (this.m_calloutBackColor != null)
			{
				chartSmartLabel.m_calloutBackColor = (ExpressionInfo)this.m_calloutBackColor.PublishClone(context);
			}
			if (this.m_calloutLineAnchor != null)
			{
				chartSmartLabel.m_calloutLineAnchor = (ExpressionInfo)this.m_calloutLineAnchor.PublishClone(context);
			}
			if (this.m_calloutLineColor != null)
			{
				chartSmartLabel.m_calloutLineColor = (ExpressionInfo)this.m_calloutLineColor.PublishClone(context);
			}
			if (this.m_calloutLineStyle != null)
			{
				chartSmartLabel.m_calloutLineStyle = (ExpressionInfo)this.m_calloutLineStyle.PublishClone(context);
			}
			if (this.m_calloutLineWidth != null)
			{
				chartSmartLabel.m_calloutLineWidth = (ExpressionInfo)this.m_calloutLineWidth.PublishClone(context);
			}
			if (this.m_calloutStyle != null)
			{
				chartSmartLabel.m_calloutStyle = (ExpressionInfo)this.m_calloutStyle.PublishClone(context);
			}
			if (this.m_showOverlapped != null)
			{
				chartSmartLabel.m_showOverlapped = (ExpressionInfo)this.m_showOverlapped.PublishClone(context);
			}
			if (this.m_markerOverlapping != null)
			{
				chartSmartLabel.m_markerOverlapping = (ExpressionInfo)this.m_markerOverlapping.PublishClone(context);
			}
			if (this.m_maxMovingDistance != null)
			{
				chartSmartLabel.m_maxMovingDistance = (ExpressionInfo)this.m_maxMovingDistance.PublishClone(context);
			}
			if (this.m_minMovingDistance != null)
			{
				chartSmartLabel.m_minMovingDistance = (ExpressionInfo)this.m_minMovingDistance.PublishClone(context);
			}
			if (this.m_noMoveDirections != null)
			{
				chartSmartLabel.m_noMoveDirections = (ChartNoMoveDirections)this.m_noMoveDirections.PublishClone(context);
			}
			if (this.m_disabled != null)
			{
				chartSmartLabel.m_disabled = (ExpressionInfo)this.m_disabled.PublishClone(context);
			}
			return chartSmartLabel;
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x000F1CAC File Offset: 0x000EFEAC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartSmartLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.AllowOutSidePlotArea, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.CalloutBackColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.CalloutLineAnchor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.CalloutLineColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.CalloutLineStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.CalloutLineWidth, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.CalloutStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ShowOverlapped, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MarkerOverlapping, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MaxMovingDistance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MinMovingDistance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.NoMoveDirections, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartNoMoveDirections),
				new MemberInfo(MemberName.Chart, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Chart, Token.Reference),
				new MemberInfo(MemberName.ChartSeries, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartSeries, Token.Reference),
				new MemberInfo(MemberName.Disabled, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x000F1E08 File Offset: 0x000F0008
		internal ChartAllowOutsideChartArea EvaluateAllowOutSidePlotArea(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return EnumTranslator.TranslateChartAllowOutsideChartArea(context.ReportRuntime.EvaluateChartSmartLabelAllowOutSidePlotAreaExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000F1E39 File Offset: 0x000F0039
		internal string EvaluateCalloutBackColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartSmartLabelCalloutBackColorExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000F1E5F File Offset: 0x000F005F
		internal ChartCalloutLineAnchor EvaluateCalloutLineAnchor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return EnumTranslator.TranslateChartCalloutLineAnchor(context.ReportRuntime.EvaluateChartSmartLabelCalloutLineAnchorExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x000F1E90 File Offset: 0x000F0090
		internal string EvaluateCalloutLineColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartSmartLabelCalloutLineColorExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000F1EB6 File Offset: 0x000F00B6
		internal ChartCalloutLineStyle EvaluateCalloutLineStyle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return EnumTranslator.TranslateChartCalloutLineStyle(context.ReportRuntime.EvaluateChartSmartLabelCalloutLineStyleExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x000F1EE7 File Offset: 0x000F00E7
		internal string EvaluateCalloutLineWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartSmartLabelCalloutLineWidthExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x000F1F0D File Offset: 0x000F010D
		internal ChartCalloutStyle EvaluateCalloutStyle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return EnumTranslator.TranslateChartCalloutStyle(context.ReportRuntime.EvaluateChartSmartLabelCalloutStyleExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x000F1F3E File Offset: 0x000F013E
		internal bool EvaluateShowOverlapped(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartSmartLabelShowOverlappedExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x000F1F64 File Offset: 0x000F0164
		internal bool EvaluateMarkerOverlapping(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartSmartLabelMarkerOverlappingExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x000F1F8A File Offset: 0x000F018A
		internal string EvaluateMaxMovingDistance(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartSmartLabelMaxMovingDistanceExpression(this, this.m_chart.Name);
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x000F1FB0 File Offset: 0x000F01B0
		internal string EvaluateMinMovingDistance(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartSmartLabelMinMovingDistanceExpression(this, this.m_chart.Name);
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x000F1FD6 File Offset: 0x000F01D6
		internal bool EvaluateDisabled(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartSmartLabelDisabledExpression(this, this.m_chart.Name);
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000F1FFC File Offset: 0x000F01FC
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ChartSmartLabel.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ChartSeries)
				{
					switch (memberName)
					{
					case MemberName.AllowOutSidePlotArea:
						writer.Write(this.m_allowOutSidePlotArea);
						continue;
					case MemberName.CalloutBackColor:
						writer.Write(this.m_calloutBackColor);
						continue;
					case MemberName.CalloutLineAnchor:
						writer.Write(this.m_calloutLineAnchor);
						continue;
					case MemberName.CalloutLineColor:
						writer.Write(this.m_calloutLineColor);
						continue;
					case MemberName.CalloutLineStyle:
						writer.Write(this.m_calloutLineStyle);
						continue;
					case MemberName.CalloutLineWidth:
						writer.Write(this.m_calloutLineWidth);
						continue;
					case MemberName.CalloutStyle:
						writer.Write(this.m_calloutStyle);
						continue;
					case MemberName.HideOverlapped:
						break;
					case MemberName.MarkerOverlapping:
						writer.Write(this.m_markerOverlapping);
						continue;
					case MemberName.MaxMovingDistance:
						writer.Write(this.m_maxMovingDistance);
						continue;
					case MemberName.MinMovingDistance:
						writer.Write(this.m_minMovingDistance);
						continue;
					case MemberName.NoMoveDirections:
						writer.Write(this.m_noMoveDirections);
						continue;
					default:
						if (memberName == MemberName.ChartSeries)
						{
							writer.WriteReference(this.m_chartSeries);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.Chart)
					{
						writer.WriteReference(this.m_chart);
						continue;
					}
					if (memberName == MemberName.ShowOverlapped)
					{
						writer.Write(this.m_showOverlapped);
						continue;
					}
					if (memberName == MemberName.Disabled)
					{
						writer.Write(this.m_disabled);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000F21A8 File Offset: 0x000F03A8
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ChartSmartLabel.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ChartSeries)
				{
					switch (memberName)
					{
					case MemberName.AllowOutSidePlotArea:
						this.m_allowOutSidePlotArea = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.CalloutBackColor:
						this.m_calloutBackColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.CalloutLineAnchor:
						this.m_calloutLineAnchor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.CalloutLineColor:
						this.m_calloutLineColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.CalloutLineStyle:
						this.m_calloutLineStyle = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.CalloutLineWidth:
						this.m_calloutLineWidth = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.CalloutStyle:
						this.m_calloutStyle = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.HideOverlapped:
						break;
					case MemberName.MarkerOverlapping:
						this.m_markerOverlapping = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.MaxMovingDistance:
						this.m_maxMovingDistance = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.MinMovingDistance:
						this.m_minMovingDistance = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.NoMoveDirections:
						this.m_noMoveDirections = (ChartNoMoveDirections)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.ChartSeries)
						{
							this.m_chartSeries = reader.ReadReference<ChartSeries>(this);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.Chart)
					{
						this.m_chart = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.Chart>(this);
						continue;
					}
					if (memberName == MemberName.ShowOverlapped)
					{
						this.m_showOverlapped = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Disabled)
					{
						this.m_disabled = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000F23A0 File Offset: 0x000F05A0
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ChartSmartLabel.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName != MemberName.ChartSeries)
					{
						if (memberName == MemberName.Chart)
						{
							Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
							this.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)referenceableItems[memberReference.RefID];
						}
						else
						{
							Global.Tracer.Assert(false);
						}
					}
					else
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_chartSeries = (ChartSeries)referenceableItems[memberReference.RefID];
					}
				}
			}
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x000F2484 File Offset: 0x000F0684
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartSmartLabel;
		}

		// Token: 0x04001AE1 RID: 6881
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Chart m_chart;

		// Token: 0x04001AE2 RID: 6882
		[Reference]
		private ChartSeries m_chartSeries;

		// Token: 0x04001AE3 RID: 6883
		private ExpressionInfo m_allowOutSidePlotArea;

		// Token: 0x04001AE4 RID: 6884
		private ExpressionInfo m_calloutBackColor;

		// Token: 0x04001AE5 RID: 6885
		private ExpressionInfo m_calloutLineAnchor;

		// Token: 0x04001AE6 RID: 6886
		private ExpressionInfo m_calloutLineColor;

		// Token: 0x04001AE7 RID: 6887
		private ExpressionInfo m_calloutLineStyle;

		// Token: 0x04001AE8 RID: 6888
		private ExpressionInfo m_calloutLineWidth;

		// Token: 0x04001AE9 RID: 6889
		private ExpressionInfo m_calloutStyle;

		// Token: 0x04001AEA RID: 6890
		private ExpressionInfo m_showOverlapped;

		// Token: 0x04001AEB RID: 6891
		private ExpressionInfo m_markerOverlapping;

		// Token: 0x04001AEC RID: 6892
		private ExpressionInfo m_maxMovingDistance;

		// Token: 0x04001AED RID: 6893
		private ExpressionInfo m_minMovingDistance;

		// Token: 0x04001AEE RID: 6894
		private ChartNoMoveDirections m_noMoveDirections;

		// Token: 0x04001AEF RID: 6895
		private ExpressionInfo m_disabled;

		// Token: 0x04001AF0 RID: 6896
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartSmartLabel.GetDeclaration();

		// Token: 0x04001AF1 RID: 6897
		[NonSerialized]
		private ChartSmartLabelExprHost m_exprHost;
	}
}

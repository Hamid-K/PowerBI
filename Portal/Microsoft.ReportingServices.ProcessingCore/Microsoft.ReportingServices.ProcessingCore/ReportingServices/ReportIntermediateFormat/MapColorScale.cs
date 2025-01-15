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
	// Token: 0x02000424 RID: 1060
	[Serializable]
	internal sealed class MapColorScale : MapDockableSubItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002EA0 RID: 11936 RVA: 0x000D3FF4 File Offset: 0x000D21F4
		internal MapColorScale()
		{
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x000D3FFC File Offset: 0x000D21FC
		internal MapColorScale(Map map, int id)
			: base(map, id)
		{
		}

		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x06002EA2 RID: 11938 RVA: 0x000D4006 File Offset: 0x000D2206
		// (set) Token: 0x06002EA3 RID: 11939 RVA: 0x000D400E File Offset: 0x000D220E
		internal MapColorScaleTitle MapColorScaleTitle
		{
			get
			{
				return this.m_mapColorScaleTitle;
			}
			set
			{
				this.m_mapColorScaleTitle = value;
			}
		}

		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x06002EA4 RID: 11940 RVA: 0x000D4017 File Offset: 0x000D2217
		// (set) Token: 0x06002EA5 RID: 11941 RVA: 0x000D401F File Offset: 0x000D221F
		internal ExpressionInfo TickMarkLength
		{
			get
			{
				return this.m_tickMarkLength;
			}
			set
			{
				this.m_tickMarkLength = value;
			}
		}

		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x06002EA6 RID: 11942 RVA: 0x000D4028 File Offset: 0x000D2228
		// (set) Token: 0x06002EA7 RID: 11943 RVA: 0x000D4030 File Offset: 0x000D2230
		internal ExpressionInfo ColorBarBorderColor
		{
			get
			{
				return this.m_colorBarBorderColor;
			}
			set
			{
				this.m_colorBarBorderColor = value;
			}
		}

		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x06002EA8 RID: 11944 RVA: 0x000D4039 File Offset: 0x000D2239
		// (set) Token: 0x06002EA9 RID: 11945 RVA: 0x000D4041 File Offset: 0x000D2241
		internal ExpressionInfo LabelInterval
		{
			get
			{
				return this.m_labelInterval;
			}
			set
			{
				this.m_labelInterval = value;
			}
		}

		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x06002EAA RID: 11946 RVA: 0x000D404A File Offset: 0x000D224A
		// (set) Token: 0x06002EAB RID: 11947 RVA: 0x000D4052 File Offset: 0x000D2252
		internal ExpressionInfo LabelFormat
		{
			get
			{
				return this.m_labelFormat;
			}
			set
			{
				this.m_labelFormat = value;
			}
		}

		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x06002EAC RID: 11948 RVA: 0x000D405B File Offset: 0x000D225B
		// (set) Token: 0x06002EAD RID: 11949 RVA: 0x000D4063 File Offset: 0x000D2263
		internal ExpressionInfo LabelPlacement
		{
			get
			{
				return this.m_labelPlacement;
			}
			set
			{
				this.m_labelPlacement = value;
			}
		}

		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x06002EAE RID: 11950 RVA: 0x000D406C File Offset: 0x000D226C
		// (set) Token: 0x06002EAF RID: 11951 RVA: 0x000D4074 File Offset: 0x000D2274
		internal ExpressionInfo LabelBehavior
		{
			get
			{
				return this.m_labelBehavior;
			}
			set
			{
				this.m_labelBehavior = value;
			}
		}

		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x000D407D File Offset: 0x000D227D
		// (set) Token: 0x06002EB1 RID: 11953 RVA: 0x000D4085 File Offset: 0x000D2285
		internal ExpressionInfo HideEndLabels
		{
			get
			{
				return this.m_hideEndLabels;
			}
			set
			{
				this.m_hideEndLabels = value;
			}
		}

		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x06002EB2 RID: 11954 RVA: 0x000D408E File Offset: 0x000D228E
		// (set) Token: 0x06002EB3 RID: 11955 RVA: 0x000D4096 File Offset: 0x000D2296
		internal ExpressionInfo RangeGapColor
		{
			get
			{
				return this.m_rangeGapColor;
			}
			set
			{
				this.m_rangeGapColor = value;
			}
		}

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x06002EB4 RID: 11956 RVA: 0x000D409F File Offset: 0x000D229F
		// (set) Token: 0x06002EB5 RID: 11957 RVA: 0x000D40A7 File Offset: 0x000D22A7
		internal ExpressionInfo NoDataText
		{
			get
			{
				return this.m_noDataText;
			}
			set
			{
				this.m_noDataText = value;
			}
		}

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x000D40B0 File Offset: 0x000D22B0
		internal new MapColorScaleExprHost ExprHost
		{
			get
			{
				return (MapColorScaleExprHost)this.m_exprHost;
			}
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x000D40C0 File Offset: 0x000D22C0
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapColorScaleStart();
			base.Initialize(context);
			if (this.m_mapColorScaleTitle != null)
			{
				this.m_mapColorScaleTitle.Initialize(context);
			}
			if (this.m_tickMarkLength != null)
			{
				this.m_tickMarkLength.Initialize("TickMarkLength", context);
				context.ExprHostBuilder.MapColorScaleTickMarkLength(this.m_tickMarkLength);
			}
			if (this.m_colorBarBorderColor != null)
			{
				this.m_colorBarBorderColor.Initialize("ColorBarBorderColor", context);
				context.ExprHostBuilder.MapColorScaleColorBarBorderColor(this.m_colorBarBorderColor);
			}
			if (this.m_labelInterval != null)
			{
				this.m_labelInterval.Initialize("LabelInterval", context);
				context.ExprHostBuilder.MapColorScaleLabelInterval(this.m_labelInterval);
			}
			if (this.m_labelFormat != null)
			{
				this.m_labelFormat.Initialize("LabelFormat", context);
				context.ExprHostBuilder.MapColorScaleLabelFormat(this.m_labelFormat);
			}
			if (this.m_labelPlacement != null)
			{
				this.m_labelPlacement.Initialize("LabelPlacement", context);
				context.ExprHostBuilder.MapColorScaleLabelPlacement(this.m_labelPlacement);
			}
			if (this.m_labelBehavior != null)
			{
				this.m_labelBehavior.Initialize("LabelBehavior", context);
				context.ExprHostBuilder.MapColorScaleLabelBehavior(this.m_labelBehavior);
			}
			if (this.m_hideEndLabels != null)
			{
				this.m_hideEndLabels.Initialize("HideEndLabels", context);
				context.ExprHostBuilder.MapColorScaleHideEndLabels(this.m_hideEndLabels);
			}
			if (this.m_rangeGapColor != null)
			{
				this.m_rangeGapColor.Initialize("RangeGapColor", context);
				context.ExprHostBuilder.MapColorScaleRangeGapColor(this.m_rangeGapColor);
			}
			if (this.m_noDataText != null)
			{
				this.m_noDataText.Initialize("NoDataText", context);
				context.ExprHostBuilder.MapColorScaleNoDataText(this.m_noDataText);
			}
			context.ExprHostBuilder.MapColorScaleEnd();
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x000D4284 File Offset: 0x000D2484
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapColorScale mapColorScale = (MapColorScale)base.PublishClone(context);
			if (this.m_mapColorScaleTitle != null)
			{
				mapColorScale.m_mapColorScaleTitle = (MapColorScaleTitle)this.m_mapColorScaleTitle.PublishClone(context);
			}
			if (this.m_tickMarkLength != null)
			{
				mapColorScale.m_tickMarkLength = (ExpressionInfo)this.m_tickMarkLength.PublishClone(context);
			}
			if (this.m_colorBarBorderColor != null)
			{
				mapColorScale.m_colorBarBorderColor = (ExpressionInfo)this.m_colorBarBorderColor.PublishClone(context);
			}
			if (this.m_labelInterval != null)
			{
				mapColorScale.m_labelInterval = (ExpressionInfo)this.m_labelInterval.PublishClone(context);
			}
			if (this.m_labelFormat != null)
			{
				mapColorScale.m_labelFormat = (ExpressionInfo)this.m_labelFormat.PublishClone(context);
			}
			if (this.m_labelPlacement != null)
			{
				mapColorScale.m_labelPlacement = (ExpressionInfo)this.m_labelPlacement.PublishClone(context);
			}
			if (this.m_labelBehavior != null)
			{
				mapColorScale.m_labelBehavior = (ExpressionInfo)this.m_labelBehavior.PublishClone(context);
			}
			if (this.m_hideEndLabels != null)
			{
				mapColorScale.m_hideEndLabels = (ExpressionInfo)this.m_hideEndLabels.PublishClone(context);
			}
			if (this.m_rangeGapColor != null)
			{
				mapColorScale.m_rangeGapColor = (ExpressionInfo)this.m_rangeGapColor.PublishClone(context);
			}
			if (this.m_noDataText != null)
			{
				mapColorScale.m_noDataText = (ExpressionInfo)this.m_noDataText.PublishClone(context);
			}
			return mapColorScale;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x000D43D8 File Offset: 0x000D25D8
		internal void SetExprHost(MapColorScaleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_mapColorScaleTitle != null && this.ExprHost.MapColorScaleTitleHost != null)
			{
				this.m_mapColorScaleTitle.SetExprHost(this.ExprHost.MapColorScaleTitleHost, reportObjectModel);
			}
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x000D4434 File Offset: 0x000D2634
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDockableSubItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapColorScaleTitle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorScaleTitle),
				new MemberInfo(MemberName.TickMarkLength, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ColorBarBorderColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LabelInterval, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LabelFormat, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LabelPlacement, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LabelBehavior, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.HideEndLabels, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.RangeGapColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.NoDataText, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x000D452C File Offset: 0x000D272C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapColorScale.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.HideEndLabels)
				{
					if (memberName != MemberName.LabelInterval)
					{
						switch (memberName)
						{
						case MemberName.MapColorScaleTitle:
							writer.Write(this.m_mapColorScaleTitle);
							break;
						case MemberName.TickMarkLength:
							writer.Write(this.m_tickMarkLength);
							break;
						case MemberName.ColorBarBorderColor:
							writer.Write(this.m_colorBarBorderColor);
							break;
						case MemberName.LabelFormat:
							writer.Write(this.m_labelFormat);
							break;
						case MemberName.LabelPlacement:
							writer.Write(this.m_labelPlacement);
							break;
						case MemberName.LabelBehavior:
							writer.Write(this.m_labelBehavior);
							break;
						case MemberName.RangeGapColor:
							writer.Write(this.m_rangeGapColor);
							break;
						case MemberName.NoDataText:
							writer.Write(this.m_noDataText);
							break;
						default:
							Global.Tracer.Assert(false);
							break;
						}
					}
					else
					{
						writer.Write(this.m_labelInterval);
					}
				}
				else
				{
					writer.Write(this.m_hideEndLabels);
				}
			}
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x000D4658 File Offset: 0x000D2858
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapColorScale.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.HideEndLabels)
				{
					if (memberName != MemberName.LabelInterval)
					{
						switch (memberName)
						{
						case MemberName.MapColorScaleTitle:
							this.m_mapColorScaleTitle = (MapColorScaleTitle)reader.ReadRIFObject();
							break;
						case MemberName.TickMarkLength:
							this.m_tickMarkLength = (ExpressionInfo)reader.ReadRIFObject();
							break;
						case MemberName.ColorBarBorderColor:
							this.m_colorBarBorderColor = (ExpressionInfo)reader.ReadRIFObject();
							break;
						case MemberName.LabelFormat:
							this.m_labelFormat = (ExpressionInfo)reader.ReadRIFObject();
							break;
						case MemberName.LabelPlacement:
							this.m_labelPlacement = (ExpressionInfo)reader.ReadRIFObject();
							break;
						case MemberName.LabelBehavior:
							this.m_labelBehavior = (ExpressionInfo)reader.ReadRIFObject();
							break;
						case MemberName.RangeGapColor:
							this.m_rangeGapColor = (ExpressionInfo)reader.ReadRIFObject();
							break;
						case MemberName.NoDataText:
							this.m_noDataText = (ExpressionInfo)reader.ReadRIFObject();
							break;
						default:
							Global.Tracer.Assert(false);
							break;
						}
					}
					else
					{
						this.m_labelInterval = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_hideEndLabels = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x000D47B9 File Offset: 0x000D29B9
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorScale;
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x000D47C0 File Offset: 0x000D29C0
		internal string EvaluateTickMarkLength(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapColorScaleTickMarkLengthExpression(this, this.m_map.Name);
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x000D47E6 File Offset: 0x000D29E6
		internal string EvaluateColorBarBorderColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapColorScaleColorBarBorderColorExpression(this, this.m_map.Name);
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x000D480C File Offset: 0x000D2A0C
		internal int EvaluateLabelInterval(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapColorScaleLabelIntervalExpression(this, this.m_map.Name);
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x000D4832 File Offset: 0x000D2A32
		internal string EvaluateLabelFormat(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapColorScaleLabelFormatExpression(this, this.m_map.Name);
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x000D4858 File Offset: 0x000D2A58
		internal MapLabelPlacement EvaluateLabelPlacement(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateLabelPlacement(context.ReportRuntime.EvaluateMapColorScaleLabelPlacementExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x000D4889 File Offset: 0x000D2A89
		internal MapLabelBehavior EvaluateLabelBehavior(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateLabelBehavior(context.ReportRuntime.EvaluateMapColorScaleLabelBehaviorExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x000D48BA File Offset: 0x000D2ABA
		internal bool EvaluateHideEndLabels(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapColorScaleHideEndLabelsExpression(this, this.m_map.Name);
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000D48E0 File Offset: 0x000D2AE0
		internal string EvaluateRangeGapColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapColorScaleRangeGapColorExpression(this, this.m_map.Name);
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000D4906 File Offset: 0x000D2B06
		internal string EvaluateNoDataText(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapColorScaleNoDataTextExpression(this, this.m_map.Name);
		}

		// Token: 0x04001884 RID: 6276
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapColorScale.GetDeclaration();

		// Token: 0x04001885 RID: 6277
		private MapColorScaleTitle m_mapColorScaleTitle;

		// Token: 0x04001886 RID: 6278
		private ExpressionInfo m_tickMarkLength;

		// Token: 0x04001887 RID: 6279
		private ExpressionInfo m_colorBarBorderColor;

		// Token: 0x04001888 RID: 6280
		private ExpressionInfo m_labelInterval;

		// Token: 0x04001889 RID: 6281
		private ExpressionInfo m_labelFormat;

		// Token: 0x0400188A RID: 6282
		private ExpressionInfo m_labelPlacement;

		// Token: 0x0400188B RID: 6283
		private ExpressionInfo m_labelBehavior;

		// Token: 0x0400188C RID: 6284
		private ExpressionInfo m_hideEndLabels;

		// Token: 0x0400188D RID: 6285
		private ExpressionInfo m_rangeGapColor;

		// Token: 0x0400188E RID: 6286
		private ExpressionInfo m_noDataText;
	}
}

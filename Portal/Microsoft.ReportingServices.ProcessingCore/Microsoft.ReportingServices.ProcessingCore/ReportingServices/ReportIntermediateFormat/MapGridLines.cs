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
	// Token: 0x02000450 RID: 1104
	[Serializable]
	internal sealed class MapGridLines : MapStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003235 RID: 12853 RVA: 0x000E052A File Offset: 0x000DE72A
		internal MapGridLines()
		{
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x000E0532 File Offset: 0x000DE732
		internal MapGridLines(Map map)
			: base(map)
		{
		}

		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x06003237 RID: 12855 RVA: 0x000E053B File Offset: 0x000DE73B
		// (set) Token: 0x06003238 RID: 12856 RVA: 0x000E0543 File Offset: 0x000DE743
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

		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x06003239 RID: 12857 RVA: 0x000E054C File Offset: 0x000DE74C
		// (set) Token: 0x0600323A RID: 12858 RVA: 0x000E0554 File Offset: 0x000DE754
		internal ExpressionInfo Interval
		{
			get
			{
				return this.m_interval;
			}
			set
			{
				this.m_interval = value;
			}
		}

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x000E055D File Offset: 0x000DE75D
		// (set) Token: 0x0600323C RID: 12860 RVA: 0x000E0565 File Offset: 0x000DE765
		internal ExpressionInfo ShowLabels
		{
			get
			{
				return this.m_showLabels;
			}
			set
			{
				this.m_showLabels = value;
			}
		}

		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x0600323D RID: 12861 RVA: 0x000E056E File Offset: 0x000DE76E
		// (set) Token: 0x0600323E RID: 12862 RVA: 0x000E0576 File Offset: 0x000DE776
		internal ExpressionInfo LabelPosition
		{
			get
			{
				return this.m_labelPosition;
			}
			set
			{
				this.m_labelPosition = value;
			}
		}

		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x000E057F File Offset: 0x000DE77F
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x06003240 RID: 12864 RVA: 0x000E058C File Offset: 0x000DE78C
		internal MapGridLinesExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x000E0594 File Offset: 0x000DE794
		internal void Initialize(InitializationContext context, bool isMeridian)
		{
			context.ExprHostBuilder.MapGridLinesStart(isMeridian);
			base.Initialize(context);
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.MapGridLinesHidden(this.m_hidden);
			}
			if (this.m_interval != null)
			{
				this.m_interval.Initialize("Interval", context);
				context.ExprHostBuilder.MapGridLinesInterval(this.m_interval);
			}
			if (this.m_showLabels != null)
			{
				this.m_showLabels.Initialize("ShowLabels", context);
				context.ExprHostBuilder.MapGridLinesShowLabels(this.m_showLabels);
			}
			if (this.m_labelPosition != null)
			{
				this.m_labelPosition.Initialize("LabelPosition", context);
				context.ExprHostBuilder.MapGridLinesLabelPosition(this.m_labelPosition);
			}
			context.ExprHostBuilder.MapGridLinesEnd(isMeridian);
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x000E0670 File Offset: 0x000DE870
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapGridLines mapGridLines = (MapGridLines)base.PublishClone(context);
			if (this.m_hidden != null)
			{
				mapGridLines.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
			}
			if (this.m_interval != null)
			{
				mapGridLines.m_interval = (ExpressionInfo)this.m_interval.PublishClone(context);
			}
			if (this.m_showLabels != null)
			{
				mapGridLines.m_showLabels = (ExpressionInfo)this.m_showLabels.PublishClone(context);
			}
			if (this.m_labelPosition != null)
			{
				mapGridLines.m_labelPosition = (ExpressionInfo)this.m_labelPosition.PublishClone(context);
			}
			return mapGridLines;
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x000E0707 File Offset: 0x000DE907
		internal void SetExprHost(MapGridLinesExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			base.SetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x000E0734 File Offset: 0x000DE934
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapGridLines, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Interval, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ShowLabels, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LabelPosition, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x000E07AC File Offset: 0x000DE9AC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapGridLines.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Hidden)
				{
					if (memberName == MemberName.Interval)
					{
						writer.Write(this.m_interval);
						continue;
					}
					if (memberName == MemberName.Hidden)
					{
						writer.Write(this.m_hidden);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.ShowLabels)
					{
						writer.Write(this.m_showLabels);
						continue;
					}
					if (memberName == MemberName.LabelPosition)
					{
						writer.Write(this.m_labelPosition);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000E0860 File Offset: 0x000DEA60
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapGridLines.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Hidden)
				{
					if (memberName == MemberName.Interval)
					{
						this.m_interval = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Hidden)
					{
						this.m_hidden = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.ShowLabels)
					{
						this.m_showLabels = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.LabelPosition)
					{
						this.m_labelPosition = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000E0925 File Offset: 0x000DEB25
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapGridLines;
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000E092C File Offset: 0x000DEB2C
		internal bool EvaluateHidden(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapGridLinesHiddenExpression(this, this.m_map.Name);
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000E0952 File Offset: 0x000DEB52
		internal double EvaluateInterval(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapGridLinesIntervalExpression(this, this.m_map.Name);
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000E0978 File Offset: 0x000DEB78
		internal bool EvaluateShowLabels(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapGridLinesShowLabelsExpression(this, this.m_map.Name);
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000E099E File Offset: 0x000DEB9E
		internal MapLabelPosition EvaluateLabelPosition(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateLabelPosition(context.ReportRuntime.EvaluateMapGridLinesLabelPositionExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x0400196B RID: 6507
		[NonSerialized]
		private MapGridLinesExprHost m_exprHost;

		// Token: 0x0400196C RID: 6508
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapGridLines.GetDeclaration();

		// Token: 0x0400196D RID: 6509
		private ExpressionInfo m_hidden;

		// Token: 0x0400196E RID: 6510
		private ExpressionInfo m_interval;

		// Token: 0x0400196F RID: 6511
		private ExpressionInfo m_showLabels;

		// Token: 0x04001970 RID: 6512
		private ExpressionInfo m_labelPosition;
	}
}

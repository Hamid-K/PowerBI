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
	// Token: 0x02000447 RID: 1095
	[Serializable]
	internal sealed class MapPolygonTemplate : MapSpatialElementTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600317D RID: 12669 RVA: 0x000DDFEB File Offset: 0x000DC1EB
		internal MapPolygonTemplate()
		{
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x000DDFF3 File Offset: 0x000DC1F3
		internal MapPolygonTemplate(MapPolygonLayer mapPolygonLayer, Map map, int id)
			: base(mapPolygonLayer, map, id)
		{
		}

		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x000DDFFE File Offset: 0x000DC1FE
		// (set) Token: 0x06003180 RID: 12672 RVA: 0x000DE006 File Offset: 0x000DC206
		internal ExpressionInfo ScaleFactor
		{
			get
			{
				return this.m_scaleFactor;
			}
			set
			{
				this.m_scaleFactor = value;
			}
		}

		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x06003181 RID: 12673 RVA: 0x000DE00F File Offset: 0x000DC20F
		// (set) Token: 0x06003182 RID: 12674 RVA: 0x000DE017 File Offset: 0x000DC217
		internal ExpressionInfo CenterPointOffsetX
		{
			get
			{
				return this.m_centerPointOffsetX;
			}
			set
			{
				this.m_centerPointOffsetX = value;
			}
		}

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x06003183 RID: 12675 RVA: 0x000DE020 File Offset: 0x000DC220
		// (set) Token: 0x06003184 RID: 12676 RVA: 0x000DE028 File Offset: 0x000DC228
		internal ExpressionInfo CenterPointOffsetY
		{
			get
			{
				return this.m_centerPointOffsetY;
			}
			set
			{
				this.m_centerPointOffsetY = value;
			}
		}

		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x06003185 RID: 12677 RVA: 0x000DE031 File Offset: 0x000DC231
		// (set) Token: 0x06003186 RID: 12678 RVA: 0x000DE039 File Offset: 0x000DC239
		internal ExpressionInfo ShowLabel
		{
			get
			{
				return this.m_showLabel;
			}
			set
			{
				this.m_showLabel = value;
			}
		}

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x06003187 RID: 12679 RVA: 0x000DE042 File Offset: 0x000DC242
		// (set) Token: 0x06003188 RID: 12680 RVA: 0x000DE04A File Offset: 0x000DC24A
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

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x06003189 RID: 12681 RVA: 0x000DE053 File Offset: 0x000DC253
		internal new MapPolygonTemplateExprHost ExprHost
		{
			get
			{
				return (MapPolygonTemplateExprHost)this.m_exprHost;
			}
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000DE060 File Offset: 0x000DC260
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapPolygonTemplateStart();
			base.Initialize(context);
			if (this.m_scaleFactor != null)
			{
				this.m_scaleFactor.Initialize("ScaleFactor", context);
				context.ExprHostBuilder.MapPolygonTemplateScaleFactor(this.m_scaleFactor);
			}
			if (this.m_centerPointOffsetX != null)
			{
				this.m_centerPointOffsetX.Initialize("CenterPointOffsetX", context);
				context.ExprHostBuilder.MapPolygonTemplateCenterPointOffsetX(this.m_centerPointOffsetX);
			}
			if (this.m_centerPointOffsetY != null)
			{
				this.m_centerPointOffsetY.Initialize("CenterPointOffsetY", context);
				context.ExprHostBuilder.MapPolygonTemplateCenterPointOffsetY(this.m_centerPointOffsetY);
			}
			if (this.m_showLabel != null)
			{
				this.m_showLabel.Initialize("ShowLabel", context);
				context.ExprHostBuilder.MapPolygonTemplateShowLabel(this.m_showLabel);
			}
			if (this.m_labelPlacement != null)
			{
				this.m_labelPlacement.Initialize("LabelPlacement", context);
				context.ExprHostBuilder.MapPolygonTemplateLabelPlacement(this.m_labelPlacement);
			}
			context.ExprHostBuilder.MapPolygonTemplateEnd();
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000DE164 File Offset: 0x000DC364
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapPolygonTemplate mapPolygonTemplate = (MapPolygonTemplate)base.PublishClone(context);
			if (this.m_scaleFactor != null)
			{
				mapPolygonTemplate.m_scaleFactor = (ExpressionInfo)this.m_scaleFactor.PublishClone(context);
			}
			if (this.m_centerPointOffsetX != null)
			{
				mapPolygonTemplate.m_centerPointOffsetX = (ExpressionInfo)this.m_centerPointOffsetX.PublishClone(context);
			}
			if (this.m_centerPointOffsetY != null)
			{
				mapPolygonTemplate.m_centerPointOffsetY = (ExpressionInfo)this.m_centerPointOffsetY.PublishClone(context);
			}
			if (this.m_showLabel != null)
			{
				mapPolygonTemplate.m_showLabel = (ExpressionInfo)this.m_showLabel.PublishClone(context);
			}
			if (this.m_labelPlacement != null)
			{
				mapPolygonTemplate.m_labelPlacement = (ExpressionInfo)this.m_labelPlacement.PublishClone(context);
			}
			return mapPolygonTemplate;
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000DE21A File Offset: 0x000DC41A
		internal void SetExprHost(MapPolygonTemplateExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000DE240 File Offset: 0x000DC440
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPolygonTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialElementTemplate, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ScaleFactor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.CenterPointOffsetX, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.CenterPointOffsetY, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ShowLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LabelPlacement, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x000DE2CC File Offset: 0x000DC4CC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapPolygonTemplate.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.LabelPlacement)
				{
					switch (memberName)
					{
					case MemberName.ScaleFactor:
						writer.Write(this.m_scaleFactor);
						break;
					case MemberName.CenterPointOffsetX:
						writer.Write(this.m_centerPointOffsetX);
						break;
					case MemberName.CenterPointOffsetY:
						writer.Write(this.m_centerPointOffsetY);
						break;
					case MemberName.ShowLabel:
						writer.Write(this.m_showLabel);
						break;
					default:
						Global.Tracer.Assert(false);
						break;
					}
				}
				else
				{
					writer.Write(this.m_labelPlacement);
				}
			}
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x000DE388 File Offset: 0x000DC588
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapPolygonTemplate.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.LabelPlacement)
				{
					switch (memberName)
					{
					case MemberName.ScaleFactor:
						this.m_scaleFactor = (ExpressionInfo)reader.ReadRIFObject();
						break;
					case MemberName.CenterPointOffsetX:
						this.m_centerPointOffsetX = (ExpressionInfo)reader.ReadRIFObject();
						break;
					case MemberName.CenterPointOffsetY:
						this.m_centerPointOffsetY = (ExpressionInfo)reader.ReadRIFObject();
						break;
					case MemberName.ShowLabel:
						this.m_showLabel = (ExpressionInfo)reader.ReadRIFObject();
						break;
					default:
						Global.Tracer.Assert(false);
						break;
					}
				}
				else
				{
					this.m_labelPlacement = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x000DE45B File Offset: 0x000DC65B
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPolygonTemplate;
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000DE462 File Offset: 0x000DC662
		internal double EvaluateScaleFactor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapPolygonTemplateScaleFactorExpression(this, this.m_map.Name);
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x000DE488 File Offset: 0x000DC688
		internal double EvaluateCenterPointOffsetX(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapPolygonTemplateCenterPointOffsetXExpression(this, this.m_map.Name);
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000DE4AE File Offset: 0x000DC6AE
		internal double EvaluateCenterPointOffsetY(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapPolygonTemplateCenterPointOffsetYExpression(this, this.m_map.Name);
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x000DE4D4 File Offset: 0x000DC6D4
		internal MapAutoBool EvaluateShowLabel(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return EnumTranslator.TranslateMapAutoBool(context.ReportRuntime.EvaluateMapPolygonTemplateShowLabelExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x000DE505 File Offset: 0x000DC705
		internal MapPolygonLabelPlacement EvaluateLabelPlacement(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return EnumTranslator.TranslateMapPolygonLabelPlacement(context.ReportRuntime.EvaluateMapPolygonTemplateLabelPlacementExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x0400193C RID: 6460
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapPolygonTemplate.GetDeclaration();

		// Token: 0x0400193D RID: 6461
		private ExpressionInfo m_scaleFactor;

		// Token: 0x0400193E RID: 6462
		private ExpressionInfo m_centerPointOffsetX;

		// Token: 0x0400193F RID: 6463
		private ExpressionInfo m_centerPointOffsetY;

		// Token: 0x04001940 RID: 6464
		private ExpressionInfo m_showLabel;

		// Token: 0x04001941 RID: 6465
		private ExpressionInfo m_labelPlacement;
	}
}

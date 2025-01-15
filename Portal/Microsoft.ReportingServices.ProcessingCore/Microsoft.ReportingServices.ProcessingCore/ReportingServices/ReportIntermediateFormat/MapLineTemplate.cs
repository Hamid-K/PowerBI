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
	// Token: 0x02000446 RID: 1094
	[Serializable]
	internal sealed class MapLineTemplate : MapSpatialElementTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600316C RID: 12652 RVA: 0x000DDCFB File Offset: 0x000DBEFB
		internal MapLineTemplate()
		{
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x000DDD03 File Offset: 0x000DBF03
		internal MapLineTemplate(MapLineLayer mapLineLayer, Map map, int id)
			: base(mapLineLayer, map, id)
		{
		}

		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x0600316E RID: 12654 RVA: 0x000DDD0E File Offset: 0x000DBF0E
		// (set) Token: 0x0600316F RID: 12655 RVA: 0x000DDD16 File Offset: 0x000DBF16
		internal ExpressionInfo Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x000DDD1F File Offset: 0x000DBF1F
		// (set) Token: 0x06003171 RID: 12657 RVA: 0x000DDD27 File Offset: 0x000DBF27
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

		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x06003172 RID: 12658 RVA: 0x000DDD30 File Offset: 0x000DBF30
		internal new MapLineTemplateExprHost ExprHost
		{
			get
			{
				return (MapLineTemplateExprHost)this.m_exprHost;
			}
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x000DDD40 File Offset: 0x000DBF40
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapLineTemplateStart();
			base.Initialize(context);
			if (this.m_width != null)
			{
				this.m_width.Initialize("Width", context);
				context.ExprHostBuilder.MapLineTemplateWidth(this.m_width);
			}
			if (this.m_labelPlacement != null)
			{
				this.m_labelPlacement.Initialize("LabelPlacement", context);
				context.ExprHostBuilder.MapLineTemplateLabelPlacement(this.m_labelPlacement);
			}
			context.ExprHostBuilder.MapLineTemplateEnd();
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x000DDDC4 File Offset: 0x000DBFC4
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapLineTemplate mapLineTemplate = (MapLineTemplate)base.PublishClone(context);
			if (this.m_width != null)
			{
				mapLineTemplate.m_width = (ExpressionInfo)this.m_width.PublishClone(context);
			}
			if (this.m_labelPlacement != null)
			{
				mapLineTemplate.m_labelPlacement = (ExpressionInfo)this.m_labelPlacement.PublishClone(context);
			}
			return mapLineTemplate;
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x000DDE1D File Offset: 0x000DC01D
		internal void SetExprHost(MapLineTemplateExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x000DDE40 File Offset: 0x000DC040
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLineTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialElementTemplate, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Width, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LabelPlacement, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x000DDE90 File Offset: 0x000DC090
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapLineTemplate.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Width)
				{
					if (memberName != MemberName.LabelPlacement)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_labelPlacement);
					}
				}
				else
				{
					writer.Write(this.m_width);
				}
			}
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x000DDF04 File Offset: 0x000DC104
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapLineTemplate.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Width)
				{
					if (memberName != MemberName.LabelPlacement)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_labelPlacement = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_width = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000DDF81 File Offset: 0x000DC181
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLineTemplate;
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x000DDF88 File Offset: 0x000DC188
		internal string EvaluateWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLineTemplateWidthExpression(this, this.m_map.Name);
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x000DDFAE File Offset: 0x000DC1AE
		internal MapLineLabelPlacement EvaluateLabelPlacement(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return EnumTranslator.TranslateMapLineLabelPlacement(context.ReportRuntime.EvaluateMapLineTemplateLabelPlacementExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x04001939 RID: 6457
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapLineTemplate.GetDeclaration();

		// Token: 0x0400193A RID: 6458
		private ExpressionInfo m_width;

		// Token: 0x0400193B RID: 6459
		private ExpressionInfo m_labelPlacement;
	}
}

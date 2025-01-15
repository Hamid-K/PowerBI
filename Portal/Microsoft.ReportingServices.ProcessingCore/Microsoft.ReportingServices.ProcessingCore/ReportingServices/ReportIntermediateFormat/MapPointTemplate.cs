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
	// Token: 0x0200044A RID: 1098
	[Serializable]
	internal class MapPointTemplate : MapSpatialElementTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060031CE RID: 12750 RVA: 0x000DF110 File Offset: 0x000DD310
		internal MapPointTemplate()
		{
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000DF118 File Offset: 0x000DD318
		internal MapPointTemplate(MapVectorLayer mapVectorLayer, Map map, int id)
			: base(mapVectorLayer, map, id)
		{
		}

		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x000DF123 File Offset: 0x000DD323
		// (set) Token: 0x060031D1 RID: 12753 RVA: 0x000DF12B File Offset: 0x000DD32B
		internal ExpressionInfo Size
		{
			get
			{
				return this.m_size;
			}
			set
			{
				this.m_size = value;
			}
		}

		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x060031D2 RID: 12754 RVA: 0x000DF134 File Offset: 0x000DD334
		// (set) Token: 0x060031D3 RID: 12755 RVA: 0x000DF13C File Offset: 0x000DD33C
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

		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x060031D4 RID: 12756 RVA: 0x000DF145 File Offset: 0x000DD345
		internal new MapPointTemplateExprHost ExprHost
		{
			get
			{
				return (MapPointTemplateExprHost)this.m_exprHost;
			}
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000DF154 File Offset: 0x000DD354
		internal override void Initialize(InitializationContext context)
		{
			base.Initialize(context);
			if (this.m_size != null)
			{
				this.m_size.Initialize("Size", context);
				context.ExprHostBuilder.MapPointTemplateSize(this.m_size);
			}
			if (this.m_labelPlacement != null)
			{
				this.m_labelPlacement.Initialize("LabelPlacement", context);
				context.ExprHostBuilder.MapPointTemplateLabelPlacement(this.m_labelPlacement);
			}
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000DF1C0 File Offset: 0x000DD3C0
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapPointTemplate mapPointTemplate = (MapPointTemplate)base.PublishClone(context);
			if (this.m_size != null)
			{
				mapPointTemplate.m_size = (ExpressionInfo)this.m_size.PublishClone(context);
			}
			if (this.m_labelPlacement != null)
			{
				mapPointTemplate.m_labelPlacement = (ExpressionInfo)this.m_labelPlacement.PublishClone(context);
			}
			return mapPointTemplate;
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000DF219 File Offset: 0x000DD419
		internal virtual void SetExprHost(MapPointTemplateExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000DF23C File Offset: 0x000DD43C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPointTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialElementTemplate, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Size, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LabelPlacement, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000DF28C File Offset: 0x000DD48C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapPointTemplate.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Size)
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
					writer.Write(this.m_size);
				}
			}
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000DF300 File Offset: 0x000DD500
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapPointTemplate.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Size)
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
					this.m_size = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000DF37D File Offset: 0x000DD57D
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPointTemplate;
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000DF384 File Offset: 0x000DD584
		internal string EvaluateSize(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapPointTemplateSizeExpression(this, this.m_map.Name);
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000DF3AA File Offset: 0x000DD5AA
		internal MapPointLabelPlacement EvaluateLabelPlacement(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return EnumTranslator.TranslateMapPointLabelPlacement(context.ReportRuntime.EvaluateMapPointTemplateLabelPlacementExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x04001952 RID: 6482
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapPointTemplate.GetDeclaration();

		// Token: 0x04001953 RID: 6483
		private ExpressionInfo m_size;

		// Token: 0x04001954 RID: 6484
		private ExpressionInfo m_labelPlacement;
	}
}

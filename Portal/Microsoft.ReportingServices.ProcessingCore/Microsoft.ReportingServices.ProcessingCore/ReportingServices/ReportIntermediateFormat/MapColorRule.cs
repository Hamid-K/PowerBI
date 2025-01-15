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
	// Token: 0x0200043C RID: 1084
	[Serializable]
	internal class MapColorRule : MapAppearanceRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060030B4 RID: 12468 RVA: 0x000DB5EA File Offset: 0x000D97EA
		internal MapColorRule()
		{
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000DB5F2 File Offset: 0x000D97F2
		internal MapColorRule(MapVectorLayer mapVectorLayer, Map map)
			: base(mapVectorLayer, map)
		{
		}

		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x060030B6 RID: 12470 RVA: 0x000DB5FC File Offset: 0x000D97FC
		// (set) Token: 0x060030B7 RID: 12471 RVA: 0x000DB604 File Offset: 0x000D9804
		internal ExpressionInfo ShowInColorScale
		{
			get
			{
				return this.m_showInColorScale;
			}
			set
			{
				this.m_showInColorScale = value;
			}
		}

		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x000DB60D File Offset: 0x000D980D
		internal new MapColorRuleExprHost ExprHost
		{
			get
			{
				return (MapColorRuleExprHost)this.m_exprHost;
			}
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x000DB61A File Offset: 0x000D981A
		internal override void Initialize(InitializationContext context)
		{
			base.Initialize(context);
			if (this.m_showInColorScale != null)
			{
				this.m_showInColorScale.Initialize("ShowInColorScale", context);
				context.ExprHostBuilder.MapColorRuleShowInColorScale(this.m_showInColorScale);
			}
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000DB650 File Offset: 0x000D9850
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapColorRule mapColorRule = (MapColorRule)base.PublishClone(context);
			if (this.m_showInColorScale != null)
			{
				mapColorRule.m_showInColorScale = (ExpressionInfo)this.m_showInColorScale.PublishClone(context);
			}
			return mapColorRule;
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000DB68A File Offset: 0x000D988A
		internal override void SetExprHost(MapAppearanceRuleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000DB6B0 File Offset: 0x000D98B0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapAppearanceRule, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ShowInColorScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000DB6E8 File Offset: 0x000D98E8
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapColorRule.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.ShowInColorScale)
				{
					writer.Write(this.m_showInColorScale);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000DB740 File Offset: 0x000D9940
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapColorRule.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.ShowInColorScale)
				{
					this.m_showInColorScale = (ExpressionInfo)reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000DB79D File Offset: 0x000D999D
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorRule;
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000DB7A4 File Offset: 0x000D99A4
		internal bool EvaluateShowInColorScale(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapColorRuleShowInColorScaleExpression(this, this.m_map.Name);
		}

		// Token: 0x0400190B RID: 6411
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapColorRule.GetDeclaration();

		// Token: 0x0400190C RID: 6412
		private ExpressionInfo m_showInColorScale;
	}
}

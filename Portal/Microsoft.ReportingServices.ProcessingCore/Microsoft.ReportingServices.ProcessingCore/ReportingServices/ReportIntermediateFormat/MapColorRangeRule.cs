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
	// Token: 0x0200043B RID: 1083
	[Serializable]
	internal sealed class MapColorRangeRule : MapColorRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600309F RID: 12447 RVA: 0x000DB21D File Offset: 0x000D941D
		internal MapColorRangeRule()
		{
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000DB225 File Offset: 0x000D9425
		internal MapColorRangeRule(MapVectorLayer mapVectorLayer, Map map)
			: base(mapVectorLayer, map)
		{
		}

		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x000DB22F File Offset: 0x000D942F
		// (set) Token: 0x060030A2 RID: 12450 RVA: 0x000DB237 File Offset: 0x000D9437
		internal ExpressionInfo StartColor
		{
			get
			{
				return this.m_startColor;
			}
			set
			{
				this.m_startColor = value;
			}
		}

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x060030A3 RID: 12451 RVA: 0x000DB240 File Offset: 0x000D9440
		// (set) Token: 0x060030A4 RID: 12452 RVA: 0x000DB248 File Offset: 0x000D9448
		internal ExpressionInfo MiddleColor
		{
			get
			{
				return this.m_middleColor;
			}
			set
			{
				this.m_middleColor = value;
			}
		}

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x060030A5 RID: 12453 RVA: 0x000DB251 File Offset: 0x000D9451
		// (set) Token: 0x060030A6 RID: 12454 RVA: 0x000DB259 File Offset: 0x000D9459
		internal ExpressionInfo EndColor
		{
			get
			{
				return this.m_endColor;
			}
			set
			{
				this.m_endColor = value;
			}
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x000DB262 File Offset: 0x000D9462
		internal new MapColorRangeRuleExprHost ExprHost
		{
			get
			{
				return (MapColorRangeRuleExprHost)this.m_exprHost;
			}
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000DB270 File Offset: 0x000D9470
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapColorRangeRuleStart();
			base.Initialize(context);
			if (this.m_startColor != null)
			{
				this.m_startColor.Initialize("StartColor", context);
				context.ExprHostBuilder.MapColorRangeRuleStartColor(this.m_startColor);
			}
			if (this.m_middleColor != null)
			{
				this.m_middleColor.Initialize("MiddleColor", context);
				context.ExprHostBuilder.MapColorRangeRuleMiddleColor(this.m_middleColor);
			}
			if (this.m_endColor != null)
			{
				this.m_endColor.Initialize("EndColor", context);
				context.ExprHostBuilder.MapColorRangeRuleEndColor(this.m_endColor);
			}
			context.ExprHostBuilder.MapColorRangeRuleEnd();
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000DB31D File Offset: 0x000D951D
		internal override void InitializeMapMember(InitializationContext context)
		{
			context.ExprHostBuilder.MapColorRangeRuleStart();
			base.InitializeMapMember(context);
			context.ExprHostBuilder.MapColorRangeRuleEnd();
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000DB340 File Offset: 0x000D9540
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapColorRangeRule mapColorRangeRule = (MapColorRangeRule)base.PublishClone(context);
			if (this.m_startColor != null)
			{
				mapColorRangeRule.m_startColor = (ExpressionInfo)this.m_startColor.PublishClone(context);
			}
			if (this.m_middleColor != null)
			{
				mapColorRangeRule.m_middleColor = (ExpressionInfo)this.m_middleColor.PublishClone(context);
			}
			if (this.m_endColor != null)
			{
				mapColorRangeRule.m_endColor = (ExpressionInfo)this.m_endColor.PublishClone(context);
			}
			return mapColorRangeRule;
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000DB3B8 File Offset: 0x000D95B8
		internal override void SetExprHost(MapAppearanceRuleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000DB3DC File Offset: 0x000D95DC
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorRangeRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorRule, new List<MemberInfo>
			{
				new MemberInfo(MemberName.StartColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MiddleColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.EndColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000DB440 File Offset: 0x000D9640
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapColorRangeRule.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.StartColor:
					writer.Write(this.m_startColor);
					break;
				case MemberName.MiddleColor:
					writer.Write(this.m_middleColor);
					break;
				case MemberName.EndColor:
					writer.Write(this.m_endColor);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000DB4CC File Offset: 0x000D96CC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapColorRangeRule.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.StartColor:
					this.m_startColor = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.MiddleColor:
					this.m_middleColor = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.EndColor:
					this.m_endColor = (ExpressionInfo)reader.ReadRIFObject();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000DB565 File Offset: 0x000D9765
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorRangeRule;
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000DB56C File Offset: 0x000D976C
		internal string EvaluateStartColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapColorRangeRuleStartColorExpression(this, this.m_map.Name);
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000DB592 File Offset: 0x000D9792
		internal string EvaluateMiddleColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapColorRangeRuleMiddleColorExpression(this, this.m_map.Name);
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000DB5B8 File Offset: 0x000D97B8
		internal string EvaluateEndColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapColorRangeRuleEndColorExpression(this, this.m_map.Name);
		}

		// Token: 0x04001907 RID: 6407
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapColorRangeRule.GetDeclaration();

		// Token: 0x04001908 RID: 6408
		private ExpressionInfo m_startColor;

		// Token: 0x04001909 RID: 6409
		private ExpressionInfo m_middleColor;

		// Token: 0x0400190A RID: 6410
		private ExpressionInfo m_endColor;
	}
}

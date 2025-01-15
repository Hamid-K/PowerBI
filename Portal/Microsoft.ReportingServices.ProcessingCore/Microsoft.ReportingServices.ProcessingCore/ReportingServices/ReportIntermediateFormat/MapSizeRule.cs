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
	// Token: 0x0200043F RID: 1087
	[Serializable]
	internal sealed class MapSizeRule : MapAppearanceRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060030E6 RID: 12518 RVA: 0x000DC013 File Offset: 0x000DA213
		internal MapSizeRule()
		{
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x000DC01B File Offset: 0x000DA21B
		internal MapSizeRule(MapVectorLayer mapVectorLayer, Map map)
			: base(mapVectorLayer, map)
		{
		}

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x060030E8 RID: 12520 RVA: 0x000DC025 File Offset: 0x000DA225
		// (set) Token: 0x060030E9 RID: 12521 RVA: 0x000DC02D File Offset: 0x000DA22D
		internal ExpressionInfo StartSize
		{
			get
			{
				return this.m_startSize;
			}
			set
			{
				this.m_startSize = value;
			}
		}

		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x060030EA RID: 12522 RVA: 0x000DC036 File Offset: 0x000DA236
		// (set) Token: 0x060030EB RID: 12523 RVA: 0x000DC03E File Offset: 0x000DA23E
		internal ExpressionInfo EndSize
		{
			get
			{
				return this.m_endSize;
			}
			set
			{
				this.m_endSize = value;
			}
		}

		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x060030EC RID: 12524 RVA: 0x000DC047 File Offset: 0x000DA247
		internal new MapSizeRuleExprHost ExprHost
		{
			get
			{
				return (MapSizeRuleExprHost)this.m_exprHost;
			}
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000DC054 File Offset: 0x000DA254
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapSizeRuleStart();
			base.Initialize(context);
			if (this.m_startSize != null)
			{
				this.m_startSize.Initialize("StartSize", context);
				context.ExprHostBuilder.MapSizeRuleStartSize(this.m_startSize);
			}
			if (this.m_endSize != null)
			{
				this.m_endSize.Initialize("EndSize", context);
				context.ExprHostBuilder.MapSizeRuleEndSize(this.m_endSize);
			}
			context.ExprHostBuilder.MapSizeRuleEnd();
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x000DC0D6 File Offset: 0x000DA2D6
		internal override void InitializeMapMember(InitializationContext context)
		{
			context.ExprHostBuilder.MapSizeRuleStart();
			base.InitializeMapMember(context);
			context.ExprHostBuilder.MapSizeRuleEnd();
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000DC0F8 File Offset: 0x000DA2F8
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapSizeRule mapSizeRule = (MapSizeRule)base.PublishClone(context);
			if (this.m_startSize != null)
			{
				mapSizeRule.m_startSize = (ExpressionInfo)this.m_startSize.PublishClone(context);
			}
			if (this.m_endSize != null)
			{
				mapSizeRule.m_endSize = (ExpressionInfo)this.m_endSize.PublishClone(context);
			}
			return mapSizeRule;
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x000DC151 File Offset: 0x000DA351
		internal override void SetExprHost(MapAppearanceRuleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x000DC174 File Offset: 0x000DA374
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSizeRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapAppearanceRule, new List<MemberInfo>
			{
				new MemberInfo(MemberName.StartSize, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.EndSize, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000DC1C4 File Offset: 0x000DA3C4
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapSizeRule.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.StartSize)
				{
					if (memberName != MemberName.EndSize)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_endSize);
					}
				}
				else
				{
					writer.Write(this.m_startSize);
				}
			}
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x000DC238 File Offset: 0x000DA438
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapSizeRule.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.StartSize)
				{
					if (memberName != MemberName.EndSize)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_endSize = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_startSize = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x000DC2B5 File Offset: 0x000DA4B5
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSizeRule;
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x000DC2BC File Offset: 0x000DA4BC
		internal string EvaluateStartSize(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapSizeRuleStartSizeExpression(this, this.m_map.Name);
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x000DC2E2 File Offset: 0x000DA4E2
		internal string EvaluateEndSize(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapSizeRuleEndSizeExpression(this, this.m_map.Name);
		}

		// Token: 0x04001918 RID: 6424
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapSizeRule.GetDeclaration();

		// Token: 0x04001919 RID: 6425
		private ExpressionInfo m_startSize;

		// Token: 0x0400191A RID: 6426
		private ExpressionInfo m_endSize;
	}
}

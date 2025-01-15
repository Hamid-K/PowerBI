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
	// Token: 0x0200043A RID: 1082
	[Serializable]
	internal sealed class MapColorPaletteRule : MapColorRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003090 RID: 12432 RVA: 0x000DAFE3 File Offset: 0x000D91E3
		internal MapColorPaletteRule()
		{
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x000DAFEB File Offset: 0x000D91EB
		internal MapColorPaletteRule(MapVectorLayer mapVectorLayer, Map map)
			: base(mapVectorLayer, map)
		{
		}

		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x000DAFF5 File Offset: 0x000D91F5
		// (set) Token: 0x06003093 RID: 12435 RVA: 0x000DAFFD File Offset: 0x000D91FD
		internal ExpressionInfo Palette
		{
			get
			{
				return this.m_palette;
			}
			set
			{
				this.m_palette = value;
			}
		}

		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x06003094 RID: 12436 RVA: 0x000DB006 File Offset: 0x000D9206
		internal new MapColorPaletteRuleExprHost ExprHost
		{
			get
			{
				return (MapColorPaletteRuleExprHost)this.m_exprHost;
			}
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000DB014 File Offset: 0x000D9214
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapColorPaletteRuleStart();
			base.Initialize(context);
			if (this.m_palette != null)
			{
				this.m_palette.Initialize("Palette", context);
				context.ExprHostBuilder.MapColorPaletteRulePalette(this.m_palette);
			}
			context.ExprHostBuilder.MapColorPaletteRuleEnd();
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x000DB06B File Offset: 0x000D926B
		internal override void InitializeMapMember(InitializationContext context)
		{
			context.ExprHostBuilder.MapColorPaletteRuleStart();
			base.InitializeMapMember(context);
			context.ExprHostBuilder.MapColorPaletteRuleEnd();
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000DB08C File Offset: 0x000D928C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapColorPaletteRule mapColorPaletteRule = (MapColorPaletteRule)base.PublishClone(context);
			if (this.m_palette != null)
			{
				mapColorPaletteRule.m_palette = (ExpressionInfo)this.m_palette.PublishClone(context);
			}
			return mapColorPaletteRule;
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000DB0C6 File Offset: 0x000D92C6
		internal override void SetExprHost(MapAppearanceRuleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000DB0EC File Offset: 0x000D92EC
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorPaletteRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorRule, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Palette, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000DB124 File Offset: 0x000D9324
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapColorPaletteRule.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.Palette)
				{
					writer.Write(this.m_palette);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000DB17C File Offset: 0x000D937C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapColorPaletteRule.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.Palette)
				{
					this.m_palette = (ExpressionInfo)reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x000DB1D9 File Offset: 0x000D93D9
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorPaletteRule;
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x000DB1E0 File Offset: 0x000D93E0
		internal MapPalette EvaluatePalette(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateMapPalette(context.ReportRuntime.EvaluateMapColorPaletteRulePaletteExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x04001905 RID: 6405
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapColorPaletteRule.GetDeclaration();

		// Token: 0x04001906 RID: 6406
		private ExpressionInfo m_palette;
	}
}

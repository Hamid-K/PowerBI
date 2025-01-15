using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001C1 RID: 449
	public sealed class MapColorPaletteRule : MapColorRule
	{
		// Token: 0x0600118E RID: 4494 RVA: 0x00048FD8 File Offset: 0x000471D8
		internal MapColorPaletteRule(MapColorPaletteRule defObject, MapVectorLayer mapVectorLayer, Map map)
			: base(defObject, mapVectorLayer, map)
		{
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x00048FE4 File Offset: 0x000471E4
		public ReportEnumProperty<MapPalette> Palette
		{
			get
			{
				if (this.m_palette == null && this.MapColorPaletteRuleDef.Palette != null)
				{
					this.m_palette = new ReportEnumProperty<MapPalette>(this.MapColorPaletteRuleDef.Palette.IsExpression, this.MapColorPaletteRuleDef.Palette.OriginalText, EnumTranslator.TranslateMapPalette(this.MapColorPaletteRuleDef.Palette.StringValue, null));
				}
				return this.m_palette;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x0004904D File Offset: 0x0004724D
		internal MapColorPaletteRule MapColorPaletteRuleDef
		{
			get
			{
				return (MapColorPaletteRule)base.MapAppearanceRuleDef;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x0004905A File Offset: 0x0004725A
		public new MapColorPaletteRuleInstance Instance
		{
			get
			{
				return (MapColorPaletteRuleInstance)this.GetInstance();
			}
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00049067 File Offset: 0x00047267
		internal override MapAppearanceRuleInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapColorPaletteRuleInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00049097 File Offset: 0x00047297
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000853 RID: 2131
		private ReportEnumProperty<MapPalette> m_palette;
	}
}

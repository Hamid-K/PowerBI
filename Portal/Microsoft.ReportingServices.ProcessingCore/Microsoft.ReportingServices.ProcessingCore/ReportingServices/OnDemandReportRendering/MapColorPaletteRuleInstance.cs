using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001CF RID: 463
	public sealed class MapColorPaletteRuleInstance : MapColorRuleInstance
	{
		// Token: 0x060011F9 RID: 4601 RVA: 0x0004A34D File Offset: 0x0004854D
		internal MapColorPaletteRuleInstance(MapColorPaletteRule defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0004A360 File Offset: 0x00048560
		public MapPalette Palette
		{
			get
			{
				if (this.m_palette == null)
				{
					this.m_palette = new MapPalette?(((MapColorPaletteRule)this.m_defObject.MapColorRuleDef).EvaluatePalette(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_palette.Value;
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0004A3C0 File Offset: 0x000485C0
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_palette = null;
		}

		// Token: 0x04000888 RID: 2184
		private MapColorPaletteRule m_defObject;

		// Token: 0x04000889 RID: 2185
		private MapPalette? m_palette;
	}
}

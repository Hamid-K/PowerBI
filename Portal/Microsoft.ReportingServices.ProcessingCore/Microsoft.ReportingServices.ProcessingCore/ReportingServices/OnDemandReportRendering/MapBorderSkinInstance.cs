using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001A4 RID: 420
	public sealed class MapBorderSkinInstance : BaseInstance
	{
		// Token: 0x060010D9 RID: 4313 RVA: 0x0004752F File Offset: 0x0004572F
		internal MapBorderSkinInstance(MapBorderSkin defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x0004754C File Offset: 0x0004574C
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_defObject, this.m_defObject.MapDef.ReportScope, this.m_defObject.MapDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00047598 File Offset: 0x00045798
		public MapBorderSkinType MapBorderSkinType
		{
			get
			{
				if (this.m_mapBorderSkinType == null)
				{
					this.m_mapBorderSkinType = new MapBorderSkinType?(this.m_defObject.MapBorderSkinDef.EvaluateMapBorderSkinType(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_mapBorderSkinType.Value;
			}
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x000475F3 File Offset: 0x000457F3
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_mapBorderSkinType = null;
		}

		// Token: 0x040007FB RID: 2043
		private MapBorderSkin m_defObject;

		// Token: 0x040007FC RID: 2044
		private StyleInstance m_style;

		// Token: 0x040007FD RID: 2045
		private MapBorderSkinType? m_mapBorderSkinType;
	}
}

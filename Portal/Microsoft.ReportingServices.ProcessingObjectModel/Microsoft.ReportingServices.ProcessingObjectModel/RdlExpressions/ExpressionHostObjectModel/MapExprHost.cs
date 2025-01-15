using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000B6 RID: 182
	public abstract class MapExprHost : ReportItemExprHost
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00003973 File Offset: 0x00001B73
		internal IList<MapPolygonLayerExprHost> MapPolygonLayersHostsRemotable
		{
			get
			{
				return this.m_mapPolygonLayersHostsRemotable;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000397B File Offset: 0x00001B7B
		internal IList<MapPointLayerExprHost> MapPointLayersHostsRemotable
		{
			get
			{
				return this.m_mapPointLayersHostsRemotable;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00003983 File Offset: 0x00001B83
		internal IList<MapLineLayerExprHost> MapLineLayersHostsRemotable
		{
			get
			{
				return this.m_mapLineLayersHostsRemotable;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000398B File Offset: 0x00001B8B
		internal IList<MapTileLayerExprHost> MapTileLayersHostsRemotable
		{
			get
			{
				return this.m_mapTileLayersHostsRemotable;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00003993 File Offset: 0x00001B93
		internal IList<MapLegendExprHost> MapLegendsHostsRemotable
		{
			get
			{
				return this.m_mapLegendsHostsRemotable;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000399B File Offset: 0x00001B9B
		internal IList<MapTitleExprHost> MapTitlesHostsRemotable
		{
			get
			{
				return this.m_mapTitlesHostsRemotable;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x000039A3 File Offset: 0x00001BA3
		public virtual object AntiAliasingExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x000039A6 File Offset: 0x00001BA6
		public virtual object TextAntiAliasingQualityExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x000039A9 File Offset: 0x00001BA9
		public virtual object ShadowIntensityExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x000039AC File Offset: 0x00001BAC
		public virtual object TileLanguageExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000130 RID: 304
		public MapViewportExprHost MapViewportHost;

		// Token: 0x04000131 RID: 305
		[CLSCompliant(false)]
		protected IList<MapPolygonLayerExprHost> m_mapPolygonLayersHostsRemotable;

		// Token: 0x04000132 RID: 306
		[CLSCompliant(false)]
		protected IList<MapPointLayerExprHost> m_mapPointLayersHostsRemotable;

		// Token: 0x04000133 RID: 307
		[CLSCompliant(false)]
		protected IList<MapLineLayerExprHost> m_mapLineLayersHostsRemotable;

		// Token: 0x04000134 RID: 308
		[CLSCompliant(false)]
		protected IList<MapTileLayerExprHost> m_mapTileLayersHostsRemotable;

		// Token: 0x04000135 RID: 309
		[CLSCompliant(false)]
		protected IList<MapLegendExprHost> m_mapLegendsHostsRemotable;

		// Token: 0x04000136 RID: 310
		[CLSCompliant(false)]
		protected IList<MapTitleExprHost> m_mapTitlesHostsRemotable;

		// Token: 0x04000137 RID: 311
		public MapDistanceScaleExprHost MapDistanceScaleHost;

		// Token: 0x04000138 RID: 312
		public MapColorScaleExprHost MapColorScaleHost;

		// Token: 0x04000139 RID: 313
		public MapBorderSkinExprHost MapBorderSkinHost;
	}
}

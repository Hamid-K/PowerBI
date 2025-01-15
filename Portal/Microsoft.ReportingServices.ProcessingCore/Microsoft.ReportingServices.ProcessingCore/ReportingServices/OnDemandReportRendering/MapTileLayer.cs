using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001B1 RID: 433
	public sealed class MapTileLayer : MapLayer
	{
		// Token: 0x06001138 RID: 4408 RVA: 0x0004825E File Offset: 0x0004645E
		internal MapTileLayer(MapTileLayer defObject, Map map)
			: base(defObject, map)
		{
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x00048268 File Offset: 0x00046468
		public ReportStringProperty ServiceUrl
		{
			get
			{
				if (this.m_serviceUrl == null && this.MapTileLayerDef.ServiceUrl != null)
				{
					this.m_serviceUrl = new ReportStringProperty(this.MapTileLayerDef.ServiceUrl);
				}
				return this.m_serviceUrl;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x0004829C File Offset: 0x0004649C
		public ReportEnumProperty<MapTileStyle> TileStyle
		{
			get
			{
				if (this.m_tileStyle == null && this.MapTileLayerDef.TileStyle != null)
				{
					this.m_tileStyle = new ReportEnumProperty<MapTileStyle>(this.MapTileLayerDef.TileStyle.IsExpression, this.MapTileLayerDef.TileStyle.OriginalText, EnumTranslator.TranslateMapTileStyle(this.MapTileLayerDef.TileStyle.StringValue, null));
				}
				return this.m_tileStyle;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x00048305 File Offset: 0x00046505
		public ReportBoolProperty UseSecureConnection
		{
			get
			{
				if (this.m_useSecureConnection == null && this.MapTileLayerDef.UseSecureConnection != null)
				{
					this.m_useSecureConnection = new ReportBoolProperty(this.MapTileLayerDef.UseSecureConnection);
				}
				return this.m_useSecureConnection;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x00048338 File Offset: 0x00046538
		public MapTileCollection MapTiles
		{
			get
			{
				if (this.m_mapTiles == null && this.MapTileLayerDef.MapTiles != null)
				{
					this.m_mapTiles = new MapTileCollection(this, this.m_map);
				}
				return this.m_mapTiles;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x00048367 File Offset: 0x00046567
		internal MapTileLayer MapTileLayerDef
		{
			get
			{
				return (MapTileLayer)base.MapLayerDef;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x00048374 File Offset: 0x00046574
		public new MapTileLayerInstance Instance
		{
			get
			{
				return (MapTileLayerInstance)this.GetInstance();
			}
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00048381 File Offset: 0x00046581
		internal override MapLayerInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapTileLayerInstance(this);
			}
			return (MapLayerInstance)this.m_instance;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x000483B6 File Offset: 0x000465B6
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapTiles != null)
			{
				this.m_mapTiles.SetNewContext();
			}
		}

		// Token: 0x04000822 RID: 2082
		private ReportStringProperty m_serviceUrl;

		// Token: 0x04000823 RID: 2083
		private ReportEnumProperty<MapTileStyle> m_tileStyle;

		// Token: 0x04000824 RID: 2084
		private MapTileCollection m_mapTiles;

		// Token: 0x04000825 RID: 2085
		private ReportBoolProperty m_useSecureConnection;
	}
}

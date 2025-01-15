using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001B0 RID: 432
	public sealed class MapTile : MapObjectCollectionItem
	{
		// Token: 0x06001130 RID: 4400 RVA: 0x000481C7 File Offset: 0x000463C7
		internal MapTile(MapTile defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x000481DD File Offset: 0x000463DD
		public string Name
		{
			get
			{
				return this.m_defObject.Name;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x000481EA File Offset: 0x000463EA
		public string TileData
		{
			get
			{
				return this.m_defObject.TileData;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x000481F7 File Offset: 0x000463F7
		public string MIMEType
		{
			get
			{
				return this.m_defObject.MIMEType;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x00048204 File Offset: 0x00046404
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0004820C File Offset: 0x0004640C
		internal MapTile MapTileDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x00048214 File Offset: 0x00046414
		public MapTileInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapTileInstance(this);
				}
				return (MapTileInstance)this.m_instance;
			}
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00048249 File Offset: 0x00046449
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000820 RID: 2080
		private Map m_map;

		// Token: 0x04000821 RID: 2081
		private MapTile m_defObject;
	}
}

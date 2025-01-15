using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001C8 RID: 456
	public sealed class MapMarker : MapObjectCollectionItem
	{
		// Token: 0x060011CC RID: 4556 RVA: 0x00049A92 File Offset: 0x00047C92
		internal MapMarker(MapMarker defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x00049AA8 File Offset: 0x00047CA8
		public ReportEnumProperty<MapMarkerStyle> MapMarkerStyle
		{
			get
			{
				if (this.m_mapMarkerStyle == null && this.m_defObject.MapMarkerStyle != null)
				{
					this.m_mapMarkerStyle = new ReportEnumProperty<MapMarkerStyle>(this.m_defObject.MapMarkerStyle.IsExpression, this.m_defObject.MapMarkerStyle.OriginalText, EnumTranslator.TranslateMapMarkerStyle(this.m_defObject.MapMarkerStyle.StringValue, null));
				}
				return this.m_mapMarkerStyle;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x00049B11 File Offset: 0x00047D11
		public MapMarkerImage MapMarkerImage
		{
			get
			{
				if (this.m_mapMarkerImage == null && this.m_defObject.MapMarkerImage != null)
				{
					this.m_mapMarkerImage = new MapMarkerImage(this.m_defObject.MapMarkerImage, this.m_map);
				}
				return this.m_mapMarkerImage;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x00049B4A File Offset: 0x00047D4A
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x00049B52 File Offset: 0x00047D52
		internal MapMarker MapMarkerDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x00049B5A File Offset: 0x00047D5A
		public MapMarkerInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapMarkerInstance(this);
				}
				return (MapMarkerInstance)this.m_instance;
			}
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00049B8F File Offset: 0x00047D8F
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapMarkerImage != null)
			{
				this.m_mapMarkerImage.SetNewContext();
			}
		}

		// Token: 0x0400086D RID: 2157
		private Map m_map;

		// Token: 0x0400086E RID: 2158
		private MapMarker m_defObject;

		// Token: 0x0400086F RID: 2159
		private ReportEnumProperty<MapMarkerStyle> m_mapMarkerStyle;

		// Token: 0x04000870 RID: 2160
		private MapMarkerImage m_mapMarkerImage;
	}
}

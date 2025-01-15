using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200017D RID: 381
	public sealed class MapBorderSkin : IROMStyleDefinitionContainer
	{
		// Token: 0x06000FE3 RID: 4067 RVA: 0x000445C4 File Offset: 0x000427C4
		internal MapBorderSkin(MapBorderSkin defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x000445DA File Offset: 0x000427DA
		public Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new Style(this.m_map, this.m_map.ReportScope, this.m_defObject, this.m_map.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x00044618 File Offset: 0x00042818
		public ReportEnumProperty<MapBorderSkinType> MapBorderSkinType
		{
			get
			{
				if (this.m_mapBorderSkinType == null && this.m_defObject.MapBorderSkinType != null)
				{
					this.m_mapBorderSkinType = new ReportEnumProperty<MapBorderSkinType>(this.m_defObject.MapBorderSkinType.IsExpression, this.m_defObject.MapBorderSkinType.OriginalText, EnumTranslator.TranslateMapBorderSkinType(this.m_defObject.MapBorderSkinType.StringValue, null));
				}
				return this.m_mapBorderSkinType;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00044681 File Offset: 0x00042881
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00044689 File Offset: 0x00042889
		internal MapBorderSkin MapBorderSkinDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x00044691 File Offset: 0x00042891
		public MapBorderSkinInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapBorderSkinInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x000446C1 File Offset: 0x000428C1
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
		}

		// Token: 0x04000760 RID: 1888
		private Map m_map;

		// Token: 0x04000761 RID: 1889
		private MapBorderSkin m_defObject;

		// Token: 0x04000762 RID: 1890
		private MapBorderSkinInstance m_instance;

		// Token: 0x04000763 RID: 1891
		private Style m_style;

		// Token: 0x04000764 RID: 1892
		private ReportEnumProperty<MapBorderSkinType> m_mapBorderSkinType;
	}
}

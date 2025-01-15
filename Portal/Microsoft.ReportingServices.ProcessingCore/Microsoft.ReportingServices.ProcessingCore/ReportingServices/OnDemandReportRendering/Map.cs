using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000172 RID: 370
	public sealed class Map : ReportItem, IROMActionOwner, IPageBreakItem
	{
		// Token: 0x06000F81 RID: 3969 RVA: 0x000435C6 File Offset: 0x000417C6
		internal Map(IReportScope reportScope, IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, Map reportItemDef, RenderingContext renderingContext)
			: base(reportScope, parentDefinitionPath, indexIntoParentCollectionDef, reportItemDef, renderingContext)
		{
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x000435D5 File Offset: 0x000417D5
		internal Map MapDef
		{
			get
			{
				return (Map)this.m_reportItemDef;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x000435E2 File Offset: 0x000417E2
		public MapDataRegionCollection MapDataRegions
		{
			get
			{
				if (this.m_mapDataRegions == null && this.MapDef.MapDataRegions != null)
				{
					this.m_mapDataRegions = new MapDataRegionCollection(this);
				}
				return this.m_mapDataRegions;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0004360B File Offset: 0x0004180B
		public new MapInstance Instance
		{
			get
			{
				return (MapInstance)this.GetOrCreateInstance();
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x00043618 File Offset: 0x00041818
		public MapViewport MapViewport
		{
			get
			{
				if (this.m_mapViewport == null && this.MapDef.MapViewport != null)
				{
					this.m_mapViewport = new MapViewport(this.MapDef.MapViewport, this);
				}
				return this.m_mapViewport;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0004364C File Offset: 0x0004184C
		public MapLayerCollection MapLayers
		{
			get
			{
				if (this.m_mapLayers == null && this.MapDef.MapLayers != null)
				{
					this.m_mapLayers = new MapLayerCollection(this);
				}
				return this.m_mapLayers;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x00043675 File Offset: 0x00041875
		public MapLegendCollection MapLegends
		{
			get
			{
				if (this.m_mapLegends == null && this.MapDef.MapLegends != null)
				{
					this.m_mapLegends = new MapLegendCollection(this);
				}
				return this.m_mapLegends;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x0004369E File Offset: 0x0004189E
		public MapTitleCollection MapTitles
		{
			get
			{
				if (this.m_mapTitles == null && this.MapDef.MapTitles != null)
				{
					this.m_mapTitles = new MapTitleCollection(this);
				}
				return this.m_mapTitles;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x000436C7 File Offset: 0x000418C7
		public MapDistanceScale MapDistanceScale
		{
			get
			{
				if (this.m_mapDistanceScale == null && this.MapDef.MapDistanceScale != null)
				{
					this.m_mapDistanceScale = new MapDistanceScale(this.MapDef.MapDistanceScale, this);
				}
				return this.m_mapDistanceScale;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x000436FB File Offset: 0x000418FB
		public MapColorScale MapColorScale
		{
			get
			{
				if (this.m_mapColorScale == null && this.MapDef.MapColorScale != null)
				{
					this.m_mapColorScale = new MapColorScale(this.MapDef.MapColorScale, this);
				}
				return this.m_mapColorScale;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x0004372F File Offset: 0x0004192F
		public MapBorderSkin MapBorderSkin
		{
			get
			{
				if (this.m_mapBorderSkin == null && this.MapDef.MapBorderSkin != null)
				{
					this.m_mapBorderSkin = new MapBorderSkin(this.MapDef.MapBorderSkin, this);
				}
				return this.m_mapBorderSkin;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00043764 File Offset: 0x00041964
		public ReportEnumProperty<MapAntiAliasing> AntiAliasing
		{
			get
			{
				if (this.m_antiAliasing == null && this.MapDef.AntiAliasing != null)
				{
					this.m_antiAliasing = new ReportEnumProperty<MapAntiAliasing>(this.MapDef.AntiAliasing.IsExpression, this.MapDef.AntiAliasing.OriginalText, EnumTranslator.TranslateMapAntiAliasing(this.MapDef.AntiAliasing.StringValue, null));
				}
				return this.m_antiAliasing;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x000437D0 File Offset: 0x000419D0
		public ReportEnumProperty<MapTextAntiAliasingQuality> TextAntiAliasingQuality
		{
			get
			{
				if (this.m_textAntiAliasingQuality == null && this.MapDef.TextAntiAliasingQuality != null)
				{
					this.m_textAntiAliasingQuality = new ReportEnumProperty<MapTextAntiAliasingQuality>(this.MapDef.TextAntiAliasingQuality.IsExpression, this.MapDef.TextAntiAliasingQuality.OriginalText, EnumTranslator.TranslateMapTextAntiAliasingQuality(this.MapDef.TextAntiAliasingQuality.StringValue, null));
				}
				return this.m_textAntiAliasingQuality;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00043839 File Offset: 0x00041A39
		public ReportDoubleProperty ShadowIntensity
		{
			get
			{
				if (this.m_shadowIntensity == null && this.MapDef.ShadowIntensity != null)
				{
					this.m_shadowIntensity = new ReportDoubleProperty(this.MapDef.ShadowIntensity);
				}
				return this.m_shadowIntensity;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x0004386C File Offset: 0x00041A6C
		public ReportStringProperty TileLanguage
		{
			get
			{
				if (this.m_tileLanguage == null && this.MapDef.TileLanguage != null)
				{
					this.m_tileLanguage = new ReportStringProperty(this.MapDef.TileLanguage);
				}
				return this.m_tileLanguage;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0004389F File Offset: 0x00041A9F
		public int MaximumSpatialElementCount
		{
			get
			{
				return this.MapDef.MaximumSpatialElementCount;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x000438AC File Offset: 0x00041AAC
		public int MaximumTotalPointCount
		{
			get
			{
				return this.MapDef.MaximumTotalPointCount;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x000438BC File Offset: 0x00041ABC
		public PageBreak PageBreak
		{
			get
			{
				if (this.m_pageBreak == null)
				{
					IPageBreakOwner pageBreakOwner = (Map)this.m_reportItemDef;
					this.m_pageBreak = new PageBreak(this.m_renderingContext, this.ReportScope, pageBreakOwner);
				}
				return this.m_pageBreak;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x000438FB File Offset: 0x00041AFB
		public ReportStringProperty PageName
		{
			get
			{
				if (this.m_pageName == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_pageName = new ReportStringProperty();
					}
					else
					{
						this.m_pageName = new ReportStringProperty(((Map)this.m_reportItemDef).PageName);
					}
				}
				return this.m_pageName;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0004393C File Offset: 0x00041B3C
		[Obsolete("Use PageBreak.BreakLocation instead.")]
		PageBreakLocation IPageBreakItem.PageBreakLocation
		{
			get
			{
				if (((IPageBreakOwner)base.ReportItemDef).PageBreak != null)
				{
					PageBreak pageBreak = this.PageBreak;
					if (pageBreak.HasEnabledInstance)
					{
						return pageBreak.BreakLocation;
					}
				}
				return PageBreakLocation.None;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x00043972 File Offset: 0x00041B72
		string IROMActionOwner.UniqueName
		{
			get
			{
				return this.MapDef.UniqueName;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0004397F File Offset: 0x00041B7F
		List<string> IROMActionOwner.FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x00043984 File Offset: 0x00041B84
		public bool SpecialBorderHandling
		{
			get
			{
				MapBorderSkin mapBorderSkin = this.MapBorderSkin;
				if (mapBorderSkin == null)
				{
					return false;
				}
				ReportEnumProperty<MapBorderSkinType> mapBorderSkinType = mapBorderSkin.MapBorderSkinType;
				if (mapBorderSkinType == null)
				{
					return false;
				}
				MapBorderSkinType mapBorderSkinType2;
				if (!mapBorderSkinType.IsExpression)
				{
					mapBorderSkinType2 = mapBorderSkinType.Value;
				}
				else
				{
					mapBorderSkinType2 = mapBorderSkin.Instance.MapBorderSkinType;
				}
				return mapBorderSkinType2 > MapBorderSkinType.None;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x000439CC File Offset: 0x00041BCC
		public ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.Action action = this.MapDef.Action;
					if (action != null)
					{
						this.m_actionInfo = new ActionInfo(base.RenderingContext, this.ReportScope, action, this.m_reportItemDef, this, this.m_reportItemDef.ObjectType, this.m_reportItemDef.Name, this);
					}
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x00043A2C File Offset: 0x00041C2C
		internal override ReportItemInstance GetOrCreateInstance()
		{
			if (this.m_instance == null)
			{
				this.m_instance = new MapInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00043A48 File Offset: 0x00041C48
		internal override void SetNewContextChildren()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapDataRegions != null)
			{
				this.m_mapDataRegions.SetNewContext();
			}
			if (this.m_mapViewport != null)
			{
				this.m_mapViewport.SetNewContext();
			}
			if (this.m_mapLayers != null)
			{
				this.m_mapLayers.SetNewContext();
			}
			if (this.m_mapLegends != null)
			{
				this.m_mapLegends.SetNewContext();
			}
			if (this.m_mapTitles != null)
			{
				this.m_mapTitles.SetNewContext();
			}
			if (this.m_mapDistanceScale != null)
			{
				this.m_mapDistanceScale.SetNewContext();
			}
			if (this.m_mapColorScale != null)
			{
				this.m_mapColorScale.SetNewContext();
			}
			if (this.m_mapBorderSkin != null)
			{
				this.m_mapBorderSkin.SetNewContext();
			}
			if (this.m_actionInfo != null)
			{
				this.m_actionInfo.SetNewContext();
			}
			if (this.m_pageBreak != null)
			{
				this.m_pageBreak.SetNewContext();
			}
		}

		// Token: 0x04000735 RID: 1845
		private MapDataRegionCollection m_mapDataRegions;

		// Token: 0x04000736 RID: 1846
		private PageBreak m_pageBreak;

		// Token: 0x04000737 RID: 1847
		private ReportStringProperty m_pageName;

		// Token: 0x04000738 RID: 1848
		private MapViewport m_mapViewport;

		// Token: 0x04000739 RID: 1849
		private MapLayerCollection m_mapLayers;

		// Token: 0x0400073A RID: 1850
		private MapLegendCollection m_mapLegends;

		// Token: 0x0400073B RID: 1851
		private MapTitleCollection m_mapTitles;

		// Token: 0x0400073C RID: 1852
		private MapDistanceScale m_mapDistanceScale;

		// Token: 0x0400073D RID: 1853
		private MapColorScale m_mapColorScale;

		// Token: 0x0400073E RID: 1854
		private MapBorderSkin m_mapBorderSkin;

		// Token: 0x0400073F RID: 1855
		private ReportEnumProperty<MapAntiAliasing> m_antiAliasing;

		// Token: 0x04000740 RID: 1856
		private ReportEnumProperty<MapTextAntiAliasingQuality> m_textAntiAliasingQuality;

		// Token: 0x04000741 RID: 1857
		private ReportDoubleProperty m_shadowIntensity;

		// Token: 0x04000742 RID: 1858
		private ReportStringProperty m_tileLanguage;

		// Token: 0x04000743 RID: 1859
		private ActionInfo m_actionInfo;
	}
}

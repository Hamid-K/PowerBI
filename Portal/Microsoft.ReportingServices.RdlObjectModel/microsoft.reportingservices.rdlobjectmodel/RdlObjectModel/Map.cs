using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200019C RID: 412
	public class Map : ReportItem
	{
		// Token: 0x06000D6B RID: 3435 RVA: 0x0002242A File Offset: 0x0002062A
		public Map()
		{
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00022432 File Offset: 0x00020632
		internal Map(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x0002243B File Offset: 0x0002063B
		// (set) Token: 0x06000D6E RID: 3438 RVA: 0x0002244F File Offset: 0x0002064F
		public MapViewport MapViewport
		{
			get
			{
				return (MapViewport)base.PropertyStore.GetObject(18);
			}
			set
			{
				base.PropertyStore.SetObject(18, value);
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0002245F File Offset: 0x0002065F
		// (set) Token: 0x06000D70 RID: 3440 RVA: 0x00022473 File Offset: 0x00020673
		[XmlElement(typeof(RdlCollection<MapDataRegion>))]
		public IList<MapDataRegion> MapDataRegions
		{
			get
			{
				return (IList<MapDataRegion>)base.PropertyStore.GetObject(19);
			}
			set
			{
				base.PropertyStore.SetObject(19, value);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x00022483 File Offset: 0x00020683
		// (set) Token: 0x06000D72 RID: 3442 RVA: 0x00022497 File Offset: 0x00020697
		[XmlElement(typeof(RdlCollection<MapLayer>))]
		public IList<MapLayer> MapLayers
		{
			get
			{
				return (IList<MapLayer>)base.PropertyStore.GetObject(20);
			}
			set
			{
				base.PropertyStore.SetObject(20, value);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x000224A7 File Offset: 0x000206A7
		// (set) Token: 0x06000D74 RID: 3444 RVA: 0x000224BB File Offset: 0x000206BB
		[XmlElement(typeof(RdlCollection<MapLegend>))]
		public IList<MapLegend> MapLegends
		{
			get
			{
				return (IList<MapLegend>)base.PropertyStore.GetObject(21);
			}
			set
			{
				base.PropertyStore.SetObject(21, value);
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x000224CB File Offset: 0x000206CB
		// (set) Token: 0x06000D76 RID: 3446 RVA: 0x000224DF File Offset: 0x000206DF
		[XmlElement(typeof(RdlCollection<MapTitle>))]
		public IList<MapTitle> MapTitles
		{
			get
			{
				return (IList<MapTitle>)base.PropertyStore.GetObject(22);
			}
			set
			{
				base.PropertyStore.SetObject(22, value);
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x000224EF File Offset: 0x000206EF
		// (set) Token: 0x06000D78 RID: 3448 RVA: 0x00022503 File Offset: 0x00020703
		public MapDistanceScale MapDistanceScale
		{
			get
			{
				return (MapDistanceScale)base.PropertyStore.GetObject(23);
			}
			set
			{
				base.PropertyStore.SetObject(23, value);
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00022513 File Offset: 0x00020713
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x00022527 File Offset: 0x00020727
		public MapColorScale MapColorScale
		{
			get
			{
				return (MapColorScale)base.PropertyStore.GetObject(24);
			}
			set
			{
				base.PropertyStore.SetObject(24, value);
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00022537 File Offset: 0x00020737
		// (set) Token: 0x06000D7C RID: 3452 RVA: 0x0002254B File Offset: 0x0002074B
		public MapBorderSkin MapBorderSkin
		{
			get
			{
				return (MapBorderSkin)base.PropertyStore.GetObject(25);
			}
			set
			{
				base.PropertyStore.SetObject(25, value);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x0002255B File Offset: 0x0002075B
		// (set) Token: 0x06000D7E RID: 3454 RVA: 0x0002256F File Offset: 0x0002076F
		public PageBreak PageBreak
		{
			get
			{
				return (PageBreak)base.PropertyStore.GetObject(26);
			}
			set
			{
				base.PropertyStore.SetObject(26, value);
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x0002257F File Offset: 0x0002077F
		// (set) Token: 0x06000D80 RID: 3456 RVA: 0x0002258E File Offset: 0x0002078E
		[ReportExpressionDefaultValue]
		public ReportExpression PageName
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(34);
			}
			set
			{
				base.PropertyStore.SetObject(34, value);
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x000225A3 File Offset: 0x000207A3
		// (set) Token: 0x06000D82 RID: 3458 RVA: 0x000225B2 File Offset: 0x000207B2
		[ReportExpressionDefaultValue(typeof(MapAntiAliasings), MapAntiAliasings.All)]
		public ReportExpression<MapAntiAliasings> AntiAliasing
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapAntiAliasings>>(27);
			}
			set
			{
				base.PropertyStore.SetObject(27, value);
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x000225C7 File Offset: 0x000207C7
		// (set) Token: 0x06000D84 RID: 3460 RVA: 0x000225D6 File Offset: 0x000207D6
		[ReportExpressionDefaultValue(typeof(MapTextAntiAliasingQualities), MapTextAntiAliasingQualities.High)]
		public ReportExpression<MapTextAntiAliasingQualities> TextAntiAliasingQuality
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapTextAntiAliasingQualities>>(28);
			}
			set
			{
				base.PropertyStore.SetObject(28, value);
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x000225EB File Offset: 0x000207EB
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x000225FA File Offset: 0x000207FA
		[ReportExpressionDefaultValue(typeof(double), "25")]
		public ReportExpression<double> ShadowIntensity
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(29);
			}
			set
			{
				base.PropertyStore.SetObject(29, value);
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0002260F File Offset: 0x0002080F
		// (set) Token: 0x06000D88 RID: 3464 RVA: 0x0002261E File Offset: 0x0002081E
		[DefaultValue(20000)]
		public int MaximumSpatialElementCount
		{
			get
			{
				return base.PropertyStore.GetInteger(30);
			}
			set
			{
				base.PropertyStore.SetInteger(30, value);
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0002262E File Offset: 0x0002082E
		// (set) Token: 0x06000D8A RID: 3466 RVA: 0x0002263D File Offset: 0x0002083D
		[DefaultValue(1000000)]
		public int MaximumTotalPointCount
		{
			get
			{
				return base.PropertyStore.GetInteger(31);
			}
			set
			{
				base.PropertyStore.SetInteger(31, value);
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x0002264D File Offset: 0x0002084D
		// (set) Token: 0x06000D8C RID: 3468 RVA: 0x0002265C File Offset: 0x0002085C
		[ReportExpressionDefaultValue]
		public ReportExpression TileLanguage
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(32);
			}
			set
			{
				base.PropertyStore.SetObject(32, value);
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00022674 File Offset: 0x00020874
		public override void Initialize()
		{
			base.Initialize();
			this.MapViewport = new MapViewport();
			this.MapLayers = new RdlCollection<MapLayer>();
			this.MapLegends = new RdlCollection<MapLegend>();
			this.MapTitles = new RdlCollection<MapTitle>();
			this.AntiAliasing = MapAntiAliasings.All;
			this.TextAntiAliasingQuality = MapTextAntiAliasingQualities.High;
			this.ShadowIntensity = 25.0;
			this.MaximumSpatialElementCount = 20000;
			this.MaximumTotalPointCount = 1000000;
		}

		// Token: 0x020003C8 RID: 968
		internal new class Definition : DefinitionStore<Map, Map.Definition.Properties>
		{
			// Token: 0x0600186C RID: 6252 RVA: 0x0003B6E1 File Offset: 0x000398E1
			private Definition()
			{
			}

			// Token: 0x020004E0 RID: 1248
			internal enum Properties
			{
				// Token: 0x04000FCB RID: 4043
				Style,
				// Token: 0x04000FCC RID: 4044
				Name,
				// Token: 0x04000FCD RID: 4045
				ActionInfo,
				// Token: 0x04000FCE RID: 4046
				Top,
				// Token: 0x04000FCF RID: 4047
				Left,
				// Token: 0x04000FD0 RID: 4048
				Height,
				// Token: 0x04000FD1 RID: 4049
				Width,
				// Token: 0x04000FD2 RID: 4050
				ZIndex,
				// Token: 0x04000FD3 RID: 4051
				Visibility,
				// Token: 0x04000FD4 RID: 4052
				ToolTip,
				// Token: 0x04000FD5 RID: 4053
				ToolTipLocID,
				// Token: 0x04000FD6 RID: 4054
				DocumentMapLabel,
				// Token: 0x04000FD7 RID: 4055
				DocumentMapLabelLocID,
				// Token: 0x04000FD8 RID: 4056
				Bookmark,
				// Token: 0x04000FD9 RID: 4057
				RepeatWith,
				// Token: 0x04000FDA RID: 4058
				CustomProperties,
				// Token: 0x04000FDB RID: 4059
				DataElementName,
				// Token: 0x04000FDC RID: 4060
				DataElementOutput,
				// Token: 0x04000FDD RID: 4061
				MapViewport,
				// Token: 0x04000FDE RID: 4062
				MapDataRegions,
				// Token: 0x04000FDF RID: 4063
				MapLayers,
				// Token: 0x04000FE0 RID: 4064
				MapLegends,
				// Token: 0x04000FE1 RID: 4065
				MapTitles,
				// Token: 0x04000FE2 RID: 4066
				MapDistanceScale,
				// Token: 0x04000FE3 RID: 4067
				MapColorScale,
				// Token: 0x04000FE4 RID: 4068
				MapBorderSkin,
				// Token: 0x04000FE5 RID: 4069
				PageBreak,
				// Token: 0x04000FE6 RID: 4070
				AntiAliasing,
				// Token: 0x04000FE7 RID: 4071
				TextAntiAliasingQuality,
				// Token: 0x04000FE8 RID: 4072
				ShadowIntensity,
				// Token: 0x04000FE9 RID: 4073
				MaximumSpatialElementCount,
				// Token: 0x04000FEA RID: 4074
				MaximumTotalPointCount,
				// Token: 0x04000FEB RID: 4075
				TileLanguage,
				// Token: 0x04000FEC RID: 4076
				PropertyCount,
				// Token: 0x04000FED RID: 4077
				PageName
			}
		}
	}
}

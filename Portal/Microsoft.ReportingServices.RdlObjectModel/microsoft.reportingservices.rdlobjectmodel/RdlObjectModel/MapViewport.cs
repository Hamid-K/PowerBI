using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000192 RID: 402
	public class MapViewport : MapSubItem
	{
		// Token: 0x06000CE7 RID: 3303 RVA: 0x00021A4D File Offset: 0x0001FC4D
		public MapViewport()
		{
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00021A55 File Offset: 0x0001FC55
		internal MapViewport(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00021A5E File Offset: 0x0001FC5E
		// (set) Token: 0x06000CEA RID: 3306 RVA: 0x00021A6C File Offset: 0x0001FC6C
		[ReportExpressionDefaultValue(typeof(MapCoordinateSystems), MapCoordinateSystems.Planar)]
		public ReportExpression<MapCoordinateSystems> MapCoordinateSystem
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapCoordinateSystems>>(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00021A80 File Offset: 0x0001FC80
		// (set) Token: 0x06000CEC RID: 3308 RVA: 0x00021A8F File Offset: 0x0001FC8F
		[ReportExpressionDefaultValue(typeof(MapProjections), MapProjections.Equirectangular)]
		public ReportExpression<MapProjections> MapProjection
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapProjections>>(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00021AA4 File Offset: 0x0001FCA4
		// (set) Token: 0x06000CEE RID: 3310 RVA: 0x00021AB3 File Offset: 0x0001FCB3
		public ReportExpression<double> ProjectionCenterX
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x00021AC8 File Offset: 0x0001FCC8
		// (set) Token: 0x06000CF0 RID: 3312 RVA: 0x00021AD7 File Offset: 0x0001FCD7
		public ReportExpression<double> ProjectionCenterY
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x00021AEC File Offset: 0x0001FCEC
		// (set) Token: 0x06000CF2 RID: 3314 RVA: 0x00021B00 File Offset: 0x0001FD00
		public MapLimits MapLimits
		{
			get
			{
				return (MapLimits)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00021B10 File Offset: 0x0001FD10
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x00021B1F File Offset: 0x0001FD1F
		[ReportExpressionDefaultValue(typeof(double), "20000")]
		public ReportExpression<double> MaximumZoom
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00021B34 File Offset: 0x0001FD34
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x00021B43 File Offset: 0x0001FD43
		[ReportExpressionDefaultValue(typeof(double), "20")]
		public ReportExpression<double> MinimumZoom
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00021B58 File Offset: 0x0001FD58
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x00021B67 File Offset: 0x0001FD67
		[ReportExpressionDefaultValue(typeof(double), "0")]
		public ReportExpression<double> SimplificationResolution
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(20);
			}
			set
			{
				base.PropertyStore.SetObject(20, value);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x00021B7C File Offset: 0x0001FD7C
		// (set) Token: 0x06000CFA RID: 3322 RVA: 0x00021B90 File Offset: 0x0001FD90
		public MapView MapView
		{
			get
			{
				return (MapView)base.PropertyStore.GetObject(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00021BA0 File Offset: 0x0001FDA0
		// (set) Token: 0x06000CFC RID: 3324 RVA: 0x00021BAF File Offset: 0x0001FDAF
		[ReportExpressionDefaultValue(typeof(ReportSize), "10pt")]
		public ReportExpression<ReportSize> ContentMargin
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00021BC4 File Offset: 0x0001FDC4
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x00021BD8 File Offset: 0x0001FDD8
		public MapGridLines MapMeridians
		{
			get
			{
				return (MapGridLines)base.PropertyStore.GetObject(17);
			}
			set
			{
				base.PropertyStore.SetObject(17, value);
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x00021BE8 File Offset: 0x0001FDE8
		// (set) Token: 0x06000D00 RID: 3328 RVA: 0x00021BFC File Offset: 0x0001FDFC
		public MapGridLines MapParallels
		{
			get
			{
				return (MapGridLines)base.PropertyStore.GetObject(18);
			}
			set
			{
				base.PropertyStore.SetObject(18, value);
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00021C0C File Offset: 0x0001FE0C
		// (set) Token: 0x06000D02 RID: 3330 RVA: 0x00021C1B File Offset: 0x0001FE1B
		public ReportExpression<bool> GridUnderContent
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(19);
			}
			set
			{
				base.PropertyStore.SetObject(19, value);
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00021C30 File Offset: 0x0001FE30
		public override void Initialize()
		{
			base.Initialize();
			this.MapCoordinateSystem = MapCoordinateSystems.Planar;
			this.MapProjection = MapProjections.Equirectangular;
			this.MaximumZoom = 20000.0;
			this.MinimumZoom = 20.0;
			this.SimplificationResolution = 0.0;
			this.ContentMargin = new ReportExpression<ReportSize>("10pt", CultureInfo.InvariantCulture);
		}

		// Token: 0x020003BF RID: 959
		internal new class Definition : DefinitionStore<MapViewport, MapViewport.Definition.Properties>
		{
			// Token: 0x06001863 RID: 6243 RVA: 0x0003B699 File Offset: 0x00039899
			private Definition()
			{
			}

			// Token: 0x020004D7 RID: 1239
			internal enum Properties
			{
				// Token: 0x04000F48 RID: 3912
				Style,
				// Token: 0x04000F49 RID: 3913
				MapLocation,
				// Token: 0x04000F4A RID: 3914
				MapSize,
				// Token: 0x04000F4B RID: 3915
				LeftMargin,
				// Token: 0x04000F4C RID: 3916
				RightMargin,
				// Token: 0x04000F4D RID: 3917
				TopMargin,
				// Token: 0x04000F4E RID: 3918
				BottomMargin,
				// Token: 0x04000F4F RID: 3919
				ZIndex,
				// Token: 0x04000F50 RID: 3920
				MapCoordinateSystem,
				// Token: 0x04000F51 RID: 3921
				MapProjection,
				// Token: 0x04000F52 RID: 3922
				ProjectionCenterX,
				// Token: 0x04000F53 RID: 3923
				ProjectionCenterY,
				// Token: 0x04000F54 RID: 3924
				MapLimits,
				// Token: 0x04000F55 RID: 3925
				MaximumZoom,
				// Token: 0x04000F56 RID: 3926
				MinimumZoom,
				// Token: 0x04000F57 RID: 3927
				MapView,
				// Token: 0x04000F58 RID: 3928
				ContentMargin,
				// Token: 0x04000F59 RID: 3929
				MapMeridians,
				// Token: 0x04000F5A RID: 3930
				MapParallels,
				// Token: 0x04000F5B RID: 3931
				GridUnderContent,
				// Token: 0x04000F5C RID: 3932
				SimplificationResolution,
				// Token: 0x04000F5D RID: 3933
				PropertyCount
			}
		}
	}
}

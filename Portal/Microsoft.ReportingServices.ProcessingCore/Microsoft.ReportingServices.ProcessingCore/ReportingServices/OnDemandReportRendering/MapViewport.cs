using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001F0 RID: 496
	public sealed class MapViewport : MapSubItem
	{
		// Token: 0x060012A9 RID: 4777 RVA: 0x0004C0BD File Offset: 0x0004A2BD
		internal MapViewport(MapViewport defObject, Map map)
			: base(defObject, map)
		{
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x0004C0C8 File Offset: 0x0004A2C8
		public ReportEnumProperty<MapCoordinateSystem> MapCoordinateSystem
		{
			get
			{
				if (this.m_mapCoordinateSystem == null && this.MapViewportDef.MapCoordinateSystem != null)
				{
					this.m_mapCoordinateSystem = new ReportEnumProperty<MapCoordinateSystem>(this.MapViewportDef.MapCoordinateSystem.IsExpression, this.MapViewportDef.MapCoordinateSystem.OriginalText, EnumTranslator.TranslateMapCoordinateSystem(this.MapViewportDef.MapCoordinateSystem.StringValue, null));
				}
				return this.m_mapCoordinateSystem;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x0004C134 File Offset: 0x0004A334
		public ReportEnumProperty<MapProjection> MapProjection
		{
			get
			{
				if (this.m_mapProjection == null && this.MapViewportDef.MapProjection != null)
				{
					this.m_mapProjection = new ReportEnumProperty<MapProjection>(this.MapViewportDef.MapProjection.IsExpression, this.MapViewportDef.MapProjection.OriginalText, EnumTranslator.TranslateMapProjection(this.MapViewportDef.MapProjection.StringValue, null));
				}
				return this.m_mapProjection;
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0004C19D File Offset: 0x0004A39D
		public ReportDoubleProperty ProjectionCenterX
		{
			get
			{
				if (this.m_projectionCenterX == null && this.MapViewportDef.ProjectionCenterX != null)
				{
					this.m_projectionCenterX = new ReportDoubleProperty(this.MapViewportDef.ProjectionCenterX);
				}
				return this.m_projectionCenterX;
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x0004C1D0 File Offset: 0x0004A3D0
		public ReportDoubleProperty ProjectionCenterY
		{
			get
			{
				if (this.m_projectionCenterY == null && this.MapViewportDef.ProjectionCenterY != null)
				{
					this.m_projectionCenterY = new ReportDoubleProperty(this.MapViewportDef.ProjectionCenterY);
				}
				return this.m_projectionCenterY;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x0004C203 File Offset: 0x0004A403
		public MapLimits MapLimits
		{
			get
			{
				if (this.m_mapLimits == null && this.MapViewportDef.MapLimits != null)
				{
					this.m_mapLimits = new MapLimits(this.MapViewportDef.MapLimits, this.m_map);
				}
				return this.m_mapLimits;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x0004C23C File Offset: 0x0004A43C
		public MapView MapView
		{
			get
			{
				if (this.m_mapView == null)
				{
					MapView mapView = this.MapViewportDef.MapView;
					if (mapView != null)
					{
						if (mapView is MapCustomView)
						{
							this.m_mapView = new MapCustomView((MapCustomView)this.MapViewportDef.MapView, this.m_map);
						}
						else if (mapView is MapElementView)
						{
							this.m_mapView = new MapElementView((MapElementView)this.MapViewportDef.MapView, this.m_map);
						}
						if (mapView is MapDataBoundView)
						{
							this.m_mapView = new MapDataBoundView((MapDataBoundView)this.MapViewportDef.MapView, this.m_map);
						}
					}
				}
				return this.m_mapView;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x0004C2E6 File Offset: 0x0004A4E6
		public ReportDoubleProperty MaximumZoom
		{
			get
			{
				if (this.m_maximumZoom == null && this.MapViewportDef.MaximumZoom != null)
				{
					this.m_maximumZoom = new ReportDoubleProperty(this.MapViewportDef.MaximumZoom);
				}
				return this.m_maximumZoom;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x0004C319 File Offset: 0x0004A519
		public ReportDoubleProperty MinimumZoom
		{
			get
			{
				if (this.m_minimumZoom == null && this.MapViewportDef.MinimumZoom != null)
				{
					this.m_minimumZoom = new ReportDoubleProperty(this.MapViewportDef.MinimumZoom);
				}
				return this.m_minimumZoom;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x0004C34C File Offset: 0x0004A54C
		public ReportSizeProperty ContentMargin
		{
			get
			{
				if (this.m_contentMargin == null && this.MapViewportDef.ContentMargin != null)
				{
					this.m_contentMargin = new ReportSizeProperty(this.MapViewportDef.ContentMargin);
				}
				return this.m_contentMargin;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x0004C37F File Offset: 0x0004A57F
		public ReportDoubleProperty SimplificationResolution
		{
			get
			{
				if (this.m_simplificationResolution == null && this.MapViewportDef.SimplificationResolution != null)
				{
					this.m_simplificationResolution = new ReportDoubleProperty(this.MapViewportDef.SimplificationResolution);
				}
				return this.m_simplificationResolution;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x0004C3B2 File Offset: 0x0004A5B2
		public MapGridLines MapMeridians
		{
			get
			{
				if (this.m_mapMeridians == null && this.MapViewportDef.MapMeridians != null)
				{
					this.m_mapMeridians = new MapGridLines(this.MapViewportDef.MapMeridians, this.m_map);
				}
				return this.m_mapMeridians;
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x0004C3EB File Offset: 0x0004A5EB
		public MapGridLines MapParallels
		{
			get
			{
				if (this.m_mapParallels == null && this.MapViewportDef.MapParallels != null)
				{
					this.m_mapParallels = new MapGridLines(this.MapViewportDef.MapParallels, this.m_map);
				}
				return this.m_mapParallels;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x0004C424 File Offset: 0x0004A624
		public ReportBoolProperty GridUnderContent
		{
			get
			{
				if (this.m_gridUnderContent == null && this.MapViewportDef.GridUnderContent != null)
				{
					this.m_gridUnderContent = new ReportBoolProperty(this.MapViewportDef.GridUnderContent);
				}
				return this.m_gridUnderContent;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x0004C457 File Offset: 0x0004A657
		internal MapViewport MapViewportDef
		{
			get
			{
				return (MapViewport)this.m_defObject;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x0004C464 File Offset: 0x0004A664
		public new MapViewportInstance Instance
		{
			get
			{
				return (MapViewportInstance)this.GetInstance();
			}
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0004C471 File Offset: 0x0004A671
		internal override MapSubItemInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapViewportInstance(this);
			}
			return (MapSubItemInstance)this.m_instance;
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0004C4A8 File Offset: 0x0004A6A8
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapLimits != null)
			{
				this.m_mapLimits.SetNewContext();
			}
			if (this.m_mapView != null)
			{
				this.m_mapView.SetNewContext();
			}
			if (this.m_mapMeridians != null)
			{
				this.m_mapMeridians.SetNewContext();
			}
			if (this.m_mapParallels != null)
			{
				this.m_mapParallels.SetNewContext();
			}
		}

		// Token: 0x040008EE RID: 2286
		private ReportEnumProperty<MapCoordinateSystem> m_mapCoordinateSystem;

		// Token: 0x040008EF RID: 2287
		private ReportEnumProperty<MapProjection> m_mapProjection;

		// Token: 0x040008F0 RID: 2288
		private ReportDoubleProperty m_projectionCenterX;

		// Token: 0x040008F1 RID: 2289
		private ReportDoubleProperty m_projectionCenterY;

		// Token: 0x040008F2 RID: 2290
		private MapLimits m_mapLimits;

		// Token: 0x040008F3 RID: 2291
		private MapView m_mapView;

		// Token: 0x040008F4 RID: 2292
		private ReportDoubleProperty m_maximumZoom;

		// Token: 0x040008F5 RID: 2293
		private ReportDoubleProperty m_minimumZoom;

		// Token: 0x040008F6 RID: 2294
		private ReportSizeProperty m_contentMargin;

		// Token: 0x040008F7 RID: 2295
		private MapGridLines m_mapMeridians;

		// Token: 0x040008F8 RID: 2296
		private MapGridLines m_mapParallels;

		// Token: 0x040008F9 RID: 2297
		private ReportBoolProperty m_gridUnderContent;

		// Token: 0x040008FA RID: 2298
		private ReportDoubleProperty m_simplificationResolution;
	}
}

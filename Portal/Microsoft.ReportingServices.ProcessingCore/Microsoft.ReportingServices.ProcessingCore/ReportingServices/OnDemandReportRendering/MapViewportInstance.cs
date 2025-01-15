using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001F7 RID: 503
	public sealed class MapViewportInstance : MapSubItemInstance
	{
		// Token: 0x060012E6 RID: 4838 RVA: 0x0004CB54 File Offset: 0x0004AD54
		internal MapViewportInstance(MapViewport defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x0004CB64 File Offset: 0x0004AD64
		public MapCoordinateSystem MapCoordinateSystem
		{
			get
			{
				if (this.m_mapCoordinateSystem == null)
				{
					this.m_mapCoordinateSystem = new MapCoordinateSystem?(((MapViewport)this.m_defObject.MapSubItemDef).EvaluateMapCoordinateSystem(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_mapCoordinateSystem.Value;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x0004CBC4 File Offset: 0x0004ADC4
		public MapProjection MapProjection
		{
			get
			{
				if (this.m_mapProjection == null)
				{
					this.m_mapProjection = new MapProjection?(((MapViewport)this.m_defObject.MapSubItemDef).EvaluateMapProjection(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_mapProjection.Value;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x0004CC24 File Offset: 0x0004AE24
		public double ProjectionCenterX
		{
			get
			{
				if (this.m_projectionCenterX == null)
				{
					this.m_projectionCenterX = new double?(((MapViewport)this.m_defObject.MapSubItemDef).EvaluateProjectionCenterX(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_projectionCenterX.Value;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x0004CC88 File Offset: 0x0004AE88
		public double ProjectionCenterY
		{
			get
			{
				if (this.m_projectionCenterY == null)
				{
					this.m_projectionCenterY = new double?(((MapViewport)this.m_defObject.MapSubItemDef).EvaluateProjectionCenterY(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_projectionCenterY.Value;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x0004CCEC File Offset: 0x0004AEEC
		public double MaximumZoom
		{
			get
			{
				if (this.m_maximumZoom == null)
				{
					this.m_maximumZoom = new double?(((MapViewport)this.m_defObject.MapSubItemDef).EvaluateMaximumZoom(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_maximumZoom.Value;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x0004CD50 File Offset: 0x0004AF50
		public double MinimumZoom
		{
			get
			{
				if (this.m_minimumZoom == null)
				{
					this.m_minimumZoom = new double?(((MapViewport)this.m_defObject.MapSubItemDef).EvaluateMinimumZoom(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_minimumZoom.Value;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x0004CDB4 File Offset: 0x0004AFB4
		public double SimplificationResolution
		{
			get
			{
				if (this.m_simplificationResolution == null)
				{
					this.m_simplificationResolution = new double?(((MapViewport)this.m_defObject.MapSubItemDef).EvaluateSimplificationResolution(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_simplificationResolution.Value;
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x0004CE18 File Offset: 0x0004B018
		public ReportSize ContentMargin
		{
			get
			{
				if (this.m_contentMargin == null)
				{
					this.m_contentMargin = new ReportSize(((MapViewport)this.m_defObject.MapSubItemDef).EvaluateContentMargin(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_contentMargin;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x0004CE70 File Offset: 0x0004B070
		public bool GridUnderContent
		{
			get
			{
				if (this.m_gridUnderContent == null)
				{
					this.m_gridUnderContent = new bool?(((MapViewport)this.m_defObject.MapSubItemDef).EvaluateGridUnderContent(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_gridUnderContent.Value;
			}
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0004CED0 File Offset: 0x0004B0D0
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_mapCoordinateSystem = null;
			this.m_mapProjection = null;
			this.m_projectionCenterX = null;
			this.m_projectionCenterY = null;
			this.m_maximumZoom = null;
			this.m_minimumZoom = null;
			this.m_contentMargin = null;
			this.m_gridUnderContent = null;
			this.m_simplificationResolution = null;
		}

		// Token: 0x04000911 RID: 2321
		private MapViewport m_defObject;

		// Token: 0x04000912 RID: 2322
		private MapCoordinateSystem? m_mapCoordinateSystem;

		// Token: 0x04000913 RID: 2323
		private MapProjection? m_mapProjection;

		// Token: 0x04000914 RID: 2324
		private double? m_projectionCenterX;

		// Token: 0x04000915 RID: 2325
		private double? m_projectionCenterY;

		// Token: 0x04000916 RID: 2326
		private double? m_maximumZoom;

		// Token: 0x04000917 RID: 2327
		private double? m_minimumZoom;

		// Token: 0x04000918 RID: 2328
		private ReportSize m_contentMargin;

		// Token: 0x04000919 RID: 2329
		private bool? m_gridUnderContent;

		// Token: 0x0400091A RID: 2330
		private double? m_simplificationResolution;
	}
}

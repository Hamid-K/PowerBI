using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001B5 RID: 437
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapLayerInstance : BaseInstance
	{
		// Token: 0x06001152 RID: 4434 RVA: 0x00048685 File Offset: 0x00046885
		internal MapLayerInstance(MapLayer defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x000486A0 File Offset: 0x000468A0
		public MapVisibilityMode VisibilityMode
		{
			get
			{
				if (this.m_visibilityMode == null)
				{
					this.m_visibilityMode = new MapVisibilityMode?(this.m_defObject.MapLayerDef.EvaluateVisibilityMode(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_visibilityMode.Value;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x000486FC File Offset: 0x000468FC
		public double MinimumZoom
		{
			get
			{
				if (this.m_minimumZoom == null)
				{
					this.m_minimumZoom = new double?(this.m_defObject.MapLayerDef.EvaluateMinimumZoom(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_minimumZoom.Value;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x00048758 File Offset: 0x00046958
		public double MaximumZoom
		{
			get
			{
				if (this.m_maximumZoom == null)
				{
					this.m_maximumZoom = new double?(this.m_defObject.MapLayerDef.EvaluateMaximumZoom(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_maximumZoom.Value;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x000487B4 File Offset: 0x000469B4
		public double Transparency
		{
			get
			{
				if (this.m_transparency == null)
				{
					this.m_transparency = new double?(this.m_defObject.MapLayerDef.EvaluateTransparency(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_transparency.Value;
			}
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00048810 File Offset: 0x00046A10
		protected override void ResetInstanceCache()
		{
			this.m_visibilityMode = null;
			this.m_minimumZoom = null;
			this.m_maximumZoom = null;
			this.m_transparency = null;
		}

		// Token: 0x0400082E RID: 2094
		private MapLayer m_defObject;

		// Token: 0x0400082F RID: 2095
		private MapVisibilityMode? m_visibilityMode;

		// Token: 0x04000830 RID: 2096
		private double? m_minimumZoom;

		// Token: 0x04000831 RID: 2097
		private double? m_maximumZoom;

		// Token: 0x04000832 RID: 2098
		private double? m_transparency;
	}
}

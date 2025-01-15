using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200011C RID: 284
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class GaugePointerInstance : BaseInstance
	{
		// Token: 0x06000C89 RID: 3209 RVA: 0x00036156 File Offset: 0x00034356
		internal GaugePointerInstance(GaugePointer defObject)
			: base(defObject.GaugePanelDef)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0003616B File Offset: 0x0003436B
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_defObject, this.m_defObject.GaugePanelDef, this.m_defObject.GaugePanelDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x000361A8 File Offset: 0x000343A8
		public GaugeBarStarts BarStart
		{
			get
			{
				if (this.m_barStart == null)
				{
					this.m_barStart = new GaugeBarStarts?(this.m_defObject.GaugePointerDef.EvaluateBarStart(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_barStart.Value;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00036204 File Offset: 0x00034404
		public double DistanceFromScale
		{
			get
			{
				if (this.m_distanceFromScale == null)
				{
					this.m_distanceFromScale = new double?(this.m_defObject.GaugePointerDef.EvaluateDistanceFromScale(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_distanceFromScale.Value;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x00036260 File Offset: 0x00034460
		public double MarkerLength
		{
			get
			{
				if (this.m_markerLength == null)
				{
					this.m_markerLength = new double?(this.m_defObject.GaugePointerDef.EvaluateMarkerLength(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_markerLength.Value;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x000362BC File Offset: 0x000344BC
		public GaugeMarkerStyles MarkerStyle
		{
			get
			{
				if (this.m_markerStyle == null)
				{
					this.m_markerStyle = new GaugeMarkerStyles?(this.m_defObject.GaugePointerDef.EvaluateMarkerStyle(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_markerStyle.Value;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x00036318 File Offset: 0x00034518
		public GaugePointerPlacements Placement
		{
			get
			{
				if (this.m_placement == null)
				{
					this.m_placement = new GaugePointerPlacements?(this.m_defObject.GaugePointerDef.EvaluatePlacement(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_placement.Value;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x00036374 File Offset: 0x00034574
		public bool SnappingEnabled
		{
			get
			{
				if (this.m_snappingEnabled == null)
				{
					this.m_snappingEnabled = new bool?(this.m_defObject.GaugePointerDef.EvaluateSnappingEnabled(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_snappingEnabled.Value;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x000363D0 File Offset: 0x000345D0
		public double SnappingInterval
		{
			get
			{
				if (this.m_snappingInterval == null)
				{
					this.m_snappingInterval = new double?(this.m_defObject.GaugePointerDef.EvaluateSnappingInterval(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_snappingInterval.Value;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0003642C File Offset: 0x0003462C
		public string ToolTip
		{
			get
			{
				if (this.m_toolTip == null)
				{
					this.m_toolTip = this.m_defObject.GaugePointerDef.EvaluateToolTip(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x00036478 File Offset: 0x00034678
		public bool Hidden
		{
			get
			{
				if (this.m_hidden == null)
				{
					this.m_hidden = new bool?(this.m_defObject.GaugePointerDef.EvaluateHidden(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_hidden.Value;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x000364D4 File Offset: 0x000346D4
		public double Width
		{
			get
			{
				if (this.m_width == null)
				{
					this.m_width = new double?(this.m_defObject.GaugePointerDef.EvaluateWidth(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_width.Value;
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00036530 File Offset: 0x00034730
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_barStart = null;
			this.m_distanceFromScale = null;
			this.m_markerLength = null;
			this.m_markerStyle = null;
			this.m_placement = null;
			this.m_snappingEnabled = null;
			this.m_snappingInterval = null;
			this.m_toolTip = null;
			this.m_hidden = null;
			this.m_width = null;
		}

		// Token: 0x04000579 RID: 1401
		private GaugePointer m_defObject;

		// Token: 0x0400057A RID: 1402
		private StyleInstance m_style;

		// Token: 0x0400057B RID: 1403
		private GaugeBarStarts? m_barStart;

		// Token: 0x0400057C RID: 1404
		private double? m_distanceFromScale;

		// Token: 0x0400057D RID: 1405
		private double? m_markerLength;

		// Token: 0x0400057E RID: 1406
		private GaugeMarkerStyles? m_markerStyle;

		// Token: 0x0400057F RID: 1407
		private GaugePointerPlacements? m_placement;

		// Token: 0x04000580 RID: 1408
		private bool? m_snappingEnabled;

		// Token: 0x04000581 RID: 1409
		private double? m_snappingInterval;

		// Token: 0x04000582 RID: 1410
		private string m_toolTip;

		// Token: 0x04000583 RID: 1411
		private bool? m_hidden;

		// Token: 0x04000584 RID: 1412
		private double? m_width;
	}
}

using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200012E RID: 302
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class TickMarkStyleInstance : BaseInstance
	{
		// Token: 0x06000D3A RID: 3386 RVA: 0x00038979 File Offset: 0x00036B79
		internal TickMarkStyleInstance(TickMarkStyle defObject)
			: base(defObject.GaugePanelDef)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x0003898E File Offset: 0x00036B8E
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

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x000389CC File Offset: 0x00036BCC
		public double DistanceFromScale
		{
			get
			{
				if (this.m_distanceFromScale == null)
				{
					this.m_distanceFromScale = new double?(this.m_defObject.TickMarkStyleDef.EvaluateDistanceFromScale(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_distanceFromScale.Value;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00038A28 File Offset: 0x00036C28
		public GaugeLabelPlacements Placement
		{
			get
			{
				if (this.m_placement == null)
				{
					this.m_placement = new GaugeLabelPlacements?(this.m_defObject.TickMarkStyleDef.EvaluatePlacement(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_placement.Value;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00038A84 File Offset: 0x00036C84
		public bool EnableGradient
		{
			get
			{
				if (this.m_enableGradient == null)
				{
					this.m_enableGradient = new bool?(this.m_defObject.TickMarkStyleDef.EvaluateEnableGradient(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_enableGradient.Value;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x00038AE0 File Offset: 0x00036CE0
		public double GradientDensity
		{
			get
			{
				if (this.m_gradientDensity == null)
				{
					this.m_gradientDensity = new double?(this.m_defObject.TickMarkStyleDef.EvaluateGradientDensity(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_gradientDensity.Value;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00038B3C File Offset: 0x00036D3C
		public double Length
		{
			get
			{
				if (this.m_length == null)
				{
					this.m_length = new double?(this.m_defObject.TickMarkStyleDef.EvaluateLength(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_length.Value;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00038B98 File Offset: 0x00036D98
		public double Width
		{
			get
			{
				if (this.m_width == null)
				{
					this.m_width = new double?(this.m_defObject.TickMarkStyleDef.EvaluateWidth(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_width.Value;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00038BF4 File Offset: 0x00036DF4
		public GaugeTickMarkShapes Shape
		{
			get
			{
				if (this.m_shape == null)
				{
					this.m_shape = new GaugeTickMarkShapes?(this.m_defObject.TickMarkStyleDef.EvaluateShape(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_shape.Value;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x00038C50 File Offset: 0x00036E50
		public bool Hidden
		{
			get
			{
				if (this.m_hidden == null)
				{
					this.m_hidden = new bool?(this.m_defObject.TickMarkStyleDef.EvaluateHidden(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_hidden.Value;
			}
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00038CAC File Offset: 0x00036EAC
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_distanceFromScale = null;
			this.m_placement = null;
			this.m_enableGradient = null;
			this.m_gradientDensity = null;
			this.m_length = null;
			this.m_width = null;
			this.m_shape = null;
			this.m_hidden = null;
		}

		// Token: 0x040005FD RID: 1533
		protected TickMarkStyle m_defObject;

		// Token: 0x040005FE RID: 1534
		private StyleInstance m_style;

		// Token: 0x040005FF RID: 1535
		private double? m_distanceFromScale;

		// Token: 0x04000600 RID: 1536
		private GaugeLabelPlacements? m_placement;

		// Token: 0x04000601 RID: 1537
		private bool? m_enableGradient;

		// Token: 0x04000602 RID: 1538
		private double? m_gradientDensity;

		// Token: 0x04000603 RID: 1539
		private double? m_length;

		// Token: 0x04000604 RID: 1540
		private double? m_width;

		// Token: 0x04000605 RID: 1541
		private GaugeTickMarkShapes? m_shape;

		// Token: 0x04000606 RID: 1542
		private bool? m_hidden;
	}
}

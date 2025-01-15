using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000125 RID: 293
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class GaugeScaleInstance : BaseInstance
	{
		// Token: 0x06000CE8 RID: 3304 RVA: 0x00037571 File Offset: 0x00035771
		internal GaugeScaleInstance(GaugeScale defObject)
			: base(defObject.GaugePanelDef)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00037586 File Offset: 0x00035786
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

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x000375C4 File Offset: 0x000357C4
		public double Interval
		{
			get
			{
				if (this.m_interval == null)
				{
					this.m_interval = new double?(this.m_defObject.GaugeScaleDef.EvaluateInterval(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_interval.Value;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00037620 File Offset: 0x00035820
		public double IntervalOffset
		{
			get
			{
				if (this.m_intervalOffset == null)
				{
					this.m_intervalOffset = new double?(this.m_defObject.GaugeScaleDef.EvaluateIntervalOffset(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_intervalOffset.Value;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0003767C File Offset: 0x0003587C
		public bool Logarithmic
		{
			get
			{
				if (this.m_logarithmic == null)
				{
					this.m_logarithmic = new bool?(this.m_defObject.GaugeScaleDef.EvaluateLogarithmic(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_logarithmic.Value;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x000376D8 File Offset: 0x000358D8
		public double LogarithmicBase
		{
			get
			{
				if (this.m_logarithmicBase == null)
				{
					this.m_logarithmicBase = new double?(this.m_defObject.GaugeScaleDef.EvaluateLogarithmicBase(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_logarithmicBase.Value;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00037734 File Offset: 0x00035934
		public double Multiplier
		{
			get
			{
				if (this.m_multiplier == null)
				{
					this.m_multiplier = new double?(this.m_defObject.GaugeScaleDef.EvaluateMultiplier(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_multiplier.Value;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x00037790 File Offset: 0x00035990
		public bool Reversed
		{
			get
			{
				if (this.m_reversed == null)
				{
					this.m_reversed = new bool?(this.m_defObject.GaugeScaleDef.EvaluateReversed(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_reversed.Value;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x000377EC File Offset: 0x000359EC
		public bool TickMarksOnTop
		{
			get
			{
				if (this.m_tickMarksOnTop == null)
				{
					this.m_tickMarksOnTop = new bool?(this.m_defObject.GaugeScaleDef.EvaluateTickMarksOnTop(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_tickMarksOnTop.Value;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x00037848 File Offset: 0x00035A48
		public string ToolTip
		{
			get
			{
				if (this.m_toolTip == null)
				{
					this.m_toolTip = this.m_defObject.GaugeScaleDef.EvaluateToolTip(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00037894 File Offset: 0x00035A94
		public bool Hidden
		{
			get
			{
				if (this.m_hidden == null)
				{
					this.m_hidden = new bool?(this.m_defObject.GaugeScaleDef.EvaluateHidden(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_hidden.Value;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x000378F0 File Offset: 0x00035AF0
		public double Width
		{
			get
			{
				if (this.m_width == null)
				{
					this.m_width = new double?(this.m_defObject.GaugeScaleDef.EvaluateWidth(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_width.Value;
			}
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0003794C File Offset: 0x00035B4C
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_interval = null;
			this.m_intervalOffset = null;
			this.m_logarithmic = null;
			this.m_logarithmicBase = null;
			this.m_multiplier = null;
			this.m_reversed = null;
			this.m_tickMarksOnTop = null;
			this.m_toolTip = null;
			this.m_hidden = null;
			this.m_width = null;
		}

		// Token: 0x040005BF RID: 1471
		private GaugeScale m_defObject;

		// Token: 0x040005C0 RID: 1472
		private StyleInstance m_style;

		// Token: 0x040005C1 RID: 1473
		private double? m_interval;

		// Token: 0x040005C2 RID: 1474
		private double? m_intervalOffset;

		// Token: 0x040005C3 RID: 1475
		private bool? m_logarithmic;

		// Token: 0x040005C4 RID: 1476
		private double? m_logarithmicBase;

		// Token: 0x040005C5 RID: 1477
		private double? m_multiplier;

		// Token: 0x040005C6 RID: 1478
		private bool? m_reversed;

		// Token: 0x040005C7 RID: 1479
		private bool? m_tickMarksOnTop;

		// Token: 0x040005C8 RID: 1480
		private string m_toolTip;

		// Token: 0x040005C9 RID: 1481
		private bool? m_hidden;

		// Token: 0x040005CA RID: 1482
		private double? m_width;
	}
}

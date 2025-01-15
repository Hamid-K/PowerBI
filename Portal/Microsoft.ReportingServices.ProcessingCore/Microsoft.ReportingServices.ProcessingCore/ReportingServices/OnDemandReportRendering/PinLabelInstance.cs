using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000108 RID: 264
	public sealed class PinLabelInstance : BaseInstance
	{
		// Token: 0x06000BA7 RID: 2983 RVA: 0x000335E1 File Offset: 0x000317E1
		internal PinLabelInstance(PinLabel defObject)
			: base(defObject.GaugePanelDef)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x000335F6 File Offset: 0x000317F6
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

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00033634 File Offset: 0x00031834
		public string Text
		{
			get
			{
				if (this.m_text == null)
				{
					this.m_text = this.m_defObject.PinLabelDef.EvaluateText(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext);
				}
				return this.m_text;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00033680 File Offset: 0x00031880
		public bool AllowUpsideDown
		{
			get
			{
				if (this.m_allowUpsideDown == null)
				{
					this.m_allowUpsideDown = new bool?(this.m_defObject.PinLabelDef.EvaluateAllowUpsideDown(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_allowUpsideDown.Value;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x000336DC File Offset: 0x000318DC
		public double DistanceFromScale
		{
			get
			{
				if (this.m_distanceFromScale == null)
				{
					this.m_distanceFromScale = new double?(this.m_defObject.PinLabelDef.EvaluateDistanceFromScale(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_distanceFromScale.Value;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00033738 File Offset: 0x00031938
		public double FontAngle
		{
			get
			{
				if (this.m_fontAngle == null)
				{
					this.m_fontAngle = new double?(this.m_defObject.PinLabelDef.EvaluateFontAngle(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_fontAngle.Value;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x00033794 File Offset: 0x00031994
		public GaugeLabelPlacements Placement
		{
			get
			{
				if (this.m_placement == null)
				{
					this.m_placement = new GaugeLabelPlacements?(this.m_defObject.PinLabelDef.EvaluatePlacement(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_placement.Value;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x000337F0 File Offset: 0x000319F0
		public bool RotateLabel
		{
			get
			{
				if (this.m_rotateLabel == null)
				{
					this.m_rotateLabel = new bool?(this.m_defObject.PinLabelDef.EvaluateRotateLabel(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_rotateLabel.Value;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0003384C File Offset: 0x00031A4C
		public bool UseFontPercent
		{
			get
			{
				if (this.m_useFontPercent == null)
				{
					this.m_useFontPercent = new bool?(this.m_defObject.PinLabelDef.EvaluateUseFontPercent(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_useFontPercent.Value;
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000338A8 File Offset: 0x00031AA8
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_text = null;
			this.m_allowUpsideDown = null;
			this.m_distanceFromScale = null;
			this.m_fontAngle = null;
			this.m_placement = null;
			this.m_rotateLabel = null;
			this.m_useFontPercent = null;
		}

		// Token: 0x04000500 RID: 1280
		private PinLabel m_defObject;

		// Token: 0x04000501 RID: 1281
		private StyleInstance m_style;

		// Token: 0x04000502 RID: 1282
		private string m_text;

		// Token: 0x04000503 RID: 1283
		private bool? m_allowUpsideDown;

		// Token: 0x04000504 RID: 1284
		private double? m_distanceFromScale;

		// Token: 0x04000505 RID: 1285
		private double? m_fontAngle;

		// Token: 0x04000506 RID: 1286
		private GaugeLabelPlacements? m_placement;

		// Token: 0x04000507 RID: 1287
		private bool? m_rotateLabel;

		// Token: 0x04000508 RID: 1288
		private bool? m_useFontPercent;
	}
}

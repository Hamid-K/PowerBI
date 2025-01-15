using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000128 RID: 296
	public sealed class ScaleLabelsInstance : BaseInstance
	{
		// Token: 0x06000CFF RID: 3327 RVA: 0x00037CAD File Offset: 0x00035EAD
		internal ScaleLabelsInstance(ScaleLabels defObject)
			: base(defObject.GaugePanelDef)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00037CC2 File Offset: 0x00035EC2
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

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00037D00 File Offset: 0x00035F00
		public double Interval
		{
			get
			{
				if (this.m_interval == null)
				{
					this.m_interval = new double?(this.m_defObject.ScaleLabelsDef.EvaluateInterval(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_interval.Value;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00037D5C File Offset: 0x00035F5C
		public double IntervalOffset
		{
			get
			{
				if (this.m_intervalOffset == null)
				{
					this.m_intervalOffset = new double?(this.m_defObject.ScaleLabelsDef.EvaluateIntervalOffset(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_intervalOffset.Value;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00037DB8 File Offset: 0x00035FB8
		public bool AllowUpsideDown
		{
			get
			{
				if (this.m_allowUpsideDown == null)
				{
					this.m_allowUpsideDown = new bool?(this.m_defObject.ScaleLabelsDef.EvaluateAllowUpsideDown(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_allowUpsideDown.Value;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00037E14 File Offset: 0x00036014
		public double DistanceFromScale
		{
			get
			{
				if (this.m_distanceFromScale == null)
				{
					this.m_distanceFromScale = new double?(this.m_defObject.ScaleLabelsDef.EvaluateDistanceFromScale(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_distanceFromScale.Value;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00037E70 File Offset: 0x00036070
		public double FontAngle
		{
			get
			{
				if (this.m_fontAngle == null)
				{
					this.m_fontAngle = new double?(this.m_defObject.ScaleLabelsDef.EvaluateFontAngle(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_fontAngle.Value;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00037ECC File Offset: 0x000360CC
		public GaugeLabelPlacements Placement
		{
			get
			{
				if (this.m_placement == null)
				{
					this.m_placement = new GaugeLabelPlacements?(this.m_defObject.ScaleLabelsDef.EvaluatePlacement(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_placement.Value;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00037F28 File Offset: 0x00036128
		public bool RotateLabels
		{
			get
			{
				if (this.m_rotateLabels == null)
				{
					this.m_rotateLabels = new bool?(this.m_defObject.ScaleLabelsDef.EvaluateRotateLabels(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_rotateLabels.Value;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00037F84 File Offset: 0x00036184
		public bool ShowEndLabels
		{
			get
			{
				if (this.m_showEndLabels == null)
				{
					this.m_showEndLabels = new bool?(this.m_defObject.ScaleLabelsDef.EvaluateShowEndLabels(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_showEndLabels.Value;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x00037FE0 File Offset: 0x000361E0
		public bool Hidden
		{
			get
			{
				if (this.m_hidden == null)
				{
					this.m_hidden = new bool?(this.m_defObject.ScaleLabelsDef.EvaluateHidden(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_hidden.Value;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0003803C File Offset: 0x0003623C
		public bool UseFontPercent
		{
			get
			{
				if (this.m_useFontPercent == null)
				{
					this.m_useFontPercent = new bool?(this.m_defObject.ScaleLabelsDef.EvaluateUseFontPercent(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_useFontPercent.Value;
			}
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00038098 File Offset: 0x00036298
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_interval = null;
			this.m_intervalOffset = null;
			this.m_allowUpsideDown = null;
			this.m_distanceFromScale = null;
			this.m_fontAngle = null;
			this.m_placement = null;
			this.m_rotateLabels = null;
			this.m_showEndLabels = null;
			this.m_hidden = null;
			this.m_useFontPercent = null;
		}

		// Token: 0x040005D3 RID: 1491
		private ScaleLabels m_defObject;

		// Token: 0x040005D4 RID: 1492
		private StyleInstance m_style;

		// Token: 0x040005D5 RID: 1493
		private double? m_interval;

		// Token: 0x040005D6 RID: 1494
		private double? m_intervalOffset;

		// Token: 0x040005D7 RID: 1495
		private bool? m_allowUpsideDown;

		// Token: 0x040005D8 RID: 1496
		private double? m_distanceFromScale;

		// Token: 0x040005D9 RID: 1497
		private double? m_fontAngle;

		// Token: 0x040005DA RID: 1498
		private GaugeLabelPlacements? m_placement;

		// Token: 0x040005DB RID: 1499
		private bool? m_rotateLabels;

		// Token: 0x040005DC RID: 1500
		private bool? m_showEndLabels;

		// Token: 0x040005DD RID: 1501
		private bool? m_hidden;

		// Token: 0x040005DE RID: 1502
		private bool? m_useFontPercent;
	}
}

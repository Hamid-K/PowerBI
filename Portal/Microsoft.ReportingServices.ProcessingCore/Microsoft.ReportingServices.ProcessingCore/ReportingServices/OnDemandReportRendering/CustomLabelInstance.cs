using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000EF RID: 239
	public sealed class CustomLabelInstance : BaseInstance
	{
		// Token: 0x06000B19 RID: 2841 RVA: 0x00031B4E File Offset: 0x0002FD4E
		internal CustomLabelInstance(CustomLabel defObject)
			: base(defObject.GaugePanelDef)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00031B63 File Offset: 0x0002FD63
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

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00031BA0 File Offset: 0x0002FDA0
		public string Text
		{
			get
			{
				if (this.m_text == null)
				{
					this.m_text = this.m_defObject.CustomLabelDef.EvaluateText(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext);
				}
				return this.m_text;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00031BEC File Offset: 0x0002FDEC
		public bool AllowUpsideDown
		{
			get
			{
				if (this.m_allowUpsideDown == null)
				{
					this.m_allowUpsideDown = new bool?(this.m_defObject.CustomLabelDef.EvaluateAllowUpsideDown(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_allowUpsideDown.Value;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00031C48 File Offset: 0x0002FE48
		public double DistanceFromScale
		{
			get
			{
				if (this.m_distanceFromScale == null)
				{
					this.m_distanceFromScale = new double?(this.m_defObject.CustomLabelDef.EvaluateDistanceFromScale(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_distanceFromScale.Value;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00031CA4 File Offset: 0x0002FEA4
		public double FontAngle
		{
			get
			{
				if (this.m_fontAngle == null)
				{
					this.m_fontAngle = new double?(this.m_defObject.CustomLabelDef.EvaluateFontAngle(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_fontAngle.Value;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00031D00 File Offset: 0x0002FF00
		public GaugeLabelPlacements Placement
		{
			get
			{
				if (this.m_placement == null)
				{
					this.m_placement = new GaugeLabelPlacements?(this.m_defObject.CustomLabelDef.EvaluatePlacement(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_placement.Value;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00031D5C File Offset: 0x0002FF5C
		public bool RotateLabel
		{
			get
			{
				if (this.m_rotateLabel == null)
				{
					this.m_rotateLabel = new bool?(this.m_defObject.CustomLabelDef.EvaluateRotateLabel(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_rotateLabel.Value;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00031DB8 File Offset: 0x0002FFB8
		public double Value
		{
			get
			{
				if (this.m_value == null)
				{
					this.m_value = new double?(this.m_defObject.CustomLabelDef.EvaluateValue(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_value.Value;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00031E14 File Offset: 0x00030014
		public bool Hidden
		{
			get
			{
				if (this.m_hidden == null)
				{
					this.m_hidden = new bool?(this.m_defObject.CustomLabelDef.EvaluateHidden(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_hidden.Value;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00031E70 File Offset: 0x00030070
		public bool UseFontPercent
		{
			get
			{
				if (this.m_useFontPercent == null)
				{
					this.m_useFontPercent = new bool?(this.m_defObject.CustomLabelDef.EvaluateUseFontPercent(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_useFontPercent.Value;
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00031ECC File Offset: 0x000300CC
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
			this.m_value = null;
			this.m_hidden = null;
			this.m_useFontPercent = null;
		}

		// Token: 0x040004B7 RID: 1207
		private CustomLabel m_defObject;

		// Token: 0x040004B8 RID: 1208
		private StyleInstance m_style;

		// Token: 0x040004B9 RID: 1209
		private string m_text;

		// Token: 0x040004BA RID: 1210
		private bool? m_allowUpsideDown;

		// Token: 0x040004BB RID: 1211
		private double? m_distanceFromScale;

		// Token: 0x040004BC RID: 1212
		private double? m_fontAngle;

		// Token: 0x040004BD RID: 1213
		private GaugeLabelPlacements? m_placement;

		// Token: 0x040004BE RID: 1214
		private bool? m_rotateLabel;

		// Token: 0x040004BF RID: 1215
		private double? m_value;

		// Token: 0x040004C0 RID: 1216
		private bool? m_hidden;

		// Token: 0x040004C1 RID: 1217
		private bool? m_useFontPercent;
	}
}

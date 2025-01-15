using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000105 RID: 261
	public sealed class GaugeLabel : GaugePanelItem
	{
		// Token: 0x06000B84 RID: 2948 RVA: 0x00032FD1 File Offset: 0x000311D1
		internal GaugeLabel(GaugeLabel defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x00032FE9 File Offset: 0x000311E9
		public ReportStringProperty Text
		{
			get
			{
				if (this.m_text == null && this.GaugeLabelDef.Text != null)
				{
					this.m_text = new ReportStringProperty(this.GaugeLabelDef.Text);
				}
				return this.m_text;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0003301C File Offset: 0x0003121C
		public ReportDoubleProperty Angle
		{
			get
			{
				if (this.m_angle == null && this.GaugeLabelDef.Angle != null)
				{
					this.m_angle = new ReportDoubleProperty(this.GaugeLabelDef.Angle);
				}
				return this.m_angle;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x00033050 File Offset: 0x00031250
		public ReportEnumProperty<GaugeResizeModes> ResizeMode
		{
			get
			{
				if (this.m_resizeMode == null && this.GaugeLabelDef.ResizeMode != null)
				{
					this.m_resizeMode = new ReportEnumProperty<GaugeResizeModes>(this.GaugeLabelDef.ResizeMode.IsExpression, this.GaugeLabelDef.ResizeMode.OriginalText, EnumTranslator.TranslateGaugeResizeModes(this.GaugeLabelDef.ResizeMode.StringValue, null));
				}
				return this.m_resizeMode;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x000330B9 File Offset: 0x000312B9
		public ReportSizeProperty TextShadowOffset
		{
			get
			{
				if (this.m_textShadowOffset == null && this.GaugeLabelDef.TextShadowOffset != null)
				{
					this.m_textShadowOffset = new ReportSizeProperty(this.GaugeLabelDef.TextShadowOffset);
				}
				return this.m_textShadowOffset;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x000330EC File Offset: 0x000312EC
		public ReportBoolProperty UseFontPercent
		{
			get
			{
				if (this.m_useFontPercent == null && this.GaugeLabelDef.UseFontPercent != null)
				{
					this.m_useFontPercent = new ReportBoolProperty(this.GaugeLabelDef.UseFontPercent);
				}
				return this.m_useFontPercent;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0003311F File Offset: 0x0003131F
		internal GaugeLabel GaugeLabelDef
		{
			get
			{
				return (GaugeLabel)this.m_defObject;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0003312C File Offset: 0x0003132C
		public new GaugeLabelInstance Instance
		{
			get
			{
				return (GaugeLabelInstance)this.GetInstance();
			}
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00033139 File Offset: 0x00031339
		internal override BaseInstance GetInstance()
		{
			if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new GaugeLabelInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00033169 File Offset: 0x00031369
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x040004E9 RID: 1257
		private ReportStringProperty m_text;

		// Token: 0x040004EA RID: 1258
		private ReportDoubleProperty m_angle;

		// Token: 0x040004EB RID: 1259
		private ReportEnumProperty<GaugeResizeModes> m_resizeMode;

		// Token: 0x040004EC RID: 1260
		private ReportSizeProperty m_textShadowOffset;

		// Token: 0x040004ED RID: 1261
		private ReportBoolProperty m_useFontPercent;
	}
}

using System;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000107 RID: 263
	public sealed class GaugeLabelInstance : GaugePanelItemInstance
	{
		// Token: 0x06000B9B RID: 2971 RVA: 0x000333D7 File Offset: 0x000315D7
		internal GaugeLabelInstance(GaugeLabel defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x000333E7 File Offset: 0x000315E7
		public string Text
		{
			get
			{
				if (this.m_formattedText == null)
				{
					this.EnsureTextEvaluated();
					this.m_formattedText = this.RifGaugeLabel.FormatText(this.m_textResult.Value, this.OdpContext);
				}
				return this.m_formattedText;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0003341F File Offset: 0x0003161F
		internal object OriginalValue
		{
			get
			{
				this.EnsureTextEvaluated();
				return this.m_textResult.Value.Value;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00033437 File Offset: 0x00031637
		internal TypeCode TypeCode
		{
			get
			{
				this.EnsureTextEvaluated();
				return this.m_textResult.Value.TypeCode;
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0003344F File Offset: 0x0003164F
		private void EnsureTextEvaluated()
		{
			if (this.m_textResult == null)
			{
				this.m_textResult = new VariantResult?(this.RifGaugeLabel.EvaluateText(this.ReportScopeInstance, this.OdpContext));
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00033480 File Offset: 0x00031680
		public double Angle
		{
			get
			{
				if (this.m_angle == null)
				{
					this.m_angle = new double?(this.RifGaugeLabel.EvaluateAngle(this.ReportScopeInstance, this.OdpContext));
				}
				return this.m_angle.Value;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x000334BD File Offset: 0x000316BD
		public GaugeResizeModes ResizeMode
		{
			get
			{
				if (this.m_resizeMode == null)
				{
					this.m_resizeMode = new GaugeResizeModes?(this.RifGaugeLabel.EvaluateResizeMode(this.ReportScopeInstance, this.OdpContext));
				}
				return this.m_resizeMode.Value;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x000334F9 File Offset: 0x000316F9
		public ReportSize TextShadowOffset
		{
			get
			{
				if (this.m_textShadowOffset == null)
				{
					this.m_textShadowOffset = new ReportSize(this.RifGaugeLabel.EvaluateTextShadowOffset(this.ReportScopeInstance, this.OdpContext));
				}
				return this.m_textShadowOffset;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0003352B File Offset: 0x0003172B
		public bool UseFontPercent
		{
			get
			{
				if (this.m_useFontPercent == null)
				{
					this.m_useFontPercent = new bool?(this.RifGaugeLabel.EvaluateUseFontPercent(this.ReportScopeInstance, this.OdpContext));
				}
				return this.m_useFontPercent.Value;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x00033567 File Offset: 0x00031767
		private GaugeLabel RifGaugeLabel
		{
			get
			{
				return (GaugeLabel)this.m_defObject.GaugePanelItemDef;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00033579 File Offset: 0x00031779
		private OnDemandProcessingContext OdpContext
		{
			get
			{
				return this.m_defObject.GaugePanelDef.RenderingContext.OdpContext;
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00033590 File Offset: 0x00031790
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_textResult = null;
			this.m_formattedText = null;
			this.m_angle = null;
			this.m_resizeMode = null;
			this.m_textShadowOffset = null;
			this.m_useFontPercent = null;
		}

		// Token: 0x040004F9 RID: 1273
		private GaugeLabel m_defObject;

		// Token: 0x040004FA RID: 1274
		private VariantResult? m_textResult;

		// Token: 0x040004FB RID: 1275
		private string m_formattedText;

		// Token: 0x040004FC RID: 1276
		private double? m_angle;

		// Token: 0x040004FD RID: 1277
		private GaugeResizeModes? m_resizeMode;

		// Token: 0x040004FE RID: 1278
		private ReportSize m_textShadowOffset;

		// Token: 0x040004FF RID: 1279
		private bool? m_useFontPercent;
	}
}

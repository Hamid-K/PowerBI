using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200011F RID: 287
	public sealed class PointerCapInstance : BaseInstance
	{
		// Token: 0x06000C9C RID: 3228 RVA: 0x00036660 File Offset: 0x00034860
		internal PointerCapInstance(PointerCap defObject)
			: base(defObject.GaugePanelDef)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x00036675 File Offset: 0x00034875
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

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x000366B4 File Offset: 0x000348B4
		public bool OnTop
		{
			get
			{
				if (this.m_onTop == null)
				{
					this.m_onTop = new bool?(this.m_defObject.PointerCapDef.EvaluateOnTop(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_onTop.Value;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x00036710 File Offset: 0x00034910
		public bool Reflection
		{
			get
			{
				if (this.m_reflection == null)
				{
					this.m_reflection = new bool?(this.m_defObject.PointerCapDef.EvaluateReflection(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_reflection.Value;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0003676C File Offset: 0x0003496C
		public GaugeCapStyles CapStyle
		{
			get
			{
				if (this.m_capStyle == null)
				{
					this.m_capStyle = new GaugeCapStyles?(this.m_defObject.PointerCapDef.EvaluateCapStyle(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_capStyle.Value;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x000367C8 File Offset: 0x000349C8
		public bool Hidden
		{
			get
			{
				if (this.m_hidden == null)
				{
					this.m_hidden = new bool?(this.m_defObject.PointerCapDef.EvaluateHidden(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_hidden.Value;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00036824 File Offset: 0x00034A24
		public double Width
		{
			get
			{
				if (this.m_width == null)
				{
					this.m_width = new double?(this.m_defObject.PointerCapDef.EvaluateWidth(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_width.Value;
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00036880 File Offset: 0x00034A80
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_onTop = null;
			this.m_reflection = null;
			this.m_capStyle = null;
			this.m_hidden = null;
			this.m_width = null;
		}

		// Token: 0x04000588 RID: 1416
		private PointerCap m_defObject;

		// Token: 0x04000589 RID: 1417
		private StyleInstance m_style;

		// Token: 0x0400058A RID: 1418
		private bool? m_onTop;

		// Token: 0x0400058B RID: 1419
		private bool? m_reflection;

		// Token: 0x0400058C RID: 1420
		private GaugeCapStyles? m_capStyle;

		// Token: 0x0400058D RID: 1421
		private bool? m_hidden;

		// Token: 0x0400058E RID: 1422
		private double? m_width;
	}
}

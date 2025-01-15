using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000126 RID: 294
	public sealed class LinearScaleInstance : GaugeScaleInstance
	{
		// Token: 0x06000CF5 RID: 3317 RVA: 0x000379DF File Offset: 0x00035BDF
		internal LinearScaleInstance(LinearScale defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x000379F0 File Offset: 0x00035BF0
		public double StartMargin
		{
			get
			{
				if (this.m_startMargin == null)
				{
					this.m_startMargin = new double?(((LinearScale)this.m_defObject.GaugeScaleDef).EvaluateStartMargin(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_startMargin.Value;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00037A54 File Offset: 0x00035C54
		public double EndMargin
		{
			get
			{
				if (this.m_endMargin == null)
				{
					this.m_endMargin = new double?(((LinearScale)this.m_defObject.GaugeScaleDef).EvaluateEndMargin(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_endMargin.Value;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x00037AB8 File Offset: 0x00035CB8
		public double Position
		{
			get
			{
				if (this.m_position == null)
				{
					this.m_position = new double?(((LinearScale)this.m_defObject.GaugeScaleDef).EvaluatePosition(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_position.Value;
			}
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00037B19 File Offset: 0x00035D19
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_startMargin = null;
			this.m_endMargin = null;
			this.m_position = null;
		}

		// Token: 0x040005CB RID: 1483
		private LinearScale m_defObject;

		// Token: 0x040005CC RID: 1484
		private double? m_startMargin;

		// Token: 0x040005CD RID: 1485
		private double? m_endMargin;

		// Token: 0x040005CE RID: 1486
		private double? m_position;
	}
}

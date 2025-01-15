using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000131 RID: 305
	public sealed class NumericIndicator : GaugePanelItem
	{
		// Token: 0x06000D4D RID: 3405 RVA: 0x00038F18 File Offset: 0x00037118
		internal NumericIndicator(NumericIndicator defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00038F22 File Offset: 0x00037122
		internal NumericIndicator NumericIndicatorDef
		{
			get
			{
				return (NumericIndicator)this.m_defObject;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x00038F2F File Offset: 0x0003712F
		public new NumericIndicatorInstance Instance
		{
			get
			{
				return (NumericIndicatorInstance)this.GetInstance();
			}
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00038F3C File Offset: 0x0003713C
		internal override BaseInstance GetInstance()
		{
			if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new NumericIndicatorInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00038F6C File Offset: 0x0003716C
		internal override void SetNewContext()
		{
			base.SetNewContext();
		}
	}
}

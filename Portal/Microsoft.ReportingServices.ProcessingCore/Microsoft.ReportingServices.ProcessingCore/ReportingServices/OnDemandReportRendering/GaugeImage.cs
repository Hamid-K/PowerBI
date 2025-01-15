using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000E5 RID: 229
	public sealed class GaugeImage : GaugePanelItem
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x00030FFF File Offset: 0x0002F1FF
		internal GaugeImage(GaugeImage defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00031017 File Offset: 0x0002F217
		internal GaugeImage GaugeImageDef
		{
			get
			{
				return (GaugeImage)this.m_defObject;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00031024 File Offset: 0x0002F224
		public new GaugeImageInstance Instance
		{
			get
			{
				return (GaugeImageInstance)this.GetInstance();
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00031031 File Offset: 0x0002F231
		internal override BaseInstance GetInstance()
		{
			if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new GaugeImageInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00031061 File Offset: 0x0002F261
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}
	}
}

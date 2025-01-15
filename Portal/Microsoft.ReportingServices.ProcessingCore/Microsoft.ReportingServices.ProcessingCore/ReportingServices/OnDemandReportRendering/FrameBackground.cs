using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000DD RID: 221
	public sealed class FrameBackground : IROMStyleDefinitionContainer
	{
		// Token: 0x06000A96 RID: 2710 RVA: 0x00030439 File Offset: 0x0002E639
		internal FrameBackground(FrameBackground defObject, GaugePanel gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0003044F File Offset: 0x0002E64F
		public Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new Style(this.m_gaugePanel, this.m_gaugePanel, this.m_defObject, this.m_gaugePanel.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x00030487 File Offset: 0x0002E687
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0003048F File Offset: 0x0002E68F
		internal FrameBackground FrameBackgroundDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00030497 File Offset: 0x0002E697
		public FrameBackgroundInstance Instance
		{
			get
			{
				if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new FrameBackgroundInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000304C7 File Offset: 0x0002E6C7
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
		}

		// Token: 0x04000479 RID: 1145
		private GaugePanel m_gaugePanel;

		// Token: 0x0400047A RID: 1146
		private FrameBackground m_defObject;

		// Token: 0x0400047B RID: 1147
		private FrameBackgroundInstance m_instance;

		// Token: 0x0400047C RID: 1148
		private Style m_style;
	}
}

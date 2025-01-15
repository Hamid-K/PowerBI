using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000DF RID: 223
	public sealed class FrameBackgroundInstance : BaseInstance
	{
		// Token: 0x06000AA3 RID: 2723 RVA: 0x00030700 File Offset: 0x0002E900
		internal FrameBackgroundInstance(FrameBackground defObject)
			: base(defObject.GaugePanelDef)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00030715 File Offset: 0x0002E915
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

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00030751 File Offset: 0x0002E951
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
		}

		// Token: 0x04000483 RID: 1155
		private FrameBackground m_defObject;

		// Token: 0x04000484 RID: 1156
		private StyleInstance m_style;
	}
}

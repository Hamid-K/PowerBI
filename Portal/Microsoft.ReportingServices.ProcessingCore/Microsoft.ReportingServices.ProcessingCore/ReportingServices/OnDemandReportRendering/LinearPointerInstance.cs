using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200011E RID: 286
	public sealed class LinearPointerInstance : GaugePointerInstance
	{
		// Token: 0x06000C99 RID: 3225 RVA: 0x000365DC File Offset: 0x000347DC
		internal LinearPointerInstance(LinearPointer defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x000365EC File Offset: 0x000347EC
		public LinearPointerTypes Type
		{
			get
			{
				if (this.m_type == null)
				{
					this.m_type = new LinearPointerTypes?(((LinearPointer)this.m_defObject.GaugePointerDef).EvaluateType(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_type.Value;
			}
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0003664C File Offset: 0x0003484C
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_type = null;
		}

		// Token: 0x04000586 RID: 1414
		private LinearPointer m_defObject;

		// Token: 0x04000587 RID: 1415
		private LinearPointerTypes? m_type;
	}
}

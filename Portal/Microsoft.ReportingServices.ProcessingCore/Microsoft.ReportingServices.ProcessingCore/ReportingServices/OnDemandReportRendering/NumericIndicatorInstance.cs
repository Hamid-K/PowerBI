using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000132 RID: 306
	public sealed class NumericIndicatorInstance : GaugePanelItemInstance
	{
		// Token: 0x06000D52 RID: 3410 RVA: 0x00038F74 File Offset: 0x00037174
		internal NumericIndicatorInstance(NumericIndicator defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00038F84 File Offset: 0x00037184
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
		}

		// Token: 0x0400060B RID: 1547
		private NumericIndicator m_defObject;
	}
}

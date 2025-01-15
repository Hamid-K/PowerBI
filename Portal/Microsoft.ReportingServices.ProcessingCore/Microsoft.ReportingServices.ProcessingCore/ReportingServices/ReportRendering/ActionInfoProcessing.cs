using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200000D RID: 13
	internal sealed class ActionInfoProcessing : MemberBase
	{
		// Token: 0x06000311 RID: 785 RVA: 0x000075B3 File Offset: 0x000057B3
		internal ActionInfoProcessing()
			: base(true)
		{
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000075BC File Offset: 0x000057BC
		internal ActionInfoProcessing DeepClone()
		{
			Global.Tracer.Assert(this.m_sharedStyles == null && this.m_nonSharedStyles == null);
			ActionInfoProcessing actionInfoProcessing = new ActionInfoProcessing();
			if (this.m_style != null)
			{
				this.m_style.ExtractRenderStyles(out actionInfoProcessing.m_sharedStyles, out actionInfoProcessing.m_nonSharedStyles);
			}
			if (this.m_actionCollection != null)
			{
				actionInfoProcessing.m_actionCollection = this.m_actionCollection.DeepClone();
			}
			return actionInfoProcessing;
		}

		// Token: 0x04000026 RID: 38
		internal ActionStyle m_style;

		// Token: 0x04000027 RID: 39
		internal ActionCollection m_actionCollection;

		// Token: 0x04000028 RID: 40
		internal DataValueInstanceList m_sharedStyles;

		// Token: 0x04000029 RID: 41
		internal DataValueInstanceList m_nonSharedStyles;
	}
}

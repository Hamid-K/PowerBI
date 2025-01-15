using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000008 RID: 8
	internal sealed class ActionRendering : MemberBase
	{
		// Token: 0x060002FE RID: 766 RVA: 0x00006BBD File Offset: 0x00004DBD
		internal ActionRendering()
			: base(false)
		{
		}

		// Token: 0x04000017 RID: 23
		internal ActionItem m_actionDef;

		// Token: 0x04000018 RID: 24
		internal ReportUrl m_actionURL;

		// Token: 0x04000019 RID: 25
		internal ActionItemInstance m_actionInstance;

		// Token: 0x0400001A RID: 26
		internal RenderingContext m_renderingContext;

		// Token: 0x0400001B RID: 27
		internal string m_drillthroughId;
	}
}

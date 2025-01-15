using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200000C RID: 12
	internal sealed class ActionInfoRendering : MemberBase
	{
		// Token: 0x06000310 RID: 784 RVA: 0x000075AA File Offset: 0x000057AA
		internal ActionInfoRendering()
			: base(false)
		{
		}

		// Token: 0x04000020 RID: 32
		internal Microsoft.ReportingServices.ReportProcessing.Action m_actionInfoDef;

		// Token: 0x04000021 RID: 33
		internal ActionInstance m_actionInfoInstance;

		// Token: 0x04000022 RID: 34
		internal RenderingContext m_renderingContext;

		// Token: 0x04000023 RID: 35
		internal ActionStyle m_style;

		// Token: 0x04000024 RID: 36
		internal ActionCollection m_actionCollection;

		// Token: 0x04000025 RID: 37
		internal string m_ownerUniqueName;
	}
}

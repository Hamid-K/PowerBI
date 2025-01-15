using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200000F RID: 15
	internal sealed class ActionCollectionRendering : MemberBase
	{
		// Token: 0x0600031C RID: 796 RVA: 0x000079D5 File Offset: 0x00005BD5
		internal ActionCollectionRendering()
			: base(false)
		{
		}

		// Token: 0x0400002B RID: 43
		internal ActionItemList m_actionList;

		// Token: 0x0400002C RID: 44
		internal ActionItemInstanceList m_actionInstanceList;

		// Token: 0x0400002D RID: 45
		internal RenderingContext m_renderingContext;

		// Token: 0x0400002E RID: 46
		internal Microsoft.ReportingServices.ReportRendering.Action[] m_actions;

		// Token: 0x0400002F RID: 47
		internal string m_ownerUniqueName;
	}
}

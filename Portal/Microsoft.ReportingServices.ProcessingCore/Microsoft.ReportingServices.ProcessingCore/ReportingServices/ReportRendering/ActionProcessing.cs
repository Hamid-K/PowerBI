using System;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000009 RID: 9
	internal sealed class ActionProcessing : MemberBase
	{
		// Token: 0x060002FF RID: 767 RVA: 0x00006BC6 File Offset: 0x00004DC6
		internal ActionProcessing()
			: base(true)
		{
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00006BD0 File Offset: 0x00004DD0
		internal ActionProcessing DeepClone()
		{
			ActionProcessing actionProcessing = new ActionProcessing();
			if (this.m_label != null)
			{
				actionProcessing.m_label = string.Copy(this.m_label);
			}
			if (this.m_action != null)
			{
				actionProcessing.m_action = string.Copy(this.m_action);
			}
			return actionProcessing;
		}

		// Token: 0x0400001C RID: 28
		internal string m_label;

		// Token: 0x0400001D RID: 29
		internal string m_action;
	}
}

using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000010 RID: 16
	internal sealed class ActionCollectionProcessing : MemberBase
	{
		// Token: 0x0600031D RID: 797 RVA: 0x000079DE File Offset: 0x00005BDE
		internal ActionCollectionProcessing()
			: base(true)
		{
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000079E8 File Offset: 0x00005BE8
		internal ActionCollectionProcessing DeepClone()
		{
			if (this.m_actions == null || this.m_actions.Count == 0)
			{
				return null;
			}
			ActionCollectionProcessing actionCollectionProcessing = new ActionCollectionProcessing();
			int count = this.m_actions.Count;
			actionCollectionProcessing.m_actions = new ArrayList();
			for (int i = 0; i < count; i++)
			{
				actionCollectionProcessing.m_actions.Add(((Action)this.m_actions[i]).DeepClone());
			}
			return actionCollectionProcessing;
		}

		// Token: 0x04000030 RID: 48
		internal ArrayList m_actions;
	}
}

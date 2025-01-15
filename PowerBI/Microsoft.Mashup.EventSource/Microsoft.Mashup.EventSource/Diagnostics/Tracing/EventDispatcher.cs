using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000020 RID: 32
	internal class EventDispatcher
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00008C9A File Offset: 0x00006E9A
		internal EventDispatcher(EventDispatcher next, bool[] eventEnabled, EventListener listener)
		{
			this.m_Next = next;
			this.m_EventEnabled = eventEnabled;
			this.m_Listener = listener;
		}

		// Token: 0x0400008B RID: 139
		internal readonly EventListener m_Listener;

		// Token: 0x0400008C RID: 140
		internal bool[] m_EventEnabled;

		// Token: 0x0400008D RID: 141
		internal EventDispatcher m_Next;
	}
}

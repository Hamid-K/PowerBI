using System;
using System.Runtime.Serialization;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000CA RID: 202
	[Serializable]
	public class EventLocationException : XEventException
	{
		// Token: 0x06000275 RID: 629 RVA: 0x0001C28C File Offset: 0x0001C28C
		public EventLocationException()
		{
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0001C2A0 File Offset: 0x0001C2A0
		public EventLocationException(string message)
			: base(message)
		{
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0001C2B4 File Offset: 0x0001C2B4
		public EventLocationException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0001C2CC File Offset: 0x0001C2CC
		protected EventLocationException(SerializationInfo info, StreamingContext context)
		{
		}
	}
}

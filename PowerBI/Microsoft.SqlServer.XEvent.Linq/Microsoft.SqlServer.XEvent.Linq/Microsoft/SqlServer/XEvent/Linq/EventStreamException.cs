using System;
using System.Runtime.Serialization;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000CE RID: 206
	[Serializable]
	public class EventStreamException : XEventException
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0001C4B0 File Offset: 0x0001C4B0
		public EventStreamException()
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0001C4C4 File Offset: 0x0001C4C4
		public EventStreamException(string message)
			: base(message)
		{
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0001C4D8 File Offset: 0x0001C4D8
		public EventStreamException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0001C4F0 File Offset: 0x0001C4F0
		protected EventStreamException(SerializationInfo info, StreamingContext context)
		{
		}
	}
}

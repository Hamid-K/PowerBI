using System;
using System.Runtime.Serialization;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000CF RID: 207
	[Serializable]
	public class EventStreamVersionException : XEventException
	{
		// Token: 0x06000292 RID: 658 RVA: 0x0001C504 File Offset: 0x0001C504
		public EventStreamVersionException()
		{
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0001C518 File Offset: 0x0001C518
		public EventStreamVersionException(string message)
			: base(message)
		{
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0001C52C File Offset: 0x0001C52C
		public EventStreamVersionException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0001C544 File Offset: 0x0001C544
		protected EventStreamVersionException(SerializationInfo info, StreamingContext context)
		{
		}
	}
}

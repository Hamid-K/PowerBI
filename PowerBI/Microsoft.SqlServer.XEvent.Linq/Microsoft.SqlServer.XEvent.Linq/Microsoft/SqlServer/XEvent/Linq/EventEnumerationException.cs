using System;
using System.Runtime.Serialization;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000C9 RID: 201
	[Serializable]
	public class EventEnumerationException : XEventException
	{
		// Token: 0x06000271 RID: 625 RVA: 0x0001C238 File Offset: 0x0001C238
		public EventEnumerationException()
		{
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0001C24C File Offset: 0x0001C24C
		public EventEnumerationException(string message)
			: base(message)
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0001C260 File Offset: 0x0001C260
		public EventEnumerationException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0001C278 File Offset: 0x0001C278
		protected EventEnumerationException(SerializationInfo info, StreamingContext context)
		{
		}
	}
}

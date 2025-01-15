using System;
using System.Runtime.Serialization;
using Microsoft.Diagnostics.Tracing.Internal;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class EventSourceException : Exception
	{
		// Token: 0x0600014B RID: 331 RVA: 0x0000ACA0 File Offset: 0x00008EA0
		public EventSourceException()
			: base(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_ListenerWriteFailure", Array.Empty<object>()))
		{
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000ACB7 File Offset: 0x00008EB7
		public EventSourceException(string message)
			: base(message)
		{
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000ACC0 File Offset: 0x00008EC0
		public EventSourceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000ACCA File Offset: 0x00008ECA
		protected EventSourceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000ACD4 File Offset: 0x00008ED4
		internal EventSourceException(Exception innerException)
			: base(Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("EventSource_ListenerWriteFailure", Array.Empty<object>()), innerException)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003D0 RID: 976
	[Serializable]
	internal class EventLogWriterException : Exception
	{
		// Token: 0x0600225B RID: 8795 RVA: 0x00069F6A File Offset: 0x0006816A
		public EventLogWriterException()
			: base("EventLogWriter failure encountered.")
		{
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x0001E135 File Offset: 0x0001C335
		public EventLogWriterException(string message)
			: base(message)
		{
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x0001E13E File Offset: 0x0001C33E
		public EventLogWriterException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x0001E148 File Offset: 0x0001C348
		protected EventLogWriterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

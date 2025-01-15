using System;
using System.Runtime.Serialization;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000CB RID: 203
	[Serializable]
	public class TypeNotMappedException : XEventException
	{
		// Token: 0x06000279 RID: 633 RVA: 0x0001C2E0 File Offset: 0x0001C2E0
		public TypeNotMappedException()
		{
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0001C2F4 File Offset: 0x0001C2F4
		public TypeNotMappedException(string message)
			: base(message)
		{
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0001C308 File Offset: 0x0001C308
		public TypeNotMappedException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0001C320 File Offset: 0x0001C320
		protected TypeNotMappedException(SerializationInfo info, StreamingContext context)
		{
		}
	}
}

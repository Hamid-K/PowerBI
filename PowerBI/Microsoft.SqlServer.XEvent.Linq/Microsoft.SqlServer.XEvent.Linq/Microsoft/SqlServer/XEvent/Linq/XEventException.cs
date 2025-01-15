using System;
using System.Runtime.Serialization;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000C8 RID: 200
	[Serializable]
	public class XEventException : Exception
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0001C1E4 File Offset: 0x0001C1E4
		public XEventException()
		{
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0001C1F8 File Offset: 0x0001C1F8
		public XEventException(string message)
			: base(message)
		{
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0001C20C File Offset: 0x0001C20C
		public XEventException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0001C224 File Offset: 0x0001C224
		protected XEventException(SerializationInfo info, StreamingContext context)
		{
		}
	}
}

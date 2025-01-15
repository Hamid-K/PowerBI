using System;
using System.Runtime.Serialization;

namespace Microsoft.Spatial
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	public class ParseErrorException : Exception
	{
		// Token: 0x06000117 RID: 279 RVA: 0x000038C0 File Offset: 0x00001AC0
		public ParseErrorException()
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000038C8 File Offset: 0x00001AC8
		public ParseErrorException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000038D2 File Offset: 0x00001AD2
		public ParseErrorException(string message)
			: base(message)
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000038DB File Offset: 0x00001ADB
		protected ParseErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

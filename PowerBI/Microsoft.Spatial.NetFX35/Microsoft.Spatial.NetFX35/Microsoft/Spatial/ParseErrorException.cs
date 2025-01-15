using System;
using System.Runtime.Serialization;

namespace Microsoft.Spatial
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	public class ParseErrorException : Exception
	{
		// Token: 0x0600015E RID: 350 RVA: 0x000043A0 File Offset: 0x000025A0
		public ParseErrorException()
		{
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000043A8 File Offset: 0x000025A8
		public ParseErrorException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000043B2 File Offset: 0x000025B2
		public ParseErrorException(string message)
			: base(message)
		{
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000043BB File Offset: 0x000025BB
		protected ParseErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000032 RID: 50
	public class ParseErrorException : Exception
	{
		// Token: 0x06000183 RID: 387 RVA: 0x000043D0 File Offset: 0x000025D0
		public ParseErrorException()
		{
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000043D8 File Offset: 0x000025D8
		public ParseErrorException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000043E2 File Offset: 0x000025E2
		public ParseErrorException(string message)
			: base(message)
		{
		}
	}
}

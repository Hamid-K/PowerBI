using System;
using System.Runtime.Serialization;

namespace antlr
{
	// Token: 0x0200002A RID: 42
	[Serializable]
	internal class TokenStreamRetryException : TokenStreamException
	{
		// Token: 0x0600015B RID: 347 RVA: 0x0000534D File Offset: 0x0000354D
		public TokenStreamRetryException()
		{
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005355 File Offset: 0x00003555
		protected TokenStreamRetryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

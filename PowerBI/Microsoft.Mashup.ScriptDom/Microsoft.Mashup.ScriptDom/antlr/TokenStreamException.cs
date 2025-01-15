using System;
using System.Runtime.Serialization;

namespace antlr
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	internal class TokenStreamException : ANTLRException
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00005106 File Offset: 0x00003306
		public TokenStreamException()
		{
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000510E File Offset: 0x0000330E
		public TokenStreamException(string s)
			: base(s)
		{
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005117 File Offset: 0x00003317
		protected TokenStreamException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

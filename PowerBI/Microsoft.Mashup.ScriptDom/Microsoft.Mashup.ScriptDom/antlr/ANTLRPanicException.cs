using System;
using System.Runtime.Serialization;

namespace antlr
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	internal class ANTLRPanicException : ANTLRException
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000029D8 File Offset: 0x00000BD8
		public ANTLRPanicException()
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029E0 File Offset: 0x00000BE0
		public ANTLRPanicException(string s)
			: base(s)
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029E9 File Offset: 0x00000BE9
		public ANTLRPanicException(string s, Exception inner)
			: base(s, inner)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000029F3 File Offset: 0x00000BF3
		protected ANTLRPanicException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

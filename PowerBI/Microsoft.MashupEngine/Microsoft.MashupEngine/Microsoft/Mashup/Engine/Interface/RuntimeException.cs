using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000111 RID: 273
	[Serializable]
	public abstract class RuntimeException : Exception
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x00005F33 File Offset: 0x00004133
		protected RuntimeException()
		{
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00002FDF File Offset: 0x000011DF
		protected RuntimeException(string message)
			: base(message)
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00005F3B File Offset: 0x0000413B
		protected RuntimeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00005F45 File Offset: 0x00004145
		protected RuntimeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

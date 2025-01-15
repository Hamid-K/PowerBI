using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	public sealed class DeadlockException : RuntimeException
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002BB1 File Offset: 0x00000DB1
		public DeadlockException()
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002BB9 File Offset: 0x00000DB9
		public DeadlockException(string message)
			: base(message)
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002BC2 File Offset: 0x00000DC2
		public DeadlockException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

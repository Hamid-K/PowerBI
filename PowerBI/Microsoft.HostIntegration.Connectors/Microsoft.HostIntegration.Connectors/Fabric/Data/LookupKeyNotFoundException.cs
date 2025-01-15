using System;
using System.Runtime.Serialization;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003BB RID: 955
	[Serializable]
	internal class LookupKeyNotFoundException : LookupException
	{
		// Token: 0x060021AE RID: 8622 RVA: 0x00067F6B File Offset: 0x0006616B
		public LookupKeyNotFoundException()
			: base("Key not found")
		{
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x00067F78 File Offset: 0x00066178
		public LookupKeyNotFoundException(string message)
			: base(message)
		{
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x00067F81 File Offset: 0x00066181
		public LookupKeyNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x00067F8B File Offset: 0x0006618B
		protected LookupKeyNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

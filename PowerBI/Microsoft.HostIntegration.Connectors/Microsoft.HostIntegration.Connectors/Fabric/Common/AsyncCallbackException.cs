using System;
using System.Runtime.Serialization;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003F1 RID: 1009
	[Serializable]
	internal class AsyncCallbackException : Exception
	{
		// Token: 0x06002378 RID: 9080 RVA: 0x0001E12D File Offset: 0x0001C32D
		public AsyncCallbackException()
		{
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x0001E135 File Offset: 0x0001C335
		public AsyncCallbackException(string message)
			: base(message)
		{
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x0001E13E File Offset: 0x0001C33E
		public AsyncCallbackException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x0001E148 File Offset: 0x0001C348
		protected AsyncCallbackException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

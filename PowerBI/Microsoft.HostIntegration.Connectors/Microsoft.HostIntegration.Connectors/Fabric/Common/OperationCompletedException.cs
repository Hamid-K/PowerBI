using System;
using System.Runtime.Serialization;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003F3 RID: 1011
	[Serializable]
	internal class OperationCompletedException : Exception
	{
		// Token: 0x06002380 RID: 9088 RVA: 0x0001E12D File Offset: 0x0001C32D
		public OperationCompletedException()
		{
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x0001E135 File Offset: 0x0001C335
		public OperationCompletedException(string message)
			: base(message)
		{
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x0001E13E File Offset: 0x0001C33E
		public OperationCompletedException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x0001E148 File Offset: 0x0001C348
		protected OperationCompletedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

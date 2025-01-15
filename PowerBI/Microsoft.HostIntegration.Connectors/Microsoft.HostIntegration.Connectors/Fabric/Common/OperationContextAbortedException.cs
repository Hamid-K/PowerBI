using System;
using System.Runtime.Serialization;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003F2 RID: 1010
	[Serializable]
	internal class OperationContextAbortedException : Exception
	{
		// Token: 0x0600237C RID: 9084 RVA: 0x0006CF8A File Offset: 0x0006B18A
		public OperationContextAbortedException()
			: base("Operation was aborted")
		{
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x0001E135 File Offset: 0x0001C335
		public OperationContextAbortedException(string message)
			: base(message)
		{
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x0001E13E File Offset: 0x0001C33E
		public OperationContextAbortedException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x0001E148 File Offset: 0x0001C348
		protected OperationContextAbortedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

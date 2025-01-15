using System;
using System.Runtime.Serialization;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003B5 RID: 949
	[Serializable]
	internal class FabricDataException : Exception
	{
		// Token: 0x0600218E RID: 8590 RVA: 0x0001E12D File Offset: 0x0001C32D
		public FabricDataException()
		{
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x0001E135 File Offset: 0x0001C335
		public FabricDataException(string message)
			: base(message)
		{
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x0001E13E File Offset: 0x0001C33E
		public FabricDataException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x0001E148 File Offset: 0x0001C348
		protected FabricDataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

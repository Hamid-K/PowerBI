using System;
using System.Runtime.Serialization;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003D7 RID: 983
	[Serializable]
	internal class FailPointException : Exception
	{
		// Token: 0x0600228C RID: 8844 RVA: 0x0001E12D File Offset: 0x0001C32D
		public FailPointException()
		{
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x0001E135 File Offset: 0x0001C335
		public FailPointException(string message)
			: base(message)
		{
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x0001E13E File Offset: 0x0001C33E
		public FailPointException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x0001E148 File Offset: 0x0001C348
		protected FailPointException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

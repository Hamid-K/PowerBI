using System;
using System.Runtime.Serialization;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000415 RID: 1045
	[Serializable]
	internal class ReleaseAssertException : Exception
	{
		// Token: 0x0600244E RID: 9294 RVA: 0x0006F63E File Offset: 0x0006D83E
		public ReleaseAssertException()
			: base("Assertion failed")
		{
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x0001E135 File Offset: 0x0001C335
		public ReleaseAssertException(string message)
			: base(message)
		{
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x0001E13E File Offset: 0x0001C33E
		public ReleaseAssertException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x0001E148 File Offset: 0x0001C348
		protected ReleaseAssertException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

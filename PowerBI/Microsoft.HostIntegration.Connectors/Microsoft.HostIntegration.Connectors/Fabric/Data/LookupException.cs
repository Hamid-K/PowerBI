using System;
using System.Runtime.Serialization;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003BA RID: 954
	[Serializable]
	internal class LookupException : FabricDataException
	{
		// Token: 0x060021AA RID: 8618 RVA: 0x00067F46 File Offset: 0x00066146
		public LookupException()
		{
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x00067F4E File Offset: 0x0006614E
		public LookupException(string message)
			: base(message)
		{
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x00067F57 File Offset: 0x00066157
		public LookupException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x00067F61 File Offset: 0x00066161
		protected LookupException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

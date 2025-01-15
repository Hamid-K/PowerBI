using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000B0 RID: 176
	[Serializable]
	public class MissingModuleException : Exception
	{
		// Token: 0x0600038E RID: 910 RVA: 0x00006CB3 File Offset: 0x00004EB3
		public MissingModuleException()
		{
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00006CBB File Offset: 0x00004EBB
		public MissingModuleException(string message)
			: base(message)
		{
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00006CC4 File Offset: 0x00004EC4
		public MissingModuleException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00006CCE File Offset: 0x00004ECE
		protected MissingModuleException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

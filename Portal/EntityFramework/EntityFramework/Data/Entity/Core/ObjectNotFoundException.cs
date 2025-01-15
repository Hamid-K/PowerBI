using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020002DC RID: 732
	[Serializable]
	public sealed class ObjectNotFoundException : DataException
	{
		// Token: 0x06002331 RID: 9009 RVA: 0x00063679 File Offset: 0x00061879
		public ObjectNotFoundException()
		{
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x00063681 File Offset: 0x00061881
		public ObjectNotFoundException(string message)
			: base(message)
		{
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x0006368A File Offset: 0x0006188A
		public ObjectNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x00063694 File Offset: 0x00061894
		private ObjectNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

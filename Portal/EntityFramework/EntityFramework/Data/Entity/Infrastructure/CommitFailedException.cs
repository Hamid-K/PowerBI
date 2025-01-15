using System;
using System.Data.Entity.Resources;
using System.Runtime.Serialization;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000261 RID: 609
	[Serializable]
	public class CommitFailedException : DataException
	{
		// Token: 0x06001EF0 RID: 7920 RVA: 0x00055EA7 File Offset: 0x000540A7
		public CommitFailedException()
			: base(Strings.CommitFailed)
		{
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00055EB4 File Offset: 0x000540B4
		public CommitFailedException(string message)
			: base(message)
		{
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x00055EBD File Offset: 0x000540BD
		public CommitFailedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x00055EC7 File Offset: 0x000540C7
		protected CommitFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

using System;
using System.Data.Entity.Core;
using System.Runtime.Serialization;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200025C RID: 604
	[Serializable]
	public sealed class RetryLimitExceededException : EntityException
	{
		// Token: 0x06001EDC RID: 7900 RVA: 0x00055B88 File Offset: 0x00053D88
		public RetryLimitExceededException()
		{
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x00055B90 File Offset: 0x00053D90
		public RetryLimitExceededException(string message)
			: base(message)
		{
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x00055B99 File Offset: 0x00053D99
		public RetryLimitExceededException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x00055BA3 File Offset: 0x00053DA3
		private RetryLimitExceededException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002071 RID: 8305
	[Serializable]
	public class CredentialValidationException : Exception
	{
		// Token: 0x0600CB44 RID: 52036 RVA: 0x00002FDF File Offset: 0x000011DF
		public CredentialValidationException(string message)
			: base(message)
		{
		}

		// Token: 0x0600CB45 RID: 52037 RVA: 0x00005F3B File Offset: 0x0000413B
		public CredentialValidationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600CB46 RID: 52038 RVA: 0x00005F45 File Offset: 0x00004145
		protected CredentialValidationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

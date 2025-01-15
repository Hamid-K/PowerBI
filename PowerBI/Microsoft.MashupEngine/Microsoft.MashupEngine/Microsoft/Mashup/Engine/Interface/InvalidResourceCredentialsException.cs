using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000109 RID: 265
	[Serializable]
	public class InvalidResourceCredentialsException : ResourceSecurityException
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x00005CDF File Offset: 0x00003EDF
		public InvalidResourceCredentialsException(IResource origin, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
			: base(origin, resource, message, userMessage, innerException)
		{
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00005CEE File Offset: 0x00003EEE
		public InvalidResourceCredentialsException(IResource resource, string message = null, string userMessage = null, Exception innerException = null)
			: this(null, resource, message, userMessage, innerException)
		{
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00005CFC File Offset: 0x00003EFC
		protected InvalidResourceCredentialsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000291 RID: 657
		public static readonly string ReasonString = "CredentialInvalid";
	}
}

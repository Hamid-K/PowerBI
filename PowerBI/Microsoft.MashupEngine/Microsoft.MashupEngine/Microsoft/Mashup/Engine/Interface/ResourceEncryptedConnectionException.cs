using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200010C RID: 268
	[Serializable]
	public class ResourceEncryptedConnectionException : ResourceSecurityException
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x00005CDF File Offset: 0x00003EDF
		public ResourceEncryptedConnectionException(IResource origin, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
			: base(origin, resource, message, userMessage, innerException)
		{
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00005D46 File Offset: 0x00003F46
		public ResourceEncryptedConnectionException(IResource resource, string message = null, string userMessage = null, Exception innerException = null)
			: this(null, resource, message, userMessage, innerException)
		{
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00005CFC File Offset: 0x00003EFC
		protected ResourceEncryptedConnectionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000294 RID: 660
		public static readonly string ReasonString = "EncryptedConnectionFailed";
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200010A RID: 266
	[Serializable]
	public class ResourceAccessAuthorizationException : ResourceSecurityException
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x00005CDF File Offset: 0x00003EDF
		public ResourceAccessAuthorizationException(IResource origin, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
			: base(origin, resource, message, userMessage, innerException)
		{
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00005D12 File Offset: 0x00003F12
		public ResourceAccessAuthorizationException(IResource resource, string message = null, string userMessage = null, Exception innerException = null)
			: this(null, resource, message, userMessage, innerException)
		{
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00005CFC File Offset: 0x00003EFC
		protected ResourceAccessAuthorizationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000292 RID: 658
		public static readonly string ReasonString = "AccessUnauthorized";
	}
}

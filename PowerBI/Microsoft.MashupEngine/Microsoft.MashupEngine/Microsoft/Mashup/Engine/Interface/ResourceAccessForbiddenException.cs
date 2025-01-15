using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200010B RID: 267
	[Serializable]
	public class ResourceAccessForbiddenException : ResourceSecurityException
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x00005CDF File Offset: 0x00003EDF
		public ResourceAccessForbiddenException(IResource origin, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
			: base(origin, resource, message, userMessage, innerException)
		{
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00005D2C File Offset: 0x00003F2C
		public ResourceAccessForbiddenException(IResource resource, string message = null, string userMessage = null, Exception innerException = null)
			: this(null, resource, message, userMessage, innerException)
		{
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00005CFC File Offset: 0x00003EFC
		protected ResourceAccessForbiddenException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000293 RID: 659
		public static readonly string ReasonString = "AccessForbidden";
	}
}

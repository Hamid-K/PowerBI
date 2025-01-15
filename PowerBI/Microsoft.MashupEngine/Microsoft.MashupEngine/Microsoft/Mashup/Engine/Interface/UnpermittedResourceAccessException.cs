using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200010E RID: 270
	[Serializable]
	public class UnpermittedResourceAccessException : ResourceSecurityException
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x00005CDF File Offset: 0x00003EDF
		public UnpermittedResourceAccessException(IResource origin, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
			: base(origin, resource, message, userMessage, innerException)
		{
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00005D7A File Offset: 0x00003F7A
		public UnpermittedResourceAccessException(string dataSourceLocationOrigin, IResource origin, string dataSourceLocation, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
			: base(dataSourceLocationOrigin, origin, dataSourceLocation, resource, message, userMessage, innerException)
		{
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00005D8D File Offset: 0x00003F8D
		public UnpermittedResourceAccessException(IResource resource, string message = null, string userMessage = null, Exception innerException = null)
			: this(null, resource, message, userMessage, innerException)
		{
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00005CFC File Offset: 0x00003EFC
		protected UnpermittedResourceAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000296 RID: 662
		public static readonly string ReasonString = "CredentialMissing";
	}
}

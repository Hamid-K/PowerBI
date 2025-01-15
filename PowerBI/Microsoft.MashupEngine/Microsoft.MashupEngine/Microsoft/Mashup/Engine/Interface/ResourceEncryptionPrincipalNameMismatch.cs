using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200010D RID: 269
	[Serializable]
	public class ResourceEncryptionPrincipalNameMismatch : ResourceSecurityException
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x00005CDF File Offset: 0x00003EDF
		public ResourceEncryptionPrincipalNameMismatch(IResource origin, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
			: base(origin, resource, message, userMessage, innerException)
		{
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00005D60 File Offset: 0x00003F60
		public ResourceEncryptionPrincipalNameMismatch(IResource resource, string message = null, string userMessage = null, Exception innerException = null)
			: this(null, resource, message, userMessage, innerException)
		{
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00005CFC File Offset: 0x00003EFC
		protected ResourceEncryptionPrincipalNameMismatch(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000295 RID: 661
		public static readonly string ReasonString = "PrincipleNameMismatch";
	}
}

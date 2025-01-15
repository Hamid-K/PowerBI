using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Protocols.OpenIdConnect
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class OpenIdConnectProtocolInvalidStateException : OpenIdConnectProtocolException
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00002867 File Offset: 0x00000A67
		public OpenIdConnectProtocolInvalidStateException()
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000286F File Offset: 0x00000A6F
		public OpenIdConnectProtocolInvalidStateException(string message)
			: base(message)
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002878 File Offset: 0x00000A78
		public OpenIdConnectProtocolInvalidStateException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002882 File Offset: 0x00000A82
		protected OpenIdConnectProtocolInvalidStateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

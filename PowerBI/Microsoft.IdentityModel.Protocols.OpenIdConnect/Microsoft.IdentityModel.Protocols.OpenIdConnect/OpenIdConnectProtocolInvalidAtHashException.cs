using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Protocols.OpenIdConnect
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public class OpenIdConnectProtocolInvalidAtHashException : OpenIdConnectProtocolException
	{
		// Token: 0x0600006D RID: 109 RVA: 0x000027F8 File Offset: 0x000009F8
		public OpenIdConnectProtocolInvalidAtHashException()
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002800 File Offset: 0x00000A00
		public OpenIdConnectProtocolInvalidAtHashException(string message)
			: base(message)
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002809 File Offset: 0x00000A09
		public OpenIdConnectProtocolInvalidAtHashException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002813 File Offset: 0x00000A13
		protected OpenIdConnectProtocolInvalidAtHashException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

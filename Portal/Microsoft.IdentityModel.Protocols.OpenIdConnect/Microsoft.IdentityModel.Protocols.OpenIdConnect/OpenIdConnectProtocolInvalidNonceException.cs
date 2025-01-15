using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Protocols.OpenIdConnect
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public class OpenIdConnectProtocolInvalidNonceException : OpenIdConnectProtocolException
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00002842 File Offset: 0x00000A42
		public OpenIdConnectProtocolInvalidNonceException()
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000284A File Offset: 0x00000A4A
		public OpenIdConnectProtocolInvalidNonceException(string message)
			: base(message)
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002853 File Offset: 0x00000A53
		public OpenIdConnectProtocolInvalidNonceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000285D File Offset: 0x00000A5D
		protected OpenIdConnectProtocolInvalidNonceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

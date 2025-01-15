using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Protocols.OpenIdConnect
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class OpenIdConnectProtocolInvalidCHashException : OpenIdConnectProtocolException
	{
		// Token: 0x06000071 RID: 113 RVA: 0x0000281D File Offset: 0x00000A1D
		public OpenIdConnectProtocolInvalidCHashException()
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002825 File Offset: 0x00000A25
		public OpenIdConnectProtocolInvalidCHashException(string message)
			: base(message)
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000282E File Offset: 0x00000A2E
		public OpenIdConnectProtocolInvalidCHashException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002838 File Offset: 0x00000A38
		protected OpenIdConnectProtocolInvalidCHashException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

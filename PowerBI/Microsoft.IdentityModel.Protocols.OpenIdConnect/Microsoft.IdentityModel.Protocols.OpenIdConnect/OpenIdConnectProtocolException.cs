using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Protocols.OpenIdConnect
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class OpenIdConnectProtocolException : Exception
	{
		// Token: 0x06000069 RID: 105 RVA: 0x000027D3 File Offset: 0x000009D3
		public OpenIdConnectProtocolException()
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000027DB File Offset: 0x000009DB
		public OpenIdConnectProtocolException(string message)
			: base(message)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000027E4 File Offset: 0x000009E4
		public OpenIdConnectProtocolException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000027EE File Offset: 0x000009EE
		protected OpenIdConnectProtocolException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000148 RID: 328
	[Serializable]
	public class SecurityTokenInvalidSigningKeyException : SecurityTokenValidationException
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0003DDAC File Offset: 0x0003BFAC
		// (set) Token: 0x06000FAA RID: 4010 RVA: 0x0003DDB4 File Offset: 0x0003BFB4
		public SecurityKey SigningKey { get; set; }

		// Token: 0x06000FAB RID: 4011 RVA: 0x0003DDBD File Offset: 0x0003BFBD
		public SecurityTokenInvalidSigningKeyException()
			: base("SecurityToken has invalid issuer signing key.")
		{
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0003DDCA File Offset: 0x0003BFCA
		public SecurityTokenInvalidSigningKeyException(string message)
			: base(message)
		{
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0003DDD3 File Offset: 0x0003BFD3
		public SecurityTokenInvalidSigningKeyException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0003DDDD File Offset: 0x0003BFDD
		protected SecurityTokenInvalidSigningKeyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

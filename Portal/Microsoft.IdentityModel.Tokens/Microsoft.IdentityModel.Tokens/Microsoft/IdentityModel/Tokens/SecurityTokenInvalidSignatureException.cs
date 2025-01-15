using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000147 RID: 327
	[Serializable]
	public class SecurityTokenInvalidSignatureException : SecurityTokenValidationException
	{
		// Token: 0x06000FA5 RID: 4005 RVA: 0x0003DD87 File Offset: 0x0003BF87
		public SecurityTokenInvalidSignatureException()
		{
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0003DD8F File Offset: 0x0003BF8F
		public SecurityTokenInvalidSignatureException(string message)
			: base(message)
		{
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003DD98 File Offset: 0x0003BF98
		public SecurityTokenInvalidSignatureException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0003DDA2 File Offset: 0x0003BFA2
		protected SecurityTokenInvalidSignatureException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

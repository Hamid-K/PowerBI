using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000150 RID: 336
	[Serializable]
	public class SecurityTokenSignatureKeyNotFoundException : SecurityTokenInvalidSignatureException
	{
		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003DFEA File Offset: 0x0003C1EA
		public SecurityTokenSignatureKeyNotFoundException()
		{
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0003DFF2 File Offset: 0x0003C1F2
		public SecurityTokenSignatureKeyNotFoundException(string message)
			: base(message)
		{
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0003DFFB File Offset: 0x0003C1FB
		public SecurityTokenSignatureKeyNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0003E005 File Offset: 0x0003C205
		protected SecurityTokenSignatureKeyNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

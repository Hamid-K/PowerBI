using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000152 RID: 338
	[Serializable]
	public class SecurityTokenValidationException : SecurityTokenException
	{
		// Token: 0x06000FDD RID: 4061 RVA: 0x0003E0C4 File Offset: 0x0003C2C4
		public SecurityTokenValidationException()
		{
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0003E0CC File Offset: 0x0003C2CC
		public SecurityTokenValidationException(string message)
			: base(message)
		{
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0003E0D5 File Offset: 0x0003C2D5
		public SecurityTokenValidationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0003E0DF File Offset: 0x0003C2DF
		protected SecurityTokenValidationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

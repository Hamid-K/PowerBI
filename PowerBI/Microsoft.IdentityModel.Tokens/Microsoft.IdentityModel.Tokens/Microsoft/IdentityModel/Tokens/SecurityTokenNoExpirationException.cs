using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200014C RID: 332
	[Serializable]
	public class SecurityTokenNoExpirationException : SecurityTokenValidationException
	{
		// Token: 0x06000FBE RID: 4030 RVA: 0x0003DECF File Offset: 0x0003C0CF
		public SecurityTokenNoExpirationException()
		{
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0003DED7 File Offset: 0x0003C0D7
		public SecurityTokenNoExpirationException(string message)
			: base(message)
		{
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0003DEE0 File Offset: 0x0003C0E0
		public SecurityTokenNoExpirationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0003DEEA File Offset: 0x0003C0EA
		protected SecurityTokenNoExpirationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

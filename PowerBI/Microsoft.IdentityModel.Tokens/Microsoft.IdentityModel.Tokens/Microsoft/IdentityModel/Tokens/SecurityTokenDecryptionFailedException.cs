using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200013E RID: 318
	[Serializable]
	public class SecurityTokenDecryptionFailedException : SecurityTokenException
	{
		// Token: 0x06000F70 RID: 3952 RVA: 0x0003D931 File Offset: 0x0003BB31
		public SecurityTokenDecryptionFailedException()
		{
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0003D939 File Offset: 0x0003BB39
		public SecurityTokenDecryptionFailedException(string message)
			: base(message)
		{
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0003D942 File Offset: 0x0003BB42
		public SecurityTokenDecryptionFailedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0003D94C File Offset: 0x0003BB4C
		protected SecurityTokenDecryptionFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

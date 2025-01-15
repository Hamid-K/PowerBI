using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200013F RID: 319
	[Serializable]
	public class SecurityTokenEncryptionFailedException : SecurityTokenException
	{
		// Token: 0x06000F74 RID: 3956 RVA: 0x0003D956 File Offset: 0x0003BB56
		public SecurityTokenEncryptionFailedException()
		{
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0003D95E File Offset: 0x0003BB5E
		public SecurityTokenEncryptionFailedException(string message)
			: base(message)
		{
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0003D967 File Offset: 0x0003BB67
		public SecurityTokenEncryptionFailedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0003D971 File Offset: 0x0003BB71
		protected SecurityTokenEncryptionFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

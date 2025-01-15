using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000140 RID: 320
	[Serializable]
	public class SecurityTokenEncryptionKeyNotFoundException : SecurityTokenDecryptionFailedException
	{
		// Token: 0x06000F78 RID: 3960 RVA: 0x0003D97B File Offset: 0x0003BB7B
		public SecurityTokenEncryptionKeyNotFoundException()
		{
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0003D983 File Offset: 0x0003BB83
		public SecurityTokenEncryptionKeyNotFoundException(string message)
			: base(message)
		{
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0003D98C File Offset: 0x0003BB8C
		public SecurityTokenEncryptionKeyNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0003D996 File Offset: 0x0003BB96
		protected SecurityTokenEncryptionKeyNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

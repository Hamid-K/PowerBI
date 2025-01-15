using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200013D RID: 317
	[Serializable]
	public class SecurityTokenDecompressionFailedException : SecurityTokenException
	{
		// Token: 0x06000F6C RID: 3948 RVA: 0x0003D907 File Offset: 0x0003BB07
		public SecurityTokenDecompressionFailedException()
			: base("SecurityToken decompression failed.")
		{
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0003D914 File Offset: 0x0003BB14
		public SecurityTokenDecompressionFailedException(string message)
			: base(message)
		{
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0003D91D File Offset: 0x0003BB1D
		public SecurityTokenDecompressionFailedException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0003D927 File Offset: 0x0003BB27
		protected SecurityTokenDecompressionFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

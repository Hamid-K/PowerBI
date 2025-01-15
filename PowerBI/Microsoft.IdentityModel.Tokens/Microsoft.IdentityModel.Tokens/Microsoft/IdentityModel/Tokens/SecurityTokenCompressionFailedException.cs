using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200013C RID: 316
	[Serializable]
	public class SecurityTokenCompressionFailedException : SecurityTokenException
	{
		// Token: 0x06000F68 RID: 3944 RVA: 0x0003D8DD File Offset: 0x0003BADD
		public SecurityTokenCompressionFailedException()
			: base("SecurityToken compression failed.")
		{
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0003D8EA File Offset: 0x0003BAEA
		public SecurityTokenCompressionFailedException(string message)
			: base(message)
		{
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0003D8F3 File Offset: 0x0003BAF3
		public SecurityTokenCompressionFailedException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0003D8FD File Offset: 0x0003BAFD
		protected SecurityTokenCompressionFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

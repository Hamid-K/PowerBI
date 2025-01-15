using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200014A RID: 330
	[Serializable]
	public class SecurityTokenKeyWrapException : SecurityTokenException
	{
		// Token: 0x06000FB6 RID: 4022 RVA: 0x0003DE85 File Offset: 0x0003C085
		public SecurityTokenKeyWrapException()
		{
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003DE8D File Offset: 0x0003C08D
		public SecurityTokenKeyWrapException(string message)
			: base(message)
		{
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0003DE96 File Offset: 0x0003C096
		public SecurityTokenKeyWrapException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0003DEA0 File Offset: 0x0003C0A0
		protected SecurityTokenKeyWrapException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

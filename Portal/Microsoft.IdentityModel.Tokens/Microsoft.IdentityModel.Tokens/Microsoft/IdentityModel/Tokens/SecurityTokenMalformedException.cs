using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200014B RID: 331
	[Serializable]
	public class SecurityTokenMalformedException : SecurityTokenArgumentException
	{
		// Token: 0x06000FBA RID: 4026 RVA: 0x0003DEAA File Offset: 0x0003C0AA
		public SecurityTokenMalformedException()
		{
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0003DEB2 File Offset: 0x0003C0B2
		public SecurityTokenMalformedException(string message)
			: base(message)
		{
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0003DEBB File Offset: 0x0003C0BB
		public SecurityTokenMalformedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0003DEC5 File Offset: 0x0003C0C5
		protected SecurityTokenMalformedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

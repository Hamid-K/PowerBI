using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000141 RID: 321
	[Serializable]
	public class SecurityTokenException : Exception
	{
		// Token: 0x06000F7C RID: 3964 RVA: 0x0003D9A0 File Offset: 0x0003BBA0
		public SecurityTokenException()
		{
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0003D9A8 File Offset: 0x0003BBA8
		public SecurityTokenException(string message)
			: base(message)
		{
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0003D9B1 File Offset: 0x0003BBB1
		public SecurityTokenException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0003D9BB File Offset: 0x0003BBBB
		protected SecurityTokenException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

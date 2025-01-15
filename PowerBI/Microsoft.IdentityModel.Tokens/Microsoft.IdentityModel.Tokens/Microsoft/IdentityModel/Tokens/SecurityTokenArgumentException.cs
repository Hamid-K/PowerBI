using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200013B RID: 315
	[Serializable]
	public class SecurityTokenArgumentException : ArgumentException
	{
		// Token: 0x06000F64 RID: 3940 RVA: 0x0003D8B8 File Offset: 0x0003BAB8
		public SecurityTokenArgumentException()
		{
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0003D8C0 File Offset: 0x0003BAC0
		public SecurityTokenArgumentException(string message)
			: base(message)
		{
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0003D8C9 File Offset: 0x0003BAC9
		public SecurityTokenArgumentException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0003D8D3 File Offset: 0x0003BAD3
		protected SecurityTokenArgumentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

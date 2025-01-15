using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200014F RID: 335
	[Serializable]
	public class SecurityTokenReplayDetectedException : SecurityTokenValidationException
	{
		// Token: 0x06000FCD RID: 4045 RVA: 0x0003DFC0 File Offset: 0x0003C1C0
		public SecurityTokenReplayDetectedException()
			: base("SecurityToken replay detected")
		{
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0003DFCD File Offset: 0x0003C1CD
		public SecurityTokenReplayDetectedException(string message)
			: base(message)
		{
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0003DFD6 File Offset: 0x0003C1D6
		public SecurityTokenReplayDetectedException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003DFE0 File Offset: 0x0003C1E0
		protected SecurityTokenReplayDetectedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

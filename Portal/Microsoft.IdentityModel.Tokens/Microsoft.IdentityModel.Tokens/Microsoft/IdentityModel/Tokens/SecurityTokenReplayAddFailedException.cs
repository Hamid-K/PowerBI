using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200014E RID: 334
	[Serializable]
	public class SecurityTokenReplayAddFailedException : SecurityTokenValidationException
	{
		// Token: 0x06000FC9 RID: 4041 RVA: 0x0003DF9B File Offset: 0x0003C19B
		public SecurityTokenReplayAddFailedException()
		{
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0003DFA3 File Offset: 0x0003C1A3
		public SecurityTokenReplayAddFailedException(string message)
			: base(message)
		{
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0003DFAC File Offset: 0x0003C1AC
		public SecurityTokenReplayAddFailedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0003DFB6 File Offset: 0x0003C1B6
		protected SecurityTokenReplayAddFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

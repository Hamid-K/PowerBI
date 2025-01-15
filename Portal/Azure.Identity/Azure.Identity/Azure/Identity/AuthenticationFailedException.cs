using System;
using System.Runtime.Serialization;

namespace Azure.Identity
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class AuthenticationFailedException : Exception
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00003FD9 File Offset: 0x000021D9
		public AuthenticationFailedException(string message)
			: this(message, null)
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003FE3 File Offset: 0x000021E3
		public AuthenticationFailedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003FED File Offset: 0x000021ED
		protected AuthenticationFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

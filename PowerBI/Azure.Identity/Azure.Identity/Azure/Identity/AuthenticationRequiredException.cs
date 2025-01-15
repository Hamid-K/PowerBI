using System;
using System.Runtime.Serialization;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public class AuthenticationRequiredException : CredentialUnavailableException
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00004355 File Offset: 0x00002555
		public AuthenticationRequiredException(string message, TokenRequestContext context)
			: this(message, context, null)
		{
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004360 File Offset: 0x00002560
		public AuthenticationRequiredException(string message, TokenRequestContext context, Exception innerException)
			: base(message, innerException)
		{
			this.TokenRequestContext = context;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004371 File Offset: 0x00002571
		protected AuthenticationRequiredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000437B File Offset: 0x0000257B
		public TokenRequestContext TokenRequestContext { get; }
	}
}

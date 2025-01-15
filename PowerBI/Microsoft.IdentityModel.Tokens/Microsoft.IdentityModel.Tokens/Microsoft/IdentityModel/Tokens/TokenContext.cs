using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200017B RID: 379
	[Obsolete("The 'TokenContext' property is obsolete. Please use 'CallContext' instead.")]
	public class TokenContext : CallContext
	{
		// Token: 0x06001125 RID: 4389 RVA: 0x0004231A File Offset: 0x0004051A
		public TokenContext()
		{
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00042322 File Offset: 0x00040522
		public TokenContext(Guid activityId)
			: base(activityId)
		{
		}
	}
}

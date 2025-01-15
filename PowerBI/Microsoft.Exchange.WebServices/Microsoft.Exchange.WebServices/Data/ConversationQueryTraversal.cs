using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001F7 RID: 503
	public enum ConversationQueryTraversal
	{
		// Token: 0x04000D8F RID: 3471
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		Shallow,
		// Token: 0x04000D90 RID: 3472
		[RequiredServerVersion(ExchangeVersion.Exchange2013)]
		Deep
	}
}

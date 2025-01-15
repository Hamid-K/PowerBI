using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.Internal.Requests;

namespace Microsoft.Identity.Client.OAuth2.Throttling
{
	// Token: 0x02000214 RID: 532
	internal interface IThrottlingProvider
	{
		// Token: 0x06001627 RID: 5671
		void TryThrottle(AuthenticationRequestParameters requestParams, IReadOnlyDictionary<string, string> bodyParams);

		// Token: 0x06001628 RID: 5672
		void RecordException(AuthenticationRequestParameters requestParams, IReadOnlyDictionary<string, string> bodyParams, MsalServiceException ex);

		// Token: 0x06001629 RID: 5673
		void ResetCache();
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000151 RID: 337
	public interface ILongRunningWebApi
	{
		// Token: 0x060010C8 RID: 4296
		AcquireTokenOnBehalfOfParameterBuilder InitiateLongRunningProcessInWebApi(IEnumerable<string> scopes, string userToken, ref string longRunningProcessSessionKey);

		// Token: 0x060010C9 RID: 4297
		AcquireTokenOnBehalfOfParameterBuilder AcquireTokenInLongRunningProcess(IEnumerable<string> scopes, string longRunningProcessSessionKey);
	}
}

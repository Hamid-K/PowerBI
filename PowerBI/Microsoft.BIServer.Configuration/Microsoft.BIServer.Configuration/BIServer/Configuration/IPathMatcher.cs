using System;
using System.Collections.Generic;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000007 RID: 7
	public interface IPathMatcher
	{
		// Token: 0x0600000E RID: 14
		bool IsMatch(string url, List<KeyValuePair<string, string[]>> queryParams);
	}
}

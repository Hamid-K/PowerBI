using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.OData.Interfaces
{
	// Token: 0x0200005E RID: 94
	internal interface IWebApiHeaders
	{
		// Token: 0x06000295 RID: 661
		bool TryGetValues(string key, out IEnumerable<string> values);
	}
}

using System;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Interfaces
{
	// Token: 0x0200005F RID: 95
	internal interface IWebApiOptions
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000296 RID: 662
		ODataUrlKeyDelimiter UrlKeyDelimiter { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000297 RID: 663
		bool NullDynamicPropertyIsEnabled { get; }
	}
}

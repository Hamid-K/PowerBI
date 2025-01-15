using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x02000186 RID: 390
	public interface IETagHandler
	{
		// Token: 0x06000CE4 RID: 3300
		EntityTagHeaderValue CreateETag(IDictionary<string, object> properties);

		// Token: 0x06000CE5 RID: 3301
		IDictionary<string, object> ParseETag(EntityTagHeaderValue etagHeaderValue);
	}
}

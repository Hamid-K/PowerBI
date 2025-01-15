using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.AspNet.OData.Interfaces
{
	// Token: 0x0200005C RID: 92
	internal interface IWebApiContext
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000280 RID: 640
		// (set) Token: 0x06000281 RID: 641
		ApplyClause ApplyClause { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000282 RID: 642
		// (set) Token: 0x06000283 RID: 643
		Uri NextLink { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000284 RID: 644
		// (set) Token: 0x06000285 RID: 645
		Uri DeltaLink { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000286 RID: 646
		// (set) Token: 0x06000287 RID: 647
		int PageSize { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000288 RID: 648
		Microsoft.AspNet.OData.Routing.ODataPath Path { get; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000289 RID: 649
		string RouteName { get; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600028A RID: 650
		IDictionary<string, object> RoutingConventionsStore { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600028B RID: 651
		// (set) Token: 0x0600028C RID: 652
		SelectExpandClause ProcessedSelectExpandClause { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600028D RID: 653
		// (set) Token: 0x0600028E RID: 654
		ODataQueryOptions QueryOptions { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600028F RID: 655
		long? TotalCount { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000290 RID: 656
		// (set) Token: 0x06000291 RID: 657
		Func<long> TotalCountFunc { get; set; }
	}
}

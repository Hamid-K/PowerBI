using System;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x020000A1 RID: 161
	internal interface IODataFeedAndEntryTypeContext
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600060A RID: 1546
		string NavigationSourceName { get; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600060B RID: 1547
		string NavigationSourceEntityTypeName { get; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600060C RID: 1548
		string NavigationSourceFullTypeName { get; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600060D RID: 1549
		EdmNavigationSourceKind NavigationSourceKind { get; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600060E RID: 1550
		string ExpectedEntityTypeName { get; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600060F RID: 1551
		bool IsMediaLinkEntry { get; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000610 RID: 1552
		bool IsFromCollection { get; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000611 RID: 1553
		UrlConvention UrlConvention { get; }
	}
}

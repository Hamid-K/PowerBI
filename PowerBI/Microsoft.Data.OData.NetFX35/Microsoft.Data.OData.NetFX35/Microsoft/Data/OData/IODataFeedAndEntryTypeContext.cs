using System;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData
{
	// Token: 0x0200010B RID: 267
	internal interface IODataFeedAndEntryTypeContext
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600071A RID: 1818
		string EntitySetName { get; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600071B RID: 1819
		string EntitySetElementTypeName { get; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600071C RID: 1820
		string ExpectedEntityTypeName { get; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600071D RID: 1821
		bool IsMediaLinkEntry { get; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600071E RID: 1822
		UrlConvention UrlConvention { get; }
	}
}

using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x0200003B RID: 59
	internal interface IODataResourceTypeContext
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000214 RID: 532
		string NavigationSourceName { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000215 RID: 533
		string NavigationSourceEntityTypeName { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000216 RID: 534
		string NavigationSourceFullTypeName { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000217 RID: 535
		EdmNavigationSourceKind NavigationSourceKind { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000218 RID: 536
		string ExpectedResourceTypeName { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000219 RID: 537
		bool IsMediaLinkEntry { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600021A RID: 538
		bool IsFromCollection { get; }
	}
}

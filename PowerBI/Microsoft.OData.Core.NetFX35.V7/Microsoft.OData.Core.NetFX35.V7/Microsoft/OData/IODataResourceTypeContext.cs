using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000014 RID: 20
	internal interface IODataResourceTypeContext
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008D RID: 141
		string NavigationSourceName { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008E RID: 142
		string NavigationSourceEntityTypeName { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008F RID: 143
		string NavigationSourceFullTypeName { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000090 RID: 144
		EdmNavigationSourceKind NavigationSourceKind { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000091 RID: 145
		string ExpectedResourceTypeName { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000092 RID: 146
		bool IsMediaLinkEntry { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000093 RID: 147
		bool IsFromCollection { get; }
	}
}

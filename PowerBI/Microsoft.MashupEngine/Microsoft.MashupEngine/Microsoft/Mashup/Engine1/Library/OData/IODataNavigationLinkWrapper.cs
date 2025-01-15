using System;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000744 RID: 1860
	public interface IODataNavigationLinkWrapper
	{
		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06003720 RID: 14112
		Uri AssociationLinkUrl { get; }

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06003721 RID: 14113
		bool? IsCollection { get; }

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06003722 RID: 14114
		string Name { get; }

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06003723 RID: 14115
		Uri Url { get; }
	}
}

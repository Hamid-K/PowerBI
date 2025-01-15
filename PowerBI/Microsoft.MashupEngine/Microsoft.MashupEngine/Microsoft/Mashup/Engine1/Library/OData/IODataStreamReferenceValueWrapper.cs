using System;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000745 RID: 1861
	internal interface IODataStreamReferenceValueWrapper
	{
		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06003724 RID: 14116
		string ContentType { get; }

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06003725 RID: 14117
		Uri EditLink { get; }

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06003726 RID: 14118
		string ETag { get; }

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x06003727 RID: 14119
		Uri ReadLink { get; }
	}
}

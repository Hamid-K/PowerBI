using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020001E6 RID: 486
	public interface IODataUrlResolver
	{
		// Token: 0x06000E34 RID: 3636
		Uri ResolveUrl(Uri baseUri, Uri payloadUri);
	}
}

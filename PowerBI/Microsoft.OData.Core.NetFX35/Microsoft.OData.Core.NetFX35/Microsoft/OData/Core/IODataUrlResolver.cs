using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020000A5 RID: 165
	public interface IODataUrlResolver
	{
		// Token: 0x06000622 RID: 1570
		Uri ResolveUrl(Uri baseUri, Uri payloadUri);
	}
}

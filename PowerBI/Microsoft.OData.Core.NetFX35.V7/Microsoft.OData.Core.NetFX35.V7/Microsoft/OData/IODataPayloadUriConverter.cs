using System;

namespace Microsoft.OData
{
	// Token: 0x02000019 RID: 25
	public interface IODataPayloadUriConverter
	{
		// Token: 0x060000A5 RID: 165
		Uri ConvertPayloadUri(Uri baseUri, Uri payloadUri);
	}
}

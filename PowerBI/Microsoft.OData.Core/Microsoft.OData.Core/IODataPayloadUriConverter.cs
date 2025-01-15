using System;

namespace Microsoft.OData
{
	// Token: 0x02000042 RID: 66
	public interface IODataPayloadUriConverter
	{
		// Token: 0x0600022E RID: 558
		Uri ConvertPayloadUri(Uri baseUri, Uri payloadUri);
	}
}

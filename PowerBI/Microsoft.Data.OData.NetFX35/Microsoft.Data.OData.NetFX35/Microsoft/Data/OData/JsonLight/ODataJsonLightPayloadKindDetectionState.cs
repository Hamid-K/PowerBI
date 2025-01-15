using System;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000142 RID: 322
	internal sealed class ODataJsonLightPayloadKindDetectionState
	{
		// Token: 0x06000874 RID: 2164 RVA: 0x0001B6D0 File Offset: 0x000198D0
		internal ODataJsonLightPayloadKindDetectionState(ODataJsonLightMetadataUriParseResult metadataUriParseResult)
		{
			this.metadataUriParseResult = metadataUriParseResult;
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0001B6DF File Offset: 0x000198DF
		internal ODataJsonLightMetadataUriParseResult MetadataUriParseResult
		{
			get
			{
				return this.metadataUriParseResult;
			}
		}

		// Token: 0x0400034C RID: 844
		private readonly ODataJsonLightMetadataUriParseResult metadataUriParseResult;
	}
}

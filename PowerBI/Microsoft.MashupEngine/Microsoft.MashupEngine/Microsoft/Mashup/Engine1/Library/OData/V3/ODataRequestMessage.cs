using System;
using Microsoft.Data.OData;
using Microsoft.Mashup.Engine1.Library.Http;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008D0 RID: 2256
	internal sealed class ODataRequestMessage : ODataRequestMessageBase, IODataRequestMessage
	{
		// Token: 0x06004077 RID: 16503 RVA: 0x000B3F63 File Offset: 0x000B2163
		public ODataRequestMessage(MashupHttpWebRequest httpWebRequest)
			: base(httpWebRequest)
		{
		}
	}
}

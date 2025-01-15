using System;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7
{
	// Token: 0x02000775 RID: 1909
	internal sealed class ODataRequestMessage : ODataRequestMessageBase, IODataRequestMessage
	{
		// Token: 0x06003837 RID: 14391 RVA: 0x000B3F63 File Offset: 0x000B2163
		public ODataRequestMessage(MashupHttpWebRequest httpWebRequest)
			: base(httpWebRequest)
		{
		}
	}
}

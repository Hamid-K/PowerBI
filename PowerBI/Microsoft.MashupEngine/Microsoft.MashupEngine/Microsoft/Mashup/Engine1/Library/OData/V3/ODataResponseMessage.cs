using System;
using Microsoft.Data.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008D6 RID: 2262
	internal class ODataResponseMessage : ODataResponseMessageBase, IODataResponseMessage
	{
		// Token: 0x0600409C RID: 16540 RVA: 0x000B46AA File Offset: 0x000B28AA
		public ODataResponseMessage(HttpResponseData httpResponseData)
			: base(httpResponseData)
		{
		}
	}
}

using System;
using Microsoft.OData.Core;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000893 RID: 2195
	internal sealed class ODataResponseMessage : ODataResponseMessageBase, IODataResponseMessage
	{
		// Token: 0x06003F0B RID: 16139 RVA: 0x000B46AA File Offset: 0x000B28AA
		public ODataResponseMessage(HttpResponseData httpResponseData)
			: base(httpResponseData)
		{
		}
	}
}

using System;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.OData.Core;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000891 RID: 2193
	internal sealed class ODataRequestMessage : ODataRequestMessageBase, IODataRequestMessage
	{
		// Token: 0x06003F02 RID: 16130 RVA: 0x000B3F63 File Offset: 0x000B2163
		public ODataRequestMessage(MashupHttpWebRequest httpWebRequest)
			: base(httpWebRequest)
		{
		}
	}
}

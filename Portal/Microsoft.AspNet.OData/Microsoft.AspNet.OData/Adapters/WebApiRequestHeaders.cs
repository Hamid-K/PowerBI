using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Interfaces;

namespace Microsoft.AspNet.OData.Adapters
{
	// Token: 0x020001DE RID: 478
	internal class WebApiRequestHeaders : IWebApiHeaders
	{
		// Token: 0x06000FA6 RID: 4006 RVA: 0x0003F8A3 File Offset: 0x0003DAA3
		public WebApiRequestHeaders(HttpRequestHeaders headers)
		{
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			this.innerCollection = headers;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003F8C0 File Offset: 0x0003DAC0
		public bool TryGetValues(string key, out IEnumerable<string> values)
		{
			return this.innerCollection.TryGetValues(key, out values);
		}

		// Token: 0x04000452 RID: 1106
		private HttpRequestHeaders innerCollection;
	}
}

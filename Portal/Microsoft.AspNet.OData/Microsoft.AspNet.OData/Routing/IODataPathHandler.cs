using System;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x0200007C RID: 124
	public interface IODataPathHandler
	{
		// Token: 0x060004A1 RID: 1185
		ODataPath Parse(string serviceRoot, string odataPath, IServiceProvider requestContainer);

		// Token: 0x060004A2 RID: 1186
		string Link(ODataPath path);

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060004A3 RID: 1187
		// (set) Token: 0x060004A4 RID: 1188
		ODataUrlKeyDelimiter UrlKeyDelimiter { get; set; }
	}
}

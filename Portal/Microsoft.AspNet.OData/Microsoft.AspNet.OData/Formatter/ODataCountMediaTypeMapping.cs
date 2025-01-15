using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x0200017F RID: 383
	public class ODataCountMediaTypeMapping : MediaTypeMapping
	{
		// Token: 0x06000CBA RID: 3258 RVA: 0x000321FB File Offset: 0x000303FB
		public override double TryMatchMediaType(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return (double)(ODataCountMediaTypeMapping.IsCountRequest(request.ODataProperties().Path) ? 1 : 0);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00032222 File Offset: 0x00030422
		public ODataCountMediaTypeMapping()
			: base("text/plain")
		{
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0003222F File Offset: 0x0003042F
		internal static bool IsCountRequest(Microsoft.AspNet.OData.Routing.ODataPath path)
		{
			return path != null && path.Segments.LastOrDefault<ODataPathSegment>() is CountSegment;
		}
	}
}

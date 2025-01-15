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
	// Token: 0x02000181 RID: 385
	public abstract class ODataRawValueMediaTypeMapping : MediaTypeMapping
	{
		// Token: 0x06000CC5 RID: 3269 RVA: 0x00032364 File Offset: 0x00030564
		public override double TryMatchMediaType(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			Microsoft.AspNet.OData.Routing.ODataPath path = request.ODataProperties().Path;
			return (double)((ODataRawValueMediaTypeMapping.IsRawValueRequest(path) && this.IsMatch(ODataRawValueMediaTypeMapping.GetProperty(path))) ? 1 : 0);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000323A6 File Offset: 0x000305A6
		protected ODataRawValueMediaTypeMapping(string mediaType)
			: base(mediaType)
		{
		}

		// Token: 0x06000CC7 RID: 3271
		protected abstract bool IsMatch(PropertySegment propertySegment);

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000323AF File Offset: 0x000305AF
		internal static bool IsRawValueRequest(Microsoft.AspNet.OData.Routing.ODataPath path)
		{
			return path != null && path.Segments.LastOrDefault<ODataPathSegment>() is ValueSegment;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000323C9 File Offset: 0x000305C9
		private static PropertySegment GetProperty(Microsoft.AspNet.OData.Routing.ODataPath odataPath)
		{
			if (odataPath == null || odataPath.Segments.Count < 2)
			{
				return null;
			}
			return odataPath.Segments[odataPath.Segments.Count - 2] as PropertySegment;
		}
	}
}

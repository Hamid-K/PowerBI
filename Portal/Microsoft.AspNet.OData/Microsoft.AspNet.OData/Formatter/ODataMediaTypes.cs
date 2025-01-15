using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x02000193 RID: 403
	internal static class ODataMediaTypes
	{
		// Token: 0x06000D1A RID: 3354 RVA: 0x0003423C File Offset: 0x0003243C
		public static ODataMetadataLevel GetMetadataLevel(string mediaType, IEnumerable<KeyValuePair<string, string>> parameters)
		{
			if (mediaType == null)
			{
				return ODataMetadataLevel.MinimalMetadata;
			}
			if (!string.Equals(ODataMediaTypes.ApplicationJson, mediaType, StringComparison.Ordinal))
			{
				return ODataMetadataLevel.MinimalMetadata;
			}
			KeyValuePair<string, string> keyValuePair = parameters.FirstOrDefault((KeyValuePair<string, string> p) => string.Equals("odata.metadata", p.Key, StringComparison.OrdinalIgnoreCase));
			if (!keyValuePair.Equals(default(KeyValuePair<string, string>)))
			{
				if (string.Equals("full", keyValuePair.Value, StringComparison.OrdinalIgnoreCase))
				{
					return ODataMetadataLevel.FullMetadata;
				}
				if (string.Equals("none", keyValuePair.Value, StringComparison.OrdinalIgnoreCase))
				{
					return ODataMetadataLevel.NoMetadata;
				}
			}
			return ODataMetadataLevel.MinimalMetadata;
		}

		// Token: 0x040003BF RID: 959
		public static readonly string ApplicationJson = "application/json";

		// Token: 0x040003C0 RID: 960
		public static readonly string ApplicationJsonODataFullMetadata = "application/json;odata.metadata=full";

		// Token: 0x040003C1 RID: 961
		public static readonly string ApplicationJsonODataFullMetadataStreamingFalse = "application/json;odata.metadata=full;odata.streaming=false";

		// Token: 0x040003C2 RID: 962
		public static readonly string ApplicationJsonODataFullMetadataStreamingTrue = "application/json;odata.metadata=full;odata.streaming=true";

		// Token: 0x040003C3 RID: 963
		public static readonly string ApplicationJsonODataMinimalMetadata = "application/json;odata.metadata=minimal";

		// Token: 0x040003C4 RID: 964
		public static readonly string ApplicationJsonODataMinimalMetadataStreamingFalse = "application/json;odata.metadata=minimal;odata.streaming=false";

		// Token: 0x040003C5 RID: 965
		public static readonly string ApplicationJsonODataMinimalMetadataStreamingTrue = "application/json;odata.metadata=minimal;odata.streaming=true";

		// Token: 0x040003C6 RID: 966
		public static readonly string ApplicationJsonODataNoMetadata = "application/json;odata.metadata=none";

		// Token: 0x040003C7 RID: 967
		public static readonly string ApplicationJsonODataNoMetadataStreamingFalse = "application/json;odata.metadata=none;odata.streaming=false";

		// Token: 0x040003C8 RID: 968
		public static readonly string ApplicationJsonODataNoMetadataStreamingTrue = "application/json;odata.metadata=none;odata.streaming=true";

		// Token: 0x040003C9 RID: 969
		public static readonly string ApplicationJsonStreamingFalse = "application/json;odata.streaming=false";

		// Token: 0x040003CA RID: 970
		public static readonly string ApplicationJsonStreamingTrue = "application/json;odata.streaming=true";

		// Token: 0x040003CB RID: 971
		public static readonly string ApplicationXml = "application/xml";
	}
}

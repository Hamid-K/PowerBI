using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200085B RID: 2139
	internal static class ODataConstants
	{
		// Token: 0x04002051 RID: 8273
		public const string DataServiceVersion = "OData-Version";

		// Token: 0x04002052 RID: 8274
		public const string MaxDataServiceVersion = "OData-MaxVersion";

		// Token: 0x04002053 RID: 8275
		public const string DataServiceVersion4Dot0 = "4.0";

		// Token: 0x04002054 RID: 8276
		public const string ODataContext = "@odata.context";

		// Token: 0x04002055 RID: 8277
		public const string ODataId = "@odata.id";

		// Token: 0x04002056 RID: 8278
		public const string ODataEditLink = "@odata.editLink";

		// Token: 0x04002057 RID: 8279
		public const string ODataReadLink = "@odata.readLink";

		// Token: 0x04002058 RID: 8280
		public const string ODataETag = "@odata.etag";

		// Token: 0x04002059 RID: 8281
		public const string ODataTypeName = "@odata.type";

		// Token: 0x0400205A RID: 8282
		public const string IfMatch = "If-Match";

		// Token: 0x0400205B RID: 8283
		public const string Location = "Location";

		// Token: 0x0400205C RID: 8284
		public const string PreferenceHeader = "Prefer";

		// Token: 0x0400205D RID: 8285
		public const string PreferenceAppliedHeader = "Preference-Applied";

		// Token: 0x0400205E RID: 8286
		public const string PreferReturnRepresentationHeader = "return=representation";

		// Token: 0x0400205F RID: 8287
		public const string PreferReturnMinimalHeader = "return=minimal";

		// Token: 0x04002060 RID: 8288
		public static readonly PersistentCacheKey ODataCacheKey = new PersistentCacheKey("OData");

		// Token: 0x04002061 RID: 8289
		public const string AggregationNamespace = "Org.OData.Aggregation.V1";

		// Token: 0x04002062 RID: 8290
		public const string Aggregatable = "Org.OData.Aggregation.V1.Aggregatable";

		// Token: 0x04002063 RID: 8291
		public const string Groupable = "Org.OData.Aggregation.V1.Groupable";
	}
}

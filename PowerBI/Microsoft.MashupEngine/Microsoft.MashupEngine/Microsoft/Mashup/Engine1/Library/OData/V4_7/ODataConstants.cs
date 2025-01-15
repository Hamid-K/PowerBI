using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7
{
	// Token: 0x0200076A RID: 1898
	internal static class ODataConstants
	{
		// Token: 0x04001CF4 RID: 7412
		public const string DataServiceVersion = "OData-Version";

		// Token: 0x04001CF5 RID: 7413
		public const string MaxDataServiceVersion = "OData-MaxVersion";

		// Token: 0x04001CF6 RID: 7414
		public const string DataServiceVersion4Dot0 = "4.0";

		// Token: 0x04001CF7 RID: 7415
		public const string ODataContext = "@odata.context";

		// Token: 0x04001CF8 RID: 7416
		public const string ODataId = "@odata.id";

		// Token: 0x04001CF9 RID: 7417
		public const string ODataEditLink = "@odata.editLink";

		// Token: 0x04001CFA RID: 7418
		public const string ODataReadLink = "@odata.readLink";

		// Token: 0x04001CFB RID: 7419
		public const string ODataETag = "@odata.etag";

		// Token: 0x04001CFC RID: 7420
		public const string ODataTypeName = "@odata.type";

		// Token: 0x04001CFD RID: 7421
		public const string IfMatch = "If-Match";

		// Token: 0x04001CFE RID: 7422
		public const string Location = "Location";

		// Token: 0x04001CFF RID: 7423
		public const string PreferenceHeader = "Prefer";

		// Token: 0x04001D00 RID: 7424
		public const string PreferenceAppliedHeader = "Preference-Applied";

		// Token: 0x04001D01 RID: 7425
		public const string PreferReturnRepresentationHeader = "return=representation";

		// Token: 0x04001D02 RID: 7426
		public const string PreferReturnMinimalHeader = "return=minimal";

		// Token: 0x04001D03 RID: 7427
		public static readonly PersistentCacheKey ODataCacheKey = new PersistentCacheKey("OData");

		// Token: 0x04001D04 RID: 7428
		public const string AggregationNamespace = "Org.OData.Aggregation.V1";

		// Token: 0x04001D05 RID: 7429
		public const string Aggregatable = "Org.OData.Aggregation.V1.Aggregatable";

		// Token: 0x04001D06 RID: 7430
		public const string Groupable = "Org.OData.Aggregation.V1.Groupable";
	}
}

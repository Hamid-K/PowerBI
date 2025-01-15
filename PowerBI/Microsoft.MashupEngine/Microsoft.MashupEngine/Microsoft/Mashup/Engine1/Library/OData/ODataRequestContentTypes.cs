using System;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x0200074B RID: 1867
	internal static class ODataRequestContentTypes
	{
		// Token: 0x04001C7D RID: 7293
		public const string ProposedResultContentTypesAll = "application/json;odata.metadata=minimal;q=1.0,application/json;odata=minimalmetadata;q=0.9,application/atomsvc+xml;q=0.8,application/atom+xml;q=0.8,application/xml;q=0.7,text/plain;q=0.7";

		// Token: 0x04001C7E RID: 7294
		public const string ProposedResultContentTypesV4 = "application/json;odata.metadata=minimal";

		// Token: 0x04001C7F RID: 7295
		public const string ProposedResultContentTypesV3 = "application/json;odata=minimalmetadata;q=1.0,application/atomsvc+xml;q=0.8,application/atom+xml;q=0.8,application/xml;q=0.7,text/plain;q=0.7";

		// Token: 0x04001C80 RID: 7296
		public const string ProposedResultContentTypesV2 = "application/atomsvc+xml;q=0.8,application/atom+xml;q=0.8,application/xml;q=0.7,text/plain;q=0.7";

		// Token: 0x04001C81 RID: 7297
		private const string AtomServiceContentTypes = "application/atomsvc+xml;q=0.8,application/atom+xml;q=0.8,application/xml;q=0.7,text/plain;q=0.7";
	}
}

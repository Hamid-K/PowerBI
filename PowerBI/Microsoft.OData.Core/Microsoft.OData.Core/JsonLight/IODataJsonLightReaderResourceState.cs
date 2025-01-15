using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200022B RID: 555
	internal interface IODataJsonLightReaderResourceState
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001844 RID: 6212
		ODataResourceBase Resource { get; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001845 RID: 6213
		IEdmStructuredType ResourceType { get; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001846 RID: 6214
		// (set) Token: 0x06001847 RID: 6215
		IEdmStructuredType ResourceTypeFromMetadata { get; set; }

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001848 RID: 6216
		IEdmNavigationSource NavigationSource { get; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001849 RID: 6217
		// (set) Token: 0x0600184A RID: 6218
		ODataResourceMetadataBuilder MetadataBuilder { get; set; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600184B RID: 6219
		// (set) Token: 0x0600184C RID: 6220
		bool AnyPropertyFound { get; set; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600184D RID: 6221
		// (set) Token: 0x0600184E RID: 6222
		ODataJsonLightReaderNestedInfo FirstNestedInfo { get; set; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600184F RID: 6223
		PropertyAndAnnotationCollector PropertyAndAnnotationCollector { get; }

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001850 RID: 6224
		SelectedPropertiesNode SelectedProperties { get; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001851 RID: 6225
		List<string> NavigationPropertiesRead { get; }

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001852 RID: 6226
		// (set) Token: 0x06001853 RID: 6227
		bool ProcessingMissingProjectedNestedResourceInfos { get; set; }
	}
}

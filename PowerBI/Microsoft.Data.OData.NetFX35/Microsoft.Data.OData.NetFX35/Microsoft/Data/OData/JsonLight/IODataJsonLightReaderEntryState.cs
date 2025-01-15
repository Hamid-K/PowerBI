using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000181 RID: 385
	internal interface IODataJsonLightReaderEntryState
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A83 RID: 2691
		ODataEntry Entry { get; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A84 RID: 2692
		IEdmEntityType EntityType { get; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A85 RID: 2693
		// (set) Token: 0x06000A86 RID: 2694
		ODataEntityMetadataBuilder MetadataBuilder { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A87 RID: 2695
		// (set) Token: 0x06000A88 RID: 2696
		bool AnyPropertyFound { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000A89 RID: 2697
		// (set) Token: 0x06000A8A RID: 2698
		ODataJsonLightReaderNavigationLinkInfo FirstNavigationLinkInfo { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000A8B RID: 2699
		DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000A8C RID: 2700
		SelectedPropertiesNode SelectedProperties { get; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A8D RID: 2701
		List<string> NavigationPropertiesRead { get; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000A8E RID: 2702
		// (set) Token: 0x06000A8F RID: 2703
		bool ProcessingMissingProjectedNavigationLinks { get; set; }
	}
}

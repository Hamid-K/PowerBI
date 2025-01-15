using System;
using System.Collections.Generic;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000A9 RID: 169
	internal interface IODataJsonLightReaderEntryState
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000626 RID: 1574
		ODataEntry Entry { get; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000627 RID: 1575
		IEdmEntityType EntityType { get; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000628 RID: 1576
		// (set) Token: 0x06000629 RID: 1577
		ODataEntityMetadataBuilder MetadataBuilder { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600062A RID: 1578
		// (set) Token: 0x0600062B RID: 1579
		bool AnyPropertyFound { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600062C RID: 1580
		// (set) Token: 0x0600062D RID: 1581
		ODataJsonLightReaderNavigationLinkInfo FirstNavigationLinkInfo { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600062E RID: 1582
		DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600062F RID: 1583
		SelectedPropertiesNode SelectedProperties { get; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000630 RID: 1584
		List<string> NavigationPropertiesRead { get; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000631 RID: 1585
		// (set) Token: 0x06000632 RID: 1586
		bool ProcessingMissingProjectedNavigationLinks { get; set; }
	}
}

using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x0200020D RID: 525
	internal interface IODataVerboseJsonReaderEntryState
	{
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000F35 RID: 3893
		ODataEntry Entry { get; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000F36 RID: 3894
		IEdmEntityType EntityType { get; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000F37 RID: 3895
		// (set) Token: 0x06000F38 RID: 3896
		bool MetadataPropertyFound { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000F39 RID: 3897
		// (set) Token: 0x06000F3A RID: 3898
		ODataNavigationLink FirstNavigationLink { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000F3B RID: 3899
		// (set) Token: 0x06000F3C RID: 3900
		IEdmNavigationProperty FirstNavigationProperty { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000F3D RID: 3901
		DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; }
	}
}

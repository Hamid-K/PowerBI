using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x020001F2 RID: 498
	internal interface IODataJsonLightReaderResourceState
	{
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001371 RID: 4977
		ODataResource Resource { get; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001372 RID: 4978
		IEdmStructuredType ResourceType { get; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001373 RID: 4979
		// (set) Token: 0x06001374 RID: 4980
		IEdmStructuredType ResourceTypeFromMetadata { get; set; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001375 RID: 4981
		IEdmNavigationSource NavigationSource { get; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001376 RID: 4982
		// (set) Token: 0x06001377 RID: 4983
		ODataResourceMetadataBuilder MetadataBuilder { get; set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001378 RID: 4984
		// (set) Token: 0x06001379 RID: 4985
		bool AnyPropertyFound { get; set; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600137A RID: 4986
		// (set) Token: 0x0600137B RID: 4987
		ODataJsonLightReaderNestedResourceInfo FirstNestedResourceInfo { get; set; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600137C RID: 4988
		PropertyAndAnnotationCollector PropertyAndAnnotationCollector { get; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600137D RID: 4989
		SelectedPropertiesNode SelectedProperties { get; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600137E RID: 4990
		List<string> NavigationPropertiesRead { get; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600137F RID: 4991
		// (set) Token: 0x06001380 RID: 4992
		bool ProcessingMissingProjectedNestedResourceInfos { get; set; }
	}
}

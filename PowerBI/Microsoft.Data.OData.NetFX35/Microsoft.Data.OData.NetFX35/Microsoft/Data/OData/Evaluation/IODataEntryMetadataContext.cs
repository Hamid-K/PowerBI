using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000101 RID: 257
	internal interface IODataEntryMetadataContext
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060006BA RID: 1722
		ODataEntry Entry { get; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060006BB RID: 1723
		IODataFeedAndEntryTypeContext TypeContext { get; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060006BC RID: 1724
		string ActualEntityTypeName { get; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060006BD RID: 1725
		ICollection<KeyValuePair<string, object>> KeyProperties { get; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060006BE RID: 1726
		IEnumerable<KeyValuePair<string, object>> ETagProperties { get; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060006BF RID: 1727
		IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties { get; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060006C0 RID: 1728
		IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties { get; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006C1 RID: 1729
		IEnumerable<IEdmFunctionImport> SelectedAlwaysBindableOperations { get; }
	}
}

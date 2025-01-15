using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x0200025D RID: 605
	internal interface IODataResourceMetadataContext
	{
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001B44 RID: 6980
		ODataResourceBase Resource { get; }

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001B45 RID: 6981
		IODataResourceTypeContext TypeContext { get; }

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001B46 RID: 6982
		string ActualResourceTypeName { get; }

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001B47 RID: 6983
		ICollection<KeyValuePair<string, object>> KeyProperties { get; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001B48 RID: 6984
		IEnumerable<KeyValuePair<string, object>> ETagProperties { get; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001B49 RID: 6985
		IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties { get; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001B4A RID: 6986
		IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties { get; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001B4B RID: 6987
		IEnumerable<IEdmOperation> SelectedBindableOperations { get; }
	}
}

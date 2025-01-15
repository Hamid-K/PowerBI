using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000222 RID: 546
	internal interface IODataResourceMetadataContext
	{
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001623 RID: 5667
		ODataResource Resource { get; }

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001624 RID: 5668
		IODataResourceTypeContext TypeContext { get; }

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001625 RID: 5669
		string ActualResourceTypeName { get; }

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001626 RID: 5670
		ICollection<KeyValuePair<string, object>> KeyProperties { get; }

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001627 RID: 5671
		IEnumerable<KeyValuePair<string, object>> ETagProperties { get; }

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001628 RID: 5672
		IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties { get; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001629 RID: 5673
		IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties { get; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600162A RID: 5674
		IEnumerable<IEdmOperation> SelectedBindableOperations { get; }
	}
}

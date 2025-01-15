using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x02000076 RID: 118
	internal interface IODataEntryMetadataContext
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004CB RID: 1227
		ODataEntry Entry { get; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004CC RID: 1228
		IODataFeedAndEntryTypeContext TypeContext { get; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004CD RID: 1229
		string ActualEntityTypeName { get; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004CE RID: 1230
		ICollection<KeyValuePair<string, object>> KeyProperties { get; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004CF RID: 1231
		IEnumerable<KeyValuePair<string, object>> ETagProperties { get; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004D0 RID: 1232
		IEnumerable<IEdmNavigationProperty> SelectedNavigationProperties { get; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004D1 RID: 1233
		IDictionary<string, IEdmStructuralProperty> SelectedStreamProperties { get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004D2 RID: 1234
		IEnumerable<IEdmOperation> SelectedBindableOperations { get; }
	}
}

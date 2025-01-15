using System;
using System.Collections.Generic;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000E7 RID: 231
	internal interface IReadOnlyDefaultContextManager
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000DE5 RID: 3557
		bool IsEmpty { get; }

		// Token: 0x06000DE6 RID: 3558
		IEnumerable<IEdmFieldInstance> GetFieldsRequiringClearDefaultFilterContext();

		// Token: 0x06000DE7 RID: 3559
		IEnumerable<IConceptualColumn> GetColumnsRequiringClearDefaultFilterContext();
	}
}

using System;
using Microsoft.Data.Metadata.Edm;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000218 RID: 536
	internal interface IEdmItemLookup
	{
		// Token: 0x060018CF RID: 6351
		EdmType LookupEdmType(EdmType edmType);
	}
}

using System;
using System.Collections.ObjectModel;
using Microsoft.PowerBI.Query.Contracts.DaxCapabilities;

namespace Microsoft.PowerBI.ExploreHost.DAX
{
	// Token: 0x02000093 RID: 147
	internal interface IDaxCapabilitiesManager
	{
		// Token: 0x060003C0 RID: 960
		ReadOnlyCollection<DaxFunction> GetDaxFunctions(string databaseID);
	}
}

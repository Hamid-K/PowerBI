using System;
using Microsoft.PowerBI.Query.Contracts.DaxCapabilities;

namespace Microsoft.PowerBI.ExploreHost.DAX
{
	// Token: 0x02000092 RID: 146
	public interface IDaxCapabilitiesHandler
	{
		// Token: 0x060003BF RID: 959
		DaxCapabilities GetDaxCapabilities(string databaseID);
	}
}

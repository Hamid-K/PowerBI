using System;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000013 RID: 19
	public interface IExplorationExtractor
	{
		// Token: 0x0600005C RID: 92
		ServiceExploration GetServiceDependentMetadata(ExplorationContract exploration);

		// Token: 0x0600005D RID: 93
		ServiceExploration GetServiceDependentMetadata(string exploration);
	}
}

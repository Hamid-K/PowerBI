using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.BusinessIntelligence;
using Microsoft.PowerBI.ExploreHost.DAX;
using Microsoft.PowerBI.ExploreHost.DocumentConversion;
using Microsoft.PowerBI.ExploreHost.Insights;
using Microsoft.PowerBI.ExploreHost.Lucia;
using Microsoft.PowerBI.ExploreHost.SemanticQuery;

namespace Microsoft.PowerBI.ExploreHost
{
	// Token: 0x0200002B RID: 43
	public interface IExploreClient : ISemanticQueryHandler, IConceptualSchemaRetriever, IExplorationConversionHandler, IInsightsHandler, IDaxCapabilitiesHandler, ILuciaHandler
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600014C RID: 332
		FeatureSwitches FeatureSwitches { get; }

		// Token: 0x0600014D RID: 333
		DataSet GetSchemaDataSet(string databaseID, string schemaName, IReadOnlyDictionary<string, object> restrictions);
	}
}

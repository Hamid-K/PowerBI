using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.PowerBI.ExploreHost;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x02000013 RID: 19
	public interface IRSExploreHost : IDisposable
	{
		// Token: 0x06000071 RID: 113
		Task<string> ExecuteSemanticQueryAsync(string modelId, string clientSessionId, string request, ASQueryLimits asQueryLimits);

		// Token: 0x06000072 RID: 114
		Task<ExportDataMetadata> ExecuteExportDataQueryAsync(string databaseId, string clientSessionId, string jsonCommands, Stream output, ASQueryLimits asQueryLimits);

		// Token: 0x06000073 RID: 115
		Task<string> GetConceptualSchemaAsync(string modelId, string clientSessionId, string request);
	}
}

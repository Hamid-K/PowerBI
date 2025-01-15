using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PowerBI.Data.ModelSchemaAnalysis;

namespace Microsoft.PowerBI.ExploreHost.Insights
{
	// Token: 0x02000080 RID: 128
	public interface IInsightsHandler
	{
		// Token: 0x0600036C RID: 876
		Task<string> DeriveInsightsAsync(string request, string databaseID, CancellationToken cancellationToken, IMeasureExpressionProvider expressionProvider = null, IModel model = null);

		// Token: 0x0600036D RID: 877
		Task<string> ExecuteAnalysisAsync(string request, string databaseID);

		// Token: 0x0600036E RID: 878
		void CancelAnalysis(string request, string databaseID);
	}
}

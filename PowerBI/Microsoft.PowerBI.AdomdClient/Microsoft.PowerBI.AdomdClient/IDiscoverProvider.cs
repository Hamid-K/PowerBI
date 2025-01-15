using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200008F RID: 143
	internal interface IDiscoverProvider
	{
		// Token: 0x0600089C RID: 2204
		void Discover(string requestType, IDictionary restrictions, DataTable table);

		// Token: 0x0600089D RID: 2205
		void DiscoverData(string requestType, IDictionary restrictions, DataTable table);

		// Token: 0x0600089E RID: 2206
		RowsetFormatter Discover(string requestType, IDictionary restrictions);
	}
}

using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200008F RID: 143
	internal interface IDiscoverProvider
	{
		// Token: 0x060008A9 RID: 2217
		void Discover(string requestType, IDictionary restrictions, DataTable table);

		// Token: 0x060008AA RID: 2218
		void DiscoverData(string requestType, IDictionary restrictions, DataTable table);

		// Token: 0x060008AB RID: 2219
		RowsetFormatter Discover(string requestType, IDictionary restrictions);
	}
}

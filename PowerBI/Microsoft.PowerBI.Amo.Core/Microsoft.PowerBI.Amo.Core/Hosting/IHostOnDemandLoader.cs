using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.Hosting
{
	// Token: 0x0200015D RID: 349
	[Guid("8DC885C2-F3F9-4a99-BCC9-70401560E952")]
	public interface IHostOnDemandLoader
	{
		// Token: 0x060011BA RID: 4538
		bool LoadCollectionOnDemand(object loadableCollection, object context);
	}
}

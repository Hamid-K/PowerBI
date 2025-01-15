using System;
using System.IO;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200008D RID: 141
	internal interface ICommandContentProvider
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060008A5 RID: 2213
		string CommandText { get; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060008A6 RID: 2214
		Stream CommandStream { get; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060008A7 RID: 2215
		bool IsContentMdx { get; }
	}
}

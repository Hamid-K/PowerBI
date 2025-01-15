using System;
using System.IO;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200008D RID: 141
	internal interface ICommandContentProvider
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000898 RID: 2200
		string CommandText { get; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000899 RID: 2201
		Stream CommandStream { get; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600089A RID: 2202
		bool IsContentMdx { get; }
	}
}

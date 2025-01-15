using System;

namespace Microsoft.AnalysisServices.Core
{
	// Token: 0x020000E6 RID: 230
	internal interface IObjectReference
	{
		// Token: 0x06000E61 RID: 3681
		string Serialize();

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06000E62 RID: 3682
		bool IsValid { get; }
	}
}

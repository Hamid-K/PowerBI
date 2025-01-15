using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000C6 RID: 198
	public interface IBlock : IIdentifiable, IShuttable
	{
		// Token: 0x060005A6 RID: 1446
		BlockInitializationStatus Initialize(IBlockHost blockHost);

		// Token: 0x060005A7 RID: 1447
		void Start();
	}
}

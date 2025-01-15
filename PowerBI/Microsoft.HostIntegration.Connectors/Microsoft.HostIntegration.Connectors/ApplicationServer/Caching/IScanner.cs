using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000235 RID: 565
	internal interface IScanner
	{
		// Token: 0x060012C0 RID: 4800
		bool Scan(object item);

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060012C1 RID: 4801
		bool BatchCompleted { get; }

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060012C2 RID: 4802
		bool InvalidateOnChange { get; }
	}
}

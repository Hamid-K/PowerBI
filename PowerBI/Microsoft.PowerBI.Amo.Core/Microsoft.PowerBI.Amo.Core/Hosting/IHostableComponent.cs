using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.Hosting
{
	// Token: 0x0200015A RID: 346
	[Guid("6173411B-3BB9-476e-A94F-9F6BD3F2929D")]
	public interface IHostableComponent
	{
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060011B2 RID: 4530
		// (set) Token: 0x060011B3 RID: 4531
		IServiceProvider Host { get; set; }
	}
}

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000095 RID: 149
	[Guid("BBCC5A46-0217-4a45-ACAE-189239F101BF")]
	public interface INamedComponent : IModelComponent, IComponent, IDisposable
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000734 RID: 1844
		// (set) Token: 0x06000735 RID: 1845
		string ID { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000736 RID: 1846
		// (set) Token: 0x06000737 RID: 1847
		string Name { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000738 RID: 1848
		// (set) Token: 0x06000739 RID: 1849
		string Description { get; set; }
	}
}

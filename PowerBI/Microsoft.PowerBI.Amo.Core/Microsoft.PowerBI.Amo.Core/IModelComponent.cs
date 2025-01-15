using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000093 RID: 147
	[Guid("3DA9FC8D-5AEA-404e-9C73-F9F393B9FFD3")]
	public interface IModelComponent : IComponent, IDisposable
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600072C RID: 1836
		// (set) Token: 0x0600072D RID: 1837
		IModelComponentCollection OwningCollection { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600072E RID: 1838
		IModelComponent Parent { get; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600072F RID: 1839
		string FriendlyPath { get; }
	}
}

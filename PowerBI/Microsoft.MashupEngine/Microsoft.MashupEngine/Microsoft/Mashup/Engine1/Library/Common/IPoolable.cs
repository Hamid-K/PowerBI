using System;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010D5 RID: 4309
	internal interface IPoolable : IDisposable
	{
		// Token: 0x17001FB1 RID: 8113
		// (get) Token: 0x060070DB RID: 28891
		string Key { get; }

		// Token: 0x17001FB2 RID: 8114
		// (get) Token: 0x060070DC RID: 28892
		bool IsValid { get; }
	}
}

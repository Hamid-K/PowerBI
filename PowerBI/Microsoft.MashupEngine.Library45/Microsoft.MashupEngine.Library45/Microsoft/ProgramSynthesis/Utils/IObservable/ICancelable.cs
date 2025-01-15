using System;

namespace Microsoft.ProgramSynthesis.Utils.IObservable
{
	// Token: 0x02000646 RID: 1606
	public interface ICancelable : IDisposable
	{
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060022F3 RID: 8947
		bool IsDisposed { get; }
	}
}

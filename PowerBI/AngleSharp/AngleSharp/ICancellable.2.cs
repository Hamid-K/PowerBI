using System;

namespace AngleSharp
{
	// Token: 0x0200000D RID: 13
	public interface ICancellable
	{
		// Token: 0x06000064 RID: 100
		void Cancel();

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000065 RID: 101
		bool IsCompleted { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000066 RID: 102
		bool IsRunning { get; }
	}
}

using System;

namespace Microsoft.Data.Serialization
{
	// Token: 0x0200014E RID: 334
	public interface IConcurrentPage : IPage, IDisposable
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060005E4 RID: 1508
		bool IsBufferedForRead { get; }

		// Token: 0x060005E5 RID: 1509
		void BufferForRead();
	}
}

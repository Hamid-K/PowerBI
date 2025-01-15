using System;

namespace System.Buffers
{
	// Token: 0x02000027 RID: 39
	public interface IMemoryOwner<T> : IDisposable
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001D9 RID: 473
		Memory<T> Memory { get; }
	}
}

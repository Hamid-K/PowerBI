using System;

namespace System.Buffers
{
	// Token: 0x020000E5 RID: 229
	internal interface IMemoryOwner<T> : IDisposable
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000819 RID: 2073
		Memory<T> Memory { get; }
	}
}

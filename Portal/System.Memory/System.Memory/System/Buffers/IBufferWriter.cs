using System;

namespace System.Buffers
{
	// Token: 0x0200001E RID: 30
	public interface IBufferWriter<T>
	{
		// Token: 0x06000183 RID: 387
		void Advance(int count);

		// Token: 0x06000184 RID: 388
		Memory<T> GetMemory(int sizeHint = 0);

		// Token: 0x06000185 RID: 389
		Span<T> GetSpan(int sizeHint = 0);
	}
}

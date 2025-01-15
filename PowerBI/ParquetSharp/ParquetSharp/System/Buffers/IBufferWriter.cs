using System;

namespace System.Buffers
{
	// Token: 0x020000DC RID: 220
	internal interface IBufferWriter<T>
	{
		// Token: 0x060007C3 RID: 1987
		void Advance(int count);

		// Token: 0x060007C4 RID: 1988
		Memory<T> GetMemory(int sizeHint = 0);

		// Token: 0x060007C5 RID: 1989
		Span<T> GetSpan(int sizeHint = 0);
	}
}

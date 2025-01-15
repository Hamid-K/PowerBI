using System;

namespace System.Buffers
{
	// Token: 0x02000026 RID: 38
	public interface IPinnable
	{
		// Token: 0x060001D7 RID: 471
		MemoryHandle Pin(int elementIndex);

		// Token: 0x060001D8 RID: 472
		void Unpin();
	}
}

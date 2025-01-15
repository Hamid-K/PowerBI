using System;

namespace System.Buffers
{
	// Token: 0x020000E4 RID: 228
	internal interface IPinnable
	{
		// Token: 0x06000817 RID: 2071
		MemoryHandle Pin(int elementIndex);

		// Token: 0x06000818 RID: 2072
		void Unpin();
	}
}

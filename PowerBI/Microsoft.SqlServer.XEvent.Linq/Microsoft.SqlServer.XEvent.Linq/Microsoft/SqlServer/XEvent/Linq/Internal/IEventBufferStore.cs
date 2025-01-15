using System;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x020000DB RID: 219
	internal interface IEventBufferStore
	{
		// Token: 0x060002EE RID: 750
		bool GetNextBuffer(BufferLocator prevBufferId, out BufferLocator bufferId, out byte[] buffer);

		// Token: 0x060002EF RID: 751
		bool GetBuffer(BufferLocator bufferId, out byte[] buffer);

		// Token: 0x060002F0 RID: 752
		bool GetBufferDirect(BufferLocator bufferId, int bufferSize, out byte[] buffer);

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002F1 RID: 753
		Exception LastException { get; }
	}
}

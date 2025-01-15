using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x0200005E RID: 94
	internal static class BufferUtils
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x000168B3 File Offset: 0x00014AB3
		public static char[] RentBuffer([Nullable(2)] IArrayPool<char> bufferPool, int minSize)
		{
			if (bufferPool == null)
			{
				return new char[minSize];
			}
			return bufferPool.Rent(minSize);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000168C6 File Offset: 0x00014AC6
		[NullableContext(2)]
		public static void ReturnBuffer(IArrayPool<char> bufferPool, char[] buffer)
		{
			if (bufferPool != null)
			{
				bufferPool.Return(buffer);
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000168D2 File Offset: 0x00014AD2
		[NullableContext(2)]
		[return: Nullable(0)]
		public static char[] EnsureBufferSize(IArrayPool<char> bufferPool, int size, char[] buffer)
		{
			if (bufferPool == null)
			{
				return new char[size];
			}
			if (buffer != null)
			{
				bufferPool.Return(buffer);
			}
			return bufferPool.Rent(size);
		}
	}
}

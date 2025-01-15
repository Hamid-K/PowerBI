using System;

namespace Microsoft.OData.Buffers
{
	// Token: 0x02000269 RID: 617
	internal static class BufferUtils
	{
		// Token: 0x06001BDF RID: 7135 RVA: 0x00055A80 File Offset: 0x00053C80
		public static char[] InitializeBufferIfRequired(char[] buffer)
		{
			return BufferUtils.InitializeBufferIfRequired(null, buffer);
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x00055A89 File Offset: 0x00053C89
		public static char[] InitializeBufferIfRequired(ICharArrayPool bufferPool, char[] buffer)
		{
			if (buffer != null)
			{
				return buffer;
			}
			return BufferUtils.RentFromBuffer(bufferPool, 128);
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x00055A9C File Offset: 0x00053C9C
		public static char[] RentFromBuffer(ICharArrayPool bufferPool, int minSize)
		{
			if (bufferPool == null)
			{
				return new char[minSize];
			}
			char[] array = bufferPool.Rent(minSize);
			if (array == null || array.Length < minSize)
			{
				throw new ODataException(Strings.BufferUtils_InvalidBufferOrSize(minSize));
			}
			return array;
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x00055AD6 File Offset: 0x00053CD6
		public static void ReturnToBuffer(ICharArrayPool bufferPool, char[] buffer)
		{
			if (bufferPool != null)
			{
				bufferPool.Return(buffer);
			}
		}

		// Token: 0x04000BA0 RID: 2976
		private const int BufferLength = 128;
	}
}

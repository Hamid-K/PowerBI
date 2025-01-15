using System;
using System.Buffers;

namespace System.IO
{
	// Token: 0x02000031 RID: 49
	internal static class TextWriterExtensions
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x00006398 File Offset: 0x00004598
		public static void WritePartialString(this TextWriter writer, string value, int offset, int count)
		{
			if (offset == 0 && count == value.Length)
			{
				writer.Write(value);
				return;
			}
			ReadOnlySpan<char> readOnlySpan = value.AsSpan(offset, count);
			char[] array = ArrayPool<char>.Shared.Rent(readOnlySpan.Length);
			readOnlySpan.CopyTo(array);
			writer.Write(array, 0, readOnlySpan.Length);
			ArrayPool<char>.Shared.Return(array, false);
		}
	}
}

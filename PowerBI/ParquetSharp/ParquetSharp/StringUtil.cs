using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace ParquetSharp
{
	// Token: 0x0200008E RID: 142
	[NullableContext(1)]
	[Nullable(0)]
	internal static class StringUtil
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		public unsafe static IntPtr ToCStringUtf8(string str, ByteBuffer byteBuffer)
		{
			Encoding utf = Encoding.UTF8;
			int byteCount = utf.GetByteCount(str);
			ByteArray byteArray = byteBuffer.Allocate(byteCount + 1);
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				utf.GetBytes(ptr, str.Length, (byte*)(void*)byteArray.Pointer, byteCount);
			}
			return byteArray.Pointer;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000EA40 File Offset: 0x0000CC40
		public static string PtrToStringUtf8(IntPtr ptr)
		{
			string text = StringUtil.PtrToNullableStringUtf8(ptr);
			if (text == null)
			{
				throw new ArgumentNullException("ptr");
			}
			return text;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		[NullableContext(2)]
		public unsafe static string PtrToNullableStringUtf8(IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			byte* ptr2 = (byte*)(void*)ptr;
			int num = 0;
			while (ptr2[num] != 0)
			{
				num++;
			}
			return Encoding.UTF8.GetString(ptr2, num);
		}
	}
}

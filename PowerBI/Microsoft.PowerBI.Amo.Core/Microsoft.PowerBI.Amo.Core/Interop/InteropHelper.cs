using System;

namespace Microsoft.AnalysisServices.Interop
{
	// Token: 0x0200012B RID: 299
	internal static class InteropHelper
	{
		// Token: 0x0600105A RID: 4186 RVA: 0x00038FA6 File Offset: 0x000371A6
		public static IntPtr Add(IntPtr pointer, int offset)
		{
			return IntPtr.Add(pointer, offset);
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00038FB0 File Offset: 0x000371B0
		public static bool IsIncludedInBuffer(IntPtr buffer, int size, IntPtr pointer, out int offset)
		{
			long num = pointer.ToInt64() - buffer.ToInt64();
			if (num >= 0L && num < (long)size)
			{
				offset = (int)num;
				return true;
			}
			offset = -1;
			return false;
		}
	}
}

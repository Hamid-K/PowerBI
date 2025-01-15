using System;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x02000136 RID: 310
	internal static class InteropHelper
	{
		// Token: 0x06000FBF RID: 4031 RVA: 0x00036372 File Offset: 0x00034572
		public static IntPtr Add(IntPtr pointer, int offset)
		{
			return IntPtr.Add(pointer, offset);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0003637C File Offset: 0x0003457C
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
